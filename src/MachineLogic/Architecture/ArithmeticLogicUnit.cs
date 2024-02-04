using System;
using System.Collections.Generic;
using System.Text;
using MaszynaPi.CommonOperations;

namespace MaszynaPi.MachineLogic.Architecture {

    /// <summary>Supported flags bits of Flag Register of Arithmetic Logic Unit.</summary>
    [Flags]
    public enum ALUFlags { 
        /// <summary>Sign bit (MSB) in accumulator register is set to 1 (negative value).</summary>
        Z   = 0b0001, 
        /// <summary>Not used</summary>
        V   = 0b0010, 
        /// <summary>Interruption requested</summary>
        INT = 0b0100,
        /// <summary>Value in accumulator register equals 0</summary>
        ZAK = 0b1000  
    }

    /// <summary>
    /// Class represent <see cref="CentralProcessingUnit"/>'s ALU. Allows to perform simple calculations using arithmetic/logic operations  
    /// between values of internal "registers" <see cref="OperandA"/> and <see cref="OperandB"/>. Results of those operations
    /// are always stored into <see cref="OperandA"/> and then passed into <see cref="AK"/> - an result <see cref="Register"/>,
    /// which value can be publicly accessed. <see cref="ArithmeticLogicUnit"/> class implements way to get information about 
    /// last operation's result and CPU's state via internal <see cref="FlagRegister"/> containing info about 
    /// currnetly set <see cref="ALUFlags"/>.
    /// </summary>
    public class ArithmeticLogicUnit{
        
        /// <summary>One of two (A, B) internal register-like value instance, for storing operations results and operands during calculations.</summary>
        uint OperandA, OperandB;

        /// <summary> ALU's operation result register. Values can be accessed only via this register.</summary>
        public Register AK; 
        
        /// <summary>Internal Flag register, holding information about ALU's and CPU's state.</summary>
        ALUFlags FlagRegister { get; set; }

        /// <summary>
        /// <see cref="ALUFlags"/> encoded as bitwise XOR of their string representation in lowercase ASCII letters 
        /// -> used for conditional statments in instruction microoperations. 
        /// </summary>
        private readonly Dictionary<int, int> EncodedFlags = new Dictionary<int, int>{  {0b_0111_1010, (int)ALUFlags.Z },
                                                                                        {0b_0111_0110, (int)ALUFlags.V  },
                                                                                        {(0b_0110_1001 ^ 0b_0110_1110 ^ 0b_0111_0100), (int)ALUFlags.INT },
                                                                                        {(0b_0111_1010 ^ 0b_0110_0001 ^ 0b_0110_1011), (int)ALUFlags.ZAK  } };

        /// <summary> Creates <see cref="ArithmeticLogicUnit"/> instance. 
        /// Calls <see cref="AutoSetFlags"/> to sets <see cref="FlagRegister"/> based on current ALU registers state.</summary>
        /// <param name="ak">Handle to result register.</param>
        /// <param name="value">Inital value of internal operands.</param>
        public ArithmeticLogicUnit(Register ak, uint value=Defines.DEFAULT_ALU_VAL){
            AK = ak;
            OperandA = value;
            OperandB = value;
            AutoSetFlags();
        }

        /// <summary>
        /// Allows to check if ALU's flag register, has flag defined by <paramref name="flag"/> stirng set (see <see cref="EncodedFlags"/>).
        /// <br></br>(<i>Side note after a year: Should've use <see cref="Enum.TryParse{TEnum}(string, out TEnum)"/> method</i>)
        /// </summary>
        /// <param name="flag">Name of specific ALU flag.</param>
        /// <returns>'true' if passed flag value is currently set in internal flag register, 'false' otherwises.</returns>
        public bool IsFlagSet(string flag) {
            int argEncoded = 0; 
            foreach(int ascii in Encoding.ASCII.GetBytes(flag)) 
                argEncoded ^= ascii;
            int iflag = EncodedFlags[argEncoded];
            return FlagRegister.HasFlag((ALUFlags)iflag);
        }

        /// <returns>'true' if passed flag value is currently set in internal flag register, 'false' otherwises.</returns>
        public bool IsFlagSet(int flag) {
            return FlagRegister.HasFlag((ALUFlags)flag);
        }

