using MaszynaPi.CommonOperations;

namespace MaszynaPi.MachineLogic.Architecture 
{
    /// <summary>Class representing basic read/write register.</summary>
    public class Register {

        /// <summary> Value currently stored in <see cref="Register"/>. </summary>
        uint Value;
        /// <summary> Size of <see cref="Register"/> in bits, defining maximum value that can be stored.</summary>
        uint Bitsize;

        /// <summary>
        /// Creates new <see cref="Register"/> instance, initialized with specific <paramref name="value"/> 
        /// that later can take maximum of (2^<paramref name="bitsize"/>-1) 
        /// </summary>
        /// <param name="bitsize">Initial Register size in bits.</param>
        /// <param name="value">Initial value of register. Defaults to <see cref="Defines.DEFAULT_REG_VAL"/>.</param>
        public Register(uint bitsize, uint value = Defines.DEFAULT_REG_VAL) { 
            Value=(value);
            Bitsize = bitsize;
        }

        /// <summary>
        /// Assingns passed <paramref name="value"/> to internal <see cref="Value"/> of <see cref="Register"/>.
        /// Passed value can have any <see cref="uint"/> value, but potential overflow will be simulated, 
        /// before assigment, using <see cref="Bitwise.HandleOverflow(uint, uint)"/> method.
        /// </summary>
        /// <param name="value">Value to be set (after overflow handling) as <see cref="Register.Value"/>.</param>
        public void SetValue(uint value) { Value = Bitwise.HandleOverflow(value, Bitsize); }

        /// <returns>Internal <see cref="Register.Value"/></returns>
        public uint GetValue() { return Value; }

        /// <summary> Sets <see cref="Bitsize"/> parameter of register.  </summary>
        /// <param name="bitsize">Bit size to be set</param>
        /// <param name="instbitsize">Instruction bit size (parameter for <see cref="InstructionRegister"/> class)</param>
        public virtual void SetBitsize(uint bitsize, uint instbitsize=0) { Bitsize=bitsize; } 
        
        /// <returns>Internal <see cref="Register.Bitsize"/></returns>
        public uint GetBitsize() { return Bitsize; }

        /// <summary> Resets <see cref="Register.Value"/> state via <see cref="SetValue(uint)"/> method using <see cref="Defines.DEFAULT_REG_VAL"/> constant.</summary>
        public virtual void Reset() { SetValue(Defines.DEFAULT_REG_VAL); }

    }
}
