﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.MachineLogic;

namespace MaszynaPi.MachineAssembler.FilesHandling {
    class InstructionLoaderException : Exception { public InstructionLoaderException(string message) : base(message) { } }


    static class InstructionLoader {
        const string BASE_INSTRUCTION_SET_FILENAME =  "Podstawa"; // Podstawa.lst Embedded in Resources
        const string INSTRUCTION_SET_FILE_EXTENSION = ".lst";
        const string OPTIONS_HEADER = "[Opcje]";
        const string COMPONENT_ON = "1";
        const string ADDRESS_SPACE_HEADER = "Adres=";
        const string CODE_BITS_HEADER = "Kod=";
        const byte OPTIONS_LINES = 12;
        const string INSTRUCTIONS_HEADER = "[Rozkazy]";
        const string INSTRUCTION_NAME_HEADER = "ROZKAZ ";
        const string INSTRUCTION_NUMBER_HEADER = "Liczba=";
        const string INSTRUCTION_LINES_HEADER = "Linie=";
        const string INSTRUCTION_ARGSNUM_HEADER = "Argumenty ";
        const string COMMENT = "//";
        static uint MAX_OPCODE = 0;

        //For instructions view panel:
        static Dictionary<string, List<string>> InstructionsLines = new Dictionary<string, List<string>>(); //{Name, filelines}

        static Dictionary<string,uint> InstructionNamesOpcodes = new Dictionary<string,uint>(); // {name,opcode}
        static Dictionary<uint, List<List<string>>> InstructionSignalsMap = new Dictionary<uint, List<List<string>>>(); // {opcode, Lines of signals (Instruction)}
        
        static List<string> ZeroArgInstructions = new List<string>();
        //static List<List<string>> ProgramSignals;
        //static List<List<string>> GetProgramAsSignals() { if (FLAG_COMPILED) return ProgramSignals; else throw new InstructionDecoderException("Trying get not compiled signals code"); }

        public static void LoadBaseInstructions() {
            var separator = Environment.NewLine.ToCharArray();
            string baseInstructions = Properties.Resources.Podstawa;
            LoadInstructionSet(baseInstructions.Split(separator).ToList());
        }

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
            int componetsSet = 0;
            for(int i=0; i<options.Count;i++) { if (options[i].EndsWith(COMPONENT_ON)) componetsSet |= i; }
            ArchitectureSettings.SetActiveComponents((Defines.Components)componetsSet);
        }
        private static void ClearLoadedInstructions() {
            InstructionsLines.Clear();
            InstructionSignalsMap.Clear();
            InstructionNamesOpcodes.Clear();
        }
        public static void LoadInstructionSet(string filepath) {
            List<string> lines = File.ReadAllLines(filepath).ToList();
            for(int i=0; i<lines.Count;i++) lines[i] = lines[i].Replace("\n", "").Replace("\r", ""); 
            LoadInstructionSet(lines);
        }

        public static void LoadInstructionSet(List<string> lines) {
            lines.RemoveAll(string.IsNullOrWhiteSpace);
            //if (lines.IndexOf(OPTIONS_HEADER) < 0 || lines.IndexOf(INSTRUCTIONS_HEADER) < 0 || lines.IndexOf(ADDRESS_SPACE_HEADER) < 0) throw new InstructionLoaderException("Invalid format of instruction file."); //TODO: make checkFile() method as this
            List<string> options = lines.GetRange(lines.IndexOf(OPTIONS_HEADER)+1, (OPTIONS_LINES-lines.IndexOf(OPTIONS_HEADER)) );
            List<string> instructios = lines.GetRange(lines.IndexOf(INSTRUCTIONS_HEADER)+1, lines.Count-(lines.IndexOf(INSTRUCTIONS_HEADER)+1) );

            MAX_OPCODE = 0;
            ClearLoadedInstructions();
            SetOptions(options);

            if (!instructios[0].Contains(INSTRUCTION_NUMBER_HEADER)) {
                throw new InstructionLoaderException("Invalid format of .lst file -> unknown symbol on this position '" + instructios[0] + "'");
            }
            int instNum = int.Parse(instructios[0].LastOrDefault().ToString());
            
            for(int i=1; i <= instNum; i++) { // if not insruction[i].Conteins()'=' throw . . .
                InstructionNamesOpcodes.Add(instructios[i].Split('=')[1], MAX_OPCODE); // Get instruction name (always after '=' char)
                InstructionSignalsMap.Add(MAX_OPCODE, new List<List<string>>());
                InstructionsLines.Add(instructios[i].Split('=')[1], new List<string>());
                MAX_OPCODE += 1;//(uint)Math.Pow(2, ArchitectureSettings.GetAddressSpace());
            }
            instructios.RemoveRange(0, instNum+1);
            string processInstruction = "";
            foreach (string line in instructios) {
                if (line.StartsWith("[") && line.EndsWith("]")) { //is [instruction_name]
                    processInstruction = line.Replace("[", "").Replace("]", "");
                    continue;
                }
                if (line.Contains(INSTRUCTION_LINES_HEADER)) continue; // is instruction line number count
                if (line.Contains(INSTRUCTION_ARGSNUM_HEADER)) { ZeroArgInstructions.Add(processInstruction); }
                if (line.Contains(COMMENT) || line.Contains(INSTRUCTION_NAME_HEADER) || line.Contains(INSTRUCTION_ARGSNUM_HEADER)) { // is not signals lines
                    InstructionsLines[processInstruction].Add(line);
                    continue;
                }
                if (line.IndexOf('=') == line.Length - 1) continue;
                List<string> signalsInLine = line.Replace(";","").Substring(line.IndexOf('=')).Split(' ').ToList();
                InstructionSignalsMap[InstructionNamesOpcodes[processInstruction]].Add(signalsInLine);
                
                
            }
        }

/*      static void AddInstruction(List<List<string>> signals) {
            MaxOpcode += (uint)Math.Pow(2, ArchitectureSettings.GetAddressSpace());
            DecodedInstructionMap.Add(MaxOpcode, signals);
        }*/






    }
}
