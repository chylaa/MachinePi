using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;

namespace MaszynaPi.SenseHatHandlers {
    class SenseHatDevice {
        const int TO_MILI = 1000;
        public const string SENSOR_TEMPERATURE = "temperature";
        public const string SENSOR_PRESSURE= "pressure";
        public const string SENSOR_HUMIDITY = "humidity";

        public static string SENSOR_SCRIPT = "scripts/SensorsHandler.py";
        public static string JOYSTICK_SCRIPT = "scripts/JoystickHandler.py"; // "scripts/WinGetJoystickPos.py";
        public static string MATRIX_SCRIPT = "scripts/MatrixHandler.py";

        public static readonly string JOYSTICK_POS_PRESS = "middle";
        public readonly static Dictionary<string, int> JoystickPosIntMap = new Dictionary<string, int>(Defines.JOYSTICK_INTERRUPTS); //Position of joistick as string mapped to interruption number

        static readonly string StartPythonCMD = "python3"; // python3 must be added to system %PATH% variable!

        Process ReadProcess;
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
            ReadProcess.OutputDataReceived += DataReceived;
            ReadProcess.ErrorDataReceived += ErrorReceived;
        }

        // To be called in thread
        string GetData() {
            try {
                ReadProcess.Start();
                ReceivedData = ReadProcess.StandardOutput.ReadToEnd().Replace(Environment.NewLine, "");
                //Console.WriteLine("Get: " + ReceivedData);
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
            if(ReadProcess == null) { throw new Exception("Code Error: Read process not initialized: invoke SenseHatDevice method CreateReadProcess(string cmd)"); }

            AsyncRead = new BackgroundWorker();
            AsyncRead.WorkerReportsProgress = true;

            AsyncRead.DoWork += AsyncRead_DoWork;
            AsyncRead.ProgressChanged += AsyncRead_ProgressChanged;

            AsyncRead.RunWorkerAsync();
        }

        private void AsyncRead_DoWork(object sender, DoWorkEventArgs e) {
            ReadProcess.Start();
            ReadProcess.BeginErrorReadLine();
            ReadProcess.BeginOutputReadLine();

            string previous = ReceivedData;
            while(true) {
                var data = ReceivedData;
                if (data != null && data != previous ) {
                    //ReadProcess.WaitForInputIdle(10); //wait 10ms
                    //ReadProcess.WaitForExit(10);
                    previous = data;
                    if (JoystickPosIntMap.TryGetValue(data, out int reportedInt)) {
                        OnInterruptionReceived((uint)(reportedInt));
                        //(sender as BackgroundWorker).ReportProgress(reportedInt);
                    }
                    
                }
            }
        }

        private void AsyncRead_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            OnInterruptionReceived((uint)e.ProgressPercentage);
        }
        void DataReceived(object sender, DataReceivedEventArgs e) {
            ReceivedData = e.Data;
        }
        void ErrorReceived(object sender, DataReceivedEventArgs e) {
            //throw new Exception("Error while executing asynchronus script. Details: " + e.Data);
        }


    }
}
