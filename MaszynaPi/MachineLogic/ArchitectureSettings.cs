using System;
using System.Collections.Generic;

namespace MaszynaPi.MachineLogic {

    /// <summary>
    /// Static class storing machine architecture-related information and allowing to modify 
    /// (for example from User Interface) and retreive them via set provided methods.
    /// </summary>
    static class ArchitectureSettings {

        #region Fields
        /// <summary> Current address space size in bits. </summary>
        static uint AddressSpace = Defines.DEFAULT_ADDR_BITS;
        /// <summary> Current opcode size in bits. </summary>
        static uint CodeBits = Defines.DEFAULT_CODE_BITS;
        
        /// <summary> Current <see cref="Defines.Architecture"/> of machine. </summary>
        static Defines.Architecture CurrentArchitecture = Defines.DEFAULT_ARCHITECTURE;
        /// <summary> Encoded active <see cref="Defines.Components"/> of current <see cref="Defines.Architecture"/> of machine. </summary>
        static Defines.Components ActiveComponents = (Defines.Components)((int)Defines.DEFAULT_ARCHITECTURE);

        /// <summary> List of microoperations available to use in currently selected architecture. </summary>
        static readonly List<string> AvailableSignals = new List<string>();

        /// <summary>Data structure mapping interruption-specific bits (<see cref="Dictionary{TKey, TValue}.Keys"/>) into memory adresses (<see cref="Dictionary{TKey, TValue}.Values"/>).</summary>
        static readonly Dictionary<uint, uint> InterruptVector = new Dictionary<uint, uint>(); 
        /// <summary>Data structure mapping addresses of IO devices (<see cref="Dictionary{TKey, TValue}.Keys"/>) into their's ID (<see cref="Dictionary{TKey, TValue}.Values"/>).</summary>
        static readonly Dictionary<uint, uint> IODevices = new Dictionary<uint, uint>();
        #endregion

        #region Machine WORD size

        /// <summary>
        /// Allows to set address space size in instructions (and therefore adress space for machine). <br></br>
        /// Throws <see cref="CentralUnitException"/> if passed value [<see cref="Defines.ADDRESS_BITS_MIN"/>;<see cref="Defines.ADDRESS_BITS_MAX"/>] range.
        /// </summary>
        /// <param name="newAddressSpace">New address size in bits.</param>
        /// <exception cref="CentralUnitException"></exception>
        public static void SetAddressSpace(uint newAddressSpace) {
            if (newAddressSpace > Defines.ADDRESS_BITS_MAX || newAddressSpace < Defines.ADDRESS_BITS_MIN)
                throw new CentralUnitException("Invalid addresss bits size: " + newAddressSpace.ToString());
            AddressSpace = newAddressSpace;
        }

        /// <summary>
        /// Allows to set instruction's opcode size. <br></br>
        /// Throws <see cref="CentralUnitException"/> if passed value not in [<see cref="Defines.CODE_BITS_MIN"/>;<see cref="Defines.CODE_BITS_MAX"/>] range.
        /// </summary>
        /// <param name="newCodeBits"></param>
        /// <exception cref="CentralUnitException"></exception>
        public static void SetCodeBits(uint newCodeBits) {
            if (newCodeBits > Defines.CODE_BITS_MAX || newCodeBits < Defines.CODE_BITS_MIN)
                throw new CentralUnitException("Invalid code bits size: " + newCodeBits.ToString());
            CodeBits = newCodeBits;
        }

        /// <summary><see cref="AddressSpace"/> getter. </summary>
        /// <returns>Currently set <see cref="AddressSpace"/> value.</returns>
        public static uint GetAddressSpace() => AddressSpace;

        /// <summary><see cref="CodeBits"/> getter. </summary>
        /// <returns>Currently set <see cref="CodeBits"/> value.</returns>
        public static uint GetCodeBits() => (CodeBits);

        /// <summary>Calculates current machine's WORD size in bits as sum of opcode+address sizes. </summary>
        /// <returns>Calculated WORD size in bits.</returns>
        public static uint GetWordBits() => (CodeBits + AddressSpace);

