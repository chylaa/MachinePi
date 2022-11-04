using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.MachineLogic;
using MaszynaPi.MachineAssembler.FilesHandling;
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
        const string HEADER_MEM_ALLOC = "RPA"; // [Header] Allocate Memory
        const string HEADER_CONST_VAR = "RST"; // [Header] Const Variable Def 
        const string HEADER_LABEL_END = ":";   // [Header] End of assembly label (foo) definition

        const int POS_LABEL = 0;
        const int POS_VALUE = 2;

        static bool FLAG_COMPILED = false;

        static List<uint> ProgramNumeric;
        static List<List<string>> ProgramSignals;

        static string DeleteMultipleSpaces(string s) {
            return Regex.Replace(s, @"\s+", " ");
        }

        static bool ContainsInstruction(string line) {
            return InstructionLoader.GetInstructionsNamesOpcodes().Keys.Any(line.ToLower().Contains);
        }
        // Creates dictionary of User Constants: key-pair -> Name-Value
        static Dictionary<string, uint> GetUserConstans(List<string> codeLines) { 
            Dictionary<string, uint> userConstatns = new Dictionary<string, uint>();
            foreach (string line in codeLines) {
                if (line.Contains(HEADER_CONST_VAR)) {
                    string templine = DeleteMultipleSpaces(line);
                    string[] usrconst = templine.Split(' ');
                    userConstatns.Add(usrconst[POS_LABEL], (uint)int.Parse(usrconst[POS_VALUE]));
                }
            }
            return userConstatns;
        }
        // Creates dictionary of User Variables: key-pair -> Name-Address
        static Dictionary<string, uint> GetUserVariables(List<string> codeLines, int programSize) { 
            Dictionary<string, uint> userVars = new Dictionary<string, uint>();
            uint varAddress = (uint)programSize + 1;
            foreach (string line in codeLines) {
                if (line.Contains(HEADER_MEM_ALLOC)) {
                    string varname = line.Replace(" ","").Split(HEADER_LABEL_END[0])[POS_LABEL];
                    userVars.Add(varname, varAddress);
                }
            }
            return userVars;
        }
        // returns program size without taking into account space for variables
        static int CalculateProgramSize(List<string> codeLines) {
            int size = 0;
            foreach (string line in codeLines)
                if (ContainsInstruction(line)) size++;
            return size;
        }

        public static List<uint> CompileCode(List<string> codeLines) {
            int progSize = CalculateProgramSize(codeLines);
            Dictionary<string, uint> userVariables = GetUserVariables(codeLines, progSize);
            if (userVariables.Last().Value > ArchitectureSettings.MaxAddress() + 1) throw new CompilerException("Compilation Error: Too large program for current architecture settings. Increase address space!");
            Dictionary<string, uint> userConstans = GetUserConstans(codeLines);
            var namesOpcodes = InstructionLoader.GetInstructionsNamesOpcodes();
            //#TODO:
            //- Create new dictionary and function -> as b4, map procedure Labels to adressess of they firs instructions. (+/- same as in GetUserVariables)
            //      tp help create foo IsProcLabel -> contains(':') and not contains(RST || RPA) ? . Also instruction line number is address! 
            return null;
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

