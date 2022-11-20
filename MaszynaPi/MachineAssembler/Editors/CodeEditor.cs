using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.FilesHandling;
using System.Windows.Forms;
using System.IO;
namespace MaszynaPi.MachineAssembler.Editors{
    class CodeEditor {
        const string COMMENT = "//";

        public static List<string> CodeLines;
        public TextBox CodeEditorHandle;

        public CodeEditor(TextBox editorHandle) {
            SetCodeEditorHandle(editorHandle);
        }

        public void SetCodeEditorHandle(TextBox editorHandle) {
            CodeEditorHandle = editorHandle;
        }

        public void SetCodeEditorViewContent(string program) {
            CodeEditorHandle.Text = program;
        }

        public void SetCodeLinesFromEditorContent() {
            CodeLines = CodeEditorHandle.Text.Split(Environment.NewLine.ToCharArray()).ToList();
        }

        public static List<string> GetCodeLines() {
            return CodeLines;
        }

        public static  List<string> GetCodeLinesCopy() {
            return new List<string>(CodeLines);
        }

        private string DeleteComment(string line) {
            if (line.Contains(COMMENT))
                return line.Remove(line.IndexOf(COMMENT));
            return line;
        }

        private void ProcessCode(List<string> codeLines) {
            for (int i = 0; i < codeLines.Count; i++) {
                codeLines[i] = DeleteComment(codeLines[i]);
                codeLines[i] = codeLines[i].Trim();
                codeLines[i] = codeLines[i].ToLower();
            }
        }

        public List<string> FormatCodeForCompiler() {
            List<string> codeLines = new List<string>(CodeLines);
            codeLines.RemoveAll(string.IsNullOrWhiteSpace); // for peace of mind
            ProcessCode(codeLines);
            return codeLines;
        }

        // Removes one empty string between each string of len>0 (if exist) (leftovers from code-to-List<string> processing) 
        // ["xx","","yy","","","zz"] -> ["xx","yy","","zz"]
        public static List<string> RemoveExcessiveEmptyStrings(List<string> codelines) {
            List<string> newlines = new List<string>();
            bool wasNotEmpty = false;
            foreach (var line in codelines) {
                if (wasNotEmpty || line.Length == 0) {
                    wasNotEmpty = false;
                    continue;
                }
                wasNotEmpty = true;
                newlines.Add(line);
            }
            return newlines;
        }

        // Gets line index of nearest line (forward from 'start' praram) which contains specific string
        public static int FindLineNumber(uint start, string content) {
            var codelines = RemoveExcessiveEmptyStrings(CodeLines);
            for (uint i = start; i < codelines.Count; i++)
                if (codelines[(int)i].Contains(content))
                    return (int)i;
            return -1;
        }

        
    }
}
