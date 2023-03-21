﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaszynaPi.MachineLogic;
using MaszynaPi.CommonOperations;
using System.Text.RegularExpressions;

namespace MaszynaPi.MachineAssembler {

    class CompilerException : Exception { public CompilerException(string message) : base(message) { } }

    /* Class responsible for compilation - 
     * changing the string from the Code Editor to a form understandable for the machine.
     * In this case, list of numbers as (opcode << addressBits & argument) 
     */
    /* Compiler uses the static class InstructionSetDecoder - instructions opcodes are  
     * identified by searching for them in the opcode map (Dictionary InstructionNames) 
     * The code is stored in two versions: 
     *      1. Loaded as numbers into memory            [List<uint>]
     *      2. Translated into a sequence of signals    [List<List<string>>] [OLD - UNUSED]
     */
    static class Assembler {
        public const string PROGRAM_FILE_EXTENSION = ".prg";
        public const string HEADER_LABEL_END = ":";    // [Header] End of assembly label (foo) definition
        const string CHAR_SYMBOL = "'"; 

        const int POS_LABEL = 0;
        const int POS_VALUE = 2;
        
        static bool FLAG_COMPILED = false;

        static List<uint> MachineCode = new List<uint>();
        static List<List<string>> ProgramSignals;

        // Dictionary that maps each address of code instruction in memory to line in editor (its content)
        static Dictionary<uint, string> MemoryEditorMap = new Dictionary<uint, string>();

        public static Dictionary<uint, string> GetMemoryEditorMap() {
            return MemoryEditorMap;
        }

        static string DeleteMultipleSpaces(string s) {
            return Regex.Replace(s, @"\s+", " ");
        }

        public static bool ContainsInstruction(string line) {
            return InstructionLoader.GetInstructionsNamesOpcodes().Keys.Any(line.Contains);
        }

        public static bool IsMemoryAlocation(string line) {
            return line.Contains(Defines.HEADER_MEM_ALLOC);
        }
        public static bool IsConstDeclaration(string line) {
            return line.Contains(Defines.HEADER_CONST_VAR);
        }

        static bool ContainsProcedureLabel(string line) {
            return (line.Contains(HEADER_LABEL_END) && (!line.Contains(Defines.HEADER_CONST_VAR)) && (!line.Contains(Defines.HEADER_MEM_ALLOC)));
        }
        static string GetLableName(string line) {
            line = DeleteMultipleSpaces(line);
            return line.Replace(" ", "").Split(HEADER_LABEL_END[0])[POS_LABEL].Replace(HEADER_LABEL_END, "");
        }
        //Modifies List from argument to not contain labels in any line 
        static void RemoveLabels(List<string> codeLines) {
            for (int i = 0; i < codeLines.Count; i++) {
                if (codeLines[i].Contains(HEADER_LABEL_END)) {
                    codeLines[i] = codeLines[i].Remove(0, codeLines[i].IndexOf(HEADER_LABEL_END) + 1);
                }
                codeLines[i] = codeLines[i].Trim();
            }
            codeLines.RemoveAll(string.IsNullOrEmpty);
            codeLines.RemoveAll(string.IsNullOrWhiteSpace);
        }

        // Returns program size - lines, declarations
        //(properly if codeLines were get from CodeEditor().proccessCode() method)
        static int GetProgramSize(List<string> codeLines) {
            var codeCopy = new List<string>(codeLines);
            RemoveLabels(codeCopy);
            return codeCopy.Count();
        }

        // Gets adressses for Procedures, Variables and Constans Labels 
        // Returns dictionary with key=NameOfLabel value=Address
        static Dictionary<string,uint> GetLabelsAddresses(List<string> codeLines) {
            var codeLabels = new Dictionary<string, uint>();
            bool wasLabel = false;
            int address = -1;
            string label = "";
            foreach(var line in codeLines) {
                if (line.Contains(HEADER_LABEL_END) == false && wasLabel == false) {
                    address++;
                    continue;
                }
                if (line.EndsWith(":")) { // only label name
                    wasLabel = true;
                    label = GetLableName(line);
                } else {
                    address++;
                    if (wasLabel == false) label = GetLableName(line);
                    if (codeLabels.ContainsKey(label)) throw new CompilerException("[Syntax error]) Duplicate label: " + label);
                    codeLabels.Add(label, (uint)address);
                    wasLabel = false;
                }
            }
            return codeLabels;
        }

