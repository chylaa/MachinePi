using System;
using System.Collections.Generic;
using MaszynaPi.MachineLogic.Architecture;

namespace MaszynaPi.MachineLogic.IODevices {
    public class CharacterOutput: IODevice {
        const uint ID = 2;
        const IOType TYPE = IOType.Output;

        List<char> CharactersBuffer;

        public CharacterOutput(Register g, Register rb, uint id = ID, IOType iOType = TYPE) : base(g, rb, id, iOType) {
            CharactersBuffer = new List<char>();
        }

        public List<char> GetCharactersBufferHandle() { return CharactersBuffer; }

        public Action OnCharacterPushed;

        // Push value from IO buffer Register to output device as character (ASCII Encoding) 
        void PushChar() {
            SetReadyValue(IO_READY);
            CharactersBuffer.Add(Convert.ToChar(GetIOBufferValue()));
            OnCharacterPushed();
        }

        public override void ReadFromIOBuffer() {
            base.ReadFromIOBuffer();
            PushChar();
        }

        
    }
}
