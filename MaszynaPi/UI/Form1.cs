using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaszynaPi.MachineLogic;
using MaszynaPi.MachineAssembler;
using MaszynaPi.MachineUI;
using MaszynaPi.FilesHandling;
using MaszynaPi.MachineAssembler.Editors;

namespace MaszynaPi {
    public partial class Form1 : Form {
        //==================================< External DLL Functions for SenseHat >=======================================================
        //public const string SenseDLL = @"..\..\Debug\SenseHatController.dll";
        //[DllImport(SenseDLL, CallingConvention = CallingConvention.Cdecl)]
        //public static extern int GetJoystickState();
        //================================================================================================================================

        string LastUsedFilepath;

        /*Sterowanie ręczne maszyną -> każdy aktywowany sygnał dodaje jego nazwę do listy, która jest następnie sortowana i
         *przekazywana do wykonania Maszynie (metoda ManualTick / Ustawianie "ActiveSignals") 
         *(możliwe wykonanie tylko kroku "Takt" przy sterowaniu ręcznym)*/
        CodeEditor codeEditor;
        ControlUnit Machine;
        Debugger Debugger;

        public Form1() {

            //Must Be First!  [TODO Handle exception with loading for Raspbian -> allow user to select diferent instruction set]
            try { InstructionLoader.LoadBaseInstructions(); } catch (InstructionLoaderException ex) {
                MessageBox.Show("Failed to load base instruction set. " + InstructionLoader.BASE_INSTRUCTION_SET_FILENAME + InstructionLoader.INSTRUCTION_SET_FILE_EXTENSION
                    + " file corrupted. Load another instruction set to use aplication. Details: " + ex.Message);
                Close();
            }
            InitializeComponent();

            codeEditor = new CodeEditor();
            Debugger = new Debugger();
            Machine = new ControlUnit();


            Debugger.SetCodeEditorHandle(codeEditor.GetCodeLinesHandle());
            Debugger.OnSetExecutedLine += UserControlCodeEditor.SetExecutedLine;
            Debugger.OnSetExecutedMicroinstructions += userControlInstructionList1.SelectCurrentAciveInstruction;

            Machine.OnRefreshValues += RefreshMicrocontrolerControls; //Set method for refreshing components values on each tick
            Machine.OnSetExecutedLine += Debugger.SetExecutedLine;
            Machine.OnSetExecutedMicroinstruction += Debugger.SetExecutedMicronstructions;
            Machine.OnProgramEnd += EndOfProgram;

            // uC UI

            UserControlCodeEditor.SetCodeLinesHandle(codeEditor.GetCodeLinesHandle());
            UserControlIntButton.OnSetRequestValue += RefreshMicrocontrolerControls;

            SetMachineComponentsViewHandles();
            RefreshMicrocontrolerControls();

            // IO's
            UserControlCharacterInput.SetCharactersBufferSource(Machine.GetTextInputBufferHandle());

            // GUI
            userControlInstructionList1.SetMicrocodeViewHandle(userControlInstructionMicrocode1);
            RefreshRightPanelControls();

        }
        private void Form1_Load(object sender, EventArgs e) {
            if (Environment.OSVersion.Platform != PlatformID.Unix) {
                unixCodeEditorMenuStrip.Enabled = false;
                unixCodeEditorMenuStrip.Visible = false;
            }

        }

        private void SetMachineComponentsViewHandles() {
            MemoryControl.SetItemsValueSource(Machine.GetMemoryContentHandle());
            UserControlRegisterA.SetSourceRegister(Machine.A);
            UserControlRegisterS.SetSourceRegister(Machine.S);
            UserControlRegisterI.SetSourceRegister(Machine.I);
            UserControlRegisterL.SetSourceRegister(Machine.L);
            UserControlRegisterAK.SetSourceRegister(Machine.AK);
            UserControlRegisterX.SetSourceRegister(Machine.X);
            UserControlRegisterY.SetSourceRegister(Machine.Y);
            UserControlRegisterRB.SetSourceRegister(Machine.RB);
            UserControlRegisterG.SetSourceRegister(Machine.G);
            UserControlRegisterWS.SetSourceRegister(Machine.WS);
            UserControlRegisterRZ.SetSourceRegister(Machine.RZ);
            UserControlRegisterRM.SetSourceRegister(Machine.RM);
            UserControlRegisterRP.SetSourceRegister(Machine.RP);
            UserControlRegisterAP.SetSourceRegister(Machine.AP);
            userControlBusData.SetSourceBus(Machine.MagS);
            userControlBusAddress.SetSourceBus(Machine.MagA);

            userControlIntButton1.SetIntRequestRegisterHandle(Machine.RZ);
            userControlIntButton2.SetIntRequestRegisterHandle(Machine.RZ);
            userControlIntButton3.SetIntRequestRegisterHandle(Machine.RZ);
            userControlIntButton4.SetIntRequestRegisterHandle(Machine.RZ);
        }