        // Returns compiled program as list of unsigned integer values to be load into Machine memory
        public static List<uint> CompileCode(List<string> codeLines) {
            int progSize = GetProgramSize(codeLines);
            if (progSize > ArchitectureSettings.GetMaxAddress()) 
                throw new CompilerException("[Compilation Error] Too large program for current architecture settings. Increase address space!");

            var LabelsAddresses = GetLabelsAddresses(codeLines);
            var namesOpcodes = InstructionLoader.GetInstructionsNamesOpcodes();

            MachineCode.Clear();
            RemoveLabels(codeLines);

            MemoryEditorMap.Clear();
            uint memAddress = 0;

            foreach (string line in codeLines) {
                var instArg = line.Split(' ');
                string instruction = null;
                string argument = null;
                int arg = -1;

                MemoryEditorMap.Add(memAddress,line); // Mapping to editor lines
                memAddress++;

                if (instArg.Length == 1) { // No argument instruction
                    if (IsMemoryAlocation(line)) { MachineCode.Add(Defines.DEFAULT_MEM_VAL); continue; } // memory allocation (RES)
                    instruction = namesOpcodes.Keys.FirstOrDefault(toCheck => instArg[0].Equals(toCheck));
                    if (instruction == null)
                        throw new CompilerException("[Syntax error] Unknown instruction label in: " + line);
                    if (InstructionLoader.GetZeroArgInstructions().Contains(instArg[0]) == false)
                        throw new CompilerException("[Syntax error] Missing argument for instruction " + line);
                   
                    MachineCode.Add(Bitwise.EncodeInstruction(namesOpcodes[instruction], 0)); // 0 arg instruction add
                    continue;
                }
                if (instArg.Length == 2) { // one argument instruction (max)
                    argument = LabelsAddresses.Keys.FirstOrDefault(toCheck => instArg[1].Equals(toCheck));
                    if (argument == null) { // argument is not defined label
                        if (instArg[1].StartsWith(CHAR_SYMBOL) && instArg[1].EndsWith(CHAR_SYMBOL)) { // If argument is "char" type 
                            if (instArg[1].Length > 3)
                                throw new CompilerException("[Syntax Error] RST type of 'char' can be only one character long!");
                            arg = Encoding.ASCII.GetBytes(instArg[1].Replace(CHAR_SYMBOL, ""))[0];
                        } else {
                            if (int.TryParse(instArg[1], out arg) == false) // Is argument a number?
                                throw new CompilerException("[Syntax error] Invalid argument in: " + line); // no.
                        }

                        arg = (int)(arg & ArchitectureSettings.GetMaxWord()); // Handle Overflow instead throwing exception

                        if (IsConstDeclaration(line)) { MachineCode.Add((uint)arg); continue; } // Variable label define (DEF) - argument is number

                        instruction = namesOpcodes.Keys.FirstOrDefault(toCheck => instArg[0].Equals(toCheck));
                        if (instruction == null)
                            throw new CompilerException("[Syntax error] Unknown instruction label in: " + line);
                        MachineCode.Add(Bitwise.EncodeInstruction(namesOpcodes[instruction], (uint)arg)); //Instruction and Number argument [Direct addressing]
                        continue;
                    }
                    if (IsConstDeclaration(line)) { MachineCode.Add(LabelsAddresses[argument]); continue; } //Variable label define (DEF) - argument is label
                    instruction = namesOpcodes.Keys.FirstOrDefault(toCheck => instArg[0].Equals(toCheck));
                    if (instruction == null)
                        throw new CompilerException("[Syntax error] Unknown instruction label in: " + line);
                    MachineCode.Add(Bitwise.EncodeInstruction(namesOpcodes[instruction], LabelsAddresses[argument])); // Instruction and Label argument 
                    continue;

                }
                throw new CompilerException("[Syntax error] To many arguments in line: " + line);
            }
            return MachineCode;
        }
    
    }

}