using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace MaszynaPi.MachineAssembler.Editors
{
    class CodeEditor
    {
        /// <summary>Allows to precise if edited code is <see cref="Instruction"/> or <see cref="Program"/></summary>
        public enum Definition { Unknown, Instruction, Program };

        public List<string> CodeLines; //Orginal Handle (for code editor view and debugger)

        private static readonly string[] InstructionPatterns = new string[] {
            ($"^{Defines.INSTRUCTION_NAME_HEADER}"),                                                // Matches instruction name keyword
            (string.Join(@"\s+", Defines.FETCH_SIGNALS) + Defines.LINE_END),                        // Matches occurence of fetch pattern
            ($"{Defines.SIGNAL_LABEL_PREFIX}[a-z]+"),                                               // Matches zero or more occurrences of '@' followed by one word.
            (Defines.SIGNAL_STATEMENT_IF + @"\s[a-z]+\s" + Defines.SIGNAL_STATEMENT_THEN + @"\s"),  // Matches instruction IF THEN pattern
        };

        public CodeEditor() {
            CodeLines = new List<string>();
        }

        public List<string> GetCodeLinesHandle() {
            return CodeLines;
        }

        public  List<string> GetCodeLinesCopy() {
            return new List<string>(CodeLines);
        }

        public string JoinToLowerString(IEnumerable<string> values) { return string.Join(Environment.NewLine, values).ToLower(); }

        /// <summary>
        /// Returns type of code that <see cref="CodeLines"/> contains. Each occurence of specific <see cref="Definition"/>
        /// keyword gives it a point. If equal amout of points is given, <paramref name="draw"/> value is assumed.
        /// Before analyzing, copy of <see cref="CodeLines"/> is made and all standarization processing are made on it.
        /// Method does not affect content of original list. 
        /// <br></br>Note: if both probabilites points are equal to 0, <see cref="Definition.Unknown"/> is returned.
        /// </summary>
        /// <param name="draw">Value to be returned if probablity of each code type is the same (except if it is 0).</param>
        /// <returns>Assumed <see cref="Definition"/> type of code from <see cref="CodeLines"/>.</returns>
        public Definition DetectCodeType(Definition draw = Definition.Program)
        {
            try
            {
                List<string> copy = GetCodeLinesCopy();
                ProcessCode(copy);
                string code = JoinToLowerString(copy);
                int pp = ProgramPoints(code);
                int ip = InstructionPoints(code, copy.Count);

                if (pp == ip && pp + ip > 0) return draw; // if equal and both not 0
                if (pp > ip) return Definition.Program;
                if (pp < ip) return Definition.Instruction;
                return Definition.Unknown;
            } 
            catch
            {
                return Definition.Unknown;
            }
        }
        private int ProgramPoints(string code)
        {
            RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Multiline;
            int points = 0;

            points += new Regex(Defines.KEYWORD_CONST_VAR, options).Matches(code).Count;
            points += new Regex(Defines.KEYWORD_MEM_ALLOC, options).Matches(code).Count;
            points += InstructionLoader.GetAvaibleInstructionsNames().Count(inst => code.Contains(inst));
            return points;
        }

        private int InstructionPoints(string code, int codelines)
        {
            RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Multiline;
            int points = 0;

            string regexEOL = ($"{Defines.LINE_END}$");  // Matches ';' line end symbol with newline
            if (codelines > 1 && new Regex(regexEOL, options).Matches(code).Count == (codelines - 1) )
                points += 1; // one point for all valid line ends

            foreach (string pattern in InstructionPatterns)
                points += new Regex(pattern, options).Matches(code).Count;
            return points;
        }

        private string DeleteComment(string line) {
            if (line.Contains(Defines.COMMENT))
                return line.Remove(line.IndexOf(Defines.COMMENT));
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
            ProcessCode(codeLines);                         // must be before removing!
            codeLines.RemoveAll(string.IsNullOrWhiteSpace); // for peace of mind
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
