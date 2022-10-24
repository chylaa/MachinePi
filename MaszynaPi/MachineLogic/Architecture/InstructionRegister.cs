using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.CommonOperations;

namespace MaszynaPi.MachineLogic.Architecture {
    /// <summary>
    /// CIR - Current instruction register. In classic Machine W named "I"
    /// </summary>
    class InstructionRegister : Register {
        uint InstructionArgumnet;
        uint InstructionOpcode;
        public InstructionRegister(uint value = Defines.DEFAULT_REG_VAL) : base(value) {
            InstructionArgumnet = Defines.DEFAULT_REG_VAL;
        }
        public uint getArgument() { return InstructionArgumnet; }
        public uint getOpcode() { return InstructionOpcode; }

        void DecodeArgument(uint addressSpace, uint codeBits) {
            InstructionArgumnet = (Value & Arithmetics.CreateBitMask(noOfZeroes: codeBits, noOfOnes: addressSpace));
        }
        void DecodeOpcode(uint addressSpace, uint codeBits) {
            InstructionOpcode = (Value & Arithmetics.CreateBitMask(noOfZeroes: codeBits, noOfOnes: addressSpace, zeroesFirst:false));
        }
        public void DecodeInstruction(uint addressSpace, uint codeBits) {
            DecodeArgument(addressSpace, codeBits);
            DecodeOpcode(addressSpace, codeBits);
        }

    }
}
