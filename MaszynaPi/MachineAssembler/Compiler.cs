using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.MachineLogic;
using MaszynaPi.MachineAssembler.FilesHandling;
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
     *      2. Translated into a sequence of signals    [List<List<string>>]
     */
    static class Compiler {
        public const string HEADER_MEM_ALLOC = "rpa";  // [Header] Allocate Memory - lowercase to standarize code
        public const string HEADER_CONST_VAR = "rst";  // [Header] Const Variable Def - lowercase to standarize code
        public const string HEADER_LABEL_END = ":";    // [Header] End of assembly label (foo) definition

        const int POS_LABEL = 0;
        const int POS_VALUE = 2;

        static bool FLAG_COMPILED = false;

        static List<uint> ProgramNumeric = new List<uint>();
        static List<List<string>> ProgramSignals;

        static string DeleteMultipleSpaces(string s) {
            return Regex.Replace(s, @"\s+", " ");
        }

        public static bool ContainsInstruction(string line) {
            return InstructionLoader.GetInstructionsNamesOpcodes().Keys.Any(line.Contains);
        }

        static bool ContainsProcedureLabel(string line) {
            return (line.Contains(HEADER_LABEL_END) && (!line.Contains(HEADER_CONST_VAR)) && (!line.Contains(HEADER_MEM_ALLOC)));
        }
        static string GetLableName(string line) {
            line = DeleteMultipleSpaces(line);
            return line.Replace(" ", "").Split(HEADER_LABEL_END[0])[POS_LABEL].Replace(HEADER_LABEL_END,"");
        }

        // Creates dictionary of User Constants/Variables: key-pair -> Name-Address & Updates Variables values list
        static Dictionary<string, uint> GetUserConstantsAndVariables(List<string> codeLines, List<uint> variablesValues, int programSize) {
            Dictionary<string, uint> userConstsVars = new Dictionary<string, uint>();
            uint varAddress = (uint)programSize;

            foreach (string line in codeLines) {
                if (line.Contains(HEADER_CONST_VAR) || line.Contains(HEADER_MEM_ALLOC)) {
                    string templine = DeleteMultipleSpaces(line);
                    string[] usrDeclare = templine.Split(' ');

                    if (userConstsVars.ContainsKey(usrDeclare[POS_LABEL])) throw new CompilerException("[Syntax error] Duplicate Variable label: " + usrDeclare[POS_LABEL]);
                    userConstsVars.Add(usrDeclare[POS_LABEL].Replace(HEADER_LABEL_END, ""), varAddress);
                    varAddress++;

                    if (line.Contains(HEADER_CONST_VAR)) {
                        variablesValues.Add((uint)int.Parse(usrDeclare[POS_VALUE]));
                    } else {
                        variablesValues.Add(Defines.DEFAULT_MEM_VAL);
                    }
                }
            }
            return userConstsVars;
        }

        static Dictionary<string, uint> GetProceduresLabes(List<string> codeLines) {
            int instructionAddress = -1;
            bool wasLabel = false;
            string labelLine = "";
            Dictionary<string, uint> userLabels = new Dictionary<string, uint>();
            foreach(string line in codeLines) {
                if (line.Contains(HEADER_CONST_VAR) || line.Contains(HEADER_MEM_ALLOC)) break; //end if get to const/var defines
                if (line.Contains(HEADER_LABEL_END)) {
                    wasLabel = true;
                    labelLine = line;
                }
                if (ContainsInstruction(line)) {
                    instructionAddress++;
                    if (wasLabel) {
                        string procname = GetLableName(labelLine);
                        if (userLabels.ContainsKey(procname)) throw new CompilerException("[Syntax error]) Duplicate Procedure label: "+procname);
                        userLabels.Add(procname, (uint)instructionAddress);
                        wasLabel = false;
                    }
                }
            }
            return userLabels;
        }

        static void RemoveLabels(List<string> codeLines) {
            for(int i=0; i<codeLines.Count; i++) {
                if (codeLines[i].Contains(HEADER_LABEL_END)) {
                    codeLines[i] = codeLines[i].Remove(0, codeLines[i].IndexOf(HEADER_LABEL_END)+1);
                }
                codeLines[i] = codeLines[i].Trim();
            }
            codeLines.RemoveAll(el => (el.Contains(HEADER_CONST_VAR) || el.Contains(HEADER_MEM_ALLOC)));
            codeLines.RemoveAll(string.IsNullOrEmpty);
            codeLines.RemoveAll(string.IsNullOrWhiteSpace);
        }

        // returns program size without taking into account space for variables and consts
        static int CalculateProgramSize(List<string> codeLines) {
            int size = 0;
            foreach (string line in codeLines)
                if (ContainsInstruction(line)) size++;
            return size;
        }

        static void MergeDicts(Dictionary<string, uint> baseDict, Dictionary<string, uint> fromDict) {
            fromDict.ToList().ForEach(x => baseDict.Add(x.Key, x.Value));
        }

        public static List<uint> CompileCode(List<string> codeLines) {
            bool usrConstVarEmpty = true;
            int progSize = CalculateProgramSize(codeLines);
            List<uint> varsValues = new List<uint>(); //append at the end of compilation to code
            Dictionary<string, uint> userConstansVariables = GetUserConstantsAndVariables(codeLines, varsValues, progSize);
            usrConstVarEmpty = (userConstansVariables.Count() == 0);
            if (progSize == 0) throw new CompilerException("[Compilation Error] No syntax.");

            if (!usrConstVarEmpty && userConstansVariables.Last().Value > ArchitectureSettings.GetMaxAddress()) throw new CompilerException("[Compilation Error] Too large program for current architecture settings. Increase address space!");
            
            var namesOpcodes = InstructionLoader.GetInstructionsNamesOpcodes();
            Dictionary<string, uint> userProcedures = GetProceduresLabes(codeLines);

            Dictionary<string, uint> LabelsAdresses = new Dictionary<string, uint>();
            try {
                MergeDicts(baseDict: LabelsAdresses, fromDict: userConstansVariables);
                MergeDicts(baseDict: LabelsAdresses, fromDict: userProcedures);
            } catch (Exception ex) {
                throw new CompilerException("[Syntax error] Duplicate label of variable, constant or procedure. Details: " + ex.Message);
            }
            
            ProgramNumeric.Clear();
            RemoveLabels(codeLines); // Remove all procedures, variables and const defines -> should only left instructions
             
            foreach(string line in codeLines) {
                var instArg = line.Split(' ');
                string argument = null;
                int arg = -1;
                
                string instruction = namesOpcodes.Keys.FirstOrDefault(toCheck => instArg[0].Equals(toCheck));
                if (instruction == null) throw new CompilerException("[Syntax error] Unknown instruction label in: " + line);

                if (instArg.Length == 1) {
                    if(InstructionLoader.GetZeroArgInstructions().Contains(instArg[0]) == false) 
                        throw new CompilerException("[Syntax error] Missing argument for instruction " + line);
                    ProgramNumeric.Add(Arithmetics.EncodeInstruction(namesOpcodes[instruction], 0));
                    continue;
                }
                if (instArg.Length == 2) {
                    argument = LabelsAdresses.Keys.FirstOrDefault(toCheck => instArg[1].Equals(toCheck)); // TODO: Fix -> Make method
                    if (argument == null) {
                        if (int.TryParse(instArg[1], out arg) == false || arg < 0 || arg > ArchitectureSettings.GetMaxAddress())
                            throw new CompilerException("[Syntax error] Unknown argument label in: " + line);
                        ProgramNumeric.Add(Arithmetics.EncodeInstruction(namesOpcodes[instruction], (uint)arg)); // Direct addressing (in orginal machine works)
                        continue;
                    }
                    ProgramNumeric.Add(Arithmetics.EncodeInstruction(namesOpcodes[instruction], LabelsAdresses[argument]));
                    continue;
                }
                throw new CompilerException("[Syntax error] To many arguments in line: "+line);
            }
            ProgramNumeric.AddRange(varsValues); //add to the ond of the list
            FLAG_COMPILED = true;
            return ProgramNumeric;
        } 
    }
}

