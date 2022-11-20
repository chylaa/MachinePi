using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.MachineLogic.Architecture;

namespace MaszynaPi.MachineLogic.IODevices {

    public class IODeviceException : Exception { public IODeviceException(string message) : base(message) { } }

    public enum IOType { Input, Output, InputOutput };

    public abstract class IODevice {

        public const uint IO_WAIT = 0;
        public const uint IO_READY = 1;

        protected IOType Type; 

        protected bool START_FLAG;
        protected Register Ready;
        protected Register IOBuffer;
        protected IODevice(Register g, Register rb, IOType iOType) {
            Ready = g;
            IOBuffer = rb;
            START_FLAG = false;
            Type = iOType;
        }

        public void SetStartFlag(bool set = true) { START_FLAG = set; }
        public void ClearStartFlag() { START_FLAG = false; }
        public bool IsStartFlagSet() { return START_FLAG; }
        public uint GetReadyValue() { return Ready.GetValue(); }
        public void SetReadyValue(uint value) { Ready.SetValue(value); }

        // For microinstructions get; set;
        public uint GetIOtBufferValue() { return IOBuffer.GetValue(); }
        public void SetIOBufferValue(uint value) { IOBuffer.SetValue(value); }


        //For external devices:  Buffer -> IO
        public virtual void ReadFromIOBuffer() {
            if (Type.Equals(IOType.Input)) throw new IODeviceException("Input Device tryig to read from buffer."); 
        }
        //For external devices:  IO -> Buffer 
        public virtual void WriteToIOBuffer() { 
            if (Type.Equals(IOType.Output)) throw new IODeviceException("Output Device tryig to write to buffer."); 
        }
    }
}
