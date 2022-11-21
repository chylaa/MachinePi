using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace MaszynaPi.MachineLogic {

    static class ArchitectureSettings {
        static uint AddressSpace = Defines.DEFAULT_ADDR_BITS;
        static uint CodeBits = Defines.DEFAULT_CODE_BITS;
        static Defines.Machines CurrentArchitecture = Defines.DEFAULT_MACHINE;
        static Defines.Components ActiveComponents = (Defines.Components)Defines.DEFAULT_MACHINE;
        static List<string> AvaibleSignals = new List<string>();

        // Methods - ----------------------------------------------------------------------
        public static uint GetAddressSpace() { return AddressSpace; }
        public static void SetAddressSpace(uint newAddressSpace) {
            if (newAddressSpace > Defines.ADDRESS_BITS_MAX || newAddressSpace < Defines.ADDRESS_BITS_MIN)
                throw new CentralUnitException("Invalid addresss bits size: " + newAddressSpace.ToString());
            AddressSpace = newAddressSpace;
        }

        public static uint GetCodeBits() { return CodeBits; }
        public static void SetCodeBits(uint newCodeBits) {
            if (newCodeBits > Defines.CODE_BITS_MAX || newCodeBits < Defines.CODE_BITS_MIN)
                throw new CentralUnitException("Invalid code bits size: " + newCodeBits.ToString());
            CodeBits = newCodeBits;
        }

        public static uint GetWordBits() { return CodeBits + AddressSpace; }
        public static uint GetMaxWord() {
            return (uint)Math.Pow(2, AddressSpace+CodeBits)-1;
        }

        public static uint GetMaxAddress() {
            return (uint)Math.Pow(2, AddressSpace) - 1;
        }
        public static uint GetMaxOpcode() {
            return (uint)Math.Pow(2, CodeBits) - 1;
        }
        public static uint ShrinkToWordLength(uint value) {
            return Math.Min(value, GetMaxWord());
        }

        public static void SetActiveComponents(Defines.Components active) { ActiveComponents = active; }
        public static Defines.Components GetActiveComponents() { return ActiveComponents; }

        public static List<string> GetAvaibleSignals() { return AvaibleSignals; }
        public static void SetAvaibleSignals(Defines.Components active) {
            SetActiveComponents(active);
            AvaibleSignals.Clear();
            for (int i = 0; i < Defines.Signals.Count; i <<= 1) {
                if (ActiveComponents.HasFlag((Defines.Components)i))
                    AvaibleSignals.AddRange(Defines.Signals[i]);
            }

        }

        // Returns names of avaible instructions considering the currently set code bits value
        public static List<string> GetAvaibleInstructions() {
            var allInstructions = MachineAssembler.InstructionLoader.GetAvaibleInstructionsNames();
            int visible = Math.Min(allInstructions.Count, (int)GetMaxOpcode() + 1);
            return allInstructions.GetRange(0,visible);
        }

        public static Defines.Machines GetArchitecture() { return CurrentArchitecture; }
        public static void SetArchitecture(Defines.Machines machine) {
            CurrentArchitecture = machine;
            SetAvaibleSignals((Defines.Components)machine);

        }

        // Returns number of avaible IO devices based on current architecture
        public static int GetNumberOfIODevices() {
            if (ActiveComponents.HasFlag(Defines.Components.ExtendedIO)) return Defines.DEFAULT_IO_NUMBER + Defines.EXTENDED_IO_NUMBER;
            return Defines.DEFAULT_IO_NUMBER;
        }

        // Returns how many bits there are needed to encode all IO Devices addresses
        public static uint GetAddressSpaceForIO() {
            int numOfIOs = GetNumberOfIODevices();
            return (uint)Math.Ceiling(Math.Log(a: numOfIOs, newBase: 2));
        }

    }
        
}
