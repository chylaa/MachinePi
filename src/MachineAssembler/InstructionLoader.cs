using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using MaszynaPi.MachineLogic;
using System.Text.RegularExpressions;

using static MaszynaPi.Defines;

namespace MaszynaPi.MachineAssembler {

    /// <summary>
    /// Generic <see cref="Exception"/> overload, for throwing errors related to <see cref="InstructionLoader"/> class.
    /// </summary>
    class InstructionLoaderException : Exception { public InstructionLoaderException(string message) : base(message) { } }

    /// <summary>
    /// Class providing methods for parsing Instruction files, written in .ini-like format.
    /// Syntax of instruction set files was preserved to assure backward compability with previous simulator application.
    /// </summary>
    static class InstructionLoader {

        const byte OPTIONS_LINES = 12;
        const string NO_ARGUMENT = "0";
        const string COMPONENT_ON = "1";
        const char LINE_HEADER_SEPARATOR = '=';
        
        static int MAX_OPCODE = -1; // init with no value

        /// <summary> List of keywords for microinstruction definition pseoudo-language </summary>
        public readonly static List<string> uInstDefKeywords = new List<string> { INSTRUCTION_NAME_HEADER.Replace(" ",""), SIGNAL_STATEMENT_IF, SIGNAL_STATEMENT_ELSE.Split(' ')[0], SIGNAL_STATEMENT_ELSE.Split(' ')[1],
                                                                        SIGNAL_STATEMENT_THEN, SIGNAL_STATEMENT_END, ALU_FLAG_INT, ALU_FLAG_Z, ALU_FLAG_V, ALU_FLAG_ZAK};

        /// <summary> Mapping of instruction names to their uOps definitions (lines from file, used for <see cref="MachineUI.UserControlInstructionMicrocode"/>)</summary>
        static Dictionary<string, List<string>> InstructionsLines = new Dictionary<string, List<string>>(); 

        /// <summary> Mapping Instructions names to their opcodes (as unsigned integers). </summary>
        static Dictionary<string,uint> InstructionsNamesOpcodes = new Dictionary<string,uint>(); 
        /// <summary> Mapping Instructions names to list of lists of their's uOps signals (each list of uops defines set of them than can be executed in single clock cycle)</summary>
        static Dictionary<uint, List<List<string>>> InstructionsSignalsMap = new Dictionary<uint, List<List<string>>>();
        /// <summary> Names of instructions that takes no arguments.</summary>
        static List<string> ZeroArgInstructions = new List<string>();

        /// <summary>
        /// Creates list of names of all avaible (loaded/declared) instructions 
        /// (NOT considering the currently set code bits value - returned instruction count might be larger than maximum that can be encoded
        /// with currently set opcode bit size).
        /// </summary>
        /// <returns>List of strings, where each element is name of one of all avaible instruction.</returns>
        public static List<string> GetAvaibleInstructionsNames() {
            return InstructionsNamesOpcodes.Keys.ToList();
        }
        /// <summary>
        /// Method loads base instructions set, using instructions defined in <see cref="Properties.Resources.Podstawa"/> or <see cref="Properties.Resources.Base"/>
        /// (dependins on currently set <see cref="LangInUse"/> language). Uses <see cref="LoadInstructionSet(List{string})"/> method.
        /// </summary>
        /// <returns>'true' if all instructions could be encoded using currently set size of opcode (<see cref="ArchitectureSettings.GetMaxOpcode"/>), false otherwise.</returns>
        /// <exception cref="InstructionLoaderException"></exception>
        public static bool LoadBaseInstructions() {
            var separator = Environment.NewLine.ToCharArray();
            string baseInstructions;
            try {
                    if (Environment.OSVersion.Platform == PlatformID.Unix) baseInstructions = File.ReadAllText("./" + BASE_INSTRUCTION_SET_FILENAME);
                else if (Environment.OSVersion.Platform == PlatformID.Win32NT) {
                    if (LangInUse == Lang.ENG) baseInstructions = Properties.Resources.Base;
                    else baseInstructions = Properties.Resources.Podstawa;
                } else throw new InstructionLoaderException("Unknown deploy OS: " + Environment.OSVersion.VersionString);
            
                 return LoadInstructionSet(baseInstructions.Split(separator).ToList());
            } catch (Exception ex) { throw new InstructionLoaderException("Loading Base instructions set "+INSTRUCTION_SET_FILE_EXTENSION+" file error: " + ex.Message);}
          }

