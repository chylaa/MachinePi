using System.Collections.Generic;
using MaszynaPi.MachineLogic.IODevices;
namespace MaszynaPi.MachineLogic.Architecture {
    class IODevicesController {
        private readonly Dictionary<uint, IODevice> IODevices;

        public IODevicesController(params IODevice[] devices) {
            IODevices = new Dictionary<uint, IODevice>();
            foreach(var IO in devices) IODevices.Add(IO.GetID(),IO);
        }

        // IOAddress as One of IO Instructions Argument
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
