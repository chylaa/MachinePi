using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading;


namespace MaszynaPi.SenseHatHandlers 
{
    /// <summary>General custom <see cref="Exception"/> for signalizing errors related to <see cref="SenseHatDevice"/> data/control flow.</summary>
    public class SenseHatException : Exception { public SenseHatException(string message) : base(message) { } }
    
    /// <summary>
    /// Base class for CPU's IO devices represented by RaspberryPI SenseHat module's devices.
    /// </summary>
    class SenseHatDevice 
    {
        /// <summary>Selector argument for <see cref="SENSOR_SCRIPT"/>, indicatinig temperature read.</summary>
        public const string SENSOR_TEMPERATURE = "temperature";
        /// <summary>Selector argument for <see cref="SENSOR_SCRIPT"/>, indicatinig pressure read.</summary>
        public const string SENSOR_PRESSURE= "pressure";
        /// <summary>Selector argument for <see cref="SENSOR_SCRIPT"/>, indicatinig humidity read.</summary>
        public const string SENSOR_HUMIDITY = "humidity";

        /// <summary>Path to python script performing communitation with SenseHat's sensors. Default in .exe scripts/ dir.</summary>
        public static string SENSOR_SCRIPT = "scripts/SensorsHandler.py";
        /// <summary>Path to python script performing communitation with SenseHat's joystick output. Default in .exe scripts/ dir.</summary>
        public static string JOYSTICK_SCRIPT = "scripts/JoystickHandler.py"; 
        /// <summary>Path to python script performing communitation with SenseHat's matrix. Default in .exe scripts/ dir.</summary>
        public static string MATRIX_SCRIPT = "scripts/MatrixHandler.py";

        /// <summary>Stdout message from <see cref="JOYSTICK_SCRIPT"/> that indicates joystick was pressed</summary>
        public static readonly string JOYSTICK_POS_PRESS = "middle";
        /// <summary>Maps joystick position string from <see cref="JOYSTICK_SCRIPT"/> std output into interrupt priority.</summary>
        public readonly static Dictionary<string, int> JoystickPosIntMap = new Dictionary<string, int>(Defines.JOYSTICK_INTERRUPTS); //Position of joistick as string mapped to interruption number

        /// <summary>Command for starting python script.</summary>
        static readonly string StartPythonCMD = "python3"; // python3 must be added to system %PATH% variable!

        string ReceivedData;
        Process ReadProcess;
        BackgroundWorker AsyncRead;

        /// <summary>Creates new <see cref="SenseHatDevice"/> instance initialized with received data '0'.</summary>
        public SenseHatDevice() {
            ReceivedData = "0";
        }

        /// <summary>
        /// Creates new <see cref="StartPythonCMD"/> process using <paramref name="cmd"/> as script argument.
        /// If current platform is not <see cref="PlatformID.Unix"/>, returns wihout creating <see cref="ReadProcess"/>.
        /// </summary>
        /// <param name="cmd">Path to python script that should be called</param>
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

        /// <summary>
        /// Allows to retreive single standard-output data from stript that's executing in <see cref="ReadProcess"/>.
        /// Method watis for <see cref="ReadProcess"/> to end with <see cref="Timeout.Infinite"/>. Script must not 
        /// get into infinite loop to avoid blocking.
        /// <br></br>Throws <see cref="SenseHatException"/> if <see cref="ReadProcess"/> is null or error occurs while reading data.
        /// </summary>
        /// <returns>String instance containing data read from <see cref="ReadProcess"></see> <see cref="Process.StandardOutput"/>.</returns>
        /// <exception cref="SenseHatException"></exception>
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
        /// <summary>
        /// Creates new <see cref="Process"/> using <see cref="StartPythonCMD"/> and passes <paramref name="cmd"/> as argument to script.
        /// Method watis for exit with <see cref="Timeout.Infinite"/>. Script must not get into infinite loop to avoid blocking.
        /// <br></br>Throws <see cref="SenseHatException"/> if error occurs during process execution.
        /// </summary>
        /// <exception cref="SenseHatException"></exception>
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

