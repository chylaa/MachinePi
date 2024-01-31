using System;
using System.Collections.Generic;
using System.Text;
using MaszynaPi.MachineLogic.Architecture;
namespace MaszynaPi.MachineLogic.IODevices {

    /// <summary>
    /// Character <see cref="IOType.Input"/> device. Provides method for <see cref="CentralProcessingUnit"/> to read input data provided by user.
    /// One character at time can be fetched from internal buffer, and it will be deleted from it afterwards.
    /// </summary>
    public class CharacterInput : IODevice 
    {
        
        const uint ID = 1;
        const IOType TYPE = IOType.Input;

        /// <summary>
        /// Representation of input buffer structure, where characters provided by user are stored and can be retreived by <see cref="CentralProcessingUnit"/>.
        /// via this <see cref="CharacterInput"/> instance.
        /// </summary>
        readonly Queue<char> CharactersBuffer;

        /// <summary>
        /// Creates new instance of <see cref="CharacterInput"/> class, assigning by defaut 
        /// <see cref="IODevice.DeviceID"/> of 1 and <see cref="IODevice.Type"/> of <see cref="IOType.Input"/>.
        /// </summary>
        /// <param name="g"><inheritdoc path="/param[@name='g']"/></param>
        /// <param name="rb"><inheritdoc path="/param[@name='rb']"/></param>
        /// <param name="id"><inheritdoc path="/param[@name='id']"/>. Default is <see cref="ID"/>.</param>
        /// <param name="iOType"><inheritdoc path="/param[@name='iOType']"/>. Default is <see cref="TYPE"/> and should not be chaged.</param>
        public CharacterInput(Register g, Register rb, uint id = ID, IOType iOType = TYPE) : base(g, rb, id, iOType) {
            CharactersBuffer = new Queue<char>();
        }

        /// <summary>Get representation of input buffer structure, where characters provided by user are stored.</summary>
        public Queue<char> GetCharactersBufferHandle() { return CharactersBuffer; }

        /// <summary>
        /// Parametless <see cref="Action"/> invoked when new character is fetch from <see cref="CharactersBuffer"/> by this instance of <see cref="CharacterInput"/>./
        /// </summary>
        public Action OnCharacterFetched;


        /// <summary> 
        /// Checks if any character are avaiable in <see cref="CharactersBuffer"/> to read and sets <see cref="IODevice.Ready"/> value accordingly.
        /// On character availabe, fetches first character from an input buffer queue as ASCII number and puts it into single-char <see cref="IODevice.IOBuffer"/> <see cref="Register"/>.
        /// Character is removed from Queue buffer and <see cref="OnCharacterFetched"/> <see cref="Action"/> is invoked.
        /// </summary>
        void GetChar() {
            if (CharactersBuffer.Count == 0) {
                SetReadyValue(IO_WAIT);
                return;
            }
            SetReadyValue(IO_READY);
            SetIOBufferValue(Encoding.ASCII.GetBytes(CharactersBuffer.Dequeue().ToString())[0]);
            OnCharacterFetched();
        }

        /// <summary> 
        /// Checks if device is valid <see cref="IOType.Input"/> device using base implementation of <see cref="IODevice.WriteToIOBuffer"/>.
        /// Checks if any character are avaiable in <see cref="CharactersBuffer"/> to read and sets <see cref="IODevice.Ready"/> value accordingly.
        /// On character availabe, fetches first character from an input buffer queue as ASCII number and puts it into single-char <see cref="IODevice.IOBuffer"/> <see cref="Register"/>.
        /// Character is removed from Queue buffer and <see cref="OnCharacterFetched"/> <see cref="Action"/> is invoked.
        /// </summary>
        public override void WriteToIOBuffer() {
            base.WriteToIOBuffer();
            GetChar();
        }
    }
}
