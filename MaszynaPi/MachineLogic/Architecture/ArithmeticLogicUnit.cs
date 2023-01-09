using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.CommonOperations;
namespace MaszynaPi.MachineLogic.Architecture {
    
    [Flags]
    // Flags are encoded as it's bitwise XOR of lowercase letters in ASCII -> for if-else statment in instruction signals 
    public enum ALUFlags { 
        Z   = 0b_0111_1010, // AK < 0 (MSB == 1)
        V   = 0b_0111_0110, // 
        INT = (0b_0110_1001 ^ 0b_0110_1110 ^ 0b_0111_0100), // Interuption
        ZAK = (0b_0111_1010 ^ 0b_0110_0001 ^ 0b_0110_1011)  // AK = 0
    }

    public class ArithmeticLogicUnit{
        uint OperandA, OperandB;
        public Register AK; // Dostęp do wyników tylko przez akumulator
        ALUFlags JALFlags { get; set; }


        public ArithmeticLogicUnit(Register ak, uint value=Defines.DEFAULT_ALU_VAL){
            AK = ak;
            OperandA = value;
            OperandB = value;
            AutoSetFlags();
        }

        // returns true if JALFlags has set flag == encoded argument (see ALUFlags specification of encoding)
        public bool IsFlagSet(string argument) {
            int argEncoded = 0;
            foreach(int ascii in Encoding.ASCII.GetBytes(argument)) 
                argEncoded ^= ascii;
            return JALFlags.HasFlag((ALUFlags)argEncoded);
        }
        public bool IsFlagSet(int flag) {
            return JALFlags.HasFlag((ALUFlags)flag);
        }

        // Manually adds specified flags 
        public void SetFlags(ALUFlags flags) {
            JALFlags |= flags;
        }

        public ALUFlags GetFlags() { return JALFlags; }

        public void ClearFlags(ALUFlags flags) {
            JALFlags &= ~(flags);
        }

        public void SetOperandB(uint value) { OperandB = value; }

        //Sets flags basing on ALU register content
        public void AutoSetFlags() { 
            JALFlags &= ~(ALUFlags.ZAK | ALUFlags.Z); // Clear Specific Flags
            if (Bitwise.IsSignBitSet(AK.GetValue(),AK.GetBitsize())) JALFlags |= ALUFlags.Z; ///  From script: Najbardziej znaczący bit akumulatora nazwano bitem znaku liczby(Z)
            if (AK.GetValue() == 0) JALFlags |= ALUFlags.ZAK;
        }
        public void SetResult() {
            AK.SetValue(OperandA); // overflow handled in Register set method
        }

        public void SetResultAndFlags() {
            SetResult();
            AutoSetFlags();
        }

        public void Reset() {
            OperandA = Defines.DEFAULT_ALU_VAL;
            OperandB = Defines.DEFAULT_ALU_VAL;
            AK.Reset();
            AutoSetFlags();
        }


        public void Nop() { OperandA = OperandB; }
        public void Inc() { --OperandA; }
        public void Dec() { ++OperandA; }
        public void Not() { OperandA = ~OperandA; }
        public void Or() { OperandA |= OperandB; }
        public void And() { OperandA &= OperandB; }
        public void Shr() { OperandA >>= (int)OperandB; }
        public void Add() { OperandA += OperandB; }
        public void Sub() { OperandA -= OperandB; }
        public void Mul() { OperandA *= OperandB; }
        public void Div() { OperandA /= OperandB; }
    }
}
