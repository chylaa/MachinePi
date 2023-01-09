using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaszynaPi {
    public static class Defines {
        public const uint CODE_BITS_MIN = 3;
        public const uint CODE_BITS_MAX = 8;

        public const uint ADDRESS_BITS_MIN = 5;
        public const uint ADDRESS_BITS_MAX = 16;

        public const uint DEFAULT_MEM_VAL = 0;
        public const uint DEFAULT_REG_VAL = 0;
        public const int DEFAULT_BUS_VAL = -1;
        public const uint DEFAULT_ALU_VAL = 0;

        public const uint DEFAULT_CODE_BITS = 3;
        public const uint DEFAULT_ADDR_BITS = 5;

        public const uint RB_REG_BIT_SIZE = 8;
        public const uint G_REG_BIT_SIZE = 1;

        public const int DEFAULT_IO_NUMBER = 2;
        public const int EXTENDED_IO_NUMBER = 6; // Excluding default io's

        public const uint INTERRUPTIONS_NUM = 4;

        //Enum represent different Machine architectures -> they are encoded as the bit AND of their base components ([Flag] enum Components) 
        public enum Architectures : int { MachineW = 1, MachineWp = 3, MachineL = 255, MachineEW = 2047, MachinePI = 4095 };
        public const Architectures DEFAULT_ARCHITECTURE = Architectures.MachineW;

        [Flags]
        public enum Components {
            Basic = 0b_0000_0000_0001,               // 1
            BusConnection = 0b_0000_0000_0010,       // 2
            ALUIncrementations = 0b_0000_0000_0100,  // 4
            ALULogical = 0b_0000_0000_1000,          // 8
            ALUArythmetical = 0b_0000_0001_0000,     // 16
            Stack = 0b_0000_0010_0000,               // 32
            RegisterX = 0b_0000_0100_0000,           // 64
            RegisterY = 0b_0000_1000_0000,           // 128
            Interuptions = 0b_0001_0000_0000,        // 256
            IO = 0b_0010_0000_0000,                  // 512
            Flags = 0b_0100_0000_0000,               // 1024
            ExtendedIO = 0b_1000_0000_0000           // 2048
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


        public const string ALU_FLAG_Z = "z";
        public const string ALU_FLAG_V = "v";
        public const string ALU_FLAG_INT = "int";
        public const string ALU_FLAG_ZAK = "zak";
        // Pirority of interrrupts from diff joystick movements
        public static readonly Dictionary<string, int> JOYSTICK_INTERRUPTS = new Dictionary<string, int> { { "left", 1 }, { "right", 2 }, { "up", 4 }, { "down", 8 } };

        public const string SIGNAL_LABEL = "@";
        public const int STATMENT_ARG_POSITION = 1;

        // Settable if eng. version loaded

        public static string BASE_INSTRUCTION_SET_FILENAME { get; set; }
        public static List<string> FETCH_SIGNALS { get; private set; }

        public static string HEADER_MEM_ALLOC { get; private set; }  // [Header] Allocate Memory - lowercase to standarize code
        public static string HEADER_CONST_VAR { get; private set; }  // [Header] Const Variable Def - lowercase to standarize code

        public static string SIGNAL_TEST_IO_READY { get; private set; }

        public static string SIGNAL_STATEMENT_IF { get; private set; }
        public static string SIGNAL_STATEMENT_THEN { get; private set; }
        public static string SIGNAL_STATEMENT_ELSE { get; private set; }
        public static string SIGNAL_STATEMENT_END { get; private set; }

        public static string INSTRUCTION_ARGSNUM_HEADER { get; private set; }
        public static string INSTRUCTION_NAME_HEADER {get; private set; } //importat space at the end!

        public enum Lang { PL, ENG }

        public static Lang LangInUse { get; private set; }

        public static void SetInstructionsLanguageVersion(Lang lang) {
            LangInUse = lang;
            if(lang == Lang.ENG) {
                if(BASE_INSTRUCTION_SET_FILENAME == "Podstawa.lst" || BASE_INSTRUCTION_SET_FILENAME == null)
                    BASE_INSTRUCTION_SET_FILENAME = "Base.lst";
                FETCH_SIGNALS = new List<string> { "rd", "od", "iins", "icit" };
                HEADER_MEM_ALLOC = "res";
                HEADER_CONST_VAR = "def";
                SIGNAL_TEST_IO_READY = "start";
                SIGNAL_STATEMENT_IF = "if";
                SIGNAL_STATEMENT_THEN = "then";
                SIGNAL_STATEMENT_ELSE = "if else";
                SIGNAL_STATEMENT_END = "end";

                INSTRUCTION_ARGSNUM_HEADER = "arguments ";
                INSTRUCTION_NAME_HEADER = "instruction ";
            }
            if(lang == Lang.PL) {
                if (BASE_INSTRUCTION_SET_FILENAME == "Base.lst" || BASE_INSTRUCTION_SET_FILENAME == null)
                    BASE_INSTRUCTION_SET_FILENAME = "Podstawa.lst";
                FETCH_SIGNALS = new List<string> { "czyt", "wys", "wei", "il" };
                HEADER_MEM_ALLOC = "rpa";  
                HEADER_CONST_VAR = "rst"; 
                SIGNAL_TEST_IO_READY = "start";
                SIGNAL_STATEMENT_IF = "jeżeli";
                SIGNAL_STATEMENT_THEN = "to";
                SIGNAL_STATEMENT_ELSE = "gdy nie";
                SIGNAL_STATEMENT_END = "koniec";

                INSTRUCTION_ARGSNUM_HEADER = "argumenty ";
                INSTRUCTION_NAME_HEADER = "rozkaz ";
            }
        }
        static Defines() {
            SetInstructionsLanguageVersion(Lang.ENG);
        }
    }
}

