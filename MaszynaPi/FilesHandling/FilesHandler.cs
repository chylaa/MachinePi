using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace MaszynaPi.FilesHandling {

    class FileHandlerException : Exception { public FileHandlerException(string message) : base(message) { } }
    static class FilesHandler {

        public static bool PointFileAndGetPath(string dialogFilter, out string filepath) {
            filepath = "";
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                openFileDialog.Filter = dialogFilter;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    filepath = openFileDialog.FileName;
            }
            if (filepath.Length > 0) return true;
            return false;
        }
        public static bool PointFileAndGetText(string dialogFilter, out string filepath, out string fileContent) {
            fileContent = "";
            if(PointFileAndGetPath(dialogFilter,out filepath)) {
                try { fileContent = GetFileText(filepath); } 
                catch (FileHandlerException) { return false; } 
                catch (Exception ex) {throw new FileHandlerException("Unexpected Error while reading " + filepath + " file: " + ex.Message);} 
                return true;
            }
            return false;
        }

        public static string GetFileText(string filepath) {
            if (File.Exists(filepath) == false) throw new FileHandlerException("Cannot load instruction file " + filepath + ". File not exist.");
            Encoding encoding = GetEncoding(filepath, Encoding.Default);
            return File.ReadAllText(filepath, encoding);
        }


        /// Determines a text file's encoding by analyzing its byte order mark (BOM).
        /// Defaults set by "defaultEncoding" param when detection of the text file's endianness fails.
        public static Encoding GetEncoding(string filename, Encoding defaultEncoding) {
            var bom = new byte[4];
            using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read)) {
                file.Read(bom, 0, 4);
            }
            // Analyze the BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
            if (bom[0] == 0xff && bom[1] == 0xfe && bom[2] == 0 && bom[3] == 0) return Encoding.UTF32; //UTF-32LE
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return new UTF32Encoding(true, true);  //UTF-32BE

            return defaultEncoding;
        }
    }
}
