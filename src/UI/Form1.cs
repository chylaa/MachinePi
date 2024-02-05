using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MaszynaPi.MachineLogic;
using MaszynaPi.MachineAssembler;
using MaszynaPi.MachineUI;
using MaszynaPi.FilesHandling;
using MaszynaPi.MachineAssembler.Editors;

namespace MaszynaPi {

    /// <summary>Application main <see cref="Form"/>, handling CPU, IOs and Editors views. </summary>
    public partial class Form1 : Form 
    {

        /// <summary>Indicates that <see cref="CentralProcessingUnit.ExecuteProgram"/> was called.</summary>
        bool CPUProgramExecuting;
        /// <summary>Indicates that <see cref="Form1"/> should repaint all controls with <see cref="UserControlSignalWire.Active"/> field set.</summary>
        bool PaintActiveSignals;
        /// <summary>Last path used in file-open/save operation.</summary>
        string LastUsedFilepath;

        /// <summary>All CPU view <see cref="UserControl"/>s.</summary>
        List<object> MachineComponents;
        /// <summary>List of all <see cref="UserControlSignalWire"/> objects in CPU view.</summary>
        List<UserControlSignalWire> SignalWires;

        readonly CodeEditor codeEditor;
        readonly CentralProcessingUnit Machine;
        readonly Debugger Debugger;

        bool PROG_BREAK_FLAG = false;

