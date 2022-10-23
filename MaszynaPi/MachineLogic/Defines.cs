using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaszynaPi.MachineLogic {
    public static class Defines {

        public const byte CODE_BITS_MIN = 3;
        public const byte CODE_BITS_MAX = 8;

        public const byte ADDRESS_BITS_MIN = 5;
        public const byte ADDRESS_BITS_MAX = 16;

        public const uint DEFAULT_MEM_VAL = 0;
        public const uint DEFAULT_REG_VAL = 0;
        public const uint DEFAULT_BUS_VAL = 0;
        public const uint DEFAULT_ALU_VAL = 0;

        public const uint DEFAULT_CODE_BITS = 3;
        public const uint DEFAULT_ADDR_BITS = 5;
    }
}
