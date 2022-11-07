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
        public Bus MagA { get; }
        public Bus MagS { get; } // BUSes
        public ArithmeticLogicUnit JAL { get; }
        public Register A { get; }
        public Register S {get;}
        public Register AK {get;} // Address Register, Value Register, Accumulator
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
        public void wyad() { MagA.SetValue(I.getArgument()); }

        public void przep() { JAL.Nop(); }
        public void dod() { JAL.Add(); }
        public void ode() { JAL.Sub(); }
        public void weak() { JAL.SetResult(); }
        public void weja() { JAL.SetOperandB(MagS.GetValue()); }
        public void wyak() { MagS.SetValue(AK.Value); }
        // . . . TODO

        // ========================= <  Execution Methods > =================================== //
        void InitialazeSignalsMap() {
            var AllSignalsMap = new Dictionary<string, Action> {
                { "czyt", czyt },{ "wyad", wyad },{ "pisz", pisz },{ "przep", przep },
                { "wys", wys },{ "dod", dod },{ "wes", wes },{ "ode", ode },
                { "wei", wei },{ "weak", weak },{ "il", il },{ "weja", weja },
                { "wyl", wyl },{ "wyak", wyak },{ "wel", wel }
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

        private string GetSignalStatementArgument(List<string> signals) {
            //return signals.fin
            return "";
        }

        //===========< Machine Cycle >============
        void LoadInstruction() {
            ActiveSignals = new List<string> { "czyt", "wys", "wei", "il" };
            ExecuteTick();
        }

        void ExecuteTick() {
            MagA.SetEmpty(); MagS.SetEmpty();
            foreach (string signal in ActiveSignals) 
                SignalsMap[signal]();
        }

        void ExecuteInstruction() {
            LoadInstruction();
            List<List<string>> instructionSignals = InstructionMap[I.getOpcode()]; // Tym musiała by zająć się klasa reprezentująca Dekoder Instrukcji

            foreach (var signals in instructionSignals) {
                if (signals.Any(signal => signal.Contains(Defines.SIGNAL_LABEL))) {

                }
                ActiveSignals = new List<string>(signals);
                ExecuteTick();
            }
        }
        //========================================
        void ExecuteProgram() {
            try { do { ExecuteInstruction(); } while (I.getOpcode() != 0); } 
            catch (BusException ex) { throw new CentralUnitException(ex.Message); }
            catch (Exception ex) { throw new CentralUnitException("[Program error] " + ex.GetType().ToString() + ". Instruction "+I.getOpcode().ToString()+". " + ex.Message); }
        }

        // ======================= <  User Interface Methods > ================================= //
        public void SetMemoryContent(uint addr, uint value) { PaO.StoreValue(addr, value); }
        public void SetMemoryContent(List<uint> values, uint offset=0) { for (uint i = offset; i < values.Count; i++) PaO.StoreValue(i, values[(int)i]); }
        public uint GetMemoryContent(uint addr) { return PaO.GetValue(addr); }
        public List<uint> GetMemoryContent(uint addr, uint size) { return PaO.GetMemoryContent().GetRange((int)addr, (int)size); }
        public List<uint> GetWholeMemoryContent() { return PaO.GetMemoryContent(); }
        public void ExpandMemory(uint oldAddrSpace) { PaO.ExpandMemory(oldAddrSpace); }
        public void ExpandAndClearMemory() { PaO.InitMemoryContent(); }

        public void AddActiveSignals(List<string> handActivatedSignals) { ActiveSignals.AddRange(handActivatedSignals); }
        public void SetActiveSignals(List<string> handActivatedSignals) { ActiveSignals = new List<string>(handActivatedSignals); }

        public void ManualTick() { ExecuteTick(); }
        public void ManualInstruction() { ExecuteInstruction(); } // Not avaible if ManualControl signal active
        public void ManualProgram() {ExecuteProgram(); } // Not avaible if ManualControl signal active


    }

}