        /// <summary>Calculates max value that currently set WORD size can store wihout overflowing (unsigned value assumed). </summary>
        /// <returns>Maximum unsigned number possible to represent within single WORD of machine simulator.</returns>
        public static uint GetMaxWord() =>(uint)Math.Pow(2, AddressSpace + CodeBits) - 1;

        /// <summary>Calculates maximum memory adress that can be accessed using currently set <see cref="AddressSpace"/> size.</summary>
        /// <returns>Address of last memory location that can be adressed.</returns>
        public static uint GetMaxAddress() => (uint)Math.Pow(2, AddressSpace) - 1;

        /// <summary> Calculates maximum opcode, hence the maximum number of instructions that can be encoded.</summary>
        /// <returns>Max value that opcode segment of machine's WORD can represent.</returns>
        public static uint GetMaxOpcode() => (uint)Math.Pow(2, CodeBits) - 1;

        /// <summary> Shrinks passed <paramref name="value"/> to maximum size of machine's WORD. </summary>
        /// <param name="value"> Unsigned value to process</param>
        /// <returns>Minimum of <paramref name="value"/> and current maximum WORD.</returns>
        public static uint ShrinkToWordLength(uint value) => Math.Min(value, GetMaxWord());

        #endregion

        #region Architecure and it's Components

        /// <summary>Sets active <see cref="Defines.Components"/> of machine. </summary>
        /// <param name="active">Encoded components to be set as new active components of machine.</param>
        public static void SetActiveComponents(Defines.Components active) => ActiveComponents = active;

        /// <summary> Encoded active <see cref="Defines.Components"/> of current <see cref="Defines.Architecture"/> of machine. </summary>
        /// <returns><see cref="ActiveComponents"/></returns>
        public static Defines.Components GetActiveComponents() => ActiveComponents;

        /// <summary> List of microoperations available to use in currently selected architecture. </summary>
        /// <returns>List of currently <see cref="AvailableSignals"/> </returns>
        public static List<string> GetAvaibleSignals() => AvailableSignals;
        
        /// <summary>  Allows to set <see cref="AvailableSignals"/> base on previously set <see cref="ActiveComponents"/> 
        /// and collection of all microops from <see cref="Defines.Signals"/>.</summary>
        public static void SetAvaibleSignals() {
            //SetActiveComponents(active);
            AvailableSignals.Clear();
            for (int i = 0; i < Defines.Signals.Count; i <<= 1) {
                if (ActiveComponents.HasFlag((Defines.Components)i))
                    AvailableSignals.AddRange(Defines.Signals[i]);
            }
        }
        /// <summary>
        /// Returns list of all avaible instructions names, considering the currently set <see cref="CodeBits"/> value <br></br>
        /// <i>(not always whole set of instructions can be loaded if hardware settings does not provide enough space for encoding)</i>.
        /// </summary>
        /// <returns>List containing up to (<see cref="GetMaxOpcode"/>+1) elements, representing instruction names. </returns>
        public static List<string> GetAvaibleInstructions() {
            var allInstructions = MachineAssembler.InstructionLoader.GetAvaibleInstructionsNames();
            int visible = Math.Min(allInstructions.Count, (int)GetMaxOpcode() + 1);
            return allInstructions.GetRange(0, visible);
        }
        /// <summary> Retreives current <see cref="Defines.Architecture"/> of machine. </summary>
        /// <returns><see cref="CurrentArchitecture"/></returns>
        public static Defines.Architecture GetArchitecture() => CurrentArchitecture;

        /// <summary>Allows to set current <see cref="Defines.Architecture"/> of machine.</summary>
        /// <param name="machine">Architecture to set.</param>
        public static void SetArchitecture(Defines.Architecture machine) {
            CurrentArchitecture = machine;
            //SetAvaibleSignals((Defines.Components)machine); // TODO if architecture change supported.
        }

        #endregion

        #region Interrupts and IOs
        /// <summary>
        /// Initializes interrupt vector dictionary, creating new instance of it, 
        /// filled with consecutive values from 0 to <see cref="Defines.INTERRUPTIONS_NUM"/> (as keys - interrupt IDs),
        /// and reverse sequence (as values - interrupt handler addresses)
        /// </summary>
        public static void InitializeInterruptVector() {
            Dictionary<uint, uint> baseIntVect = new Dictionary<uint, uint>();
            for (uint bit = 0; bit < Defines.INTERRUPTIONS_NUM; bit++) {
                baseIntVect.Add(bit, Defines.INTERRUPTIONS_NUM - bit);
            }
            SetInterruptVector(baseIntVect);
        }

