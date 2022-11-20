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

        public InstructionRegister(uint addrBitsize, uint opcodeBitsize,  uint value = Defines.DEFAULT_REG_VAL) : base(addrBitsize + opcodeBitsize, value) {
            AD = new Register(addrBitsize);
            KOD = new Register(opcodeBitsize);
            SetValue(value);
        }
        public uint GetArgument() { return AD.GetValue(); }
        public uint GetOpcode() { return KOD.GetValue(); }

        public override void SetBitsize(uint addrBitsize, uint opcodeBitsize) {
            base.SetBitsize(addrBitsize+opcodeBitsize);
            AD.SetBitsize(addrBitsize);
            KOD.SetBitsize(opcodeBitsize);
        }

        public void DecodeInstruction() {
            AD.SetValue(Bitwise.DecodeIntructionArgument(this.GetValue()));
            KOD.SetValue(Bitwise.DecodeInstructionOpcode(this.GetValue()));
        }

        public override void Reset() { base.Reset(); AD.Reset(); KOD.Reset(); }

    }
}
