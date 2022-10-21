using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaszynaPi.MachineLogic {
    public static class Defines {

        public static byte CODE_BITS_MIN {get;} = 3;
        public static byte CODE_BITS_MAX { get; } = 8;

        public static byte ADDRESS_BITS_MIN { get; } = 5;
        public static byte ADDRESS_BITS_MAX { get; } = 16;

        public static uint DEFAULT_MEM_VAL { get; } = 0;
    }
}
