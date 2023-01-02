using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaszynaPi.FilesHandling;

namespace MaszynaPi.MachineAssembler {
   public class Debugger {

        // Dictionary of pairs memory_address - number of code line in editor 
        static Dictionary<uint, int> MemoryLineNumberMap = new Dictionary<uint, int>();
        List<string> CodeLinesHandle;
        List<string> InstructionLinesHandle;

        public Debugger() {
        }

        void ClearMemoryEditorMap() { MemoryLineNumberMap.Clear(); }

        public void SetCodeEditorHandle(List<string> handle) {
            CodeLinesHandle = handle;
        }
        public void SetInstructionListHandle(List<string> handle) {
            InstructionLinesHandle = handle;
        }

        string CodeLinesToString() { return string.Join(Environment.NewLine, CodeLinesHandle); }


        // Gets line index of nearest line (forward from 'start' praram) which contains specific string
        public int FindLineNumber(uint start, string content) {
            content = content.ToLower();
            var codelines = FilesHandler.RemoveExcessiveEmptyStrings(CodeLinesHandle);
            for (uint i = start; i < codelines.Count; i++)
                if (codelines[(int)i].ToLower().Contains(content))
                    return (int)i;
            return -1;
        }

        // To be called after compilation
        public void FillMemoryLineNumberMap() {
            ClearMemoryEditorMap();
            foreach (var pair in Compiler.GetMemoryEditorMap())
                MemoryLineNumberMap.Add(pair.Key, FindLineNumber(pair.Key, pair.Value));
        }

        int GetFirstCharIndexFromLine(int lineindex) {
            var codelines = FilesHandler.RemoveExcessiveEmptyStrings(CodeLinesHandle);
            if (lineindex >= codelines.Count) throw new Exception("Error in 'GetFirstCharIndexFromLine' - param lineindex out of CodeLines bounds.");
            int chars = 0;
            for(int i=0; i<lineindex; i++) {
                chars += codelines[i].Length;
            }
            return chars;
        }

        int GetCodeLength() {
            return CodeLinesHandle.Sum(s => s.Length);
        }
        
        
        public Action<int, string> OnSetExecutedLine;
        public Action<uint, List<string>> OnSetExecutedMicroinstructions;

        public void SetExecutedLine(uint memAddress) {
            if (MemoryLineNumberMap.Count == 0) return;
            if (MemoryLineNumberMap.ContainsKey(memAddress) == false) return;
            if (MemoryLineNumberMap[memAddress] == -1) return;

            int position = GetFirstCharIndexFromLine(MemoryLineNumberMap[memAddress]);
            //int lineEnd = CodeLinesToString().IndexOf(Environment.NewLine, position);
            //if (lineEnd < 0) lineEnd = GetCodeLength();
            if (position < 0) return;

            OnSetExecutedLine(position, Compiler.GetMemoryEditorMap()[memAddress]);
        }


        public void SetExecutedMicronstructions(uint opcode, List<string> activeSignals) {
            OnSetExecutedMicroinstructions(opcode, activeSignals);
        }

    }
}
