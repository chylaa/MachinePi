using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.MachineLogic.Architecture;
using MaszynaPi.SenseHatHandlers;

namespace MaszynaPi.MachineLogic.IODevices {
    class HumiditySensor : IODevice {
        const uint ID = 4;
        const IOType TYPE = IOType.Input;

        SenseHatDevice Sensor;

        public HumiditySensor(Register g, Register rb, uint id = ID, IOType iOType = TYPE) : base(g, rb, id, iOType) {
            Sensor = new SenseHatDevice();
        }
        public Action OnCharacterFetched;

        // Temperature get in mili-celcius
        void GetValue() {
            SetReadyValue(IO_READY);

            uint data = Sensor.GetHumidityData();
            SetIOBufferValue(data);
        }

        public override void WriteToIOBuffer() {
            base.WriteToIOBuffer();
            GetValue();
        }
    }

}
