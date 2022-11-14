using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.MachineLogic.Architecture;
using System.Windows.Forms;
namespace MaszynaPi.MachineLogic.InputDevices {
    public class CharacterInput : IODevice {

        TextBox TextInput; 

        public CharacterInput(Register g, Register rb, IOType iOType = IOType.Input) : base(g, rb, iOType) {}

        public void SetTextInput(TextBox input) {
            TextInput = input;
        }

        // Gets first character as ASCII number from an input buffer and removes it 
        void GetChar() {
            string input = TextInput.Text;
            if (input.Length == 0) {
                SetReadyValue(IO_WAIT);
                return;
            }
            SetReadyValue(IO_READY);
            SetIOBufferValue(Encoding.ASCII.GetBytes(input)[0]);
            TextInput.Text = input.Substring(1);
        }

        public override void WriteToIOBuffer() {
            if (TextInput == null) { throw new IODeviceException("TextBox Input of CharacterInput IODevice not set! (Use SetTextInput() method)"); }
            base.WriteToIOBuffer();
            GetChar();
        }
    }
}
