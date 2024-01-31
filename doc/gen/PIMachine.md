# PiMachine

```
Genearated using https://github.com/discosultan/vsdoc-2-md
MIT License
```

<table>
<tbody>
<tr>
<td><a href="#arithmetic">Arithmetic</a></td>
<td><a href="#bitwise">Bitwise</a></td>
</tr>
<tr>
<td><a href="#defines">Defines</a></td>
<td><a href="#architecture">Architecture</a></td>
</tr>
<tr>
<td><a href="#components">Components</a></td>
<td><a href="#lang">Lang</a></td>
</tr>
<tr>
<td><a href="#filehandlerexception">FileHandlerException</a></td>
<td><a href="#fileshandler">FilesHandler</a></td>
</tr>
<tr>
<td><a href="#form1">Form1</a></td>
<td><a href="#logger">Logger</a></td>
</tr>
<tr>
<td><a href="#assembler">Assembler</a></td>
<td><a href="#debugger">Debugger</a></td>
</tr>
<tr>
<td><a href="#instructionloader">InstructionLoader</a></td>
<td><a href="#instructionloaderexception">InstructionLoaderException</a></td>
</tr>
<tr>
<td><a href="#aluflags">ALUFlags</a></td>
<td><a href="#arithmeticlogicunit">ArithmeticLogicUnit</a></td>
</tr>
<tr>
<td><a href="#bus">Bus</a></td>
<td><a href="#busexception">BusException</a></td>
</tr>
<tr>
<td><a href="#instructiondecoder">InstructionDecoder</a></td>
<td><a href="#instructionregister">InstructionRegister</a></td>
</tr>
<tr>
<td><a href="#interruptioncontroller">InterruptionController</a></td>
<td><a href="#iodevicescontroller">IODevicesController</a></td>
</tr>
<tr>
<td><a href="#memory">Memory</a></td>
<td><a href="#memoryexception">MemoryException</a></td>
</tr>
<tr>
<td><a href="#register">Register</a></td>
<td><a href="#architecturesettings">ArchitectureSettings</a></td>
</tr>
<tr>
<td><a href="#centralprocessingunit">CentralProcessingUnit</a></td>
<td><a href="#cpuexception">CPUException</a></td>
</tr>
<tr>
<td><a href="#characterinput">CharacterInput</a></td>
<td><a href="#characteroutput">CharacterOutput</a></td>
</tr>
<tr>
<td><a href="#humiditysensor">HumiditySensor</a></td>
<td><a href="#iodevice">IODevice</a></td>
</tr>
<tr>
<td><a href="#iodeviceexception">IODeviceException</a></td>
<td><a href="#iotype">IOType</a></td>
</tr>
<tr>
<td><a href="#matrixled">MatrixLED</a></td>
<td><a href="#mode">Mode</a></td>
</tr>
<tr>
<td><a href="#pressuresensor">PressureSensor</a></td>
<td><a href="#temperaturesensor">TemperatureSensor</a></td>
</tr>
<tr>
<td><a href="#resources">Resources</a></td>
<td><a href="#sensehatdevice">SenseHatDevice</a></td>
</tr>
<tr>
<td><a href="#sensehatexception">SenseHatException</a></td>
</tr>
</tbody>
</table>

### MaszynaPi.ArchitectureRadioButton.components

Wymagana zmienna projektanta.

### MaszynaPi.ArchitectureRadioButton.Dispose(disposing)

Wyczyść wszystkie używane zasoby.

| Name | Description |
| ---- | ----------- |
| disposing | *System.Boolean*<br>prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku. |

### MaszynaPi.ArchitectureRadioButton.InitializeComponent

Metoda wymagana do obsługi projektanta — nie należy modyfikować jej zawartości w edytorze kodu.


## Arithmetic

Static class providing methods for base arithmetic calculations.

### PowersDifference(exp1, exp2, base)

Calculates difference between two numbers represented as of base (default 2) to given exponent.

| Name | Description |
| ---- | ----------- |
| exp1 | *System.UInt32*<br>Exponent of minuend. |
| exp2 | *System.UInt32*<br>Exponent of subtrahend. |
| base | *System.UInt32*<br>Power base for both numbers (default 2). |

#### Returns

Integer-casted result of (base^exp1 - base^exp2) subtraction.


## Bitwise

Static class providing methods for base bitwise calculations.

### ConvertToSigned(value, numberBitsize)

Converts passed value to signed <a href="#system.int32">System.Int32</a> base on most significant bit pointed by numberBitsize.

| Name | Description |
| ---- | ----------- |
| value | *System.UInt32*<br>InstructionValue to convert |
| numberBitsize | *System.UInt32*<br>Requested value size in bits. |

#### Returns

Sign-extended, max <a href="#system.int32">System.Int32</a> value

### CreateBitMask(noOfZeroes, noOfOnes, zeroesFirst)

Creates unsigned number to act like bitmask with requested bits set.

| Name | Description |
| ---- | ----------- |
| noOfZeroes | *System.UInt32*<br>Number of consecutive '0' bits. |
| noOfOnes | *System.UInt32*<br>Number of consecutive '1' bits. |
| zeroesFirst | *System.Boolean*<br>If set to true (default), indicates that zero-bits should begin from MSB position. |

#### Returns

Unsigned value which bits are set as requested.

### DecodeInstructionOpcode(InstructionValue)

Extracts opcode component of passed InstructionValue base on <a href="#maszynapi.machinelogic.architecturesettings">MaszynaPi.MachineLogic.ArchitectureSettings</a>.

| Name | Description |
| ---- | ----------- |
| InstructionValue | *System.UInt32*<br>InstructionValue representing CPU instruction. |

#### Returns

Opcode of passed InstructionValue.

### DecodeIntructionArgument(InstructionValue)

Extracts argumet (address) component of passed InstructionValue base on <a href="#maszynapi.machinelogic.architecturesettings">MaszynaPi.MachineLogic.ArchitectureSettings</a>.

| Name | Description |
| ---- | ----------- |
| InstructionValue | *System.UInt32*<br>InstructionValue representing CPU instruction. |

#### Returns

Argument (address) component of passed InstructionValue.

### EncodeInstruction(opcode, argument)

Creates valid CPU instruction from opcode and argument components base on <a href="#maszynapi.machinelogic.architecturesettings">MaszynaPi.MachineLogic.ArchitectureSettings</a>.

| Name | Description |
| ---- | ----------- |
| opcode | *System.UInt32*<br>Opcode component of instruction value. |
| argument | *System.UInt32*<br>Argument (address) component of instruction value. |

#### Returns

New CPU instruction value containing opcode and address argument.

### GetBitsAmount(number)

Returns min amout of bits that are required to represent number as binary.

| Name | Description |
| ---- | ----------- |
| number | *System.Int32*<br> |

#### Returns



### HandleOverflow(value, bitsize)

Simulates value overflow based on provided 'virtual' bitsize. If bitsize == 0, overflow is based on <a href="#maszynapi.machinelogic.architecturesettings.getmaxword">MaszynaPi.MachineLogic.ArchitectureSettings.GetMaxWord</a>.

| Name | Description |
| ---- | ----------- |
| value | *System.UInt32*<br>InstructionValue that can potentially overflow if it would habe bitsize bits. |
| bitsize | *System.UInt32*<br>Virtual value size in bit. |

#### Returns

Original value if it would not overflow when having bitsize, overflown value otherwise.

### IsSignBitSet(value, numberBitsize)

Checks if most significant bit of value (pointed by numberBitsize) is set.

| Name | Description |
| ---- | ----------- |
| value | *System.UInt32*<br>InstructionValue to check. |
| numberBitsize | *System.UInt32*<br>Requested value size in bits. |

#### Returns

True if MSB of value as numberBitsize-bit number is set, false otherwise.

### MaszynaPi.ComponentsCheckBox.components

Wymagana zmienna projektanta.

### MaszynaPi.ComponentsCheckBox.Dispose(disposing)

Wyczyść wszystkie używane zasoby.

| Name | Description |
| ---- | ----------- |
| disposing | *System.Boolean*<br>prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku. |

### MaszynaPi.ComponentsCheckBox.InitializeComponent

Metoda wymagana do obsługi projektanta — nie należy modyfikować jej zawartości w edytorze kodu.


## Defines

Static class, containing public constans values, allowing easy defining/modification application-specific 'assumption' values. 

### #cctor

Static construction-like allows to invoke language specification method automatically on application startup.

### ADDRESS_BITS_MAX

Maximum size (in bits) of address space (restriction for setting via UI).

### ADDRESS_BITS_MIN

Minimum size (in bits) of address space (restriction for setting via UI).

### ALU_FLAG_INT

Instruction-definition-language keyword representing ALU's 'interrupt' flag.

### ALU_FLAG_V

Instruction-definition-language keyword representing ALU's 'value' flag.

### ALU_FLAG_Z

Instruction-definition-language keyword representing ALU's 'sign' bit flag.

### ALU_FLAG_ZAK

Instruction-definition-language keyword representing ALU's 'zero' flag.


## Architecture

Enum represent different Machine architectures -> they are encoded as the bitwise AND of their base <a href="#components">Components</a>. <a href="#architecture.machinepi">Architecture.MachinePI</a> implementation was subject of this application's extension over original simulator.

### MaszynaPi.Defines.BASE_INSTRUCTION_SET_FILENAME

Name of file containing definition of instruction set that will be loaded as on application startup.

### MaszynaPi.Defines.CODE_BITS_MAX

Maximum size (in bits) of machine's WORD opcode part (restriction for setting via UI).

### MaszynaPi.Defines.CODE_BITS_MIN

Minimal size (in bits) of machine's WORD opcode part (restriction for setting via UI).


## Components

Flag typed enumeration, representing whole set of implemented Machine components. <a href="#components.extendedio">Components.ExtendedIO</a> implementaton was subject of this application's extension over original simulator.

### MaszynaPi.Defines.DEFAULT_ADDR_BITS

Default number of bits used as address part of machine's WORD.

### MaszynaPi.Defines.DEFAULT_ALU_VAL

Default value of <a href="#maszynapi.machinelogic.architecture.register.value">MaszynaPi.MachineLogic.Architecture.Register.Value</a> field inside <a href="#maszynapi.machinelogic.architecture.arithmeticlogicunit.ak">MaszynaPi.MachineLogic.Architecture.ArithmeticLogicUnit.AK</a> register.

### MaszynaPi.Defines.DEFAULT_ARCHITECTURE

Default <a href="#architecture">Architecture</a> mode, used when applicaiton is started.

### MaszynaPi.Defines.DEFAULT_BUS_VAL

Default value of <a href="#maszynapi.machinelogic.architecture.bus.value">MaszynaPi.MachineLogic.Architecture.Bus.Value</a> field.

### MaszynaPi.Defines.DEFAULT_CODE_BITS

Default number of bits used as opcode part of machine's WORD.

### MaszynaPi.Defines.DEFAULT_IO_NUMBER

Number of IO devices wihout <a href="#components.extendedio">Components.ExtendedIO</a> mode enabled.

### MaszynaPi.Defines.DEFAULT_MEM_VAL

Default value of <a href="#maszynapi.machinelogic.architecture.memory.content">MaszynaPi.MachineLogic.Architecture.Memory.Content</a> element.

### MaszynaPi.Defines.DEFAULT_REG_VAL

Default value of <a href="#maszynapi.machinelogic.architecture.register.value">MaszynaPi.MachineLogic.Architecture.Register.Value</a> field.

### MaszynaPi.Defines.EXTENDED_IO_NUMBER

Number of IO devices in <a href="#components.extendedio">Components.ExtendedIO</a> mode (Excluding <a href="#default_io_number">DEFAULT_IO_NUMBER</a>).

### MaszynaPi.Defines.FETCH_SIGNALS

List of signals () used to fetch instruciton from memory (those remains always the same, becaouse fetch cycle uops do not depends on which instruction being executed).

### MaszynaPi.Defines.G_REG_BIT_SIZE

Hardcoded size (in bits) of <a href="#maszynapi.machinelogic.centralprocessingunit.g">MaszynaPi.MachineLogic.CentralProcessingUnit.G</a> register.

### MaszynaPi.Defines.INSTRUCTION_ARGSNUM_HEADER

Reffers to instruction set definition (*.lst) file's segment, that marks definition of number of arguments that instruction takes. Note: For standarization purposes, assigned string 

### MaszynaPi.Defines.INSTRUCTION_NAME_HEADER

Reffers to instruction set definition (*.lst) file's segment, that marks definition of instruction name. Note: For standarization purposes, assigned string 

### MaszynaPi.Defines.INTERRUPTIONS_NUM

Amout of possible interruptions sources.

### MaszynaPi.Defines.JOYSTICK_INTERRUPTS

Hardcoded pirority of interrrupts from different joystick movements (refers to <a href="#architecture.machinepi">Architecture.MachinePI</a> architecture).

### MaszynaPi.Defines.KEYWORD_CONST_VAR

Defines assembly keyword for defining referable constant (value written into program data segment after compilation)

### MaszynaPi.Defines.KEYWORD_MEM_ALLOC

Defines assembly keyword for allocating word-sized part of memory to use as variable.


## Lang

Represents supproted languages version of application, (affects assembly and micrioinstructions code syntax).

### ENG

English

### PL

Polish

### MaszynaPi.Defines.LangInUse

Stores language version curenntly in use.

### MaszynaPi.Defines.RB_REG_BIT_SIZE

Hardcoded size (in bits) of <a href="#maszynapi.machinelogic.centralprocessingunit.rb">MaszynaPi.MachineLogic.CentralProcessingUnit.RB</a> register.

### MaszynaPi.Defines.SetInstructionsLanguageVersion(lang)

Assigns each of 'static string' fields their lang-specific value via private setter.

| Name | Description |
| ---- | ----------- |
| lang | *MaszynaPi.Defines.Lang*<br><a href="#lang">Lang</a> enum to be used in programming syntax. |

### MaszynaPi.Defines.SIGNAL_LABEL_PREFIX

Instruction-definition-language label prefix character.

### MaszynaPi.Defines.SIGNAL_STATEMENT_ELSE

Instruction-definition-language keyword, defining 'ELSE' part of conditional (if-then-else-end) statement.

### MaszynaPi.Defines.SIGNAL_STATEMENT_END

Instruction-definition-language keyword, defining 'END' part of conditional (if-then-else-end) statement.

### MaszynaPi.Defines.SIGNAL_STATEMENT_IF

Instruction-definition-language keyword, defining 'IF' part of conditional (if-then-else-end) statement.

### MaszynaPi.Defines.SIGNAL_STATEMENT_THEN

Instruction-definition-language keyword, defining 'THEN' part of conditional (if-then-else-end) statement.

### MaszynaPi.Defines.SIGNAL_TEST_IO_READY

Instruction-definition-language keyword, used when checking if IO device signalized ready state.

### MaszynaPi.Defines.STATMENT_ARG_POSITION

Position of argument (after space string split) in conditional statement syntax in instruction-definition-language.


## FileHandlerException

General exception of <a href="#fileshandler">FilesHandler</a> class


## FilesHandler

Static class providing methods for handling displaing specific <a href="#system.windows.forms.filedialog">System.Windows.Forms.FileDialog</a> to user.

### GetEncoding(filename, defaultEncoding)

Determines a text file's encoding by analyzing its byte order mark (BOM). Defaults set by "defaultEncoding" param when detection of the text file's endianness fails.  Supprots: - <a href="#system.text.encoding.utf7">System.Text.Encoding.UTF7</a>- <a href="#system.text.encoding.utf8">System.Text.Encoding.UTF8</a>- <a href="#system.text.encoding.utf32">System.Text.Encoding.UTF32</a>- <a href="#system.text.encoding.unicode">System.Text.Encoding.Unicode</a>- <a href="#system.text.encoding.bigendianunicode">System.Text.Encoding.BigEndianUnicode</a>- <a href="#system.text.utf32encoding.#ctor(system.boolean,system.boolean)">System.Text.UTF32Encoding.#ctor(System.Boolean,System.Boolean)</a>

