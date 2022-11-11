using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.CommonOperations;

namespace MaszynaPi.MachineLogic.Architecture {
    public class Register {

        uint Value;
        public uint Bitsize { get; set; }
        public Register(uint bitsize, uint value = Defines.DEFAULT_REG_VAL) { 
            Value=(value);
            Bitsize = bitsize;
        }

        public void SetValue(uint value) { Value = Arithmetics.HandleOverflow(value, Bitsize); }
        public uint GetValue() { return Value; }

    }


}