        /// <summary>Allows to access data structure mapping interruption-specific bit (<see cref="Dictionary{TKey, TValue}.Keys"/>) into memory adresses (<see cref="Dictionary{TKey, TValue}.Values"/>).</sum
        /// <returns><see cref="InterruptVector"/> instance.</returns>
        public static Dictionary<uint, uint> GetInterruptVector() => InterruptVector;

        /// <summary> Clears old <see cref="InterruptVector"/> and copies content of <paramref name="newVector"/> instance to it.</summary>
        /// <param name="newVector"><see cref="Dictionary{TKey, TValue}"/> object with new interrupt vector (INTbit-address) pairs.</param>
        public static void SetInterruptVector(Dictionary<uint, uint> newVector) {
            InterruptVector.Clear();
            foreach (var pair in newVector)
                InterruptVector.Add(pair.Key, pair.Value);
        }


        /// <summary>
        /// Should return number of avaible IO devices based on current architecture settings 
        /// (if <see cref="ActiveComponents"/> change was fully handled in GUI) but currently hardcoded for <see cref="Defines.Architecture.MachinePI"/> state.
        /// </summary>
        /// <returns>Hardcoded value of (<see cref="Defines.DEFAULT_IO_NUMBER"/> + <see cref="Defines.EXTENDED_IO_NUMBER"/>)</returns>
        public static int GetNumberOfIODevices() {
            return Defines.DEFAULT_IO_NUMBER + Defines.EXTENDED_IO_NUMBER;
            //if (ActiveComponents.HasFlag(Defines.Components.ExtendedIO)) return Defines.DEFAULT_IO_NUMBER + Defines.EXTENDED_IO_NUMBER;
            //return Defines.DEFAULT_IO_NUMBER;
        }

        /// <summary>Calculates how many bits are neccessary to encode all IO Devices addresses.</summary>
        /// <returns>Minimal number of bits that will allow to assign each IO device it's own memory-mapped address.</returns>
        public static uint GetAddressSpaceForIO() {
            int numOfIOs = GetNumberOfIODevices();
            return (uint)Math.Ceiling(Math.Log(a: numOfIOs, newBase: 2));
        }

        /// <summary> Allows to retreive IO device's ID number, base on its memory-mappped address. <br></br>
        /// Throws <see cref="Exception"/> if no device with <paramref name="IOAddress"/> was found in <see cref="IODevices"/> map.</summary>
        /// <param name="IOAddress">Address of IO device in machine memory.</param>
        /// <returns>ID of device with address == <paramref name="IOAddress"/>.</returns>
        /// <exception cref="Exception"></exception>
        public static uint GetIODeviceID(uint IOAddress) {
            if (IODevices.ContainsKey(IOAddress) == false) throw new Exception("No Input/Output Device with address: {" + IOAddress.ToString() + "}");
            return IODevices[IOAddress];
        }

        /// <summary>
        /// Initializes IO device's index-address dictionary, creating new instance of it, filled with consecutive values from 0 to 'max number of devices', 
        /// for both keys and values of created map (means that device with ID = 0 is memory-mapped to address 0 etc.).
        /// </summary>
        public static void InitializeIODevicesAddresses() {
            Dictionary<uint, uint> baseIOAddr = new Dictionary<uint, uint>();
            for (uint i = 1; i <= GetNumberOfIODevices(); i++) {
                baseIOAddr.Add(i, i);
            }
            SetIODevicesAddresses(baseIOAddr);
        }

        /// <summary> Clears old <see cref="IODevices"/> and copies content of <paramref name="DevicesAddresses"/> instance to it.</summary>
        /// <param name="DevicesAddresses"><see cref="Dictionary{TKey, TValue}"/> object with new IO device's address-ID pairs.</param>
        public static void SetIODevicesAddresses(Dictionary<uint, uint> DevicesAddresses) {
            IODevices.Clear();
            foreach (var pair in DevicesAddresses)
                IODevices.Add(pair.Key, pair.Value);
        }
        #endregion

    }

}
