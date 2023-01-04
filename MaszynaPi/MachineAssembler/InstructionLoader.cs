using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.MachineLogic;
using System.Text.RegularExpressions;
using MaszynaPi.FilesHandling;

namespace MaszynaPi.MachineAssembler {
    class InstructionLoaderException : Exception { public InstructionLoaderException(string message) : base(message) { } }


    static class InstructionLoader {
        public const string INSTRUCTION_FILE_EXTENSION = ".rzk";
        public const string INSTRUCTION_SET_FILE_EXTENSION = ".lst";
        const string OPTIONS_HEADER = "[opcje]";
        const string NO_ARGUMENT = "0";
        const string COMPONENT_ON = "1";
        const char LINE_HEADER_SEPARATOR = '=';
        const string ADDRESS_SPACE_HEADER = "adres=";
        const string CODE_BITS_HEADER = "kod=";
        const byte OPTIONS_LINES = 12;
        const string INSTRUCTIONS_HEADER = "[rozkazy]";

        const string INSTRUCTION_NUMBER_HEADER = "liczba=";
        const string INSTRUCTION_LINES_HEADER = "linie=";
        const string COMMENT = "//";
        public const string LINE_END = ";";
        
        static int MAX_OPCODE=-1;

        public readonly static List<string> UPPER_WORDS = new List<string> { Defines.INSTRUCTION_NAME_HEADER.Replace(" ",""), Defines.SIGNAL_STATEMENT_IF, Defines.SIGNAL_STATEMENT_ELSE.Split(' ')[0], Defines.SIGNAL_STATEMENT_ELSE.Split(' ')[1],
                                                                        Defines.SIGNAL_STATEMENT_THEN, Defines.SIGNAL_STATEMENT_END, Defines.ALU_FLAG_INT, Defines.ALU_FLAG_Z, Defines.ALU_FLAG_V, Defines.ALU_FLAG_ZAK};

        //For instructions view panel:
        static Dictionary<string, List<string>> InstructionsLines = new Dictionary<string, List<string>>(); //{Name, filelines}

        static Dictionary<string,uint> InstructionNamesOpcodes = new Dictionary<string,uint>(); // {name,opcode}
        static Dictionary<uint, List<List<string>>> InstructionSignalsMap = new Dictionary<uint, List<List<string>>>(); // {opcode, Lines of signals (Instruction)}
        
        static List<string> ZeroArgInstructions = new List<string>();
       

        // Returns names of all avaible (loaded/declared) instructions (NOT considering the currently set code bits value)
        public static List<string> GetAvaibleInstructionsNames() {
            return InstructionNamesOpcodes.Keys.ToList();
        }

        public static bool LoadBaseInstructions() {
            var separator = Environment.NewLine.ToCharArray();
            string baseInstructions;

            if (Environment.OSVersion.Platform == PlatformID.Unix) baseInstructions = File.ReadAllText("./" + Defines.BASE_INSTRUCTION_SET_FILENAME);
            else if (Environment.OSVersion.Platform == PlatformID.Win32NT) {
                if (Defines.LangInUse == Defines.Lang.ENG) baseInstructions = Properties.Resources.Base;
                else baseInstructions = Properties.Resources.Podstawa;
            } else throw new InstructionLoaderException("Unknown deploy OS: " + Environment.OSVersion.VersionString);
            
            try { return LoadInstructionSet(baseInstructions.Split(separator).ToList());
            } catch (InstructionLoaderException ex) { throw new InstructionLoaderException("Loading Base instructions set "+INSTRUCTION_SET_FILE_EXTENSION+" file error: " + ex.Message);}
          }

        public static bool LoadInstructionsFile(string instructions) {
            var separator = Environment.NewLine.ToCharArray();
            try { return LoadInstructionSet(instructions.Split(separator).ToList());
            } catch (InstructionLoaderException ex) { throw new InstructionLoaderException("Loading Instruction Set " + INSTRUCTION_SET_FILE_EXTENSION + " file error: " + ex.Message); }
        }

