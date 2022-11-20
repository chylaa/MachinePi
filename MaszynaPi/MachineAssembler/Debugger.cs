using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaszynaPi.MachineAssembler {
   public class Debugger {

        static Dictionary<uint, int> MemoryEditorMap = new Dictionary<uint, int>();
        TextBox CodeEditorHandle; //TODO: Wywalić to stąd, (CodeEditor będzie rozdzielony na Logic/UI) i zrobić obsługę za pomocą Action<> i Func<> (OnSmth)

        public Debugger(TextBox codeEditorHandle) {
            SetCodeEditorHandle(codeEditorHandle);
        }

        static public void ClearMemoryEditorMap() { MemoryEditorMap.Clear(); }

        static public void AddToMemoryEditorMap(uint memAddress, string line) {
            MemoryEditorMap.Add(memAddress, Editors.CodeEditor.FindLineNumber(memAddress, line));
        }

        public void SetCodeEditorHandle(TextBox handle) {
            CodeEditorHandle = handle;
        }

        public void SetExecutedLine( uint memAddress ) {
            if (MemoryEditorMap.Count == 0) return;
            if (MemoryEditorMap.ContainsKey(memAddress) == false) return;
            if (MemoryEditorMap[memAddress] == -1) return;

            int position = CodeEditorHandle.GetFirstCharIndexFromLine(MemoryEditorMap[memAddress]);
            int lineEnd = CodeEditorHandle.Text.IndexOf(Environment.NewLine, position);
            if (lineEnd < 0) lineEnd = CodeEditorHandle.Text.Length;
            if (position < 0) return;
            CodeEditorHandle.Select(position, lineEnd - position);
            CodeEditorHandle.Focus();
        }
            
    }
}
