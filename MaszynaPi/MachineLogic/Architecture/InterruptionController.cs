using System;
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

        public Action OnInterruptReported;

        void ReportInterrupt(uint IntPriority) {
            RZ.SetValue(RZ.GetValue() | IntPriority);
            //OnInterruptReported();
        }


        //On eni() put the interrupt bit with the highest priority from register RZ to RP (if not masked)
        public void SetAcceptedAndINTVectorRegister(ArithmeticLogicUnit JAL) {
            uint mask = RM.GetValue();
            uint reported = RZ.GetValue();
            for (int bit = (int)RZ.GetBitsize() - 1; bit >= 0; bit--) {//starts from msb
                uint bitvalue = (uint)Math.Pow(2, bit);
                if (((reported & bitvalue) != 0) && ((bitvalue & mask) == 0)) { // if bit set and not masked
                    RP.SetValue(bitvalue);
                    AP.SetValue(ArchitectureSettings.GetInterruptVector()[(uint)bit]);
                    break;
                }
            }
            if (AP.GetValue() != 0) {
                JAL.SetFlags(ALUFlags.INT);
            } else {
                JAL.ClearFlags(ALUFlags.INT);
            }
        }
        // On rint() clears RZ, RP, AP
        public void ClearMSBOfAcceptedINTs(){
            RZ.SetValue(RZ.GetValue() - RP.GetValue());
            RP.SetValue(Defines.DEFAULT_REG_VAL);
            AP.SetValue(Defines.DEFAULT_REG_VAL);
        }

    }
}
