using System;
using System.Collections.Generic;
using MaszynaPi.CommonOperations;
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

        /// <summary>If <see cref="ExecuteInstructionCycle"/> method takes more that this value to complete, <see cref="TimeoutException"/> should be thrown.</summary>
        static readonly TimeSpan InstructionExcecutionTimeout = new TimeSpan(0,0,5);

        /// <summary> Initialized in <see cref="InitialazeSignalsMap"/> mapping of singnals names to related <see cref="CentralProcessingUnit"/> void methods.</summary>
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
        /// <summary>Address-Data Transition Bus (address space sized)</summary>
        public Bus MagT { get; private set; }
        /// <summary>Machine's Arithmetic Logic Unit</summary>
        public ArithmeticLogicUnit JAL { get; private set; }
        /// <summary>Address Register - allows to address <see cref="PaO"/> access.</summary>
        public Register A { get; private set; } 
        /// <summary>Value Register - output for <see cref="PaO"/> values.</summary>
        public Register S {get; private set; }
        /// <summary>Accumulator - output register of <see cref="JAL"/></summary>
        public Register AK { get; private set; }
        /// <summary>Flag register - contains <see cref="ALUFlags"/> values set by <see cref="JAL"/>.</summary>
        public Register F { get; private set; }
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
        /// <summary><see cref="CharacterInput"/> controller, for managing getting characters from assigned input device.</summary>
        readonly CharacterInput TextInput;
        /// <summary><see cref="CharacterInput"/> controller, for managing putting characters to assigned output device.</summary>
        readonly CharacterOutput TextOutput;
        /// <summary>SenseHat additional IO controller, managing reading data from module's temperature sensor using <i>SensorsHandler.py</i> script.</summary>
        readonly TemperatureSensor TemperatureInput;
        /// <summary>SenseHat additional IO controller, managing reading data from module's humidity sensor using <i>SensorsHandler.py</i> script</summary>
        readonly HumiditySensor HumidityInput;
        /// <summary>SenseHat additional IO controller, managing reading data from module's pressure sensor using <i>SensorsHandler.py</i> script</summary>
        readonly PressureSensor PressureInput;
        /// <summary>SenseHat additional IO controller, managing writing data to module's LED Matrix, using <i>MatrixHandler.py</i> script</summary>
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
            
            uint Aspace   = ArchitectureSettings.GetAddressSpace();
            uint Cbits    = ArchitectureSettings.GetCodeBits();
            uint Mword    = ArchitectureSettings.GetWordBits();
            uint IOspace  = ArchitectureSettings.GetAddressSpaceForIO();
            uint FlagBits = Bitwise.GetBitsAmount(Defines.MAX_ALU_FLAG_VALUE); 
            // Atchitecture W
            PaO = new Memory();
            A = new Register(Aspace);
            S = new Register(Mword);
            L = new Register(Aspace);
            I = new InstructionRegister(Aspace,Cbits);
            AK = new Register(Mword);
            F = new Register(FlagBits);
            JAL = new ArithmeticLogicUnit(AK, F);
            MagA = new Bus(Aspace, "Address");
            MagT = new Bus(Aspace, "Transitive");
            MagS = new Bus(Mword, "Data");
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

            InitialazeSignalsMap();
        }

        #region < Signals >

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
		
        void sa_as() 
        {
            Bus source = (MagS.IsEmpty() ? MagA : MagS); 
            Bus dest = (MagS.IsEmpty() ? MagS : MagA);
            if (false == dest.IsEmpty()) throw new CPUException(dest.Name+" Bus already in use!");
            MagT.SetValue(source.GetValue());
            dest.SetValue(MagT.GetValue());
        }
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

        #region Architecture L (ALU's extention)

        void iak() { JAL.Inc(); } // works directry on accumulator reg!
        void dak() { JAL.Dec(); } // works directry on accumulator reg!
        void mno() { JAL.Mul(); }
        void dziel() { JAL.Div(); }
        void shr() { JAL.Shr(); }
        void neg() { JAL.Not(); }
        void lub() { JAL.Or(); }
        void i() { JAL.And(); }
        #endregion

        #endregion

        /// <summary>
        /// Initializes map of signals, where each signal method is defined by it's string name.
        /// <br></br>(Note: Custom [Attribute] for signal methods could be used to create map 'automatically'.)
        /// </summary>
        public void InitialazeSignalsMap() {
            var AllPLSignalsMap = new Dictionary<string, Action> {
                {"czyt",czyt},{"wyad",wyad},{"pisz",pisz},{"przep",przep},{"wys",wys},{"dod",dod},{"wes",wes},{"ode",ode},{"wei",wei},{"weak",weak},
                { "il", il },{ "weja", weja },{ "wyl", wyl },{ "wyak", wyak },{"wea",wea},{"wel",wel},{"stop",stop},{"as",sa_as}, {"sa", sa_as},{"iak",iak},
                {"dak",dak},{"mno",mno}, {"dziel",dziel},{"shr",shr},{"neg",neg},{"lub",lub},{"i",i},{"wyx",wyx},{"wex",wex},{"wyy",wyy},{"wey",wey},
                {"wyws",wyws},{"wews",wews},{"iws",iws},{"dws",dws},{"wyrb",wyrb},{"werb",werb},{"wyg",wyg},{"start",start},{"wyrm",wyrm},{"werm",werm},
                {"wyls",wyls},{"wyap",wyap},{"rint",rint },{"eni",eni}
            };
            var AllENGSignalsMap = new Dictionary<string, Action> { 
                { "start", start },{ "stop", stop }, { "rd", czyt }, { "wr", pisz }, { "ia", wea }, { "od", wys }, { "id", wes }, { "ialu", weja }, { "add", dod },
                { "sub", ode }, { "wracc", przep }, { "iacc", weak }, { "oacc", wyak }, { "iins", wei }, { "oa", wyad }, { "oit", wyl }, { "iit", wel }, { "icit", il },
                { "tbs", sa_as }, { "oitd", wyls }, { "osp", wyws }, { "isp", wews }, { "icsp", iws }, { "dcsp", dws }, { "mul", mno }, { "div", dziel },
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
        /// <summary>Allows to define action responsible for refreshing CPU components representation in GUI.</summary>
        public Action OnRefreshValues;
        /// <summary>Allows to define action responsible for signalizing in that GUI that signals repaint is neccessary.</summary>
        public Action<bool> SetPaintActiveSignals;
        /// <summary>Allows to define action responsible for showing currently executed line of program. Invoked each single instruction.</summary>
        public Action<uint> OnSetExecutedLine;
        /// <summary>Allows to define action responsible for showing currently executed microinstruction. Invoked each cycle.</summary>
        public Action<uint, List<string>> OnSetExecutedMicroinstruction;
        /// <summary>Allows to define action invoked when <see cref="stop"/> signal is activated.</summary>
        public Action OnProgramEnd;
        /// <summary>Allows to define function returning value indicating whenever loop running whole program should be halted.</summary>
        public Func<bool>CheckProgramBreak;

        /// <summary>Invoke of <see cref="OnRefreshValues"/> <see cref="Action"/>.</summary>
        private void RefreshValues() {
            OnRefreshValues.Invoke();
        }
        /// <summary>Invoke of <see cref="OnSetExecutedLine"/> <see cref="Action"/>.</summary>
        private void SetExecutedLineInEditor(uint instructionMemAddress) {
            OnSetExecutedLine(instructionMemAddress);
        }
        /// <summary>Invoke of <see cref="OnSetExecutedMicroinstruction"/> <see cref="Action"/> with current instruction's opcode and <see cref="ActiveSignals"/> list.</summary>
        private void SetExecutedMicroinstructions() {
            OnSetExecutedMicroinstruction(I.GetOpcode(), ActiveSignals);
        }

        #endregion

        #region < Machine Cycle > 
        
        /// <summary>Check if <paramref name="time"/> enlapsed since <paramref name="event"/> has occured using <see cref="DateTime.Now"/>.</summary>
        /// <param name="event">Saved <see cref="DateTime"/> when event occured.</param>
        /// <param name="time">Amout of time that should pass.</param>
        /// <returns>true if since <paramref name="event"/>, <paramref name="time"/> passed, false otherwise.</returns>
        bool DoesEnlapsedSinceEvent(DateTime @event, TimeSpan time) {
            return (DateTime.Now.Subtract(@event).Ticks > time.Ticks);
        }

        /// <summary>Assigns <see cref="Defines.FETCH_SIGNALS"/> to currently <see cref="ActiveSignals"/> list.</summary>
        void FetchInstruction() { ActiveSignals = new List<string>(Defines.FETCH_SIGNALS); }

        // if the instruction completion signal is hit (STATEMENT_END) returns -1
        // Parameter "tick" controlls which point of instruction execution should be performed (start from 0 if ticks controlled manually, from 1 if called from ExecuteInstructionCycle() method)
        /// <summary>Executes single 'tick' of CPU, base on current machine state. Sets <see cref="ActiveSignals"/> list.</summary>
        /// <param name="tick">Number (index) of consecutive tick of currently executed instruction. Defaults to 0 (see <see cref="FETCH_CYCLE_TICK"/>).</param>
        /// <param name="manual">Indicates whenever active signals (<see cref="ActiveSignals"/> content) are selected by user in manual mode (true). Defaults to false.</param>
        /// <returns>Index of current instruction's tick that should be executed, or <see cref="ENDOF_INSTRUCTION"/> if all instruction's cycles were performed.</returns>
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
                    SignalsMap[signal].Invoke();
            }
            RefreshValues();
            MagA.SetEmpty(); MagS.SetEmpty(); MagT.SetEmpty(); //Buses no longer sustain last state (MUST BE AFTER INSTRUCTION FETCH CYCLE)
            
            if (USE_DEBUGGER)
                SetExecutedMicroinstructions(); //select currently executed microinstructions in list of instructions (DEBUGGER)
            
            return tick;
        }

        /// <summary>
        /// Performes all CPU's steps neccessary for executing whole instruction. 
        /// Invokes all methods responsible for full Fetch-Decode-Execute cycle.
        /// </summary>
        void ExecuteInstructionCycle() {
            int uInstructionBlock = FETCH_CYCLE_TICK;
            DateTime instrBegin = DateTime.Now;

            FetchInstruction();
            ExecuteTick(0, false);
            uInstructionBlock++;

            uint opcode = I.GetOpcode();
            int requiredTicks = InstrDecoder.GetNumberOfTicksInInstruction(opcode);


            for (int i = uInstructionBlock; i < requiredTicks; i++) {
                i = ExecuteTick(i, false);
                LastTick = i;
                if (i == ENDOF_INSTRUCTION || CheckProgramBreak()) 
                    break;
                if (DoesEnlapsedSinceEvent(instrBegin, InstructionExcecutionTimeout))
                    throw new TimeoutException(nameof(ExecuteInstructionCycle) + " was running for more than " +
                                               InstructionExcecutionTimeout.TotalSeconds.ToString()+"[s]. Possible deadlock.");
            }
        }

        /// <summary>
        /// Executes all program instrucitons from current instruction register address to first instruction with opcode 0.
        /// Disables connected <see cref="MachineAssembler.Debugger"/> actions until end of program execution, due to performace issues,
        /// related to displaing CPU state in user interface. 
        /// <br></br>On any error, raises <see cref="CPUException"/> with informations about currently executed instruction address, as well as active signals.
        /// </summary>
        /// <exception cref="CPUException"></exception>
        void ExecuteProgram() {
            //MaszynaPi.Logger.Logger.EnableFileLog(additionalName: "_Program_Execution_Logs");
            //Exception error = null;
            try {
                DisableDebugger();
                SetPaintActiveSignals(false);
                do {
                    if (CheckProgramBreak()) { 
                        //IntController.StopJoystickInterruptionMonitor(); 
                        break; 
                    } else { 
                        ExecuteInstructionCycle(); 
                    }
                } while (I.GetOpcode() != 0);
                EnableDebugger();
                SetPaintActiveSignals(true);
            } //here also can add watchdog if there is no STP instruction in programm 
            catch (BusException ex) {
                //error = ex;
                throw new CPUException(ex.Message + ". Last succesed instruction: (" + (L.GetValue() - 1).ToString() + ") line: " + string.Join(" ", ActiveSignals) + Environment.NewLine + ex.Message); } 
            catch (Exception ex) {
                //error = ex;
                throw new CPUException("[Program error] " + ex.GetType().ToString() + ". Last succesed instruction: (" + (L.GetValue() - 1).ToString() + ") line: " + string.Join(" ", ActiveSignals) + Environment.NewLine + ex.Message); }
            finally {
                LastTick = -1;
                EnableDebugger();
                SetPaintActiveSignals(true);
                //if (false == (error is SenseHatHandlers.SenseHatException))
                //    IntController.StartJoystickInterruptionMonitor(); 
            }
        }
        #endregion

        #region < Operational Memory >

        /// <summary>Allows to set single value in <see cref="Memory.Content"/>.</summary>
        /// <param name="addr">Address of value to set.</param>
        /// <param name="value">Value to set</param>
        public void SetMemoryContent(uint addr, uint value) { PaO.StoreValue(addr, value); }

        /// <summary>Allows to set multiple values in <see cref="Memory.Content"/>, beggining at <paramref name="offset"/> address. Size of memory is not checked here!</summary>
        /// <param name="values">List of values to set.</param>
        /// <param name="offset">Begining offset from adress 0.</param>
        public void SetMemoryContent(List<uint> values, uint offset=0) { for (uint i = offset; i < values.Count; i++) PaO.StoreValue(i, values[(int)i]); }

        /// <summary>Allows to get single value from <see cref="Memory.Content"/>.</summary>
        /// <param name="addr">Address of value</param>
        /// <returns>Value stored under address <paramref name="addr"/>.</returns>
        public uint GetMemoryContent(uint addr) { return PaO.GetValue(addr); }

        /// <summary>Allows to retreive selected section of <see cref="Memory.Content"/>.</summary>
        /// <param name="addr">Starting address of section.</param>
        /// <param name="size">Size of section to retreive.</param>
        /// <returns>Range of <see cref="Memory.Content"/> defined by starting <paramref name="addr"/> and <paramref name="size"/>.</returns>
        public List<uint> GetMemoryContent(uint addr, uint size) { return PaO.GetContentHandle().GetRange((int)addr, (int)size); }
        
        /// <summary>
        /// Retreives instance of list of <see cref="uint"/> values, from internal <see cref="Memory"/> component, representing 
        /// contents of microcomputer's memory.
        /// </summary>
        /// <returns>List instance where <see cref="Memory"/> content is stored.</returns>
        public List<uint> GetMemoryContentHandle() { return PaO.GetContentHandle(); }

        /// <summary>
        /// Allows to change size of CPU's visible <see cref="Memory"/> base on currently set <see cref="ArchitectureSettings.AddressSpace"/>. 
        /// <br></br>Memory can both grow and shrink - it preserves its content, except cells that are potentially removed when shrinking.
        /// </summary>
        /// <param name="oldAddrSpace">Value of addressing bitsize before changed in <see cref="ArchitectureSettings.AddressSpace"/></param>
        public void ChangeMemorySize(uint oldAddrSpace) {
            if (oldAddrSpace < ArchitectureSettings.GetAddressSpace()) PaO.ExpandMemory(oldAddrSpace);
            else PaO.ShrinkMemory(oldAddrSpace);
        }

        /// <summary>Calls <see cref="Memory.Reset"/> method that sets memory back to it's default state (filed with <see cref="Defines.DEFAULT_MEM_VAL"/>).</summary>
        public void ResetMemory() { PaO.Reset(); }

        #endregion

        #region < User Interface Methods >

        /// <summary>Allows to set CPU's <see cref="ActiveSignals"/>.</summary>
        /// <param name="handActivatedSignals">Names of signals that should be set active.</param>
        private void SetActiveSignals(List<string> handActivatedSignals) { ActiveSignals = new List<string>(handActivatedSignals); }

        /// <summary>
        /// Invokes "<see cref="ExecuteInstructionCycle"/>" method on CPU. Performs all microoperations neccessary for executing whole instruction.
        /// <br></br><b>Not avaible if 'ManualControl' signal is active.</b>
        /// </summary>
        public void ManualInstruction() { ExecuteInstructionCycle(); } 
        /// <summary>
        /// Invokes "<see cref="ExecuteProgram"/>" method on CPU. Performs all microoperations neccessary for executing all loaded instructions, until instruction '0' is found.
        /// <br></br><b>Not avaible if 'ManualControl' signal is active.</b>
        /// </summary>
        public void ManualProgram() { ExecuteProgram(); }

        /// <summary>Allows to invoke <see cref="ExecuteTick(int, bool)"/> method. Updates CPU state base on provided list of <paramref name="activeSigs"/>.</summary>
        /// <param name="activeSigs">
        /// List of names of CPU's signals that specifies which operations should be performed. 
        /// If 'null' (default), executes from current CPU state.
        /// </param>
        public void ManualTick(List<string> activeSigs = null) { 
            if(activeSigs is null) {
                if (LastTick == ENDOF_INSTRUCTION) LastTick = 0;
                LastTick = ExecuteTick(LastTick);
                LastTick++;
                //ProgramEnd();
            } else {
                SetActiveSignals(activeSigs);
                ExecuteTick(manual:true);
            }
                
        }
        /// <summary>Sets flag that enables passing CPU's state info to user interface via 
        /// <see cref="OnSetExecutedLine"/> and <see cref="OnSetExecutedMicroinstruction"/> <see cref="Action"/>s.</summary>
        public void EnableDebugger() { USE_DEBUGGER = true; }
        /// <summary>Disables flag that indicates if state of CPU should be passed into user interface via connected <see cref="Action"/>s.</summary>
        public void DisableDebugger() { USE_DEBUGGER = false; }

        /// <summary>Allows to retreive list of <see cref="ActiveSignals"/>.</summary>
        /// <returns>Names of signals currently active in CPU.</returns>
        public List<string> GetActiveSignals() { return ActiveSignals; }

        /// <summary>Allows to retreive handle to character buffer list instance from <see cref="TextInput"/> component.</summary>
        /// <returns>Instance of <see cref="Queue{Char}"/>, containing <see cref="TextInput"/> unprocessed data.</returns>
        public Queue<char> GetTextInputBufferHandle() { return TextInput.GetCharactersBufferHandle();  }
        
        /// <summary>Allows to set <see cref="TextInput"/> <see cref="CharacterInput.OnCharacterFetched"/> <see cref="Action"/>.</summary>
        /// <param name="characterFetched"><see cref="Action"/> that should be performed when CPU fetches single character from text input's buffer.</param>
        public void SetOnFetchCharAction(Action characterFetched) { TextInput.OnCharacterFetched = characterFetched;}

        /// <summary>Allows to retreive handle to character buffer list instance from <see cref="TextOutput"/> component.</summary>
        /// <returns>Instance of <see cref="List{Char}"/>, containing <see cref="TextOutput"/> unprocessed data.</returns>
        public List<char> GetTextOutputBufferHandle() { return TextOutput.GetCharactersBufferHandle(); }

        /// <summary>Allows to set <see cref="TextOutput"/> <see cref="CharacterOutput.OnCharacterPushed"/> <see cref="Action"/>.</summary>
        /// <param name="characterPushed"><see cref="Action"/> that should be performed when CPU puts single character into text output's buffer.</param>
        public void SetOnPushCharAction(Action characterPushed) { TextOutput.OnCharacterPushed = characterPushed; }

        /// <summary>Allows to set <see cref="IntController"/> <see cref="InterruptionController.OnInterruptReported"/> <see cref="Action"/>.</summary>
        /// <param name="interruptReported"><see cref="Action"/> that should be performed when interrupt is first reported to CPU in <see cref="RZ"/> register.</param>
        public void SetOnInterruptReportedAction(Action interruptReported) { IntController.OnInterruptReported += interruptReported; }

        /// <summary>Sets <see cref="MatrixLED.Mode.Letter"></see> in Matrix IO handler, <see cref="MatrixOutput"/>.</summary>
        public void SetLEDMatrixModeLetter() { MatrixOutput.SetLetterMode(); }

        /// <summary>Sets <see cref="MatrixLED.Mode.Paint"></see> in Matrix IO handler, <see cref="MatrixOutput"/>.</summary>
        public void SetLEDMatrixModePaint() { MatrixOutput.SetPaintMode(); }

        /// <summary>Allows to retreive <see cref="ArithmeticLogicUnit"/>'s, <see cref="ALUFlags"/> state.</summary>
        /// <returns>Currently active <see cref="ALUFlags"/> from CPU's ALU instance.</returns>
        public ALUFlags GetALUFlags() { return JAL.GetFlags(); }

        public void SetALUFlagsBaseOnAccumulator() { JAL.AutoSetFlags(); }

        #endregion

        #region < Properties Changed/Reset Methods >

        /// <summary>
        /// Activates Reset() in all resetable components of <see cref="CentralProcessingUnit"/> (Could be handled by simple <b>IResetable</b>).
        /// Clears <see cref="ActiveSignals"/> and sets default value -1 of <see cref="LastTick"/> field.
        /// </summary>
        public void ResetRegisters() {
            A.Reset();
            S.Reset();
            L.Reset();
            I.Reset();
            JAL.Reset();
            MagA.Reset();
            MagS.Reset();
            MagT.Reset();
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

        /// <summary>
        /// Sets bitsizes in all related components, such as <see cref="Register"/>s or <see cref="Bus"/>es, base on current <see cref="ArchitectureSettings"/>.
        /// </summary>
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
            MagT.SetBitsize(Aspace);
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
