using System;
using System.Collections.Generic;
using System.Text;
using MaszynaPi.MachineLogic.Architecture;
namespace MaszynaPi.MachineLogic.IODevices {
    public class CharacterInput : IODevice {
        const uint ID = 1;
        const IOType TYPE = IOType.Input;

        List<char> CharactersBuffer;

        public CharacterInput(Register g, Register rb, uint id = ID, IOType iOType = TYPE) : base(g, rb, id, iOType) {
            CharactersBuffer = new List<char>();
        }

        public List<char> GetCharactersBufferHandle() { return CharactersBuffer; }

        public Action OnCharacterFetched;

        // Gets first character as ASCII number from an input buffer and removes it 
        void GetChar() {
            if (CharactersBuffer.Count == 0) {
                SetReadyValue(IO_WAIT);
                return;
            }
            SetReadyValue(IO_READY);
            SetIOBufferValue(Encoding.ASCII.GetBytes(CharactersBuffer[0].ToString())[0]);
            CharactersBuffer.RemoveAt(0);
            OnCharacterFetched();
        }

        public override void WriteToIOBuffer() {
            base.WriteToIOBuffer();
            GetChar();
        }
    }
}