        /// <summary>
        /// Loads new set of instructions basing on provided instruction file content (<paramref name="instructions"/>).
        /// Uses <see cref="LoadInstructionSet(List{string})"/> method.
        /// </summary>
        /// <param name="instructions">Contents of <see cref="INSTRUCTION_FILE_EXTENSION"/> instruction set file.</param>
        /// <returns>'true' if all instructions could be encoded using currently set size of opcode (<see cref="ArchitectureSettings.GetMaxOpcode"/>), false otherwise.</returns>
        /// <exception cref="InstructionLoaderException"></exception>
        public static bool LoadInstructionsFromFileContent(string instructions) {
            var separator = Environment.NewLine.ToCharArray();
            try { return LoadInstructionSet(instructions.Split(separator).ToList());
            } catch (Exception ex) { throw new InstructionLoaderException("Loading Instruction Set " + INSTRUCTION_SET_FILE_EXTENSION + " file error: " + ex.Message); }
        }

        /// <summary> Allows to load single instruction using passed file's content <paramref name="instructions"/> as insturction definition source. </summary>
        /// <param name="instructions">Contents of <see cref="INSTRUCTION_FILE_EXTENSION"/> file containing definition of single instruction.</param>
        /// <returns>'true' if new instruction can be encoded with currently set size of opcode, 'false' otherwise. </returns>
        /// <exception cref="InstructionLoaderException"></exception>
        public static bool LoadSingleInstructionFromFileContent(string instructions) {
            var separator = Environment.NewLine.ToCharArray();
            try {
                return LoadSingleInstruction(instructions.Split(separator).ToList());
            } catch (Exception ex) { throw new InstructionLoaderException("Loading Instruction " + INSTRUCTION_FILE_EXTENSION + " file error: " + ex.Message); }
        }

        //===========================================================================================================================================
        /// <returns> Mapping of instruction names to their uOps definitions (lines from file, used for <see cref="MachineUI.UserControlInstructionMicrocode"/>)</returns>
        public static Dictionary<string, List<string>> GetInstructionsLines() {
            if (InstructionsLines.Count() > 0) return InstructionsLines;
            throw new InstructionLoaderException("Trying get empty Instructions View Dictionary (LoadInstructionSetFile() method not called?).");
        }
        /// <returns> Mapping Instructions names to their opcodes (as unsigned integers). </returns>
        public static Dictionary<string,uint> GetInstructionsNamesOpcodes() {
            if (InstructionsNamesOpcodes.Count() > 0) return InstructionsNamesOpcodes;
            throw new InstructionLoaderException("Trying get empty Instructions View Dictionary (LoadInstructionSetFile() method not called?).");
        }
        /// <returns> Mapping Instructions names to list of lists of their's uOps signals (each list of uops defines set of them than can be executed in single clock cycle)</returns>
        public static Dictionary<uint, List<List<string>>> GetInstructionSignalsMap() {
            if (InstructionsSignalsMap.Count() > 0) return InstructionsSignalsMap;
            throw new InstructionLoaderException("Trying get empty Instructions View Dictionary (LoadInstructionSetFile() method not called?).");
        }
        /// <returns> Names of instructions that takes no arguments.</returns>
        public static List<string> GetZeroArgInstructions() { return ZeroArgInstructions; }

        //===========================================================================================================================================
        /// <summary>
        /// Sets related <see cref="ArchitectureSettings"/> fields (AddressSpace, CodeBits) according to <see cref="OPTIONS_HEADER"/> section content. 
        /// </summary>
        /// <param name="options">Part of <see cref="INSTRUCTION_SET_FILE_EXTENSION"/> file containing <see cref="OPTIONS_HEADER"/> section.</param>
        /// <exception cref="InstructionLoaderException"></exception>
        private static void SetOptions(List<string> options) { 
            bool asParse = int.TryParse(options.Find(el => el.Contains(ADDRESS_SPACE_HEADER)).Replace(ADDRESS_SPACE_HEADER, ""), out int addrSpace);
            bool cbParse = int.TryParse(options.Find(el => el.Contains(CODE_BITS_HEADER)).Replace(CODE_BITS_HEADER, ""), out int codeBits);
            if (!asParse) throw new InstructionLoaderException("Invalid value in "+OPTIONS_HEADER+" "+ADDRESS_SPACE_HEADER+". Must be numeric.");
            if (!cbParse) throw new InstructionLoaderException("Invalid value in " + OPTIONS_HEADER + " " + CODE_BITS_HEADER + ". Must be numeric.");
            ArchitectureSettings.SetAddressSpace((uint)addrSpace);
            ArchitectureSettings.SetCodeBits((uint)codeBits);
            options.Remove(ADDRESS_SPACE_HEADER+addrSpace.ToString()); 
            options.Remove(CODE_BITS_HEADER + codeBits.ToString());
            //--------------------------------------------------------------------------------------------------------------
            int componetsSet = (int)Components.Basic;
            for(int i=0; i<options.Count;i++) { if (options[i].EndsWith(COMPONENT_ON)) componetsSet |= (1<<i+(int)Components.Basic); }
            ArchitectureSettings.SetActiveComponents((Components)componetsSet);
        }

