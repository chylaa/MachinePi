using MaszynaPi.MachineLogic.Architecture;
using MaszynaPi.SenseHatHandlers;

namespace MaszynaPi.MachineLogic.IODevices {
    class MatrixLED : IODevice {
        const uint ID = 6;
        const IOType TYPE = IOType.Output;

        enum Mode { Letter, Paint};
        Mode WorkingMode;

        readonly SenseHatDevice Matrix;

        public MatrixLED(Register g, Register rb, uint id = ID, IOType iOType = TYPE) : base(g, rb, id, iOType) {
            SetLetterMode();
            Matrix = new SenseHatDevice();
        }

        public void SetLetterMode() { WorkingMode = Mode.Letter; }

        public void SetPaintMode() { WorkingMode = Mode.Paint; } 

        // Push value from IO buffer Register to output device
        void PushChar() {
            SetReadyValue(IO_READY);
            Matrix.MatrixPrint(GetIOBufferValue(), WorkingMode.ToString());
        }

        public override void ReadFromIOBuffer() {
            base.ReadFromIOBuffer();
            PushChar();
        }


    }
}