        /// <summary> Allows to manually set specific ALU's flag register <see cref="ALUFlags"/>. </summary>
        /// <param name="flags"><see cref="ALUFlags"/> to set in <see cref="FlagRegister"/></param>
        public void SetFlags(ALUFlags flags) {
            FlagRegister |= flags;
        }

        /// <returns>ALU's flag register content.</returns>
        public ALUFlags GetFlags() { return FlagRegister; }

        /// <summary> Clears passed <see cref="ALUFlags"/> from internal <see cref="FlagRegister"/> register.</summary>
        /// <param name="flags"></param>
        public void ClearFlags(ALUFlags flags) {
            FlagRegister &= ~(flags);
        }
        /// <summary>Allows to set value of second operand of operation.</summary>
        /// <param name="value"></param>
        public void SetOperandB(uint value) { OperandB = value; }

        /// <summary> Allows to set <see cref="FlagRegister"/> based on current ALU registers content.</summary>
        public void AutoSetFlags() { 
            FlagRegister &= ~(ALUFlags.ZAK | ALUFlags.Z); // Clear Specific Flags
            if (Bitwise.IsSignBitSet(AK.GetValue(),AK.GetBitsize())) FlagRegister |= ALUFlags.Z; //  From script: Most significant bit of ACC is sign bit (Z)
            if (AK.GetValue() == 0) FlagRegister |= ALUFlags.ZAK;
        }
        /// <summary>Assings value of first operand (<see cref="OperandA"/>) to <see cref="AK"/>.</summary>
        public void SetResult() {
            AK.SetValue(OperandA); // overflow handled in Register set method
        }

        /// <summary> Sets value of flag and result registers (<see cref="FlagRegister"/>, <see cref="AK"/>), base on internal operands state/values. </summary>
        public void SetResultAndFlags() {
            SetResult();
            AutoSetFlags();
        }

        /// <summary>Resets state of all internal component to their default state.</summary>
        public void Reset() {
            OperandA = Defines.DEFAULT_ALU_VAL;
            OperandB = Defines.DEFAULT_ALU_VAL;
            AK.Reset();
            AutoSetFlags();
        }

        /// <summary> As no operation, assigns <see cref="OperandB"/> value to <see cref="OperandA"/>. </summary>
        public void Nop() { OperandA = OperandB; }
        /// <summary> Performs incrementation of ALU's <see cref="AK"/> register value.</summary>
        public void Inc() { AK.SetValue(AK.GetValue() + 1); }
        /// <summary> Performs decrementation of ALU's <see cref="AK"/> register value.</summary>
        public void Dec() { AK.SetValue(AK.GetValue() - 1); }
        /// <summary> Performs bitwise negation of ALU's <see cref="OperandA"/> register value.</summary>
        public void Not() { OperandA = ~OperandA; }
        /// <summary> Performs bitwise OR between ALU's <see cref="OperandA"/> and <see cref="OperandB"/> values. Stores result in A. </summary>
        public void Or() { OperandA |= OperandB; }
        /// <summary> Performs bitwise AND between ALU's <see cref="OperandA"/> and <see cref="OperandB"/> values. Stores result in A.</summary>
        public void And() { OperandA &= OperandB; }
        /// <summary> Performs bitwise right shif of ALU's <see cref="OperandA"/> value, using <see cref="OperandB"/> value as shift amount. Stores result in A. </summary>
        public void Shr() { OperandA >>= (int)OperandB; }
        /// <summary>Adds ALU's <see cref="OperandA"/> and <see cref="OperandB"/> values and stores result in A. </summary>
        public void Add() { OperandA += OperandB; }
        /// <summary>Subtracts ALU's <see cref="OperandB"/> from <see cref="OperandA"/> values and stores result in A. </summary>
        public void Sub() { OperandA -= OperandB; }
        /// <summary>Multiplies ALU's <see cref="OperandA"/> and <see cref="OperandB"/> values and stores result in A. </summary>
        public void Mul() { OperandA *= OperandB; }
        /// <summary>Divides ALU's <see cref="OperandA"/> by <see cref="OperandB"/> value and stores integer result in A. </summary>
        public void Div() { OperandA /= OperandB; }
    }
}