        /// <summary> Clears all lists containing loaded instruction set information; instruction definitions, opcoded, and uOps.</summary>
        private static void ClearLoadedInstructions() {
            InstructionsLines.Clear();
            InstructionsSignalsMap.Clear();
            InstructionsNamesOpcodes.Clear();
            ZeroArgInstructions.Clear();
        }


        /// <summary>Checks if instruction starts with valid set of fetch microoperations.</summary>
        /// <param name="uOpsLine">Single clock cycle set of microoperations from insturction definition.</param>
        /// <returns>'true' if passed instruction definition part contains valid fetch sequence (defined in <see cref="FETCH_SIGNALS"/>).</returns>
        private static bool IsValidStartOfInstruction(List<string> uOpsLine) {
            return FETCH_SIGNALS.Intersect(uOpsLine).Count() == FETCH_SIGNALS.Count();
        }

        /// <summary> Checks if conditional statement written in instruction definition language has valid syntax. </summary>
        /// <param name="sigline">List of statements from single line of instruction definition.</param>
        /// <returns>'true' if it is not a conditional statement or statemets passed in parameter are in valid order (if [arg] then @label else @label || if [arg] then @label) </returns>
        private static bool IsStatementValid(List<string> sigline) {
            var sigcopy = new List<string>(sigline);
            if (sigcopy[0].StartsWith(SIGNAL_LABEL_PREFIX.ToString())) sigcopy.RemoveAt(0);
            if (sigcopy.All(sig => false==sig.Contains(SIGNAL_LABEL_PREFIX))) return true;

            string line = string.Join(" ",sigcopy);
            string ifRegex = "^[a-z ]*"+SIGNAL_STATEMENT_IF+" [a-z]+ "+SIGNAL_STATEMENT_THEN+" @[a-z]+$";
            string ifelseRegex = ifRegex.Replace("$"," ")+SIGNAL_STATEMENT_ELSE+" @[a-z]+$";
            if (Regex.IsMatch(line, ifRegex) || Regex.IsMatch(line, ifelseRegex)) return true;
            return false;
        }

        /// <summary>Removes all null or white space elements from <paramref name="lines"/> and converts all strings to lowercase. </summary>
        /// <param name="lines">Instruction definition text, splitted into lines.</param>
        /// <returns>New standarized List instance.</returns>
        private static List<string> StandarizeLines(List<string> lines) {
            lines.RemoveAll(string.IsNullOrWhiteSpace);
            return lines.ConvertAll(d => d.ToLower());
        }

        /// <summary>
        /// Changes capitalization specific words defined in <see cref="uInstDefKeywords"/> to upper (if <paramref name="toUpper"/> == true) or lower.
        /// <br></br>Used mainly for displaing avaible instructions definitions in <see cref="MachineUI.UserControlInstructionMicrocode"/>.
        /// </summary>
        /// <param name="toUpper">
        /// Set to 'true' (default) to capitalize instruction definition keywords using <see cref="string.ToUpper()"/> method,
        /// 'false' to perform <see cref="string.ToLower()"/>.
        /// </param>
        private static void ChangeCapitalizationOfInstructionDefinitionKeywords(bool toUpper = true) {
            var refractoredLines = new Dictionary<string, List<string>>();
            List<string> toReplace;
            foreach (string name in InstructionsLines.Keys) {
                refractoredLines.Add(name, new List<string>());

                foreach (string item in InstructionsLines[name]) {
                    toReplace = uInstDefKeywords.Intersect(item.Split(' ')).ToList();
                    string line = item;
                    if (line.StartsWith(COMMENT)) { refractoredLines[name].Add(line); continue; }

                    foreach (string word in toReplace) {
                        string toreplace = word + ' '; //" " + word + " ";
                        line = line.Replace(toreplace, (toUpper ? toreplace.ToUpper() : toreplace.ToLower())); 
                    }

                    refractoredLines[name].Add(line);    
                }
            }
            InstructionsLines.Clear();
            InstructionsLines = new Dictionary<string, List<string>>(refractoredLines);
        }

