﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaszynaPi.MachineAssembler {
   public class Debugger {

        // Dictionary of pairs memory_address - number of code line in editor 
        static Dictionary<uint, int> MemoryLineNumberMap = new Dictionary<uint, int>();
        List<string> CodeLinesHandle;

        public Debugger() {
        }

        static public void ClearMemoryEditorMap() { MemoryLineNumberMap.Clear(); }

        public void SetCodeEditorHandle(List<string> handle) {
            CodeLinesHandle = handle;
        }

        string CodeLinesToString() { return string.Join(Environment.NewLine, CodeLinesHandle); }

        // Removes one empty string between each string of len>0 (if exist) (leftovers from code-to-List<string> processing) 
        // ["xx","","yy","","","zz"] -> ["xx","yy","","zz"]
        List<string> RemoveExcessiveEmptyStrings(List<string> codelines) {
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
        public int FindLineNumber(uint start, string content) {
            var codelines = RemoveExcessiveEmptyStrings(CodeLinesHandle);
            for (uint i = start; i < codelines.Count; i++)
                if (codelines[(int)i].Contains(content))
                    return (int)i;
            return -1;
        }

        // To be called after compilation
        public void FillMemoryLineNumberMap() {
            foreach(var pair in Compiler.GetMemoryEditorMap())
                MemoryLineNumberMap.Add(pair.Key, FindLineNumber(pair.Key, pair.Value));
        }

        int GetFirstCharIndexFromLine(int lineindex) {
            if (lineindex >= CodeLinesHandle.Count) throw new Exception("Error in 'GetFirstCharIndexFromLine' - param lineindex out of CodeLines bounds.");
            int chars = 0;
            for(int i=0; i<lineindex; i++) {
                chars += CodeLinesHandle[i].Length;
            }
            return chars;
        }

        int GetCodeLength() {
            return CodeLinesHandle.Sum(s => s.Length);
        }
        
        
        public Action<int, int> OnSetExecutedLine;

        public void SetExecutedLine(uint memAddress) {
            if (MemoryLineNumberMap.Count == 0) return;
            if (MemoryLineNumberMap.ContainsKey(memAddress) == false) return;
            if (MemoryLineNumberMap[memAddress] == -1) return;

            int position = GetFirstCharIndexFromLine(MemoryLineNumberMap[memAddress]);
            int lineEnd = CodeLinesToString().IndexOf(Environment.NewLine, position);
            if (lineEnd < 0) lineEnd = GetCodeLength();
            if (position < 0) return;

            OnSetExecutedLine(position, lineEnd);
        }


    }
}