using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.MachineAssembler.FilesHandling;
namespace MaszynaPi.MachineAssembler.Editors{
    class CodeEditor {
        const string COMMENT = "//";
        public static List<string> CodeLines { get; set; }


        static string DeleteComment(string line) {
            if (line.Contains(COMMENT))
                return line.Remove(line.IndexOf(COMMENT));
            return line;
        }

        static void ProcessCode(List<string> codeLines) {
            for (int i = 0; i < codeLines.Count; i++) {
                codeLines[i] = DeleteComment(codeLines[i]);
                codeLines[i] = codeLines[i].Trim();
                codeLines[i] = codeLines[i].ToLower();
            }
        }

        public static List<string> FormatCodeForCompiler() {
            List<string> codeLines = new List<string>(CodeLines);
            codeLines.RemoveAll(string.IsNullOrWhiteSpace); // for peace of mind
            ProcessCode(codeLines);
            return codeLines;
        }
    }
}
