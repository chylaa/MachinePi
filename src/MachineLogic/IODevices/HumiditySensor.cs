using MaszynaPi.MachineLogic.Architecture;
using MaszynaPi.SenseHatHandlers;

namespace MaszynaPi.MachineLogic.IODevices {

    /// <summary>
    /// <see cref="IODevice"/> of <see cref="IOType.Input"/> type. Provides method for <see cref="CentralProcessingUnit"/> to read 
    /// humidity data from <see cref="SenseHatDevice"/> sensor.
    /// </summary>
    class HumiditySensor : IODevice 
    {
        const uint ID = 4;
        const IOType TYPE = IOType.Input;

        /// <summary><see cref="SenseHatDevice"/> instance for communicating via python script processes with humidity sensor located on SenseHat module.</summary>
        readonly SenseHatDevice Sensor;

        /// <summary>Creates new instance of <see cref="HumiditySensor"/> <see cref="IODevice"/>.</summary>
        /// <param name="g"><inheritdoc path="/param[@name='g']"/></param>
        /// <param name="rb"><inheritdoc path="/param[@name='rb']"/></param>
        /// <param name="id"><inheritdoc path="/param[@name='id']"/>. Default is <see cref="ID"/>.</param>
        /// <param name="iOType"><inheritdoc path="/param[@name='iOType']"/>. Default is <see cref="TYPE"/> and should not be chaged.</param>
        public HumiditySensor(Register g, Register rb, uint id = ID, IOType iOType = TYPE) : base(g, rb, id, iOType) {
            Sensor = new SenseHatDevice();
            Sensor.CreateReadProcess(SenseHatDevice.SENSOR_SCRIPT + " " + SenseHatDevice.SENSOR_HUMIDITY);
        }

        /// <summary>
        /// Sets  <see cref="Register"/> <see cref="IODevice.Ready"/> bit and assign value retreived from 
        /// related <see cref="SenseHatDevice.GetSensorData()"/> into this instance's  <see cref="Register"/> <see cref="IODevice.IOBuffer"/>.
        /// </summary>
        void GetValue() {
            SetReadyValue(IO_READY);

            uint data = Sensor.GetSensorData();
            SetIOBufferValue(data);
        }

        /// <summary><inheritdoc cref="GetValue"/></summary>
        public override void WriteToIOBuffer() {
            base.WriteToIOBuffer();
            GetValue();
        }
    }

}
