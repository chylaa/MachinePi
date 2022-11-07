using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaszynaPi.MachineLogic.Architecture {

    class BusException : Exception { public BusException(string message) : base(message) { } }
    public class Bus {
        public const int EMPTY = -1;
        public int Value;

        public Bus(uint value = Defines.DEFAULT_BUS_VAL) {
            Value = (int)(value);
        }

        public uint GetValue() {
            if (!IsEmpty()) { return (uint)Value; } else { throw new BusException("Bus is empty!"); }
        }
        
        public void SetValue(uint value) { Value = (int)value; }
        public void SetValue(int value) { Value = value; }

        public void SetEmpty() { SetValue(EMPTY); }

        public bool IsEmpty() {
            if (Value == EMPTY) return true;
            return false;
        }
    }
}
