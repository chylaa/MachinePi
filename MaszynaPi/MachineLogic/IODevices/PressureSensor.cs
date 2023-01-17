using MaszynaPi.MachineLogic.Architecture;
using MaszynaPi.SenseHatHandlers;

namespace MaszynaPi.MachineLogic.IODevices {
    class PressureSensor : IODevice {
        const uint ID = 5;
        const IOType TYPE = IOType.Input;

        SenseHatDevice Sensor;

        public PressureSensor(Register g, Register rb, uint id = ID, IOType iOType = TYPE) : base(g, rb, id, iOType) {
            Sensor = new SenseHatDevice();
            Sensor.CreateReadProcess(SenseHatDevice.SENSOR_SCRIPT + " " + SenseHatDevice.SENSOR_PRESSURE);
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
