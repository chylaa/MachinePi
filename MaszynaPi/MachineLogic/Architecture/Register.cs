using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.CommonOperations;

namespace MaszynaPi.MachineLogic.Architecture {
    public class Register {

        uint Value;
        uint Bitsize;
        public Register(uint bitsize, uint value = Defines.DEFAULT_REG_VAL) { 
            Value=(value);
            Bitsize = bitsize;
        }

        internal ControlUnit ControlUnit {
            get => default;
            set {
            }
        }

        public void SetValue(uint value) { Value = Bitwise.HandleOverflow(value, Bitsize); }
        public uint GetValue() { return Value; }

        public virtual void SetBitsize(uint bitsize, uint instbitsize=0) { Bitsize=bitsize; } //instbizsize parameter for InstructionRegister class
        public uint GetBitsize() { return Bitsize; }

        public virtual void Reset() { SetValue(Defines.DEFAULT_REG_VAL); }

    }


}
