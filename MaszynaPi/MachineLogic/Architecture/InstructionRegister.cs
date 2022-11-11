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

        public InstructionRegister(uint addrbBitsize, uint opcodeBitsize,  uint value = Defines.DEFAULT_REG_VAL) : base(addrbBitsize + opcodeBitsize, value) {
            AD = new Register(addrbBitsize);
            KOD = new Register(opcodeBitsize);
            SetValue(value);
        }
        public uint GetArgument() { return AD.GetValue(); }
        public uint GetOpcode() { return KOD.GetValue(); }


        public void DecodeInstruction() {
            AD.SetValue(Arithmetics.DecodeIntructionArgument(this.GetValue()));
            KOD.SetValue(Arithmetics.DecodeInstructionOpcode(this.GetValue()));
        }

    }
}
