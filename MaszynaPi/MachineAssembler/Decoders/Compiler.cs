using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.MachineLogic;

namespace MaszynaPi.MachineAssembler.Decoders {
    //TODO: Rozważyć merge'a klas Compilator oraz InstructionSetDecoder w jedną klasę "Compilator" (nawet z nazwy jest to samo xD)

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

        static uint MAX_OPCODE = 0;
        //static Dictionary<uint, List<List<string>>> InstructionMap = new Dictionary<uint, List<List<string>>>();
        static List<uint> Programm = new List<uint>();

        static void LoadInstructionSetFromFile(List<List<string>> readInstructions) {
        }



    }
}
