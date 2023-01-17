﻿using System;
using System.Collections.Generic;
using MaszynaPi.MachineLogic.Architecture;
using MaszynaPi.MachineLogic.IODevices;
//using MaszynaPi.MachineLogic.Machines;

namespace MaszynaPi.MachineLogic {

    public class CentralUnitException : Exception { public CentralUnitException(string message) : base(message) { } }

    /*
    Computer's central processing unit (CPU) that directs the operation of the processor.
    A CU typically uses a binary decoder to convert coded instructions into timing and control signals 
    that direct the operation of the other units (memory, arithmetic logic unit and input and output devices, etc.).

     The Instruction Decoder is a CPU component that decodes and interprets the contents of the Instruction Register,
     i.e. its splits whole instruction into fields for the Control Unit to interpret. 
     The Instruction decoder is often considered to be a part of the Control Unit.
    */
    public class ControlUnit {
        const int ENDOF_INSTRUCTION = -1;      // Value for tick order tracking variables indicating that all ticks of a single instruction have been executed
        const int FETCH_CYCLE_TICK = 0; // In which Tick number in single instruction, Instruction Fetch must be performed

        // Always initialized when creating CentralUnit child class
        Dictionary<string, Action> SignalsMap;
        
        //Currently (in tick) executed microinstructions [Maybe for Central Unit View of signals]
        List<string> ActiveSignals;

        private bool USE_DEBUGGER = true;

        private int LastTick = -1;
        // Others internal Components
        private InstructionDecoder InstrDecoder;
        private InterruptionController IntController;
        private IODevicesController IOController;
        // Components visible in architecture view
        public Memory PaO { get; private set; } // Operation Memory ("FLash"?)
        public Bus MagA { get; private set; } // Address BUS
        public Bus MagS { get; private set; } // Data BUS
        public ArithmeticLogicUnit JAL { get; private set; } // Arithmetic Logic Unit
        public Register A { get; private set; }  // Address Register
        public Register S {get; private set; } // Value Register
        public Register AK { get; private set; }  // Accumulator
        public Register L { get; private set; }   //Instruction Pointer
        public InstructionRegister I { get; private set; }   // Instruction Register
        public Register X { get; private set; } // Additional Register X
        public Register Y { get; private set; } // Additional Register Y
        public Register WS { get; private set; } // Stack register
        public Register RB { get; private set; } // IO Devices Communication Register (Buffer)
        public Register G { get; private set; } // 1 bit IO Device Ready Register 
        public Register RZ { get; private set; } // 4 bit Interrupt Report Register
        public Register RM { get; private set; } // 4 bit Mask Register
        public Register RP { get; private set; } // 4 bit Register of Accepted Interrupts 
        public Register AP { get; private set; } // (CodeBits) Interrupt Vector Register

        // IO's
        CharacterInput TextInput;
        CharacterOutput TextOutput;
        TemperatureSensor TemperatureInput;
        HumiditySensor HumidityInput;
        PressureSensor PressureInput;
        MatrixLED MatrixOutput;

        public ControlUnit() {
            InstrDecoder = new InstructionDecoder();
            InstrDecoder.OnRequestALUFlagState += new Func<string,bool>(delegate { return JAL.IsFlagSet(InstrDecoder.StatementArg); });
            
            ///! Whole section of bitsize defines must be relocated into separate function (some sizes depends on architecture)
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

        // ========================== <  Signals Methods > ========--=========================== // (Microinstructions)
        void stop() { OnProgramEnd(); }
        // Architecture W
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
        
        // Architecture W+
        void _as() { if ((MagA.IsEmpty() || MagS.IsEmpty()) == false) throw new CentralUnitException("Data Bus already in use!"); MagS.SetValue(MagA.GetValue()); }
        void sa() { if ((MagA.IsEmpty() || MagS.IsEmpty()) == false) throw new CentralUnitException("Address Bus already in use!");  MagA.SetValue(MagS.GetValue()); }

        // Architecture L
        void wyx() { MagS.SetValue(X.GetValue()); }
        void wex() { X.SetValue(MagS.GetValue()); }
        void wyy() { MagS.SetValue(Y.GetValue()); }
        void wey() { Y.SetValue(MagS.GetValue()); }
        void wyws() { MagA.SetValue(WS.GetValue()); }
        void wews() { WS.SetValue(MagA.GetValue()); }
        void iws() { WS.SetValue(WS.GetValue()+1); }
        void dws() { WS.SetValue(WS.GetValue()-1); }

        // Architecture EW

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

        // JAL
        // Architecture W
        void przep() { JAL.Nop(); }
        void dod() { JAL.Add(); }
        void ode() { JAL.Sub(); }
        void weak() { JAL.SetResultAndFlags(); }
        void weja() { JAL.SetOperandB(MagS.GetValue()); }
        void wyak() { MagS.SetValue(AK.GetValue()); }

        // Architecture L

        void iak() { JAL.Inc(); }
        void dak() { JAL.Dec(); }
        void mno() { JAL.Mul(); }
        void dziel() { JAL.Div(); }
        void shr() { JAL.Shr(); }
        void neg() { JAL.Not(); }
        void lub() { JAL.Or(); }
        void i() { JAL.And(); }

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


        // =========================< UI Related Actions > ================================== // 
        // |Part which needs to be changed if another technology of UI creation is preffered|
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

        // ========================= <  Machine Cycle > =================================== //
        void FetchInstruction() {
            ActiveSignals = new List<string>(Defines.FETCH_SIGNALS);
        }

        // if the instruction completion signal is hit (STATEMENT_END) returns -1
        // Parameter "tick" controlls which point of instruction execution should be performed (start from 0 if ticks controlled manually, from 1 if called from ExecuteInstructionCycle() method)
        int ExecuteTick(int tick = FETCH_CYCLE_TICK, bool manual = false) {

            if(USE_DEBUGGER)
                SetExecutedLineInEditor(L.GetValue()-1); //select currently executed instruction on code editor (DEBUGGER)

            tick = InstrDecoder.GetJumpIndex(tick);

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

            if (wasForcedTick && LastTick>0) requiredTicks = requiredTicks - LastTick;
            for (int i = uInstructionBlock; i < requiredTicks; i++) {
                i = ExecuteTick(i);
                LastTick = i;
                if (i == ENDOF_INSTRUCTION) break;
            }
        }
        //========================================
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
                throw new CentralUnitException(ex.Message + ". Instruction-1: (" + (L.GetValue() - 1).ToString() + ") line: " + string.Join(" ", ActiveSignals)); } 
            catch (Exception ex) {
                EnableDebugger();
                SetPaintActiveSignals(true);
                throw new CentralUnitException("[Program error] " + ex.GetType().ToString() + ". Instruction-1: (" + (L.GetValue() - 1).ToString() + ") line: " + string.Join(" ", ActiveSignals) + "| " + ex.Message); }

        }


        // ======================= <  User Interface Methods > ================================= //
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

        // ========================= <  Properties Changed/Reset Methods  > =================================== //
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
            if(ActiveSignals!=null)
                ActiveSignals.Clear();
        }

        public void SetComponentsBitsizes() {
            uint Aspace = ArchitectureSettings.GetAddressSpace();
            uint Cbits = ArchitectureSettings.GetCodeBits();
            uint Mword = ArchitectureSettings.GetWordBits();
            uint IOspace = ArchitectureSettings.GetAddressSpaceForIO();
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

    }

}
