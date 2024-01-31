using MaszynaPi.MachineLogic.Architecture;
using MaszynaPi.SenseHatHandlers;

namespace MaszynaPi.MachineLogic.IODevices 
{
    /// <summary>
    /// Implementaion of <see cref="IOType.Output"/> <see cref="IODevice"/> representing 8x8 LED Matrix on <see cref="SenseHatDevice"/>.
    /// Provides functionality for <see cref="CentralProcessingUnit"/> to write data that will be shown on module's matrix, 
    /// base on selected <see cref="Mode"/>:
    /// <br></br><see cref="Mode.Letter"/> allows to represent passed number as one of printable ASCII symbols on Matrix 
    /// <br></br><see cref="Mode.Paint"/> allows to paint single pixel at time, using bottom 6 bits of passed number as matrix pixel address 
    /// (consecutive numbers from top-left to bottom-right). Color of each pixel is selected using 9th, 10th and 11th bit as [R,G,B] component set to 255 or 0. 
    /// </summary>
    class MatrixLED : IODevice 
    {
        /// <summary>Represents 2 <see cref="MatrixLED"/> working modes.</summary>
        internal enum Mode 
        {
            /// <summary>Each number written into <see cref="IODevice.IOBuffer"/> if show in <see cref="Matrix"/> as it's ASCII representaion.</summary>
            Letter, 
            /// <summary>Each number written into <see cref="IODevice.IOBuffer"/> is translated into single pixel of colour to be shown in <see cref="Matrix"/>.
            Paint 
        };

        /// <summary>Current working <see cref="Mode"/> of <see cref="MatrixLED"/>.</summary>
        Mode WorkingMode;

        const uint ID = 6;
        const IOType TYPE = IOType.Output;

        /// <summary><see cref="SenseHatDevice"/> instance for communicating via python script processes with 8x8  LED matrix located on SenseHat module.</summary>
        readonly SenseHatDevice Matrix;

        /// <summary>Creates new instance of <see cref="MatrixLED"/> <see cref="IODevice"/>.</summary>
        /// <param name="g"><inheritdoc path="/param[@name='g']"/></param>
        /// <param name="rb"><inheritdoc path="/param[@name='rb']"/></param>
        /// <param name="id"><inheritdoc path="/param[@name='id']"/>. Default is <see cref="ID"/>.</param>
        /// <param name="iOType"><inheritdoc path="/param[@name='iOType']"/>. Default is <see cref="TYPE"/> and should not be chaged.</param>
        public MatrixLED(Register g, Register rb, uint id = ID, IOType iOType = TYPE) : base(g, rb, id, iOType) {
            SetLetterMode();
            Matrix = new SenseHatDevice();
        }

        /// <summary>Sets this istance's <see cref="Mode"/> to <see cref="Mode.Letter"/></summary>
        public void SetLetterMode() { WorkingMode = Mode.Letter; }

        /// <summary>Sets this istance's <see cref="Mode"/> to <see cref="Mode.Paint"/></summary>
        public void SetPaintMode() { WorkingMode = Mode.Paint; } 


        /// <summary> 
        /// Push value from <see cref="IODevice.IOBuffer"/> to output <see cref="Matrix"/> device using <see cref="SenseHatDevice.MatrixPrint(uint, string)"/>.
        /// </summary>
        void PushChar() {
            SetReadyValue(IO_READY);
            Matrix.MatrixPrint(GetIOBufferValue(), WorkingMode.ToString());
        }

        /// <summary><inheritdoc cref="PushChar"/></summary>
        public override void ReadFromIOBuffer() {
            base.ReadFromIOBuffer();
            PushChar();
        }


    }
}
