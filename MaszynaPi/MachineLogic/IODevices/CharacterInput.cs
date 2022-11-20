using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.MachineLogic.Architecture;
using System.Windows.Forms;
namespace MaszynaPi.MachineLogic.IODevices {
    public class CharacterInput : IODevice {

        List<char> CharactersBuffer;

        public CharacterInput(Register g, Register rb, IOType iOType = IOType.Input) : base(g, rb, iOType) { CharactersBuffer = new List<char>(); }

        public List<char> GetCharactersBufferHandle() { return CharactersBuffer; }

        // Gets first character as ASCII number from an input buffer and removes it 
        void GetChar() {
            if (CharactersBuffer.Count == 0) {
                SetReadyValue(IO_WAIT);
                return;
            }
            SetReadyValue(IO_READY);
            SetIOBufferValue(Encoding.ASCII.GetBytes(CharactersBuffer[0].ToString())[0]);
            CharactersBuffer.RemoveAt(0);
        }

        public override void WriteToIOBuffer() {
            if (CharactersBuffer == null) { throw new IODeviceException("CharactersBuffer of CharacterInput IODevice not set!"); }
            base.WriteToIOBuffer();
            GetChar();
        }
    }
}
