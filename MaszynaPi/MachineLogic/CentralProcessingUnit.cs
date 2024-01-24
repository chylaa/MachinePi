using System;
using System.Collections.Generic;
using MaszynaPi.MachineLogic.Architecture;
using MaszynaPi.MachineLogic.IODevices;

namespace MaszynaPi.MachineLogic {

    /// <summary>General custom <see cref="Exception"/> for signalizing errors related to <see cref="CentralProcessingUnit"/> data/control flow.</summary>
    public class CPUException : Exception { public CPUException(string message) : base(message) { } }

    /// <summary> 
    /// Main monolith class, representing microcomputer's processing unit with its connections to other components such as memory or IO devices.
    /// Contain set of internal <see cref="Register"/>, responsible for preserving state of machine between clock cycles.
    /// Class allows to manage executing instructions by controlling internal components of the machine.<br></br>
    /// <br></br>Note:<i> The abbreviations for component and signal names are partly abbreviations from Polish,
    /// in homage to the original naming of the architecture parts of this microcomputer. The English expansion can be found in the comments.</i> 
    /// </summary>
    public class CentralProcessingUnit {
        /// <summary>Value for in-instruction tick tracking variables indicating that all cycles of a single instruction have been executed.</summary>
        const int ENDOF_INSTRUCTION = -1;
        /// <summary>In which cycle of single instruction, Fetch must be performed.</summary>
        const int FETCH_CYCLE_TICK = 0; // 

        /// <summary> Initialized in <see cref="InitialazeMicroinstructionsMap"/> mapping of singnals names to related <see cref="CentralProcessingUnit"/> void methods.</summary>
        Dictionary<string, Action> SignalsMap;

        /// <summary>List of signals active in current clock cycle.</summary>
        List<string> ActiveSignals;

        /// <summary> Flag indicating whenever <see cref="MachineAssembler.Debugger"/> class should be carring UI machine state preview.</summary>
        private bool USE_DEBUGGER = true;

        /// <summary> Keeps track current cycle of single instruction execution. </summary> 
        private int LastTick = -1;
        
        
        /// <summary>Decoder providing methods to determine next machine state base on instructions from <see cref="Memory"/>.</summary>
        private readonly InstructionDecoder InstrDecoder;
        /// <summary> Interruptions controller, providing methods to determine next machine state base on various interrupts sources. </summary>
        private readonly InterruptionController IntController;
        /// <summary> Controller providing methods for controlling state of input/output devices (except <see cref="Memory"/>) connected to machine.</summary>
        private readonly IODevicesController IOController;

        #region Components visible in architecture view
        /// <summary> Operational Code and Data Memory </summary>
        public Memory PaO { get; private set; }
        /// <summary>Address Bus</summary>
        public Bus MagA { get; private set; }
        /// <summary>Data Bus</summary>
        public Bus MagS { get; private set; }
        /// <summary>Machine's Arithmetic Logic Unit</summary>
        public ArithmeticLogicUnit JAL { get; private set; }
        /// <summary>Address Register - allows to address <see cref="PaO"/> access.</summary>
        public Register A { get; private set; } 
        /// <summary>Value Register - output for <see cref="PaO"/> values.</summary>
        public Register S {get; private set; }
        /// <summary>Accumulator - output register of <see cref="JAL"/></summary>
        public Register AK { get; private set; } 
        /// <summary>Instruction pointer register. Stores address of next instruction.</summary>
        public Register L { get; private set; }   //Instruction Pointer
        /// <summary>Current Instruction register.</summary>
        public InstructionRegister I { get; private set; }  
        /// <summary>Additional general-purpose register X</summary>
        public Register X { get; private set; }
        /// <summary>Additional general-purpose register Y</summary>
        public Register Y { get; private set; }
        /// <summary>Stack register - point to top of the stack.</summary>
        public Register WS { get; private set; }
        /// <summary>Buffer for communication with IO Devices.</summary>
        public Register RB { get; private set; }
        /// <summary>1 bit IO Device Ready register.</summary>
        public Register G { get; private set; }
        /// <summary>4 bit Interruption Report register.</summary>
        public Register RZ { get; private set; }
        /// <summary>4 bit Interruption Mask register.</summary>
        public Register RM { get; private set; }
        /// <summary>4 bit register storing info about accepted Interrupt.</summary>
        public Register RP { get; private set; }
        /// <summary>Interrupt Vector register.</summary>
        public Register AP { get; private set; }

        #endregion

