using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MaszynaPi.MachineLogic;
using MaszynaPi.MachineAssembler;
using MaszynaPi.MachineUI;
using MaszynaPi.FilesHandling;
using MaszynaPi.MachineAssembler.Editors;

namespace MaszynaPi {
    public partial class Form1 : Form {

        bool PaintActiveSignals;
        string LastUsedFilepath;

        List<object> MachineComponents;
        List<UserControlSignalWire> SignalWires;

        /*
         * Machine manual control -> each activated signal adds its name to a list, which is then sorted and
         *transmitted to the machine for execution (ManualTick method / "ActiveSignals" setting). 
         *(only "Cycle" step possible with manual control)
         */

        readonly CodeEditor codeEditor;
        readonly CentralProcessingUnit Machine;
        readonly Debugger Debugger;

        System.Threading.Thread BreakDetectorThread;
        UI.BreakForm breakForm;
        bool BREAK_FLAG = false;

        public Form1() {
            //Must Be First!  [TODO Handle exception with loading for Raspbian -> allow user to select diferent instruction set]
            try { InstructionLoader.LoadBaseInstructions(); } catch (InstructionLoaderException ex) {
                MessageBox.Show("Failed to load base instruction set. " + Defines.BASE_INSTRUCTION_SET_FILENAME
                                + " file corrupted. Load another instruction set to use aplication. Details: " + ex.Message);
                FormProjectOptions projectOptions = new FormProjectOptions(onlyPaths:true);
                var dialogResult = projectOptions.ShowDialog();
                if (dialogResult.Equals(DialogResult.OK) == false) {
                    Close();
                    return;
                }
            }
            PaintActiveSignals = true;
            InitializeComponent();
            InitializeMachineComponentsList();
            InitializeSignalWiresList();

            codeEditor = new CodeEditor();
            Debugger = new Debugger();
            Machine = new CentralProcessingUnit();

            UserControlRegisterI.SetDisplayMode(mode: RegisterMode.Instruction);
            userControlFlags.FlagsValueRequest += Machine.GetALUFlags;
            
            userControlFlags.Enabled = false;
            userControlFlags.Visible = false;


            Debugger.SetCodeEditorHandle(codeEditor.GetCodeLinesHandle());
            Debugger.OnSetExecutedLine += UserControlCodeEditor.SetExecutedLine;
            Debugger.OnSetExecutedMicroinstructions += userControlInstructionList1.SelectCurrentAciveInstruction;

            Machine.OnRefreshValues += RefreshMicrocontrolerControls; //Set method for refreshing components values on each tick
            Machine.OnSetExecutedLine += Debugger.SetExecutedLine;
            Machine.OnSetExecutedMicroinstruction += Debugger.SetExecutedMicronstructions;
            Machine.OnProgramEnd += EndOfProgram;
            Machine.SetPaintActiveSignals += SetSignalsPaintOnRefresh;
            Machine.CheckProgramBreak += GetBreakFlag;

            ArchitectureSettings.InitializeInterruptVector();
            ArchitectureSettings.InitializeIODevicesAddresses();
            // uC UI

            UserControlCodeEditor.SetCodeLinesHandle(codeEditor.GetCodeLinesHandle());
            UserControlIntButton.OnSetRequestValue += RefreshMicrocontrolerControls;

            SetMachineComponentsViewHandles();
            RefreshMicrocontrolerControls();

            //Machine.SetOnInterruptReportedAction(JoystickInterruptReported);

            // IO's
            UserControlCharacterInput.SetCharactersBufferSource(Machine.GetTextInputBufferHandle());
            Machine.SetOnFetchCharAction(UserControlCharacterInput.OnCharacterFetched);
            UserControlCharacterOutput.SetCharactersBufferSource(Machine.GetTextOutputBufferHandle());
            Machine.SetOnPushCharAction(UserControlCharacterOutput.OnCharacterPushed);

            // GUI
            userControlInstructionList1.SetMicrocodeViewHandle(userControlInstructionMicrocode1);
            userControlBusAddress.SetBusValueToolTip(MainToolTip);
            userControlBusData.SetBusValueToolTip(MainToolTip);
            userControlBusAS.SetBusValueToolTip(MainToolTip);
            RefreshRightPanelControls();

        }
        private void Form1_Load(object sender, EventArgs e) {
            if (Environment.OSVersion.Platform != PlatformID.Unix) {
                unixCodeEditorMenuStrip.Enabled = false;
                unixCodeEditorMenuStrip.Visible = false;
            }
            UserControlRegisterRZ.StartRefreshing();
        }

