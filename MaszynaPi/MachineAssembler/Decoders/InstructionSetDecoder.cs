using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MaszynaPi.MachineLogic;
namespace MaszynaPi.MachineAssembler.Decoders {

    static class InstructionSetDecoder {
        const string INSTRUCTION_SET_FILE_EXTENSION = ".lst";
        const string OPTIONS_HEADER = "[Opcje]";
        const byte OPTIONS_LINES = 12;
        const string INSTRUCTIONS_HEADER = "[Rozkazy]";

        //static uint MaxOpcode = 0;

        static Dictionary<uint, string> InstructionNames = new Dictionary<uint, string>(); // {opcode, name}
        static Dictionary<uint, List<List<string>>> InstructionSignalsMap = new Dictionary<uint, List<List<string>>>(); // {opcode, Lines of signals (Instruction)}


        static void DecodeInstructionSetFile(string filepath) {
            List<string> lines = File.ReadAllLines(filepath).ToList();
            List<string> options = lines.GetRange(lines.IndexOf(OPTIONS_HEADER), OPTIONS_LINES);
            List<string> instructios = lines.GetRange(lines.IndexOf(INSTRUCTIONS_HEADER), lines.Count);
            
            foreach (string line in lines) {

            }
        }

/*        static void AddInstruction(List<List<string>> signals) {
            MaxOpcode += (uint)Math.Pow(2, ArchitectureSettings.GetAddressSpace());
            DecodedInstructionMap.Add(MaxOpcode, signals);
        }*/
    }
}
