﻿
namespace MaszynaPi {
    partial class Form1 {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.tabControlOnBottomPanel = new System.Windows.Forms.TabControl();
            this.tabPageInput = new System.Windows.Forms.TabPage();
            this.UserControlCharacterInput = new MaszynaPi.MachineUI.UserControlCharacterInput();
            this.tabPageOutput = new System.Windows.Forms.TabPage();
            this.UserControlCharacterOutput = new MaszynaPi.MachineUI.UserControlCharacterOutput();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.TopLeftPanel = new System.Windows.Forms.Panel();
            this.CPUMainPanel = new System.Windows.Forms.Panel();
            this.UserControlRegisterF = new MaszynaPi.MachineUI.UserControlRegister();
            this.breakPanel = new System.Windows.Forms.Panel();
            this.userControlSignalWire_stop = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_oitd = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_oa = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_t = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_osp = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_isp = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_dcsp = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_icsp = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_icit = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_iit = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_oit = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_iins = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_start = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_ord = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_ibuf = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_obuf = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_oy = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_iy = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_ox = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_ix = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_oiv = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_oim = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_im = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_eni = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_rint = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_shr = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_div = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_mul = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_wracc = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_sub = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_add = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_icacc = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_and = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire2 = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_or = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_dcacc = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_not = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_oacc = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_ialu = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_wr = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_rd = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_ia = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire1_od = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlSignalWire_id = new MaszynaPi.MachineUI.UserControlSignalWire();
            this.userControlIntButton4 = new MaszynaPi.MachineUI.UserControlIntButton();
            this.userControlIntButton3 = new MaszynaPi.MachineUI.UserControlIntButton();
            this.userControlIntButton2 = new MaszynaPi.MachineUI.UserControlIntButton();
            this.userControlIntButton1 = new MaszynaPi.MachineUI.UserControlIntButton();
            this.userControlBusAS = new MaszynaPi.MachineUI.UserControlBus();
            this.UserControlRegisterRM = new MaszynaPi.MachineUI.UserControlRegister();
            this.UserControlRegisterAP = new MaszynaPi.MachineUI.UserControlRegister();
            this.UserControlRegisterRP = new MaszynaPi.MachineUI.UserControlRegister();
            this.UserControlRegisterRZ = new MaszynaPi.MachineUI.UserControlRegister();
            this.UserControlRegisterWS = new MaszynaPi.MachineUI.UserControlRegister();
            this.UserControlRegisterX = new MaszynaPi.MachineUI.UserControlRegister();
            this.UserControlRegisterY = new MaszynaPi.MachineUI.UserControlRegister();
            this.UserControlRegisterRB = new MaszynaPi.MachineUI.UserControlRegister();
            this.UserControlRegisterG = new MaszynaPi.MachineUI.UserControlRegister();
            this.panelALUView = new System.Windows.Forms.Panel();
            this.UserControlRegisterAK = new MaszynaPi.MachineUI.UserControlRegister();
            this.UserControlRegisterL = new MaszynaPi.MachineUI.UserControlRegister();
            this.UserControlRegisterI = new MaszynaPi.MachineUI.UserControlRegister();
            this.userControlBusAddress = new MaszynaPi.MachineUI.UserControlBus();
            this.userControlBusData = new MaszynaPi.MachineUI.UserControlBus();
            this.MemoryControl = new MaszynaPi.MachineUI.UserControlMemory();
            this.checkBoxManualDebug = new System.Windows.Forms.CheckBox();
            this.UserControlRegisterS = new MaszynaPi.MachineUI.UserControlRegister();
            this.UserControlRegisterA = new MaszynaPi.MachineUI.UserControlRegister();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.plikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nowyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rozkazToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otwórzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wyjścieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.widokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearOutputConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registersDisplayModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unsignedDecimalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signedDecimalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hexadecimalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.binaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projektToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ładujListęRozkazówToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instructionLanguageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opcjeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.matrixModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.letterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wykonajToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.rozkazToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.taktToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.TopRightPanel = new System.Windows.Forms.Panel();
            this.ProgramPanel = new System.Windows.Forms.Panel();
            this.tabControlEditors = new System.Windows.Forms.TabControl();
            this.tabPageCodeEditor = new System.Windows.Forms.TabPage();
            this.UserControlCodeEditor = new MaszynaPi.MachineUI.UserControlCodeEditor();
            this.CodeEditorContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CompileItemToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.saveContexMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.wytnijToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kopiujToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wklejToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unixCodeEditorMenuStrip = new System.Windows.Forms.MenuStrip();
            this.kodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kompilujToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveUnixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageInstructionList = new System.Windows.Forms.TabPage();
            this.panelInstructionsMicrocode = new System.Windows.Forms.Panel();
            this.userControlInstructionMicrocode1 = new MaszynaPi.MachineUI.UserControlInstructionMicrocode();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.panelInstructionsList = new System.Windows.Forms.Panel();
            this.userControlInstructionList1 = new MaszynaPi.MachineUI.UserControlInstructionList();
            this.MainToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.BottomPanel.SuspendLayout();
            this.tabControlOnBottomPanel.SuspendLayout();
            this.tabPageInput.SuspendLayout();
            this.tabPageOutput.SuspendLayout();
            this.TopLeftPanel.SuspendLayout();
            this.CPUMainPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.TopRightPanel.SuspendLayout();
            this.ProgramPanel.SuspendLayout();
            this.tabControlEditors.SuspendLayout();
            this.tabPageCodeEditor.SuspendLayout();
            this.CodeEditorContextMenu.SuspendLayout();
            this.unixCodeEditorMenuStrip.SuspendLayout();
            this.tabPageInstructionList.SuspendLayout();
            this.panelInstructionsMicrocode.SuspendLayout();
            this.panelInstructionsList.SuspendLayout();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.BottomPanel.Controls.Add(this.tabControlOnBottomPanel);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 563);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(1443, 139);
            this.BottomPanel.TabIndex = 0;
            // 
            // tabControlOnBottomPanel
            // 
            this.tabControlOnBottomPanel.Controls.Add(this.tabPageInput);
            this.tabControlOnBottomPanel.Controls.Add(this.tabPageOutput);
            this.tabControlOnBottomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlOnBottomPanel.Location = new System.Drawing.Point(0, 0);
            this.tabControlOnBottomPanel.Name = "tabControlOnBottomPanel";
            this.tabControlOnBottomPanel.SelectedIndex = 0;
            this.tabControlOnBottomPanel.Size = new System.Drawing.Size(1439, 135);
            this.tabControlOnBottomPanel.TabIndex = 0;
            this.tabControlOnBottomPanel.TabStop = false;
            // 
            // tabPageInput
            // 
            this.tabPageInput.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageInput.Controls.Add(this.UserControlCharacterInput);
            this.tabPageInput.Location = new System.Drawing.Point(4, 22);
            this.tabPageInput.Name = "tabPageInput";
            this.tabPageInput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInput.Size = new System.Drawing.Size(1431, 109);
            this.tabPageInput.TabIndex = 0;
            this.tabPageInput.Text = "Input console";
            // 
            // UserControlCharacterInput
            // 
            this.UserControlCharacterInput.Location = new System.Drawing.Point(6, 27);
            this.UserControlCharacterInput.Name = "UserControlCharacterInput";
            this.UserControlCharacterInput.Size = new System.Drawing.Size(1416, 20);
            this.UserControlCharacterInput.TabIndex = 0;
            this.UserControlCharacterInput.TabStop = false;
            // 
            // tabPageOutput
            // 
            this.tabPageOutput.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageOutput.Controls.Add(this.UserControlCharacterOutput);
            this.tabPageOutput.Location = new System.Drawing.Point(4, 22);
            this.tabPageOutput.Name = "tabPageOutput";
            this.tabPageOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOutput.Size = new System.Drawing.Size(1431, 109);
            this.tabPageOutput.TabIndex = 1;
            this.tabPageOutput.Text = "Output console";
            // 
            // UserControlCharacterOutput
            // 
            this.UserControlCharacterOutput.BackColor = System.Drawing.Color.White;
            this.UserControlCharacterOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserControlCharacterOutput.Location = new System.Drawing.Point(3, 3);
            this.UserControlCharacterOutput.Multiline = true;
            this.UserControlCharacterOutput.Name = "UserControlCharacterOutput";
            this.UserControlCharacterOutput.ReadOnly = true;
            this.UserControlCharacterOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.UserControlCharacterOutput.Size = new System.Drawing.Size(1425, 103);
            this.UserControlCharacterOutput.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 560);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1443, 3);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // TopLeftPanel
            // 
            this.TopLeftPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TopLeftPanel.Controls.Add(this.CPUMainPanel);
            this.TopLeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.TopLeftPanel.Location = new System.Drawing.Point(0, 0);
            this.TopLeftPanel.Name = "TopLeftPanel";
            this.TopLeftPanel.Size = new System.Drawing.Size(826, 560);
            this.TopLeftPanel.TabIndex = 2;
            // 
            // CPUMainPanel
            // 
            this.CPUMainPanel.Controls.Add(this.UserControlRegisterF);
            this.CPUMainPanel.Controls.Add(this.breakPanel);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_stop);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_oitd);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_oa);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_t);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_osp);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_isp);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_dcsp);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_icsp);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_icit);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_iit);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_oit);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_iins);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_start);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_ord);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_ibuf);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_obuf);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_oy);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_iy);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_ox);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_ix);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_oiv);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_oim);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_im);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_eni);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_rint);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_shr);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_div);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_mul);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_wracc);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_sub);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_add);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_icacc);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_and);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire2);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_or);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_dcacc);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_not);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_oacc);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_ialu);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_wr);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_rd);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_ia);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire1_od);
            this.CPUMainPanel.Controls.Add(this.userControlSignalWire_id);
            this.CPUMainPanel.Controls.Add(this.userControlIntButton4);
            this.CPUMainPanel.Controls.Add(this.userControlIntButton3);
            this.CPUMainPanel.Controls.Add(this.userControlIntButton2);
            this.CPUMainPanel.Controls.Add(this.userControlIntButton1);
            this.CPUMainPanel.Controls.Add(this.userControlBusAS);
            this.CPUMainPanel.Controls.Add(this.UserControlRegisterRM);
            this.CPUMainPanel.Controls.Add(this.UserControlRegisterAP);
            this.CPUMainPanel.Controls.Add(this.UserControlRegisterRP);
            this.CPUMainPanel.Controls.Add(this.UserControlRegisterRZ);
            this.CPUMainPanel.Controls.Add(this.UserControlRegisterWS);
            this.CPUMainPanel.Controls.Add(this.UserControlRegisterX);
            this.CPUMainPanel.Controls.Add(this.UserControlRegisterY);
            this.CPUMainPanel.Controls.Add(this.UserControlRegisterRB);
            this.CPUMainPanel.Controls.Add(this.UserControlRegisterG);
            this.CPUMainPanel.Controls.Add(this.panelALUView);
            this.CPUMainPanel.Controls.Add(this.UserControlRegisterAK);
            this.CPUMainPanel.Controls.Add(this.UserControlRegisterL);
            this.CPUMainPanel.Controls.Add(this.UserControlRegisterI);
            this.CPUMainPanel.Controls.Add(this.userControlBusAddress);
            this.CPUMainPanel.Controls.Add(this.userControlBusData);
            this.CPUMainPanel.Controls.Add(this.MemoryControl);
            this.CPUMainPanel.Controls.Add(this.checkBoxManualDebug);
            this.CPUMainPanel.Controls.Add(this.UserControlRegisterS);
            this.CPUMainPanel.Controls.Add(this.UserControlRegisterA);
            this.CPUMainPanel.Controls.Add(this.menuStrip1);
            this.CPUMainPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.CPUMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CPUMainPanel.Location = new System.Drawing.Point(0, 0);
            this.CPUMainPanel.Name = "CPUMainPanel";
            this.CPUMainPanel.Size = new System.Drawing.Size(822, 556);
            this.CPUMainPanel.TabIndex = 0;
            // 
            // UserControlRegisterF
            // 
            this.UserControlRegisterF.BackColor = System.Drawing.Color.White;
            this.UserControlRegisterF.Location = new System.Drawing.Point(341, 260);
            this.UserControlRegisterF.Name = "UserControlRegisterF";
            this.UserControlRegisterF.ReadOnly = true;
            this.UserControlRegisterF.RegisterName = "F";
            this.UserControlRegisterF.Size = new System.Drawing.Size(125, 20);
            this.UserControlRegisterF.TabIndex = 77;
            this.UserControlRegisterF.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // breakPanel
            // 
            this.breakPanel.Location = new System.Drawing.Point(508, 57);
            this.breakPanel.Name = "breakPanel";
            this.breakPanel.Size = new System.Drawing.Size(185, 40);
            this.breakPanel.TabIndex = 76;
            // 
            // userControlSignalWire_stop
            // 
            this.userControlSignalWire_stop.Active = false;
            this.userControlSignalWire_stop.Cap = System.Drawing.Drawing2D.LineCap.SquareAnchor;
            this.userControlSignalWire_stop.Location = new System.Drawing.Point(92, 281);
            this.userControlSignalWire_stop.Name = "userControlSignalWire_stop";
            this.userControlSignalWire_stop.Rotation = 0;
            this.userControlSignalWire_stop.SignalName = "stop";
            this.userControlSignalWire_stop.Size = new System.Drawing.Size(68, 32);
            this.userControlSignalWire_stop.TabIndex = 74;
            // 
            // userControlSignalWire_oitd
            // 
            this.userControlSignalWire_oitd.Active = false;
            this.userControlSignalWire_oitd.Cap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.userControlSignalWire_oitd.Location = new System.Drawing.Point(9, 198);
            this.userControlSignalWire_oitd.Name = "userControlSignalWire_oitd";
            this.userControlSignalWire_oitd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.userControlSignalWire_oitd.Rotation = 90;
            this.userControlSignalWire_oitd.SignalName = "oitd";
            this.userControlSignalWire_oitd.Size = new System.Drawing.Size(54, 282);
            this.userControlSignalWire_oitd.TabIndex = 73;
            // 
            // userControlSignalWire_oa
            // 
            this.userControlSignalWire_oa.Active = false;
            this.userControlSignalWire_oa.Cap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.userControlSignalWire_oa.Location = new System.Drawing.Point(188, 142);
            this.userControlSignalWire_oa.Name = "userControlSignalWire_oa";
            this.userControlSignalWire_oa.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.userControlSignalWire_oa.Rotation = 270;
            this.userControlSignalWire_oa.SignalName = "oa";
            this.userControlSignalWire_oa.Size = new System.Drawing.Size(41, 282);
            this.userControlSignalWire_oa.TabIndex = 72;
            // 
            // userControlSignalWire_t
            // 
            this.userControlSignalWire_t.Active = false;
            this.userControlSignalWire_t.Cap = System.Drawing.Drawing2D.LineCap.Flat;
            this.userControlSignalWire_t.Location = new System.Drawing.Point(244, 211);
            this.userControlSignalWire_t.Name = "userControlSignalWire_t";
            this.userControlSignalWire_t.Rotation = 180;
            this.userControlSignalWire_t.SignalName = "tbs";
            this.userControlSignalWire_t.Size = new System.Drawing.Size(44, 30);
            this.userControlSignalWire_t.TabIndex = 70;
            // 
            // userControlSignalWire_osp
            // 
            this.userControlSignalWire_osp.Active = false;
            this.userControlSignalWire_osp.Cap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.userControlSignalWire_osp.Location = new System.Drawing.Point(341, 137);
            this.userControlSignalWire_osp.Name = "userControlSignalWire_osp";
            this.userControlSignalWire_osp.Rotation = 270;
            this.userControlSignalWire_osp.SignalName = "osp";
            this.userControlSignalWire_osp.Size = new System.Drawing.Size(65, 32);
            this.userControlSignalWire_osp.TabIndex = 69;
            // 
            // userControlSignalWire_isp
            // 
            this.userControlSignalWire_isp.Active = false;
            this.userControlSignalWire_isp.Cap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.userControlSignalWire_isp.Location = new System.Drawing.Point(436, 137);
            this.userControlSignalWire_isp.Name = "userControlSignalWire_isp";
            this.userControlSignalWire_isp.Rotation = 90;
            this.userControlSignalWire_isp.SignalName = "isp";
            this.userControlSignalWire_isp.Size = new System.Drawing.Size(65, 32);
            this.userControlSignalWire_isp.TabIndex = 68;
            // 
            // userControlSignalWire_dcsp
            // 
            this.userControlSignalWire_dcsp.Active = false;
            this.userControlSignalWire_dcsp.Cap = System.Drawing.Drawing2D.LineCap.SquareAnchor;
            this.userControlSignalWire_dcsp.Location = new System.Drawing.Point(497, 167);
            this.userControlSignalWire_dcsp.Name = "userControlSignalWire_dcsp";
            this.userControlSignalWire_dcsp.Rotation = 180;
            this.userControlSignalWire_dcsp.SignalName = "dcsp";
            this.userControlSignalWire_dcsp.Size = new System.Drawing.Size(68, 32);
            this.userControlSignalWire_dcsp.TabIndex = 67;
            // 
            // userControlSignalWire_icsp
            // 
            this.userControlSignalWire_icsp.Active = false;
            this.userControlSignalWire_icsp.Cap = System.Drawing.Drawing2D.LineCap.SquareAnchor;
            this.userControlSignalWire_icsp.Location = new System.Drawing.Point(272, 167);
            this.userControlSignalWire_icsp.Name = "userControlSignalWire_icsp";
            this.userControlSignalWire_icsp.Rotation = 0;
            this.userControlSignalWire_icsp.SignalName = "icsp";
            this.userControlSignalWire_icsp.Size = new System.Drawing.Size(68, 32);
            this.userControlSignalWire_icsp.TabIndex = 66;
            // 
            // userControlSignalWire_icit
            // 
            this.userControlSignalWire_icit.Active = false;
            this.userControlSignalWire_icit.Cap = System.Drawing.Drawing2D.LineCap.SquareAnchor;
            this.userControlSignalWire_icit.Location = new System.Drawing.Point(78, 199);
            this.userControlSignalWire_icit.Name = "userControlSignalWire_icit";
            this.userControlSignalWire_icit.Rotation = 270;
            this.userControlSignalWire_icit.SignalName = "icit";
            this.userControlSignalWire_icit.Size = new System.Drawing.Size(65, 32);
            this.userControlSignalWire_icit.TabIndex = 65;
            // 
            // userControlSignalWire_iit
            // 
            this.userControlSignalWire_iit.Active = false;
            this.userControlSignalWire_iit.Cap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.userControlSignalWire_iit.Location = new System.Drawing.Point(26, 137);
            this.userControlSignalWire_iit.Name = "userControlSignalWire_iit";
            this.userControlSignalWire_iit.Rotation = 90;
            this.userControlSignalWire_iit.SignalName = "iit";
            this.userControlSignalWire_iit.Size = new System.Drawing.Size(65, 32);
            this.userControlSignalWire_iit.TabIndex = 64;
            // 
            // userControlSignalWire_oit
            // 
            this.userControlSignalWire_oit.Active = false;
            this.userControlSignalWire_oit.Cap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.userControlSignalWire_oit.Location = new System.Drawing.Point(121, 137);
            this.userControlSignalWire_oit.Name = "userControlSignalWire_oit";
            this.userControlSignalWire_oit.Rotation = 270;
            this.userControlSignalWire_oit.SignalName = "oit";
            this.userControlSignalWire_oit.Size = new System.Drawing.Size(65, 32);
            this.userControlSignalWire_oit.TabIndex = 63;
            // 
            // userControlSignalWire_iins
            // 
            this.userControlSignalWire_iins.Active = false;
            this.userControlSignalWire_iins.Cap = System.Drawing.Drawing2D.LineCap.Flat;
            this.userControlSignalWire_iins.Location = new System.Drawing.Point(139, 452);
            this.userControlSignalWire_iins.Name = "userControlSignalWire_iins";
            this.userControlSignalWire_iins.Rotation = 270;
            this.userControlSignalWire_iins.SignalName = "iins";
            this.userControlSignalWire_iins.Size = new System.Drawing.Size(65, 32);
            this.userControlSignalWire_iins.TabIndex = 62;
            // 
            // userControlSignalWire_start
            // 
            this.userControlSignalWire_start.Active = false;
            this.userControlSignalWire_start.Cap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.userControlSignalWire_start.Location = new System.Drawing.Point(678, 492);
            this.userControlSignalWire_start.Name = "userControlSignalWire_start";
            this.userControlSignalWire_start.Rotation = 90;
            this.userControlSignalWire_start.SignalName = "start";
            this.userControlSignalWire_start.Size = new System.Drawing.Size(68, 34);
            this.userControlSignalWire_start.TabIndex = 61;
            // 
            // userControlSignalWire_ord
            // 
            this.userControlSignalWire_ord.Active = false;
            this.userControlSignalWire_ord.Cap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.userControlSignalWire_ord.Location = new System.Drawing.Point(724, 492);
            this.userControlSignalWire_ord.Name = "userControlSignalWire_ord";
            this.userControlSignalWire_ord.Rotation = 270;
            this.userControlSignalWire_ord.SignalName = "ord";
            this.userControlSignalWire_ord.Size = new System.Drawing.Size(68, 34);
            this.userControlSignalWire_ord.TabIndex = 60;
            // 
            // userControlSignalWire_ibuf
            // 
            this.userControlSignalWire_ibuf.Active = false;
            this.userControlSignalWire_ibuf.Cap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.userControlSignalWire_ibuf.Location = new System.Drawing.Point(522, 492);
            this.userControlSignalWire_ibuf.Name = "userControlSignalWire_ibuf";
            this.userControlSignalWire_ibuf.Rotation = 90;
            this.userControlSignalWire_ibuf.SignalName = "ibuf";
            this.userControlSignalWire_ibuf.Size = new System.Drawing.Size(68, 34);
            this.userControlSignalWire_ibuf.TabIndex = 59;
            // 
            // userControlSignalWire_obuf
            // 
            this.userControlSignalWire_obuf.Active = false;
            this.userControlSignalWire_obuf.Cap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.userControlSignalWire_obuf.Location = new System.Drawing.Point(586, 492);
            this.userControlSignalWire_obuf.Name = "userControlSignalWire_obuf";
            this.userControlSignalWire_obuf.Rotation = 270;
            this.userControlSignalWire_obuf.SignalName = "obuf";
            this.userControlSignalWire_obuf.Size = new System.Drawing.Size(68, 34);
            this.userControlSignalWire_obuf.TabIndex = 58;
            // 
            // userControlSignalWire_oy
            // 
            this.userControlSignalWire_oy.Active = false;
            this.userControlSignalWire_oy.Cap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.userControlSignalWire_oy.Location = new System.Drawing.Point(271, 497);
            this.userControlSignalWire_oy.Name = "userControlSignalWire_oy";
            this.userControlSignalWire_oy.Rotation = 270;
            this.userControlSignalWire_oy.SignalName = "oy";
            this.userControlSignalWire_oy.Size = new System.Drawing.Size(54, 27);
            this.userControlSignalWire_oy.TabIndex = 57;
            // 
            // userControlSignalWire_iy
            // 
            this.userControlSignalWire_iy.Active = false;
            this.userControlSignalWire_iy.Cap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.userControlSignalWire_iy.Location = new System.Drawing.Point(324, 497);
            this.userControlSignalWire_iy.Name = "userControlSignalWire_iy";
            this.userControlSignalWire_iy.Rotation = 90;
            this.userControlSignalWire_iy.SignalName = "iy";
            this.userControlSignalWire_iy.Size = new System.Drawing.Size(54, 27);
            this.userControlSignalWire_iy.TabIndex = 56;
            // 
            // userControlSignalWire_ox
            // 
            this.userControlSignalWire_ox.Active = false;
            this.userControlSignalWire_ox.Cap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.userControlSignalWire_ox.Location = new System.Drawing.Point(63, 497);
            this.userControlSignalWire_ox.Name = "userControlSignalWire_ox";
            this.userControlSignalWire_ox.Rotation = 270;
            this.userControlSignalWire_ox.SignalName = "ox";
            this.userControlSignalWire_ox.Size = new System.Drawing.Size(54, 27);
            this.userControlSignalWire_ox.TabIndex = 55;
            // 
            // userControlSignalWire_ix
            // 
            this.userControlSignalWire_ix.Active = false;
            this.userControlSignalWire_ix.Cap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.userControlSignalWire_ix.Location = new System.Drawing.Point(116, 497);
            this.userControlSignalWire_ix.Name = "userControlSignalWire_ix";
            this.userControlSignalWire_ix.Rotation = 90;
            this.userControlSignalWire_ix.SignalName = "ix";
            this.userControlSignalWire_ix.Size = new System.Drawing.Size(54, 27);
            this.userControlSignalWire_ix.TabIndex = 54;
            // 
            // userControlSignalWire_oiv
            // 
            this.userControlSignalWire_oiv.Active = false;
            this.userControlSignalWire_oiv.Cap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.userControlSignalWire_oiv.Location = new System.Drawing.Point(271, 100);
            this.userControlSignalWire_oiv.Name = "userControlSignalWire_oiv";
            this.userControlSignalWire_oiv.Rotation = 90;
            this.userControlSignalWire_oiv.SignalName = "oiv";
            this.userControlSignalWire_oiv.Size = new System.Drawing.Size(54, 27);
            this.userControlSignalWire_oiv.TabIndex = 53;
            // 
            // userControlSignalWire_oim
            // 
            this.userControlSignalWire_oim.Active = false;
            this.userControlSignalWire_oim.Cap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.userControlSignalWire_oim.Location = new System.Drawing.Point(48, 100);
            this.userControlSignalWire_oim.Name = "userControlSignalWire_oim";
            this.userControlSignalWire_oim.Rotation = 90;
            this.userControlSignalWire_oim.SignalName = "oim";
            this.userControlSignalWire_oim.Size = new System.Drawing.Size(54, 27);
            this.userControlSignalWire_oim.TabIndex = 52;
            // 
            // userControlSignalWire_im
            // 
            this.userControlSignalWire_im.Active = false;
            this.userControlSignalWire_im.Cap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.userControlSignalWire_im.Location = new System.Drawing.Point(116, 100);
            this.userControlSignalWire_im.Name = "userControlSignalWire_im";
            this.userControlSignalWire_im.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.userControlSignalWire_im.Rotation = 270;
            this.userControlSignalWire_im.SignalName = "iim";
            this.userControlSignalWire_im.Size = new System.Drawing.Size(54, 27);
            this.userControlSignalWire_im.TabIndex = 51;
            // 
            // userControlSignalWire_eni
            // 
            this.userControlSignalWire_eni.Active = false;
            this.userControlSignalWire_eni.Cap = System.Drawing.Drawing2D.LineCap.Flat;
            this.userControlSignalWire_eni.Location = new System.Drawing.Point(393, 78);
            this.userControlSignalWire_eni.Name = "userControlSignalWire_eni";
            this.userControlSignalWire_eni.Rotation = 180;
            this.userControlSignalWire_eni.SignalName = "eni";
            this.userControlSignalWire_eni.Size = new System.Drawing.Size(65, 32);
            this.userControlSignalWire_eni.TabIndex = 50;
            // 
            // userControlSignalWire_rint
            // 
            this.userControlSignalWire_rint.Active = false;
            this.userControlSignalWire_rint.Cap = System.Drawing.Drawing2D.LineCap.Flat;
            this.userControlSignalWire_rint.Location = new System.Drawing.Point(393, 46);
            this.userControlSignalWire_rint.Name = "userControlSignalWire_rint";
            this.userControlSignalWire_rint.Rotation = 180;
            this.userControlSignalWire_rint.SignalName = "rint";
            this.userControlSignalWire_rint.Size = new System.Drawing.Size(65, 32);
            this.userControlSignalWire_rint.TabIndex = 49;
            // 
            // userControlSignalWire_shr
            // 
            this.userControlSignalWire_shr.Active = false;
            this.userControlSignalWire_shr.Cap = System.Drawing.Drawing2D.LineCap.SquareAnchor;
            this.userControlSignalWire_shr.Location = new System.Drawing.Point(503, 418);
            this.userControlSignalWire_shr.Name = "userControlSignalWire_shr";
            this.userControlSignalWire_shr.Rotation = 180;
            this.userControlSignalWire_shr.SignalName = "shr";
            this.userControlSignalWire_shr.Size = new System.Drawing.Size(68, 32);
            this.userControlSignalWire_shr.TabIndex = 48;
            // 
            // userControlSignalWire_div
            // 
            this.userControlSignalWire_div.Active = false;
            this.userControlSignalWire_div.Cap = System.Drawing.Drawing2D.LineCap.SquareAnchor;
            this.userControlSignalWire_div.Location = new System.Drawing.Point(503, 393);
            this.userControlSignalWire_div.Name = "userControlSignalWire_div";
            this.userControlSignalWire_div.Rotation = 180;
            this.userControlSignalWire_div.SignalName = "div";
            this.userControlSignalWire_div.Size = new System.Drawing.Size(68, 32);
            this.userControlSignalWire_div.TabIndex = 47;
            // 
            // userControlSignalWire_mul
            // 
            this.userControlSignalWire_mul.Active = false;
            this.userControlSignalWire_mul.Cap = System.Drawing.Drawing2D.LineCap.SquareAnchor;
            this.userControlSignalWire_mul.Location = new System.Drawing.Point(263, 418);
            this.userControlSignalWire_mul.Name = "userControlSignalWire_mul";
            this.userControlSignalWire_mul.Rotation = 0;
            this.userControlSignalWire_mul.SignalName = "mul";
            this.userControlSignalWire_mul.Size = new System.Drawing.Size(75, 32);
            this.userControlSignalWire_mul.TabIndex = 46;
            // 
            // userControlSignalWire_wracc
            // 
            this.userControlSignalWire_wracc.Active = false;
            this.userControlSignalWire_wracc.Cap = System.Drawing.Drawing2D.LineCap.SquareAnchor;
            this.userControlSignalWire_wracc.Location = new System.Drawing.Point(263, 393);
            this.userControlSignalWire_wracc.Name = "userControlSignalWire_wracc";
            this.userControlSignalWire_wracc.Rotation = 0;
            this.userControlSignalWire_wracc.SignalName = "wracc";
            this.userControlSignalWire_wracc.Size = new System.Drawing.Size(75, 32);
            this.userControlSignalWire_wracc.TabIndex = 45;
            // 
            // userControlSignalWire_sub
            // 
            this.userControlSignalWire_sub.Active = false;
            this.userControlSignalWire_sub.Cap = System.Drawing.Drawing2D.LineCap.SquareAnchor;
            this.userControlSignalWire_sub.Location = new System.Drawing.Point(263, 369);
            this.userControlSignalWire_sub.Name = "userControlSignalWire_sub";
            this.userControlSignalWire_sub.Rotation = 0;
            this.userControlSignalWire_sub.SignalName = "sub";
            this.userControlSignalWire_sub.Size = new System.Drawing.Size(75, 32);
            this.userControlSignalWire_sub.TabIndex = 44;
            // 
            // userControlSignalWire_add
            // 
            this.userControlSignalWire_add.Active = false;
            this.userControlSignalWire_add.Cap = System.Drawing.Drawing2D.LineCap.SquareAnchor;
            this.userControlSignalWire_add.Location = new System.Drawing.Point(263, 341);
            this.userControlSignalWire_add.Name = "userControlSignalWire_add";
            this.userControlSignalWire_add.Rotation = 0;
            this.userControlSignalWire_add.SignalName = "add";
            this.userControlSignalWire_add.Size = new System.Drawing.Size(75, 32);
            this.userControlSignalWire_add.TabIndex = 43;
            // 
            // userControlSignalWire_icacc
            // 
            this.userControlSignalWire_icacc.Active = false;
            this.userControlSignalWire_icacc.Cap = System.Drawing.Drawing2D.LineCap.SquareAnchor;
            this.userControlSignalWire_icacc.Location = new System.Drawing.Point(265, 277);
            this.userControlSignalWire_icacc.Name = "userControlSignalWire_icacc";
            this.userControlSignalWire_icacc.Rotation = 0;
            this.userControlSignalWire_icacc.SignalName = "icacc";
            this.userControlSignalWire_icacc.Size = new System.Drawing.Size(75, 32);
            this.userControlSignalWire_icacc.TabIndex = 42;
            // 
            // userControlSignalWire_and
            // 
            this.userControlSignalWire_and.Active = false;
            this.userControlSignalWire_and.Cap = System.Drawing.Drawing2D.LineCap.SquareAnchor;
            this.userControlSignalWire_and.Location = new System.Drawing.Point(503, 369);
            this.userControlSignalWire_and.Name = "userControlSignalWire_and";
            this.userControlSignalWire_and.Rotation = 180;
            this.userControlSignalWire_and.SignalName = "and";
            this.userControlSignalWire_and.Size = new System.Drawing.Size(68, 32);
            this.userControlSignalWire_and.TabIndex = 41;
            // 
            // userControlSignalWire2
            // 
            this.userControlSignalWire2.Active = false;
            this.userControlSignalWire2.Cap = System.Drawing.Drawing2D.LineCap.Flat;
            this.userControlSignalWire2.Location = new System.Drawing.Point(263, 315);
            this.userControlSignalWire2.Name = "userControlSignalWire2";
            this.userControlSignalWire2.Rotation = 0;
            this.userControlSignalWire2.SignalName = "iacc";
            this.userControlSignalWire2.Size = new System.Drawing.Size(75, 32);
            this.userControlSignalWire2.TabIndex = 40;
            // 
            // userControlSignalWire_or
            // 
            this.userControlSignalWire_or.Active = false;
            this.userControlSignalWire_or.Cap = System.Drawing.Drawing2D.LineCap.SquareAnchor;
            this.userControlSignalWire_or.Location = new System.Drawing.Point(503, 341);
            this.userControlSignalWire_or.Name = "userControlSignalWire_or";
            this.userControlSignalWire_or.Rotation = 180;
            this.userControlSignalWire_or.SignalName = "or";
            this.userControlSignalWire_or.Size = new System.Drawing.Size(68, 32);
            this.userControlSignalWire_or.TabIndex = 39;
            // 
            // userControlSignalWire_dcacc
            // 
            this.userControlSignalWire_dcacc.Active = false;
            this.userControlSignalWire_dcacc.Cap = System.Drawing.Drawing2D.LineCap.SquareAnchor;
            this.userControlSignalWire_dcacc.Location = new System.Drawing.Point(497, 277);
            this.userControlSignalWire_dcacc.Name = "userControlSignalWire_dcacc";
            this.userControlSignalWire_dcacc.Rotation = 180;
            this.userControlSignalWire_dcacc.SignalName = "dcacc";
            this.userControlSignalWire_dcacc.Size = new System.Drawing.Size(79, 32);
            this.userControlSignalWire_dcacc.TabIndex = 38;
            // 
            // userControlSignalWire_not
            // 
            this.userControlSignalWire_not.Active = false;
            this.userControlSignalWire_not.Cap = System.Drawing.Drawing2D.LineCap.SquareAnchor;
            this.userControlSignalWire_not.Location = new System.Drawing.Point(503, 315);
            this.userControlSignalWire_not.Name = "userControlSignalWire_not";
            this.userControlSignalWire_not.Rotation = 180;
            this.userControlSignalWire_not.SignalName = "not";
            this.userControlSignalWire_not.Size = new System.Drawing.Size(68, 32);
            this.userControlSignalWire_not.TabIndex = 37;
            // 
            // userControlSignalWire_oacc
            // 
            this.userControlSignalWire_oacc.Active = false;
            this.userControlSignalWire_oacc.Cap = System.Drawing.Drawing2D.LineCap.Flat;
            this.userControlSignalWire_oacc.Location = new System.Drawing.Point(341, 453);
            this.userControlSignalWire_oacc.Name = "userControlSignalWire_oacc";
            this.userControlSignalWire_oacc.Rotation = 90;
            this.userControlSignalWire_oacc.SignalName = "oacc";
            this.userControlSignalWire_oacc.Size = new System.Drawing.Size(65, 32);
            this.userControlSignalWire_oacc.TabIndex = 36;
            // 
            // userControlSignalWire_ialu
            // 
            this.userControlSignalWire_ialu.Active = false;
            this.userControlSignalWire_ialu.Cap = System.Drawing.Drawing2D.LineCap.Flat;
            this.userControlSignalWire_ialu.Location = new System.Drawing.Point(436, 453);
            this.userControlSignalWire_ialu.Name = "userControlSignalWire_ialu";
            this.userControlSignalWire_ialu.Rotation = 270;
            this.userControlSignalWire_ialu.SignalName = "ialu";
            this.userControlSignalWire_ialu.Size = new System.Drawing.Size(65, 32);
            this.userControlSignalWire_ialu.TabIndex = 35;
            // 
            // userControlSignalWire_wr
            // 
            this.userControlSignalWire_wr.Active = false;
            this.userControlSignalWire_wr.Cap = System.Drawing.Drawing2D.LineCap.Flat;
            this.userControlSignalWire_wr.Location = new System.Drawing.Point(768, 307);
            this.userControlSignalWire_wr.Name = "userControlSignalWire_wr";
            this.userControlSignalWire_wr.Rotation = 180;
            this.userControlSignalWire_wr.SignalName = "wr";
            this.userControlSignalWire_wr.Size = new System.Drawing.Size(65, 32);
            this.userControlSignalWire_wr.TabIndex = 34;
            // 
            // userControlSignalWire_rd
            // 
            this.userControlSignalWire_rd.Active = false;
            this.userControlSignalWire_rd.Cap = System.Drawing.Drawing2D.LineCap.Flat;
            this.userControlSignalWire_rd.Location = new System.Drawing.Point(768, 269);
            this.userControlSignalWire_rd.Name = "userControlSignalWire_rd";
            this.userControlSignalWire_rd.Rotation = 180;
            this.userControlSignalWire_rd.SignalName = "rd";
            this.userControlSignalWire_rd.Size = new System.Drawing.Size(65, 32);
            this.userControlSignalWire_rd.TabIndex = 33;
            // 
            // userControlSignalWire_ia
            // 
            this.userControlSignalWire_ia.Active = false;
            this.userControlSignalWire_ia.Cap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.userControlSignalWire_ia.Location = new System.Drawing.Point(645, 137);
            this.userControlSignalWire_ia.Name = "userControlSignalWire_ia";
            this.userControlSignalWire_ia.Rotation = 90;
            this.userControlSignalWire_ia.SignalName = "ia";
            this.userControlSignalWire_ia.Size = new System.Drawing.Size(65, 32);
            this.userControlSignalWire_ia.TabIndex = 32;
            // 
            // userControlSignalWire1_od
            // 
            this.userControlSignalWire1_od.Active = false;
            this.userControlSignalWire1_od.Cap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.userControlSignalWire1_od.Location = new System.Drawing.Point(598, 452);
            this.userControlSignalWire1_od.Name = "userControlSignalWire1_od";
            this.userControlSignalWire1_od.Rotation = 90;
            this.userControlSignalWire1_od.SignalName = "od";
            this.userControlSignalWire1_od.Size = new System.Drawing.Size(65, 32);
            this.userControlSignalWire1_od.TabIndex = 30;
            // 
            // userControlSignalWire_id
            // 
            this.userControlSignalWire_id.Active = false;
            this.userControlSignalWire_id.Cap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.userControlSignalWire_id.Location = new System.Drawing.Point(693, 452);
            this.userControlSignalWire_id.Name = "userControlSignalWire_id";
            this.userControlSignalWire_id.Rotation = 270;
            this.userControlSignalWire_id.SignalName = "id";
            this.userControlSignalWire_id.Size = new System.Drawing.Size(65, 32);
            this.userControlSignalWire_id.TabIndex = 29;
            // 
            // userControlIntButton4
            // 
            this.userControlIntButton4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userControlIntButton4.InterruptPriority = ((uint)(1u));
            this.userControlIntButton4.Location = new System.Drawing.Point(146, 36);
            this.userControlIntButton4.Name = "userControlIntButton4";
            this.userControlIntButton4.Size = new System.Drawing.Size(36, 16);
            this.userControlIntButton4.TabIndex = 28;
            this.userControlIntButton4.Text = "4";
            this.userControlIntButton4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.userControlIntButton4.UseVisualStyleBackColor = true;
            // 
            // userControlIntButton3
            // 
            this.userControlIntButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userControlIntButton3.InterruptPriority = ((uint)(2u));
            this.userControlIntButton3.Location = new System.Drawing.Point(107, 36);
            this.userControlIntButton3.Name = "userControlIntButton3";
            this.userControlIntButton3.Size = new System.Drawing.Size(36, 16);
            this.userControlIntButton3.TabIndex = 27;
            this.userControlIntButton3.Text = "3";
            this.userControlIntButton3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.userControlIntButton3.UseVisualStyleBackColor = true;
            // 
            // userControlIntButton2
            // 
            this.userControlIntButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userControlIntButton2.InterruptPriority = ((uint)(4u));
            this.userControlIntButton2.Location = new System.Drawing.Point(66, 36);
            this.userControlIntButton2.Name = "userControlIntButton2";
            this.userControlIntButton2.Size = new System.Drawing.Size(36, 16);
            this.userControlIntButton2.TabIndex = 26;
            this.userControlIntButton2.Text = "2";
            this.userControlIntButton2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.userControlIntButton2.UseVisualStyleBackColor = true;
            // 
            // userControlIntButton1
            // 
            this.userControlIntButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userControlIntButton1.InterruptPriority = ((uint)(8u));
            this.userControlIntButton1.Location = new System.Drawing.Point(26, 36);
            this.userControlIntButton1.Name = "userControlIntButton1";
            this.userControlIntButton1.Size = new System.Drawing.Size(36, 16);
            this.userControlIntButton1.TabIndex = 25;
            this.userControlIntButton1.Text = "1";
            this.userControlIntButton1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.userControlIntButton1.UseVisualStyleBackColor = true;
            // 
            // userControlBusAS
            // 
            this.userControlBusAS.BackColor = System.Drawing.SystemColors.Control;
            this.userControlBusAS.Cursor = System.Windows.Forms.Cursors.Help;
            this.userControlBusAS.Location = new System.Drawing.Point(235, 137);
            this.userControlBusAS.Multiline = true;
            this.userControlBusAS.Name = "userControlBusAS";
            this.userControlBusAS.ReadOnly = true;
            this.userControlBusAS.Size = new System.Drawing.Size(5, 348);
            this.userControlBusAS.TabIndex = 24;
            // 
            // UserControlRegisterRM
            // 
            this.UserControlRegisterRM.BackColor = System.Drawing.Color.White;
            this.UserControlRegisterRM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UserControlRegisterRM.Location = new System.Drawing.Point(26, 79);
            this.UserControlRegisterRM.Name = "UserControlRegisterRM";
            this.UserControlRegisterRM.ReadOnly = true;
            this.UserControlRegisterRM.RegisterName = "IM";
            this.UserControlRegisterRM.Size = new System.Drawing.Size(156, 20);
            this.UserControlRegisterRM.TabIndex = 23;
            this.UserControlRegisterRM.TabStop = false;
            this.UserControlRegisterRM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UserControlRegisterAP
            // 
            this.UserControlRegisterAP.BackColor = System.Drawing.Color.White;
            this.UserControlRegisterAP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UserControlRegisterAP.Location = new System.Drawing.Point(217, 78);
            this.UserControlRegisterAP.Name = "UserControlRegisterAP";
            this.UserControlRegisterAP.ReadOnly = true;
            this.UserControlRegisterAP.RegisterName = "IV";
            this.UserControlRegisterAP.Size = new System.Drawing.Size(156, 20);
            this.UserControlRegisterAP.TabIndex = 22;
            this.UserControlRegisterAP.TabStop = false;
            this.UserControlRegisterAP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UserControlRegisterRP
            // 
            this.UserControlRegisterRP.BackColor = System.Drawing.Color.White;
            this.UserControlRegisterRP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UserControlRegisterRP.Location = new System.Drawing.Point(217, 53);
            this.UserControlRegisterRP.Name = "UserControlRegisterRP";
            this.UserControlRegisterRP.ReadOnly = true;
            this.UserControlRegisterRP.RegisterName = "AI";
            this.UserControlRegisterRP.Size = new System.Drawing.Size(156, 20);
            this.UserControlRegisterRP.TabIndex = 21;
            this.UserControlRegisterRP.TabStop = false;
            this.UserControlRegisterRP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UserControlRegisterRZ
            // 
            this.UserControlRegisterRZ.BackColor = System.Drawing.Color.White;
            this.UserControlRegisterRZ.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UserControlRegisterRZ.Location = new System.Drawing.Point(26, 53);
            this.UserControlRegisterRZ.Name = "UserControlRegisterRZ";
            this.UserControlRegisterRZ.ReadOnly = true;
            this.UserControlRegisterRZ.RegisterName = "IR";
            this.UserControlRegisterRZ.Size = new System.Drawing.Size(156, 20);
            this.UserControlRegisterRZ.TabIndex = 20;
            this.UserControlRegisterRZ.TabStop = false;
            this.UserControlRegisterRZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UserControlRegisterWS
            // 
            this.UserControlRegisterWS.BackColor = System.Drawing.Color.White;
            this.UserControlRegisterWS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UserControlRegisterWS.Location = new System.Drawing.Point(341, 173);
            this.UserControlRegisterWS.Name = "UserControlRegisterWS";
            this.UserControlRegisterWS.ReadOnly = true;
            this.UserControlRegisterWS.RegisterName = "Stack";
            this.UserControlRegisterWS.Size = new System.Drawing.Size(156, 20);
            this.UserControlRegisterWS.TabIndex = 19;
            this.UserControlRegisterWS.TabStop = false;
            this.UserControlRegisterWS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UserControlRegisterX
            // 
            this.UserControlRegisterX.BackColor = System.Drawing.Color.White;
            this.UserControlRegisterX.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UserControlRegisterX.Location = new System.Drawing.Point(48, 528);
            this.UserControlRegisterX.Name = "UserControlRegisterX";
            this.UserControlRegisterX.ReadOnly = true;
            this.UserControlRegisterX.RegisterName = "X";
            this.UserControlRegisterX.Size = new System.Drawing.Size(156, 20);
            this.UserControlRegisterX.TabIndex = 18;
            this.UserControlRegisterX.TabStop = false;
            this.UserControlRegisterX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UserControlRegisterY
            // 
            this.UserControlRegisterY.BackColor = System.Drawing.Color.White;
            this.UserControlRegisterY.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UserControlRegisterY.Location = new System.Drawing.Point(249, 528);
            this.UserControlRegisterY.Name = "UserControlRegisterY";
            this.UserControlRegisterY.ReadOnly = true;
            this.UserControlRegisterY.RegisterName = "Y";
            this.UserControlRegisterY.Size = new System.Drawing.Size(156, 20);
            this.UserControlRegisterY.TabIndex = 17;
            this.UserControlRegisterY.TabStop = false;
            this.UserControlRegisterY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UserControlRegisterRB
            // 
            this.UserControlRegisterRB.BackColor = System.Drawing.Color.White;
            this.UserControlRegisterRB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UserControlRegisterRB.Location = new System.Drawing.Point(507, 528);
            this.UserControlRegisterRB.Name = "UserControlRegisterRB";
            this.UserControlRegisterRB.ReadOnly = true;
            this.UserControlRegisterRB.RegisterName = "IO Buffer";
            this.UserControlRegisterRB.Size = new System.Drawing.Size(156, 20);
            this.UserControlRegisterRB.TabIndex = 16;
            this.UserControlRegisterRB.TabStop = false;
            this.UserControlRegisterRB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UserControlRegisterG
            // 
            this.UserControlRegisterG.BackColor = System.Drawing.Color.White;
            this.UserControlRegisterG.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UserControlRegisterG.Location = new System.Drawing.Point(693, 528);
            this.UserControlRegisterG.Name = "UserControlRegisterG";
            this.UserControlRegisterG.ReadOnly = true;
            this.UserControlRegisterG.RegisterName = "RD";
            this.UserControlRegisterG.Size = new System.Drawing.Size(89, 20);
            this.UserControlRegisterG.TabIndex = 15;
            this.UserControlRegisterG.TabStop = false;
            this.UserControlRegisterG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panelALUView
            // 
            this.panelALUView.BackColor = System.Drawing.Color.White;
            this.panelALUView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelALUView.Location = new System.Drawing.Point(341, 304);
            this.panelALUView.Name = "panelALUView";
            this.panelALUView.Size = new System.Drawing.Size(156, 146);
            this.panelALUView.TabIndex = 14;
            // 
            // UserControlRegisterAK
            // 
            this.UserControlRegisterAK.BackColor = System.Drawing.Color.White;
            this.UserControlRegisterAK.CausesValidation = false;
            this.UserControlRegisterAK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UserControlRegisterAK.Location = new System.Drawing.Point(341, 282);
            this.UserControlRegisterAK.Name = "UserControlRegisterAK";
            this.UserControlRegisterAK.ReadOnly = true;
            this.UserControlRegisterAK.RegisterName = "ACC";
            this.UserControlRegisterAK.Size = new System.Drawing.Size(156, 20);
            this.UserControlRegisterAK.TabIndex = 13;
            this.UserControlRegisterAK.TabStop = false;
            this.UserControlRegisterAK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UserControlRegisterL
            // 
            this.UserControlRegisterL.BackColor = System.Drawing.Color.White;
            this.UserControlRegisterL.CausesValidation = false;
            this.UserControlRegisterL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UserControlRegisterL.Location = new System.Drawing.Point(26, 173);
            this.UserControlRegisterL.Name = "UserControlRegisterL";
            this.UserControlRegisterL.ReadOnly = true;
            this.UserControlRegisterL.RegisterName = "Program counter";
            this.UserControlRegisterL.Size = new System.Drawing.Size(156, 20);
            this.UserControlRegisterL.TabIndex = 12;
            this.UserControlRegisterL.TabStop = false;
            this.UserControlRegisterL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UserControlRegisterI
            // 
            this.UserControlRegisterI.BackColor = System.Drawing.Color.White;
            this.UserControlRegisterI.CausesValidation = false;
            this.UserControlRegisterI.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UserControlRegisterI.Location = new System.Drawing.Point(63, 430);
            this.UserControlRegisterI.Name = "UserControlRegisterI";
            this.UserControlRegisterI.ReadOnly = true;
            this.UserControlRegisterI.RegisterName = "INS";
            this.UserControlRegisterI.Size = new System.Drawing.Size(156, 20);
            this.UserControlRegisterI.TabIndex = 11;
            this.UserControlRegisterI.TabStop = false;
            this.UserControlRegisterI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // userControlBusAddress
            // 
            this.userControlBusAddress.BackColor = System.Drawing.SystemColors.Control;
            this.userControlBusAddress.Cursor = System.Windows.Forms.Cursors.Help;
            this.userControlBusAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 2.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userControlBusAddress.Location = new System.Drawing.Point(9, 130);
            this.userControlBusAddress.Multiline = true;
            this.userControlBusAddress.Name = "userControlBusAddress";
            this.userControlBusAddress.ReadOnly = true;
            this.userControlBusAddress.Size = new System.Drawing.Size(800, 6);
            this.userControlBusAddress.TabIndex = 10;
            // 
            // userControlBusData
            // 
            this.userControlBusData.BackColor = System.Drawing.SystemColors.Control;
            this.userControlBusData.Cursor = System.Windows.Forms.Cursors.Help;
            this.userControlBusData.Font = new System.Drawing.Font("Microsoft Sans Serif", 2.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userControlBusData.Location = new System.Drawing.Point(9, 486);
            this.userControlBusData.Multiline = true;
            this.userControlBusData.Name = "userControlBusData";
            this.userControlBusData.ReadOnly = true;
            this.userControlBusData.Size = new System.Drawing.Size(800, 6);
            this.userControlBusData.TabIndex = 9;
            // 
            // MemoryControl
            // 
            this.MemoryControl.BackColor = System.Drawing.Color.White;
            this.MemoryControl.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MemoryControl.Location = new System.Drawing.Point(582, 199);
            this.MemoryControl.Multiline = true;
            this.MemoryControl.Name = "MemoryControl";
            this.MemoryControl.PartiallySupressRefreshing = false;
            this.MemoryControl.ReadOnly = true;
            this.MemoryControl.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MemoryControl.Size = new System.Drawing.Size(180, 225);
            this.MemoryControl.TabIndex = 8;
            this.MemoryControl.TabStop = false;
            this.MemoryControl.WordWrap = false;
            // 
            // checkBoxManualDebug
            // 
            this.checkBoxManualDebug.AutoSize = true;
            this.checkBoxManualDebug.Location = new System.Drawing.Point(712, 82);
            this.checkBoxManualDebug.Name = "checkBoxManualDebug";
            this.checkBoxManualDebug.Size = new System.Drawing.Size(97, 17);
            this.checkBoxManualDebug.TabIndex = 7;
            this.checkBoxManualDebug.TabStop = false;
            this.checkBoxManualDebug.Text = "Manual Control";
            this.checkBoxManualDebug.UseVisualStyleBackColor = true;
            this.checkBoxManualDebug.CheckedChanged += new System.EventHandler(this.checkBoxManualDebug_CheckedChanged);
            // 
            // UserControlRegisterS
            // 
            this.UserControlRegisterS.BackColor = System.Drawing.Color.White;
            this.UserControlRegisterS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UserControlRegisterS.Location = new System.Drawing.Point(582, 430);
            this.UserControlRegisterS.Name = "UserControlRegisterS";
            this.UserControlRegisterS.ReadOnly = true;
            this.UserControlRegisterS.RegisterName = "Data";
            this.UserControlRegisterS.Size = new System.Drawing.Size(180, 20);
            this.UserControlRegisterS.TabIndex = 4;
            this.UserControlRegisterS.TabStop = false;
            this.UserControlRegisterS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UserControlRegisterA
            // 
            this.UserControlRegisterA.BackColor = System.Drawing.Color.White;
            this.UserControlRegisterA.CausesValidation = false;
            this.UserControlRegisterA.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UserControlRegisterA.Location = new System.Drawing.Point(582, 173);
            this.UserControlRegisterA.Name = "UserControlRegisterA";
            this.UserControlRegisterA.ReadOnly = true;
            this.UserControlRegisterA.RegisterName = "Address";
            this.UserControlRegisterA.Size = new System.Drawing.Size(180, 20);
            this.UserControlRegisterA.TabIndex = 2;
            this.UserControlRegisterA.TabStop = false;
            this.UserControlRegisterA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plikToolStripMenuItem,
            this.widokToolStripMenuItem,
            this.projektToolStripMenuItem,
            this.wykonajToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(822, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // plikToolStripMenuItem
            // 
            this.plikToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nowyToolStripMenuItem,
            this.otwórzToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.wyjścieToolStripMenuItem});
            this.plikToolStripMenuItem.Name = "plikToolStripMenuItem";
            this.plikToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.plikToolStripMenuItem.Text = "File";
            // 
            // nowyToolStripMenuItem
            // 
            this.nowyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programToolStripMenuItem,
            this.rozkazToolStripMenuItem});
            this.nowyToolStripMenuItem.Name = "nowyToolStripMenuItem";
            this.nowyToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.nowyToolStripMenuItem.Text = "New";
            // 
            // programToolStripMenuItem
            // 
            this.programToolStripMenuItem.Name = "programToolStripMenuItem";
            this.programToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.programToolStripMenuItem.Text = "Program";
            // 
            // rozkazToolStripMenuItem
            // 
            this.rozkazToolStripMenuItem.Name = "rozkazToolStripMenuItem";
            this.rozkazToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.rozkazToolStripMenuItem.Text = "Instruction";
            // 
            // otwórzToolStripMenuItem
            // 
            this.otwórzToolStripMenuItem.Name = "otwórzToolStripMenuItem";
            this.otwórzToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.otwórzToolStripMenuItem.Text = "Open";
            this.otwórzToolStripMenuItem.Click += new System.EventHandler(this.otwórzToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToFile_Click);
            // 
            // wyjścieToolStripMenuItem
            // 
            this.wyjścieToolStripMenuItem.Name = "wyjścieToolStripMenuItem";
            this.wyjścieToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.wyjścieToolStripMenuItem.Text = "Exit";
            // 
            // widokToolStripMenuItem
            // 
            this.widokToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearOutputConsoleToolStripMenuItem,
            this.registersDisplayModeToolStripMenuItem});
            this.widokToolStripMenuItem.Name = "widokToolStripMenuItem";
            this.widokToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.widokToolStripMenuItem.Text = "View";
            // 
            // clearOutputConsoleToolStripMenuItem
            // 
            this.clearOutputConsoleToolStripMenuItem.Name = "clearOutputConsoleToolStripMenuItem";
            this.clearOutputConsoleToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.clearOutputConsoleToolStripMenuItem.Text = "Clear output console";
            this.clearOutputConsoleToolStripMenuItem.Click += new System.EventHandler(this.clearOutputConsoleToolStripMenuItem_Click);
            // 
            // registersDisplayModeToolStripMenuItem
            // 
            this.registersDisplayModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.unsignedDecimalToolStripMenuItem,
            this.signedDecimalToolStripMenuItem,
            this.hexadecimalToolStripMenuItem,
            this.binaryToolStripMenuItem});
            this.registersDisplayModeToolStripMenuItem.Name = "registersDisplayModeToolStripMenuItem";
            this.registersDisplayModeToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.registersDisplayModeToolStripMenuItem.Text = "Registers display mode";
            // 
            // unsignedDecimalToolStripMenuItem
            // 
            this.unsignedDecimalToolStripMenuItem.Name = "unsignedDecimalToolStripMenuItem";
            this.unsignedDecimalToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.unsignedDecimalToolStripMenuItem.Text = "Unsigned decimal";
            this.unsignedDecimalToolStripMenuItem.Click += new System.EventHandler(this.DisplayModeToolStripMenuItem_Click);
            // 
            // signedDecimalToolStripMenuItem
            // 
            this.signedDecimalToolStripMenuItem.Name = "signedDecimalToolStripMenuItem";
            this.signedDecimalToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.signedDecimalToolStripMenuItem.Text = "Signed decimal";
            this.signedDecimalToolStripMenuItem.Click += new System.EventHandler(this.DisplayModeToolStripMenuItem_Click);
            // 
            // hexadecimalToolStripMenuItem
            // 
            this.hexadecimalToolStripMenuItem.Name = "hexadecimalToolStripMenuItem";
            this.hexadecimalToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.hexadecimalToolStripMenuItem.Text = "Hexadecimal";
            this.hexadecimalToolStripMenuItem.Click += new System.EventHandler(this.DisplayModeToolStripMenuItem_Click);
            // 
            // binaryToolStripMenuItem
            // 
            this.binaryToolStripMenuItem.Name = "binaryToolStripMenuItem";
            this.binaryToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.binaryToolStripMenuItem.Text = "Binary";
            this.binaryToolStripMenuItem.Click += new System.EventHandler(this.DisplayModeToolStripMenuItem_Click);
            // 
            // projektToolStripMenuItem
            // 
            this.projektToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ładujListęRozkazówToolStripMenuItem,
            this.instructionLanguageToolStripMenuItem,
            this.opcjeToolStripMenuItem,
            this.pIToolStripMenuItem});
            this.projektToolStripMenuItem.Name = "projektToolStripMenuItem";
            this.projektToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.projektToolStripMenuItem.Text = "Project";
            // 
            // ładujListęRozkazówToolStripMenuItem
            // 
            this.ładujListęRozkazówToolStripMenuItem.Name = "ładujListęRozkazówToolStripMenuItem";
            this.ładujListęRozkazówToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.ładujListęRozkazówToolStripMenuItem.Text = "Load instruction set";
            this.ładujListęRozkazówToolStripMenuItem.Click += new System.EventHandler(this.ładujListęRozkazówToolStripMenuItem_Click);
            // 
            // instructionLanguageToolStripMenuItem
            // 
            this.instructionLanguageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.polishToolStripMenuItem,
            this.englishToolStripMenuItem});
            this.instructionLanguageToolStripMenuItem.Name = "instructionLanguageToolStripMenuItem";
            this.instructionLanguageToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.instructionLanguageToolStripMenuItem.Text = "Instruction language";
            // 
            // polishToolStripMenuItem
            // 
            this.polishToolStripMenuItem.CheckOnClick = true;
            this.polishToolStripMenuItem.Name = "polishToolStripMenuItem";
            this.polishToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.polishToolStripMenuItem.Text = "Polish";
            this.polishToolStripMenuItem.Click += new System.EventHandler(this.polishToolStripMenuItem_Click);
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Checked = true;
            this.englishToolStripMenuItem.CheckOnClick = true;
            this.englishToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.englishToolStripMenuItem.Text = "English";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // opcjeToolStripMenuItem
            // 
            this.opcjeToolStripMenuItem.Name = "opcjeToolStripMenuItem";
            this.opcjeToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.opcjeToolStripMenuItem.Text = "Options . . .";
            this.opcjeToolStripMenuItem.Click += new System.EventHandler(this.opcjeToolStripMenuItem_Click);
            // 
            // pIToolStripMenuItem
            // 
            this.pIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.matrixModeToolStripMenuItem});
            this.pIToolStripMenuItem.Name = "pIToolStripMenuItem";
            this.pIToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.pIToolStripMenuItem.Text = "PI";
            // 
            // matrixModeToolStripMenuItem
            // 
            this.matrixModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.letterToolStripMenuItem,
            this.paintToolStripMenuItem});
            this.matrixModeToolStripMenuItem.Name = "matrixModeToolStripMenuItem";
            this.matrixModeToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.matrixModeToolStripMenuItem.Text = "Matrix mode";
            // 
            // letterToolStripMenuItem
            // 
            this.letterToolStripMenuItem.Checked = true;
            this.letterToolStripMenuItem.CheckOnClick = true;
            this.letterToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.letterToolStripMenuItem.Name = "letterToolStripMenuItem";
            this.letterToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.letterToolStripMenuItem.Text = "Letter";
            this.letterToolStripMenuItem.Click += new System.EventHandler(this.letterToolStripMenuItem_Click);
            // 
            // paintToolStripMenuItem
            // 
            this.paintToolStripMenuItem.CheckOnClick = true;
            this.paintToolStripMenuItem.Name = "paintToolStripMenuItem";
            this.paintToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.paintToolStripMenuItem.Text = "Paint";
            this.paintToolStripMenuItem.Click += new System.EventHandler(this.paintToolStripMenuItem_Click);
            // 
            // wykonajToolStripMenuItem
            // 
            this.wykonajToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programToolStripMenuItem1,
            this.rozkazToolStripMenuItem1,
            this.taktToolStripMenuItem,
            this.resetToolStripMenuItem});
            this.wykonajToolStripMenuItem.Name = "wykonajToolStripMenuItem";
            this.wykonajToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.wykonajToolStripMenuItem.Text = "Run";
            // 
            // programToolStripMenuItem1
            // 
            this.programToolStripMenuItem1.Name = "programToolStripMenuItem1";
            this.programToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.programToolStripMenuItem1.Size = new System.Drawing.Size(150, 22);
            this.programToolStripMenuItem1.Text = "Program";
            this.programToolStripMenuItem1.Click += new System.EventHandler(this.programToolStripMenuItem1_Click);
            // 
            // rozkazToolStripMenuItem1
            // 
            this.rozkazToolStripMenuItem1.Name = "rozkazToolStripMenuItem1";
            this.rozkazToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.rozkazToolStripMenuItem1.Size = new System.Drawing.Size(150, 22);
            this.rozkazToolStripMenuItem1.Text = "Instruction";
            this.rozkazToolStripMenuItem1.Click += new System.EventHandler(this.rozkazToolStripMenuItem1_Click);
            // 
            // taktToolStripMenuItem
            // 
            this.taktToolStripMenuItem.Name = "taktToolStripMenuItem";
            this.taktToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.taktToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.taktToolStripMenuItem.Text = "Tick";
            this.taktToolStripMenuItem.Click += new System.EventHandler(this.taktToolStripMenuItem_Click);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F2)));
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.resetToolStripMenuItem.Text = "Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(826, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 560);
            this.splitter2.TabIndex = 3;
            this.splitter2.TabStop = false;
            // 
            // TopRightPanel
            // 
            this.TopRightPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TopRightPanel.Controls.Add(this.ProgramPanel);
            this.TopRightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopRightPanel.Location = new System.Drawing.Point(829, 0);
            this.TopRightPanel.Name = "TopRightPanel";
            this.TopRightPanel.Size = new System.Drawing.Size(614, 560);
            this.TopRightPanel.TabIndex = 4;
            // 
            // ProgramPanel
            // 
            this.ProgramPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgramPanel.Controls.Add(this.tabControlEditors);
            this.ProgramPanel.Location = new System.Drawing.Point(-3, -2);
            this.ProgramPanel.Name = "ProgramPanel";
            this.ProgramPanel.Size = new System.Drawing.Size(617, 559);
            this.ProgramPanel.TabIndex = 0;
            // 
            // tabControlEditors
            // 
            this.tabControlEditors.Controls.Add(this.tabPageCodeEditor);
            this.tabControlEditors.Controls.Add(this.tabPageInstructionList);
            this.tabControlEditors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlEditors.HotTrack = true;
            this.tabControlEditors.Location = new System.Drawing.Point(0, 0);
            this.tabControlEditors.Name = "tabControlEditors";
            this.tabControlEditors.RightToLeftLayout = true;
            this.tabControlEditors.SelectedIndex = 0;
            this.tabControlEditors.Size = new System.Drawing.Size(617, 559);
            this.tabControlEditors.TabIndex = 3;
            this.tabControlEditors.TabStop = false;
            this.tabControlEditors.SelectedIndexChanged += new System.EventHandler(this.tabControlEditorsPanel_SelectedIndexChanged);
            // 
            // tabPageCodeEditor
            // 
            this.tabPageCodeEditor.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageCodeEditor.Controls.Add(this.UserControlCodeEditor);
            this.tabPageCodeEditor.Controls.Add(this.unixCodeEditorMenuStrip);
            this.tabPageCodeEditor.Location = new System.Drawing.Point(4, 22);
            this.tabPageCodeEditor.Name = "tabPageCodeEditor";
            this.tabPageCodeEditor.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCodeEditor.Size = new System.Drawing.Size(609, 533);
            this.tabPageCodeEditor.TabIndex = 0;
            this.tabPageCodeEditor.Text = "Editor";
            // 
            // UserControlCodeEditor
            // 
            this.UserControlCodeEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserControlCodeEditor.ContextMenuStrip = this.CodeEditorContextMenu;
            this.UserControlCodeEditor.Location = new System.Drawing.Point(17, 34);
            this.UserControlCodeEditor.Multiline = true;
            this.UserControlCodeEditor.Name = "UserControlCodeEditor";
            this.UserControlCodeEditor.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.UserControlCodeEditor.Size = new System.Drawing.Size(591, 478);
            this.UserControlCodeEditor.TabIndex = 9;
            // 
            // CodeEditorContextMenu
            // 
            this.CodeEditorContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CompileItemToolStrip,
            this.saveContexMenuItem,
            this.toolStripSeparator1,
            this.wytnijToolStripMenuItem,
            this.kopiujToolStripMenuItem,
            this.wklejToolStripMenuItem});
            this.CodeEditorContextMenu.Name = "CodeEditorContextMenu";
            this.CodeEditorContextMenu.Size = new System.Drawing.Size(169, 120);
            // 
            // CompileItemToolStrip
            // 
            this.CompileItemToolStrip.Name = "CompileItemToolStrip";
            this.CompileItemToolStrip.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F9)));
            this.CompileItemToolStrip.Size = new System.Drawing.Size(168, 22);
            this.CompileItemToolStrip.Text = "Kompiluj";
            this.CompileItemToolStrip.Click += new System.EventHandler(this.CompileItemToolStrip_Click);
            // 
            // saveContexMenuItem
            // 
            this.saveContexMenuItem.Name = "saveContexMenuItem";
            this.saveContexMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveContexMenuItem.Size = new System.Drawing.Size(168, 22);
            this.saveContexMenuItem.Text = "Zapisz";
            this.saveContexMenuItem.Click += new System.EventHandler(this.saveToFile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(165, 6);
            // 
            // wytnijToolStripMenuItem
            // 
            this.wytnijToolStripMenuItem.Name = "wytnijToolStripMenuItem";
            this.wytnijToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.wytnijToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.wytnijToolStripMenuItem.Text = "Wytnij";
            this.wytnijToolStripMenuItem.Click += new System.EventHandler(this.wytnijToolStripMenuItem_Click);
            // 
            // kopiujToolStripMenuItem
            // 
            this.kopiujToolStripMenuItem.Name = "kopiujToolStripMenuItem";
            this.kopiujToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.kopiujToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.kopiujToolStripMenuItem.Text = "Kopiuj";
            this.kopiujToolStripMenuItem.Click += new System.EventHandler(this.kopiujToolStripMenuItem_Click);
            // 
            // wklejToolStripMenuItem
            // 
            this.wklejToolStripMenuItem.Name = "wklejToolStripMenuItem";
            this.wklejToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.wklejToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.wklejToolStripMenuItem.Text = "Wklej";
            this.wklejToolStripMenuItem.Click += new System.EventHandler(this.wklejToolStripMenuItem_Click);
            // 
            // unixCodeEditorMenuStrip
            // 
            this.unixCodeEditorMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kodToolStripMenuItem});
            this.unixCodeEditorMenuStrip.Location = new System.Drawing.Point(3, 3);
            this.unixCodeEditorMenuStrip.Name = "unixCodeEditorMenuStrip";
            this.unixCodeEditorMenuStrip.Size = new System.Drawing.Size(603, 24);
            this.unixCodeEditorMenuStrip.TabIndex = 2;
            this.unixCodeEditorMenuStrip.Text = "menuStrip2";
            // 
            // kodToolStripMenuItem
            // 
            this.kodToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kompilujToolStripMenuItem,
            this.saveUnixToolStripMenuItem});
            this.kodToolStripMenuItem.Name = "kodToolStripMenuItem";
            this.kodToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.kodToolStripMenuItem.Text = "Code";
            // 
            // kompilujToolStripMenuItem
            // 
            this.kompilujToolStripMenuItem.Name = "kompilujToolStripMenuItem";
            this.kompilujToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F9)));
            this.kompilujToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.kompilujToolStripMenuItem.Text = "Compile";
            this.kompilujToolStripMenuItem.Click += new System.EventHandler(this.CompileItemToolStrip_Click);
            // 
            // saveUnixToolStripMenuItem
            // 
            this.saveUnixToolStripMenuItem.Name = "saveUnixToolStripMenuItem";
            this.saveUnixToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveUnixToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.saveUnixToolStripMenuItem.Text = "Save";
            this.saveUnixToolStripMenuItem.Click += new System.EventHandler(this.saveToFile_Click);
            // 
            // tabPageInstructionList
            // 
            this.tabPageInstructionList.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageInstructionList.Controls.Add(this.panelInstructionsMicrocode);
            this.tabPageInstructionList.Controls.Add(this.splitter3);
            this.tabPageInstructionList.Controls.Add(this.panelInstructionsList);
            this.tabPageInstructionList.Location = new System.Drawing.Point(4, 22);
            this.tabPageInstructionList.Name = "tabPageInstructionList";
            this.tabPageInstructionList.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInstructionList.Size = new System.Drawing.Size(609, 533);
            this.tabPageInstructionList.TabIndex = 1;
            this.tabPageInstructionList.Text = "Instructions list";
            // 
            // panelInstructionsMicrocode
            // 
            this.panelInstructionsMicrocode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelInstructionsMicrocode.Controls.Add(this.userControlInstructionMicrocode1);
            this.panelInstructionsMicrocode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInstructionsMicrocode.Location = new System.Drawing.Point(206, 3);
            this.panelInstructionsMicrocode.Name = "panelInstructionsMicrocode";
            this.panelInstructionsMicrocode.Size = new System.Drawing.Size(400, 527);
            this.panelInstructionsMicrocode.TabIndex = 2;
            // 
            // userControlInstructionMicrocode1
            // 
            this.userControlInstructionMicrocode1.BackColor = System.Drawing.Color.White;
            this.userControlInstructionMicrocode1.Cursor = System.Windows.Forms.Cursors.Default;
            this.userControlInstructionMicrocode1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlInstructionMicrocode1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userControlInstructionMicrocode1.Location = new System.Drawing.Point(0, 0);
            this.userControlInstructionMicrocode1.Multiline = true;
            this.userControlInstructionMicrocode1.Name = "userControlInstructionMicrocode1";
            this.userControlInstructionMicrocode1.ReadOnly = true;
            this.userControlInstructionMicrocode1.Size = new System.Drawing.Size(396, 523);
            this.userControlInstructionMicrocode1.TabIndex = 0;
            // 
            // splitter3
            // 
            this.splitter3.Location = new System.Drawing.Point(203, 3);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(3, 527);
            this.splitter3.TabIndex = 1;
            this.splitter3.TabStop = false;
            // 
            // panelInstructionsList
            // 
            this.panelInstructionsList.Controls.Add(this.userControlInstructionList1);
            this.panelInstructionsList.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelInstructionsList.Location = new System.Drawing.Point(3, 3);
            this.panelInstructionsList.Name = "panelInstructionsList";
            this.panelInstructionsList.Size = new System.Drawing.Size(200, 527);
            this.panelInstructionsList.TabIndex = 0;
            // 
            // userControlInstructionList1
            // 
            this.userControlInstructionList1.BackColor = System.Drawing.Color.White;
            this.userControlInstructionList1.Cursor = System.Windows.Forms.Cursors.Default;
            this.userControlInstructionList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlInstructionList1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userControlInstructionList1.Location = new System.Drawing.Point(0, 0);
            this.userControlInstructionList1.Multiline = true;
            this.userControlInstructionList1.Name = "userControlInstructionList1";
            this.userControlInstructionList1.ReadOnly = true;
            this.userControlInstructionList1.Size = new System.Drawing.Size(200, 527);
            this.userControlInstructionList1.TabIndex = 0;
            this.userControlInstructionList1.WordWrap = false;
            // 
            // MainToolTip
            // 
            this.MainToolTip.UseAnimation = false;
            this.MainToolTip.UseFading = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1443, 702);
            this.Controls.Add(this.TopRightPanel);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.TopLeftPanel);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.BottomPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "PI Machine ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.BottomPanel.ResumeLayout(false);
            this.tabControlOnBottomPanel.ResumeLayout(false);
            this.tabPageInput.ResumeLayout(false);
            this.tabPageInput.PerformLayout();
            this.tabPageOutput.ResumeLayout(false);
            this.tabPageOutput.PerformLayout();
            this.TopLeftPanel.ResumeLayout(false);
            this.CPUMainPanel.ResumeLayout(false);
            this.CPUMainPanel.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.TopRightPanel.ResumeLayout(false);
            this.ProgramPanel.ResumeLayout(false);
            this.tabControlEditors.ResumeLayout(false);
            this.tabPageCodeEditor.ResumeLayout(false);
            this.tabPageCodeEditor.PerformLayout();
            this.CodeEditorContextMenu.ResumeLayout(false);
            this.unixCodeEditorMenuStrip.ResumeLayout(false);
            this.unixCodeEditorMenuStrip.PerformLayout();
            this.tabPageInstructionList.ResumeLayout(false);
            this.panelInstructionsMicrocode.ResumeLayout(false);
            this.panelInstructionsMicrocode.PerformLayout();
            this.panelInstructionsList.ResumeLayout(false);
            this.panelInstructionsList.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel BottomPanel;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel TopLeftPanel;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel TopRightPanel;
        private System.Windows.Forms.Panel CPUMainPanel;
        private System.Windows.Forms.Panel ProgramPanel;
        private System.Windows.Forms.ContextMenuStrip CodeEditorContextMenu;
        private System.Windows.Forms.ToolStripMenuItem CompileItemToolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem wytnijToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kopiujToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wklejToolStripMenuItem;
        private MachineUI.UserControlRegister UserControlRegisterA;
        private MachineUI.UserControlRegister UserControlRegisterS;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem plikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nowyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rozkazToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otwórzToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wyjścieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem widokToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projektToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ładujListęRozkazówToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opcjeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wykonajToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem rozkazToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem taktToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxManualDebug;
        private MachineUI.UserControlMemory MemoryControl;
        private MachineUI.UserControlCharacterInput UserControlCharacterInput;
        private System.Windows.Forms.TabControl tabControlOnBottomPanel;
        private System.Windows.Forms.TabPage tabPageInput;
        private System.Windows.Forms.ToolStripMenuItem saveContexMenuItem;
        private MachineUI.UserControlBus userControlBusAddress;
        private MachineUI.UserControlBus userControlBusData;
        private System.Windows.Forms.Panel panelALUView;
        private MachineUI.UserControlRegister UserControlRegisterAK;
        private MachineUI.UserControlRegister UserControlRegisterL;
        private MachineUI.UserControlRegister UserControlRegisterI;
        private MachineUI.UserControlBus userControlBusAS;
        private MachineUI.UserControlRegister UserControlRegisterRM;
        private MachineUI.UserControlRegister UserControlRegisterAP;
        private MachineUI.UserControlRegister UserControlRegisterRP;
        private MachineUI.UserControlRegister UserControlRegisterRZ;
        private MachineUI.UserControlRegister UserControlRegisterWS;
        private MachineUI.UserControlRegister UserControlRegisterX;
        private MachineUI.UserControlRegister UserControlRegisterY;
        private MachineUI.UserControlRegister UserControlRegisterRB;
        private MachineUI.UserControlRegister UserControlRegisterG;
        private MachineUI.UserControlIntButton userControlIntButton4;
        private MachineUI.UserControlIntButton userControlIntButton3;
        private MachineUI.UserControlIntButton userControlIntButton2;
        private MachineUI.UserControlIntButton userControlIntButton1;
        private System.Windows.Forms.TabPage tabPageOutput;
        private MachineUI.UserControlCharacterOutput UserControlCharacterOutput;
        private System.Windows.Forms.ToolStripMenuItem clearOutputConsoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem matrixModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem letterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem instructionLanguageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private MachineUI.UserControlSignalWire userControlSignalWire_id;
        private System.Windows.Forms.ToolStripMenuItem registersDisplayModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unsignedDecimalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signedDecimalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hexadecimalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem binaryToolStripMenuItem;
        private MachineUI.UserControlSignalWire userControlSignalWire_rd;
        private MachineUI.UserControlSignalWire userControlSignalWire_ia;
        private MachineUI.UserControlSignalWire userControlSignalWire1_od;
        private MachineUI.UserControlSignalWire userControlSignalWire_wr;
        private MachineUI.UserControlSignalWire userControlSignalWire_dcacc;
        private MachineUI.UserControlSignalWire userControlSignalWire_not;
        private MachineUI.UserControlSignalWire userControlSignalWire_oacc;
        private MachineUI.UserControlSignalWire userControlSignalWire_ialu;
        private MachineUI.UserControlSignalWire userControlSignalWire2;
        private MachineUI.UserControlSignalWire userControlSignalWire_or;
        private MachineUI.UserControlSignalWire userControlSignalWire_shr;
        private MachineUI.UserControlSignalWire userControlSignalWire_div;
        private MachineUI.UserControlSignalWire userControlSignalWire_mul;
        private MachineUI.UserControlSignalWire userControlSignalWire_wracc;
        private MachineUI.UserControlSignalWire userControlSignalWire_sub;
        private MachineUI.UserControlSignalWire userControlSignalWire_add;
        private MachineUI.UserControlSignalWire userControlSignalWire_icacc;
        private MachineUI.UserControlSignalWire userControlSignalWire_and;
        private MachineUI.UserControlSignalWire userControlSignalWire_t;
        private MachineUI.UserControlSignalWire userControlSignalWire_osp;
        private MachineUI.UserControlSignalWire userControlSignalWire_isp;
        private MachineUI.UserControlSignalWire userControlSignalWire_dcsp;
        private MachineUI.UserControlSignalWire userControlSignalWire_icsp;
        private MachineUI.UserControlSignalWire userControlSignalWire_icit;
        private MachineUI.UserControlSignalWire userControlSignalWire_iit;
        private MachineUI.UserControlSignalWire userControlSignalWire_oit;
        private MachineUI.UserControlSignalWire userControlSignalWire_iins;
        private MachineUI.UserControlSignalWire userControlSignalWire_start;
        private MachineUI.UserControlSignalWire userControlSignalWire_ord;
        private MachineUI.UserControlSignalWire userControlSignalWire_ibuf;
        private MachineUI.UserControlSignalWire userControlSignalWire_obuf;
        private MachineUI.UserControlSignalWire userControlSignalWire_oy;
        private MachineUI.UserControlSignalWire userControlSignalWire_iy;
        private MachineUI.UserControlSignalWire userControlSignalWire_ox;
        private MachineUI.UserControlSignalWire userControlSignalWire_oiv;
        private MachineUI.UserControlSignalWire userControlSignalWire_oim;
        private MachineUI.UserControlSignalWire userControlSignalWire_im;
        private MachineUI.UserControlSignalWire userControlSignalWire_eni;
        private MachineUI.UserControlSignalWire userControlSignalWire_rint;
        private MachineUI.UserControlSignalWire userControlSignalWire_oa;
        private MachineUI.UserControlSignalWire userControlSignalWire_oitd;
        private MachineUI.UserControlSignalWire userControlSignalWire_stop;
        private MachineUI.UserControlSignalWire userControlSignalWire_ix;
        private System.Windows.Forms.Panel breakPanel;
        private System.Windows.Forms.TabControl tabControlEditors;
        private System.Windows.Forms.TabPage tabPageCodeEditor;
        private MachineUI.UserControlCodeEditor UserControlCodeEditor;
        private System.Windows.Forms.MenuStrip unixCodeEditorMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem kodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kompilujToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveUnixToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageInstructionList;
        private System.Windows.Forms.Panel panelInstructionsMicrocode;
        private MachineUI.UserControlInstructionMicrocode userControlInstructionMicrocode1;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Panel panelInstructionsList;
        private MachineUI.UserControlInstructionList userControlInstructionList1;
        private System.Windows.Forms.ToolTip MainToolTip;
        private MachineUI.UserControlRegister UserControlRegisterF;
    }
}

