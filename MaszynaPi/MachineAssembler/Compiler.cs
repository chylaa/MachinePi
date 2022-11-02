using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.MachineLogic;
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
        const string HEATER_CONST_VAR = "RST"; // [Header] Const Variable Def 

        static bool FLAG_COMPILED = false;

        static List<uint> ProgramNumeric;
        static List<List<string>> ProgramSignals;

        static List<uint> GetProgramAsNumbers() {
            if (FLAG_COMPILED) return ProgramNumeric;
            else throw new CompilerException("Trying get not compiled code");
        }
        static List<List<string>> GetProgramAsSignals() {
            if (FLAG_COMPILED) return ProgramSignals;
            else throw new CompilerException("Trying get not compiled code");
        }

        static void ProcessCodeToNumeric(List<string> codeLines) { //do not mutate list!

        }
        static void ProcessCodeToSignals(List<string> codeLines) { //do not mutate list!
        }

        static void DeleteComments(List<string> codeLines) {
            for (int i = 0; i < codeLines.Count; i++)
                if (codeLines[i].Contains(Defines.COMMENT))
                    codeLines[i].Remove(codeLines[i].IndexOf(Defines.COMMENT));
        }

        public static void ProcessCodeFromEditor(List<string> codeLines) {
            codeLines.RemoveAll(string.IsNullOrWhiteSpace); // for peace of mind
            //codeLines = codeLines.ConvertAll(d => d.ToLower());
            DeleteComments(codeLines);
            ProcessCodeToNumeric(codeLines);
            ProcessCodeToSignals(codeLines);
            FLAG_COMPILED = true;

        }
    }
}

