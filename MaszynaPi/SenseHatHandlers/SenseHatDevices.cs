using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading;


namespace MaszynaPi.SenseHatHandlers 
{
    /// <summary>General custom <see cref="Exception"/> for signalizing errors related to <see cref="SenseHatDevice"/> data/control flow.</summary>
    public class SenseHatException : Exception { public SenseHatException(string message) : base(message) { } }
    
    class SenseHatDevice 
    {
        public const string SENSOR_TEMPERATURE = "temperature";
        public const string SENSOR_PRESSURE= "pressure";
        public const string SENSOR_HUMIDITY = "humidity";

        public static string SENSOR_SCRIPT = "scripts/SensorsHandler.py";
        public static string JOYSTICK_SCRIPT = "scripts/JoystickHandler.py"; // "scripts/WinGetJoystickPos.py";
        public static string MATRIX_SCRIPT = "scripts/MatrixHandler.py";

        public static readonly string JOYSTICK_POS_PRESS = "middle";
        public readonly static Dictionary<string, int> JoystickPosIntMap = new Dictionary<string, int>(Defines.JOYSTICK_INTERRUPTS); //Position of joistick as string mapped to interruption number

        static readonly string StartPythonCMD = "python3"; // python3 must be added to system %PATH% variable!

        string ReceivedData;
        Process ReadProcess;
        BackgroundWorker AsyncRead;

        public SenseHatDevice() {
            ReceivedData = "0";
        }

        public void CreateReadProcess(string cmd) {
            if (Environment.OSVersion.Platform != PlatformID.Unix)
                return;

            ReadProcess = new Process
            {
                StartInfo = new ProcessStartInfo(StartPythonCMD)
                {
                    Arguments = cmd,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = Environment.CurrentDirectory,//System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase),
                }
            };
            ReadProcess.OutputDataReceived += DataReceived;
            ReadProcess.ErrorDataReceived += ErrorReceived;
        }

        // To be called in thread
        string GetData() {
            if (ReadProcess is null)
                throw new SenseHatException("Sense Hat module Read Process not initailzed. Application probably does not run on RasperryPi device.");
            try {
                ReadProcess.Start();
                ReceivedData = ReadProcess.StandardOutput.ReadToEnd().Replace(Environment.NewLine, "");
                //Console.WriteLine("Get: " + ReceivedData);
                ReadProcess.WaitForExit();
            } catch (Exception e) {
                throw new SenseHatException("Error while getting data from SenseHat Device. Check SanseHat module connection. Details: " + e.Message);
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
                    throw new SenseHatException("Error while sending data to SenseHat Device. Check SanseHat module connection. Details: " + e.Message);
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
            if (Environment.OSVersion.Platform != PlatformID.Unix || (AsyncRead != null && AsyncRead.IsBusy))
                return;
            if (ReadProcess == null)  
                throw new SenseHatException("Code Error: Read process not initialized: invoke SenseHatDevice method CreateReadProcess(string cmd)");

            
            AsyncRead = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            AsyncRead.DoWork += AsyncRead_DoWork;

            AsyncRead.RunWorkerAsync();
        }

        /// <summary>
        /// Stops asynchronus operation of reading data from <see cref="JOYSTICK_SCRIPT"/> using <see cref="ReadProcess"/>/.
        /// If <see cref="ReadProcess"/> or <see cref="AsyncRead"/> is null, returns without taking any actions.
        /// Throws <see cref="Exception"/> if reated <see cref="BackgroundWorker.IsBusy"/> after <paramref name="cancelTimeout"/>. 
        /// </summary>
        /// <param name="cancelTimeout"></param>
        /// <exception cref="Exception"></exception>
        public void StopAsyncRead(TimeSpan cancelTimeout)
        {
            if (ReadProcess is null || AsyncRead is null)
                return;

            AsyncRead.CancelAsync();
            if (false == SpinWait.SpinUntil(() => AsyncRead.IsBusy, cancelTimeout))
                throw new SenseHatException($"Cancelation Timeout! Cannot stop SenseHatDevice async read within {cancelTimeout.TotalMilliseconds}[ms].");
            
            AsyncRead.Dispose();
        }

        private void AsyncRead_DoWork(object sender, DoWorkEventArgs e) {
            ReadProcess.Start();
            ReadProcess.BeginErrorReadLine();
            ReadProcess.BeginOutputReadLine();

            string previous = ReceivedData;
            while(false == (e.Cancel = AsyncRead.CancellationPending)) { 
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
            try
            {
                ReadProcess.CancelErrorRead();
                ReadProcess.CancelOutputRead();
                ReadProcess.Kill();
                ReadProcess.Dispose();
            } catch (Exception ex)
            { 
                Console.WriteLine($"Error Stopping ReadProcess failed: {ex}"); 
            }
        }

        void DataReceived(object sender, DataReceivedEventArgs e) {
            ReceivedData = e.Data;
        }
        void ErrorReceived(object sender, DataReceivedEventArgs e) {
            //throw new Exception("Error while executing asynchronus script. Details: " + e.Data);
        }


    }
}
