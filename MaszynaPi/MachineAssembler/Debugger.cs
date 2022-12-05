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
            content = content.ToLower();
            var codelines = RemoveExcessiveEmptyStrings(CodeLinesHandle);
            MaszynaPi.Logger.Logger.LogInfo("Content " + content);
            MaszynaPi.Logger.Logger.LogInfo("findLineNumber codelines: " + string.Join(", ", codelines));
            MaszynaPi.Logger.Logger.LogInfo(" findLineNumber CodeLinesHandle: " + string.Join(", ", CodeLinesHandle));
            for (uint i = start; i < codelines.Count; i++)
                if (codelines[(int)i].ToLower().Contains(content))
                    return (int)i;
            return -1;
        }

        // To be called after compilation
        public void FillMemoryLineNumberMap() {
            ClearMemoryEditorMap();
            MaszynaPi.Logger.Logger.LogInfo("Memory Editor Map: "+ string.Join(", ",Compiler.GetMemoryEditorMap()));
            foreach (var pair in Compiler.GetMemoryEditorMap())
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
        
        
        public Action<int, string> OnSetExecutedLine;
        public Action<uint, List<string>> OnSetExecutedMicroinstructions;

        public void SetExecutedLine(uint memAddress) {

            MaszynaPi.Logger.Logger.LogInfo("SetExecutedLine()");

            if (MemoryLineNumberMap.Count == 0) return;
            if (MemoryLineNumberMap.ContainsKey(memAddress) == false) return;
            if (MemoryLineNumberMap[memAddress] == -1) return;


            int position = GetFirstCharIndexFromLine(MemoryLineNumberMap[memAddress]);
            //int lineEnd = CodeLinesToString().IndexOf(Environment.NewLine, position);
            //if (lineEnd < 0) lineEnd = GetCodeLength();
            MaszynaPi.Logger.Logger.LogInfo("MemoryLineNumberMap: " + string.Join(", ", MemoryLineNumberMap));

            if (position < 0) return;

            OnSetExecutedLine(position, Compiler.GetMemoryEditorMap()[memAddress]);
        }


        public void SetExecutedMicronstructions(uint opcode, List<string> activeSignals) {
            OnSetExecutedMicroinstructions(opcode, activeSignals);
        }

    }
}