        private void RefreshControls(Control control) {
            control.Refresh();
            if (control.HasChildren == false) return;
            foreach (Control con in control.Controls)
                RefreshControls(con);
        }
        private void RefreshMicrocontrolerControls() {
            RefreshControls(MicrocontrollerPanel);
        }

        private void RefreshRightPanelControls() {
            RefreshControls(TopRightPanel);
        }
        private void RefreshTabPanelControls() {
            RefreshControls(tabControlEditors);
        }

        private void RefreshAfterSet(uint oldAddressSpace) {
            Machine.SetComponentsBitsizes();
            Machine.ChangeMemorySize(oldAddressSpace);
            RefreshMicrocontrolerControls();
        }

        //=====================< Central Unit Manual Constrol >============================== 

        private void EndOfProgram() {
            System.Media.SystemSounds.Exclamation.Play();
            MessageBox.Show("Program został zakończony.", "Maszyna Pi");
        }

        private void programToolStripMenuItem1_Click(object sender, EventArgs e) {
            try {
                Machine.ManualProgram();
                RefreshMicrocontrolerControls();
            } catch (CentralUnitException cEx) {
                MessageBox.Show(cEx.Message.Replace(GetErrorType(cEx.Message), ""), GetErrorType(cEx.Message));
            } catch (Exception ex) {
                MessageBox.Show("Unknown error while executing programm: " + ex.Message.Replace(GetErrorType(ex.Message), ""), GetErrorType(ex.Message));
            }

        }
        private void rozkazToolStripMenuItem1_Click(object sender, EventArgs e) {
            try {
                Machine.ManualInstruction();
                RefreshMicrocontrolerControls();
            } catch (CentralUnitException cEx) {
                MessageBox.Show(cEx.Message.Replace(GetErrorType(cEx.Message), ""), GetErrorType(cEx.Message));
            } catch (Exception ex) {
                MessageBox.Show("Unknown error while executing programm: " + ex.Message.Replace(GetErrorType(ex.Message), ""), GetErrorType(ex.Message));
            }
        }

        private void taktToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        // ================ CodeEditor =======================================
        private string GetErrorType(string errMsg) {
            int starttype = errMsg.IndexOf("[");
            int endtype = errMsg.IndexOf("]");
            if (starttype != -1 && starttype < endtype) return errMsg.Substring(starttype + 1, endtype);
            return "Error";
        }

        private void Compile() {
            try {
                //codeEditor.SetCodeLinesFromEditorContent();
                if (codeEditor.IsInstructionDefinition()) {
                    bool isEnoughSpace = InstructionLoader.LoadSingleInstruction(codeEditor.FormatMicroinstructionsCode());
                    System.Media.SystemSounds.Exclamation.Play();
                    if (isEnoughSpace) MessageBox.Show("Rozkaz został dodany.", "Maszyna Pi");
                    else MessageBox.Show("Rozkaz został dodany lecz nie będzie widoczny (zbyt mała ilość bitów kodu)", "Warning!");
                    return;
                }
                if (codeEditor.IsProgram()) {
                    List<uint> code = Compiler.CompileCode(codeEditor.FormatCodeForCompiler());
                    Debugger.FillMemoryLineNumberMap();
                    Machine.SetMemoryContent(code);
                    Machine.ResetRegisters();
                    RefreshMicrocontrolerControls();
                    System.Media.SystemSounds.Exclamation.Play();
                    MessageBox.Show("Program został skompilowany.", "Maszyna Pi");
                    return;
                }
                MessageBox.Show("Compilation Error: Unknown syntax type - not program or instruction definition.", "Error");
            } catch (CompilerException ex) {
                MessageBox.Show(ex.Message, GetErrorType(ex.Message));
            } catch (Exception ex) {
                MessageBox.Show("Unexpected Compilation Error from " + ex.Source + ": " + ex.Message + ". Stack: " + ex.StackTrace, "Error");
            }
        }

