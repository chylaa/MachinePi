using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.CommonOperations;

namespace MaszynaPi.MachineLogic.Architecture {
    class Register {
        public uint Value { 
            get { return Value; }
            set { Value = value; } 
        }
        public Register(uint value = Defines.DEFAULT_REG_VAL) { 
            Value=(value);
        }

    }


}