        public static bool LoadSingleInstructionFile(string instructions) {
            var separator = Environment.NewLine.ToCharArray();
            try {
                return LoadSingleInstruction(instructions.Split(separator).ToList());
            } catch (InstructionLoaderException ex) { throw new InstructionLoaderException("Loading Instruction " + INSTRUCTION_FILE_EXTENSION + " file error: " + ex.Message); }
        }

        //===========================================================================================================================================

        public static Dictionary<string, List<string>> GetInstructionsLines() {
            if (InstructionsLines.Count() > 0) return InstructionsLines;
            throw new InstructionLoaderException("Trying get empty Instructions View Dictionary (LoadInstructionSetFile() method not called?).");
        }
        public static Dictionary<string,uint> GetInstructionsNamesOpcodes() {
            if (InstructionNamesOpcodes.Count() > 0) return InstructionNamesOpcodes;
            throw new InstructionLoaderException("Trying get empty Instructions View Dictionary (LoadInstructionSetFile() method not called?).");
        }
        public static Dictionary<uint, List<List<string>>> GetInstructionSignalsMap() {
            if (InstructionSignalsMap.Count() > 0) return InstructionSignalsMap;
            throw new InstructionLoaderException("Trying get empty Instructions View Dictionary (LoadInstructionSetFile() method not called?).");
        }
        public static List<string> GetZeroArgInstructions() { return ZeroArgInstructions; }

        //===========================================================================================================================================
        private static void SetOptions(List<string> options) { //TODO wrzucić do osobnej funkcji z parametrem addrSpace/codeBits żeby nie powtarzać 2x kodu 
            bool asParse = int.TryParse(options.Find(el => el.Contains(ADDRESS_SPACE_HEADER)).Replace(ADDRESS_SPACE_HEADER, ""), out int addrSpace);
            bool cbParse = int.TryParse(options.Find(el => el.Contains(CODE_BITS_HEADER)).Replace(CODE_BITS_HEADER, ""), out int codeBits);
            if (!asParse) throw new InstructionLoaderException("Invalid value in "+OPTIONS_HEADER+" "+ADDRESS_SPACE_HEADER+". Must be numeric.");
            if (!cbParse) throw new InstructionLoaderException("Invalid value in " + OPTIONS_HEADER + " " + CODE_BITS_HEADER + ". Must be numeric.");
            ArchitectureSettings.SetAddressSpace((uint)addrSpace);
            ArchitectureSettings.SetCodeBits((uint)codeBits);
            options.Remove(ADDRESS_SPACE_HEADER+addrSpace.ToString()); 
            options.Remove(CODE_BITS_HEADER + codeBits.ToString());
            //--------------------------------------------------------------------------------------------------------------
            int componetsSet = (int)Defines.Components.Basic;
            for(int i=0; i<options.Count;i++) { if (options[i].EndsWith(COMPONENT_ON)) componetsSet |= (1<<i+(int)Defines.Components.Basic); }
            ArchitectureSettings.SetActiveComponents((Defines.Components)componetsSet);
        }
        private static void ClearLoadedInstructions() {
            InstructionsLines.Clear();
            InstructionSignalsMap.Clear();
            InstructionNamesOpcodes.Clear();
            ZeroArgInstructions.Clear();
        }


        // Checks if instruction starts with czyt wys wei il; :)
        private static bool IsValidStartOfInstruction(List<string> instructionline) {
            return Defines.FETCH_SIGNALS.Intersect(instructionline).Count() == Defines.FETCH_SIGNALS.Count();
        }


