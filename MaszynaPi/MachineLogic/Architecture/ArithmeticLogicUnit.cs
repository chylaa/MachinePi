using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaszynaPi.MachineLogic.Architecture {
    class ArithmeticLogicUnit{
        uint OperandA, OperandB;
        Register Accumulator; // Dostęp do wników tylko przez akumulator
        // Flags
        bool Z;

        public ArithmeticLogicUnit(Register ak, uint value=Defines.DEFAULT_ALU_VAL){
            Accumulator = ak;
            OperandA = value;
            OperandB = value;
            Z = false;
        }

        public void SetResult() { Accumulator.Value = OperandA; }
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
