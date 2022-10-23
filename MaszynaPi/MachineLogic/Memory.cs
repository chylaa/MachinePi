using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.CommonOperations;

namespace MaszynaPi.MachineLogic {
    class MemoryException : Exception {public MemoryException(string message) : base(message) {}}

    class Memory {
        private List<uint> Content = new List<uint>();

        public Memory(uint addressSpace = Defines.DEFAULT_ADDR_BITS) {
            InitMemoryContent(addressSpace);
        }

        public void InitMemoryContent(uint newAddressSpace) {
            Content.Clear();
            for (int i = 0; i < Math.Pow(2,newAddressSpace); i++)
                Content.Add(Defines.DEFAULT_MEM_VAL);
        }
        public void ExpandMemory(uint oldAddressSpace, uint newAddressSpace) {
            for(int i=0; i < Arythmetics.PowersDifference(newAddressSpace,oldAddressSpace); i++)
                Content.Add(Defines.DEFAULT_MEM_VAL);
        }

        public void StoreValue(uint addr, uint value) {
            if (Content.Count >= addr) throw new MemoryException("Address request greater than memory size");
            Content[(int)addr] = value;
        }

        public uint GetValue(uint addr) {
            if (Content.Count >= addr) throw new MemoryException("Address request greater than memory size");
            return Content[(int)addr];
        }

        // ======================= <  User Interface Methods > ================================= //
        public List<uint> GetAllMemoryContent() { return Content; }
    }
}
