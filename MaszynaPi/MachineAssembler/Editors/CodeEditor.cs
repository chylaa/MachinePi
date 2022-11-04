using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaszynaPi.MachineAssembler.Editors{
    class CodeEditor {
        const string COMMENT = "//";
        public static List<string> CodeLines;

        static void DeleteComments(List<string> codeLines) {
            for (int i = 0; i < codeLines.Count; i++)
                if (codeLines[i].Contains(COMMENT))
                    codeLines[i].Remove(codeLines[i].IndexOf(COMMENT));
        }
        public static List<string> FormatCodeForCompiler() {
            List<string> codeLines = new List<string>(CodeLines);
            codeLines.RemoveAll(string.IsNullOrWhiteSpace); // for peace of mind
            //codeLines = codeLines.ConvertAll(d => d.ToLower());
            DeleteComments(codeLines);
            return codeLines;
        }
    }
}