        // Returns true if foud only in valid order statments sequences => none || if [arg] then @label else @label || if [arg] then @label
        private static bool IsStatementValid(List<string> sigline) {
            var sigcopy = new List<string>(sigline);
            if (sigcopy[0].StartsWith(Defines.SIGNAL_LABEL)) sigcopy.RemoveAt(0);
            if (sigcopy.All(sig => false==sig.Contains(Defines.SIGNAL_LABEL))) return true;

            string line = string.Join(" ",sigcopy);
            string ifRegex = "^[a-z ]*"+Defines.SIGNAL_STATEMENT_IF+" [a-z]+ "+Defines.SIGNAL_STATEMENT_THEN+" @[a-z]+$";
            string ifelseRegex = ifRegex.Replace("$"," ")+Defines.SIGNAL_STATEMENT_ELSE+" @[a-z]+$";
            if (Regex.IsMatch(line, ifRegex) || Regex.IsMatch(line, ifelseRegex)) return true;
            return false;
        }

        private static List<string> StandarizeLines(List<string> lines) {
            lines.RemoveAll(string.IsNullOrWhiteSpace);
            return lines.ConvertAll(d => d.ToLower());
        }

        /// Changes specific words to Upper/Lower (param) for displaing Avaible Instructions
        private static void ChangeCapitalizationOfInstructionLines(bool toUpper = true) {
            var refractoredLines = new Dictionary<string, List<string>>();
            List<string> toReplace;
            string line = "";
            foreach (string name in InstructionsLines.Keys) {
                refractoredLines.Add(name, new List<string>());

                foreach (string item in InstructionsLines[name]) {
                    toReplace = UPPER_WORDS.Intersect(item.Split(' ')).ToList();
                    line = item;
                    if (line.StartsWith(COMMENT)) { refractoredLines[name].Add(line); continue; }

                    foreach (string word in toReplace) {
                        string toreplace = word + " "; //" " + word + " ";
                        line = line.Replace(toreplace, (toUpper ? toreplace.ToUpper() : toreplace.ToLower())); 
                    }

                    refractoredLines[name].Add(line);    
                }
            }
            InstructionsLines.Clear();
            InstructionsLines = new Dictionary<string, List<string>>(refractoredLines);
        }


        public static bool LoadInstructionSet(List<string> lines) {
            lines = StandarizeLines(lines);
            List<string> options = lines.GetRange(lines.IndexOf(OPTIONS_HEADER)+1, (OPTIONS_LINES-lines.IndexOf(OPTIONS_HEADER)) );
            List<string> instructios = lines.GetRange(lines.IndexOf(INSTRUCTIONS_HEADER)+1, lines.Count-(lines.IndexOf(INSTRUCTIONS_HEADER)+1) );

            MAX_OPCODE = -1;

            var instructionsLines = new Dictionary<string, List<string>>();//new Dictionary<string, List<string>>(InstructionsLines);
            var instructionNamesOpcodes = new Dictionary<string, uint>();//new Dictionary<string, uint>(InstructionNamesOpcodes);
            var instructionSignalsMap = new Dictionary<uint, List<List<string>>>();//new Dictionary<uint, List<List<string>>>(InstructionSignalsMap);
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
            string processInstruction = "";
            bool czytwysweiil = false;
            foreach (string line in instructios) {
                if (line.StartsWith("[") && line.EndsWith("]")) { //is [instruction_name]
                    processInstruction = line.Replace("[", "").Replace("]", "");
                    czytwysweiil = true;
                    continue;
                }

                if (line.Contains(INSTRUCTION_LINES_HEADER)) continue; // is instruction line number count
                
                instructionsLines[processInstruction].Add(line.Substring(line.IndexOf(LINE_HEADER_SEPARATOR)+1));

                if (line.Contains(Defines.INSTRUCTION_ARGSNUM_HEADER) && line.Contains(NO_ARGUMENT)) { zeroArgInstructions.Add(processInstruction); }
                if (line.Contains(COMMENT) || line.Contains(Defines.INSTRUCTION_NAME_HEADER) || line.Contains(Defines.INSTRUCTION_ARGSNUM_HEADER)) { // is not signals lines
                    continue;
                }
                if (line.IndexOf(LINE_HEADER_SEPARATOR) == line.Length - 1) continue; //empty line

                List<string> signalsInLine = line.Replace(LINE_END,"").Substring(line.IndexOf(LINE_HEADER_SEPARATOR) +1).Split(' ').ToList();

                if (IsStatementValid(signalsInLine) == false)
                    throw new InstructionLoaderException("Invalid define of instruction statement: " + string.Join(" ", signalsInLine) +"\nCheck instruction file encoding (ANSI might not be supported)");
                if (czytwysweiil) {
                    if (false == IsValidStartOfInstruction(signalsInLine)) 
                        throw new InstructionLoaderException("Critical error in defined instruction "+processInstruction+". Instruction does not start with fetch cycle. Say after me! "+string.Join(", ",Defines.FETCH_SIGNALS)+" [!]");
                    czytwysweiil = false;
                }
                instructionSignalsMap[instructionNamesOpcodes[processInstruction]].Add(signalsInLine);
            }

            ClearLoadedInstructions(); // not neccessary?
            InstructionsLines = new Dictionary<string, List<string>>(instructionsLines);
            InstructionNamesOpcodes = new Dictionary<string, uint>(instructionNamesOpcodes);
            InstructionSignalsMap = new Dictionary<uint, List<List<string>>>(instructionSignalsMap);
            ZeroArgInstructions = new List<string>(zeroArgInstructions);

            SetOptions(options);
            ChangeCapitalizationOfInstructionLines(toUpper:true);

            if (MAX_OPCODE > ArchitectureSettings.GetMaxOpcode())
                return false;
            return true;

        }

