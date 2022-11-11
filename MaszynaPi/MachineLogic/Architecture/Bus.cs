using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.CommonOperations;

namespace MaszynaPi.MachineLogic.Architecture {

    class BusException : Exception { public BusException(string message) : base(message) { } }
    public class Bus {
        const int EMPTY = -1;
        uint Bitsize;
        int Value;

        public Bus(uint bitsize, uint value = Defines.DEFAULT_BUS_VAL) {
            Value = (int)(value);
            Bitsize = bitsize;
        }

        public uint GetValue() {
            if (!IsEmpty()) { return (uint)Value; } else { throw new BusException("Bus is empty!"); }
        }
        
        public void SetValue(uint value) { Value = (int)Arithmetics.HandleOverflow(value, Bitsize); }
        public void SetValue(int value) {
            if (value == EMPTY) Value = value;
            else SetValue((uint)value); }

        public void SetEmpty() { SetValue(EMPTY); }

        public bool IsEmpty() {
            if (Value == EMPTY) return true;
            return false;
        }
    }
}
