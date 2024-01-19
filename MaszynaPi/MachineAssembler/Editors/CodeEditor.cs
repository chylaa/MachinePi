using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.FilesHandling;
using System.Windows.Forms;
using MaszynaPi.MachineLogic;
using System.IO;
namespace MaszynaPi.MachineAssembler.Editors{
    class CodeEditor {              
        const string COMMENT = "//";

        public List<string> CodeLines; //Orginal Handle (for code editor view and debugger)

        public CodeEditor() {
            CodeLines = new List<string>();
        }

        public List<string> GetCodeLinesHandle() {
            return CodeLines;
        }

        public  List<string> GetCodeLinesCopy() {
            return new List<string>(CodeLines);
        }

        public string CodeLinesToString() { return string.Join(Environment.NewLine, CodeLines).ToLower(); }

        public bool IsInstructionDefinition() {
            string text = CodeLinesToString();
            return (text.IndexOf(Defines.INSTRUCTION_NAME_HEADER, StringComparison.InvariantCultureIgnoreCase) >= 0); //Contains (ignorecase)
           // return (Defines.FETCH_SIGNALS.Any(signal => text.ToLower().Contains(signal)) ||
           //        InstructionLoader.UPPER_WORDS.Any(signal => text.ToLower().Contains(signal)));
        }
        public bool IsProgram() {
            string text = CodeLinesToString();
            return (text.Contains(Defines.KEYWORD_CONST_VAR) || text.Contains(Defines.KEYWORD_MEM_ALLOC)
                    || InstructionLoader.GetAvaibleInstructionsNames().Any(inst => text.Contains(inst)));
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

        public List<string> FormatMicroinstructionsCode() {
            List<string> codeLines = new List<string>(CodeLines);
            codeLines.RemoveAll(string.IsNullOrWhiteSpace); // for peace of mind
            for (int i = 0; i < codeLines.Count; i++) codeLines[i] = codeLines[i].ToLower();
            return codeLines;
        }


        
    }
}
