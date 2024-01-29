using System;
using MaszynaPi.MachineLogic.Architecture;
using MaszynaPi.SenseHatHandlers;


namespace MaszynaPi.MachineLogic.IODevices {
    public class TemperatureSensor : IODevice {
        const uint ID = 3;
        const IOType TYPE = IOType.Input;

        readonly SenseHatDevice Sensor;

        public TemperatureSensor(Register g, Register rb, uint id = ID, IOType iOType = TYPE) : base(g, rb, id, iOType) {
            Sensor = new SenseHatDevice();
            Sensor.CreateReadProcess(SenseHatDevice.SENSOR_SCRIPT + " " + SenseHatDevice.SENSOR_TEMPERATURE);
        }
        public Action OnCharacterFetched;

        /// <summary>Sets <see cref="IODevice.IOBuffer"/> value to <see cref="SenseHatDevice"/> read data (in mili-celcius).</summary>
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

