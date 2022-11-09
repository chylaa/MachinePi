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
    public class InstructionRegister : Register {
        Register AD;  // InstructionArgument;
        Register KOD; // InstructionOpcode;
        public InstructionRegister(uint value = Defines.DEFAULT_REG_VAL) : base(value) {
            AD = new Register();
            KOD = new Register();
        }
        public uint GetArgument() { return AD.Value; }
        public uint GetOpcode() { return KOD.Value; }


        public void DecodeInstruction() {
            AD.Value  = Arithmetics.DecodeIntructionArgument(Value);
            KOD.Value = Arithmetics.DecodeInstructionOpcode(Value);
        }

    }
}
