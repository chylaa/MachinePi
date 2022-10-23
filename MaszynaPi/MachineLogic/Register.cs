using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.CommonOperations;

namespace MaszynaPi.MachineLogic {
    class Register {
        public uint Value { 
            get { return Value; }
            set { Value = value; } 
        }
        public Register(uint value = Defines.DEFAULT_REG_VAL) { 
            Value=(value);
        }

    }

    /// <summary>
    /// CIR - Current instruction register. In classic Machine W named "I"
    /// </summary>
    class InstructionRegister : Register {
        public uint InstructionArgumnet;
        public InstructionRegister(uint value = Defines.DEFAULT_REG_VAL) : base(value) {
            InstructionArgumnet = Defines.DEFAULT_REG_VAL;
        }

        public uint getArgument() { return InstructionArgumnet; }
        private void DecodeInstruction(uint addressSpace, uint codeBits) {
            InstructionArgumnet = (Value & Arythmetics.CreateBitMask(noOfZeroes:codeBits, noOfOnes:addressSpace));
        }
        
    }

    class ArithmetitLogicUnit: Register {

        // Flags
        bool Z;
        
        public ArithmetitLogicUnit(uint value = Defines.DEFAULT_ALU_VAL) : base(value) {
            Z = false;
        }

        public void Nop() { Value = Value; }
        public void Inc() { --Value; }
        public void Dec() { ++Value; }
        public void Not() { Value = ~Value; }
        public void Or(uint arg) { Value |= arg; }
        public void And(uint arg) { Value &= arg; }
        public void Shr(uint arg) { Value >>= (int)arg; }
        public void Add(uint arg) { Value += arg; }
        public void Sub(uint arg) { Value -= arg; }
        public void Mul(uint arg) { Value *= arg; }
        public void Div(uint arg) { Value /= arg; }

    }

}
