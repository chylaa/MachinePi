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
            return line.Replace(" ", "").Split(HEADER_LABEL_END[0])[POS_LABEL].Replace(HEADER_LABEL_END,"");
        }


        // Creates dictionary of User Constants: key-pair -> Name-Value
        static Dictionary<string, uint> GetUserConstans(List<string> codeLines) { 
            Dictionary<string, uint> userConstans = new Dictionary<string, uint>();
            foreach (string line in codeLines) {
                if (line.Contains(HEADER_CONST_VAR)) {
                    string templine = DeleteMultipleSpaces(line);
                    string[] usrconst = templine.Split(' ');
                    if (userConstans.ContainsKey(usrconst[POS_LABEL])) throw new CompilerException("[Syntax error] Duplicate Variable label: " + usrconst[POS_LABEL]);
                    userConstans.Add(usrconst[POS_LABEL].Replace(HEADER_LABEL_END,""), (uint)int.Parse(usrconst[POS_VALUE]));
                }
            }
            return userConstans;
        }
        // Creates dictionary of User Variables: key-pair -> Name-Address
        static Dictionary<string, uint> GetUserVariables(List<string> codeLines, int programSize) { 
            Dictionary<string, uint> userVars = new Dictionary<string, uint>();
            uint varAddress = (uint)programSize;
            foreach (string line in codeLines) {
                if (line.Contains(HEADER_MEM_ALLOC)) {
                    string varname = GetLableName(line);
                    if (userVars.ContainsKey(varname)) throw new CompilerException("[Syntax error] Duplicate Variable label: " + varname);
                    userVars.Add(varname, varAddress);
                    varAddress++;
                }
            }
            return userVars;
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
            }
            codeLines.RemoveAll(el => (el.Contains(HEADER_CONST_VAR) || el.Contains(HEADER_MEM_ALLOC)));
            codeLines.RemoveAll(string.IsNullOrEmpty);
            codeLines.RemoveAll(string.IsNullOrWhiteSpace); 
        }

        // returns program size without taking into account space for variables
        static int CalculateProgramSize(List<string> codeLines) {
            int size = 0;
            foreach (string line in codeLines)
                if (ContainsInstruction(line)) size++;
            return size;
        }

        static void MergeDicts(Dictionary<string, uint> baseDict, Dictionary<string, uint> fromDict) {
            fromDict.ToList().ForEach(x => baseDict.Add(x.Key, x.Value));
        }
        /* |==============================================================================================+
         TODO: BUG -> Program compiles and loads to memory but argumemts are invalid! ++++++++++++++++++++|
           |==============================================================================================+
         */
        public static List<uint> CompileCode(List<string> codeLines) {
            int progSize = CalculateProgramSize(codeLines);
            Dictionary<string, uint> userVariables = GetUserVariables(codeLines, progSize);
            if (userVariables.Last().Value > ArchitectureSettings.GetMaxAddress()) throw new CompilerException("Compilation Error: Too large program for current architecture settings. Increase address space!");
            Dictionary<string, uint> userConstans = GetUserConstans(codeLines);
            var namesOpcodes = InstructionLoader.GetInstructionsNamesOpcodes();
            Dictionary<string, uint> userProcedures = GetProceduresLabes(codeLines);

            Dictionary<string, uint> LabelsAdresses = new Dictionary<string, uint>();
            try {
                MergeDicts(baseDict: LabelsAdresses, fromDict: userVariables);
                MergeDicts(baseDict: LabelsAdresses, fromDict: userConstans);
                MergeDicts(baseDict: LabelsAdresses, fromDict: userProcedures);
            } catch (Exception ex) {
                throw new CompilerException("[Syntax error] Duplicate label of variable, constant or procedure. Details: " + ex.Message);
            }
            
            ProgramNumeric.Clear();
            RemoveLabels(codeLines); // Remove all procedures, variables and const defines -> should only left instructions

            foreach(string line in codeLines) {
                //if (ContainsInstruction(line) == false) continue;
                string instruction = namesOpcodes.Keys.FirstOrDefault(toCheck => line.Contains(toCheck)); // TODO: Fix -> Make method 
                string argument = LabelsAdresses.Keys.FirstOrDefault(toCheck => line.Contains(toCheck)); // TODO: Fix -> Make method
                
                if (instruction == null) throw new CompilerException("[Syntax error] unknown instruction label in: " + line);
                if (argument == null) {
                    if (InstructionLoader.GetZeroArgInstructions().Contains(instruction)) {
                        ProgramNumeric.Add(Arithmetics.EncodeInstruction(namesOpcodes[instruction], 0));
                        continue;
                    }
                    throw new CompilerException("[Syntax error] unknown argument label in: " + line);
                }
              
                ProgramNumeric.Add(Arithmetics.EncodeInstruction(namesOpcodes[instruction], LabelsAdresses[argument]));
            }
            FLAG_COMPILED = true;
            return ProgramNumeric;
        } 

        //static List<uint> GetProgramAsNumbers() {
        //    if (FLAG_COMPILED) return ProgramNumeric;
        //    else throw new CompilerException("Trying get not compiled code");
        //}
        //static List<List<string>> GetProgramAsSignals() {
        //    if (FLAG_COMPILED) return ProgramSignals;
        //    else throw new CompilerException("Trying get not compiled code");
        //}

        //static void ProcessCodeToNumeric(List<string> codeLines) {
        //    var namesOpcodes = InstructionLoader.GetInstructionsNamesOpcodes();

        //}
        //static void ProcessCodeToSignals(List<string> codeLines) {
        //    var opcodesSignals = InstructionLoader.GetInstructionSignalsMap();

        //}

        //public static void CompileCodeLines(List<string> codeLines) {
        //    ProcessCodeToNumeric(new List<string>(codeLines));
        //    ProcessCodeToSignals(new List<string>(codeLines));
        //    FLAG_COMPILED = true;
        //}
    }
}

