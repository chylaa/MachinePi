using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.MachineLogic;
using MaszynaPi.MachineLogic.Architecture;
/*
namespace MaszynaPi.MachineLogic.Machines {
    class MachineW : CentralUnit {
        Memory PaO; // Operation Memory ("FLash"?)
        Bus MagA, MagS; // BUSes
        ArithmeticLogicUnit JAL;
        Register A, S, AK; // Address Register, Value Register, Accumulator
        Register L; //Instruction Pointer
        InstructionRegister I; // Instruction Register

        Dictionary<string, Action> SignalsMap;
        Dictionary<uint, List<List<string>>> InstructionMap; //(opcode: list of ticks) -> ticks = list of signals

        public MachineW(uint addressSpace = Defines.DEFAULT_ADDR_BITS, uint codeBits = Defines.DEFAULT_CODE_BITS) {
            SetAddressSpace(addressSpace);
            SetCodeBits(codeBits);
            PaO = new Memory(addressSpace);
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
        // ========================== <  Signals Methods > ========--=========================== //

        public override void czyt() { S.Value = PaO.GetValue(A.Value); }
        public override void pisz() { PaO.StoreValue(A.Value, S.Value); }
        public override void wys() { MagS.Value = S.Value; }
        public override void wes() { S.Value = MagS.Value; }
        public override void wei() { I.Value = MagA.Value; }
        public override void il() { L.Value++; }
        public override void wyl() { MagA.Value = L.Value; }
        public override void wel() { L.Value = MagA.Value; }
        public override void wyad() { MagA.Value = I.getArgument(); }

        public override void przep() { JAL.Nop(); }
        public override void dod() { JAL.Add(); }
        public override void ode() { JAL.Sub(); }
        public override void weak() { JAL.SetResult(); }
        public override void weja() { JAL.SetOperandB(MagS.Value); }
        public override void wyak() { MagS.Value = AK.Value; }

        // ========================= <  Execution Methods > =================================== //
        void InitialazeSignalsMap() {
             var AllSignalsMap = new Dictionary<string, Action> {
                { "czyt", czyt },
                { "wyad", wyad },
                { "pisz", pisz },
                { "przep", przep },
                { "wys", wys },
                { "dod", dod },
                { "wes", wes },
                { "ode", ode },
                { "wei", wei },
                { "weak", weak },
                { "il", il },
                { "weja", weja },
                { "wyl", wyl },
                { "wyak", wyak },
                { "wel", wel }
            };
            SignalsMap = AllSignalsMap
                .Where(item => ArchitectureSettings.GetAvaibleSignals().Contains(item.Key))
                .ToDictionary(item => item.Key, item => item.Value);
        }

        public void LoadInstructionMap() {
            InstructionMap = new Dictionary<uint, List<List<string>>>();
            MachineAssembler.Decoders.InstructionSetDecoder.
        }
        


    }


}
/*