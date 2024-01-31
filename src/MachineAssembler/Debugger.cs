using MaszynaPi.FilesHandling;
using System;
using System.Collections.Generic;

namespace MaszynaPi.MachineAssembler
{
    /// <summary>
    /// Class allows for mapping current machine state to specific lines of assembly code and microinstructions.
    /// </summary>
    public class Debugger {

        /// <summary>
        /// Dictionary of pairs (memory_address - number of code line in editor)  
        /// </summary>
        static readonly Dictionary<uint, int> MemoryLineNumberMap = new Dictionary<uint, int>();
        /// <summary>
        /// List of all lines of code from editor.
        /// </summary>
        List<string> CodeLinesHandle;

        /// <summary>
        /// Action that should be performed when <see cref="Debugger"/> calculates current line. 
        /// Assembly Code string index is passed and contents of line that is execuded.
        /// </summary>
        public Action<int, string> OnSetExecutedLine;

        /// <summary>
        /// Action that should be performed when <see cref="Debugger"/> calculates current instruction's micrinstruction signals. 
        /// Instruction opcode and list of active microinstructions signals names are passed.
        /// </summary>
        public Action<uint, List<string>> OnSetExecutedMicroinstructions;

        /// <summary> Creates <see cref="Debugger"/> instance. </summary>
        public Debugger() {}
        
        /// <summary>Clears <see cref="MemoryLineNumberMap"/> dictionary.</summary>
        void ClearMemoryEditorMap() { MemoryLineNumberMap.Clear(); }
        /// <summary>
        /// Assigns instance of List of code lines into internal <see cref="CodeLinesHandle"/>.
        /// </summary>
        /// <param name="handle">List object containing current assembly code.</param>
        public void SetCodeEditorHandle(List<string> handle) {
            CodeLinesHandle = handle;
        }

        /// <summary>
        /// Gets 0 based index of nearest line (forward from 'start' praram) which contains specific string (invariant characers case)
        /// in <see cref="CodeLinesHandle"/> list.
        /// </summary>
        /// <param name="start">Number of line from which search should be started.</param>
        /// <param name="content">Substring to be found in code lines.</param>
        /// <returns>0 based number of found line, -1 if not found.</returns>
        public int FindLineNumber(uint start, string content) {
            content = content.ToLower();
            var codelines = FilesHandler.RemoveExcessiveEmptyStrings(CodeLinesHandle);
            for (uint i = start; i < codelines.Count; i++)
                if (codelines[(int)i].ToLower().Contains(content))
                    return (int)i;
            return -1;
        }

        /// <summary>
        /// Fills <see cref="MemoryLineNumberMap"/> with memory word adress corresponding to number of code line.
        /// To be called after compilation. 
        /// </summary>
        public void FillMemoryLineNumberMap() {
            ClearMemoryEditorMap();
            foreach (var pair in Assembler.GetMemoryEditorMap())
                MemoryLineNumberMap.Add(pair.Key, FindLineNumber(pair.Key, pair.Value));
        }

        /// <summary>
        /// Calculates index (where 0 is first characted of first element in <see cref="CodeLinesHandle"/>) 
        /// of first character in given line.
        /// </summary>
        /// <param name="lineindex">Index of line for which total leading characters number will be calculated.</param>
        /// <returns>Number of total leading characters before first character of given <see cref="CodeLinesHandle"/>[<paramref name="lineindex"/>].</returns>
        /// <exception cref="Exception"></exception>
        int GetFirstCharIndexFromLine(int lineindex) {
            var codelines = FilesHandler.RemoveExcessiveEmptyStrings(CodeLinesHandle);
            if (lineindex >= codelines.Count) throw new Exception("Error in 'GetFirstCharIndexFromLine' - param lineindex out of CodeLines bounds.");
            int chars = 0;
            for(int i=0; i<lineindex; i++) {
                chars += codelines[i].Length;
            }
            return chars;
        }

        /// <summary>
        /// Calls assigned <see cref="OnSetExecutedLine"/> <see cref="Action"/> for passed executed <paramref name="memAddress"/> if 
        /// <see cref="MemoryLineNumberMap"/> contais this address data and any code exist in assigned to<see cref="CodeLinesHandle"/> list. 
        /// </summary>
        /// <param name="memAddress">In-Memory Address of currently executed instruction.</param>
        public void SetExecutedLine(uint memAddress) {
            if (CodeLinesHandle.Count == 0) return;
            //if (MemoryLineNumberMap.Count == 0) return;
            if (MemoryLineNumberMap.ContainsKey(memAddress) == false) return;
            if (MemoryLineNumberMap[memAddress] == -1) return;

            int position = GetFirstCharIndexFromLine(MemoryLineNumberMap[memAddress]);
            //int lineEnd = CodeLinesToString().IndexOf(Environment.NewLine, position);
            //if (lineEnd < 0) lineEnd = GetCodeLength();
            if (position < 0) return;

            OnSetExecutedLine(position, Assembler.GetMemoryEditorMap()[memAddress]);
        }

        /// <summary>
        /// Calls <see cref="Action"/> assigned to <see cref="OnSetExecutedMicroinstructions"/> with passed in argument <paramref name="opcode"/>
        /// and list of <paramref name="activeSignals"/>.
        /// </summary>
        /// <param name="opcode">Opcode of currently execuded instruction.</param>
        /// <param name="activeSignals">List containing names of microinstructions active on current instruction.</param>
        public void SetExecutedMicronstructions(uint opcode, List<string> activeSignals) {
            OnSetExecutedMicroinstructions(opcode, activeSignals);
        }

    }
}
