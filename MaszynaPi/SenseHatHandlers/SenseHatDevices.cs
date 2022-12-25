using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.MachineLogic;

namespace MaszynaPi.SenseHatHandlers {
    class SenseHatDevice {
        const int TO_MILI = 1000;

        const string SENSOR_NONE = "[SENSOR_N]";
        const string SENSOR_TEMPERATURE = "temperature";
        const string SENSOR_PRESSURE= "pressure";
        const string SENSOR_HUMIDITY = "humidity";

        const string GET_SENSOR_VALUE_BASE = "from sense_hat import SenseHat\nsense = SenseHat()\nprint(str(sense.get_"+SENSOR_NONE+"()))\nsys.stdout.flush()\n";

        public static readonly string JOYSTICK_POS_PRESS = "middle";
        public readonly static Dictionary<string, uint> JoystickPosIntMap = new Dictionary<string, uint>(Defines.JOYSTICK_INTERRUPTS); //Position of joistick as string mapped to interruption number

        // Python Scripts for reading SenseHat states
        public static readonly string GetJStatePythonSctipt = @"import sys
                                                                from sense_hat import SenseHat
                                                                sense = SenseHat()
                                                                while True:
                                                                    for e in sense.stick.get_events():
                                                                        if e.action=='pressed': 
                                                                            print(str(e.direction))
                                                                            sys.stdout.flush()";
        
        public static readonly string GetTemperaturePythonSctipt = GET_SENSOR_VALUE_BASE.Replace(SENSOR_NONE, SENSOR_TEMPERATURE);
        public static readonly string GetPressurePythonSctipt = GET_SENSOR_VALUE_BASE.Replace(SENSOR_NONE, SENSOR_PRESSURE);
        public static readonly string GetHumidityPythonSctipt = GET_SENSOR_VALUE_BASE.Replace(SENSOR_NONE, SENSOR_HUMIDITY);

        string PythonPath;

        string ReceivedData;

        public SenseHatDevice() {
            PythonPath = "python3";
        }


        // To be called in thread
        public void GetData(string cmd) {
            using (Process proc = new Process()) {
                proc.StartInfo = new ProcessStartInfo(PythonPath) { 
                    Arguments = cmd,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = Environment.CurrentDirectory,

                };
                proc.EnableRaisingEvents = true;
                proc.ErrorDataReceived += OnErrorReceived;
                proc.OutputDataReceived += OnOutputReceived;

                proc.Start();
                proc.WaitForExit();
            }
        }


        // Should be CentralUnit method for settings proper interuption
        public Action<uint> OnDataReceived;

        // Different methods on transforming SenseHat data, based on whitch sensor was used 
        public uint GetDataAsTemperature() {
            uint temp = (uint)float.Parse(ReceivedData)*TO_MILI;
            return temp;
        }
        public uint GetDataAsHumidity() {

            return 0;
        }
        public uint GetDataAsPressure() {
            return 0;
        }
        public uint GetDataAsInterruption() {
            return 0;
        }

        void OnOutputReceived(object sender, DataReceivedEventArgs e) {
            ReceivedData = e.Data;
            Console.WriteLine("Get: " + ReceivedData);
        }
        void OnErrorReceived(object sender, DataReceivedEventArgs e) {
                throw new Exception("Error while getting data from SenseHat Device. Details: " + e.Data);
        }

    }
}
