using System;
using System.Collections.Generic;
using MaszynaPi.CommonOperations;

namespace MaszynaPi.MachineLogic.Architecture {

    /// <summary> Generic <see cref="Exception"/> for representing errors related to <see cref="Memory"/> class issues. </summary>
    class MemoryException : Exception {public MemoryException(string message) : base(message) {}}

    /// <summary>
    /// Class representing Machines' data-program read/write memory.
    /// </summary>
    public class Memory {

        /// <summary>
        /// Internal contents of <see cref="Memory"/> where each element is single machine word 
        /// (<see cref="ArchitectureSettings.GetWordBits"/> irrelevat - always <see cref="uint"/> max). 
        /// </summary>
        private List<uint> Content;

        /// <summary> Default constructor, calls <see cref="InitMemoryContent"/> for setting <see cref="Content"/>. </summary>
        public Memory() {
            InitMemoryContent();
        }

        /// <summary>
        /// Initializes <see cref="Content"/> with <see cref="Defines.DEFAULT_MEM_VAL"/> of size <see cref="ArchitectureSettings.GetAddressSpace"/>.
        /// </summary>
        public void InitMemoryContent() {
            Content = new List<uint>();
            var newAddressSpace = ArchitectureSettings.GetAddressSpace();
            for (int i = 0; i < Math.Pow(2,newAddressSpace); i++)
                Content.Add(Defines.DEFAULT_MEM_VAL);
        }

        /// <summary>
        /// Expands <see cref="Content"/>, base on <see cref="ArchitectureSettings.GetAddressSpace"/> (should be called if address space was changed).
        /// All newly added elements are initialized with <see cref="Defines.DEFAULT_MEM_VAL"/>.
        /// </summary>
        /// <param name="oldAddressSpace">Old value of address space, for calculating amount of elements to add.</param>
        public void ExpandMemory(uint oldAddressSpace) {
            var newAddressSpace = ArchitectureSettings.GetAddressSpace();
            for (int i=0; i < Arithmetic.PowersDifference(newAddressSpace,oldAddressSpace); i++)
                Content.Add(Defines.DEFAULT_MEM_VAL);
        }
        /// <summary>
        /// Shrinks <see cref="Content"/>, base on <see cref="ArchitectureSettings.GetAddressSpace"/> (should be called if address space was changed).
        /// All deleted elements contents are lost.
        /// </summary>
        /// <param name="oldAddressSpace">Old value of address space, for calculating amount of elements to add.</param>
        public void ShrinkMemory(uint oldAddressSpace) {
            var newAddressSpace = ArchitectureSettings.GetAddressSpace();
            var toRemove = Arithmetic.PowersDifference(newAddressSpace, oldAddressSpace);
            Content.RemoveRange(Content.Count-toRemove-1, toRemove);
        }

        /// <summary>
        /// Writes given <paramref name="value"/> under specific address <paramref name="addr"/> (where address is index of machine word).
        /// Written value is checked for potential overflow with <see cref="Bitwise.HandleOverflow(uint, uint)"/>.
        /// If passed <paramref name="addr"/> is not in address space of <see cref="Memory"/>, <see cref="MemoryException"/> is thrown.
        /// </summary>
        /// <param name="addr">Address representing index of machine word that should be updated (index of <see cref="Content"/> list).</param>
        /// <param name="value">Value that should be written into specific <see cref="Content"/> index.</param>
        /// <exception cref="MemoryException"></exception>
        public void StoreValue(uint addr, uint value) {
            if (Content.Count <= addr) throw new MemoryException("[Memory Overflow] Address store request greater than memory size");
            Content[(int)addr] = Bitwise.HandleOverflow(value);
        }

        /// <summary>
        /// Retreives value from specific index (<paramref name="addr"/>) of <see cref="Content"/>.
        /// If passed <paramref name="addr"/> is not in address space of <see cref="Memory"/>, <see cref="MemoryException"/> is thrown.
        /// </summary>
        /// <param name="addr">Address (index) of value that should be read.</param>
        /// <returns>Read value under <see cref="Content"/>[<paramref name="addr"/>]</returns>
        /// <exception cref="MemoryException"></exception>
        public uint GetValue(uint addr) {
            if (Content.Count <= addr) throw new MemoryException("[Memory Overflow] Address get request greater than memory size");
            return Content[(int)addr];
        }

        /// <summary>Resets <see cref="Memory"/> by calling <see cref="InitMemoryContent"/> method.</summary>
        public void Reset() { InitMemoryContent(); }

        // ======================= <  User Interface Methods > ================================= //
        
        /// <summary>Allows to get internal list <see cref="Content"/> handle, for specific User Interface purposes.</summary>
        /// <returns>Instance of internal <see cref="Content"/> list.</returns>
        public List<uint> GetContentHandle() { return Content; }
    }
}