        /// <summary>
        /// Returns data from <see cref="ReadProcess"/> standard output parsed to float and casted into uint value.
        /// <br></br>Throws <see cref="SenseHatException"/> if parsing failed.
        /// </summary>
        /// <returns>uint representation of read from stdout stirng.</returns>
        /// <exception cref="SenseHatException"></exception>
        public uint GetSensorData() {
            GetData();
            if (float.TryParse(ReceivedData, out float parsed))
                return (uint)parsed;
            else
                throw new SenseHatException($"Parsing Sensor Data failed: {ReceivedData} is not floating point number.");
        }

        /// <summary>
        /// Starts new process of <see cref="MATRIX_SCRIPT"/> with argumetns given in 
        /// <paramref name="value"/> and <paramref name="mode"/> parameters using <see cref="SendData(string)"/> method.
        /// </summary>
        /// <param name="value">Value to be feed into <see cref="MATRIX_SCRIPT"/> as first call argument.</param>
        /// <param name="mode">String to be feed into <see cref="MATRIX_SCRIPT"/> as second call argument.</param>
        public void MatrixPrint(uint value, string mode) {
            SendData(MATRIX_SCRIPT + " " + value.ToString() + " " + mode);
        }


        /// <summary>
        /// Handler for <see cref="MachineLogic.CentralProcessingUnit"/> method that should be invoked on <see cref="JOYSTICK_SCRIPT"/>
        /// data receive. Parameter of <see cref="Action"/> will be taking value translated from joystick positions into interruption 
        /// priority using <see cref="JoystickPosIntMap"/>.
         /// </summary>
        public Action<uint> OnInterruptionReceived;


        /// <summary>
        /// If app runing on <see cref="PlatformID.Unix"/>, creates new <see cref="BackgroundWorker"/> instance (see <see cref="AsyncRead"/>) that
        /// can report progress and supports cacelation. Assigns <see cref="AsyncRead_DoWork(object, DoWorkEventArgs)"/> 
        /// into <see cref="BackgroundWorker.DoWork"/> event handler and runs asynchornus operation.
        /// <br></br>Throws <see cref="SenseHatException"/> if <see cref="ReadProcess"/> is not created before calling this method.
        /// </summary>
        /// <exception cref="SenseHatException"></exception>
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

        /// <summary>
        /// <see cref="AsyncRead"/> <see cref="BackgroundWorker.DoWork"/> event handler. 
        /// Starts <see cref="ReadProcess"/> and reads data from <see cref="Process.StandardOutput"/> until 
        /// <see cref="BackgroundWorker.CancellationPending"/> is false. After that, kills <see cref="ReadProcess"/>.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Provides data for handler.</param>
        private void AsyncRead_DoWork(object sender, DoWorkEventArgs e) {
            ReadProcess.Start();
            ReadProcess.BeginErrorReadLine();
            ReadProcess.BeginOutputReadLine();

            string previous = ReceivedData;
            while(false == e.Cancel && false == AsyncRead.CancellationPending) { 
                var data = ReceivedData;
                if (data != null && data != previous ) {
                    previous = data;
                    if (JoystickPosIntMap.TryGetValue(data, out int reportedInt)) {
                        OnInterruptionReceived((uint)(reportedInt));
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

        /// <summary>
        /// <see cref="ReadProcess"/>, <see cref="Process.OutputDataReceived"/> event.
        /// Assings <see cref="DataReceivedEventArgs.Data"/> to <see cref="ReceivedData"/> field.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Provides data from event handler.</param>
        void DataReceived(object sender, DataReceivedEventArgs e) {
            ReceivedData = e.Data;
        }

        /// <summary>
        /// <b>Currently no action performed</b> due do python scripts writing irrelevant warnings into error stream.
        /// Disards all <see cref="Process.StandardError"/> communicates.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Provides data from event handler.</param>
        void ErrorReceived(object sender, DataReceivedEventArgs e) {
            //throw new Exception("Error while executing asynchronus script. Details: " + e.Data);
        }


    }
}
