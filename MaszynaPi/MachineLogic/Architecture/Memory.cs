using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.CommonOperations;

namespace MaszynaPi.MachineLogic.Architecture {
    class MemoryException : Exception {public MemoryException(string message) : base(message) {}}

    public class Memory {
        private List<uint> Content;

        public Memory() {
            InitMemoryContent();
        }

        public void InitMemoryContent() {
            Content = new List<uint>();
            var newAddressSpace = ArchitectureSettings.GetAddressSpace();
            for (int i = 0; i < Math.Pow(2,newAddressSpace); i++)
                Content.Add(Defines.DEFAULT_MEM_VAL);
        }
        public void ExpandMemory(uint oldAddressSpace) {
            var newAddressSpace = ArchitectureSettings.GetAddressSpace();
            for (int i=0; i < Arithmetic.PowersDifference(newAddressSpace,oldAddressSpace); i++)
                Content.Add(Defines.DEFAULT_MEM_VAL);
        }

        public void ShrinkMemory(uint oldAddressSpace) {
            var newAddressSpace = ArchitectureSettings.GetAddressSpace();
            var toRemove = Arithmetic.PowersDifference(newAddressSpace, oldAddressSpace);
            Content.RemoveRange(Content.Count-toRemove-1, toRemove);
        }

        public void StoreValue(uint addr, uint value) {
            if (Content.Count <= addr) throw new MemoryException("[Memory Overflow] Address store request greater than memory size");
            Content[(int)addr] = Bitwise.HandleOverflow(value);
        }

        public uint GetValue(uint addr) {
            if (Content.Count <= addr) throw new MemoryException("[Memory Overflow] Address get request greater than memory size");
            return Content[(int)addr];
        }

        public void Reset() { InitMemoryContent(); }

        // ======================= <  User Interface Methods > ================================= //
        public List<uint> GetContentHandle() { return Content; }
    }
}
