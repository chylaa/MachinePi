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
        const string SENSOR_TEMPERATURE = "temperature";
        const string SENSOR_PRESSURE= "pressure";
        const string SENSOR_HUMIDITY = "humidity";

        const string SENSOR_SCRIPT = "scripts\\GetSensor.py";

        public static readonly string JOYSTICK_POS_PRESS = "middle";
        public readonly static Dictionary<string, uint> JoystickPosIntMap = new Dictionary<string, uint>(Defines.JOYSTICK_INTERRUPTS); //Position of joistick as string mapped to interruption number

        static readonly string StartPythonCMD = "python3"; 

        string ReceivedData;

        public SenseHatDevice() {
        }

        // To be called in thread
        public string GetData(string cmd) {
            using (Process proc = new Process()) {
                proc.StartInfo = new ProcessStartInfo(StartPythonCMD) {
                    Arguments = cmd,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = Environment.CurrentDirectory,

                };
                try {
                    proc.Start();
                    ReceivedData = proc.StandardOutput.ReadToEnd().Replace(Environment.NewLine, "");
                    Console.WriteLine("Get: " + ReceivedData);
                    proc.WaitForExit();
                } catch (Exception e) {
                    throw new Exception("Error while getting data from SenseHat Device. Details: " + e.Data);
                }
            }
            return ReceivedData;
        }


        // Should be CentralUnit method for settings proper interuption
        public Action<uint> OnDataReceived;

        // Different methods on transforming SenseHat data, based on whitch sensor was used 
        public uint GetTemperatureData() {
            GetData(SENSOR_SCRIPT + " " + SENSOR_TEMPERATURE);
            uint temp = (uint)float.Parse(ReceivedData);
            return temp;
        }
        public uint GetHumidityData() {

            return 0;
        }
        public uint GetPressureData() {
            return 0;
        }
        public uint GetDataAsInterruption() {
            return 0;
        }

        void OnErrorReceived(object sender, DataReceivedEventArgs e) {
               
        }

    }
}

/* OLD
         const string SENSOR_NONE = "[SENSOR_N]";

        // Python Scripts for reading SenseHat states
        public static readonly string GetJStatePythonSctipt = "import sys\nfrom sense_hat import SenseHat\nsense = SenseHat()\nwhile True:\n    for e in sense.stick.get_events():\n        if e.action==\"pressed\": \n          print(str(e.direction))\n           sys.stdout.flush()";
        
 const string GET_SENSOR_VALUE_BASE = "from sense_hat import SenseHat\nsense = SenseHat()\nprint(str(sense.get_"+SENSOR_NONE+"()))\nsys.stdout.flush()\n";

        public static readonly string GetTemperaturePythonSctipt = GET_SENSOR_VALUE_BASE.Replace(SENSOR_NONE, SENSOR_TEMPERATURE);
        public static readonly string GetPressurePythonSctipt = GET_SENSOR_VALUE_BASE.Replace(SENSOR_NONE, SENSOR_PRESSURE);
        public static readonly string GetHumidityPythonSctipt = GET_SENSOR_VALUE_BASE.Replace(SENSOR_NONE, SENSOR_HUMIDITY);
 
 */