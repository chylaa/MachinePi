using System;
using MaszynaPi.MachineLogic.Architecture;

namespace MaszynaPi.MachineLogic.IODevices {

    /// <summary>General <see cref="Exception"/> for errors related to <see cref="IODevice"/> classes.</summary>
    public class IODeviceException : Exception { public IODeviceException(string message) : base(message) { } }

    /// <summary>Enumeration representing different types of <see cref="IODevice"/>es.</summary>
    public enum IOType { Input, Output, InputOutput };  
    
    /// <summary>
    /// Abstract class providing template for basic <see cref="IODevice"/> that can be <see cref="IOType.Input"/> device
    /// <see cref="IOType.Output"/> or both.
    /// </summary>
    public abstract class IODevice {
        /// <summary>Uint flag value indicating that device is in processing state - data awaited.</summary>
        public const uint IO_WAIT = 0;
        /// <summary>Uint flag value indicating that device is ready to take/give data.</summary>
        public const uint IO_READY = 1;

        /// <summary>ID of device.</summary>
        protected readonly uint DeviceID;
        /// <summary>Type of device.</summary>
        protected readonly IOType Type; 


        /// <summary>Internal 1 bit <see cref="Register"/> storing information about ready state (<see cref="IO_WAIT"/>/<see cref="IO_READY"/>).</summary>
        protected readonly Register Ready;
        /// <summary>Internal <see cref="Register"/> acting as buffer for data transmitted from/to I/O.</summary>
        protected readonly Register IOBuffer;

        /// <summary>Default constructior for new I/O device of given <paramref name="iOType"/> and <paramref name="id"/>. </summary>
        /// <param name="g">Instance of CPU's "Ready" register. </param>
        /// <param name="rb">Instance of CPU's "I/O Buffer" register.</param>
        /// <param name="id">Requested ID number of device.</param>
        /// <param name="iOType">Type of device. Will affect <see cref="virtual"/> read/write methdos that can be used.</param>
        protected IODevice(Register g, Register rb, uint id, IOType iOType) {
            Ready = g;
            IOBuffer = rb;
            Type = iOType;
            DeviceID = id;
        }

        /// <summary>Provides access to <see cref="Ready"/> <see cref="Register.Value"/>.</summary>
        /// <returns>1 if IO data is ready 0 otherwise.</returns>
        protected uint GetReadyValue() { return Ready.GetValue(); }

        /// <summary>Allows to set <see cref="Ready"/> <see cref="Register.Value"/>.</summary>
        /// <param name="value">New value of ready register. <see cref="Register.SetValue(uint)"/> will handle potential overflow of 1-bit datasize.</param>
        protected  void SetReadyValue(uint value) { Ready.SetValue(value); }

        /// <summary>Allows to access current I/O buffer <see cref="Register.Value"/></summary>
        /// <returns>Value stored in internal I/O data buffer register. Used mainly for microinstruction datafolow access.</returns>
        protected uint GetIOBufferValue() { return IOBuffer.GetValue(); }

        /// <summary>Allows to set <see cref="IOBuffer"/> <see cref="Register.Value"/>.</summary>
        /// <param name="value">New value of data buffer register. <see cref="Register.SetValue(uint)"/> will handle potential overflow of set datasize.</param>
        protected void SetIOBufferValue(uint value) { IOBuffer.SetValue(value); }
        
        /// <summary>Allows to retreive <see cref="IOType"/> of device.</summary>
        /// <returns>Device <see cref="IOType"/>.</returns>
        public IOType GetIOType() { return Type; }

        /// <summary>Allows to retreive <see cref="DeviceID"/>.</summary>
        /// <returns>Device ID value.</returns>
        public uint GetID() { return DeviceID; }

        /// <summary>
        /// Virtual template of I/O Buffer reading method for <b>external devices</b>: Buffer -> IO. Provides check of Device's <see cref="IOType"/> 
        /// (buffer can be read only by <see cref="IOType.Output"/> device, otherwise <see cref="IODeviceException"/> is thrown).
        /// </summary>
        /// <exception cref="IODeviceException"></exception>
        public virtual void ReadFromIOBuffer() {
            if (Type.Equals(IOType.Input)) throw new IODeviceException("Input Device tryig to read from buffer."); 
        }

        /// <summary>
        /// Virtual template of I/O Buffer writing method for <b>external devices</b>: IO -> Buffer . Provides check of Device's <see cref="IOType"/> 
        /// (value can be written to buffer only by <see cref="IOType.Input"/> device, otherwise <see cref="IODeviceException"/> is thrown).
        /// </summary>
        /// <exception cref="IODeviceException"></exception>
        public virtual void WriteToIOBuffer() { 
            if (Type.Equals(IOType.Output)) throw new IODeviceException("Output Device tryig to write to buffer."); 
        }
    }
}
