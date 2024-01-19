using System;
using MaszynaPi.CommonOperations;

namespace MaszynaPi.MachineLogic.Architecture {

    /// <summary> Generic <see cref="Exception"/> for representing errors related to <see cref="Bus"/> class issues. </summary>
    class BusException : Exception { public BusException(string message) : base(message) { } }

    /// <summary>
    /// <see cref="Register"/>-like class representing Machines' <see cref="Bus"/>. 
    /// Allows to specify current data on bus using <see cref="Value"/> and bus width with <see cref="Bitsize"/>.
    /// It can have <see cref="EMPTY"/> value assigned to represent no-value, <i>'high-impedance'</i> state.
    /// </summary>
    public class Bus 
    {

        const int EMPTY = Defines.DEFAULT_BUS_VAL;
        uint Bitsize;
        int Value;

        /// <summary>Initializes new <see cref="Bus"/> instance, with <see cref="EMPTY"/> value, of allowed <paramref name="bitsize"/> size.</summary>
        /// <param name="bitsize">"width" of bus, defines maximum size of internal <see cref="Value"/>.</param>
        public Bus(uint bitsize) {
            Bitsize = bitsize;
            SetEmpty();
        }

        /// <returns>Returns <see cref="Value"/> if bus is not empty, <see cref="BusException"/> is thrown otherwise.</returns>
        /// <exception cref="BusException"></exception>
        public uint GetValue() {
            if (!IsEmpty()) { return (uint)Value; } else { throw new BusException("Bus is empty!"); }
        }

        /// <summary>
        /// Allows to set unsigned integer value as bus <see cref="Value"/>. 
        /// Passed value can have any <see cref="uint"/> value, but potential overflow will be simulated, 
        /// before assigment, using <see cref="Bitwise.HandleOverflow(uint, uint)"/> method with <see cref="Bitsize"/> param.
        /// </summary>
        public void SetValue(uint value) { Value = (int)Bitwise.HandleOverflow(value, Bitsize); }
        
        /// <summary>Allows to set integer value as bus <see cref="Value"/>.</summary>
        /// <param name="value">Integer to be assigned as bus <see cref="Value"/>.</param>
        public void SetValue(int value) {
            if (value == EMPTY) Value = value;
            else SetValue((uint)value); }

        /// <summary>Sets special <see cref="EMPTY"/> value to represent high-impedance state. </summary>
        public void SetEmpty() { SetValue(EMPTY); }

        /// <summary> Checks if <see cref="Bus"/> is empty.  </summary>
        /// <returns>True if bus <see cref="Value"/> is equal to <see cref="EMPTY"/> constans, false otherwise.</returns>
        public bool IsEmpty() {
            if (Value == EMPTY) return true;
            return false;
        }

        /// <summary> Sets <see cref="Bitsize"/> parameter (width) of bus. </summary>
        /// <param name="bitsize">Bit size (width) to be set.</param>
        public void SetBitsize(uint bitsize) { Bitsize = bitsize; }
        
        /// <returns>Internal <see cref="Bitsize"/> of this bus instance.</returns>
        public uint GetBitsize() { return Bitsize; }

        /// <summary>Allows to reset this bus instance state by calling <see cref="SetEmpty"/> method on itself.</summary>
        public void Reset() { SetEmpty(); }

    }
}
