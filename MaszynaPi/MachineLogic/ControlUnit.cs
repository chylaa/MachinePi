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
        //Loaded from .lst file
        Dictionary<uint, List<List<string>>> InstructionMap; //(opcode: list of ticks) -> ticks = list of signals
        
        //For Central Unit View of signals
        List<string> ActiveSignals;

        // Compiler: "INSTRUCTION ARG" -> opcode&arg -> .hex to Memory
        // CentralUnit: MachineCycle: LoadInstruction(Czyt wys we il) DecodeInstruction(), ExecuteInstruction() -> 
        // CentralUnit: GetOpcode() from IR -> map{Opcode : Ticks} (ticks as List<List<string>>) -> perform Instruction (as ticks)

        // U tutka assemblacją (wykonaniem programu) zajmuje się chyba osobny obiekt "Assembler" -> stworzyć taki (AssemblerUnit?)
        //                                                                          i wrzucić jako atrybut CentralUnit???

        public Memory PaO { get; } // Operation Memory ("FLash"?)
        public Bus MagA { get; } // BUS
        public Bus MagS { get; } // BUS
        public ArithmeticLogicUnit JAL { get; }
        public Register A { get; }  // Address Register
        public Register S {get; } // Value Register
        public Register AK {get;}  // Accumulator
        public Register L; //Instruction Pointer
        public InstructionRegister I; // Instruction Register

        public ControlUnit() {
            PaO = new Memory();
            AK = new Register();
            A = new Register();
            S = new Register();
            L = new Register();
            I = new InstructionRegister();
            JAL = new ArithmeticLogicUnit(AK);
            MagA = new Bus();
            MagS = new Bus();

            InitialazeSignalsMap();
            LoadInstructionMap();
        }

        // ========================== <  Signals Methods > ========--=========================== // (Microinstructions)
        public void czyt() { S.Value = PaO.GetValue(A.Value); }
        public void pisz() { PaO.StoreValue(A.Value, S.Value); }
        public void wys() { MagS.SetValue(S.Value); }
        public void wes() { S.Value = MagS.GetValue(); }
        public void wei() { I.Value = MagS.GetValue(); I.DecodeInstruction(); }
        public void il() { L.Value++; }
        public void wyl() { MagA.SetValue(L.Value); }
        public void wel() { L.Value = MagA.GetValue(); }
        public void wyad() { MagA.SetValue(I.GetArgument()); }
        public void wea() { A.Value = MagA.GetValue(); }
        
        public void przep() { JAL.Nop(); }
        public void dod() { JAL.Add(); }
        public void ode() { JAL.Sub(); }
        public void weak() { JAL.SetResultAndFlags(); }
        public void weja() { JAL.SetOperandB(MagS.GetValue()); }
        public void wyak() { MagS.SetValue(AK.Value); }

        public void stop() { return; }
        // . . . TODO

        

        // ========================= <  Execution Methods > =================================== //
        void InitialazeSignalsMap() {
            var AllSignalsMap = new Dictionary<string, Action> {
                { "czyt", czyt },{ "wyad", wyad },{ "pisz", pisz },{ "przep", przep },
                { "wys", wys },{ "dod", dod },{ "wes", wes },{ "ode", ode },{ "wei", wei },
                { "weak", weak },{ "il", il },{ "weja", weja },{ "wyl", wyl },
                { "wyak", wyak },{"wea",wea},{ "wel", wel }, {"stop",stop }
            };
            //SignalsMap = AllSignalsMap
            //    .Where(item => ArchitectureSettings.GetAvaibleSignals().Contains(item.Key))
            //    .ToDictionary(item => item.Key, item => item.Value);
            SignalsMap = AllSignalsMap;
        }

        public void LoadInstructionMap() {
            InstructionMap = MachineAssembler.FilesHandling.InstructionLoader.GetInstructionSignalsMap();
            //MachineAssembler.Decoders.InstructionSetDecoder.
        }

        //Returns empty string if statement not foud (line just starts with label)
        private string GetStatementJumpLabel(bool conditionalStatementResult, List<string> signals) {
            if (conditionalStatementResult) {
                int argPos = signals.IndexOf(Defines.SIGNAL_STATEMENT_THEN) + 1;
                if (argPos == 0 || argPos >= signals.Count) throw new CentralUnitException("No statement jump label in line " + string.Join(" ", signals));
                return signals[argPos];
            } else {
                int elsePos = signals.IndexOf(Defines.SIGNAL_STATEMENT_ELSE.Split(' ')[1]);
                if (elsePos > 0) return signals[elsePos + 1];
            }
            return "";
        }

        //Returns empty string if statement not foud (line just starts with label)
        private string GetSignalStatementArgument(List<string> signals) {
            int argPos = signals.IndexOf(Defines.SIGNAL_STATEMENT_IF) + 1;
            if (argPos == 0 || argPos >= signals.Count) return "";
            return signals[argPos];
        }

        private int GetJumpIndex(List<List<string>> instructionSignals, string label) {
            if (label.Equals("")) return -1;
            for(int i=0; i<instructionSignals.Count; i++) {
                if (instructionSignals[i][0].Equals(label))
                    return i;
            }
            return -1;
        }

        // ========================= <  Machine Cycle > =================================== //
        void LoadInstruction() {
            ActiveSignals = new List<string> { "czyt", "wys", "wei", "il" };
            ExecuteTick();
        }

        void ExecuteTick() {
            //===?| DEBUGGING
            string state = String.Format("| A:{0} | S:{1} | L:{2} | I:{3} | MagA:{4} | MagS:{5} | AK:{6} |", A.Value, S.Value, L.Value, I.Value, MagA.Value, MagS.Value, AK.Value);
            Logger.Logger.Div(NL: true);
            Logger.Logger.LogInfo(msg:state,NL:true);
            Logger.Logger.LogInfo(msg:string.Join(" ", ActiveSignals));
            //===?| DEBUGGING
            MagA.SetEmpty(); MagS.SetEmpty();
            foreach (string signal in ActiveSignals) 
                if(SignalsMap.ContainsKey(signal)) //skips conditional statements 
                    SignalsMap[signal]();
        }

        void ExecuteInstruction() {
            LoadInstruction();
            List<List<string>> instructionSignals = InstructionMap[I.GetOpcode()];

            int INSTRUCTION_LD_SKIP = 1; // skipping czt,wys,wei,il because it is forced by LoadInstrucion() method on begining of each cycle

            string statementArg = "";
            string jumpLabel = "";
            int jumpIndex = -1;
// here if instruction microdoce is broken, machine can enter infinite loop -> can add "watchdog" that stops programm after X non-break iterations
            for (int i = INSTRUCTION_LD_SKIP; i < instructionSignals.Count; i++) { 
                if (jumpIndex > -1){
                    i = jumpIndex;
                    jumpIndex = -1;
                }
                if (instructionSignals[i].Any(signal => signal.Contains(Defines.SIGNAL_STATEMENT_IF))) {
                    statementArg = GetSignalStatementArgument(instructionSignals[i]);
                    jumpLabel = GetStatementJumpLabel(JAL.IsFlagSet(statementArg), instructionSignals[i]);
                    jumpIndex = GetJumpIndex(instructionSignals, jumpLabel);
                }
                ActiveSignals = new List<string>(instructionSignals[i]);
                ExecuteTick();
                if (instructionSignals[i].Contains(Defines.SIGNAL_STATEMENT_END)) break;
            }
        }
        //========================================
        void ExecuteProgram() {
            MaszynaPi.Logger.Logger.EnableFileLog(additionalName:"_Program_Execution_Logs");
            try { do { ExecuteInstruction(); } while (I.GetOpcode() != 0); } //here also can add watchdog if there is no STP instruction in programm 
            catch (BusException ex) { throw new CentralUnitException(ex.Message + ". Licznik intrukcji-1: ("+ (L.Value-1).ToString()+") linia: "+string.Join(" ",ActiveSignals)); }
            catch (Exception ex) { throw new CentralUnitException("[Program error] " + ex.GetType().ToString() + ". Licznik intrukcji-1: (" + (L.Value - 1).ToString() + ") linia: " + string.Join(" ", ActiveSignals)+ "| " + ex.Message); }
            
        }
        //========================================
        public void ResetRegisters() {
            AK.Value = Defines.DEFAULT_ALU_VAL;
            A.Value = Defines.DEFAULT_REG_VAL;
            S.Value = Defines.DEFAULT_REG_VAL;
            L.Value = Defines.DEFAULT_REG_VAL;
            I.Value = Defines.DEFAULT_REG_VAL;
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
        public void ManualInstruction() { ExecuteInstruction(); } // Not avaible if ManualControl signal active
        public void ManualProgram() {ExecuteProgram(); } // Not avaible if ManualControl signal active


    }

}