        /// <summary>Creates application main <see cref="Form"/>.</summary>
        public Form1() 
        {
            //Must Be First!  [TODO Handle exception with loading for Raspbian -> allow user to select diferent instruction set]
            try { InstructionLoader.LoadBaseInstructions(); } catch (InstructionLoaderException ex) {
                MessageBox.Show("Failed to load base instruction set. " + Defines.BASE_INSTRUCTION_SET_FILENAME
                                + " file corrupted. Load another instruction set to use aplication. Details: " + ex.Message,
                                "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FormProjectOptions projectOptions = new FormProjectOptions(onlyPaths:true);
                var dialogResult = projectOptions.ShowDialog();
                if (dialogResult.Equals(DialogResult.OK) == false) {
                    Close();
                    return;
                }
            }
            CPUProgramExecuting = false;
            PaintActiveSignals = true;

            InitializeComponent();
            InitializeMachineComponentsList();
            InitializeSignalWiresList();

            codeEditor = new CodeEditor();
            Debugger = new Debugger();
            Machine = new CentralProcessingUnit();

            Debugger.SetCodeEditorHandle(codeEditor.GetCodeLinesHandle());
            Debugger.OnSetExecutedLine += UserControlCodeEditor.SetExecutedLine;
            Debugger.OnSetExecutedMicroinstructions += userControlInstructionList1.SelectCurrentAciveInstruction;

            Machine.OnRefreshValues += RefreshCPUControls; //Set method for refreshing components values on each tick
            Machine.OnSetExecutedLine += Debugger.SetExecutedLine;
            Machine.OnSetExecutedMicroinstruction += Debugger.SetExecutedMicronstructions;
            Machine.OnProgramEnd += EndOfProgram;
            Machine.SetPaintActiveSignals += SetSignalsPaintOnRefresh;
            Machine.CheckProgramBreak += DoEventsGetBreakFlag;

            ArchitectureSettings.InitializeInterruptVector();
            ArchitectureSettings.InitializeIODevicesAddresses();
            // uC UI

            UserControlCodeEditor.SetCodeLinesHandle(codeEditor.GetCodeLinesHandle());
            UserControlIntButton.OnSetRequestValue += RefreshCPUControls;

            SetMachineComponentsViewHandles();
            RefreshCPUControls();

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
            RefreshControls(TopRightPanel);

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
                UserControlRegisterRP,UserControlRegisterAP, UserControlRegisterF,
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
            UserControlRegisterF.SetSourceRegister(Machine.F);
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

            UserControlRegisterI.SetRegisterType(type: UserControlRegister.RegisterType.Instruction);
            UserControlRegisterF.SetRegisterType(type: UserControlRegister.RegisterType.Flag);
            UserControlRegisterRZ.SetRegisterType(type: UserControlRegister.RegisterType.Interruption);

            // Set new value of Flags register if Accumulator values changed by hand
            UserControlRegisterAK.MouseDoubleClick += new MouseEventHandler(
                delegate { Machine.SetALUFlagsBaseOnAccumulator(); UserControlRegisterF.Refresh(); }
            );
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
        private void RefreshCPUControls() {
            foreach (var instance in MachineComponents) {
                if (false == (CPUProgramExecuting && (instance is UserControlBus)) )
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

        private void RefreshAfterSet(uint oldAddressSpace) {
            Machine.SetComponentsBitsizes();
            Machine.ChangeMemorySize(oldAddressSpace);
            RefreshCPUControls();
        }

        private void DisableManuallySetSignals() {
            foreach (var wire in SignalWires) {
                wire.Deactivate();
            }
        }

        /// <summary>Sorts the microinstruction signals so that register output signals are always prioritized.</summary>
        /// <param name="activeSignals">List of signals names to be sorted.</param>
        /// <returns>Sorted list of signal names: output->transit->input->special signals.</returns>
        private List<string> SortSignals(List<string> activeSignals) {
            var inputSignals = activeSignals.Where(s => (s.StartsWith("i") && s.StartsWith("ic") == false));
            var outputSignals = activeSignals.Where(s => s.StartsWith("o"));
            var transBusSignal = activeSignals.Where(s => s.Equals("tbs"));
            var accSignals = activeSignals.Where(s => s.Equals("icacc") || s.Equals("dcacc"));
            var specialSignals = activeSignals.Where(s => (s.Equals("start") || s.Equals("stop")));
            var otherSignals = activeSignals.Except(inputSignals).Except(outputSignals).Except(specialSignals).Except(transBusSignal).Except(accSignals);
            return otherSignals.Concat(outputSignals).Concat(transBusSignal).Concat(inputSignals).Concat(accSignals).Concat(specialSignals).ToList();
        }

        /// <summary>
        /// Machine manual control -> each <see cref="UserControlSignalWire.Active"/> signal adds its name to a list,
        /// which is then sorted (<see cref="SortSignals(List{string})"/>) and pass to the machine for execution
        /// using <see cref="CentralProcessingUnit.ManualTick(List{string})"/> method.
        /// <br></br>Note: only "Tick" step possible with manual control enabled.
        /// </summary>
        /// <returns>List of names of signals activated by user, sored by <see cref="SortSignals(List{string})"/>.</returns>
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
                
                CPUProgramExecuting = true;
                MemoryControl.PartiallySupressRefreshing = true;
                checkBoxManualDebug.Enabled = false;
                DisableManuallySetSignals();
                CreateBreakButton(visible:true, resetBreakFlag:true);

                Machine.ManualProgram();
                RefreshCPUControls();

            } catch (CPUException cEx) {
                MessageBox.Show(cEx.Message, "CPU Program Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Unknown Program Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally
            {
                DestroyBreakButton(resetBreakFlag: true);
                CPUProgramExecuting = false;
                MemoryControl.PartiallySupressRefreshing = false;
                checkBoxManualDebug.Enabled = true;
                MemoryControl.Refresh();

            }

        }
        private void rozkazToolStripMenuItem1_Click(object sender, EventArgs e) {
            try {
                DisableManuallySetSignals();
                Machine.ManualInstruction();
                RefreshCPUControls();
            } catch (CPUException cEx) {
                MessageBox.Show(cEx.Message, "CPU Instruction Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Unknown Instruction Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void taktToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                List<string> signals = GetManualActiveSignals();
                Machine.ManualTick(signals);
            } catch (CPUException cEx) {
                MessageBox.Show(cEx.Message, "CPU Tick Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Unknown Tick Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void Compile(CodeEditor.Definition assume = CodeEditor.Definition.Unknown) 
        {
            CodeEditor.Definition detected = (assume == CodeEditor.Definition.Unknown)
                                                ? codeEditor.DetectCodeType()
                                                : assume;
            try {

                if (detected == CodeEditor.Definition.Instruction) {
                    bool isEnoughSpace = InstructionLoader.LoadSingleInstruction(codeEditor.FormatMicroinstructionsCode());
                    System.Media.SystemSounds.Exclamation.Play();
                    if (isEnoughSpace) MessageBox.Show("The instruction has been added.", "Success");
                    else MessageBox.Show("The instruction has been added but will not be visible (too few code bits)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (detected == CodeEditor.Definition.Program) {
                    List<uint> code = Assembler.CompileCode(codeEditor.FormatCodeForCompiler());
                    Debugger.FillMemoryLineNumberMap();
                    Machine.SetMemoryContent(code);
                    Machine.ResetRegisters();
                    RefreshCPUControls();
                    System.Media.SystemSounds.Exclamation.Play();
                    MessageBox.Show("Compiled.", "Pi Machine");
                    return;
                }
                if (assume == CodeEditor.Definition.Unknown)
                {
                    MessageBox.Show("Compilation Error: Unknown syntax type - not program or instruction definition.", "Error");
                    var isprogram = MessageBox.Show("Is your code a program?", "Select type of code to assume", MessageBoxButtons.YesNoCancel);
                    if(isprogram != DialogResult.Cancel)
                        Compile(isprogram == DialogResult.Yes ? CodeEditor.Definition.Program : CodeEditor.Definition.Instruction);
                }
            } catch (CompilerException ex) {
                MessageBox.Show(ex.Message, $"{detected} Compiler Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } catch (Exception ex) {
                MessageBox.Show("Unexpected Compilation Error from " + ex.Source + ": " + ex.Message + ". Stack: " + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CompileItemToolStrip_Click(object sender, EventArgs e) { Compile(); }


        // Menu Bar things
        private void ładujListęRozkazówToolStripMenuItem_Click(object sender, EventArgs e) {
            string lst = Defines.INSTRUCTION_SET_FILE_EXTENSION;
            string filter = "instruction files (*" + lst + ")|*" + lst;
            if (FilesHandler.PointFileAndGetText(filter, LastUsedFilepath, out string filepath, out string lstFileContent)) {
                try {
                    uint oldAddressSpace = ArchitectureSettings.GetAddressSpace();
                    bool isEnoughSpace = InstructionLoader.LoadInstructionsFromFileContent(lstFileContent);
                    Machine.ChangeMemorySize(oldAddressSpace);
                    Machine.SetComponentsBitsizes();                                //  \/ Shouldn't happen if .lst file created properly \/
                    if (!isEnoughSpace) MessageBox.Show("Instruction list has been added but will not be visible (too few code bits)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else MessageBox.Show($"New Instruction Set {System.IO.Path.GetFileName(filepath)} loaded!", "Success");
                } catch (InstructionLoaderException ex) {
                    MessageBox.Show("Error while loading " + lst + " file " + filepath + "\n" + ex.Message, "Instruction Loader Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                RefreshCPUControls();
                RefreshControls(tabPageInstructionList);
            }
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e) {
            Machine.ResetRegisters();
            UserControlCharacterOutput.Reset();
            RefreshCPUControls();
        }

        void SetRegistersDisplayMode(UserControlRegister.ValueMode mode) {
            foreach (var component in MachineComponents) {
                if (component is UserControlRegister) {
                    (component as UserControlRegister).SetDisplayMode(mode);
                    (component as UserControlRegister).Refresh();
                }
            }
        }

        private void DisplayModeToolStripMenuItem_Click(object sender, EventArgs e) {
            if (sender == unsignedDecimalToolStripMenuItem)
                SetRegistersDisplayMode(UserControlRegister.ValueMode.Dec);
            else if (sender == signedDecimalToolStripMenuItem)
                SetRegistersDisplayMode(UserControlRegister.ValueMode.Signed);
            else if (sender == hexadecimalToolStripMenuItem)
                SetRegistersDisplayMode(UserControlRegister.ValueMode.Hex);
            else if (sender == binaryToolStripMenuItem)
                SetRegistersDisplayMode(UserControlRegister.ValueMode.Bin);
        }

        // Non Machine-Related Interface Behaviour Methods

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

        private void tabControlEditorsPanel_SelectedIndexChanged(object sender, EventArgs e) {
            UserControlCodeEditor.ClearSelected();
            RefreshControls(tabControlEditors);
        }

        private string GetPrgInstFileFiler(CodeEditor.Definition assume = CodeEditor.Definition.Unknown)
        {
            string prgfilter = "Program files (*" + Defines.PROGRAM_FILE_EXTENSION + ")|*" + Defines.PROGRAM_FILE_EXTENSION;
            string rzkfilter = "Instruction files (*" + Defines.INSTRUCTION_FILE_EXTENSION + ")|*" + Defines.INSTRUCTION_FILE_EXTENSION;
            string allfiler = "All files (*.*)|*.*";

            bool instructionFirst;
            if (assume != CodeEditor.Definition.Unknown) instructionFirst = (assume == CodeEditor.Definition.Instruction);
            else instructionFirst = ((LastUsedFilepath != null) && LastUsedFilepath.EndsWith(Defines.INSTRUCTION_FILE_EXTENSION));

            return instructionFirst ? (rzkfilter + '|' + prgfilter + '|' + allfiler) : (prgfilter + '|' + rzkfilter + '|' + allfiler);
        }

        private void otwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FilesHandler.PointFileAndGetText(GetPrgInstFileFiler(), LastUsedFilepath, out string filepath, out string fileContent))
            {
                UserControlCodeEditor.SetText(fileContent);
                LastUsedFilepath = filepath;
            }
        }

        private void SaveToFile() 
        {
            if (LastUsedFilepath != null) {
                FilesHandler.OverwriteOrCreateFile(codeEditor.GetCodeLinesCopy(), LastUsedFilepath);
                return;
            }

            CodeEditor.Definition codeType = codeEditor.DetectCodeType();
            string initDir = FilesHandler.ValidDirOrCurrent(LastUsedFilepath);
            if (FilesHandler.PointToOvervriteFileOrCreateNew(GetPrgInstFileFiler(codeType), initDir, out string filepath)) {
                LastUsedFilepath = filepath;
                FilesHandler.OverwriteOrCreateFile(codeEditor.GetCodeLinesCopy(), LastUsedFilepath);
            }

        }

        private void saveToFile_Click(object sender, EventArgs e)
        {
            try { SaveToFile(); } 
            catch (Exception ex) { MessageBox.Show($"Cannot save content to file: {ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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


        //==============================================================================================================================
        // Program Break button handling
        bool DoEventsGetBreakFlag() { Application.DoEvents();  return PROG_BREAK_FLAG; }

        void CreateBreakButton(bool visible = true, bool resetBreakFlag = true) {
            PROG_BREAK_FLAG &= !resetBreakFlag;  
            Button progBreak = new Button() { 
                FlatStyle = FlatStyle.Flat, Dock = DockStyle.Fill,
                Text = "Break Program", Visible = visible 
            };
            progBreak.Click += new EventHandler(delegate { PROG_BREAK_FLAG = true; });
            breakPanel.Controls.Add(progBreak);
        }

        void SetVisibleBreakButton(bool vis) {
            if (breakPanel.Controls.Count > 0)
                breakPanel.Controls[0].Visible = vis;
        }

        void DestroyBreakButton(bool resetBreakFlag = true)
        {
            PROG_BREAK_FLAG &= !resetBreakFlag; 
            if (breakPanel.Controls.Count > 0)            
                breakPanel.Controls[0].Dispose();         
            breakPanel.Controls.Clear();                  
        }

    }
}