        /// <summary>
        /// Parses passed in <paramref name="lines"/> content of <see cref="INSTRUCTION_SET_FILE_EXTENSION"/> file, and fills specific instruction mappings/lists acordingly.
        /// <br></br>Creates new instances for:
        /// <br></br> - <see cref="InstructionsLines"/>
        /// <br></br> - <see cref="InstructionsSignalsMap"/>
        /// <br></br> - <see cref="InstructionsNamesOpcodes"/>
        /// <br></br> - <see cref="ZeroArgInstructions"/>
        /// <br></br>
        /// Throws <see cref="InstructionLoaderException"/> if parsing fails.
        /// </summary>
        /// <param name="lines">Contents of <see cref="INSTRUCTION_SET_FILE_EXTENSION"/> file in form of list of its lines (splitted by new line symbol(s) content of .lst file)</param>
        /// <returns>'true' if all instructions could be encoded using currently set size of opcode (<see cref="ArchitectureSettings.GetMaxOpcode"/>), false otherwise.</returns>
        /// <exception cref="InstructionLoaderException"></exception>
        public static bool LoadInstructionSet(List<string> lines) {
            lines = StandarizeLines(lines);
            List<string> options = lines.GetRange(lines.IndexOf(OPTIONS_HEADER)+1, (OPTIONS_LINES-lines.IndexOf(OPTIONS_HEADER)) );
            List<string> instructios = lines.GetRange(lines.IndexOf(INSTRUCTIONS_HEADER)+1, lines.Count-(lines.IndexOf(INSTRUCTIONS_HEADER)+1) );

            MAX_OPCODE = -1;

            var instructionsLines = new Dictionary<string, List<string>>();//new Dictionary<string, List<string>>(InstructionsLines);
            var instructionNamesOpcodes = new Dictionary<string, uint>();//new Dictionary<string, uint>(InstructionsNamesOpcodes);
            var instructionSignalsMap = new Dictionary<uint, List<List<string>>>();//new Dictionary<uint, List<List<string>>>(InstructionsSignalsMap);
            var zeroArgInstructions = new List<string>();//new List<string>(ZeroArgInstructions); 

            if (!instructios[0].Contains(INSTRUCTION_NUMBER_HEADER)) {
                throw new InstructionLoaderException("Invalid format of .lst file -> unknown symbol on this position '" + instructios[0] + "'");
            }
            int instNum = int.Parse(instructios[0].Replace(INSTRUCTION_NUMBER_HEADER,""));
            
            for(int i=1; i <= instNum; i++) { // if not insruction[i].Conteins()'=' throw . . .
                MAX_OPCODE += 1;
                instructionNamesOpcodes.Add(instructios[i].Split(LINE_HEADER_SEPARATOR)[1], (uint)MAX_OPCODE); // Get instruction name (always after '=' char)
                instructionSignalsMap.Add((uint)MAX_OPCODE, new List<List<string>>());
                instructionsLines.Add(instructios[i].Split(LINE_HEADER_SEPARATOR)[1], new List<string>());
            }
         
            instructios.RemoveRange(0, instNum+1);
            string processInstruction = string.Empty;
            bool czytwysweiil = false;
            foreach (string line in instructios) {
                if (line.StartsWith("[") && line.EndsWith("]")) { //is [instruction_name]
                    processInstruction = line.Replace("[", "").Replace("]", "");
                    czytwysweiil = true;
                    continue;
                }

                if (line.Contains(INSTRUCTION_LINES_HEADER)) continue; // is instruction line number count
                
                instructionsLines[processInstruction].Add(line.Substring(line.IndexOf(LINE_HEADER_SEPARATOR)+1));

                if (line.Contains(INSTRUCTION_ARGSNUM_HEADER) && line.Contains(NO_ARGUMENT)) { zeroArgInstructions.Add(processInstruction); }
                if (line.Contains(COMMENT) || line.Contains(INSTRUCTION_NAME_HEADER) || line.Contains(INSTRUCTION_ARGSNUM_HEADER)) { // is not signals lines
                    continue;
                }
                if (line.IndexOf(LINE_HEADER_SEPARATOR) == line.Length - 1) continue; //empty line

                List<string> signalsInLine = line.TrimEnd(LINE_END).Substring(line.IndexOf(LINE_HEADER_SEPARATOR) +1).Split(' ').ToList();

                if (IsStatementValid(signalsInLine) == false)
                    throw new InstructionLoaderException("Invalid define of instruction statement: " + string.Join(" ", signalsInLine) +"\nCheck instruction file encoding (ANSI might not be supported)");
                if (czytwysweiil) {
                    if (false == IsValidStartOfInstruction(signalsInLine)) 
                        throw new InstructionLoaderException("Critical error in defined instruction "+processInstruction+". Instruction does not start with fetch cycle. Say after me! "+string.Join(", ",FETCH_SIGNALS)+" [!]");
                    czytwysweiil = false;
                }
                instructionSignalsMap[instructionNamesOpcodes[processInstruction]].Add(signalsInLine);
            }

            ClearLoadedInstructions(); // not neccessary?
            InstructionsLines = new Dictionary<string, List<string>>(instructionsLines);
            InstructionsNamesOpcodes = new Dictionary<string, uint>(instructionNamesOpcodes);
            InstructionsSignalsMap = new Dictionary<uint, List<List<string>>>(instructionSignalsMap);
            ZeroArgInstructions = new List<string>(zeroArgInstructions);

            SetOptions(options);
            ChangeCapitalizationOfInstructionDefinitionKeywords(toUpper:true);

            if (MAX_OPCODE > ArchitectureSettings.GetMaxOpcode())
                return false;
            return true;

        }

