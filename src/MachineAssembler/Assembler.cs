using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaszynaPi.MachineLogic;
using MaszynaPi.CommonOperations;
using System.Text.RegularExpressions;

namespace MaszynaPi.MachineAssembler {

    class CompilerException : Exception { public CompilerException(string message) : base(message) { } }

    /// <summary>
    /// Class responsible for compilation - 
    /// changing the string from the Code Editor to a form understandable for the machine.
    /// In this case, list of numbers as (opcode shifted left by addressBits bitwaise anded with argument) 
    /// 
    /// Compiler uses the static class InstructionSetDecoder - instructions opcodes are  
    /// identified by searching for them in the opcode map (Dictionary InstructionNames) 
    /// The code is stored in two versions: 
    /// <br></br>    1. Loaded as numbers into memory            [List of uints]
    /// <br></br>    2. Translated into a sequence of signals    [List of Lists of strings][OLD - UNUSED] 
    /// </summary>
    static class Assembler {
        public const string PROGRAM_FILE_EXTENSION = ".prg";
        public const string HEADER_LABEL_END = ":";    // [Header] End of assembly label (foo) definition
        const string CHAR_SYMBOL = "'"; 

        const int POS_LABEL = 0;

        static readonly List<uint> MachineCode = new List<uint>();

        // Dictionary that maps each address of code instruction in memory to line in editor (its content)
        static readonly Dictionary<uint, string> MemoryEditorMap = new Dictionary<uint, string>();

        public static Dictionary<uint, string> GetMemoryEditorMap() {
            return MemoryEditorMap;
        }
        /// <summary>Replaces all multi-spaces instances with single space character.</summary>
        /// <param name="s">Input string</param>
        /// <returns>New, processed string with single spaces.</returns>
        static string DeleteMultipleSpaces(string s) {
            return Regex.Replace(s, @"\s+", " ");
        }

        /// <summary>Checks if passed <paramref name="line"/> contais <see cref="Defines.KEYWORD_MEM_ALLOC"/> value. </summary>
        public static bool IsMemoryAlocation(string line) {
            return line.Contains(Defines.KEYWORD_MEM_ALLOC);
        }
        /// <summary>Checks if passed <paramref name="line"/> contais <see cref="Defines.KEYWORD_CONST_VAR"/> value. </summary>
        public static bool IsConstDeclaration(string line) {
            return line.Contains(Defines.KEYWORD_CONST_VAR);
        }
        /// <summary>Returns label of variable component of line.</summary>
        static string GetLableName(string line) {
            line = DeleteMultipleSpaces(line);
            return line.Replace(" ", "").Split(HEADER_LABEL_END[0])[POS_LABEL].Replace(HEADER_LABEL_END, "");
        }

        ///<summary>Modifies <paramref name="codeLines"/> argument to not contain labels in any line </summary>
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

        /// <summary>
        /// Returns expected program size after compilation - sum of lines, declarations
        /// <b>(works properly if codeLines were get from CodeEditor().proccessCode() method)</b>
        /// </summary>
        /// <param name="codeLines">Processed lines of code.</param>
        /// <returns>Program size as number of words (size of word: <see cref="ArchitectureSettings.GetWordBits"/>).</returns>
        static int GetProgramSize(List<string> codeLines) {
            var codeCopy = new List<string>(codeLines);
            RemoveLabels(codeCopy);
            return codeCopy.Count();
        }

        /// <summary>
        /// Gets adressses for Procedures, Variables and Constans Labels and loads them into
        /// dictionary where <see cref="Dictionary{String, UInt32}.Keys"/> are Names Of Labels and <see cref="Dictionary{String, UInt32}.Values"/> are Addresses.
        /// </summary>
        /// <param name="codeLines">Processed lines of code.</param>
        /// <returns>Created <see cref="Dictionary{String, UInt32}"/> filled with asseembly components adresses.</returns>
        /// <exception cref="CompilerException"></exception>
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

        /// <summary>Creates compiled program as list of unsigned integer values to be load into Machine memory</summary>
        /// <param name="codeLines">Processed lines of Machine assembly code.</param>
        /// <returns>Compiled to Machine Code program, ready to be loaded into memory. </returns>
        /// <exception cref="CompilerException"></exception>
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
