using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.MachineLogic;

namespace MaszynaPi.MachineAssembler.Decoders {
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
        const string INSTRUCTION_SET_FILE_EXTENSION = ".lst";
        const string OPTIONS_HEADER = "[Opcje]";
        const string COMPONENT_ON = "1"
        const string ADDRESS_SPACE_HEADER = "Adres=";
        const string CODE_BITS_HEADER = "Kod=";
        const byte OPTIONS_LINES = 12;
        const string INSTRUCTIONS_HEADER = "[Rozkazy]";
        static uint MAX_OPCODE = 0;
        static bool FLAG_COMPILED = false;

        static Dictionary<uint, string> InstructionNames = new Dictionary<uint, string>(); // {opcode, name}
        static Dictionary<uint, List<List<string>>> InstructionSignalsMap = new Dictionary<uint, List<List<string>>>(); // {opcode, Lines of signals (Instruction)}

        static List<uint> ProgramNumeric;
        static List<List<string>> ProgramSignals;

        static List<uint> GetProgramAsNumbers() { if (FLAG_COMPILED) return ProgramNumeric; else throw new CompilerException("Trying get not compiled numeric code"); }
        static List<List<string>> GetProgramAsSignals() { if (FLAG_COMPILED) return ProgramSignals; else throw new CompilerException("Trying get not compiled signals code"); }

        static void SetOptions(List<string> options) { //TODO wrzucić do osobnej funkcji z parametrem addrSpace/codeBits żeby nie powtarzać 2x kodu 
            int addrSpace = 0; int codeBits=0;
            bool asParse = int.TryParse(options[options.IndexOf(ADDRESS_SPACE_HEADER)].Replace(ADDRESS_SPACE_HEADER, ""), out addrSpace);
            bool cbParse = int.TryParse(options[options.IndexOf(CODE_BITS_HEADER)].Replace(CODE_BITS_HEADER, ""), out codeBits);
            if (!asParse) throw new CompilerException("Invalid value in "+OPTIONS_HEADER+" "+ADDRESS_SPACE_HEADER+". Must be numeric.");
            if (!cbParse) throw new CompilerException("Invalid value in " + OPTIONS_HEADER + " " + CODE_BITS_HEADER + ". Must be numeric.");
            ArchitectureSettings.SetAddressSpace((uint)addrSpace);
            ArchitectureSettings.SetCodeBits((uint)codeBits);
            options.Remove(ADDRESS_SPACE_HEADER+addrSpace.ToString()); 
            options.Remove(CODE_BITS_HEADER + codeBits.ToString());
            //--------------------------------------------------------------------------------------------------------------
            int componetsSet = 0;
            for(int i=0; i<options.Count;i++) { if (options[i].EndsWith(COMPONENT_ON)) componetsSet |= i; }
            ArchitectureSettings.SetActiveComponents((Defines.Components)componetsSet);
        }
        static void LoadInstructionSetFile(string filepath) {
            List<string> lines = File.ReadAllLines(filepath).ToList();
            List<string> options = lines.GetRange(lines.IndexOf(OPTIONS_HEADER), OPTIONS_LINES);
            List<string> instructios = lines.GetRange(lines.IndexOf(INSTRUCTIONS_HEADER), lines.Count);

            SetOptions(options);

            foreach (string line in lines) {

            }
        }

/*      static void AddInstruction(List<List<string>> signals) {
            MaxOpcode += (uint)Math.Pow(2, ArchitectureSettings.GetAddressSpace());
            DecodedInstructionMap.Add(MaxOpcode, signals);
        }*/

        static void ProcessCodeToNumeric(List<string> codeLines) {
            
        }
        static void ProcessCodeToSignals(List<string> codeLines) { }

        static void DeleteComments(List<string> codeLines) {
            for (int i = 0; i < codeLines.Count; i++)
                if (codeLines[i].Contains(Defines.COMMENT))
                    codeLines[i].Remove(codeLines[i].IndexOf(Defines.COMMENT));
        }
        
        public static void ProcessCodeFromEditor(List<string> codeLines) {
            codeLines.RemoveAll(string.IsNullOrWhiteSpace); // for peace of mind
            DeleteComments(codeLines);

        }




    }
}