        #region IO's
        readonly CharacterInput TextInput;
        readonly CharacterOutput TextOutput;
        readonly TemperatureSensor TemperatureInput;
        readonly HumiditySensor HumidityInput;
        readonly PressureSensor PressureInput;
        readonly MatrixLED MatrixOutput;
        #endregion

        /// <summary>
        /// Creates new CPU's instance, initialized with defined <see cref="ArchitectureSettings"/>
        /// and microinstructions set, connected to selected <see cref="Defines.LangInUse"/>.
        /// <br></br><br></br>
        /// Note: <i>Updating simulator to support different <see cref="Defines.Architecture"/> during runtime,
        /// requires internal section of bitsize-related values initializaiton to be relocated into separate function
        /// (part of component's sizes depends on architecture)</i>.
        /// </summary>
        public CentralProcessingUnit() 
        {
            InstrDecoder = new InstructionDecoder();
            InstrDecoder.OnRequestALUFlagState += new Func<string,bool>(delegate { return JAL.IsFlagSet(InstrDecoder.StatementArg); });
            
            uint Aspace  = ArchitectureSettings.GetAddressSpace();
            uint Cbits   = ArchitectureSettings.GetCodeBits();
            uint Mword   = ArchitectureSettings.GetWordBits();
            uint IOspace = ArchitectureSettings.GetAddressSpaceForIO(); 
            // Atchitecture W
            PaO = new Memory();
            A = new Register(Aspace);
            S = new Register(Mword);
            L = new Register(Aspace);
            I = new InstructionRegister(Aspace,Cbits);
            AK = new Register(Mword);
            JAL = new ArithmeticLogicUnit(AK);
            MagA = new Bus(Aspace);
            MagS = new Bus(Mword);
            // Architecture L
            X = new Register(Mword);
            Y = new Register(Mword);
            WS = new Register(Aspace);
            // Architecture EW
            RB = new Register(Mword); // In orginal machine size=Defines.RB_REG_BIT_SIZE (Only ASCII - 8bit)
            G = new Register(Defines.G_REG_BIT_SIZE);

            RZ = new Register(Defines.INTERRUPTIONS_NUM);
            RM = new Register(Defines.INTERRUPTIONS_NUM);
            RP = new Register(Defines.INTERRUPTIONS_NUM);
            AP = new Register(Cbits);
            IntController = new InterruptionController(RZ, RM, RP, AP);

            TextInput = new CharacterInput(G, RB); 
            TextOutput = new CharacterOutput(G, RB);
            TemperatureInput = new TemperatureSensor(G, RB);
            HumidityInput = new HumiditySensor(G, RB);
            PressureInput = new PressureSensor(G, RB);
            MatrixOutput = new MatrixLED(G, RB);
            IOController = new IODevicesController(TextInput, TextOutput, TemperatureInput, HumidityInput, PressureInput, MatrixOutput);

            InitialazeMicroinstructionsMap();
        }

        #region < Signals/Microinstructions >

        void stop() { OnProgramEnd(); }

        #region Architecture W
        void czyt() { S.SetValue(PaO.GetValue(A.GetValue())); }
        void pisz() { PaO.StoreValue(A.GetValue(), S.GetValue()); }
        void wys() { MagS.SetValue(S.GetValue()); }
        void wes() { S.SetValue(MagS.GetValue()); }
        void wei() { I.SetValue(MagS.GetValue()); I.DecodeInstruction(); }
        void il() { L.SetValue(L.GetValue()+1); }
        void wyl() { MagA.SetValue(L.GetValue()); }
        void wel() { L.SetValue(MagA.GetValue()); }
        void wyad() { MagA.SetValue(I.GetArgument()); }
        void wea() { A.SetValue(MagA.GetValue()); }
        #endregion

        #region Architecture W+
        void _as() { if ((MagA.IsEmpty() || MagS.IsEmpty()) == false) throw new CPUException("Data Bus already in use!"); MagS.SetValue(MagA.GetValue()); }
        void sa() { if ((MagA.IsEmpty() || MagS.IsEmpty()) == false) throw new CPUException("Address Bus already in use!");  MagA.SetValue(MagS.GetValue()); }
        #endregion

        #region Architecture L
        void wyx() { MagS.SetValue(X.GetValue()); }
        void wex() { X.SetValue(MagS.GetValue()); }
        void wyy() { MagS.SetValue(Y.GetValue()); }
        void wey() { Y.SetValue(MagS.GetValue()); }
        void wyws() { MagA.SetValue(WS.GetValue()); }
        void wews() { WS.SetValue(MagA.GetValue()); }
        void iws() { WS.SetValue(WS.GetValue()+1); }
        void dws() { WS.SetValue(WS.GetValue()-1); }
        #endregion

