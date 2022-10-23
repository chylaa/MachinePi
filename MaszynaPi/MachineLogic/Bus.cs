using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaszynaPi.MachineLogic {
    class Bus {
        public uint Value {
            get { return Value; }
            set { Value = value; }
        }
        public Bus(uint value = Defines.DEFAULT_BUS_VAL) {
            Value = (value);
        }
    }
}
