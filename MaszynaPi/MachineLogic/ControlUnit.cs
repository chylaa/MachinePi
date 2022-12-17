using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.MachineLogic.Architecture;
using MaszynaPi.MachineLogic.IODevices;
using MaszynaPi.MachineAssembler;
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
        const int INSTRUCTION_FETCH_ORDER = 0; // In which Tick number in single instruction, Instruction Fetch must be performed

        // Always initialized when creating CentralUnit child class
        Dictionary<string, Action> SignalsMap;
        
        //Currently (in tick) executed microinstructions [Maybe for Central Unit View of signals]
        List<string> ActiveSignals;



        private int LastTick = -1;
        // Others internal Components
        private InstructionDecoder RzKDecoder;
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
        public CharacterInput TextInput;
        public CharacterOutput TextOutput;

        public ControlUnit() {
            RzKDecoder = new InstructionDecoder();
            RzKDecoder.OnRequestALUFlagState += new Func<string,bool>(delegate { return JAL.IsFlagSet(RzKDecoder.StatementArg); });
            
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
            IOController = new IODevicesController(TextInput, TextOutput);

            InitialazeMicroinstructionsMap();
        }

        // ========================== <  Signals Methods > ========--=========================== // (Microinstructions)
        public void stop() { return; }
        // Architecture W
        public void czyt() { S.SetValue(PaO.GetValue(A.GetValue())); }
        public void pisz() { PaO.StoreValue(A.GetValue(), S.GetValue()); }
        public void wys() { MagS.SetValue(S.GetValue()); }
        public void wes() { S.SetValue(MagS.GetValue()); }
        public void wei() { I.SetValue(MagS.GetValue()); I.DecodeInstruction(); }
        public void il() { L.SetValue(L.GetValue()+1); }
        public void wyl() { MagA.SetValue(L.GetValue()); }
        public void wel() { L.SetValue(MagA.GetValue()); }
        public void wyad() { MagA.SetValue(I.GetArgument()); }
        public void wea() { A.SetValue(MagA.GetValue()); }
        
        // Architecture W+
        public void _as() { MagS.SetValue(MagA.GetValue()); }
        public void sa() { MagA.SetValue(MagS.GetValue()); }

        // Architecture L
        public void wyx() { MagS.SetValue(X.GetValue()); }
        public void wex() { X.SetValue(MagS.GetValue()); }
        public void wyy() { MagS.SetValue(Y.GetValue()); }
        public void wey() { Y.SetValue(MagS.GetValue()); }
        public void wyws() { MagA.SetValue(WS.GetValue()); }
        public void wews() { WS.SetValue(MagA.GetValue()); }
        public void iws() { WS.SetValue(WS.GetValue()+1); }
        public void dws() { WS.SetValue(WS.GetValue()-1); }

        // Architecture EW

        public void wyrb() { MagS.SetValue(RB.GetValue()); }
        public void werb() { RB.SetValue(MagS.GetValue()); }
        public void wyg() { MagS.SetValue(G.GetValue()); }
        public void start() { IOController.HandleIOOnStartSignal(I.GetArgument()); } 
        public void wyrm() { MagA.SetValue(RM.GetValue()); }
        public void werm() { RM.SetValue(MagA.GetValue()); }
        public void wyap() { MagA.SetValue(AP.GetValue()); }
        public void rint() { IntController.ClearMSBOfAcceptedINTs(); }
        public void eni()  { IntController.SetAcceptedAndINTVectorRegister(); JAL.SetFlags(ALUFlags.INT); }

        // JAL
        // Architecture W
        public void przep() { JAL.Nop(); }
        public void dod() { JAL.Add(); }
        public void ode() { JAL.Sub(); }
        public void weak() { JAL.SetResultAndFlags(); }
        public void weja() { JAL.SetOperandB(MagS.GetValue()); }
        public void wyak() { MagS.SetValue(AK.GetValue()); }

        // Architecture L

        public void iak() { JAL.Inc(); }
        public void dak() { JAL.Dec(); }
        public void mno() { JAL.Mul(); }
        public void dziel() { JAL.Div(); }
        public void shr() { JAL.Shr(); }
        public void neg() { JAL.Not(); }
        public void lub() { JAL.Or(); }
        public void i() { JAL.And(); }

        void InitialazeMicroinstructionsMap() {
            var AllPLSignalsMap = new Dictionary<string, Action> {
                {"czyt",czyt},{"wyad",wyad},{"pisz",pisz},{"przep",przep},{"wys",wys},{"dod",dod},{"wes",wes},{"ode",ode},{"wei",wei},{"weak",weak},
                { "il", il },{ "weja", weja },{ "wyl", wyl },{ "wyak", wyak },{"wea",wea},{"wel",wel},{"stop",stop},{"as",_as}, {"sa", sa},{"iak",iak},
                {"dak",dak},{"mno",mno}, {"dziel",dziel},{"shr",shr},{"neg",neg},{"lub",lub},{"i",i},{"wyx",wyx},{"wex",wex},{"wyy",wyy},{"wey",wey},
                {"wyws",wyws},{"wews",wews},{"iws",iws},{"dws",dws},{"wyrb",wyrb},{"werb",werb},{"wyg",wyg},{"start",start},{"wyrm",wyrm},{"werm",werm},
                {"wyap",wyap},{"rint",rint },{"eni",eni}
            };
            //var AllENGSignalsMap = new Dictionary<string, Action> {
            //     {"rd",czyt},{"oa",wyad},{"wr",pisz},{"wracc",przep},{"od",wys},{"add",dod},{"id",wes},{"sub",ode},{"iins",wei},{"wracc",weak},
            //    { "it", il },{ "ialu", weja },{ "oit", wyl },{ "oacc", wyak },{"wea",wea},{"wel",wel},{"stop",stop},{"as",_as}, {"sa", sa},{"iak",iak},
            //    {"dak",dak},{"mno",mno}, {"dziel",dziel},{"shr",shr},{"neg",neg},{"lub",lub},{"i",i},{"wyx",wyx},{"wex",wex},{"wyy",wyy},{"wey",wey},
            //    {"wyws",wyws},{"wews",wews},{"iws",iws},{"dws",dws},{"wyrb",wyrb},{"werb",werb},{"wyg",wyg},{"start",start}
            //}
            //SignalsMap = AllSignalsMap
            //    .Where(item => ArchitectureSettings.GetAvaibleSignals().Contains(item.Key))
            //    .ToDictionary(item => item.Key, item => item.Value);
            SignalsMap = AllPLSignalsMap;
        }


        // =========================< UI Related Actions > ================================== // 
        // |Part which needs to be changed if another technology of UI creation is preffered|

        public Action OnRefreshValues;
        public Action<uint> OnSetExecutedLine;
        public Action<uint, List<string>> OnSetExecutedMicroinstruction;
        public Action OnProgramEnd;

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

        // Returns false if the instruction completion signal is hit (STATEMENT_END)
 // Parameter "i" controlls which point of instruction execution should be performed (start from 0 if ticks controlled manually, from 1 if called from ExecuteInstructionCycle() method)
        int ExecuteTick(int i=INSTRUCTION_FETCH_ORDER) {
            ////===?| DEBUGGING
            //string state = String.Format("| A:{0} | S:{1} | L:{2} | I:{3} | MagA:{4} | MagS:{5} | AK:{6} |", A.GetValue(), S.GetValue(), L.GetValue(), I.GetValue(), MagA.LoggerGet(), MagS.LoggerGet()); /// AK.GetValue()
            //Logger.Logger.Div(NL: true);
            //Logger.Logger.LogInfo(msg:state,NL:true);
            //Logger.Logger.LogInfo(msg:string.Join(" ", ActiveSignals));
            ////===?| DEBUGGING
            int ticksNum = RzKDecoder.GetNumberOfTicksInInstruction(I.GetOpcode());
            if (i > ticksNum) i %= ticksNum; // Protection from manual tick execution

            SetExecutedLineInEditor(L.GetValue()-1); //select currently executed instruction on code editor (DEBUGGER)

            //if (i == INSTRUCTION_FETCH_ORDER) { // Not neccesary if? DecodeActiveSignals will fetch czyt;wys;wei;il whatsoever (param i)?
            //    FetchInstruction(); // If tick called not from ExecuteInstructionCycle() method
            //} else {
            i = RzKDecoder.GetJumpIndex(tick: i);
            ActiveSignals = RzKDecoder.DecodeActiveSignals(instructionOpcode: I.GetOpcode(), tick: i);
            //}
            
            // [ TODO: Check if there is value to be stored in RB register from any IO device ]
            foreach (string signal in ActiveSignals) {
                if (signal.Equals(Defines.SIGNAL_STATEMENT_END)) return -1;
                if (SignalsMap.ContainsKey(signal)) //skips conditional statements 
                    SignalsMap[signal]();
            }
            MagA.SetEmpty(); MagS.SetEmpty(); //Buses no longer sustain last state (MUST BE AFTER INSTRUCTION FETCH CYCLE)
            
            RefreshValues();
            SetExecutedMicroinstructions();
            return i;
        }

        //TODO: Add parameter that specifies if last tick was forced by user and substract LastTick from ticksNum
        void ExecuteInstructionCycle(bool wasForcedTick=false) {
            int uInstructionBlock = INSTRUCTION_FETCH_ORDER;

            FetchInstruction();
            ExecuteTick();
            uInstructionBlock++;

            uint opcode = I.GetOpcode();
            int requiredTicks = RzKDecoder.GetNumberOfTicksInInstruction(opcode);

            if (wasForcedTick && LastTick>0) requiredTicks = requiredTicks - LastTick;
            // here if instruction microdoce is broken, machine can enter infinite loop -> can add "watchdog" that stops programm after X non-break iterations
            for (int i = uInstructionBlock; i < requiredTicks; i++) {
                i = ExecuteTick(i);
                LastTick = i;
                if (i<0) break;
            }
        }
        //========================================
        void ExecuteProgram() {
            //MaszynaPi.Logger.Logger.EnableFileLog(additionalName: "_Program_Execution_Logs");
            try { do {
                    //System.Threading.Thread.Sleep(1000);
                    ExecuteInstructionCycle(); 
                } while (I.GetOpcode() != 0);
            } //here also can add watchdog if there is no STP instruction in programm 
            catch (BusException ex) { throw new CentralUnitException(ex.Message + ". Licznik intrukcji-1: (" + (L.GetValue() - 1).ToString() + ") linia: " + string.Join(" ", ActiveSignals)); } 
            catch (Exception ex) { throw new CentralUnitException("[Program error] " + ex.GetType().ToString() + ". Licznik intrukcji-1: (" + (L.GetValue() - 1).ToString() + ") linia: " + string.Join(" ", ActiveSignals) + "| " + ex.Message); }

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
        public void ManualInstruction() { ExecuteInstructionCycle(wasForcedTick: false); ProgramEnd(); } // Not avaible if ManualControl signal active
        public void ManualProgram() {ExecuteProgram(); ProgramEnd(); } // Not avaible if ManualControl signal active

        public List<char> GetTextInputBufferHandle() { return TextInput.GetCharactersBufferHandle();  }
        public void SetOnFetchCharAction(Action characterFetched) { TextInput.OnCharacterFetched = characterFetched;}
        public List<char> GetTextOutputBufferHandle() { return TextOutput.GetCharactersBufferHandle(); }

        public void SetOnPushCharAction(Action characterPushed) { TextOutput.OnCharacterPushed = characterPushed; }

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