        #region Architecture EW

        void wyls() { MagS.SetValue(L.GetValue()); }
        void wyrb() { MagS.SetValue(RB.GetValue()); }
        void werb() { RB.SetValue(MagS.GetValue()); }
        void wyg() { MagS.SetValue(G.GetValue()); }
        void start() { IOController.HandleIOOnStartSignal(I.GetArgument()); } 
        void wyrm() { MagA.SetValue(RM.GetValue()); }
        void werm() { RM.SetValue(MagA.GetValue()); }
        void wyap() { MagA.SetValue(AP.GetValue()); }
        void rint() { IntController.ClearMSBOfAcceptedINTs(); }
        void eni()  { IntController.SetAcceptedAndINTVectorRegister(JAL); }
        #endregion

        #region ALU's uOps

        #region Architecture W
        void przep() { JAL.Nop(); }
        void dod() { JAL.Add(); }
        void ode() { JAL.Sub(); }
        void weak() { JAL.SetResultAndFlags(); }
        void weja() { JAL.SetOperandB(MagS.GetValue()); }
        void wyak() { MagS.SetValue(AK.GetValue()); }
        #endregion

        #region Architecture L

        void iak() { JAL.Inc(); }
        void dak() { JAL.Dec(); }
        void mno() { JAL.Mul(); }
        void dziel() { JAL.Div(); }
        void shr() { JAL.Shr(); }
        void neg() { JAL.Not(); }
        void lub() { JAL.Or(); }
        void i() { JAL.And(); }
        #endregion

        #endregion
        
        public void InitialazeMicroinstructionsMap() {
            var AllPLSignalsMap = new Dictionary<string, Action> {
                {"czyt",czyt},{"wyad",wyad},{"pisz",pisz},{"przep",przep},{"wys",wys},{"dod",dod},{"wes",wes},{"ode",ode},{"wei",wei},{"weak",weak},
                { "il", il },{ "weja", weja },{ "wyl", wyl },{ "wyak", wyak },{"wea",wea},{"wel",wel},{"stop",stop},{"as",_as}, {"sa", sa},{"iak",iak},
                {"dak",dak},{"mno",mno}, {"dziel",dziel},{"shr",shr},{"neg",neg},{"lub",lub},{"i",i},{"wyx",wyx},{"wex",wex},{"wyy",wyy},{"wey",wey},
                {"wyws",wyws},{"wews",wews},{"iws",iws},{"dws",dws},{"wyrb",wyrb},{"werb",werb},{"wyg",wyg},{"start",start},{"wyrm",wyrm},{"werm",werm},
                {"wyls",wyls},{"wyap",wyap},{"rint",rint },{"eni",eni}
            };
            var AllENGSignalsMap = new Dictionary<string, Action> { 
                { "start", start },{ "stop", stop }, { "rd", czyt }, { "wr", pisz }, { "ia", wea }, { "od", wys }, { "id", wes }, { "ialu", weja }, { "add", dod },
                { "sub", ode }, { "wracc", przep }, { "iacc", weak }, { "oacc", wyak }, { "iins", wei }, { "oa", wyad }, { "oit", wyl }, { "iit", wel }, { "icit", il },
                { "ad", _as }, { "da", sa }, { "oitd", wyls }, { "osp", wyws }, { "isp", wews }, { "icsp", iws }, { "dcsp", dws }, { "mul", mno }, { "div", dziel },
                { "shr", shr }, { "icacc", iak }, { "dcacc", dak }, { "not", neg }, { "or", lub }, { "and", i }, { "oim", wyrm }, { "iim", werm }, { "oiv", wyap },
                { "yx", wyx }, { "ix", wex }, { "oy", wyy }, { "iy", wey }, { "obuf", wyrb }, { "ibuf", werb }, { "ord", wyg } ,{"rint",rint },{"eni",eni}
            };
            //SignalsMap = AllSignalsMap
            //    .Where(item => ArchitectureSettings.GetAvaibleSignals().Contains(item.Key))
            //    .ToDictionary(item => item.Key, item => item.Value);
            if (Defines.LangInUse == Defines.Lang.PL) {
                SignalsMap = AllPLSignalsMap;
            } else {
                SignalsMap = AllENGSignalsMap;
            }
        }
        #endregion

