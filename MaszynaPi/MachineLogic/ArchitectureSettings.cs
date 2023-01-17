using System;
using System.Collections.Generic;

namespace MaszynaPi.MachineLogic {

    static class ArchitectureSettings {
        static uint AddressSpace = Defines.DEFAULT_ADDR_BITS;
        static uint CodeBits = Defines.DEFAULT_CODE_BITS;
        static Defines.Architectures CurrentArchitecture = Defines.DEFAULT_ARCHITECTURE;
        static Defines.Components ActiveComponents = (Defines.Components)((int)Defines.DEFAULT_ARCHITECTURE);
        static List<string> AvaibleSignals = new List<string>();

        static Dictionary<uint, uint> InterruptVector = new Dictionary<uint, uint>(); // Pairs of <INT bit pos, Mem Address>
        static Dictionary<uint, uint> IODevices = new Dictionary<uint, uint>(); // Map of IO Devices by pair Address <-> DeviceID (Addresses set in Projekt->Opcje->Adresy)
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
            return (uint)Math.Pow(2, AddressSpace + CodeBits) - 1;
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
        public static void SetAvaibleSignals() {
            //SetActiveComponents(active);
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
            return allInstructions.GetRange(0, visible);
        }

        public static Defines.Architectures GetArchitecture() { return CurrentArchitecture; }
        public static void SetArchitecture(Defines.Architectures machine) {
            CurrentArchitecture = machine;
            //SetAvaibleSignals((Defines.Components)machine);

        }

        public static Dictionary<uint, uint> GetInterruptVector() {
            return InterruptVector;
        }

        public static void InitializeInterruptVector() {
            Dictionary<uint, uint> baseIntVect = new Dictionary<uint, uint>();
            for (uint bit = 0; bit < Defines.INTERRUPTIONS_NUM; bit++) {
                baseIntVect.Add(bit, Defines.INTERRUPTIONS_NUM - bit);
            }
            MaszynaPi.MachineLogic.ArchitectureSettings.SetInterruptVector(baseIntVect);
        }

        public static void SetInterruptVector(Dictionary<uint, uint> newVector) {
            InterruptVector = new Dictionary<uint, uint>(newVector);
        }


        // Returns number of avaible IO devices based on current architecture
        public static int GetNumberOfIODevices() {
            return Defines.DEFAULT_IO_NUMBER + Defines.EXTENDED_IO_NUMBER;
            //if (ActiveComponents.HasFlag(Defines.Components.ExtendedIO)) return Defines.DEFAULT_IO_NUMBER + Defines.EXTENDED_IO_NUMBER;
            //return Defines.DEFAULT_IO_NUMBER;
        }

        // Returns how many bits there are needed to encode all IO Devices addresses
        public static uint GetAddressSpaceForIO() {
            int numOfIOs = GetNumberOfIODevices();
            return (uint)Math.Ceiling(Math.Log(a: numOfIOs, newBase: 2));
        }

        public static uint GetIODeviceID(uint IOAddress) {
            if (IODevices.ContainsKey(IOAddress) == false) throw new Exception("No Input/Output Device with address: {" + IOAddress.ToString() + "}");
            return IODevices[IOAddress];
        }

        public static void InitializeIODevicesAddresses() {
            Dictionary<uint, uint> baseIOAddr = new Dictionary<uint, uint>();
            for (uint i = 1; i <= (Defines.EXTENDED_IO_NUMBER+Defines.DEFAULT_IO_NUMBER); i++) {
                baseIOAddr.Add(i, i);
            }
            SetIODevicesAddresses(baseIOAddr);
        }

        public static void SetIODevicesAddresses(Dictionary<uint, uint> DevicesAddresses) {
            IODevices.Clear();
            foreach (var pair in DevicesAddresses)
                IODevices.Add(pair.Key, pair.Value);
        }
            

    }
        
}