        //===========================================================================================================================================

        /// <summary> If <paramref name="line"/> contains <see cref="COMMENT"/> substring, deletes all characters after that symbol (including it).</summary>
        /// <param name="line">Line that contiains potential valid comment.</param>
        /// <returns>New <see cref="string"/> instance wihout potential comment substring, or original <paramref name="line"/> if it did not contain any comment.</returns>
        static string DeleteComment(string line) {
            if (line.Contains(COMMENT))
                return line.Substring(0, line.IndexOf(COMMENT));
            return line;
        }

        /// <summary>
        /// Adds single instruction into existing instruction set. <br></br>
        /// Throws <see cref="InstructionLoaderException"/> if instruciton parsing failed.
        /// </summary>
        /// <param name="lines">List of lines containing definition of single instruction.</param>
        /// <returns>'true' if new instruction can be encoded with currently set size of opcode, 'false' otherwise. </returns>
        /// <exception cref="InstructionLoaderException"></exception>
        public static bool LoadSingleInstruction(List<string> lines) {
            lines = StandarizeLines(lines);
            string instructionName="";
            var signals = new List<List<string>>();

            var firstitem = lines.Where(x => !x.StartsWith(COMMENT)).ToList().First();
            if (firstitem.Contains(INSTRUCTION_NAME_HEADER)==false) 
                throw new InstructionLoaderException("Invalid instruction define syntax: Instruction definition must begin with: '" + INSTRUCTION_NAME_HEADER + " instruction_name'");

            foreach(string line in lines) {
                if (line.StartsWith(COMMENT)) continue;
                if (line.Contains(INSTRUCTION_NAME_HEADER)) {
                    instructionName = line.Replace(INSTRUCTION_NAME_HEADER, string.Empty).TrimEnd(' ', LINE_END);
                    continue;
                }
                if (line.Contains(INSTRUCTION_ARGSNUM_HEADER) && line.Contains(NO_ARGUMENT)) {
                    ZeroArgInstructions.Add(instructionName);
                    continue;
                }
                var signalsInLine = DeleteComment(line).TrimEnd(LINE_END).Split(' ').ToList();
                signals.Add(signalsInLine);
            }
            MAX_OPCODE++;
            InstructionsLines[instructionName] = lines;
            InstructionsNamesOpcodes[instructionName] = (uint)MAX_OPCODE;
            InstructionsSignalsMap[(uint)MAX_OPCODE] = signals;

            ChangeCapitalizationOfInstructionDefinitionKeywords(toUpper: true);

            if (MAX_OPCODE > ArchitectureSettings.GetMaxOpcode())
               return false;
            return true;

        }

    }
}