        // |Part which needs to be changed if another technology of UI creation is preffered| //
        #region < UI Related Actions > 
        public Action<bool> SetPaintActiveSignals;
        public Action OnRefreshValues;
        public Action<uint> OnSetExecutedLine;
        public Action<uint, List<string>> OnSetExecutedMicroinstruction;
        public Action OnProgramEnd;
        public Func<bool>CheckProgramBreak;

        public void RefreshValues() {
            OnRefreshValues();
        }
        public void SetExecutedLineInEditor(uint instructionMemAddress) {
            OnSetExecutedLine(instructionMemAddress);
        }
        public void SetExecutedMicroinstructions() {
            OnSetExecutedMicroinstruction(I.GetOpcode(), ActiveSignals);
        }
        public void ProgramEnd() {
            if (I.GetOpcode() == 0)
                OnProgramEnd();
        }
        #endregion

        #region < Machine Cycle > 
        
        void FetchInstruction() { ActiveSignals = new List<string>(Defines.FETCH_SIGNALS); }

        // if the instruction completion signal is hit (STATEMENT_END) returns -1
        // Parameter "tick" controlls which point of instruction execution should be performed (start from 0 if ticks controlled manually, from 1 if called from ExecuteInstructionCycle() method)
        int ExecuteTick(int tick = FETCH_CYCLE_TICK, bool manual = false) {

            if(USE_DEBUGGER)
                SetExecutedLineInEditor(L.GetValue()-1); //select currently executed instruction on code editor (DEBUGGER)

            tick = InstrDecoder.GetNextSignalsIndex(tick);

            if(manual == false) {
                ActiveSignals = InstrDecoder.DecodeActiveSignals(instructionOpcode: I.GetOpcode(), tick);
                bool eofInstruction = (tick == InstrDecoder.GetNumberOfTicksInInstruction(I.GetOpcode()) - 1);
                if (eofInstruction) tick = ENDOF_INSTRUCTION; // Protection from manual tick execution
            }

            foreach (string signal in ActiveSignals) {
                if (signal.Equals(Defines.SIGNAL_STATEMENT_END)) {
                    tick = ENDOF_INSTRUCTION;
                    break;
                };
                if (SignalsMap.ContainsKey(signal)) //skips conditional statements 
                    SignalsMap[signal]();
            }
            MagA.SetEmpty(); MagS.SetEmpty(); //Buses no longer sustain last state (MUST BE AFTER INSTRUCTION FETCH CYCLE)
            RefreshValues();
            
            if (USE_DEBUGGER)
                SetExecutedMicroinstructions(); //select currently executed microinstructions in list of instructions (DEBUGGER)
            
            return tick;
        }

        void ExecuteInstructionCycle(bool wasForcedTick=false) {
            int uInstructionBlock = FETCH_CYCLE_TICK;

            FetchInstruction();
            ExecuteTick();
            uInstructionBlock++;

            uint opcode = I.GetOpcode();
            int requiredTicks = InstrDecoder.GetNumberOfTicksInInstruction(opcode);

            if (wasForcedTick && LastTick > 0) 
                requiredTicks -= LastTick;

            for (int i = uInstructionBlock; i < requiredTicks; i++) {
                i = ExecuteTick(i);
                LastTick = i;
                if (i == ENDOF_INSTRUCTION) 
                    break;
            }
        }

        void ExecuteProgram() {
            //MaszynaPi.Logger.Logger.EnableFileLog(additionalName: "_Program_Execution_Logs");
            try {
                DisableDebugger();
                SetPaintActiveSignals(false);
                do {
                    if (CheckProgramBreak()) break;
                    ExecuteInstructionCycle(); 
                } while (I.GetOpcode() != 0);
                EnableDebugger();
                SetPaintActiveSignals(true);
            } //here also can add watchdog if there is no STP instruction in programm 
            catch (BusException ex) {
                EnableDebugger();
                SetPaintActiveSignals(true);
                throw new CPUException(ex.Message + ". Instruction-1: (" + (L.GetValue() - 1).ToString() + ") line: " + string.Join(" ", ActiveSignals)); } 
            catch (Exception ex) {
                EnableDebugger();
                SetPaintActiveSignals(true);
                throw new CPUException("[Program error] " + ex.GetType().ToString() + ". Instruction-1: (" + (L.GetValue() - 1).ToString() + ") line: " + string.Join(" ", ActiveSignals) + "| " + ex.Message); }

        }
        #endregion

