using System;
using System.Collections.Generic;

namespace MaszynaPi 
{
    /// <summary>
    /// Static class, containing public constans values, allowing easy defining/modification application-specific 'assumption' values. 
    /// <br></br><u><b>Note:</b> Selected standard is to store all string values in lowercase!</u>
    /// </summary>
    public static class Defines 
    {
        /// <summary>Minimal size (in bits) of machine's WORD opcode part (restriction for setting via UI).</summary>
        public const uint CODE_BITS_MIN = 3;
        /// <summary>Maximum size (in bits) of machine's WORD opcode part (restriction for setting via UI).</summary>
        public const uint CODE_BITS_MAX = 8;

        /// <summary>Minimum size (in bits) of address space (restriction for setting via UI).</summary>
        public const uint ADDRESS_BITS_MIN = 5;
        /// <summary>Maximum size (in bits) of address space (restriction for setting via UI).</summary>
        public const uint ADDRESS_BITS_MAX = 14;

        /// <summary> Default number of bits used as opcode part of machine's WORD.</summary>
        public const uint DEFAULT_CODE_BITS = 3;
        /// <summary> Default number of bits used as address part of machine's WORD.</summary>
        public const uint DEFAULT_ADDR_BITS = 5;

        /// <summary> Default value of <see cref="MachineLogic.Architecture.Memory.Content"/> element.</summary>
        public const uint DEFAULT_MEM_VAL = 0;
        /// <summary> Default value of <see cref="MachineLogic.Architecture.Register.Value"/> field.</summary>
        public const uint DEFAULT_REG_VAL = 0;
        /// <summary> Default value of <see cref="MachineLogic.Architecture.Bus.Value"/> field.</summary>
        public const int DEFAULT_BUS_VAL = -1;
        /// <summary> Default value of <see cref="MachineLogic.Architecture.Register.Value"/> field inside <see cref="MachineLogic.Architecture.ArithmeticLogicUnit.AK"/> register.</summary>
        public const uint DEFAULT_ALU_VAL = 0;

        /// <summary> Hardcoded size (in bits) of <see cref="MachineLogic.CentralProcessingUnit.RB"/> register.</summary>
        public const uint RB_REG_BIT_SIZE = 8;
        /// <summary> Hardcoded size (in bits) of <see cref="MachineLogic.CentralProcessingUnit.G"/> register.</summary>
        public const uint G_REG_BIT_SIZE = 1;

        /// <summary>Number of IO devices wihout <see cref="Components.ExtendedIO"/> mode enabled.</summary>
        public const int DEFAULT_IO_NUMBER = 2;
        /// <summary>Number of IO devices in <see cref="Components.ExtendedIO"/> mode (Excluding <see cref="DEFAULT_IO_NUMBER"/>).</summary>
        public const int EXTENDED_IO_NUMBER = 6; 

        /// <summary>Amout of possible interruptions sources.</summary>
        public const uint INTERRUPTIONS_NUM = 4;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

        /// <summary> Enum represent different Machine architectures -> they are encoded as the bitwise AND of their base <see cref="Components"/>.
        /// <br></br><see cref="Architecture.MachinePI"/> implementation was subject of this application's extension over original simulator.</summary>
        public enum Architecture : int { MachineW = 1, MachineWp = 3, MachineL = 255, MachineEW = 2047, MachinePI = 4095 };
        /// <summary> Default <see cref="Architecture"/> mode, used when applicaiton is started.</summary>
        public const Architecture DEFAULT_ARCHITECTURE = Architecture.MachineW;

        /// <summary>
        /// Flag typed enumeration, representing whole set of implemented Machine components.
        /// <br></br><see cref="Components.ExtendedIO"/> implementaton was subject of this application's extension over original simulator.
        /// </summary>
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
#pragma warning restore CS1591 

        #region Unfinished, Architecture & Components enabling/disabling part of implementation 

        /* If instruction list containing any signal from a given component is loaded, it must be activated -> related formats appear in GUI. 
         * If component is activated -> related formats appears in GUI and corresponding list of avaible signals is added 
         * TODO: fill lists with related signals and handle changes. For now, Machine stuck in PI extension (all Components) 
         * public static List<string> SignalsBasic = new List<string> { };
         * public static List<string> SignalsBusConnection = new List<string> { };
         * public static List<string> SignalsALUIncrementations = new List<string> { };
         * public static List<string> SignalsALULogical = new List<string> { };
         * public static List<string> SignalsALUArythmetical = new List<string> { };
         * public static List<string> SignalsStack = new List<string> { };
         * public static List<string> SignalsRegisterX = new List<string> { };
         * public static List<string> SignalsRegisterY = new List<string> { };
         * public static List<string> SignalsInteruptions = new List<string> { };
         * public static List<string> SignalsInput = new List<string> { };
         * public static List<string> SignalsFlags = new List<string> { };
        /// <summary>
        /// Collection of all implemented microinstructions (of all available <see cref="Architecture"/>s).
        /// <br></br><i>(Currently contains only empty lists due to lack of implementation of dynamic architecture changes - TODO)</i> 
        /// </summary>
        public static List<List<string>> Signals = new List<List<string>> { SignalsBasic, SignalsBusConnection, SignalsALUIncrementations,
                                                                            SignalsALULogical, SignalsALUArythmetical, SignalsStack,
                                                                            SignalsRegisterX, SignalsRegisterY, SignalsInteruptions,
                                                                            SignalsInput, SignalsFlags};
         */

