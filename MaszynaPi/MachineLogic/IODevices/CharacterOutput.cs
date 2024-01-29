using System;
using System.Collections.Generic;
using MaszynaPi.MachineLogic.Architecture;

namespace MaszynaPi.MachineLogic.IODevices {

    /// <summary>
    /// Character <see cref="IOType.Output"/> device. Provides method for <see cref="CentralProcessingUnit"/> to write data to it.
    /// One character at time can be put into internal buffer and will remain there until UI representation handles it's content.
    /// </summary>
    public class CharacterOutput: IODevice 
    {
        const uint ID = 2;
        const IOType TYPE = IOType.Output;

        /// <summary>
        /// Representation of output buffer structure, where characters written by CPU are stored and can be viewed by user 
        /// in GUI representation of class (via <see cref="MachineUI.UserControlCharacterOutput"/> instance).
        /// </summary>
        readonly List<char> CharactersBuffer;

        /// <summary>
        /// Creates new instance of <see cref="CharacterOutput"/> class, assigning by defaut 
        /// <see cref="IODevice.DeviceID"/> of 2 and <see cref="IODevice.Type"/> of <see cref="IOType.Output"/>.
        /// </summary>
        /// <param name="g"><inheritdoc/></param>
        /// <param name="rb"><inheritdoc/></param>
        /// <param name="id"><inheritdoc/>. Default is <see cref="ID"/>.</param>
        /// <param name="iOType"><inheritdoc/>. Default is <see cref="TYPE"/> and should not be chaged.</param>
        public CharacterOutput(Register g, Register rb, uint id = ID, IOType iOType = TYPE) : base(g, rb, id, iOType) {
            CharactersBuffer = new List<char>();
        }

        /// <summary>Get representation of output buffer structure, where characters written by CPU are stored.</summary>
        public List<char> GetCharactersBufferHandle() { return CharactersBuffer; }

        /// <summary>
        /// Parametless <see cref="Action"/> invoked when new character is put into <see cref="CharactersBuffer"/> by instance of <see cref="CentralProcessingUnit"/>./
        /// </summary>
        public Action OnCharacterPushed;

        /// <summary>
        /// Set <see cref="IODevice.IO_READY"/> valu into <see cref="IODevice.Ready"/> <see cref="Register"/>.
        /// Push value from <see cref="IODevice.IOBuffer"/> <see cref="Register"/> to <see cref="CharactersBuffer"/> as character (ASCII Encoding)
        /// and invokes <see cref="OnCharacterPushed"/>.
        /// </summary>
        void PushChar() {
            SetReadyValue(IO_READY);
            CharactersBuffer.Add(Convert.ToChar(GetIOBufferValue()));
            OnCharacterPushed();
        }
        /// <summary>
        /// Checks if device is valid <see cref="IOType.Output"/> device using base implementation of <see cref="IODevice.ReadFromIOBuffer"/>.
        /// Set <see cref="IODevice.IO_READY"/> valu into <see cref="IODevice.Ready"/> <see cref="Register"/>.
        /// Push value from <see cref="IODevice.IOBuffer"/> <see cref="Register"/> to <see cref="CharactersBuffer"/> as character (ASCII Encoding)
        /// and invokes <see cref="OnCharacterPushed"/>.
        /// </summary>
        public override void ReadFromIOBuffer() {
            base.ReadFromIOBuffer();
            PushChar();
        }


    }
}
