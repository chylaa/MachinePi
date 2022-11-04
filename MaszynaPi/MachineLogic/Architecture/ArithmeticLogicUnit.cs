using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.CommonOperations;
namespace MaszynaPi.MachineLogic.Architecture {
    [Flags]
    enum ALUFlags {
        Z   = 0b0001, // AK < 0
        V   = 0b0010, // 
        INT = 0b0100, // Interuption
        ZAK = 0b1000  // AK = 0
    }

    public class ArithmeticLogicUnit{
        uint OperandA, OperandB;
        Register Accumulator; // Dostęp do wników tylko przez akumulator
        ALUFlags JALFlags { get; set; }


        public ArithmeticLogicUnit(Register ak, uint value=Defines.DEFAULT_ALU_VAL){
            Accumulator = ak;
            JALFlags = (ALUFlags)value;
            OperandA = value;
            OperandB = value;
        }
        
        public void SetFlags() {
            JALFlags &= ~(ALUFlags.ZAK | ALUFlags.Z); //clear Specyfic Flags
            if (Accumulator.Value < OperandA) JALFlags |= ALUFlags.Z;
            if (Accumulator.Value == 0) JALFlags |= ALUFlags.ZAK;
        }
        public void SetResult() {
            Accumulator.Value = Arithmetics.HandleOverflow(OperandA); // to keep result valid based on current architecture settings (Word Length)
        }

        public void SetResultAndFlags() {
            SetResult();
            SetFlags();
        }

        public void SetOperandB(uint value) { OperandB = value; }

 
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
