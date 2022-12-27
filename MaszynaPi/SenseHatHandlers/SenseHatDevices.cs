using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using MaszynaPi.MachineLogic;

namespace MaszynaPi.SenseHatHandlers {
    class SenseHatDevice {
        const int TO_MILI = 1000;
        public const string SENSOR_TEMPERATURE = "temperature";
        public const string SENSOR_PRESSURE= "pressure";
        public const string SENSOR_HUMIDITY = "humidity";

        public const string SENSOR_SCRIPT = "scripts/GetSensor.py";
        public const string JOYSTICK_SCRIPT = "scripts/GetJoystickPos.py";
        public const string MATRIX_SCRIPT = "scripts/MatrixPrint.py";

        public static readonly string JOYSTICK_POS_PRESS = "middle";
        public readonly static Dictionary<string, int> JoystickPosIntMap = new Dictionary<string, int>(Defines.JOYSTICK_INTERRUPTS); //Position of joistick as string mapped to interruption number

        static readonly string StartPythonCMD = "python3";

        Process ReadProcess;
        Process SendProcess;
        string ReceivedData;
        BackgroundWorker AsyncRead;
        public SenseHatDevice() {
            ReceivedData = "0";
        }

        public void CreateReadProcess(string cmd) {
            ReadProcess = new Process();
            ReadProcess.StartInfo = new ProcessStartInfo(StartPythonCMD) {
                Arguments = cmd,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = Environment.CurrentDirectory,//System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase),
            };
        }

        // To be called in thread
        string GetData() {
            try {
                ReadProcess.Start();
                ReceivedData = ReadProcess.StandardOutput.ReadToEnd().Replace(Environment.NewLine, "");
                Console.WriteLine("Get: " + ReceivedData);
                ReadProcess.WaitForExit();
            } catch (Exception e) {
                throw new Exception("Error while getting data from SenseHat Device. Details: " + e.Data);
            }
            return ReceivedData;
        }
 

        void SendData(string cmd) {
            using (Process proc = new Process()) {
                proc.StartInfo = new ProcessStartInfo(StartPythonCMD) {
                    Arguments = cmd,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = Environment.CurrentDirectory,//System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase),
                };
                try {
                    proc.Start();
                    proc.WaitForExit();
                } catch (Exception e) {
                    throw new Exception("Error while sending data to SenseHat Device. Details: " + e.Data);
                }
            }
        }


        public uint GetSensorData() {
            GetData();
            uint temp = (uint)float.Parse(ReceivedData);
            return temp;
        }

        public void MatrixPrint(uint value, string mode) {
            SendData(MATRIX_SCRIPT + " " + value.ToString() + " " + mode);
        }

        // Should be CentralUnit method for settings proper interuption
        public Action<uint> OnInterruptionReceived;

        public void StartAsyncRead() {

            AsyncRead = new BackgroundWorker();
            AsyncRead.WorkerReportsProgress = true;

            AsyncRead.DoWork += AsyncRead_DoWork;
            AsyncRead.ProgressChanged += AsyncRead_ProgressChanged;

            AsyncRead.RunWorkerAsync();
        }

        private void AsyncRead_DoWork(object sender, DoWorkEventArgs e) {
            while (true) {
                string previous = ReceivedData;
                GetData();
                if (ReceivedData.Equals(previous) == false) {
                    if (JoystickPosIntMap.TryGetValue(ReceivedData, out int reportedInt))
                        (sender as BackgroundWorker).ReportProgress(reportedInt);
                }
            }
        }

        private void AsyncRead_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            OnInterruptionReceived((uint)e.ProgressPercentage);
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

void GetDataAsynchronous(string cmd) {
            using (Process proc = new Process()) {
                proc.StartInfo = new ProcessStartInfo(StartPythonCMD) {
                    Arguments = cmd,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = Environment.CurrentDirectory,//System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase),
                };
                try {
                    proc.OutputDataReceived += DataReceived;
                    proc.ErrorDataReceived += ErrorReceived;
                    proc.Start();
                    proc.BeginErrorReadLine();
                    proc.BeginOutputReadLine();
                    proc.WaitForExit();
                } catch (Exception e) {
                    throw new Exception("Error while getting data from SenseHat Device. Details: " + e.Data);
                }
            }
        }
 

        void DataReceived(object sender, DataReceivedEventArgs e) {
            ReceivedData = e.Data;
            OnDataReceived(uint.Parse(ReceivedData));
        }
        void ErrorReceived(object sender, DataReceivedEventArgs e) {
            throw new Exception("Error while executing asynchronus script. Details: " + e.Data);
        }
 */