        #endregion
        /// <summary> Instruction-definition-language keyword representing ALU's 'sign' bit flag.</summary>
        public const string ALU_FLAG_Z = "z";
        /// <summary> Instruction-definition-language keyword representing ALU's 'value' flag.</summary>
        public const string ALU_FLAG_V = "v";
        /// <summary> Instruction-definition-language keyword representing ALU's 'interrupt' flag.</summary>
        public const string ALU_FLAG_INT = "int";
        /// <summary> Instruction-definition-language keyword representing ALU's 'zero' flag.</summary>
        public const string ALU_FLAG_ZAK = "zak";

        /// <summary> Hardcoded pirority of interrrupts from different joystick movements (refers to <see cref="Architecture.MachinePI"/> architecture).</summary>
        public static readonly Dictionary<string, int> JOYSTICK_INTERRUPTS = new Dictionary<string, int> { { "left", 1 }, { "right", 2 }, { "up", 4 }, { "down", 8 } };

        /// <summary> Instruction-definition-language label prefix character.</summary>
        public const string SIGNAL_LABEL_PREFIX = "@";
        /// <summary> Position of argument (after space string split) in conditional statement syntax in instruction-definition-language.</summary>
        public const int STATMENT_ARG_POSITION = 1;

        #region Methods and settable fields related to language version specification

        /// <summary> Name of file containing definition of instruction set that will be loaded as on application startup. </summary>
        public static string BASE_INSTRUCTION_SET_FILENAME { get; set; }

        /// <summary> List of signals (<i>microinstructions</i>) used to fetch instruciton from memory
        /// (those remains always the same, becaouse fetch cycle uops do not depends on which instruction being executed).</summary>
        public static List<string> FETCH_SIGNALS { get; private set; }

        /// <summary> Defines assembly keyword for allocating word-sized part of memory to use as variable.</summary>
        public static string KEYWORD_MEM_ALLOC { get; private set; }
        /// <summary> Defines assembly keyword for defining referable constant (value written into program data segment after compilation)</summary>
        public static string KEYWORD_CONST_VAR { get; private set; }

        /// <summary> Instruction-definition-language keyword, used when checking if IO device signalized ready state.</summary>
        public static string SIGNAL_TEST_IO_READY { get; private set; }

        /// <summary> Instruction-definition-language keyword, defining 'IF' part of conditional (if-then-else-end)  statement.</summary>
        public static string SIGNAL_STATEMENT_IF { get; private set; }
        /// <summary> Instruction-definition-language keyword, defining 'THEN' part of conditional (if-then-else-end)  statement.</summary>
        public static string SIGNAL_STATEMENT_THEN { get; private set; }
        /// <summary> Instruction-definition-language keyword, defining 'ELSE' part of conditional (if-then-else-end) statement.</summary>
        public static string SIGNAL_STATEMENT_ELSE { get; private set; }
        /// <summary> Instruction-definition-language keyword, defining 'END' part of conditional (if-then-else-end) statement.</summary>
        public static string SIGNAL_STATEMENT_END { get; private set; }

        /// <summary>Reffers to instruction set definition (*.lst) file's segment, that marks definition of number of arguments that instruction takes.
        /// <br></br>Note: For standarization purposes, assigned string <u>should end with space character!</u></summary>
        public static string INSTRUCTION_ARGSNUM_HEADER { get; private set; }
        /// <summary>Reffers to instruction set definition (*.lst) file's segment, that marks definition of instruction name. 
        /// <br></br>Note: For standarization purposes, assigned string <u>should end with space character!</u></summary>
        public static string INSTRUCTION_NAME_HEADER {get; private set; }

        /// <summary> Represents supproted languages version of application, (affects assembly and micrioinstructions code syntax). </summary>
        public enum Lang 
        { 
            /// <summary>Polish</summary>
            PL, 
            /// <summary>English</summary>
            ENG 
        }

        /// <summary> Stores language version curenntly in use. </summary>
        public static Lang LangInUse { get; private set; }

        /// <summary> Assigns each of 'static string' fields their <paramref name="lang"/>-specific value via private setter. </summary>
        /// <param name="lang"><see cref="Lang"/> enum to be used in programming syntax.</param>
        public static void SetInstructionsLanguageVersion(Lang lang) {
            LangInUse = lang;
            if(lang == Lang.ENG) {
                if(BASE_INSTRUCTION_SET_FILENAME == "Podstawa.lst" || BASE_INSTRUCTION_SET_FILENAME == null)
                    BASE_INSTRUCTION_SET_FILENAME = "Base.lst";
                FETCH_SIGNALS = new List<string> { "rd", "od", "iins", "icit" };
                KEYWORD_MEM_ALLOC = "res";
                KEYWORD_CONST_VAR = "def";
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
                KEYWORD_MEM_ALLOC = "rpa";  
                KEYWORD_CONST_VAR = "rst"; 
                SIGNAL_TEST_IO_READY = "start";
                SIGNAL_STATEMENT_IF = "jeżeli";
                SIGNAL_STATEMENT_THEN = "to";
                SIGNAL_STATEMENT_ELSE = "gdy nie";
                SIGNAL_STATEMENT_END = "koniec";

                INSTRUCTION_ARGSNUM_HEADER = "argumenty ";
                INSTRUCTION_NAME_HEADER = "rozkaz ";
            }
        }
        /// <summary> Static construction-like allows to invoke language specification method automatically on application startup. </summary>
        static Defines() => SetInstructionsLanguageVersion(Lang.ENG);

        #endregion
    }
}

