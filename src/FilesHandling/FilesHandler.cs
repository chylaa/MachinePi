using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace MaszynaPi.FilesHandling {
    /// <summary> General exception of <see cref="FilesHandler"/> class </summary>
    class FileHandlerException : Exception { public FileHandlerException(string message) : base(message) { } }
    
    /// <summary>Static class providing methods for handling displaing specific <see cref="FileDialog"/> to user.</summary>
    static class FilesHandler {

        /// <summary>
        /// Base on <paramref name="filepath"/> parameter, returns its <see cref="Directory"/> 
        /// component if it is valid path, or current working directory.
        /// </summary>
        /// <param name="filepath">Potential path to file </param>
        /// <returns><see cref="Directory"/> component of <paramref name="filepath"/> if exist, current working directory otherwise.</returns>
        public static string ValidDirOrCurrent(string filepath)
        {
            if (string.IsNullOrEmpty(filepath)) return Directory.GetCurrentDirectory();
            string dir = Path.GetDirectoryName(filepath);
            if (Directory.Exists(dir)) return dir;
            else return Directory.GetCurrentDirectory();
        }

        /// <summary>Opens <see cref="OpenFileDialog"/> with passed <paramref name="dialogFilter"/> and assign path of selected file to <paramref name="filepath"/>.</summary>
        /// <param name="dialogFilter"><see cref="OpenFileDialog"/> filter string.</param>
        /// <param name="initDir">Initial for <see cref="OpenFileDialog"/>.</param>
        /// <param name="filepath">Output parameter which will store path to pointed file or <see cref="String.Empty"/> if no file selected.</param>
        /// <returns>true if file was selected, false otherwise.</returns>
        public static bool PointFileAndGetPath(string dialogFilter, string initDir, out string filepath) {
            filepath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.InitialDirectory = initDir;
                openFileDialog.Filter = dialogFilter;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    filepath = openFileDialog.FileName;
            }
            if (filepath.Length > 0) return true;
            return false;
        }
        /// <summary>Allows to get filepath from user using <see cref="SaveFileDialog"/>. Path of file is assigned to <paramref name="filepath"/> param.</summary>
        /// <param name="dialogFilter"><see cref="SaveFileDialog"/> filter string.</param>
        /// <param name="initDir">Initial for <see cref="SaveFileDialog"/>.</param>
        /// <param name="filepath">Output parameter which will store path to pointed file or <see cref="String.Empty"/> if no file selected.</param>
        /// <returns>true if file to save was selected, false otherwise.</returns>
        public static bool PointToOvervriteFileOrCreateNew(string dialogFilter, string initDir, out string filepath) {
            filepath = string.Empty;
            using (SaveFileDialog saveFileDialog = new SaveFileDialog()) {
                saveFileDialog.InitialDirectory = initDir;
                saveFileDialog.Filter = dialogFilter;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.AddExtension = true;
                saveFileDialog.DefaultExt = Defines.PROGRAM_FILE_EXTENSION;
                saveFileDialog.CheckPathExists = true;
                saveFileDialog.CheckFileExists = true;
                saveFileDialog.ValidateNames = false;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    filepath = saveFileDialog.FileName;
            }
            if (filepath.Length > 0) return true;
            return false;
        }
        /// <summary>Creates or overwrites file pointed by <paramref name="filepath"/> with <paramref name="content"/>.</summary>
        /// <param name="content">Data to write to file. <see cref="string"/> and <see cref="List{String}"/> are supproted.</param>
        /// <param name="filepath">Path to file to be written.</param>
        /// <exception cref="FileHandlerException"></exception>
        public static void OverwriteOrCreateFile(object content, string filepath) {
            if (File.Exists(filepath) == false) File.Create(filepath);
            using (StreamWriter outputFile = new StreamWriter(filepath, false, Encoding.GetEncoding("ISO-8859-1"))) // ISO-8859-2
           {
                if (content is List<string>) {
                    content = RemoveExcessiveEmptyStrings(content as List<string>);
                    foreach (string line in content as List<string>)
                        outputFile.WriteLine(line);
                } else if (content is string) {
                    outputFile.Write(content as string);
                } else throw new FileHandlerException($"{content.GetType()} is not supported by {nameof(OverwriteOrCreateFile)} method!");
                
                outputFile.Close();
            }
        }

        /// <summary>
        /// Removes one empty string between each string of len greater than 0 (if exist) (leftovers from code-to-List processing on win) 
        /// <br></br>["xx","","yy","","","zz"] -> ["xx","yy","","zz"]<br></br>
        /// </summary>
        /// <param name="lines">List to process</param>
        /// <returns>New instance of wihout newline-split empty strings.</returns>
        public static List<string> RemoveExcessiveEmptyStrings(List<string> lines) {
            var everyOtherElement = lines.Where((x, i) => i % 2 == 1);
            if (everyOtherElement.All(item => item.Length == 0) == false) return lines;

            List<string> newlines = new List<string>();
            bool wasNotEmpty = false;
            foreach (var line in lines) {
                if (wasNotEmpty || line.Length == 0) {
                    wasNotEmpty = false;
                    continue;
                }
                wasNotEmpty = true;
                newlines.Add(line);
            }
            return newlines;
        }
        /// <summary>
        /// Calls <see cref="PointFileAndGetPath(string, string, out string)"/> with <paramref name="dialogFilter"/> and <paramref name="filepath"/> 
        /// and retreives content of that file into <paramref name="fileContent"/> output variable.
        /// </summary>
        /// <param name="dialogFilter"><see cref="SaveFileDialog"/> filter string.</param>
        /// <param name="lastfile">Path to last pointed file. If null, current working directory used.</param>
        /// <param name="filepath">Output parameter which will store path to pointed file or <see cref="String.Empty"/> if no file selected.</param>
        /// <param name="fileContent">Output variable for read file content.</param>
        /// <returns>true if file was read properly, false otherwise.</returns>
        /// <exception cref="FileHandlerException"></exception>
        public static bool PointFileAndGetText(string dialogFilter, string lastfile, out string filepath, out string fileContent) {
            fileContent = string.Empty;
            if(PointFileAndGetPath(dialogFilter, ValidDirOrCurrent(lastfile), out filepath)) {
                try { fileContent = GetFileText(filepath); } 
                catch (FileHandlerException) { return false; } 
                catch (Exception ex) {throw new FileHandlerException("Unexpected Error while reading " + filepath + " file: " + ex.Message);} 
                return true;
            }
            return false;
        }
        /// <summary>Reads whole contents of file under given <paramref name="filepath"/>/</summary>
        /// <param name="filepath"></param>
        /// <returns>Content read by <see cref="File.ReadAllText(string, Encoding)"/></returns>
        /// <exception cref="FileHandlerException"></exception>
        private static string GetFileText(string filepath) {
            if (File.Exists(filepath) == false) throw new FileHandlerException("Cannot load instruction file " + filepath + ". File not exist.");
            Encoding encoding = GetEncoding(filepath, Encoding.Default);
            return File.ReadAllText(filepath, encoding);
        }




        /// <summary>
        /// Determines a text file's encoding by analyzing its byte order mark (BOM).<br></br>
        /// Defaults set by "defaultEncoding" param when detection of the text file's endianness fails.
        /// <br></br> Supprots:
        /// <br></br>- <see cref="Encoding.UTF7"/>
        /// <br></br>- <see cref="Encoding.UTF8"/>
        /// <br></br>- <see cref="Encoding.UTF32"/>
        /// <br></br>- <see cref="Encoding.Unicode"/>
        /// <br></br>- <see cref="Encoding.BigEndianUnicode"/>
        /// <br></br>- <see cref="UTF32Encoding(bool, bool)"/>
        /// </summary>
        /// <param name="filename">Path pointing to file which encoding should be get.</param>
        /// <param name="defaultEncoding">Encoding that should be assumed if file is saved with none of known encodings.</param>
        /// <returns>Detected <see cref="Encoding"/> object.</returns>
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
