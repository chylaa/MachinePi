using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaszynaPi.MachineLogic {
    class MemoryException : Exception {public MemoryException(string message) : base(message) {}}

    class Memory {
        private uint AddressSpace=0; // addr space int bits
        private uint CodeSize = 0; // code space in bits
        private List<uint> Content { get; set; } = new List<uint>();
        public Memory(uint addressSpace, uint codeSize) {
            if (addressSpace > Defines.ADDRESS_BITS_MAX || addressSpace < Defines.ADDRESS_BITS_MIN)
                throw new MemoryException("Invalid memory initial size");
            setAddressSpace(addressSpace);
            setCodeSize(codeSize);
        }

        public void setAddressSpace(uint newAddressSpace) {
            for(int i=0; i < newAddressSpace - AddressSpace; i++)
                Content.Add(Defines.DEFAULT_MEM_VAL);
            AddressSpace = newAddressSpace;
        }
        public void setCodeSize(uint newCodeSize) { CodeSize = newCodeSize; }

        public uint GetValue(uint addr) {
            if (Content.Count >= addr) throw new MemoryException("Address request greater than memory size");
            return Content[(int)addr];
        }
    }
}