| Name | Description |
| ---- | ----------- |
| filename | *System.String*<br>Path pointing to file which encoding should be get. |
| defaultEncoding | *System.Text.Encoding*<br>Encoding that should be assumed if file is saved with none of known encodings. |

#### Returns

Detected <a href="#system.text.encoding">System.Text.Encoding</a> object.

### GetFileText(filepath)

Reads whole contents of file under given filepath/

| Name | Description |
| ---- | ----------- |
| filepath | *System.String*<br> |

#### Returns

Content read by <a href="#system.io.file.readalltext(system.string,system.text.encoding)">System.IO.File.ReadAllText(System.String,System.Text.Encoding)</a>

*FileHandlerException:* 

### OverwriteOrCreateFile(content, filepath)

Creates or overwrites file pointed by filepath with content.

| Name | Description |
| ---- | ----------- |
| content | *System.Object*<br>Data to write to file. <a href="#system.string">System.String</a> and <a href="#system.collections.generic.list\`1">System.Collections.Generic.List\`1</a> are supproted. |
| filepath | *System.String*<br>Path to file to be written. |

*FileHandlerException:* 

### PointFileAndGetPath(dialogFilter, filepath)

Opens <a href="#system.windows.forms.openfiledialog">System.Windows.Forms.OpenFileDialog</a> with passed dialogFilter and assign path of selected file to filepath.

| Name | Description |
| ---- | ----------- |
| dialogFilter | *System.String*<br><a href="#system.windows.forms.openfiledialog">System.Windows.Forms.OpenFileDialog</a> filter string. |
| filepath | *System.String@*<br>Output parameter which will store path to pointed file or <a href="#system.string.empty">System.String.Empty</a> if no file selected. |

#### Returns

true if file was selected, false otherwise.

### PointFileAndGetText(dialogFilter, filepath, fileContent)

Calls <a href="#fileshandler.pointfileandgetpath(system.string,system.string@)">FilesHandler.PointFileAndGetPath(System.String,System.String@)</a> with dialogFilter and filepath and retreives content of that file into fileContent output variable.

| Name | Description |
| ---- | ----------- |
| dialogFilter | *System.String*<br><a href="#system.windows.forms.savefiledialog">System.Windows.Forms.SaveFileDialog</a> filter string. |
| filepath | *System.String@*<br>Output parameter which will store path to pointed file or <a href="#system.string.empty">System.String.Empty</a> if no file selected. |
| fileContent | *System.String@*<br>Output variable for read file content. |

#### Returns

true if file was read properly, false otherwise.

*FileHandlerException:* 

### PointToOvervriteFileOrCreateNew(dialogFilter, filepath)

Allows to get filepath from user using <a href="#system.windows.forms.savefiledialog">System.Windows.Forms.SaveFileDialog</a>. Path of file is assigned to filepath param.

| Name | Description |
| ---- | ----------- |
| dialogFilter | *System.String*<br><a href="#system.windows.forms.savefiledialog">System.Windows.Forms.SaveFileDialog</a> filter string. |
| filepath | *System.String@*<br>Output parameter which will store path to pointed file or <a href="#system.string.empty">System.String.Empty</a> if no file selected. |

#### Returns

true if file to save was selected, false otherwise.

### RemoveExcessiveEmptyStrings(lines)

Removes one empty string between each string of len greater than 0 (if exist) (leftovers from code-to-List processing on win) ["xx","","yy","","","zz"] -> ["xx","yy","","zz"]

| Name | Description |
| ---- | ----------- |
| lines | *System.Collections.Generic.List{System.String}*<br>List to process |

#### Returns

New instance of wihout newline-split empty strings.


## Form1

Application main <a href="#system.windows.forms.form">System.Windows.Forms.Form</a>, handling CPU, IOs and Editors views.

### Constructor

Creates application main <a href="#system.windows.forms.form">System.Windows.Forms.Form</a>.

### components

Wymagana zmienna projektanta.

### CPUProgramExecuting

Indicates that <a href="#machinelogic.centralprocessingunit.executeprogram">MachineLogic.CentralProcessingUnit.ExecuteProgram</a> was called.

### Dispose(disposing)

Wyczyść wszystkie używane zasoby.

| Name | Description |
| ---- | ----------- |
| disposing | *System.Boolean*<br>prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku. |

### GetManualActiveSignals

Machine manual control -> each <a href="#machineui.usercontrolsignalwire.active">MachineUI.UserControlSignalWire.Active</a> signal adds its name to a list, which is then sorted (<a href="#form1.sortsignals(system.collections.generic.list{system.string})">Form1.SortSignals(System.Collections.Generic.List{System.String})</a>) and pass to the machine for execution using <a href="#machinelogic.centralprocessingunit.manualtick(system.collections.generic.list{system.string})">MachineLogic.CentralProcessingUnit.ManualTick(System.Collections.Generic.List{System.String})</a> method. Note: only "Tick" step possible with manual control enabled.

#### Returns

List of names of signals activated by user, sored by <a href="#form1.sortsignals(system.collections.generic.list{system.string})">Form1.SortSignals(System.Collections.Generic.List{System.String})</a>.

### InitializeComponent

Metoda wymagana do obsługi projektanta — nie należy modyfikować jej zawartości w edytorze kodu.

### LastUsedFilepath

Last path used in file-open/save operation.

### MachineComponents

All CPU view <a href="#system.windows.forms.usercontrol">System.Windows.Forms.UserControl</a>s.

### PaintActiveSignals

Indicates that <a href="#form1">Form1</a> should repaint all controls with <a href="#machineui.usercontrolsignalwire.active">MachineUI.UserControlSignalWire.Active</a> field set.

### SignalWires

List of all <a href="#machineui.usercontrolsignalwire">MachineUI.UserControlSignalWire</a> objects in CPU view.

### SortSignals(activeSignals)

Sorts the microinstruction signals so that register output signals are always prioritized.

| Name | Description |
| ---- | ----------- |
| activeSignals | *System.Collections.Generic.List{System.String}*<br>List of signals names to be sorted. |

#### Returns

Sorted list of signal names: output->transit->input->special signals.

### MaszynaPi.FormProjectOptions.Constructor(onlyPaths)

Creates new <a href="#system.windows.forms.form">System.Windows.Forms.Form</a>, with editable application settings.

| Name | Description |
| ---- | ----------- |
| onlyPaths | *System.Boolean*<br>Collection of <a href="#system.windows.forms.textbox">System.Windows.Forms.TextBox</a>es containing interruption handlers adresses. |

### MaszynaPi.FormProjectOptions.components

Required designer variable.

### MaszynaPi.FormProjectOptions.Dispose(disposing)

Clean up any resources being used.

| Name | Description |
| ---- | ----------- |
| disposing | *System.Boolean*<br>true if managed resources should be disposed; otherwise, false. |

### MaszynaPi.FormProjectOptions.InitializeComponent

Required method for Designer support - do not modify the contents of this method with the code editor.

### MaszynaPi.FormProjectOptions.ONLY_PATHS

Indicates that only <a href="#formprojectoptions.tabpagepaths">FormProjectOptions.tabPagePaths</a> should be shown.

### MaszynaPi.FormProjectOptions.textBoxesINTAddr

Collection of <a href="#system.windows.forms.textbox">System.Windows.Forms.TextBox</a>es containing interruption handlers adresses.


## Logger

Custom logging class, created for development purposes, currently not used. All fields and methods are commented due to projects' finalization.


## Assembler

Class responsible for compilation - changing the string from the Code Editor to a form understandable for the machine. In this case, list of numbers as (opcode shifted left by addressBits bitwaise anded with argument) Compiler uses the static class InstructionSetDecoder - instructions opcodes are identified by searching for them in the opcode map (Dictionary InstructionNames) The code is stored in two versions:  1. Loaded as numbers into memory [List of uints]  2. Translated into a sequence of signals [List of Lists of strings][OLD - UNUSED]

### CompileCode(codeLines)

Creates compiled program as list of unsigned integer values to be load into Machine memory

| Name | Description |
| ---- | ----------- |
| codeLines | *System.Collections.Generic.List{System.String}*<br>Processed lines of Machine assembly code. |

#### Returns

Compiled to Machine Code program, ready to be loaded into memory.

*CompilerException:* 

### DeleteMultipleSpaces(s)

Replaces all multi-spaces instances with single space character.

| Name | Description |
| ---- | ----------- |
| s | *System.String*<br>Input string |

#### Returns

New, processed string with single spaces.

### GetLabelsAddresses(codeLines)

Gets adressses for Procedures, Variables and Constans Labels and loads them into dictionary where <a href="#system.collections.generic.dictionary\`2.keys">System.Collections.Generic.Dictionary\`2.Keys</a> are Names Of Labels and <a href="#system.collections.generic.dictionary\`2.values">System.Collections.Generic.Dictionary\`2.Values</a> are Addresses.

| Name | Description |
| ---- | ----------- |
| codeLines | *System.Collections.Generic.List{System.String}*<br>Processed lines of code. |

#### Returns

Created <a href="#system.collections.generic.dictionary\`2">System.Collections.Generic.Dictionary\`2</a> filled with asseembly components adresses.

*CompilerException:* 

### GetLableName(System.String)

Returns label of variable component of line.

### GetProgramSize(codeLines)

Returns expected program size after compilation - sum of lines, declarations 

| Name | Description |
| ---- | ----------- |
| codeLines | *System.Collections.Generic.List{System.String}*<br>Processed lines of code. |

#### Returns

Program size as number of words (size of word: <a href="#maszynapi.machinelogic.architecturesettings.getwordbits">MaszynaPi.MachineLogic.ArchitectureSettings.GetWordBits</a>).

### IsConstDeclaration(System.String)

Checks if passed line contais <a href="#maszynapi.defines.keyword_const_var">MaszynaPi.Defines.KEYWORD_CONST_VAR</a> value.

### IsMemoryAlocation(System.String)

Checks if passed line contais <a href="#maszynapi.defines.keyword_mem_alloc">MaszynaPi.Defines.KEYWORD_MEM_ALLOC</a> value.

### RemoveLabels(System.Collections.Generic.List{System.String})

Modifies codeLines argument to not contain labels in any line


## Debugger

Class allows for mapping current machine state to specific lines of assembly code and microinstructions.

### Constructor

Creates <a href="#debugger">Debugger</a> instance.

### ClearMemoryEditorMap

Clears <a href="#debugger.memorylinenumbermap">Debugger.MemoryLineNumberMap</a> dictionary.

### CodeLinesHandle

List of all lines of code from editor.

### FillMemoryLineNumberMap

Fills <a href="#debugger.memorylinenumbermap">Debugger.MemoryLineNumberMap</a> with memory word adress corresponding to number of code line. To be called after compilation.

### FindLineNumber(start, content)

Gets 0 based index of nearest line (forward from 'start' praram) which contains specific string (invariant characers case) in <a href="#debugger.codelineshandle">Debugger.CodeLinesHandle</a> list.

| Name | Description |
| ---- | ----------- |
| start | *System.UInt32*<br>Number of line from which search should be started. |
| content | *System.String*<br>Substring to be found in code lines. |

#### Returns

0 based number of found line, -1 if not found.

### GetFirstCharIndexFromLine(lineindex)

Calculates index (where 0 is first characted of first element in <a href="#debugger.codelineshandle">Debugger.CodeLinesHandle</a>) of first character in given line.

| Name | Description |
| ---- | ----------- |
| lineindex | *System.Int32*<br>Index of line for which total leading characters number will be calculated. |

#### Returns

Number of total leading characters before first character of given <a href="#debugger.codelineshandle">Debugger.CodeLinesHandle</a>[lineindex].

*System.Exception:* 

### MemoryLineNumberMap

Dictionary of pairs (memory_address - number of code line in editor)

### OnSetExecutedLine

Action that should be performed when <a href="#debugger">Debugger</a> calculates current line. Assembly Code string index is passed and contents of line that is execuded.

### OnSetExecutedMicroinstructions

Action that should be performed when <a href="#debugger">Debugger</a> calculates current instruction's micrinstruction signals. Instruction opcode and list of active microinstructions signals names are passed.

### SetCodeEditorHandle(handle)

Assigns instance of List of code lines into internal <a href="#debugger.codelineshandle">Debugger.CodeLinesHandle</a>.

| Name | Description |
| ---- | ----------- |
| handle | *System.Collections.Generic.List{System.String}*<br>List object containing current assembly code. |

### SetExecutedLine(memAddress)

Calls assigned <a href="#debugger.onsetexecutedline">Debugger.OnSetExecutedLine</a><a href="#system.action">System.Action</a> for passed executed memAddress if <a href="#debugger.memorylinenumbermap">Debugger.MemoryLineNumberMap</a> contais this address data and any code exist in assigned to<a href="#debugger.codelineshandle">Debugger.CodeLinesHandle</a> list.

| Name | Description |
| ---- | ----------- |
| memAddress | *System.UInt32*<br>In-Memory Address of currently executed instruction. |

### SetExecutedMicronstructions(opcode, activeSignals)

Calls <a href="#system.action">System.Action</a> assigned to <a href="#debugger.onsetexecutedmicroinstructions">Debugger.OnSetExecutedMicroinstructions</a> with passed in argument opcode and list of activeSignals.

| Name | Description |
| ---- | ----------- |
| opcode | *System.UInt32*<br>Opcode of currently execuded instruction. |
| activeSignals | *System.Collections.Generic.List{System.String}*<br>List containing names of microinstructions active on current instruction. |


## InstructionLoader

Class providing methods for parsing Instruction files, written in .ini-like format. Syntax of instruction set files was preserved to assure backward compability with previous simulator application.

### ChangeCapitalizationOfInstructionDefinitionKeywords(toUpper)

Changes capitalization specific words defined in <a href="#instructionloader.uinstdefkeywords">InstructionLoader.uInstDefKeywords</a> to upper (if toUpper == true) or lower. Used mainly for displaing avaible instructions definitions in <a href="#maszynapi.machineui.usercontrolinstructionmicrocode">MaszynaPi.MachineUI.UserControlInstructionMicrocode</a>.

| Name | Description |
| ---- | ----------- |
| toUpper | *System.Boolean*<br>Set to 'true' (default) to capitalize instruction definition keywords using <a href="#system.string.toupper">System.String.ToUpper</a> method, 'false' to perform <a href="#system.string.tolower">System.String.ToLower</a>. |

### ClearLoadedInstructions

Clears all lists containing loaded instruction set information; instruction definitions, opcoded, and uOps.

### DeleteComment(line)

If line contains <a href="#instructionloader.comment">InstructionLoader.COMMENT</a> substring, deletes all characters after that symbol (including it).

| Name | Description |
| ---- | ----------- |
| line | *System.String*<br>Line that contiains potential valid comment. |

#### Returns

New <a href="#system.string">System.String</a> instance wihout potential comment substring, or original line if it did not contain any comment.

### GetAvaibleInstructionsNames

Creates list of names of all avaible (loaded/declared) instructions (NOT considering the currently set code bits value - returned instruction count might be larger than maximum that can be encoded with currently set opcode bit size).

#### Returns

List of strings, where each element is name of one of all avaible instruction.

### GetInstructionSignalsMap

#### Returns

Mapping Instructions names to list of lists of their's uOps signals (each list of uops defines set of them than can be executed in single clock cycle)

### GetInstructionsLines

#### Returns

Mapping of instruction names to their uOps definitions (lines from file, used for <a href="#maszynapi.machineui.usercontrolinstructionmicrocode">MaszynaPi.MachineUI.UserControlInstructionMicrocode</a>)

### GetInstructionsNamesOpcodes

#### Returns

Mapping Instructions names to their opcodes (as unsigned integers).

### GetZeroArgInstructions

#### Returns

Names of instructions that takes no arguments.

### InstructionsLines

Mapping of instruction names to their uOps definitions (lines from file, used for <a href="#maszynapi.machineui.usercontrolinstructionmicrocode">MaszynaPi.MachineUI.UserControlInstructionMicrocode</a>)

### InstructionsNamesOpcodes

Mapping Instructions names to their opcodes (as unsigned integers).

### InstructionsSignalsMap

Mapping Instructions names to list of lists of their's uOps signals (each list of uops defines set of them than can be executed in single clock cycle)

### IsStatementValid(sigline)

Checks if conditional statement written in instruction definition language has valid syntax.

| Name | Description |
| ---- | ----------- |
| sigline | *System.Collections.Generic.List{System.String}*<br>List of statements from single line of instruction definition. |

#### Returns

'true' if it is not a conditional statement or statemets passed in parameter are in valid order (if [arg] then @label else @label || if [arg] then @label)

### IsValidStartOfInstruction(uOpsLine)

Checks if instruction starts with valid set of fetch microoperations.

| Name | Description |
| ---- | ----------- |
| uOpsLine | *System.Collections.Generic.List{System.String}*<br>Single clock cycle set of microoperations from insturction definition. |

#### Returns

'true' if passed instruction definition part contains valid fetch sequence (defined in <a href="#maszynapi.defines.fetch_signals">MaszynaPi.Defines.FETCH_SIGNALS</a>).

### LoadBaseInstructions

Method loads base instructions set, using instructions defined in <a href="#maszynapi.properties.resources.podstawa">MaszynaPi.Properties.Resources.Podstawa</a> or <a href="#maszynapi.properties.resources.base">MaszynaPi.Properties.Resources.Base</a> (dependins on currently set <a href="#maszynapi.defines.langinuse">MaszynaPi.Defines.LangInUse</a> language). Uses <a href="#instructionloader.loadinstructionset(system.collections.generic.list{system.string})">InstructionLoader.LoadInstructionSet(System.Collections.Generic.List{System.String})</a> method.

#### Returns

'true' if all instructions could be encoded using currently set size of opcode (<a href="#maszynapi.machinelogic.architecturesettings.getmaxopcode">MaszynaPi.MachineLogic.ArchitectureSettings.GetMaxOpcode</a>), false otherwise.

*InstructionLoaderException:* 

### LoadInstructionSet(lines)

Parses passed in lines content of <a href="#instructionloader.instruction_set_file_extension">InstructionLoader.INSTRUCTION_SET_FILE_EXTENSION</a> file, and fills specific instruction mappings/lists acordingly. Creates new instances for:  - <a href="#instructionloader.instructionslines">InstructionLoader.InstructionsLines</a> - <a href="#instructionloader.instructionssignalsmap">InstructionLoader.InstructionsSignalsMap</a> - <a href="#instructionloader.instructionsnamesopcodes">InstructionLoader.InstructionsNamesOpcodes</a> - <a href="#instructionloader.zeroarginstructions">InstructionLoader.ZeroArgInstructions</a> Throws <a href="#instructionloaderexception">InstructionLoaderException</a> if parsing fails.

| Name | Description |
| ---- | ----------- |
| lines | *System.Collections.Generic.List{System.String}*<br>Contents of <a href="#instructionloader.instruction_set_file_extension">InstructionLoader.INSTRUCTION_SET_FILE_EXTENSION</a> file in form of list of its lines (splitted by new line symbol(s) content of .lst file) |

#### Returns

'true' if all instructions could be encoded using currently set size of opcode (<a href="#maszynapi.machinelogic.architecturesettings.getmaxopcode">MaszynaPi.MachineLogic.ArchitectureSettings.GetMaxOpcode</a>), false otherwise.

*InstructionLoaderException:* 

### LoadInstructionsFromFileContent(instructions)

Loads new set of instructions basing on provided instruction file content (instructions). Uses <a href="#instructionloader.loadinstructionset(system.collections.generic.list{system.string})">InstructionLoader.LoadInstructionSet(System.Collections.Generic.List{System.String})</a> method.

| Name | Description |
| ---- | ----------- |
| instructions | *System.String*<br>Contents of <a href="#instructionloader.instruction_file_extension">InstructionLoader.INSTRUCTION_FILE_EXTENSION</a> instruction set file. |

#### Returns

'true' if all instructions could be encoded using currently set size of opcode (<a href="#maszynapi.machinelogic.architecturesettings.getmaxopcode">MaszynaPi.MachineLogic.ArchitectureSettings.GetMaxOpcode</a>), false otherwise.

*InstructionLoaderException:* 

### LoadSingleInstruction(lines)

Adds single instruction into existing instruction set.  Throws <a href="#instructionloaderexception">InstructionLoaderException</a> if instruciton parsing failed.

| Name | Description |
| ---- | ----------- |
| lines | *System.Collections.Generic.List{System.String}*<br>List of lines containing definition of single instruction. |

#### Returns

'true' if new instruction can be encoded with currently set size of opcode, 'false' otherwise.

*InstructionLoaderException:* 

### LoadSingleInstructionFromFileContent(instructions)

Allows to load single instruction using passed file's content instructions as insturction definition source.

| Name | Description |
| ---- | ----------- |
| instructions | *System.String*<br>Contents of <a href="#instructionloader.instruction_file_extension">InstructionLoader.INSTRUCTION_FILE_EXTENSION</a> file containing definition of single instruction. |

#### Returns

'true' if new instruction can be encoded with currently set size of opcode, 'false' otherwise.

*InstructionLoaderException:* 

### SetOptions(options)

Sets related <a href="#maszynapi.machinelogic.architecturesettings">MaszynaPi.MachineLogic.ArchitectureSettings</a> fields (AddressSpace, CodeBits) according to <a href="#instructionloader.options_header">InstructionLoader.OPTIONS_HEADER</a> section content.

| Name | Description |
| ---- | ----------- |
| options | *System.Collections.Generic.List{System.String}*<br>Part of <a href="#instructionloader.instruction_set_file_extension">InstructionLoader.INSTRUCTION_SET_FILE_EXTENSION</a> file containing <a href="#instructionloader.options_header">InstructionLoader.OPTIONS_HEADER</a> section. |

*InstructionLoaderException:* 

### StandarizeLines(lines)

Removes all null or white space elements from lines and converts all strings to lowercase.

| Name | Description |
| ---- | ----------- |
| lines | *System.Collections.Generic.List{System.String}*<br>Instruction definition text, splitted into lines. |

#### Returns

New standarized List instance.

### uInstDefKeywords

List of keywords for microinstruction definition pseoudo-language

### ZeroArgInstructions

Names of instructions that takes no arguments.


## InstructionLoaderException

Generic <a href="#system.exception">System.Exception</a> overload, for throwing errors related to <a href="#instructionloader">InstructionLoader</a> class.


## ALUFlags

Supported flags bits of Flag Register of Arithmetic Logic Unit.

### INT

Interruption requested

### V

Not used

### Z

Sign bit (MSB) in accumulator register is set to 1 (negative value).

### ZAK

Value in accumulator register equals 0


## ArithmeticLogicUnit

Class represent <a href="#maszynapi.machinelogic.centralprocessingunit">MaszynaPi.MachineLogic.CentralProcessingUnit</a>'s ALU. Allows to perform simple calculations using arithmetic/logic operations between values of internal "registers" <a href="#arithmeticlogicunit.operanda">ArithmeticLogicUnit.OperandA</a> and <a href="#arithmeticlogicunit.operandb">ArithmeticLogicUnit.OperandB</a>. Results of those operations are always stored into <a href="#arithmeticlogicunit.operanda">ArithmeticLogicUnit.OperandA</a> and then passed into <a href="#arithmeticlogicunit.ak">ArithmeticLogicUnit.AK</a> - an result <a href="#register">Register</a>, which value can be publicly accessed. <a href="#arithmeticlogicunit">ArithmeticLogicUnit</a> class implements way to get information about last operation's result and CPU's state via internal <a href="#arithmeticlogicunit.flagregister">ArithmeticLogicUnit.FlagRegister</a> containing info about currnetly set <a href="#aluflags">ALUFlags</a>.

### Constructor(ak, value)

Creates <a href="#arithmeticlogicunit">ArithmeticLogicUnit</a> instance. Calls <a href="#arithmeticlogicunit.autosetflags">ArithmeticLogicUnit.AutoSetFlags</a> to sets <a href="#arithmeticlogicunit.flagregister">ArithmeticLogicUnit.FlagRegister</a> based on current ALU registers state.

| Name | Description |
| ---- | ----------- |
| ak | *MaszynaPi.MachineLogic.Architecture.Register*<br>Handle to result register. |
| value | *System.UInt32*<br>Inital value of internal operands. |

### Add

Adds ALU's <a href="#arithmeticlogicunit.operanda">ArithmeticLogicUnit.OperandA</a> and <a href="#arithmeticlogicunit.operandb">ArithmeticLogicUnit.OperandB</a> values and stores result in A.

### AK

ALU's operation result register. Values can be accessed only via this register.

### And

Performs bitwise AND between ALU's <a href="#arithmeticlogicunit.operanda">ArithmeticLogicUnit.OperandA</a> and <a href="#arithmeticlogicunit.operandb">ArithmeticLogicUnit.OperandB</a> values. Stores result in A.

### AutoSetFlags

Allows to set <a href="#arithmeticlogicunit.flagregister">ArithmeticLogicUnit.FlagRegister</a> based on current ALU registers content.

### ClearFlags(flags)

Clears passed <a href="#aluflags">ALUFlags</a> from internal <a href="#arithmeticlogicunit.flagregister">ArithmeticLogicUnit.FlagRegister</a> register.

| Name | Description |
| ---- | ----------- |
| flags | *MaszynaPi.MachineLogic.Architecture.ALUFlags*<br> |

### Dec

Performs decrementation of ALU's <a href="#arithmeticlogicunit.operanda">ArithmeticLogicUnit.OperandA</a> register value.

### Div

Divides ALU's <a href="#arithmeticlogicunit.operanda">ArithmeticLogicUnit.OperandA</a> by <a href="#arithmeticlogicunit.operandb">ArithmeticLogicUnit.OperandB</a> value and stores integer result in A.

### EncodedFlags

<a href="#aluflags">ALUFlags</a> encoded as bitwise XOR of their string representation in lowercase ASCII letters -> used for conditional statments in instruction microoperations.

### FlagRegister

Internal Flag register, holding information about ALU's and CPU's state.

### GetFlags

#### Returns

ALU's flag register content.

### Inc

Performs incrementation of ALU's <a href="#arithmeticlogicunit.operanda">ArithmeticLogicUnit.OperandA</a> register value.

### IsFlagSet(System.Int32)

#### Returns

'true' if passed flag value is currently set in internal flag register, 'false' otherwises.

### IsFlagSet(flag)

Allows to check if ALU's flag register, has flag defined by flag stirng set (see <a href="#arithmeticlogicunit.encodedflags">ArithmeticLogicUnit.EncodedFlags</a>). ()

| Name | Description |
| ---- | ----------- |
| flag | *System.String*<br>Name of specific ALU flag. |

#### Returns

'true' if passed flag value is currently set in internal flag register, 'false' otherwises.

### Mul

Multiplies ALU's <a href="#arithmeticlogicunit.operanda">ArithmeticLogicUnit.OperandA</a> and <a href="#arithmeticlogicunit.operandb">ArithmeticLogicUnit.OperandB</a> values and stores result in A.

### Nop

As no operation, assigns <a href="#arithmeticlogicunit.operandb">ArithmeticLogicUnit.OperandB</a> value to <a href="#arithmeticlogicunit.operanda">ArithmeticLogicUnit.OperandA</a>.

### Not

Performs bitwise negation of ALU's <a href="#arithmeticlogicunit.operanda">ArithmeticLogicUnit.OperandA</a> register value.

### OperandA

One of two (A, B) internal register-like value instance, for storing operations results and operands during calculations.

### OperandB

One of two (A, B) internal register-like value instance, for storing operations results and operands during calculations.

### Or

Performs bitwise OR between ALU's <a href="#arithmeticlogicunit.operanda">ArithmeticLogicUnit.OperandA</a> and <a href="#arithmeticlogicunit.operandb">ArithmeticLogicUnit.OperandB</a> values. Stores result in A.

### Reset

Resets state of all internal component to their default state.

### SetFlags(flags)

Allows to manually set specific ALU's flag register <a href="#aluflags">ALUFlags</a>.

| Name | Description |
| ---- | ----------- |
| flags | *MaszynaPi.MachineLogic.Architecture.ALUFlags*<br><a href="#aluflags">ALUFlags</a> to set in <a href="#arithmeticlogicunit.flagregister">ArithmeticLogicUnit.FlagRegister</a> |

### SetOperandB(value)

Allows to set value of second operand of operation.

| Name | Description |
| ---- | ----------- |
| value | *System.UInt32*<br> |

### SetResult

Assings value of first operand (<a href="#arithmeticlogicunit.operanda">ArithmeticLogicUnit.OperandA</a>) to <a href="#arithmeticlogicunit.ak">ArithmeticLogicUnit.AK</a>.

### SetResultAndFlags

Sets value of flag and result registers (<a href="#arithmeticlogicunit.flagregister">ArithmeticLogicUnit.FlagRegister</a>, <a href="#arithmeticlogicunit.ak">ArithmeticLogicUnit.AK</a>), base on internal operands state/values.

### Shr

Performs bitwise right shif of ALU's <a href="#arithmeticlogicunit.operanda">ArithmeticLogicUnit.OperandA</a> value, using <a href="#arithmeticlogicunit.operandb">ArithmeticLogicUnit.OperandB</a> value as shift amount. Stores result in A.

### Sub

Subtracts ALU's <a href="#arithmeticlogicunit.operandb">ArithmeticLogicUnit.OperandB</a> from <a href="#arithmeticlogicunit.operanda">ArithmeticLogicUnit.OperandA</a> values and stores result in A.


## Bus

<a href="#register">Register</a>-like class representing Machines' <a href="#bus">Bus</a>. Allows to specify current data on bus using <a href="#bus.value">Bus.Value</a> and bus width with <a href="#bus.bitsize">Bus.Bitsize</a>. It can have <a href="#bus.empty">Bus.EMPTY</a> value assigned to represent no-value,  state.

### Constructor(bitsize, name)

Initializes new <a href="#bus">Bus</a> instance, with <a href="#bus.empty">Bus.EMPTY</a> value, of allowed bitsize size.

| Name | Description |
| ---- | ----------- |
| bitsize | *System.UInt32*<br>"width" of bus, defines maximum size of internal <a href="#bus.value">Bus.Value</a>. |
| name | *System.String*<br>String to be set as name of <a href="#bus">Bus</a> instance (address/data/transitive). |

### GetBitsize

#### Returns

Internal <a href="#bus.bitsize">Bus.Bitsize</a> of this bus instance.

### GetValue

#### Returns

Returns <a href="#bus.value">Bus.Value</a> if bus is not empty, <a href="#busexception">BusException</a> is thrown otherwise.

*BusException:* 

### IsEmpty

Checks if <a href="#bus">Bus</a> is empty.

#### Returns

True if bus <a href="#bus.value">Bus.Value</a> is equal to <a href="#bus.empty">Bus.EMPTY</a> constans, false otherwise.

### Name

String representing name of <a href="#bus">Bus</a> instance (address/data/transitive)

### Reset

Allows to reset this bus instance state by calling <a href="#bus.setempty">Bus.SetEmpty</a> method on itself.

### SetBitsize(bitsize)

Sets <a href="#bus.bitsize">Bus.Bitsize</a> parameter (width) of bus.

| Name | Description |
| ---- | ----------- |
| bitsize | *System.UInt32*<br>Bit size (width) to be set. |

### SetEmpty

Sets special <a href="#bus.empty">Bus.EMPTY</a> value to represent high-impedance state.

### SetValue(value)

Allows to set integer value as bus <a href="#bus.value">Bus.Value</a>.

| Name | Description |
| ---- | ----------- |
| value | *System.Int32*<br>Integer to be assigned as bus <a href="#bus.value">Bus.Value</a>. |

### SetValue(System.UInt32)

Allows to set unsigned integer value as bus <a href="#bus.value">Bus.Value</a>. Passed value can have any <a href="#system.uint32">System.UInt32</a> value, but potential overflow will be simulated, before assigment, using <a href="#maszynapi.commonoperations.bitwise.handleoverflow(system.uint32,system.uint32)">MaszynaPi.CommonOperations.Bitwise.HandleOverflow(System.UInt32,System.UInt32)</a> method with <a href="#bus.bitsize">Bus.Bitsize</a> param.


## BusException

Generic <a href="#system.exception">System.Exception</a> for representing errors related to <a href="#bus">Bus</a> class issues.


## InstructionDecoder

Represents <a href="#maszynapi.machinelogic.centralprocessingunit">MaszynaPi.MachineLogic.CentralProcessingUnit</a>'s component responsible for decoding instructions into set of their underlaing microinstructions (uOperations).

### Constructor

Creates new instance of decoder.

### DecodeActiveSignals(instructionOpcode, tick)

Creates list consisting of names of uOps signals that should be active in current tick of currently executed instruction, specified by instructionOpcode. If any of instruction's micocode lines contains conditional statement, method sets internal <a href="#instructiondecoder.statementarg">InstructionDecoder.StatementArg</a>, <a href="#instructiondecoder.jumplabel">InstructionDecoder.JumpLabel</a> and <a href="#instructiondecoder.jumpindex">InstructionDecoder.JumpIndex</a> fields, that allows to control, instruction later flow.

| Name | Description |
| ---- | ----------- |
| instructionOpcode | *System.UInt32*<br>Opcode of currently executed instruction. |
| tick | *System.Int32*<br>Zero-based number of clock cycle, where cycle 0 is current instruction fetch cycle. |

#### Returns

New list of microinstructions that should be activated.

### GetJumpIndex(instructionSignals, label)

Calculates index of line (destination jump) within single instruction instructionSignals, that contains destination label.

| Name | Description |
| ---- | ----------- |
| instructionSignals | *System.Collections.Generic.List{System.Collections.Generic.List{System.String}}*<br>All microinstructions that creates currently decoded instruction. |
| label | *System.String*<br>Microcode destination jump label from conditional statement. |

#### Returns

Index of line in instructionSignals (main list index) that starts with label, -1 if not found.

### GetNextSignalsIndex(tickidx)

Allows to retreive index of next part of instruction's uOps that should be excuted. Base on passed current tickidx and internal <a href="#instructiondecoder.jumpindex">InstructionDecoder.JumpIndex</a> field (set in <a href="#instructiondecoder.decodeactivesignals(system.uint32,system.int32)">InstructionDecoder.DecodeActiveSignals(System.UInt32,System.Int32)</a> method).

| Name | Description |
| ---- | ----------- |
| tickidx | *System.Int32*<br>Index of signals set currently executed in pending instrucition. |

#### Returns

Index of instruction's set of microoperations, that should be set active on next clock cycle.

### GetNumberOfTicksInInstruction(instructionOpcode)

Calculates number of clock cycles that must be performed to execute instruction with opcode instructionOpcode.

| Name | Description |
| ---- | ----------- |
| instructionOpcode | *System.UInt32*<br>Opcode of processed instruction. |

#### Returns

Number of cycles neccessary to execute instruction defined by instructionOpcode.

### GetSignalStatementArgument(signals)

Sarches for argument argument of microcode conditional statement in passed signals list.

| Name | Description |
| ---- | ----------- |
| signals | *System.Collections.Generic.List{System.String}*<br>List of signals acive in current clock cycle. |

#### Returns

Found argument string or <a href="#system.string.empty">System.String.Empty</a> if conditional statement not found in signals.

### GetStatementJumpLabel(conditionalStatementResult, signals)

Searches for jump label in signals that should be next destination within single instruction's one-clock-cycle uOps signals definition. signals list must contain valid conditional statement with label, otherwise <a href="#maszynapi.machinelogic.cpuexception">MaszynaPi.MachineLogic.CPUException</a> is thrown.

| Name | Description |
| ---- | ----------- |
| conditionalStatementResult | *System.Boolean*<br>Calculated result of uOp contitional statement. |
| signals | *System.Collections.Generic.List{System.String}*<br>List of signals acive in current clock cycle. |

#### Returns

String with found destination label or <a href="#system.string.empty">System.String.Empty</a> if label was not found.

*MaszynaPi.MachineLogic.CPUException:* 

### OnRequestALUFlagState

Function hadle for getting from <a href="#arithmeticlogicunit">ArithmeticLogicUnit</a> specific <a href="#aluflags">ALUFlags</a> state (set/not as boolean true/false), specified as it's name in string format.

### RequestALUFlagState(flagName)

Performs null check and calls <a href="#instructiondecoder.onrequestaluflagstate">InstructionDecoder.OnRequestALUFlagState</a> with flagName.

| Name | Description |
| ---- | ----------- |
| flagName | *System.String*<br>Argument of microop conditional statement as name of ALU's flag. |

#### Returns

True if ALU's related flag (that have flagName name) is set, false otherwise.

*System.Exception:* 

### StatementArg

Stores argumet of currently decoded instruction's uOp pseudo-lang conditional statement.


## InstructionRegister

Special <a href="#register">Register</a> class representing CIR - Current instruction register. Responsible for storing and separating opcode and address/argument components of processed instruction.

### Constructor(addrBitsize, opcodeBitsize, value)

. Represents special <a href="#instructionregister">InstructionRegister</a> CPU's component.

| Name | Description |
| ---- | ----------- |
| addrBitsize | *System.UInt32*<br>Currently set size of address/argument component of instruction in bits |
| opcodeBitsize | *System.UInt32*<br>Currently set size of opcode component of instruction in bits |
| value | *System.UInt32*<br> |

### AD

Internal Instruction Argument register.

### DecodeInstruction

Decodes and separates currently stored instruction value into address and opcode registers.

### GetArgument

Allows to retreive argument component of stored instruction from internal <a href="#instructionregister.ad">InstructionRegister.AD</a> value.

#### Returns

Value of address/argument component of processed instruction.

### GetOpcode

Allows to retreive opcode component of stored instruction from internal <a href="#instructionregister.kod">InstructionRegister.KOD</a> value.

#### Returns

Value of opcode component of processed instruction.

### KOD

Internal Instruction Opcode register.

### Reset



### SetBitsize(addrBitsize, opcodeBitsize)

 Additionally set bitsize of internal address and opcode registers.

| Name | Description |
| ---- | ----------- |
| addrBitsize | *System.UInt32*<br> |
| opcodeBitsize | *System.UInt32*<br> |


## InterruptionController

Class represents <a href="#maszynapi.machinelogic.centralprocessingunit">MaszynaPi.MachineLogic.CentralProcessingUnit</a>'s component, called Interruption Controller. This unit performs all neccessary operations related to handling incoming/pending interrupts from diferent sources.

### Constructor(rz, rm, rp, ap)

Creates new interrupts controller instance.

| Name | Description |
| ---- | ----------- |
| rz | *MaszynaPi.MachineLogic.Architecture.Register*<br>Handle to Interrupt Report Register |
| rm | *MaszynaPi.MachineLogic.Architecture.Register*<br>Handle to Mask Register |
| rp | *MaszynaPi.MachineLogic.Architecture.Register*<br>Handle to Register of accepted interrupts |
| ap | *MaszynaPi.MachineLogic.Architecture.Register*<br>Handle to Interrupt Vector Register |

### AP

CPU's Interrupt Vector Register (<a href="#maszynapi.machinelogic.architecturesettings.codebits">MaszynaPi.MachineLogic.ArchitectureSettings.CodeBits</a>) bit size).

### ClearMSBOfAcceptedINTs

Clears bits related to accepted interrupt from <a href="#interruptioncontroller">InterruptionController</a> registers.

### INTJoystick

Sense-Hat joystick as optional, additional source of interrupts.

### OnInterruptReported

Action delegate invoked when new interruption is repored.

### ReportInterrupt(IntPriority)

Reports new interrupt of given IntPriority by setting corresponding bit of Interrupt Report register (<a href="#interruptioncontroller.rz">InterruptionController.RZ</a>).

| Name | Description |
| ---- | ----------- |
| IntPriority | *System.UInt32*<br>Interruption priority as appropriate power of 2 ([1/2/4/8]). |

### RM

CPU's 4 bit Mask Register.

### RP

CPU's 4 bit Register of accepted interrupts.

### RZ

CPU's 4 bit Interrupt Report Register.

### SetAcceptedAndINTVectorRegister(JAL)

Sets values of Accepted INT register and INT vector register (<a href="#interruptioncontroller.rp">InterruptionController.RP</a>, <a href="#interruptioncontroller.ap">InterruptionController.AP</a>) base on values of reported interrupts and mask registers (<a href="#interruptioncontroller.rm">InterruptionController.RM</a>, <a href="#interruptioncontroller.rz">InterruptionController.RZ</a>). Interrupt bit with the highest priority that is not masked, is put in the <a href="#interruptioncontroller.rp">InterruptionController.RP</a> and points to address of interrupt handle which is set as new <a href="#interruptioncontroller.ap">InterruptionController.AP</a> register value. Sets <a href="#aluflags.int">ALUFlags.INT</a> flag of <a href="#arithmeticlogicunit.flagregister">ArithmeticLogicUnit.FlagRegister</a> if any interrupt is accepted.

| Name | Description |
| ---- | ----------- |
| JAL | *MaszynaPi.MachineLogic.Architecture.ArithmeticLogicUnit*<br>Handle to CPU's <a href="#arithmeticlogicunit">ArithmeticLogicUnit</a> instance. |

### StartJoystickInterruptionMonitor

Starts async process of reading <a href="#interruptioncontroller.intjoystick">InterruptionController.INTJoystick</a> state as source of interrupts.

### StopJoystickInterruptionMonitor

Sends signal to terminates async proccess of reading <a href="#interruptioncontroller.intjoystick">InterruptionController.INTJoystick</a> state as source of interrupts.


## IODevicesController

Class representing input/output devices controller, providing <a href="#iodevicescontroller.handleioonstartsignal(system.uint32)">IODevicesController.HandleIOOnStartSignal(System.UInt32)</a> method for writing/reading data to/from particular <a href="#maszynapi.machinelogic.iodevices.iodevice">MaszynaPi.MachineLogic.IODevices.IODevice</a>.

### Constructor(devices)

Creates new <a href="#iodevicescontroller">IODevicesController</a> with internal set of passed IO devices.

| Name | Description |
| ---- | ----------- |
| devices | *MaszynaPi.MachineLogic.IODevices.IODevice[]*<br>Collection of available <a href="#maszynapi.machinelogic.iodevices.iodevice">MaszynaPi.MachineLogic.IODevices.IODevice</a> instances. |

### HandleIOOnStartSignal(IOAddress)

Invokes read/write IO buffer operation of <a href="#maszynapi.machinelogic.iodevices.iodevice">MaszynaPi.MachineLogic.IODevices.IODevice</a>, visible under IO address IOAddress (I/O base on <a href="#maszynapi.machinelogic.iodevices.iotype">MaszynaPi.MachineLogic.IODevices.IOType</a> of assosciated <a href="#maszynapi.machinelogic.iodevices.iodevice">MaszynaPi.MachineLogic.IODevices.IODevice</a>).

| Name | Description |
| ---- | ----------- |
| IOAddress | *System.UInt32*<br>Address of <a href="#maszynapi.machinelogic.iodevices.iodevice">MaszynaPi.MachineLogic.IODevices.IODevice</a> from IO read/write instruction. |


## Memory

Class representing Machines' data-program read/write memory.

### Constructor

Default constructor, calls <a href="#memory.initmemorycontent">Memory.InitMemoryContent</a> for setting <a href="#memory.content">Memory.Content</a>.

### Content

Internal contents of <a href="#memory">Memory</a> where each element is single machine word (<a href="#maszynapi.machinelogic.architecturesettings.getwordbits">MaszynaPi.MachineLogic.ArchitectureSettings.GetWordBits</a> irrelevat - always <a href="#system.uint32">System.UInt32</a> max).

### ExpandMemory(oldAddressSpace)

Expands <a href="#memory.content">Memory.Content</a>, base on <a href="#maszynapi.machinelogic.architecturesettings.getaddressspace">MaszynaPi.MachineLogic.ArchitectureSettings.GetAddressSpace</a> (should be called if address space was changed). All newly added elements are initialized with <a href="#maszynapi.defines.default_mem_val">MaszynaPi.Defines.DEFAULT_MEM_VAL</a>.

| Name | Description |
| ---- | ----------- |
| oldAddressSpace | *System.UInt32*<br>Old value of address space, for calculating amount of elements to add. |

### GetContentHandle

Allows to get internal list <a href="#memory.content">Memory.Content</a> handle, for specific User Interface purposes.

#### Returns

Instance of internal <a href="#memory.content">Memory.Content</a> list.

### GetValue(addr)

Retreives value from specific index (addr) of <a href="#memory.content">Memory.Content</a>. If passed addr is not in address space of <a href="#memory">Memory</a>, <a href="#memoryexception">MemoryException</a> is thrown.

| Name | Description |
| ---- | ----------- |
| addr | *System.UInt32*<br>Address (index) of value that should be read. |

#### Returns

Read value under <a href="#memory.content">Memory.Content</a>[addr]

*MemoryException:* 

### InitMemoryContent

Initializes <a href="#memory.content">Memory.Content</a> with <a href="#maszynapi.defines.default_mem_val">MaszynaPi.Defines.DEFAULT_MEM_VAL</a> of size <a href="#maszynapi.machinelogic.architecturesettings.getaddressspace">MaszynaPi.MachineLogic.ArchitectureSettings.GetAddressSpace</a>.

### Reset

Resets <a href="#memory">Memory</a> by calling <a href="#memory.initmemorycontent">Memory.InitMemoryContent</a> method.

### ShrinkMemory(oldAddressSpace)

Shrinks <a href="#memory.content">Memory.Content</a>, base on <a href="#maszynapi.machinelogic.architecturesettings.getaddressspace">MaszynaPi.MachineLogic.ArchitectureSettings.GetAddressSpace</a> (should be called if address space was changed). All deleted elements contents are lost.

| Name | Description |
| ---- | ----------- |
| oldAddressSpace | *System.UInt32*<br>Old value of address space, for calculating amount of elements to add. |

### StoreValue(addr, value)

Writes given value under specific address addr (where address is index of machine word). Written value is checked for potential overflow with <a href="#maszynapi.commonoperations.bitwise.handleoverflow(system.uint32,system.uint32)">MaszynaPi.CommonOperations.Bitwise.HandleOverflow(System.UInt32,System.UInt32)</a>. If passed addr is not in address space of <a href="#memory">Memory</a>, <a href="#memoryexception">MemoryException</a> is thrown.

| Name | Description |
| ---- | ----------- |
| addr | *System.UInt32*<br>Address representing index of machine word that should be updated (index of <a href="#memory.content">Memory.Content</a> list). |
| value | *System.UInt32*<br>Value that should be written into specific <a href="#memory.content">Memory.Content</a> index. |

*MemoryException:* 


## MemoryException

Generic <a href="#system.exception">System.Exception</a> for representing errors related to <a href="#memory">Memory</a> class issues.


## Register

Class representing basic read/write register.

### Constructor(bitsize, value)

Creates new <a href="#register">Register</a> instance, initialized with specific value that later can take maximum of (2^bitsize-1)

| Name | Description |
| ---- | ----------- |
| bitsize | *System.UInt32*<br>Initial Register size in bits. |
| value | *System.UInt32*<br>Initial value of register. Defaults to <a href="#maszynapi.defines.default_reg_val">MaszynaPi.Defines.DEFAULT_REG_VAL</a>. |

### Bitsize

Size of <a href="#register">Register</a> in bits, defining maximum value that can be stored.

### GetBitsize

#### Returns

Internal <a href="#register.bitsize">Register.Bitsize</a>

### GetValue

#### Returns

Internal <a href="#register.value">Register.Value</a>

### Reset

Resets <a href="#register.value">Register.Value</a> state via <a href="#register.setvalue(system.uint32)">Register.SetValue(System.UInt32)</a> method using <a href="#maszynapi.defines.default_reg_val">MaszynaPi.Defines.DEFAULT_REG_VAL</a> constant.

### SetBitsize(bitsize, instbitsize)

Sets <a href="#register.bitsize">Register.Bitsize</a> parameter of register.

| Name | Description |
| ---- | ----------- |
| bitsize | *System.UInt32*<br>Bit size to be set |
| instbitsize | *System.UInt32*<br>Instruction bit size (parameter for <a href="#instructionregister">InstructionRegister</a> class) |

### SetValue(value)

Assingns passed value to internal <a href="#register.value">Register.Value</a> of <a href="#register">Register</a>. Passed value can have any <a href="#system.uint32">System.UInt32</a> value, but potential overflow will be simulated, before assigment, using <a href="#maszynapi.commonoperations.bitwise.handleoverflow(system.uint32,system.uint32)">MaszynaPi.CommonOperations.Bitwise.HandleOverflow(System.UInt32,System.UInt32)</a> method.

| Name | Description |
| ---- | ----------- |
| value | *System.UInt32*<br>Value to be set (after overflow handling) as <a href="#register.value">Register.Value</a>. |

### Value

Value currently stored in <a href="#register">Register</a>.


## ArchitectureSettings

Static class storing machine architecture-related information and allowing to modify (for example from User Interface) and retreive them via set provided methods.

### ActiveComponents

Encoded active <a href="#maszynapi.defines.components">MaszynaPi.Defines.Components</a> of current <a href="#maszynapi.defines.architecture">MaszynaPi.Defines.Architecture</a> of machine.

### AddressSpace

Current address space size in bits.

### AvailableSignals

List of microoperations available to use in currently selected architecture.

### CodeBits

Current opcode size in bits.

### CurrentArchitecture

Current <a href="#maszynapi.defines.architecture">MaszynaPi.Defines.Architecture</a> of machine.

### GetActiveComponents

Encoded active <a href="#maszynapi.defines.components">MaszynaPi.Defines.Components</a> of current <a href="#maszynapi.defines.architecture">MaszynaPi.Defines.Architecture</a> of machine.

#### Returns

<a href="#architecturesettings.activecomponents">ArchitectureSettings.ActiveComponents</a>

### GetAddressSpace

<a href="#architecturesettings.addressspace">ArchitectureSettings.AddressSpace</a> getter.

#### Returns

Currently set <a href="#architecturesettings.addressspace">ArchitectureSettings.AddressSpace</a> value.

### GetAddressSpaceForIO

Calculates how many bits are neccessary to encode all IO Devices addresses.

#### Returns

Minimal number of bits that will allow to assign each IO device it's own address.

### GetArchitecture

Retreives current <a href="#maszynapi.defines.architecture">MaszynaPi.Defines.Architecture</a> of machine.

#### Returns

<a href="#architecturesettings.currentarchitecture">ArchitectureSettings.CurrentArchitecture</a>

### GetAvaibleInstructions

Returns list of all avaible instructions names, considering the currently set <a href="#architecturesettings.codebits">ArchitectureSettings.CodeBits</a> value .

#### Returns

List containing up to (<a href="#architecturesettings.getmaxopcode">ArchitectureSettings.GetMaxOpcode</a>+1) elements, representing instruction names.

### GetAvaibleSignals

List of microoperations available to use in currently selected architecture.

#### Returns

List of currently <a href="#architecturesettings.availablesignals">ArchitectureSettings.AvailableSignals</a>

### GetCodeBits

<a href="#architecturesettings.codebits">ArchitectureSettings.CodeBits</a> getter.

#### Returns

Currently set <a href="#architecturesettings.codebits">ArchitectureSettings.CodeBits</a> value.

### GetInterruptVector

Allows to access data structure mapping interruption-specific bit (<a href="#system.collections.generic.dictionary\`2.keys">System.Collections.Generic.Dictionary\`2.Keys</a>) into memory adresses (<a href="#system.collections.generic.dictionary\`2.values">System.Collections.Generic.Dictionary\`2.Values</a>).

#### Returns

<a href="#architecturesettings.interruptvector">ArchitectureSettings.InterruptVector</a> instance.

### GetIODeviceID(IOAddress)

Allows to retreive IO device's ID number, base on its assigned address.  Throws <a href="#system.exception">System.Exception</a> if no device with IOAddress was found in <a href="#architecturesettings.iodevices">ArchitectureSettings.IODevices</a> map.

| Name | Description |
| ---- | ----------- |
| IOAddress | *System.UInt32*<br>Assigned address of IO device. |

#### Returns

ID of device with address == IOAddress.

*System.Exception:* 

### GetMaxAddress

Calculates maximum memory adress that can be accessed using currently set <a href="#architecturesettings.addressspace">ArchitectureSettings.AddressSpace</a> size.

#### Returns

Address of last memory location that can be adressed.

### GetMaxOpcode

Calculates maximum opcode, hence the maximum number of instructions that can be encoded.

#### Returns

Max value that opcode segment of machine's WORD can represent.

### GetMaxWord

Calculates max value that currently set WORD size can store wihout overflowing (unsigned value assumed).

#### Returns

Maximum unsigned number possible to represent within single WORD of machine simulator.

### GetNumberOfIODevices

Should return number of avaible IO devices based on current architecture settings (if <a href="#architecturesettings.activecomponents">ArchitectureSettings.ActiveComponents</a> change was fully handled in GUI) but currently hardcoded for <a href="#maszynapi.defines.architecture.machinepi">MaszynaPi.Defines.Architecture.MachinePI</a> state.

#### Returns

Hardcoded value of (<a href="#maszynapi.defines.default_io_number">MaszynaPi.Defines.DEFAULT_IO_NUMBER</a> + <a href="#maszynapi.defines.extended_io_number">MaszynaPi.Defines.EXTENDED_IO_NUMBER</a>)

### GetWordBits

Calculates current machine's WORD size in bits as sum of opcode+address sizes.

#### Returns

Calculated WORD size in bits.

### InitializeInterruptVector

Initializes interrupt vector dictionary, creating new instance of it, filled with consecutive values from 0 to <a href="#maszynapi.defines.interruptions_num">MaszynaPi.Defines.INTERRUPTIONS_NUM</a> (as keys - interrupt IDs), and reverse sequence (as values - interrupt handler addresses)

### InitializeIODevicesAddresses

Initializes IO device's index-address dictionary, creating new instance of it, filled with consecutive values from 0 to 'max number of devices', for both keys and values of created map (means that device with ID = 0 have address 0 etc.).

### InterruptVector

Data structure mapping interruption-specific bits (<a href="#system.collections.generic.dictionary\`2.keys">System.Collections.Generic.Dictionary\`2.Keys</a>) into memory adresses (<a href="#system.collections.generic.dictionary\`2.values">System.Collections.Generic.Dictionary\`2.Values</a>).

### IODevices

Data structure mapping addresses of IO devices (<a href="#system.collections.generic.dictionary\`2.keys">System.Collections.Generic.Dictionary\`2.Keys</a>) into their's ID (<a href="#system.collections.generic.dictionary\`2.values">System.Collections.Generic.Dictionary\`2.Values</a>).

### SetActiveComponents(active)

Sets active <a href="#maszynapi.defines.components">MaszynaPi.Defines.Components</a> of machine.

| Name | Description |
| ---- | ----------- |
| active | *MaszynaPi.Defines.Components*<br>Encoded components to be set as new active components of machine. |

### SetAddressSpace(newAddressSpace)

Allows to set address space size in instructions (and therefore adress space for machine).  Throws <a href="#cpuexception">CPUException</a> if passed value [<a href="#maszynapi.defines.address_bits_min">MaszynaPi.Defines.ADDRESS_BITS_MIN</a>;<a href="#maszynapi.defines.address_bits_max">MaszynaPi.Defines.ADDRESS_BITS_MAX</a>] range.

| Name | Description |
| ---- | ----------- |
| newAddressSpace | *System.UInt32*<br>New address size in bits. |

*CPUException:* 

### SetArchitecture(machine)

Allows to set current <a href="#maszynapi.defines.architecture">MaszynaPi.Defines.Architecture</a> of machine.

| Name | Description |
| ---- | ----------- |
| machine | *MaszynaPi.Defines.Architecture*<br>Architecture to set. |

### SetCodeBits(newCodeBits)

Allows to set instruction's opcode size.  Throws <a href="#cpuexception">CPUException</a> if passed value not in [<a href="#maszynapi.defines.code_bits_min">MaszynaPi.Defines.CODE_BITS_MIN</a>;<a href="#maszynapi.defines.code_bits_max">MaszynaPi.Defines.CODE_BITS_MAX</a>] range.

| Name | Description |
| ---- | ----------- |
| newCodeBits | *System.UInt32*<br> |

*CPUException:* 

### SetInterruptVector(newVector)

Clears old <a href="#architecturesettings.interruptvector">ArchitectureSettings.InterruptVector</a> and copies content of newVector instance to it.

| Name | Description |
| ---- | ----------- |
| newVector | *System.Collections.Generic.Dictionary{System.UInt32,System.UInt32}*<br><a href="#system.collections.generic.dictionary\`2">System.Collections.Generic.Dictionary\`2</a> object with new interrupt vector (INTbit-address) pairs. |

### SetIODevicesAddresses(DevicesAddresses)

Clears old <a href="#architecturesettings.iodevices">ArchitectureSettings.IODevices</a> and copies content of DevicesAddresses instance to it.

| Name | Description |
| ---- | ----------- |
| DevicesAddresses | *System.Collections.Generic.Dictionary{System.UInt32,System.UInt32}*<br><a href="#system.collections.generic.dictionary\`2">System.Collections.Generic.Dictionary\`2</a> object with new IO device's address-ID pairs. |

### ShrinkToWordLength(value)

Shrinks passed value to maximum size of machine's WORD.

| Name | Description |
| ---- | ----------- |
| value | *System.UInt32*<br>Unsigned value to process |

#### Returns

Minimum of value and current maximum WORD.


## CentralProcessingUnit

Main monolith class, representing microcomputer's processing unit with its connections to other components such as memory or IO devices. Contain set of internal <a href="#architecture.register">Architecture.Register</a>, responsible for preserving state of machine between clock cycles. Class allows to manage executing instructions by controlling internal components of the machine.Note:

### Constructor

Creates new CPU's instance, initialized with defined <a href="#architecturesettings">ArchitectureSettings</a> and microinstructions set, connected to selected <a href="#maszynapi.defines.langinuse">MaszynaPi.Defines.LangInUse</a>.  Note: .

### A

Address Register - allows to address <a href="#centralprocessingunit.pao">CentralProcessingUnit.PaO</a> access.

### ActiveSignals

List of signals active in current clock cycle.

### AK

Accumulator - output register of <a href="#centralprocessingunit.jal">CentralProcessingUnit.JAL</a>

### AP

Interrupt Vector register.

### ChangeMemorySize(oldAddrSpace)

Allows to change size of CPU's visible <a href="#architecture.memory">Architecture.Memory</a> base on currently set <a href="#architecturesettings.addressspace">ArchitectureSettings.AddressSpace</a>. Memory can both grow and shrink - it preserves its content, except cells that are potentially removed when shrinking.

| Name | Description |
| ---- | ----------- |
| oldAddrSpace | *System.UInt32*<br>Value of addressing bitsize before changed in <a href="#architecturesettings.addressspace">ArchitectureSettings.AddressSpace</a> |

### CheckProgramBreak

Allows to define function returning value indicating whenever loop running whole program should be halted.

### DisableDebugger

Disables flag that indicates if state of CPU should be passed into user interface via connected <a href="#system.action">System.Action</a>s.

### EnableDebugger

Sets flag that enables passing CPU's state info to user interface via <a href="#centralprocessingunit.onsetexecutedline">CentralProcessingUnit.OnSetExecutedLine</a> and <a href="#centralprocessingunit.onsetexecutedmicroinstruction">CentralProcessingUnit.OnSetExecutedMicroinstruction</a><a href="#system.action">System.Action</a>s.

### ENDOF_INSTRUCTION

Value for in-instruction tick tracking variables indicating that all cycles of a single instruction have been executed.

### ExecuteInstructionCycle(program)

Performes all CPU's steps neccessary for executing whole instruction. Invokes all methods responsible for full Fetch-Decode-Execute cycle.

| Name | Description |
| ---- | ----------- |
| program | *System.Boolean*<br>Indicates if method was called from <a href="#centralprocessingunit.executeprogram">CentralProcessingUnit.ExecuteProgram</a>. |

### ExecuteProgram

Executes all program instrucitons from current instruction register address to first instruction with opcode 0. Disables connected <a href="#maszynapi.machineassembler.debugger">MaszynaPi.MachineAssembler.Debugger</a> actions until end of program execution, due to performace issues, related to displaing CPU state in user interface. On any error, raises <a href="#cpuexception">CPUException</a> with informations about currently executed instruction address, as well as active signals.

*CPUException:* 

### ExecuteTick(tick, manual)

Executes single 'tick' of CPU, base on current machine state. Sets <a href="#centralprocessingunit.activesignals">CentralProcessingUnit.ActiveSignals</a> list.

| Name | Description |
| ---- | ----------- |
| tick | *System.Int32*<br>Number (index) of consecutive tick of currently executed instruction. Defaults to 0 (see <a href="#centralprocessingunit.fetch_cycle_tick">CentralProcessingUnit.FETCH_CYCLE_TICK</a>). |
| manual | *System.Boolean*<br>Indicates whenever active signals (<a href="#centralprocessingunit.activesignals">CentralProcessingUnit.ActiveSignals</a> content) are selected by user in manual mode (true). Defaults to false. |

#### Returns

Index of current instruction's tick that should be executed, or <a href="#centralprocessingunit.endof_instruction">CentralProcessingUnit.ENDOF_INSTRUCTION</a> if all instruction's cycles were performed.

### FETCH_CYCLE_TICK

In which cycle of single instruction, Fetch must be performed.

### FetchInstruction

Assigns <a href="#maszynapi.defines.fetch_signals">MaszynaPi.Defines.FETCH_SIGNALS</a> to currently <a href="#centralprocessingunit.activesignals">CentralProcessingUnit.ActiveSignals</a> list.

### G

1 bit IO Device Ready register.

### GetActiveSignals

Allows to retreive list of <a href="#centralprocessingunit.activesignals">CentralProcessingUnit.ActiveSignals</a>.

#### Returns

Names of signals currently active in CPU.

### GetALUFlags

Allows to retreive <a href="#architecture.arithmeticlogicunit">Architecture.ArithmeticLogicUnit</a>'s, <a href="#architecture.aluflags">Architecture.ALUFlags</a> state.

#### Returns

Currently active <a href="#architecture.aluflags">Architecture.ALUFlags</a> from CPU's ALU instance.

### GetMemoryContent(addr, size)

Allows to retreive selected section of <a href="#architecture.memory.content">Architecture.Memory.Content</a>.

| Name | Description |
| ---- | ----------- |
| addr | *System.UInt32*<br>Starting address of section. |
| size | *System.UInt32*<br>Size of section to retreive. |

#### Returns

Range of <a href="#architecture.memory.content">Architecture.Memory.Content</a> defined by starting addr and size.

### GetMemoryContent(addr)

Allows to get single value from <a href="#architecture.memory.content">Architecture.Memory.Content</a>.

| Name | Description |
| ---- | ----------- |
| addr | *System.UInt32*<br>Address of value |

#### Returns

Value stored under address addr.

### GetMemoryContentHandle

Retreives instance of list of <a href="#system.uint32">System.UInt32</a> values, from internal <a href="#architecture.memory">Architecture.Memory</a> component, representing contents of microcomputer's memory.

#### Returns

List instance where <a href="#architecture.memory">Architecture.Memory</a> content is stored.

### GetTextInputBufferHandle

Allows to retreive handle to character buffer list instance from <a href="#centralprocessingunit.textinput">CentralProcessingUnit.TextInput</a> component.

#### Returns

Instance of <a href="#system.collections.generic.queue\`1">System.Collections.Generic.Queue\`1</a>, containing <a href="#centralprocessingunit.textinput">CentralProcessingUnit.TextInput</a> unprocessed data.

### GetTextOutputBufferHandle

Allows to retreive handle to character buffer list instance from <a href="#centralprocessingunit.textoutput">CentralProcessingUnit.TextOutput</a> component.

#### Returns

Instance of <a href="#system.collections.generic.list\`1">System.Collections.Generic.List\`1</a>, containing <a href="#centralprocessingunit.textoutput">CentralProcessingUnit.TextOutput</a> unprocessed data.

### HumidityInput

SenseHat additional IO controller, managing reading data from module's humidity sensor using  script

### I

Current Instruction register.

### InitialazeSignalsMap

Initializes map of signals, where each signal method is defined by it's string name. (Note: Custom [Attribute] for signal methods could be used to create map 'automatically'.)

### InstrDecoder

Decoder providing methods to determine next machine state base on instructions from <a href="#architecture.memory">Architecture.Memory</a>.

### IntController

Interruptions controller, providing methods to determine next machine state base on various interrupts sources.

### IOController

Controller providing methods for controlling state of input/output devices (except <a href="#architecture.memory">Architecture.Memory</a>) connected to machine.

### JAL

Machine's Arithmetic Logic Unit

### L

Instruction pointer register. Stores address of next instruction.

### LastTick

Keeps track current cycle of single instruction execution.

### MagA

Address Bus

### MagS

Data Bus

### MagT

Address-Data Transition Bus (address space sized)

### ManualInstruction

Invokes "<a href="#centralprocessingunit.executeinstructioncycle(system.boolean)">CentralProcessingUnit.ExecuteInstructionCycle(System.Boolean)</a>" method on CPU. Performs all microoperations neccessary for executing whole instruction. 

### ManualProgram

Invokes "<a href="#centralprocessingunit.executeprogram">CentralProcessingUnit.ExecuteProgram</a>" method on CPU. Performs all microoperations neccessary for executing all loaded instructions, until instruction '0' is found. 

### ManualTick(activeSigs)

Allows to invoke <a href="#centralprocessingunit.executetick(system.int32,system.boolean)">CentralProcessingUnit.ExecuteTick(System.Int32,System.Boolean)</a> method. Updates CPU state base on provided list of activeSigs.

| Name | Description |
| ---- | ----------- |
| activeSigs | *System.Collections.Generic.List{System.String}*<br>List of names of CPU's signals that specifies which operations should be performed. If 'null' (default), executes from current CPU state. |

### MatrixOutput

SenseHat additional IO controller, managing writing data to module's LED Matrix, using  script

### OnProgramEnd

Allows to define action invoked when <a href="#centralprocessingunit.stop">CentralProcessingUnit.stop</a> signal is activated.

### OnRefreshValues

Allows to define action responsible for refreshing CPU components representation in GUI.

### OnSetExecutedLine

Allows to define action responsible for showing currently executed line of program. Invoked each single instruction.

### OnSetExecutedMicroinstruction

Allows to define action responsible for showing currently executed microinstruction. Invoked each cycle.

### PaO

Operational Code and Data Memory

### PressureInput

SenseHat additional IO controller, managing reading data from module's pressure sensor using  script

### ProgramEnd

Calls <a href="#centralprocessingunit.onprogramend">CentralProcessingUnit.OnProgramEnd</a> if current instriction's opcode is equal to 0.

### RB

Buffer for communication with IO Devices.

### RefreshValues

Invoke of <a href="#centralprocessingunit.onrefreshvalues">CentralProcessingUnit.OnRefreshValues</a><a href="#system.action">System.Action</a>.

### ResetMemory

Calls <a href="#architecture.memory.reset">Architecture.Memory.Reset</a> method that sets memory back to it's default state (filed with <a href="#maszynapi.defines.default_mem_val">MaszynaPi.Defines.DEFAULT_MEM_VAL</a>).

### ResetRegisters

Activates Reset() in all resetable components of <a href="#centralprocessingunit">CentralProcessingUnit</a> (Could be handled by simple ). Clears <a href="#centralprocessingunit.activesignals">CentralProcessingUnit.ActiveSignals</a> and sets default value -1 of <a href="#centralprocessingunit.lasttick">CentralProcessingUnit.LastTick</a> field.

### RM

4 bit Interruption Mask register.

### RP

4 bit register storing info about accepted Interrupt.

### RZ

4 bit Interruption Report register.

### S

Value Register - output for <a href="#centralprocessingunit.pao">CentralProcessingUnit.PaO</a> values.

### SetActiveSignals(handActivatedSignals)

Allows to set CPU's <a href="#centralprocessingunit.activesignals">CentralProcessingUnit.ActiveSignals</a>.

| Name | Description |
| ---- | ----------- |
| handActivatedSignals | *System.Collections.Generic.List{System.String}*<br>Names of signals that should be set active. |

### SetComponentsBitsizes

Sets bitsizes in all related components, such as <a href="#architecture.register">Architecture.Register</a>s or <a href="#architecture.bus">Architecture.Bus</a>es, base on current <a href="#architecturesettings">ArchitectureSettings</a>.

### SetExecutedLineInEditor(System.UInt32)

Invoke of <a href="#centralprocessingunit.onsetexecutedline">CentralProcessingUnit.OnSetExecutedLine</a><a href="#system.action">System.Action</a>.

### SetExecutedMicroinstructions

Invoke of <a href="#centralprocessingunit.onsetexecutedmicroinstruction">CentralProcessingUnit.OnSetExecutedMicroinstruction</a><a href="#system.action">System.Action</a> with current instruction's opcode and <a href="#centralprocessingunit.activesignals">CentralProcessingUnit.ActiveSignals</a> list.

### SetLEDMatrixModeLetter

Sets <a href="#iodevices.matrixled.mode.letter">IODevices.MatrixLED.Mode.Letter</a> in Matrix IO handler, <a href="#centralprocessingunit.matrixoutput">CentralProcessingUnit.MatrixOutput</a>.

### SetLEDMatrixModePaint

Sets <a href="#iodevices.matrixled.mode.paint">IODevices.MatrixLED.Mode.Paint</a> in Matrix IO handler, <a href="#centralprocessingunit.matrixoutput">CentralProcessingUnit.MatrixOutput</a>.

### SetMemoryContent(values, offset)

Allows to set multiple values in <a href="#architecture.memory.content">Architecture.Memory.Content</a>, beggining at offset address. Size of memory is not checked here!

| Name | Description |
| ---- | ----------- |
| values | *System.Collections.Generic.List{System.UInt32}*<br>List of values to set. |
| offset | *System.UInt32*<br>Begining offset from adress 0. |

### SetMemoryContent(addr, value)

Allows to set single value in <a href="#architecture.memory.content">Architecture.Memory.Content</a>.

| Name | Description |
| ---- | ----------- |
| addr | *System.UInt32*<br>Address of value to set. |
| value | *System.UInt32*<br>Value to set |

### SetOnFetchCharAction(characterFetched)

Allows to set <a href="#centralprocessingunit.textinput">CentralProcessingUnit.TextInput</a><a href="#iodevices.characterinput.oncharacterfetched">IODevices.CharacterInput.OnCharacterFetched</a><a href="#system.action">System.Action</a>.

| Name | Description |
| ---- | ----------- |
| characterFetched | *System.Action*<br><a href="#system.action">System.Action</a> that should be performed when CPU fetches single character from text input's buffer. |

### SetOnInterruptReportedAction(interruptReported)

Allows to set <a href="#centralprocessingunit.intcontroller">CentralProcessingUnit.IntController</a><a href="#architecture.interruptioncontroller.oninterruptreported">Architecture.InterruptionController.OnInterruptReported</a><a href="#system.action">System.Action</a>.

| Name | Description |
| ---- | ----------- |
| interruptReported | *System.Action*<br><a href="#system.action">System.Action</a> that should be performed when interrupt is first reported to CPU in <a href="#centralprocessingunit.rz">CentralProcessingUnit.RZ</a> register. |

### SetOnPushCharAction(characterPushed)

Allows to set <a href="#centralprocessingunit.textoutput">CentralProcessingUnit.TextOutput</a><a href="#iodevices.characteroutput.oncharacterpushed">IODevices.CharacterOutput.OnCharacterPushed</a><a href="#system.action">System.Action</a>.

| Name | Description |
| ---- | ----------- |
| characterPushed | *System.Action*<br><a href="#system.action">System.Action</a> that should be performed when CPU puts single character into text output's buffer. |

### SetPaintActiveSignals

Allows to define action responsible for signalizing in that GUI that signals repaint is neccessary.

### SignalsMap

Initialized in <a href="#centralprocessingunit.initialazesignalsmap">CentralProcessingUnit.InitialazeSignalsMap</a> mapping of singnals names to related <a href="#centralprocessingunit">CentralProcessingUnit</a> void methods.

### TemperatureInput

SenseHat additional IO controller, managing reading data from module's temperature sensor using  script.

### TextInput

<a href="#iodevices.characterinput">IODevices.CharacterInput</a> controller, for managing getting characters from assigned input device.

### TextOutput

<a href="#iodevices.characterinput">IODevices.CharacterInput</a> controller, for managing putting characters to assigned output device.

### USE_DEBUGGER

Flag indicating whenever <a href="#maszynapi.machineassembler.debugger">MaszynaPi.MachineAssembler.Debugger</a> class should be carring UI machine state preview.

### WS

Stack register - point to top of the stack.

### X

Additional general-purpose register X

### Y

Additional general-purpose register Y


## CPUException

General custom <a href="#system.exception">System.Exception</a> for signalizing errors related to <a href="#centralprocessingunit">CentralProcessingUnit</a> data/control flow.


## CharacterInput

Character <a href="#iotype.input">IOType.Input</a> device. Provides method for <a href="#maszynapi.machinelogic.centralprocessingunit">MaszynaPi.MachineLogic.CentralProcessingUnit</a> to read input data provided by user. One character at time can be fetched from internal buffer, and it will be deleted from it afterwards.

### Constructor(g, rb, id, iOType)

Creates new instance of <a href="#characterinput">CharacterInput</a> class, assigning by defaut <a href="#iodevice.deviceid">IODevice.DeviceID</a> of 1 and <a href="#iodevice.type">IODevice.Type</a> of <a href="#iotype.input">IOType.Input</a>.

| Name | Description |
| ---- | ----------- |
| g | *MaszynaPi.MachineLogic.Architecture.Register*<br> |
| rb | *MaszynaPi.MachineLogic.Architecture.Register*<br> |
| id | *System.UInt32*<br>. Default is <a href="#characterinput.id">CharacterInput.ID</a>. |
| iOType | *MaszynaPi.MachineLogic.IODevices.IOType*<br>. Default is <a href="#characterinput.type">CharacterInput.TYPE</a> and should not be chaged. |

### CharactersBuffer

Representation of input buffer structure, where characters provided by user are stored and can be retreived by <a href="#maszynapi.machinelogic.centralprocessingunit">MaszynaPi.MachineLogic.CentralProcessingUnit</a>. via this <a href="#characterinput">CharacterInput</a> instance.

### GetChar

Checks if any character are avaiable in <a href="#characterinput.charactersbuffer">CharacterInput.CharactersBuffer</a> to read and sets <a href="#iodevice.ready">IODevice.Ready</a> value accordingly. On character availabe, fetches first character from an input buffer queue as ASCII number and puts it into single-char <a href="#iodevice.iobuffer">IODevice.IOBuffer</a><a href="#maszynapi.machinelogic.architecture.register">MaszynaPi.MachineLogic.Architecture.Register</a>. Character is removed from Queue buffer and <a href="#characterinput.oncharacterfetched">CharacterInput.OnCharacterFetched</a><a href="#system.action">System.Action</a> is invoked.

### GetCharactersBufferHandle

Get representation of input buffer structure, where characters provided by user are stored.

### OnCharacterFetched

Parametless <a href="#system.action">System.Action</a> invoked when new character is fetch from <a href="#characterinput.charactersbuffer">CharacterInput.CharactersBuffer</a> by this instance of <a href="#characterinput">CharacterInput</a>./

### WriteToIOBuffer

Checks if device is valid <a href="#iotype.input">IOType.Input</a> device using base implementation of <a href="#iodevice.writetoiobuffer">IODevice.WriteToIOBuffer</a>. Checks if any character are avaiable in <a href="#characterinput.charactersbuffer">CharacterInput.CharactersBuffer</a> to read and sets <a href="#iodevice.ready">IODevice.Ready</a> value accordingly. On character availabe, fetches first character from an input buffer queue as ASCII number and puts it into single-char <a href="#iodevice.iobuffer">IODevice.IOBuffer</a><a href="#maszynapi.machinelogic.architecture.register">MaszynaPi.MachineLogic.Architecture.Register</a>. Character is removed from Queue buffer and <a href="#characterinput.oncharacterfetched">CharacterInput.OnCharacterFetched</a><a href="#system.action">System.Action</a> is invoked.


## CharacterOutput

Character <a href="#iotype.output">IOType.Output</a> device. Provides method for <a href="#maszynapi.machinelogic.centralprocessingunit">MaszynaPi.MachineLogic.CentralProcessingUnit</a> to write data to it. One character at time can be put into internal buffer and will remain there until UI representation handles it's content.

### Constructor(g, rb, id, iOType)

Creates new instance of <a href="#characteroutput">CharacterOutput</a> class, assigning by defaut <a href="#iodevice.deviceid">IODevice.DeviceID</a> of 2 and <a href="#iodevice.type">IODevice.Type</a> of <a href="#iotype.output">IOType.Output</a>.

| Name | Description |
| ---- | ----------- |
| g | *MaszynaPi.MachineLogic.Architecture.Register*<br> |
| rb | *MaszynaPi.MachineLogic.Architecture.Register*<br> |
| id | *System.UInt32*<br>. Default is <a href="#characteroutput.id">CharacterOutput.ID</a>. |
| iOType | *MaszynaPi.MachineLogic.IODevices.IOType*<br>. Default is <a href="#characteroutput.type">CharacterOutput.TYPE</a> and should not be chaged. |

### CharactersBuffer

Representation of output buffer structure, where characters written by CPU are stored and can be viewed by user in GUI representation of class (via <a href="#maszynapi.machineui.usercontrolcharacteroutput">MaszynaPi.MachineUI.UserControlCharacterOutput</a> instance).

### GetCharactersBufferHandle

Get representation of output buffer structure, where characters written by CPU are stored.

### OnCharacterPushed

Parametless <a href="#system.action">System.Action</a> invoked when new character is put into <a href="#characteroutput.charactersbuffer">CharacterOutput.CharactersBuffer</a> by instance of <a href="#maszynapi.machinelogic.centralprocessingunit">MaszynaPi.MachineLogic.CentralProcessingUnit</a>./

### PushChar

Set <a href="#iodevice.io_ready">IODevice.IO_READY</a> valu into <a href="#iodevice.ready">IODevice.Ready</a><a href="#maszynapi.machinelogic.architecture.register">MaszynaPi.MachineLogic.Architecture.Register</a>. Push value from <a href="#iodevice.iobuffer">IODevice.IOBuffer</a><a href="#maszynapi.machinelogic.architecture.register">MaszynaPi.MachineLogic.Architecture.Register</a> to <a href="#characteroutput.charactersbuffer">CharacterOutput.CharactersBuffer</a> as character (ASCII Encoding) and invokes <a href="#characteroutput.oncharacterpushed">CharacterOutput.OnCharacterPushed</a>.

### ReadFromIOBuffer

Checks if device is valid <a href="#iotype.output">IOType.Output</a> device using base implementation of <a href="#iodevice.readfromiobuffer">IODevice.ReadFromIOBuffer</a>. 


## HumiditySensor

<a href="#iodevice">IODevice</a> of <a href="#iotype.input">IOType.Input</a> type. Provides method for <a href="#maszynapi.machinelogic.centralprocessingunit">MaszynaPi.MachineLogic.CentralProcessingUnit</a> to read humidity data from <a href="#maszynapi.sensehathandlers.sensehatdevice">MaszynaPi.SenseHatHandlers.SenseHatDevice</a> sensor.

### Constructor(g, rb, id, iOType)

Creates new instance of <a href="#humiditysensor">HumiditySensor</a><a href="#iodevice">IODevice</a>.

| Name | Description |
| ---- | ----------- |
| g | *MaszynaPi.MachineLogic.Architecture.Register*<br> |
| rb | *MaszynaPi.MachineLogic.Architecture.Register*<br> |
| id | *System.UInt32*<br>. Default is <a href="#humiditysensor.id">HumiditySensor.ID</a>. |
| iOType | *MaszynaPi.MachineLogic.IODevices.IOType*<br>. Default is <a href="#humiditysensor.type">HumiditySensor.TYPE</a> and should not be chaged. |

### GetValue

Sets <a href="#maszynapi.machinelogic.architecture.register">MaszynaPi.MachineLogic.Architecture.Register</a><a href="#iodevice.ready">IODevice.Ready</a> bit and assign value retreived from related <a href="#maszynapi.sensehathandlers.sensehatdevice.getsensordata">MaszynaPi.SenseHatHandlers.SenseHatDevice.GetSensorData</a> into this instance's <a href="#maszynapi.machinelogic.architecture.register">MaszynaPi.MachineLogic.Architecture.Register</a><a href="#iodevice.iobuffer">IODevice.IOBuffer</a>.

### Sensor

<a href="#maszynapi.sensehathandlers.sensehatdevice">MaszynaPi.SenseHatHandlers.SenseHatDevice</a> instance for communicating via python script processes with humidity sensor located on SenseHat module.

### WriteToIOBuffer




## IODevice

Abstract class providing template for basic <a href="#iodevice">IODevice</a> that can be <a href="#iotype.input">IOType.Input</a> device <a href="#iotype.output">IOType.Output</a> or both.

### Constructor(g, rb, id, iOType)

Default constructior for new I/O device of given iOType and id.

| Name | Description |
| ---- | ----------- |
| g | *MaszynaPi.MachineLogic.Architecture.Register*<br>Instance of CPU's "Ready" register. |
| rb | *MaszynaPi.MachineLogic.Architecture.Register*<br>Instance of CPU's "I/O Buffer" register. |
| id | *System.UInt32*<br>Requested ID number of device. |
| iOType | *MaszynaPi.MachineLogic.IODevices.IOType*<br>Type of device. Will affect virtual read/write methdos that can be used. |

### DeviceID

ID of device.

### GetID

Allows to retreive <a href="#iodevice.deviceid">IODevice.DeviceID</a>.

#### Returns

Device ID value.

### GetIOBufferValue

Allows to access current I/O buffer <a href="#maszynapi.machinelogic.architecture.register.value">MaszynaPi.MachineLogic.Architecture.Register.Value</a>

#### Returns

Value stored in internal I/O data buffer register. Used mainly for microinstruction datafolow access.

### GetIOType

Allows to retreive <a href="#iotype">IOType</a> of device.

#### Returns

Device <a href="#iotype">IOType</a>.

### GetReadyValue

Provides access to <a href="#iodevice.ready">IODevice.Ready</a><a href="#maszynapi.machinelogic.architecture.register.value">MaszynaPi.MachineLogic.Architecture.Register.Value</a>.

#### Returns

1 if IO data is ready 0 otherwise.

### IO_READY

Uint flag value indicating that device is ready to take/give data.

### IO_WAIT

Uint flag value indicating that device is in processing state - data awaited.

### IOBuffer

Internal <a href="#maszynapi.machinelogic.architecture.register">MaszynaPi.MachineLogic.Architecture.Register</a> acting as buffer for data transmitted from/to I/O.

### ReadFromIOBuffer

Virtual template of I/O Buffer reading method for : Buffer -> IO. Provides  check of Device's <a href="#iotype">IOType</a> (buffer can be read only by <a href="#iotype.output">IOType.Output</a> device, otherwise <a href="#iodeviceexception">IODeviceException</a> is thrown). 

*IODeviceException:* 

### Ready

Internal 1 bit <a href="#maszynapi.machinelogic.architecture.register">MaszynaPi.MachineLogic.Architecture.Register</a> storing information about ready state (<a href="#iodevice.io_wait">IODevice.IO_WAIT</a>/<a href="#iodevice.io_ready">IODevice.IO_READY</a>).

### SetIOBufferValue(value)

Allows to set <a href="#iodevice.iobuffer">IODevice.IOBuffer</a><a href="#maszynapi.machinelogic.architecture.register.value">MaszynaPi.MachineLogic.Architecture.Register.Value</a>.

| Name | Description |
| ---- | ----------- |
| value | *System.UInt32*<br>New value of data buffer register. <a href="#maszynapi.machinelogic.architecture.register.setvalue(system.uint32)">MaszynaPi.MachineLogic.Architecture.Register.SetValue(System.UInt32)</a> will handle potential overflow of set datasize. |

### SetReadyValue(value)

Allows to set <a href="#iodevice.ready">IODevice.Ready</a><a href="#maszynapi.machinelogic.architecture.register.value">MaszynaPi.MachineLogic.Architecture.Register.Value</a>.

| Name | Description |
| ---- | ----------- |
| value | *System.UInt32*<br>New value of ready register. <a href="#maszynapi.machinelogic.architecture.register.setvalue(system.uint32)">MaszynaPi.MachineLogic.Architecture.Register.SetValue(System.UInt32)</a> will handle potential overflow of 1-bit datasize. |

### Type

Type of device.

### WriteToIOBuffer

Virtual template of I/O Buffer writing method for : IO -> Buffer . Provides  check of Device's <a href="#iotype">IOType</a> (value can be written to buffer only by <a href="#iotype.input">IOType.Input</a> device, otherwise <a href="#iodeviceexception">IODeviceException</a> is thrown). 

*IODeviceException:* 


## IODeviceException

General <a href="#system.exception">System.Exception</a> for errors related to <a href="#iodevice">IODevice</a> classes.


## IOType

Enumeration representing different types of <a href="#iodevice">IODevice</a>es.

### Input

Signalizes external device sending data to <a href="#maszynapi.machinelogic.centralprocessingunit">MaszynaPi.MachineLogic.CentralProcessingUnit</a>.

### InputOutput

Signalizes external device that can send and read data to/from <a href="#maszynapi.machinelogic.centralprocessingunit">MaszynaPi.MachineLogic.CentralProcessingUnit</a>.

### Output

Signalizes external device reading data from <a href="#maszynapi.machinelogic.centralprocessingunit">MaszynaPi.MachineLogic.CentralProcessingUnit</a>.


## MatrixLED

Implementaion of <a href="#iotype.output">IOType.Output</a><a href="#iodevice">IODevice</a> representing 8x8 LED Matrix on <a href="#maszynapi.sensehathandlers.sensehatdevice">MaszynaPi.SenseHatHandlers.SenseHatDevice</a>. Provides functionality for <a href="#maszynapi.machinelogic.centralprocessingunit">MaszynaPi.MachineLogic.CentralProcessingUnit</a> to write data that will be shown on module's matrix, base on selected <a href="#matrixled.mode">MatrixLED.Mode</a>: <a href="#matrixled.mode.letter">MatrixLED.Mode.Letter</a> allows to represent passed number as one of printable ASCII symbols on Matrix <a href="#matrixled.mode.paint">MatrixLED.Mode.Paint</a> allows to paint single pixel at time, using bottom 6 bits of passed number as matrix pixel address (consecutive numbers from top-left to bottom-right). Color of each pixel is selected using 9th, 10th and 11th bit as [R,G,B] component set to 255 or 0.

### Constructor(g, rb, id, iOType)

Creates new instance of <a href="#matrixled">MatrixLED</a><a href="#iodevice">IODevice</a>.

| Name | Description |
| ---- | ----------- |
| g | *MaszynaPi.MachineLogic.Architecture.Register*<br> |
| rb | *MaszynaPi.MachineLogic.Architecture.Register*<br> |
| id | *System.UInt32*<br>. Default is <a href="#matrixled.id">MatrixLED.ID</a>. |
| iOType | *MaszynaPi.MachineLogic.IODevices.IOType*<br>. Default is <a href="#matrixled.type">MatrixLED.TYPE</a> and should not be chaged. |

### Matrix

<a href="#maszynapi.sensehathandlers.sensehatdevice">MaszynaPi.SenseHatHandlers.SenseHatDevice</a> instance for communicating via python script processes with 8x8 LED matrix located on SenseHat module.


## Mode

Represents 2 <a href="#maszynapi.machinelogic.iodevices.matrixled">MaszynaPi.MachineLogic.IODevices.MatrixLED</a> working modes.

### Letter

Each number written into <a href="#maszynapi.machinelogic.iodevices.iodevice.iobuffer">MaszynaPi.MachineLogic.IODevices.IODevice.IOBuffer</a> if show in <a href="#matrix">Matrix</a> as it's ASCII representaion.

### Paint

Each number written into <a href="#maszynapi.machinelogic.iodevices.iodevice.iobuffer">MaszynaPi.MachineLogic.IODevices.IODevice.IOBuffer</a> is translated into single pixel of colour to be shown in <a href="#matrix">Matrix</a>.

### MaszynaPi.MachineLogic.IODevices.MatrixLED.PushChar

Push value from <a href="#maszynapi.machinelogic.iodevices.iodevice.iobuffer">MaszynaPi.MachineLogic.IODevices.IODevice.IOBuffer</a> to output <a href="#matrix">Matrix</a> device using <a href="#maszynapi.sensehathandlers.sensehatdevice.matrixprint(system.uint32,system.string)">MaszynaPi.SenseHatHandlers.SenseHatDevice.MatrixPrint(System.UInt32,System.String)</a>.

### MaszynaPi.MachineLogic.IODevices.MatrixLED.ReadFromIOBuffer



### MaszynaPi.MachineLogic.IODevices.MatrixLED.SetLetterMode

Sets this istance's <a href="#mode">Mode</a> to <a href="#mode.letter">Mode.Letter</a>

### MaszynaPi.MachineLogic.IODevices.MatrixLED.SetPaintMode

Sets this istance's <a href="#mode">Mode</a> to <a href="#mode.paint">Mode.Paint</a>

### MaszynaPi.MachineLogic.IODevices.MatrixLED.WorkingMode

Current working <a href="#mode">Mode</a> of <a href="#maszynapi.machinelogic.iodevices.matrixled">MaszynaPi.MachineLogic.IODevices.MatrixLED</a>.


## PressureSensor

<a href="#iodevice">IODevice</a> of <a href="#iotype.input">IOType.Input</a> type. Provides method for <a href="#maszynapi.machinelogic.centralprocessingunit">MaszynaPi.MachineLogic.CentralProcessingUnit</a> to read pressure data from <a href="#maszynapi.sensehathandlers.sensehatdevice">MaszynaPi.SenseHatHandlers.SenseHatDevice</a> sensor.

### Constructor(g, rb, id, iOType)

Creates new instance of <a href="#pressuresensor">PressureSensor</a><a href="#iodevice">IODevice</a>.

| Name | Description |
| ---- | ----------- |
| g | *MaszynaPi.MachineLogic.Architecture.Register*<br> |
| rb | *MaszynaPi.MachineLogic.Architecture.Register*<br> |
| id | *System.UInt32*<br>. Default is <a href="#pressuresensor.id">PressureSensor.ID</a>. |
| iOType | *MaszynaPi.MachineLogic.IODevices.IOType*<br>. Default is <a href="#pressuresensor.type">PressureSensor.TYPE</a> and should not be chaged. |

### GetValue

Sets <a href="#maszynapi.machinelogic.architecture.register">MaszynaPi.MachineLogic.Architecture.Register</a><a href="#iodevice.ready">IODevice.Ready</a> bit and assign value retreived from related <a href="#maszynapi.sensehathandlers.sensehatdevice.getsensordata">MaszynaPi.SenseHatHandlers.SenseHatDevice.GetSensorData</a> into this instance's <a href="#maszynapi.machinelogic.architecture.register">MaszynaPi.MachineLogic.Architecture.Register</a><a href="#iodevice.iobuffer">IODevice.IOBuffer</a>.

### Sensor

<a href="#maszynapi.sensehathandlers.sensehatdevice">MaszynaPi.SenseHatHandlers.SenseHatDevice</a> instance for communicating via python script processes with pressure sensor located on SenseHat module.

### WriteToIOBuffer




## TemperatureSensor

<a href="#iodevice">IODevice</a> of <a href="#iotype.input">IOType.Input</a> type. Provides method for <a href="#maszynapi.machinelogic.centralprocessingunit">MaszynaPi.MachineLogic.CentralProcessingUnit</a> to read temperature data from <a href="#maszynapi.sensehathandlers.sensehatdevice">MaszynaPi.SenseHatHandlers.SenseHatDevice</a> sensor.

### Constructor(g, rb, id, iOType)

Creates new instance of <a href="#temperaturesensor">TemperatureSensor</a><a href="#iodevice">IODevice</a>.

| Name | Description |
| ---- | ----------- |
| g | *MaszynaPi.MachineLogic.Architecture.Register*<br> |
| rb | *MaszynaPi.MachineLogic.Architecture.Register*<br> |
| id | *System.UInt32*<br>. Default is <a href="#temperaturesensor.id">TemperatureSensor.ID</a>. |
| iOType | *MaszynaPi.MachineLogic.IODevices.IOType*<br>. Default is <a href="#temperaturesensor.type">TemperatureSensor.TYPE</a> and should not be chaged. |

### GetValue

Sets <a href="#maszynapi.machinelogic.architecture.register">MaszynaPi.MachineLogic.Architecture.Register</a><a href="#iodevice.ready">IODevice.Ready</a> bit and assign value retreived from related <a href="#maszynapi.sensehathandlers.sensehatdevice.getsensordata">MaszynaPi.SenseHatHandlers.SenseHatDevice.GetSensorData</a> into this instance's <a href="#maszynapi.machinelogic.architecture.register">MaszynaPi.MachineLogic.Architecture.Register</a><a href="#iodevice.iobuffer">IODevice.IOBuffer</a>. Value provided in mili-celcius.

### Sensor

<a href="#maszynapi.sensehathandlers.sensehatdevice">MaszynaPi.SenseHatHandlers.SenseHatDevice</a> instance for communicating via python script processes with temperature sensor located on SenseHat module.

### WriteToIOBuffer



### MaszynaPi.MachineUI.UserControlBus.components

Wymagana zmienna projektanta.

### MaszynaPi.MachineUI.UserControlBus.Dispose(disposing)

Wyczyść wszystkie używane zasoby.

| Name | Description |
| ---- | ----------- |
| disposing | *System.Boolean*<br>prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku. |

### MaszynaPi.MachineUI.UserControlBus.InitializeComponent

Metoda wymagana do obsługi projektanta — nie należy modyfikować jej zawartości w edytorze kodu.

### MaszynaPi.MachineUI.UserControlCharacterInput.components

Wymagana zmienna projektanta.

### MaszynaPi.MachineUI.UserControlCharacterInput.Dispose(disposing)

Wyczyść wszystkie używane zasoby.

| Name | Description |
| ---- | ----------- |
| disposing | *System.Boolean*<br>prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku. |

### MaszynaPi.MachineUI.UserControlCharacterInput.InitializeComponent

Metoda wymagana do obsługi projektanta — nie należy modyfikować jej zawartości w edytorze kodu.

### MaszynaPi.MachineUI.UserControlCharacterOutput.components

Wymagana zmienna projektanta.

### MaszynaPi.MachineUI.UserControlCharacterOutput.Dispose(disposing)

Wyczyść wszystkie używane zasoby.

| Name | Description |
| ---- | ----------- |
| disposing | *System.Boolean*<br>prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku. |

### MaszynaPi.MachineUI.UserControlCharacterOutput.InitializeComponent

Metoda wymagana do obsługi projektanta — nie należy modyfikować jej zawartości w edytorze kodu.

### MaszynaPi.MachineUI.UserControlCodeEditor.components

Wymagana zmienna projektanta.

### MaszynaPi.MachineUI.UserControlCodeEditor.Dispose(disposing)

Wyczyść wszystkie używane zasoby.

| Name | Description |
| ---- | ----------- |
| disposing | *System.Boolean*<br>prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku. |

### MaszynaPi.MachineUI.UserControlCodeEditor.InitializeComponent

Metoda wymagana do obsługi projektanta — nie należy modyfikować jej zawartości w edytorze kodu.

### MaszynaPi.MachineUI.UserControlFlags.components

Wymagana zmienna projektanta.

### MaszynaPi.MachineUI.UserControlFlags.Dispose(disposing)

Wyczyść wszystkie używane zasoby.

| Name | Description |
| ---- | ----------- |
| disposing | *System.Boolean*<br>prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku. |

### MaszynaPi.MachineUI.UserControlFlags.InitializeComponent

Metoda wymagana do obsługi projektanta — nie należy modyfikować jej zawartości w edytorze kodu.

### MaszynaPi.MachineUI.UserControlInstructionList.components

Wymagana zmienna projektanta.

### MaszynaPi.MachineUI.UserControlInstructionList.Dispose(disposing)

Wyczyść wszystkie używane zasoby.

| Name | Description |
| ---- | ----------- |
| disposing | *System.Boolean*<br>prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku. |

### MaszynaPi.MachineUI.UserControlInstructionList.InitializeComponent

Metoda wymagana do obsługi projektanta — nie należy modyfikować jej zawartości w edytorze kodu.

### MaszynaPi.MachineUI.UserControlInstructionMicrocode.components

Wymagana zmienna projektanta.

### MaszynaPi.MachineUI.UserControlInstructionMicrocode.Dispose(disposing)

Wyczyść wszystkie używane zasoby.

| Name | Description |
| ---- | ----------- |
| disposing | *System.Boolean*<br>prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku. |

### MaszynaPi.MachineUI.UserControlInstructionMicrocode.InitializeComponent

Metoda wymagana do obsługi projektanta — nie należy modyfikować jej zawartości w edytorze kodu.

### MaszynaPi.MachineUI.UserControlIntButton.components

Wymagana zmienna projektanta.

### MaszynaPi.MachineUI.UserControlIntButton.Dispose(disposing)

Wyczyść wszystkie używane zasoby.

| Name | Description |
| ---- | ----------- |
| disposing | *System.Boolean*<br>prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku. |

### MaszynaPi.MachineUI.UserControlIntButton.InitializeComponent

Metoda wymagana do obsługi projektanta — nie należy modyfikować jej zawartości w edytorze kodu.

### MaszynaPi.MachineUI.UserControlMemory.components

Wymagana zmienna projektanta.

### MaszynaPi.MachineUI.UserControlMemory.Dispose(disposing)

Wyczyść wszystkie używane zasoby.

| Name | Description |
| ---- | ----------- |
| disposing | *System.Boolean*<br>prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku. |

### MaszynaPi.MachineUI.UserControlMemory.InitializeComponent

Metoda wymagana do obsługi projektanta — nie należy modyfikować jej zawartości w edytorze kodu.

### MaszynaPi.MachineUI.UserControlMemory.PartiallySupressRefreshing

Sets flag that indicates if <a href="#maszynapi.machineui.usercontrolmemory.refresh">MaszynaPi.MachineUI.UserControlMemory.Refresh</a> overload should refresh only visible parts of memory control.

### MaszynaPi.MachineUI.UserControlMemory.VISIBLE_MEMORY_SIZE

Lines of memory generated in control when <a href="#maszynapi.machineui.usercontrolmemory.partiallysupressrefreshing">MaszynaPi.MachineUI.UserControlMemory.PartiallySupressRefreshing</a> enabled.

### MaszynaPi.MachineUI.UserControlRegister.components

Wymagana zmienna projektanta.

### MaszynaPi.MachineUI.UserControlRegister.Dispose(disposing)

Wyczyść wszystkie używane zasoby.

| Name | Description |
| ---- | ----------- |
| disposing | *System.Boolean*<br>prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku. |

### MaszynaPi.MachineUI.UserControlRegister.InitializeComponent

Metoda wymagana do obsługi projektanta — nie należy modyfikować jej zawartości w edytorze kodu.

### MaszynaPi.MachineUI.UserControlSignalWire.components

Wymagana zmienna projektanta.

### MaszynaPi.MachineUI.UserControlSignalWire.Dispose(disposing)

Wyczyść wszystkie używane zasoby.

| Name | Description |
| ---- | ----------- |
| disposing | *System.Boolean*<br>prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku. |

### MaszynaPi.MachineUI.UserControlSignalWire.InitializeComponent

Metoda wymagana do obsługi projektanta — nie należy modyfikować jej zawartości w edytorze kodu.

### MaszynaPi.Program.Main

Main entry.


## Resources

Klasa zasobu wymagająca zdefiniowania typu do wyszukiwania zlokalizowanych ciągów itd.

### Base

Wyszukuje zlokalizowany ciąg podobny do ciągu [Opcje] Połączenie=0 Inkrementacja=0 Logiczne=0 Arytmetyczne=0 Stos=0 RejestrX=0 RejestrY=0 Przerwania=0 Wejście=0 Znaczniki=0 Adres=5 Kod=3 [Rozkazy] Liczba=8 Nazwa1=stp Nazwa2=add Nazwa3=sub Nazwa4=load Nazwa5=stor Nazwa6=jmp Nazwa7=blz Nazwa8=bez [stp] Linie=5 Linia1=// zakończenie programu Linia2=INSTRUCTION STP; Linia3=Arguments 0; Linia4=rd od iins icit; Linia5=stop; [add] Linie=4 Linia1=INSTRUCTION ADD; Linia2=rd od iins icit; Linia3=oa ia; Linia4=rd od ialu add iac [obcięto pozostałą część ciągu]";.

### Culture

Przesłania właściwość CurrentUICulture bieżącego wątku dla wszystkich przypadków przeszukiwania zasobów za pomocą tej klasy zasobów wymagającej zdefiniowania typu.

### Podstawa

Wyszukuje zlokalizowany ciąg podobny do ciągu [Opcje] Połączenie=0 Inkrementacja=0 Logiczne=0 Arytmetyczne=0 Stos=0 RejestrX=0 RejestrY=0 Przerwania=0 Wejście=0 Znaczniki=0 Adres=5 Kod=3 [Rozkazy] Liczba=8 Nazwa1=stp Nazwa2=dod Nazwa3=ode Nazwa4=pob Nazwa5=ład Nazwa6=sob Nazwa7=som Nazwa8=soz [stp] Linie=5 Linia1=// zakończenie programu Linia2=ROZKAZ STP; Linia3=Argumenty 0; Linia4=czyt wys wei il; Linia5=stop; [dod] Linie=4 Linia1=ROZKAZ DOD; Linia2=czyt wys wei il; Linia3=wyad wea; Linia4=czyt wys weja dod weak wyl [obcięto pozostałą część ciągu]";.

### ResourceManager

Zwraca buforowane wystąpienie ResourceManager używane przez tę klasę.


## SenseHatDevice

Base class for CPU's IO devices represented by RaspberryPI SenseHat module's devices.

### Constructor

Creates new <a href="#sensehatdevice">SenseHatDevice</a> instance initialized with received data '0'.

### AsyncRead_DoWork(sender, e)

<a href="#sensehatdevice.asyncread">SenseHatDevice.AsyncRead</a><a href="#system.componentmodel.backgroundworker.dowork">System.ComponentModel.BackgroundWorker.DoWork</a> event handler. Starts <a href="#sensehatdevice.readprocess">SenseHatDevice.ReadProcess</a> and reads data from <a href="#system.diagnostics.process.standardoutput">System.Diagnostics.Process.StandardOutput</a> until <a href="#system.componentmodel.backgroundworker.cancellationpending">System.ComponentModel.BackgroundWorker.CancellationPending</a> is false. After that, kills <a href="#sensehatdevice.readprocess">SenseHatDevice.ReadProcess</a>.

| Name | Description |
| ---- | ----------- |
| sender | *System.Object*<br>Not used. |
| e | *System.ComponentModel.DoWorkEventArgs*<br>Provides data for handler. |

### CreateReadProcess(cmd)

Creates new <a href="#sensehatdevice.startpythoncmd">SenseHatDevice.StartPythonCMD</a> process using cmd as script argument. If current platform is not <a href="#system.platformid.unix">System.PlatformID.Unix</a>, returns wihout creating <a href="#sensehatdevice.readprocess">SenseHatDevice.ReadProcess</a>.

| Name | Description |
| ---- | ----------- |
| cmd | *System.String*<br>Path to python script that should be called |

### DataReceived(sender, e)

<a href="#sensehatdevice.readprocess">SenseHatDevice.ReadProcess</a>, <a href="#system.diagnostics.process.outputdatareceived">System.Diagnostics.Process.OutputDataReceived</a> event. Assings <a href="#system.diagnostics.datareceivedeventargs.data">System.Diagnostics.DataReceivedEventArgs.Data</a> to <a href="#sensehatdevice.receiveddata">SenseHatDevice.ReceivedData</a> field.

| Name | Description |
| ---- | ----------- |
| sender | *System.Object*<br>Not used. |
| e | *System.Diagnostics.DataReceivedEventArgs*<br>Provides data from event handler. |

### ErrorReceived(sender, e)

 due do python scripts writing irrelevant warnings into error stream. Disards all <a href="#system.diagnostics.process.standarderror">System.Diagnostics.Process.StandardError</a> communicates.

| Name | Description |
| ---- | ----------- |
| sender | *System.Object*<br>Not used. |
| e | *System.Diagnostics.DataReceivedEventArgs*<br>Provides data from event handler. |

### GetData

Allows to retreive single standard-output data from stript that's executing in <a href="#sensehatdevice.readprocess">SenseHatDevice.ReadProcess</a>. Method watis for <a href="#sensehatdevice.readprocess">SenseHatDevice.ReadProcess</a> to end with <a href="#system.threading.timeout.infinite">System.Threading.Timeout.Infinite</a>. Script must not get into infinite loop to avoid blocking. Throws <a href="#sensehatexception">SenseHatException</a> if <a href="#sensehatdevice.readprocess">SenseHatDevice.ReadProcess</a> is null or error occurs while reading data.

#### Returns

String instance containing data read from <a href="#sensehatdevice.readprocess">SenseHatDevice.ReadProcess</a><a href="#system.diagnostics.process.standardoutput">System.Diagnostics.Process.StandardOutput</a>.

*SenseHatException:* 

### GetSensorData

Returns data from <a href="#sensehatdevice.readprocess">SenseHatDevice.ReadProcess</a> standard output parsed to float and casted into uint value. Throws <a href="#sensehatexception">SenseHatException</a> if parsing failed.

#### Returns

uint representation of read from stdout stirng.

*SenseHatException:* 

### JOYSTICK_POS_PRESS

Stdout message from <a href="#sensehatdevice.joystick_script">SenseHatDevice.JOYSTICK_SCRIPT</a> that indicates joystick was pressed

### JOYSTICK_SCRIPT

Path to python script performing communitation with SenseHat's joystick output. Default in .exe scripts/ dir.

### JoystickPosIntMap

Maps joystick position string from <a href="#sensehatdevice.joystick_script">SenseHatDevice.JOYSTICK_SCRIPT</a> std output into interrupt priority.

### MATRIX_SCRIPT

Path to python script performing communitation with SenseHat's matrix. Default in .exe scripts/ dir.

### MatrixPrint(value, mode)

Starts new process of <a href="#sensehatdevice.matrix_script">SenseHatDevice.MATRIX_SCRIPT</a> with argumetns given in value and mode parameters using <a href="#sensehatdevice.senddata(system.string)">SenseHatDevice.SendData(System.String)</a> method.

| Name | Description |
| ---- | ----------- |
| value | *System.UInt32*<br>Value to be feed into <a href="#sensehatdevice.matrix_script">SenseHatDevice.MATRIX_SCRIPT</a> as first call argument. |
| mode | *System.String*<br>String to be feed into <a href="#sensehatdevice.matrix_script">SenseHatDevice.MATRIX_SCRIPT</a> as second call argument. |

### OnInterruptionReceived

Handler for <a href="#maszynapi.machinelogic.centralprocessingunit">MaszynaPi.MachineLogic.CentralProcessingUnit</a> method that should be invoked on <a href="#sensehatdevice.joystick_script">SenseHatDevice.JOYSTICK_SCRIPT</a> data receive. Parameter of <a href="#system.action">System.Action</a> will be taking value translated from joystick positions into interruption priority using <a href="#sensehatdevice.joystickposintmap">SenseHatDevice.JoystickPosIntMap</a>.

### SendData(System.String)

Creates new <a href="#system.diagnostics.process">System.Diagnostics.Process</a> using <a href="#sensehatdevice.startpythoncmd">SenseHatDevice.StartPythonCMD</a> and passes cmd as argument to script. Method watis for exit with <a href="#system.threading.timeout.infinite">System.Threading.Timeout.Infinite</a>. Script must not get into infinite loop to avoid blocking. Throws <a href="#sensehatexception">SenseHatException</a> if error occurs during process execution.

*SenseHatException:* 

### SENSOR_HUMIDITY

Selector argument for <a href="#sensehatdevice.sensor_script">SenseHatDevice.SENSOR_SCRIPT</a>, indicatinig humidity read.

### SENSOR_PRESSURE

Selector argument for <a href="#sensehatdevice.sensor_script">SenseHatDevice.SENSOR_SCRIPT</a>, indicatinig pressure read.

### SENSOR_SCRIPT

Path to python script performing communitation with SenseHat's sensors. Default in .exe scripts/ dir.

### SENSOR_TEMPERATURE

Selector argument for <a href="#sensehatdevice.sensor_script">SenseHatDevice.SENSOR_SCRIPT</a>, indicatinig temperature read.

### StartAsyncRead

If app runing on <a href="#system.platformid.unix">System.PlatformID.Unix</a>, creates new <a href="#system.componentmodel.backgroundworker">System.ComponentModel.BackgroundWorker</a> instance (see <a href="#sensehatdevice.asyncread">SenseHatDevice.AsyncRead</a>) that can report progress and supports cacelation. Assigns <a href="#sensehatdevice.asyncread_dowork(system.object,system.componentmodel.doworkeventargs)">SenseHatDevice.AsyncRead_DoWork(System.Object,System.ComponentModel.DoWorkEventArgs)</a> into <a href="#system.componentmodel.backgroundworker.dowork">System.ComponentModel.BackgroundWorker.DoWork</a> event handler and runs asynchornus operation. Throws <a href="#sensehatexception">SenseHatException</a> if <a href="#sensehatdevice.readprocess">SenseHatDevice.ReadProcess</a> is not created before calling this method.

*SenseHatException:* 

### StartPythonCMD

Command for starting python script.

### StopAsyncRead(cancelTimeout)

Stops asynchronus operation of reading data from <a href="#sensehatdevice.joystick_script">SenseHatDevice.JOYSTICK_SCRIPT</a> using <a href="#sensehatdevice.readprocess">SenseHatDevice.ReadProcess</a>/. If <a href="#sensehatdevice.readprocess">SenseHatDevice.ReadProcess</a> or <a href="#sensehatdevice.asyncread">SenseHatDevice.AsyncRead</a> is null, returns without taking any actions. Throws <a href="#system.exception">System.Exception</a> if reated <a href="#system.componentmodel.backgroundworker.isbusy">System.ComponentModel.BackgroundWorker.IsBusy</a> after cancelTimeout.

| Name | Description |
| ---- | ----------- |
| cancelTimeout | *System.TimeSpan*<br> |

*System.Exception:* 


## SenseHatException

General custom <a href="#system.exception">System.Exception</a> for signalizing errors related to <a href="#sensehatdevice">SenseHatDevice</a> data/control flow.

### MaszynaPi.UI.BreakForm.components

Required designer variable.

### MaszynaPi.UI.BreakForm.Dispose(disposing)

Clean up any resources being used.

| Name | Description |
| ---- | ----------- |
| disposing | *System.Boolean*<br>true if managed resources should be disposed; otherwise, false. |

### MaszynaPi.UI.BreakForm.InitializeComponent

Required method for Designer support - do not modify the contents of this method with the code editor.
