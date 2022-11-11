using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.MachineLogic.Architecture;
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
        //protected static uint AddressSpace = Defines.DEFAULT_ADDR_BITS;
        //protected static uint CodeBits = Defines.DEFAULT_CODE_BITS;

        // Always initialized when creating CentralUnit child class
        Dictionary<string, Action> SignalsMap;
        
        
        //For Central Unit View of signals
        List<string> ActiveSignals;

        // Compiler: "INSTRUCTION ARG" -> opcode&arg -> .hex to Memory
        // CentralUnit: MachineCycle: LoadInstruction(Czyt wys we il) DecodeInstruction(), ExecuteInstruction() -> 
        // CentralUnit: GetOpcode() from IR -> map{Opcode : Ticks} (ticks as List<List<string>>) -> perform Instruction (as ticks)

        // U tutka assemblacją (wykonaniem programu) zajmuje się chyba osobny obiekt "Assembler" -> stworzyć taki (AssemblerUnit?)
        //                                                                          i wrzucić jako atrybut CentralUnit???

        // Others internal Components
        private InstructionDecoder RzKDecoder;
        // Components visible in architecture view
        public Memory PaO { get; private set; } // Operation Memory ("FLash"?)
        public Bus MagA { get; private set; } // BUS
        public Bus MagS { get; private set; } // BUS
        public ArithmeticLogicUnit JAL { get; private set; }
        public Register A { get; private set; }  // Address Register
        public Register S {get; private set; } // Value Register
        public Register AK { get; private set; }  // Accumulator
        public Register L { get; private set; }   //Instruction Pointer
        public InstructionRegister I { get; private set; }   // Instruction Register
        public Register X { get; private set; }
        public Register Y { get; private set; }

        public ControlUnit() {
            RzKDecoder = new InstructionDecoder();
            RzKDecoder.OnRequestALUFlagState += new Func<string,bool>(delegate { return JAL.IsFlagSet(RzKDecoder.StatementArg); });

            uint Aspace = ArchitectureSettings.GetAddressSpace();
            uint Cbits = ArchitectureSettings.GetCodeBits();
            uint Mword = ArchitectureSettings.GetWordBits();
            // Atchitecture W
            PaO = new Memory();
            AK = new Register(Mword);
            A = new Register(Aspace);
            S = new Register(Mword);
            L = new Register(Mword);
            I = new InstructionRegister(Aspace,Cbits);
            JAL = new ArithmeticLogicUnit(AK);
            MagA = new Bus(Mword);
            MagS = new Bus(Mword);
            // Architecture L
            X = new Register(Mword);
            Y = new Register(Mword);
            // Architecture EW

            InitialazeMicroinstructionsMap();
        }

        // ========================== <  Signals Methods > ========--=========================== // (Microinstructions)
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
        
        public void przep() { JAL.Nop(); }
        public void dod() { JAL.Add(); }
        public void ode() { JAL.Sub(); }
        public void weak() { JAL.SetResultAndFlags(); }
        public void weja() { JAL.SetOperandB(MagS.GetValue()); }
        public void wyak() { MagS.SetValue(AK.GetValue()); }

        public void stop() { return; }

        // Architecture W+
        public void _as() { MagS.SetValue(MagA.GetValue()); }
        public void sa() { MagA.SetValue(MagS.GetValue()); }

        // Architecture EW
        // . . . TODO

        

        // ========================= <  Execution Methods (Instruction Decoder?) > =================================== //
        void InitialazeMicroinstructionsMap() {
            var AllSignalsMap = new Dictionary<string, Action> {
                { "czyt", czyt },{ "wyad", wyad },{ "pisz", pisz },{ "przep", przep },
                { "wys", wys },{ "dod", dod },{ "wes", wes },{ "ode", ode },{ "wei", wei },
                { "weak", weak },{ "il", il },{ "weja", weja },{ "wyl", wyl },
                { "wyak", wyak },{"wea",wea},{ "wel", wel }, {"stop",stop },
                { "as", _as}, { "sa", sa}
            };
            //SignalsMap = AllSignalsMap
            //    .Where(item => ArchitectureSettings.GetAvaibleSignals().Contains(item.Key))
            //    .ToDictionary(item => item.Key, item => item.Value);
            SignalsMap = AllSignalsMap;
        }



        // ========================= <  Machine Cycle > =================================== //
        void FetchInstruction() {
            ActiveSignals = new List<string> { "czyt", "wys", "wei", "il" };
            ExecuteTick();
        }

        // Returns false if the instruction completion signal is hit (STATEMENT_END)
        bool ExecuteTick() {
            //===?| DEBUGGING
            string state = String.Format("| A:{0} | S:{1} | L:{2} | I:{3} | MagA:{4} | MagS:{5} | AK:{6} |", A.GetValue(), S.GetValue(), L.GetValue(), I.GetValue(), MagA.GetValue(), MagS.GetValue(), AK.GetValue());
            Logger.Logger.Div(NL: true);
            Logger.Logger.LogInfo(msg:state,NL:true);
            Logger.Logger.LogInfo(msg:string.Join(" ", ActiveSignals));
            //===?| DEBUGGING
            MagA.SetEmpty(); MagS.SetEmpty();
            foreach (string signal in ActiveSignals) {
                if (signal.Equals(Defines.SIGNAL_STATEMENT_END)) return false;
                if (SignalsMap.ContainsKey(signal)) //skips conditional statements 
                    SignalsMap[signal]();
            }
            return true;
        }

        void ExecuteInstructionCycle() {
            const int INSTRUCTION_LD_SKIP = 1; // skipping czt,wys,wei,il because it is forced by LoadInstrucion() method on begining of each cycle
            
            FetchInstruction();
            uint opcode = I.GetOpcode();
            int ticksNum = RzKDecoder.GetNumberOfTicksInInstruction(opcode);
            // here if instruction microdoce is broken, machine can enter infinite loop -> can add "watchdog" that stops programm after X non-break iterations
            for (int i = INSTRUCTION_LD_SKIP; i < ticksNum; i++) {
                i = RzKDecoder.GetJumpIndex(tick:i);
                ActiveSignals = RzKDecoder.DecodeActiveSignals(instructionOpcode:I.GetOpcode(), tick:i);
                if (!ExecuteTick()) break;
            }
        }
        //========================================
        void ExecuteProgram() {
            MaszynaPi.Logger.Logger.EnableFileLog(additionalName:"_Program_Execution_Logs");
            try { do { ExecuteInstructionCycle(); } while (I.GetOpcode() != 0); } //here also can add watchdog if there is no STP instruction in programm 
            catch (BusException ex) { throw new CentralUnitException(ex.Message + ". Licznik intrukcji-1: ("+ (L.GetValue()-1).ToString()+") linia: "+string.Join(" ",ActiveSignals)); }
            catch (Exception ex) { throw new CentralUnitException("[Program error] " + ex.GetType().ToString() + ". Licznik intrukcji-1: (" + (L.GetValue() - 1).ToString() + ") linia: " + string.Join(" ", ActiveSignals)+ "| " + ex.Message); }
            
        }
        //========================================
        public void ResetRegisters() {
            AK.SetValue(Defines.DEFAULT_ALU_VAL);
            A.SetValue(Defines.DEFAULT_REG_VAL);
            S.SetValue(Defines.DEFAULT_REG_VAL);
            L.SetValue(Defines.DEFAULT_REG_VAL);
            I.SetValue(Defines.DEFAULT_REG_VAL);
            JAL.Reset();
            MagA.SetValue(Defines.DEFAULT_BUS_VAL);
            MagS.SetValue(Defines.DEFAULT_BUS_VAL);
        }


        // ======================= <  User Interface Methods > ================================= //
        public void SetMemoryContent(uint addr, uint value) { PaO.StoreValue(addr, value); }
        public void SetMemoryContent(List<uint> values, uint offset=0) { for (uint i = offset; i < values.Count; i++) PaO.StoreValue(i, values[(int)i]); }
        public uint GetMemoryContent(uint addr) { return PaO.GetValue(addr); }
        public List<uint> GetMemoryContent(uint addr, uint size) { return PaO.GetMemoryContent().GetRange((int)addr, (int)size); }
        public List<uint> GetWholeMemoryContent() { return PaO.GetMemoryContent(); }
        public void ExpandMemory(uint oldAddrSpace) { PaO.ExpandMemory(oldAddrSpace); }
        public void ResetMemory() { PaO.InitMemoryContent(); }

        public void AddActiveSignals(List<string> handActivatedSignals) { ActiveSignals.AddRange(handActivatedSignals); }
        public void SetActiveSignals(List<string> handActivatedSignals) { ActiveSignals = new List<string>(handActivatedSignals); }

        public void ManualTick() { ExecuteTick(); }
        public void ManualInstruction() { ExecuteInstructionCycle(); } // Not avaible if ManualControl signal active
        public void ManualProgram() {ExecuteProgram(); } // Not avaible if ManualControl signal active


    }

}
