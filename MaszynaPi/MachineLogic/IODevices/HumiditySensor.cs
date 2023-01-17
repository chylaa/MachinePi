using MaszynaPi.MachineLogic.Architecture;
using MaszynaPi.SenseHatHandlers;

namespace MaszynaPi.MachineLogic.IODevices {
    class HumiditySensor : IODevice {
        const uint ID = 4;
        const IOType TYPE = IOType.Input;

        SenseHatDevice Sensor;

        public HumiditySensor(Register g, Register rb, uint id = ID, IOType iOType = TYPE) : base(g, rb, id, iOType) {
            Sensor = new SenseHatDevice();
            Sensor.CreateReadProcess(SenseHatDevice.SENSOR_SCRIPT + " " + SenseHatDevice.SENSOR_HUMIDITY);
        }

        void GetValue() {
            SetReadyValue(IO_READY);

            uint data = Sensor.GetSensorData();
            SetIOBufferValue(data);
        }

        public override void WriteToIOBuffer() {
            base.WriteToIOBuffer();
            GetValue();
        }
    }

}
