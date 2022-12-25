using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.MachineLogic.Architecture;
using MaszynaPi.SenseHatHandlers;


namespace MaszynaPi.MachineLogic.IODevices {
    public class TemperatureSensor : IODevice {
        const uint ID = 3;
        const IOType TYPE = IOType.Input;

        SenseHatDevice Sensor;

        public TemperatureSensor(Register g, Register rb, uint id = ID, IOType iOType = TYPE) : base(g, rb, id, iOType) {
            Sensor = new SenseHatDevice();
        }

        public Action OnCharacterFetched;

        // Temperature get in mili-celcius
        void GetValue() {
            SetReadyValue(IO_READY);
           
            Sensor.GetData(SenseHatDevice.GetTemperaturePythonSctipt);
            uint data = Sensor.GetDataAsTemperature();
            SetIOBufferValue(data);
        }

        public override void WriteToIOBuffer() {
            base.WriteToIOBuffer();
            GetValue();
        }
    }
}

