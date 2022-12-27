using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.CommonOperations;
using MaszynaPi.SenseHatHandlers;

namespace MaszynaPi.MachineLogic.Architecture {
    public class InterruptionController {
        Register RZ;  // 4 bit Interrupt Report Register
        Register RM;  // 4 bit Mask Register
        Register RP;  // 4 bit Register of Accepted Interrupts 
        Register AP;  // (CodeBits) Interrupt Vector Register

        SenseHatDevice INTJoustick;
        public InterruptionController(Register rz, Register rm, Register rp, Register ap) {
            RZ = rz; RM = rm; RP = rp; AP = ap;
            INTJoustick = new SenseHatDevice();
            INTJoustick.CreateReadProcess(SenseHatDevice.JOYSTICK_SCRIPT);
            INTJoustick.OnInterruptionReceived += ReportInterrupt;
            INTJoustick.StartAsyncRead();

        }

        void ReportInterrupt(uint IntPriority) {
            RZ.SetValue(RZ.GetValue() | IntPriority);
        }


        //On eni() put the interrupt bit with the highest priority from register RZ to RP (if not masked)
        public void SetAcceptedAndINTVectorRegister(ArithmeticLogicUnit JAL) {
            uint mask = RM.GetValue();
            for (uint intBit = RZ.GetBitsize(); intBit>0; intBit >>= 1) {
                uint rzVal = RZ.GetValue();
                if(( (rzVal & intBit) != 0) && ( (intBit & mask) == 0)) {
                    RP.SetValue(intBit);
                    AP.SetValue(ArchitectureSettings.GetInterruptVector()[intBit]);
                }
            }
            if (AP.GetValue() != 0) {
                JAL.SetFlags(ALUFlags.INT);
            } else {
                JAL.ClearFlags(ALUFlags.INT);
            }
        }
        // On rint() clears RZ
        public void ClearMSBOfAcceptedINTs(){
            RZ.SetValue(RZ.GetValue() - RP.GetValue());
        }

    }
}