        #region < User Interface Methods >
        public void SetMemoryContent(uint addr, uint value) { PaO.StoreValue(addr, value); }
        public void SetMemoryContent(List<uint> values, uint offset=0) { for (uint i = offset; i < values.Count; i++) PaO.StoreValue(i, values[(int)i]); }
        public uint GetMemoryContent(uint addr) { return PaO.GetValue(addr); }
        public List<uint> GetMemoryContent(uint addr, uint size) { return PaO.GetContentHandle().GetRange((int)addr, (int)size); }
        public List<uint> GetMemoryContentHandle() { return PaO.GetContentHandle(); }
        public void ChangeMemorySize(uint oldAddrSpace) {
            if (oldAddrSpace < ArchitectureSettings.GetAddressSpace()) PaO.ExpandMemory(oldAddrSpace);
            else PaO.ShrinkMemory(oldAddrSpace);
        }

        public void ResetMemory() { PaO.Reset(); }

        public void AddActiveSignals(List<string> handActivatedSignals) { ActiveSignals.AddRange(handActivatedSignals); }
        public void SetActiveSignals(List<string> handActivatedSignals) { ActiveSignals = new List<string>(handActivatedSignals); }

        //public void ManualTick() { ExecuteTick(); ProgramEnd(); } // TODO: Change to work  with parameter 
        public void ManualInstruction() { ExecuteInstructionCycle(wasForcedTick: false); }//ProgramEnd(); } // Not avaible if ManualControl signal active
        public void ManualProgram() { ExecuteProgram(); }//ProgramEnd(); } // Not avaible if ManualControl signal active

        public void ManualTick(List<string> activeSigs = null) { 
            if(activeSigs == null) {
                if (LastTick == ENDOF_INSTRUCTION) LastTick = 0;
                LastTick = ExecuteTick(LastTick);
                LastTick++;
                //ProgramEnd();
            } else {
                SetActiveSignals(activeSigs);
                ExecuteTick(manual:true);
            }
                
        }

        public void EnableDebugger() { USE_DEBUGGER = true; }
        public void DisableDebugger() { USE_DEBUGGER = false; }

        public List<string> GetActiveSignals() { return ActiveSignals; }

        public List<char> GetTextInputBufferHandle() { return TextInput.GetCharactersBufferHandle();  }
        public void SetOnFetchCharAction(Action characterFetched) { TextInput.OnCharacterFetched = characterFetched;}
        public List<char> GetTextOutputBufferHandle() { return TextOutput.GetCharactersBufferHandle(); }

        public void SetOnPushCharAction(Action characterPushed) { TextOutput.OnCharacterPushed = characterPushed; }
        public void SetOnInterruptReportedAction(Action interruptReported) { IntController.OnInterruptReported += interruptReported; }

        public void SetLEDMatrixModeLetter() { MatrixOutput.SetLetterMode(); }
        public void SetLEDMatrixModePaint() { MatrixOutput.SetPaintMode(); }

        public ALUFlags GetALUFlags() { return JAL.GetFlags(); }

        #endregion

        #region < Properties Changed/Reset Methods >
        public void ResetRegisters() {
            A.Reset();
            S.Reset();
            L.Reset();
            I.Reset();
            JAL.Reset();
            MagA.Reset();
            MagS.Reset();
            X.Reset();
            Y.Reset();
            WS.Reset();
            RB.Reset();
            G.Reset();
            RZ.Reset();
            RM.Reset();
            RP.Reset();
            AP.Reset();

            LastTick = -1;
            ActiveSignals?.Clear();
        }

        public void SetComponentsBitsizes() {
            uint Aspace = ArchitectureSettings.GetAddressSpace();
            uint Cbits = ArchitectureSettings.GetCodeBits();
            uint Mword = ArchitectureSettings.GetWordBits();
            //uint IOspace = ArchitectureSettings.GetAddressSpaceForIO();
            A.SetBitsize(Aspace);
            S.SetBitsize(Mword);
            L.SetBitsize(Aspace);
            I.SetBitsize(Aspace, Cbits);
            AK.SetBitsize(Mword);
            MagA.SetBitsize(Aspace);
            MagS.SetBitsize(Mword);
            X.SetBitsize(Mword);
            Y.SetBitsize(Mword);
            WS.SetBitsize(Aspace);
            RB.SetBitsize(Mword);

            RZ.SetBitsize(Defines.INTERRUPTIONS_NUM);
            RM.SetBitsize(Defines.INTERRUPTIONS_NUM);
            RP.SetBitsize(Defines.INTERRUPTIONS_NUM);
            AP.SetBitsize(Cbits);
        }

        #endregion
    }

}