        private void CompileItemToolStrip_Click(object sender, EventArgs e) { Compile(); }
        // Code Editor unix toolstrip
        private void kompilujToolStripMenuItem_Click(object sender, EventArgs e) { Compile(); }


        // Menu Bar things
        private void ładujListęRozkazówToolStripMenuItem_Click(object sender, EventArgs e) {
            string lst = InstructionLoader.INSTRUCTION_SET_FILE_EXTENSION;
            string filepath = "";
            string lstFileContent = "";
            string filter = "pliki rozkazów (*" + lst + ")|*" + lst;
            if (FilesHandler.PointFileAndGetText(filter, out filepath, out lstFileContent)) {
                try {
                    uint oldAddressSpace = ArchitectureSettings.GetAddressSpace();
                    bool isEnoughSpace = InstructionLoader.LoadInstructionsFile(lstFileContent);
                    Machine.ChangeMemorySize(oldAddressSpace);
                    Machine.SetComponentsBitsizes();                                //  \/ Shouldn't happen if .lst file created properly \/
                    if (!isEnoughSpace) MessageBox.Show("Lista rozkazów załadowana lecz część rozkazów może nie być widoczna (zbyt mała ilość bitów kodu)", "Warning!");
                } catch (InstructionLoaderException ex) {
                    MessageBox.Show("Error while loading " + lst + " file " + filepath + "\n" + ex.Message, "Instruction Loader Error");
                }
                RefreshMicrocontrolerControls();
                RefreshControls(tabPageInstructionList);
            }
        }

        private void otwórzToolStripMenuItem_Click(object sender, EventArgs e) {
            string prg = Compiler.PROGRAM_FILE_EXTENSION;
            string rzk = InstructionLoader.INSTRUCTION_FILE_EXTENSION;
            string filepath = "";
            string fileContent = "";
            string filer = "pliki rozkazów (*" + rzk + ")|*" + rzk + "|pliki programów (*" + prg + ")|*" + prg + "|wszystkie pliki (*.*)|*.*";

            if (FilesHandler.PointFileAndGetText(filer, out filepath, out fileContent)) {
                UserControlCodeEditor.SetText(fileContent);
                LastUsedFilepath = filepath;
            }

        }



        private void wklejToolStripMenuItem_Click(object sender, EventArgs e) { UserControlCodeEditor.Paste(); }

        private void kopiujToolStripMenuItem_Click(object sender, EventArgs e) { UserControlCodeEditor.Copy(); }

        private void wytnijToolStripMenuItem_Click(object sender, EventArgs e) { UserControlCodeEditor.Cut(); }

        private void opcjeToolStripMenuItem_Click(object sender, EventArgs e) {
            FormProjectOptions projectOptions = new FormProjectOptions();
            var dialogResult = projectOptions.ShowDialog();
            if (dialogResult.Equals(DialogResult.OK)) {
                uint oldAddrSpace = projectOptions.GetOldAddressSpace();
                RefreshAfterSet(oldAddrSpace);
            }
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e) {
            Machine.ResetRegisters();
            RefreshMicrocontrolerControls();
        }


        // Non Machine-Related Interface Behaviour Methods

        private void tabControlEditorsPanel_SelectedIndexChanged(object sender, EventArgs e) {
            UserControlCodeEditor.ClearSelected();
            RefreshTabPanelControls();
        }


        private void SaveToFile() {
            if (LastUsedFilepath != null) {
                FilesHandler.OverwriteOrCreateFile(codeEditor.GetCodeLinesCopy(), LastUsedFilepath);
                return;
            }
            string prg = Compiler.PROGRAM_FILE_EXTENSION;
            string rzk = InstructionLoader.INSTRUCTION_FILE_EXTENSION;
            string filepath = "";
            string filter = "pliki rozkazów (*" + rzk + ")|*" + rzk + "|pliki programów (*" + prg + ")|*" + prg + "|wszystkie pliki (*.*)|*.*";

            if (FilesHandler.PointToOvervriteFileOrCreateNew(filter, out filepath)) {
                LastUsedFilepath = filepath;
                FilesHandler.OverwriteOrCreateFile(codeEditor.GetCodeLinesCopy(), LastUsedFilepath);
            }

        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e) { SaveToFile(); }
        private void saveUnixToolStripMenuItem_Click(object sender, EventArgs e) { SaveToFile(); }
        private void saveContexMenuItem_Click(object sender, EventArgs e) { SaveToFile(); }
    }
}
