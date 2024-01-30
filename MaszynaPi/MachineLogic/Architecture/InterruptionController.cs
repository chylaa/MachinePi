using System;
using MaszynaPi.SenseHatHandlers;

namespace MaszynaPi.MachineLogic.Architecture {
    /// <summary>
    /// Class represents <see cref="CentralProcessingUnit"/>'s component, called Interruption Controller.
    /// This unit performs all neccessary operations related to handling incoming/pending interrupts
    /// from diferent sources.
    /// </summary>
    public class InterruptionController {
        /// <summary>CPU's 4 bit Interrupt Report Register.</summary>
        readonly Register RZ;
        /// <summary>CPU's 4 bit Mask Register.</summary>
        readonly Register RM;
        /// <summary>CPU's 4 bit Register of accepted interrupts.</summary>
        readonly Register RP;
        /// <summary>CPU's Interrupt Vector Register (<see cref="ArchitectureSettings.CodeBits"/>) bit size).</summary>
        readonly Register AP;

        /// <summary> Sense-Hat joystick as optional, additional source of interrupts.</summary>
        readonly SenseHatDevice INTJoystick;

        /// <summary>Creates new interrupts controller instance.</summary>
        /// <param name="rz">Handle to Interrupt Report Register</param>
        /// <param name="rm">Handle to Mask Register</param>
        /// <param name="rp">Handle to Register of accepted interrupts</param>
        /// <param name="ap">Handle to Interrupt Vector Register </param>
        public InterruptionController(Register rz, Register rm, Register rp, Register ap) {
            RZ = rz; RM = rm; RP = rp; AP = ap;
            INTJoystick = new SenseHatDevice();
            INTJoystick.CreateReadProcess(SenseHatDevice.JOYSTICK_SCRIPT);
            INTJoystick.OnInterruptionReceived += ReportInterrupt;
            INTJoystick.StartAsyncRead();
        }

        /// <summary> Action delegate invoked when new interruption is repored.</summary>
        public Action OnInterruptReported;

        /// <summary>
        /// Reports new interrupt of given <paramref name="IntPriority"/> by setting 
        /// corresponding bit of Interrupt Report register (<see cref="RZ"/>).
        /// </summary>
        /// <param name="IntPriority">Interruption priority as appropriate power of 2 ([1/2/4/8]).</param>
        void ReportInterrupt(uint IntPriority) {
            RZ.SetValue(RZ.GetValue() | IntPriority);
            OnInterruptReported?.Invoke();
        }

        // Called on eni()
        /// <summary>
        /// Sets values of Accepted INT register and INT vector register (<see cref="RP"/>, <see cref="AP"/>) base on
        /// values of reported interrupts and mask registers (<see cref="RM"/>, <see cref="RZ"/>).<br></br>
        /// Interrupt bit with the highest priority that is not masked, is put in the <see cref="RP"/>
        /// and points to address of interrupt handle which is set as new <see cref="AP"/> register value.<br></br>
        /// Sets <see cref="ALUFlags.INT"/> flag of <see cref="ArithmeticLogicUnit.FlagRegister"/> if any interrupt 
        /// is accepted.
        /// </summary>
        /// <param name="JAL">Handle to CPU's <see cref="ArithmeticLogicUnit"/> instance.</param>
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

        // Called on rint() 
        /// <summary>Clears bits related to accepted interrupt from <see cref="InterruptionController"/> registers.</summary>
        public void ClearMSBOfAcceptedINTs(){
            RZ.SetValue(RZ.GetValue() - RP.GetValue());
            RP.SetValue(Defines.DEFAULT_REG_VAL);
            AP.SetValue(Defines.DEFAULT_REG_VAL);
        }

        /// <summary>Sends signal to terminates async proccess of reading <see cref="INTJoystick"/> state as source of interrupts.</summary>
        public void StopJoystickInterruptionMonitor()
        {
            INTJoystick.StopAsyncRead(cancelTimeout:new TimeSpan(0,0,5));
        }

    }
}