        //===========================================================================================================================================

        public static void AddInstruction(string name, List<List<string>> signals) {
            MAX_OPCODE++;
            List<string> signalsLines = new List<string>();
            foreach (var inLine in signals) signalsLines.Add(string.Join(" ", inLine));

            //InstructionsLines.Add(name, signalsLines);
            //InstructionNamesOpcodes.Add(name, (uint)MAX_OPCODE);
            //InstructionSignalsMap.Add((uint)MAX_OPCODE, signals);

            //Add or update
            InstructionsLines[name] = signalsLines;
            InstructionNamesOpcodes[name] = (uint)MAX_OPCODE;
            InstructionSignalsMap[(uint)MAX_OPCODE] = signals;

            ChangeCapitalizationOfInstructionLines(toUpper: true);
        }

        // Returns false if there is not enough space for the instruction
        public static bool LoadSingleInstruction(List<string> lines) {
            lines = StandarizeLines(lines);
            string instructionName="";
            bool noArgumentInstruction = false;
            var signals = new List<List<string>>();

            if (lines.Any(line => line.Contains(Defines.INSTRUCTION_NAME_HEADER))==false) 
                throw new InstructionLoaderException("Invalid instruction define syntax: Instruction definition must begin with: '" + Defines.INSTRUCTION_NAME_HEADER + " instruction_name'");

            foreach(string line in lines) {
                if (line.Contains(Defines.INSTRUCTION_NAME_HEADER)) {
                    instructionName = line.Replace(Defines.INSTRUCTION_NAME_HEADER, "").TrimEnd((" "+LINE_END).ToCharArray());
                }
                if (lines[0].Contains(Defines.INSTRUCTION_ARGSNUM_HEADER)) {  
                    noArgumentInstruction = line.Replace(Defines.INSTRUCTION_ARGSNUM_HEADER, "").TrimEnd((" " + LINE_END).ToCharArray()).Equals(NO_ARGUMENT);
                }
                signals.Add(line.Split(' ').ToList());
            }

            AddInstruction(instructionName, signals);

            if (MAX_OPCODE > ArchitectureSettings.GetMaxOpcode())
               return false;
            return true;

        }

    }
}