        private void InitializeMachineComponentsList() {
            MachineComponents = new List<object> { MemoryControl,
                UserControlRegisterA, UserControlRegisterS, UserControlRegisterI,UserControlRegisterL,
                UserControlRegisterAK, UserControlRegisterX,UserControlRegisterY,UserControlRegisterRB,
                UserControlRegisterG,UserControlRegisterWS, UserControlRegisterRZ, UserControlRegisterRM,
                UserControlRegisterRP,UserControlRegisterAP, userControlFlags,
                userControlBusData,userControlBusAddress, userControlBusAS
            };
        }

        private void InitializeSignalWiresList() {
            SignalWires = new List<UserControlSignalWire> { userControlSignalWire_id, userControlSignalWire1_od, userControlSignalWire2, userControlSignalWire_t,
            userControlSignalWire_add, userControlSignalWire_and, userControlSignalWire_dcacc, userControlSignalWire_dcsp,
            userControlSignalWire_div, userControlSignalWire_eni, userControlSignalWire_ia, userControlSignalWire_ialu, userControlSignalWire_ibuf,
            userControlSignalWire_icacc, userControlSignalWire_icit, userControlSignalWire_icsp, userControlSignalWire_id, userControlSignalWire_iins,
            userControlSignalWire_iit, userControlSignalWire_im, userControlSignalWire_isp, userControlSignalWire_ix, userControlSignalWire_iy,
            userControlSignalWire_mul, userControlSignalWire_not, userControlSignalWire_oa, userControlSignalWire_oacc, userControlSignalWire_obuf,
            userControlSignalWire_oim, userControlSignalWire_oit, userControlSignalWire_oitd, userControlSignalWire_oiv, userControlSignalWire_or, userControlSignalWire_ord,
            userControlSignalWire_osp, userControlSignalWire_ox, userControlSignalWire_oy, userControlSignalWire_rd, userControlSignalWire_rint, userControlSignalWire_shr,
            userControlSignalWire_start, userControlSignalWire_stop, userControlSignalWire_sub, userControlSignalWire_wr, userControlSignalWire_wracc};
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
            userControlBusAS.SetSourceBus(Machine.MagT);

            userControlIntButton1.SetIntRequestRegisterHandle(Machine.RZ);
            userControlIntButton2.SetIntRequestRegisterHandle(Machine.RZ);
            userControlIntButton3.SetIntRequestRegisterHandle(Machine.RZ);
            userControlIntButton4.SetIntRequestRegisterHandle(Machine.RZ);
        }

        private void SetSignalsPaintOnRefresh(bool paint) {
            PaintActiveSignals = paint;
        }

        private void RefreshControls(Control control) {
            control.Refresh();
            if (control.HasChildren == false) return;
            foreach (Control con in control.Controls)
                RefreshControls(con);
        }
        private void RefreshMicrocontrolerControls(bool stopBusRefresh = false) {
            //RefreshControls(MicrocontrollerPanel);
            foreach (var instance in MachineComponents) {
                if (false == (stopBusRefresh && (instance is UserControlBus)) )
                    (instance as Control)?.Refresh();
            }
            List<string> ActiveSignals;
            if (PaintActiveSignals == false || (ActiveSignals = Machine.GetActiveSignals()) == null) 
                return;

            foreach(var wire in SignalWires) {
                if (ActiveSignals.Contains(wire.SignalName)) {
                    wire.Activate();
                } else {
                    wire.Deactivate();
                }
            }

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

        private void DisableManuallySetSignals() {
            foreach (var wire in SignalWires) {
                wire.Deactivate();
            }
        }

        // sorts the microinstruction signals so that register output signals are always prioritized
        private List<string> SortSignals(List<string> activeSignals) {
            var inputSignals = activeSignals.Where(s => (s.StartsWith("i") && s.StartsWith("ic") == false));
            var outputSignals = activeSignals.Where(s => s.StartsWith("o"));
            var transBusSignal = activeSignals.Where(s => s.Equals("tbs"));
            var specialSignals = activeSignals.Where(s => (s.Equals("start") || s.Equals("stop")));
            var otherSignals = activeSignals.Except(inputSignals).Except(outputSignals).Except(specialSignals).Except(transBusSignal);
            return otherSignals.Concat(outputSignals).Concat(transBusSignal).Concat(inputSignals).Concat(specialSignals).ToList();
        }

        private List<string> GetManualActiveSignals() {
            if (UserControlSignalWire.ManualControl == false) return null;
            List<string> activeSignals = new List<string>();
            foreach (var wire in SignalWires) {
                if (wire.Active) activeSignals.Add(wire.SignalName);
            }
            return SortSignals(activeSignals);
        }

        //=====================< Central Unit Manual Control >============================== 

        private void EndOfProgram() {
            System.Media.SystemSounds.Exclamation.Play();
            MessageBox.Show("The program has ended.", "Pi Machine");
        }

        private void programToolStripMenuItem1_Click(object sender, EventArgs e) {
            try {
                BREAK_FLAG = false;
                RunBreakDetector();

                MemoryControl.PartiallySupressRefreshing = true;
                DisableManuallySetSignals();
                
                Machine.ManualProgram();
                RefreshMicrocontrolerControls();

            } catch (CPUException cEx) {
                MessageBox.Show(cEx.Message, "CPU Program Error");
            } catch (Exception ex) {        
                MessageBox.Show(ex.Message, "Unknown Program Error");
            } finally
            {
                //CancelBreakDetector();
                MemoryControl.PartiallySupressRefreshing = false;
                MemoryControl.Refresh();
            }

        }
        private void rozkazToolStripMenuItem1_Click(object sender, EventArgs e) {
            try {
                DisableManuallySetSignals();
                Machine.ManualInstruction();
                RefreshMicrocontrolerControls();
            } catch (CPUException cEx) {
                MessageBox.Show(cEx.Message, "CPU Instruction Error");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Unknown Instruction Error");
            }
        }

        private void taktToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                List<string> signals = GetManualActiveSignals();
                Machine.ManualTick(signals);
            } catch (CPUException cEx) {
                MessageBox.Show(cEx.Message, "CPU Tick Error");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Unknown Tick Error");
            }
        }

