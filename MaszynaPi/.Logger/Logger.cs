using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaszynaPi.Logger {
    static public class Logger {
        static readonly string WorkingDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
        static readonly string LogFolderName = "logs";
        static readonly string LogFileName = "log" + DateTime.Now.ToString("dd") + "_" + DateTime.Now.ToString("HH_mm_ss") + ".txt";

        static readonly string LogFolderPath = Path.Combine(WorkingDirectory, LogFolderName);
        static readonly string LogFilePath = Path.Combine(LogFolderPath, LogFileName);

        static bool FILE_ENABLED = false;
        static bool CONSOLE_ENABLED = false;

        const char DIVIDER = '=';
        const int DIV_LEN = 30;

        public static void EnableFileLog(string additionalName="") {
            LogFilePath.Replace(".txt", additionalName + ".txt");
            if (!Directory.Exists(LogFolderPath)) { Directory.CreateDirectory(LogFolderPath); }
            FILE_ENABLED = true;
        }
        public static void DisableFileLog() { FILE_ENABLED = false; }
        public static void EnableConsoleLog() { CONSOLE_ENABLED = true; }
        public static void DisableConsoleLog() { CONSOLE_ENABLED = false; }
        public static void EnableAll() { EnableFileLog(); EnableConsoleLog(); }
        public static void DisableAll() { DisableFileLog(); DisableConsoleLog(); }

        public static void DeleteFileLog() { if (File.Exists(LogFilePath)) File.Delete(LogFilePath); }


        static void Log(string msg, bool NL) {
            if (CONSOLE_ENABLED) Console.WriteLine(msg);
            if (FILE_ENABLED == false) return;
            using (StreamWriter sw = File.AppendText(LogFilePath)) {
                if (NL) sw.Write(Environment.NewLine);
                sw.Write(msg + Environment.NewLine);
            }
        }
        public static void Div(bool NL = false) {
            Log(new string(DIVIDER, DIV_LEN),NL);
        }
        public static void LogInfo(string msg, bool NL = false) { 
            Log(msg, NL); 
        }
        public static void LogError(string msg, bool NL = false) {
            Log("[ERROR]: "+msg, NL);
        }
        public static void LogWarning(string msg, bool NL = false) {
            Log("[WARNING]: " + msg, NL);
        }
    }
}
