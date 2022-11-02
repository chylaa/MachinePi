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
        uint InstructionArgument;
        uint InstructionOpcode;
        public InstructionRegister(uint value = Defines.DEFAULT_REG_VAL) : base(value) {
            InstructionArgument = Defines.DEFAULT_REG_VAL;
        }
        public uint getArgument() { return InstructionArgument; }
        public uint getOpcode() { return InstructionOpcode; }


        public void DecodeInstruction() {
            InstructionArgument = Arithmetics.DecodeIntructionArgument(Value);
            InstructionOpcode = Arithmetics.DecodeInstructionOpcode(Value);
        }

    }
}