        private void clearOutputConsoleToolStripMenuItem_Click(object sender, EventArgs e) {
            UserControlCharacterOutput.Reset();
        }

        private void checkBoxManualDebug_CheckedChanged(object sender, EventArgs e) {
            if (checkBoxManualDebug.Checked) {
                UserControlSignalWire.ManualControl = true;
                programToolStripMenuItem1.Enabled = false;
                rozkazToolStripMenuItem1.Enabled = false;
            } else {
                UserControlSignalWire.ManualControl = false;
                programToolStripMenuItem1.Enabled = true;
                rozkazToolStripMenuItem1.Enabled = true;
            }
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
                    if (isEnoughSpace) MessageBox.Show("The instruction has been added.", "Pi Machine");
                    else MessageBox.Show("The instruction has been added but will not be visible (too few code bits)", "Warning!");
                    return;
                }
                if (codeEditor.IsProgram()) {
                    List<uint> code = Assembler.CompileCode(codeEditor.FormatCodeForCompiler());
                    Debugger.FillMemoryLineNumberMap();
                    Machine.SetMemoryContent(code);
                    Machine.ResetRegisters();
                    RefreshMicrocontrolerControls();
                    System.Media.SystemSounds.Exclamation.Play();
                    MessageBox.Show("Compiled.", "Pi Machine");
                    return;
                }
                MessageBox.Show("Compilation Error: Unknown syntax type - not program or instruction definition.", "Error");
            } catch (CompilerException ex) {
                MessageBox.Show(ex.Message, "Compiler Error");
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
            string filter = "instruction files (*" + lst + ")|*" + lst;
            if (FilesHandler.PointFileAndGetText(filter, out string filepath, out string lstFileContent)) {
                try {
                    uint oldAddressSpace = ArchitectureSettings.GetAddressSpace();
                    bool isEnoughSpace = InstructionLoader.LoadInstructionsFromFileContent(lstFileContent);
                    Machine.ChangeMemorySize(oldAddressSpace);
                    Machine.SetComponentsBitsizes();                                //  \/ Shouldn't happen if .lst file created properly \/
                    if (!isEnoughSpace) MessageBox.Show("instruction list has been added but will not be visible (too few code bits)", "Warning!");
                } catch (InstructionLoaderException ex) {
                    MessageBox.Show("Error while loading " + lst + " file " + filepath + "\n" + ex.Message, "Instruction Loader Error");
                }
                RefreshMicrocontrolerControls();
                RefreshControls(tabPageInstructionList);
            }
        }

        private void otwórzToolStripMenuItem_Click(object sender, EventArgs e) {
            string prg = Assembler.PROGRAM_FILE_EXTENSION;
            string rzk = InstructionLoader.INSTRUCTION_FILE_EXTENSION;
            string filer = "instruction files (*" + rzk + ")|*" + rzk + "|program files (*" + prg + ")|*" + prg + "|All files (*.*)|*.*";

            if (FilesHandler.PointFileAndGetText(filer, out string filepath, out string fileContent)) {
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
            UserControlCharacterOutput.Reset();
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
            string prg = Assembler.PROGRAM_FILE_EXTENSION;
            string rzk = InstructionLoader.INSTRUCTION_FILE_EXTENSION;
            string filter = "instruction files (*" + rzk + ")|*" + rzk + "|program files (*" + prg + ")|*" + prg + "|All files (*.*)|*.*";

            if (FilesHandler.PointToOvervriteFileOrCreateNew(filter, out string filepath)) {
                LastUsedFilepath = filepath;
                FilesHandler.OverwriteOrCreateFile(codeEditor.GetCodeLinesCopy(), LastUsedFilepath);
            }

        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { SaveToFile(); }
            catch (Exception ex) { MessageBox.Show($"Cannot save content to file: {ex}", "Error"); }
        }
        private void saveUnixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { SaveToFile(); } 
            catch (Exception ex) { MessageBox.Show($"Cannot save content to file: {ex}", "Error"); }
        }
        private void saveContexMenuItem_Click(object sender, EventArgs e) 
        { 
            try { SaveToFile(); } 
            catch (Exception ex) { MessageBox.Show($"Cannot save content to file: {ex}", "Error");  } 
        }

        private void letterToolStripMenuItem_Click(object sender, EventArgs e) {
            if (letterToolStripMenuItem.Checked) {
                paintToolStripMenuItem.Checked = false;
                Machine.SetLEDMatrixModeLetter();
            }
        }

        private void paintToolStripMenuItem_Click(object sender, EventArgs e) {
            if (paintToolStripMenuItem.Checked) {
                letterToolStripMenuItem.Checked = false;
                Machine.SetLEDMatrixModePaint();
            }
        }

        private void polishToolStripMenuItem_Click(object sender, EventArgs e) {
            if (polishToolStripMenuItem.Checked) {
                englishToolStripMenuItem.Checked = false;
                Defines.SetInstructionsLanguageVersion(Defines.Lang.PL);
                Machine.InitialazeSignalsMap();
            }
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e) {
            if (englishToolStripMenuItem.Checked) {
                polishToolStripMenuItem.Checked = false;
                Defines.SetInstructionsLanguageVersion(Defines.Lang.ENG);
                Machine.InitialazeSignalsMap();
            }
        }

        void SetRegistersDisplayMode(RegisterMode mode) {
            foreach(var component in MachineComponents) {
                if(component is UserControlRegister) {
                    (component as UserControlRegister).SetDisplayMode(mode);
                    (component as UserControlRegister).Refresh();
                }
            }
        }
        private void unsignedDecimalToolStripMenuItem_Click(object sender, EventArgs e) {SetRegistersDisplayMode(RegisterMode.Dec);}
        private void signedDecimalToolStripMenuItem_Click(object sender, EventArgs e) {SetRegistersDisplayMode(RegisterMode.Signed);}
        private void hexadecimalToolStripMenuItem_Click(object sender, EventArgs e) {SetRegistersDisplayMode(RegisterMode.Hex);}
        private void binaryToolStripMenuItem_Click(object sender, EventArgs e) { SetRegistersDisplayMode(RegisterMode.Bin); }


        void JoystickInterruptReported() {
            bool wasRuning = false;
            if (BreakDetectorThread.IsAlive) { CancelBreakDetector(); wasRuning = true; }
            UserControlRegisterRZ.Refresh();
            if (wasRuning) RunBreakDetector();
        }

        //==============================================================================================================================
        //Special Tasks
        bool GetBreakFlag() { return BREAK_FLAG; }

        void CancelBreakDetector() { CloseBreakDetector(null, null); }

        void CreateBreakButton() {
            breakForm?.Close();
            breakForm?.Dispose();
            breakForm = new UI.BreakForm();
            
        }

        void ShowBreakForm() {
            try { BREAK_FLAG = breakForm.ShowDialog().Equals(DialogResult.OK); } 
            finally { breakForm?.ForceClose(); breakForm?.Dispose(); } 
            
        }

        void RunBreakDetector() {
            CreateBreakButton();
            BreakDetectorThread = new System.Threading.Thread(new System.Threading.ThreadStart(ShowBreakForm));
            BreakDetectorThread.Start();
            breakForm.BringToFront();
        }

        void CloseBreakDetector(object sender, RunWorkerCompletedEventArgs e) {
            try { breakForm?.ForceClose(); } 
            finally { BreakDetectorThread?.Join(); } // BREAK_FLAG = (breakForm != null) && breakForm.DialogResult.Equals(DialogResult.OK);
            
        }
    }
}
