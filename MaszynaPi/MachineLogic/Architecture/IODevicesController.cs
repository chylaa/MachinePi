using System.Collections.Generic;
using MaszynaPi.MachineLogic.IODevices;

namespace MaszynaPi.MachineLogic.Architecture 
{
    /// <summary>
    /// Class representing input/output devices controller, providing 
    /// <see cref="HandleIOOnStartSignal(uint)"/> method for writing/reading data
    /// to/from particular <see cref="IODevice"/>.
    /// </summary>
    class IODevicesController {
        
        private readonly Dictionary<uint, IODevice> IODevices;

        /// <summary>
        /// Creates new <see cref="IODevicesController"/> with internal set of passed IO <paramref name="devices"/>.
        /// </summary>
        /// <param name="devices">Collection of available <see cref="IODevice"/> instances.</param>
        public IODevicesController(params IODevice[] devices) {
            IODevices = new Dictionary<uint, IODevice>();
            foreach(var IO in devices) IODevices.Add(IO.GetID(),IO);
        }

        /// <summary>
        /// Invokes read/write IO buffer operation of <see cref="IODevice"/>, 
        /// visible under memory-mapped address <paramref name="IOAddress"/> 
        /// (I/O base on <see cref="IOType"/> of assosciated <see cref="IODevice"/>).
        /// </summary>
        /// <param name="IOAddress">Memory-mapped address of <see cref="IODevice"/>.</param>
        public void HandleIOOnStartSignal(uint IOAddress) {
            if (IODevices.Count == 0) return;
            uint id = ArchitectureSettings.GetIODeviceID(IOAddress);
            IODevice ActiveIO = IODevices[id];
            var type = ActiveIO.GetIOType();
            if (type == IOType.Input) {
                ActiveIO.WriteToIOBuffer();
            }
            if(type == IOType.Output) {
                ActiveIO.ReadFromIOBuffer();
            }
        }

    }
}
