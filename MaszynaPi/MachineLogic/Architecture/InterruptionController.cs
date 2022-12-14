using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.CommonOperations;

namespace MaszynaPi.MachineLogic.Architecture {
    public class InterruptionController {
        Register RZ;  // 4 bit Interrupt Report Register
        Register RM;  // 4 bit Mask Register
        Register RP;  // 4 bit Register of Accepted Interrupts 
        Register AP;  // (CodeBits) Interrupt Vector Register

        Dictionary<uint, uint> InterruptVector = new Dictionary<uint, uint>(); // Pairs of <INT bit, Mem Address>

        public InterruptionController(Register rz, Register rm, Register rp, Register ap) {
            RZ = rz; RM = rm; RP = rp; AP = ap;
            InitializeInterruptVector();
        }

        void InitializeInterruptVector() {
            for(uint i=0; i<Defines.INTERRUPTIONS_NUM; i++)
                InterruptVector.Add(i, i);
        }


        //On eni() put the interrupt bit with the highest priority from register RZ to RP (if not masked)
        public void SetAcceptedAndINTVectorRegister() {
            uint mask = RM.GetValue();
            for (uint bit = RZ.GetBitsize(); bit>0; bit >>= 1) {
                uint rzVal = RZ.GetValue();
                if(( (rzVal & bit) != 0) && ( (bit & mask) == 0)) {
                    RP.SetValue(bit);
                    AP.SetValue(InterruptVector[bit]);
                }
            }
        }
        // On rint() clears RZ
        public void ClearMSBOfAcceptedINTs(){
            RZ.SetValue(RZ.GetValue() - RP.GetValue());
        }

    }
}
