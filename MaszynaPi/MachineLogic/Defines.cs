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

        //Enum represent different Machine architectures -> they are encoded as the sum of their base components ([Flag] enum Components) 
        public enum Machines:int { MachineW=0, MachineWp=1, MachineL=1151, MachineEW=2047};
        public const Machines DEFAULT_MACHINE = Machines.MachineW;

        [Flags]
        public enum Components {
            Basic = 0b_0000_0000_0000,               // 0
            BusConnection = 0b_0000_0000_0001,       // 1
            ALUIncrementations = 0b_0000_0000_0010,  // 2
            ALULogical = 0b_0000_0000_0100,          // 4
            ALUArythmetical = 0b_0000_0000_1000,     // 8
            Stack = 0b_0000_0001_0000,               // 16
            RegisterX = 0b_0000_0010_0000,           // 32
            RegisterY = 0b_0000_0100_0000,           // 64
            Interuptions = 0b_0001_0000_0000,        // 128
            Input = 0b_0010_0000_0000,               // 256
            Flags = 0b_0100_0000_0000                // 512
        }

        /* If instruction list containing any signal from a given component is loaded, it must be activated -> related formats appear in GUI. 
         * If component is activated -> related formats appears in GUI and corresponding list of avaible signals is added*/
        // TODO: fill lists with relted signals
        public static List<string> SignalsBasic = new List<string> { };
        public static List<string> SignalsBusConnection = new List<string> { };
        public static List<string> SignalsALUIncrementations = new List<string> { };
        public static List<string> SignalsALULogical = new List<string> { };
        public static List<string> SignalsALUArythmetical = new List<string> { };
        public static List<string> SignalsStack = new List<string> { };
        public static List<string> SignalsRegisterX = new List<string> { };
        public static List<string> SignalsRegisterY = new List<string> { };
        public static List<string> SignalsInteruptions = new List<string> { };
        public static List<string> SignalsInput = new List<string> { };
        public static List<string> SignalsFlags = new List<string> { };

        public static List<List<string>> Signals = new List<List<string>> { SignalsBasic, SignalsBusConnection, SignalsALUIncrementations,
                                                                            SignalsALULogical, SignalsALUArythmetical, SignalsStack,
                                                                            SignalsRegisterX, SignalsRegisterY, SignalsInteruptions,
                                                                            SignalsInput, SignalsFlags};
            
    }
}
