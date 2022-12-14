
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
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.tabControlOnBottomPanel = new System.Windows.Forms.TabControl();
            this.tabPageIO = new System.Windows.Forms.TabPage();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.TopLeftPanel = new System.Windows.Forms.Panel();
            this.MicrocontrollerPanel = new System.Windows.Forms.Panel();
            this.checkBoxManualDebug = new System.Windows.Forms.CheckBox();
            this.groupBoxDebugLevel = new System.Windows.Forms.GroupBox();
            this.radioButtonDebugTick = new System.Windows.Forms.RadioButton();
            this.radioButtonDebugInstruction = new System.Windows.Forms.RadioButton();
            this.radioButtonDebugProgram = new System.Windows.Forms.RadioButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.plikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nowyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rozkazToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otwórzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wyjścieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.widokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projektToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zapiszListęRozkazówToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ładujListęRozkazówToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opcjeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wykonajToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.rozkazToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.taktToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doKursoraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.przerwijToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pomocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.TopRightPanel = new System.Windows.Forms.Panel();
            this.ProgramPanel = new System.Windows.Forms.Panel();
            this.tabControlEditors = new System.Windows.Forms.TabControl();
            this.tabPageCodeEditor = new System.Windows.Forms.TabPage();
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
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.panelInstructionsList = new System.Windows.Forms.Panel();
            this.tabPageVariables = new System.Windows.Forms.TabPage();
            this.UserControlCodeEditor = new MaszynaPi.MachineUI.UserControlCodeEditor();
            this.userControlInstructionMicrocode1 = new MaszynaPi.MachineUI.UserControlInstructionMicrocode();
            this.userControlInstructionList1 = new MaszynaPi.MachineUI.UserControlInstructionList();
            this.MemoryControl = new MaszynaPi.MachineUI.UserControlMemory();
            this.UserControlRegisterS = new MaszynaPi.MachineUI.UserControlRegister();
            this.UserControlRegisterA = new MaszynaPi.MachineUI.UserControlRegister();
            this.UserControlCharacterInput = new MaszynaPi.MachineUI.UserControlCharacterInput();
            this.userControlBusData = new MaszynaPi.MachineUI.UserControlBus();
            this.userControlBusAddress = new MaszynaPi.MachineUI.UserControlBus();
            this.UserControlRegisterI = new MaszynaPi.MachineUI.UserControlRegister();
            this.UserControlRegisterL = new MaszynaPi.MachineUI.UserControlRegister();
            this.UserControlRegisterAK = new MaszynaPi.MachineUI.UserControlRegister();
            this.panelALUView = new System.Windows.Forms.Panel();
            this.BottomPanel.SuspendLayout();
            this.tabControlOnBottomPanel.SuspendLayout();
            this.tabPageIO.SuspendLayout();
            this.TopLeftPanel.SuspendLayout();
            this.MicrocontrollerPanel.SuspendLayout();
            this.groupBoxDebugLevel.SuspendLayout();
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
            this.BottomPanel.Location = new System.Drawing.Point(0, 535);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(1443, 139);
            this.BottomPanel.TabIndex = 0;
            // 
            // tabControlOnBottomPanel
            // 
            this.tabControlOnBottomPanel.Controls.Add(this.tabPageIO);
            this.tabControlOnBottomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlOnBottomPanel.Location = new System.Drawing.Point(0, 0);
            this.tabControlOnBottomPanel.Name = "tabControlOnBottomPanel";
            this.tabControlOnBottomPanel.SelectedIndex = 0;
            this.tabControlOnBottomPanel.Size = new System.Drawing.Size(1439, 135);
            this.tabControlOnBottomPanel.TabIndex = 0;
            this.tabControlOnBottomPanel.TabStop = false;
            // 
            // tabPageIO
            // 
            this.tabPageIO.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageIO.Controls.Add(this.UserControlCharacterInput);
            this.tabPageIO.Location = new System.Drawing.Point(4, 22);
            this.tabPageIO.Name = "tabPageIO";
            this.tabPageIO.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageIO.Size = new System.Drawing.Size(1431, 109);
            this.tabPageIO.TabIndex = 0;
            this.tabPageIO.Text = "Konsola wejścia";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 532);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1443, 3);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // TopLeftPanel
            // 
            this.TopLeftPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TopLeftPanel.Controls.Add(this.MicrocontrollerPanel);
            this.TopLeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.TopLeftPanel.Location = new System.Drawing.Point(0, 0);
            this.TopLeftPanel.Name = "TopLeftPanel";
            this.TopLeftPanel.Size = new System.Drawing.Size(779, 532);
            this.TopLeftPanel.TabIndex = 2;
            // 
            // MicrocontrollerPanel
            // 
            this.MicrocontrollerPanel.Controls.Add(this.panelALUView);
            this.MicrocontrollerPanel.Controls.Add(this.UserControlRegisterAK);
            this.MicrocontrollerPanel.Controls.Add(this.UserControlRegisterL);
            this.MicrocontrollerPanel.Controls.Add(this.UserControlRegisterI);
            this.MicrocontrollerPanel.Controls.Add(this.userControlBusAddress);
            this.MicrocontrollerPanel.Controls.Add(this.userControlBusData);
            this.MicrocontrollerPanel.Controls.Add(this.MemoryControl);
            this.MicrocontrollerPanel.Controls.Add(this.checkBoxManualDebug);
            this.MicrocontrollerPanel.Controls.Add(this.groupBoxDebugLevel);
            this.MicrocontrollerPanel.Controls.Add(this.UserControlRegisterS);
            this.MicrocontrollerPanel.Controls.Add(this.UserControlRegisterA);
            this.MicrocontrollerPanel.Controls.Add(this.menuStrip1);
            this.MicrocontrollerPanel.Location = new System.Drawing.Point(1, 1);
            this.MicrocontrollerPanel.Name = "MicrocontrollerPanel";
            this.MicrocontrollerPanel.Size = new System.Drawing.Size(776, 532);
            this.MicrocontrollerPanel.TabIndex = 0;
            // 
            // checkBoxManualDebug
            // 
            this.checkBoxManualDebug.AutoSize = true;
            this.checkBoxManualDebug.Location = new System.Drawing.Point(445, 21);
            this.checkBoxManualDebug.Name = "checkBoxManualDebug";
            this.checkBoxManualDebug.Size = new System.Drawing.Size(114, 17);
            this.checkBoxManualDebug.TabIndex = 7;
            this.checkBoxManualDebug.TabStop = false;
            this.checkBoxManualDebug.Text = "Sterowanie ręczne";
            this.checkBoxManualDebug.UseVisualStyleBackColor = true;
            // 
            // groupBoxDebugLevel
            // 
            this.groupBoxDebugLevel.Controls.Add(this.radioButtonDebugTick);
            this.groupBoxDebugLevel.Controls.Add(this.radioButtonDebugInstruction);
            this.groupBoxDebugLevel.Controls.Add(this.radioButtonDebugProgram);
            this.groupBoxDebugLevel.Location = new System.Drawing.Point(435, 39);
            this.groupBoxDebugLevel.Name = "groupBoxDebugLevel";
            this.groupBoxDebugLevel.Size = new System.Drawing.Size(174, 72);
            this.groupBoxDebugLevel.TabIndex = 5;
            this.groupBoxDebugLevel.TabStop = false;
            this.groupBoxDebugLevel.Text = "Poziom śledzenia";
            // 
            // radioButtonDebugTick
            // 
            this.radioButtonDebugTick.AutoSize = true;
            this.radioButtonDebugTick.Location = new System.Drawing.Point(6, 50);
            this.radioButtonDebugTick.Name = "radioButtonDebugTick";
            this.radioButtonDebugTick.Size = new System.Drawing.Size(84, 17);
            this.radioButtonDebugTick.TabIndex = 2;
            this.radioButtonDebugTick.Text = "wysoki (takt)";
            this.radioButtonDebugTick.UseVisualStyleBackColor = true;
            // 
            // radioButtonDebugInstruction
            // 
            this.radioButtonDebugInstruction.AutoSize = true;
            this.radioButtonDebugInstruction.Location = new System.Drawing.Point(6, 32);
            this.radioButtonDebugInstruction.Name = "radioButtonDebugInstruction";
            this.radioButtonDebugInstruction.Size = new System.Drawing.Size(93, 17);
            this.radioButtonDebugInstruction.TabIndex = 1;
            this.radioButtonDebugInstruction.Text = "średni (rozkaz)";
            this.radioButtonDebugInstruction.UseVisualStyleBackColor = true;
            // 
            // radioButtonDebugProgram
            // 
            this.radioButtonDebugProgram.AutoSize = true;
            this.radioButtonDebugProgram.Location = new System.Drawing.Point(6, 14);
            this.radioButtonDebugProgram.Name = "radioButtonDebugProgram";
            this.radioButtonDebugProgram.Size = new System.Drawing.Size(93, 17);
            this.radioButtonDebugProgram.TabIndex = 0;
            this.radioButtonDebugProgram.Text = "niski (program)";
            this.radioButtonDebugProgram.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plikToolStripMenuItem,
            this.widokToolStripMenuItem,
            this.projektToolStripMenuItem,
            this.wykonajToolStripMenuItem,
            this.pomocToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(776, 24);
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
            this.plikToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.plikToolStripMenuItem.Text = "Plik";
            // 
            // nowyToolStripMenuItem
            // 
            this.nowyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programToolStripMenuItem,
            this.rozkazToolStripMenuItem});
            this.nowyToolStripMenuItem.Name = "nowyToolStripMenuItem";
            this.nowyToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.nowyToolStripMenuItem.Text = "Nowy";
            // 
            // programToolStripMenuItem
            // 
            this.programToolStripMenuItem.Name = "programToolStripMenuItem";
            this.programToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.programToolStripMenuItem.Text = "Program";
            // 
            // rozkazToolStripMenuItem
            // 
            this.rozkazToolStripMenuItem.Name = "rozkazToolStripMenuItem";
            this.rozkazToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.rozkazToolStripMenuItem.Text = "Rozkaz";
            // 
            // otwórzToolStripMenuItem
            // 
            this.otwórzToolStripMenuItem.Name = "otwórzToolStripMenuItem";
            this.otwórzToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.otwórzToolStripMenuItem.Text = "Otwórz";
            this.otwórzToolStripMenuItem.Click += new System.EventHandler(this.otwórzToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.saveToolStripMenuItem.Text = "Zapisz";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // wyjścieToolStripMenuItem
            // 
            this.wyjścieToolStripMenuItem.Name = "wyjścieToolStripMenuItem";
            this.wyjścieToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.wyjścieToolStripMenuItem.Text = "Wyjście";
            // 
            // widokToolStripMenuItem
            // 
            this.widokToolStripMenuItem.Name = "widokToolStripMenuItem";
            this.widokToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.widokToolStripMenuItem.Text = "Widok";
            // 
            // projektToolStripMenuItem
            // 
            this.projektToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zapiszListęRozkazówToolStripMenuItem,
            this.ładujListęRozkazówToolStripMenuItem,
            this.opcjeToolStripMenuItem});
            this.projektToolStripMenuItem.Name = "projektToolStripMenuItem";
            this.projektToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.projektToolStripMenuItem.Text = "Projekt";
            // 
            // zapiszListęRozkazówToolStripMenuItem
            // 
            this.zapiszListęRozkazówToolStripMenuItem.Name = "zapiszListęRozkazówToolStripMenuItem";
            this.zapiszListęRozkazówToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.zapiszListęRozkazówToolStripMenuItem.Text = "Zapisz listę rozkazów ";
            // 
            // ładujListęRozkazówToolStripMenuItem
            // 
            this.ładujListęRozkazówToolStripMenuItem.Name = "ładujListęRozkazówToolStripMenuItem";
            this.ładujListęRozkazówToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.ładujListęRozkazówToolStripMenuItem.Text = "Ładuj listę rozkazów";
            this.ładujListęRozkazówToolStripMenuItem.Click += new System.EventHandler(this.ładujListęRozkazówToolStripMenuItem_Click);
            // 
            // opcjeToolStripMenuItem
            // 
            this.opcjeToolStripMenuItem.Name = "opcjeToolStripMenuItem";
            this.opcjeToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.opcjeToolStripMenuItem.Text = "Opcje . . .";
            this.opcjeToolStripMenuItem.Click += new System.EventHandler(this.opcjeToolStripMenuItem_Click);
            // 
            // wykonajToolStripMenuItem
            // 
            this.wykonajToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programToolStripMenuItem1,
            this.rozkazToolStripMenuItem1,
            this.taktToolStripMenuItem,
            this.doKursoraToolStripMenuItem,
            this.resetToolStripMenuItem,
            this.przerwijToolStripMenuItem});
            this.wykonajToolStripMenuItem.Name = "wykonajToolStripMenuItem";
            this.wykonajToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.wykonajToolStripMenuItem.Text = "Wykonaj";
            // 
            // programToolStripMenuItem1
            // 
            this.programToolStripMenuItem1.Name = "programToolStripMenuItem1";
            this.programToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.programToolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.programToolStripMenuItem1.Text = "Program";
            this.programToolStripMenuItem1.Click += new System.EventHandler(this.programToolStripMenuItem1_Click);
            // 
            // rozkazToolStripMenuItem1
            // 
            this.rozkazToolStripMenuItem1.Name = "rozkazToolStripMenuItem1";
            this.rozkazToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.rozkazToolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.rozkazToolStripMenuItem1.Text = "Rozkaz";
            this.rozkazToolStripMenuItem1.Click += new System.EventHandler(this.rozkazToolStripMenuItem1_Click);
            // 
            // taktToolStripMenuItem
            // 
            this.taktToolStripMenuItem.Name = "taktToolStripMenuItem";
            this.taktToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.taktToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.taktToolStripMenuItem.Text = "Takt";
            this.taktToolStripMenuItem.Click += new System.EventHandler(this.taktToolStripMenuItem_Click);
            // 
            // doKursoraToolStripMenuItem
            // 
            this.doKursoraToolStripMenuItem.Name = "doKursoraToolStripMenuItem";
            this.doKursoraToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.doKursoraToolStripMenuItem.Text = "Do kursora";
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F2)));
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.resetToolStripMenuItem.Text = "Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // przerwijToolStripMenuItem
            // 
            this.przerwijToolStripMenuItem.Name = "przerwijToolStripMenuItem";
            this.przerwijToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.przerwijToolStripMenuItem.Text = "Przerwij";
            // 
            // pomocToolStripMenuItem
            // 
            this.pomocToolStripMenuItem.Name = "pomocToolStripMenuItem";
            this.pomocToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.pomocToolStripMenuItem.Text = "Pomoc";
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(779, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 532);
            this.splitter2.TabIndex = 3;
            this.splitter2.TabStop = false;
            // 
            // TopRightPanel
            // 
            this.TopRightPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TopRightPanel.Controls.Add(this.ProgramPanel);
            this.TopRightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopRightPanel.Location = new System.Drawing.Point(782, 0);
            this.TopRightPanel.Name = "TopRightPanel";
            this.TopRightPanel.Size = new System.Drawing.Size(661, 532);
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
            this.ProgramPanel.Size = new System.Drawing.Size(664, 531);
            this.ProgramPanel.TabIndex = 0;
            // 
            // tabControlEditors
            // 
            this.tabControlEditors.Controls.Add(this.tabPageCodeEditor);
            this.tabControlEditors.Controls.Add(this.tabPageInstructionList);
            this.tabControlEditors.Controls.Add(this.tabPageVariables);
            this.tabControlEditors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlEditors.HotTrack = true;
            this.tabControlEditors.Location = new System.Drawing.Point(0, 0);
            this.tabControlEditors.Name = "tabControlEditors";
            this.tabControlEditors.RightToLeftLayout = true;
            this.tabControlEditors.SelectedIndex = 0;
            this.tabControlEditors.Size = new System.Drawing.Size(664, 531);
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
            this.tabPageCodeEditor.Size = new System.Drawing.Size(656, 505);
            this.tabPageCodeEditor.TabIndex = 0;
            this.tabPageCodeEditor.Text = "Edytor";
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
            this.saveContexMenuItem.Click += new System.EventHandler(this.saveContexMenuItem_Click);
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
            this.unixCodeEditorMenuStrip.Size = new System.Drawing.Size(650, 24);
            this.unixCodeEditorMenuStrip.TabIndex = 2;
            this.unixCodeEditorMenuStrip.Text = "menuStrip2";
            // 
            // kodToolStripMenuItem
            // 
            this.kodToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kompilujToolStripMenuItem,
            this.saveUnixToolStripMenuItem});
            this.kodToolStripMenuItem.Name = "kodToolStripMenuItem";
            this.kodToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.kodToolStripMenuItem.Text = "Kod";
            // 
            // kompilujToolStripMenuItem
            // 
            this.kompilujToolStripMenuItem.Name = "kompilujToolStripMenuItem";
            this.kompilujToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F9)));
            this.kompilujToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.kompilujToolStripMenuItem.Text = "Kompiluj";
            this.kompilujToolStripMenuItem.Click += new System.EventHandler(this.kompilujToolStripMenuItem_Click);
            // 
            // saveUnixToolStripMenuItem
            // 
            this.saveUnixToolStripMenuItem.Name = "saveUnixToolStripMenuItem";
            this.saveUnixToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveUnixToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.saveUnixToolStripMenuItem.Text = "Zapisz";
            this.saveUnixToolStripMenuItem.Click += new System.EventHandler(this.saveUnixToolStripMenuItem_Click);
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
            this.tabPageInstructionList.Size = new System.Drawing.Size(656, 464);
            this.tabPageInstructionList.TabIndex = 1;
            this.tabPageInstructionList.Text = "Lista rozkazów";
            // 
            // panelInstructionsMicrocode
            // 
            this.panelInstructionsMicrocode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelInstructionsMicrocode.Controls.Add(this.userControlInstructionMicrocode1);
            this.panelInstructionsMicrocode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInstructionsMicrocode.Location = new System.Drawing.Point(206, 3);
            this.panelInstructionsMicrocode.Name = "panelInstructionsMicrocode";
            this.panelInstructionsMicrocode.Size = new System.Drawing.Size(447, 458);
            this.panelInstructionsMicrocode.TabIndex = 2;
            // 
            // splitter3
            // 
            this.splitter3.Location = new System.Drawing.Point(203, 3);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(3, 458);
            this.splitter3.TabIndex = 1;
            this.splitter3.TabStop = false;
            // 
            // panelInstructionsList
            // 
            this.panelInstructionsList.Controls.Add(this.userControlInstructionList1);
            this.panelInstructionsList.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelInstructionsList.Location = new System.Drawing.Point(3, 3);
            this.panelInstructionsList.Name = "panelInstructionsList";
            this.panelInstructionsList.Size = new System.Drawing.Size(200, 458);
            this.panelInstructionsList.TabIndex = 0;
            // 
            // tabPageVariables
            // 
            this.tabPageVariables.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageVariables.Location = new System.Drawing.Point(4, 22);
            this.tabPageVariables.Name = "tabPageVariables";
            this.tabPageVariables.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageVariables.Size = new System.Drawing.Size(656, 464);
            this.tabPageVariables.TabIndex = 2;
            this.tabPageVariables.Text = "Zmienne";
            // 
            // UserControlCodeEditor
            // 
            this.UserControlCodeEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.UserControlCodeEditor.ContextMenuStrip = this.CodeEditorContextMenu;
            this.UserControlCodeEditor.Location = new System.Drawing.Point(19, 34);
            this.UserControlCodeEditor.Multiline = true;
            this.UserControlCodeEditor.Name = "UserControlCodeEditor";
            this.UserControlCodeEditor.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.UserControlCodeEditor.Size = new System.Drawing.Size(627, 450);
            this.UserControlCodeEditor.TabIndex = 9;
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
            this.userControlInstructionMicrocode1.Size = new System.Drawing.Size(443, 454);
            this.userControlInstructionMicrocode1.TabIndex = 0;
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
            this.userControlInstructionList1.Size = new System.Drawing.Size(200, 458);
            this.userControlInstructionList1.TabIndex = 0;
            this.userControlInstructionList1.WordWrap = false;
            // 
            // MemoryControl
            // 
            this.MemoryControl.BackColor = System.Drawing.Color.White;
            this.MemoryControl.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MemoryControl.Location = new System.Drawing.Point(542, 188);
            this.MemoryControl.Multiline = true;
            this.MemoryControl.Name = "MemoryControl";
            this.MemoryControl.ReadOnly = true;
            this.MemoryControl.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MemoryControl.Size = new System.Drawing.Size(156, 225);
            this.MemoryControl.TabIndex = 8;
            this.MemoryControl.TabStop = false;
            this.MemoryControl.WordWrap = false;
            // 
            // UserControlRegisterS
            // 
            this.UserControlRegisterS.BackColor = System.Drawing.Color.White;
            this.UserControlRegisterS.Location = new System.Drawing.Point(542, 419);
            this.UserControlRegisterS.Name = "UserControlRegisterS";
            this.UserControlRegisterS.ReadOnly = true;
            this.UserControlRegisterS.RegisterName = "S";
            this.UserControlRegisterS.Size = new System.Drawing.Size(156, 20);
            this.UserControlRegisterS.TabIndex = 4;
            this.UserControlRegisterS.TabStop = false;
            this.UserControlRegisterS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UserControlRegisterA
            // 
            this.UserControlRegisterA.BackColor = System.Drawing.Color.White;
            this.UserControlRegisterA.CausesValidation = false;
            this.UserControlRegisterA.Location = new System.Drawing.Point(542, 162);
            this.UserControlRegisterA.Name = "UserControlRegisterA";
            this.UserControlRegisterA.ReadOnly = true;
            this.UserControlRegisterA.RegisterName = "A";
            this.UserControlRegisterA.Size = new System.Drawing.Size(156, 20);
            this.UserControlRegisterA.TabIndex = 2;
            this.UserControlRegisterA.TabStop = false;
            this.UserControlRegisterA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UserControlCharacterInput
            // 
            this.UserControlCharacterInput.Location = new System.Drawing.Point(6, 27);
            this.UserControlCharacterInput.Name = "UserControlCharacterInput";
            this.UserControlCharacterInput.Size = new System.Drawing.Size(1416, 20);
            this.UserControlCharacterInput.TabIndex = 0;
            this.UserControlCharacterInput.TabStop = false;
            // 
            // userControlBusData
            // 
            this.userControlBusData.BackColor = System.Drawing.SystemColors.Control;
            this.userControlBusData.Location = new System.Drawing.Point(9, 472);
            this.userControlBusData.Multiline = true;
            this.userControlBusData.Name = "userControlBusData";
            this.userControlBusData.ReadOnly = true;
            this.userControlBusData.Size = new System.Drawing.Size(753, 5);
            this.userControlBusData.TabIndex = 9;
            // 
            // userControlBusAddress
            // 
            this.userControlBusAddress.BackColor = System.Drawing.SystemColors.Control;
            this.userControlBusAddress.Location = new System.Drawing.Point(9, 117);
            this.userControlBusAddress.Multiline = true;
            this.userControlBusAddress.Name = "userControlBusAddress";
            this.userControlBusAddress.ReadOnly = true;
            this.userControlBusAddress.Size = new System.Drawing.Size(753, 5);
            this.userControlBusAddress.TabIndex = 10;
            // 
            // UserControlRegisterI
            // 
            this.UserControlRegisterI.BackColor = System.Drawing.Color.White;
            this.UserControlRegisterI.CausesValidation = false;
            this.UserControlRegisterI.Location = new System.Drawing.Point(63, 419);
            this.UserControlRegisterI.Name = "UserControlRegisterI";
            this.UserControlRegisterI.ReadOnly = true;
            this.UserControlRegisterI.RegisterName = "I";
            this.UserControlRegisterI.Size = new System.Drawing.Size(156, 20);
            this.UserControlRegisterI.TabIndex = 11;
            this.UserControlRegisterI.TabStop = false;
            this.UserControlRegisterI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UserControlRegisterL
            // 
            this.UserControlRegisterL.BackColor = System.Drawing.Color.White;
            this.UserControlRegisterL.CausesValidation = false;
            this.UserControlRegisterL.Location = new System.Drawing.Point(26, 162);
            this.UserControlRegisterL.Name = "UserControlRegisterL";
            this.UserControlRegisterL.ReadOnly = true;
            this.UserControlRegisterL.RegisterName = "L";
            this.UserControlRegisterL.Size = new System.Drawing.Size(156, 20);
            this.UserControlRegisterL.TabIndex = 12;
            this.UserControlRegisterL.TabStop = false;
            this.UserControlRegisterL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UserControlRegisterAK
            // 
            this.UserControlRegisterAK.BackColor = System.Drawing.Color.White;
            this.UserControlRegisterAK.CausesValidation = false;
            this.UserControlRegisterAK.Location = new System.Drawing.Point(253, 271);
            this.UserControlRegisterAK.Name = "UserControlRegisterAK";
            this.UserControlRegisterAK.ReadOnly = true;
            this.UserControlRegisterAK.RegisterName = "AK";
            this.UserControlRegisterAK.Size = new System.Drawing.Size(156, 20);
            this.UserControlRegisterAK.TabIndex = 13;
            this.UserControlRegisterAK.TabStop = false;
            this.UserControlRegisterAK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panelALUView
            // 
            this.panelALUView.BackColor = System.Drawing.Color.White;
            this.panelALUView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelALUView.Location = new System.Drawing.Point(253, 293);
            this.panelALUView.Name = "panelALUView";
            this.panelALUView.Size = new System.Drawing.Size(156, 146);
            this.panelALUView.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1443, 674);
            this.Controls.Add(this.TopRightPanel);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.TopLeftPanel);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.BottomPanel);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.BottomPanel.ResumeLayout(false);
            this.tabControlOnBottomPanel.ResumeLayout(false);
            this.tabPageIO.ResumeLayout(false);
            this.tabPageIO.PerformLayout();
            this.TopLeftPanel.ResumeLayout(false);
            this.MicrocontrollerPanel.ResumeLayout(false);
            this.MicrocontrollerPanel.PerformLayout();
            this.groupBoxDebugLevel.ResumeLayout(false);
            this.groupBoxDebugLevel.PerformLayout();
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
        private System.Windows.Forms.Panel MicrocontrollerPanel;
        private System.Windows.Forms.Panel ProgramPanel;
        private System.Windows.Forms.ContextMenuStrip CodeEditorContextMenu;
        private System.Windows.Forms.ToolStripMenuItem CompileItemToolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem wytnijToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kopiujToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wklejToolStripMenuItem;
        private MachineUI.UserControlRegister UserControlRegisterA;
        private MachineUI.UserControlRegister UserControlRegisterS;
        private System.Windows.Forms.GroupBox groupBoxDebugLevel;
        private System.Windows.Forms.RadioButton radioButtonDebugTick;
        private System.Windows.Forms.RadioButton radioButtonDebugInstruction;
        private System.Windows.Forms.RadioButton radioButtonDebugProgram;
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
        private System.Windows.Forms.ToolStripMenuItem zapiszListęRozkazówToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ładujListęRozkazówToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opcjeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wykonajToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem rozkazToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem taktToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doKursoraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem przerwijToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pomocToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxManualDebug;
        private MachineUI.UserControlMemory MemoryControl;
        private System.Windows.Forms.MenuStrip unixCodeEditorMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem kodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kompilujToolStripMenuItem;
        private MachineUI.UserControlCharacterInput UserControlCharacterInput;
        private System.Windows.Forms.TabControl tabControlEditors;
        private System.Windows.Forms.TabPage tabPageCodeEditor;
        private System.Windows.Forms.TabPage tabPageInstructionList;
        private System.Windows.Forms.TabPage tabPageVariables;
        private System.Windows.Forms.TabControl tabControlOnBottomPanel;
        private System.Windows.Forms.TabPage tabPageIO;
        private System.Windows.Forms.Panel panelInstructionsList;
        private System.Windows.Forms.Panel panelInstructionsMicrocode;
        private System.Windows.Forms.Splitter splitter3;
        private MachineUI.UserControlInstructionMicrocode userControlInstructionMicrocode1;
        private MachineUI.UserControlInstructionList userControlInstructionList1;
        private MachineUI.UserControlCodeEditor UserControlCodeEditor;
        private System.Windows.Forms.ToolStripMenuItem saveContexMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveUnixToolStripMenuItem;
        private MachineUI.UserControlBus userControlBusAddress;
        private MachineUI.UserControlBus userControlBusData;
        private System.Windows.Forms.Panel panelALUView;
        private MachineUI.UserControlRegister UserControlRegisterAK;
        private MachineUI.UserControlRegister UserControlRegisterL;
        private MachineUI.UserControlRegister UserControlRegisterI;
    }
}

