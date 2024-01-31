
namespace MaszynaPi {
    partial class FormProjectOptions {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageArchitecture = new System.Windows.Forms.TabPage();
            this.groupBoxMachineWord = new System.Windows.Forms.GroupBox();
            this.numericUpDownCodeBits = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAddresBits = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.labelAdrBits = new System.Windows.Forms.Label();
            this.tabPageComponents = new System.Windows.Forms.TabPage();
            this.groupBoxComponents = new System.Windows.Forms.GroupBox();
            this.componentsCheckBoxExtendedIO = new MaszynaPi.ComponentsCheckBox();
            this.componentsCheckBoxALUIncDec = new MaszynaPi.ComponentsCheckBox();
            this.componentsCheckBoxALULogical = new MaszynaPi.ComponentsCheckBox();
            this.componentsCheckBoxALUExtended = new MaszynaPi.ComponentsCheckBox();
            this.componentsCheckBoxStack = new MaszynaPi.ComponentsCheckBox();
            this.componentsCheckBoxRegisterX = new MaszynaPi.ComponentsCheckBox();
            this.componentsCheckBoxRegisterY = new MaszynaPi.ComponentsCheckBox();
            this.componentsCheckBoxINT = new MaszynaPi.ComponentsCheckBox();
            this.componentsCheckBoxIO = new MaszynaPi.ComponentsCheckBox();
            this.componentsCheckBoxExtendedFlags = new MaszynaPi.ComponentsCheckBox();
            this.componentsCheckBoxBusConnection = new MaszynaPi.ComponentsCheckBox();
            this.groupBoxArchitectureType = new System.Windows.Forms.GroupBox();
            this.architectureRadioButtonL = new MaszynaPi.ArchitectureRadioButton();
            this.architectureRadioButtonEW = new MaszynaPi.ArchitectureRadioButton();
            this.architectureRadioButtonPI = new MaszynaPi.ArchitectureRadioButton();
            this.architectureRadioButtonWp = new MaszynaPi.ArchitectureRadioButton();
            this.architectureRadioButtonW = new MaszynaPi.ArchitectureRadioButton();
            this.tabPageAdresses = new System.Windows.Forms.TabPage();
            this.groupBoxIODevices = new System.Windows.Forms.GroupBox();
            this.groupBoxINTProc = new System.Windows.Forms.GroupBox();
            this.textBoxINT4Addr = new System.Windows.Forms.TextBox();
            this.textBoxINT3Addr = new System.Windows.Forms.TextBox();
            this.textBoxINT2Addr = new System.Windows.Forms.TextBox();
            this.textBoxINT1Addr = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPagePaths = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxBaseInst = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxPathMatrix = new System.Windows.Forms.TextBox();
            this.textBoxPathJoystick = new System.Windows.Forms.TextBox();
            this.textBoxPathSensors = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.tabControl.SuspendLayout();
            this.tabPageArchitecture.SuspendLayout();
            this.groupBoxMachineWord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCodeBits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAddresBits)).BeginInit();
            this.tabPageComponents.SuspendLayout();
            this.groupBoxComponents.SuspendLayout();
            this.groupBoxArchitectureType.SuspendLayout();
            this.tabPageAdresses.SuspendLayout();
            this.groupBoxIODevices.SuspendLayout();
            this.groupBoxINTProc.SuspendLayout();
            this.tabPagePaths.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(148, 387);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(107, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(276, 387);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(109, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageArchitecture);
            this.tabControl.Controls.Add(this.tabPageComponents);
            this.tabControl.Controls.Add(this.tabPageAdresses);
            this.tabControl.Controls.Add(this.tabPagePaths);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(506, 369);
            this.tabControl.TabIndex = 2;
            // 
            // tabPageArchitecture
            // 
            this.tabPageArchitecture.Controls.Add(this.groupBoxMachineWord);
            this.tabPageArchitecture.Location = new System.Drawing.Point(4, 22);
            this.tabPageArchitecture.Name = "tabPageArchitecture";
            this.tabPageArchitecture.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageArchitecture.Size = new System.Drawing.Size(498, 343);
            this.tabPageArchitecture.TabIndex = 0;
            this.tabPageArchitecture.Text = "Architecture";
            this.tabPageArchitecture.UseVisualStyleBackColor = true;
            // 
            // groupBoxMachineWord
            // 
            this.groupBoxMachineWord.Controls.Add(this.numericUpDownCodeBits);
            this.groupBoxMachineWord.Controls.Add(this.numericUpDownAddresBits);
            this.groupBoxMachineWord.Controls.Add(this.label2);
            this.groupBoxMachineWord.Controls.Add(this.labelAdrBits);
            this.groupBoxMachineWord.Location = new System.Drawing.Point(34, 28);
            this.groupBoxMachineWord.Name = "groupBoxMachineWord";
            this.groupBoxMachineWord.Size = new System.Drawing.Size(421, 103);
            this.groupBoxMachineWord.TabIndex = 0;
            this.groupBoxMachineWord.TabStop = false;
            this.groupBoxMachineWord.Text = "Computer word";
            // 
            // numericUpDownCodeBits
            // 
            this.numericUpDownCodeBits.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericUpDownCodeBits.Location = new System.Drawing.Point(185, 65);
            this.numericUpDownCodeBits.Name = "numericUpDownCodeBits";
            this.numericUpDownCodeBits.Size = new System.Drawing.Size(35, 16);
            this.numericUpDownCodeBits.TabIndex = 3;
            // 
            // numericUpDownAddresBits
            // 
            this.numericUpDownAddresBits.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericUpDownAddresBits.InterceptArrowKeys = false;
            this.numericUpDownAddresBits.Location = new System.Drawing.Point(185, 34);
            this.numericUpDownAddresBits.Name = "numericUpDownAddresBits";
            this.numericUpDownAddresBits.Size = new System.Drawing.Size(35, 16);
            this.numericUpDownAddresBits.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Code bits";
            // 
            // labelAdrBits
            // 
            this.labelAdrBits.AutoSize = true;
            this.labelAdrBits.Location = new System.Drawing.Point(55, 33);
            this.labelAdrBits.Name = "labelAdrBits";
            this.labelAdrBits.Size = new System.Drawing.Size(64, 13);
            this.labelAdrBits.TabIndex = 0;
            this.labelAdrBits.Text = "Address bits";
            // 
            // tabPageComponents
            // 
            this.tabPageComponents.Controls.Add(this.groupBoxComponents);
            this.tabPageComponents.Controls.Add(this.groupBoxArchitectureType);
            this.tabPageComponents.Location = new System.Drawing.Point(4, 22);
            this.tabPageComponents.Name = "tabPageComponents";
            this.tabPageComponents.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageComponents.Size = new System.Drawing.Size(498, 343);
            this.tabPageComponents.TabIndex = 1;
            this.tabPageComponents.Text = "Components";
            this.tabPageComponents.UseVisualStyleBackColor = true;
            // 
            // groupBoxComponents
            // 
            this.groupBoxComponents.Controls.Add(this.componentsCheckBoxExtendedIO);
            this.groupBoxComponents.Controls.Add(this.componentsCheckBoxALUIncDec);
            this.groupBoxComponents.Controls.Add(this.componentsCheckBoxALULogical);
            this.groupBoxComponents.Controls.Add(this.componentsCheckBoxALUExtended);
            this.groupBoxComponents.Controls.Add(this.componentsCheckBoxStack);
            this.groupBoxComponents.Controls.Add(this.componentsCheckBoxRegisterX);
            this.groupBoxComponents.Controls.Add(this.componentsCheckBoxRegisterY);
            this.groupBoxComponents.Controls.Add(this.componentsCheckBoxINT);
            this.groupBoxComponents.Controls.Add(this.componentsCheckBoxIO);
            this.groupBoxComponents.Controls.Add(this.componentsCheckBoxExtendedFlags);
            this.groupBoxComponents.Controls.Add(this.componentsCheckBoxBusConnection);
            this.groupBoxComponents.Location = new System.Drawing.Point(15, 146);
            this.groupBoxComponents.Name = "groupBoxComponents";
            this.groupBoxComponents.Size = new System.Drawing.Size(466, 186);
            this.groupBoxComponents.TabIndex = 1;
            this.groupBoxComponents.TabStop = false;
            // 
            // componentsCheckBoxExtendedIO
            // 
            this.componentsCheckBoxExtendedIO.AutoSize = true;
            this.componentsCheckBoxExtendedIO.Location = new System.Drawing.Point(127, 152);
            this.componentsCheckBoxExtendedIO.Name = "componentsCheckBoxExtendedIO";
            this.componentsCheckBoxExtendedIO.Size = new System.Drawing.Size(172, 17);
            this.componentsCheckBoxExtendedIO.TabIndex = 21;
            this.componentsCheckBoxExtendedIO.Text = "Extended input/output devices";
            this.componentsCheckBoxExtendedIO.UseVisualStyleBackColor = true;
            // 
            // componentsCheckBoxALUIncDec
            // 
            this.componentsCheckBoxALUIncDec.AutoSize = true;
            this.componentsCheckBoxALUIncDec.Location = new System.Drawing.Point(25, 50);
            this.componentsCheckBoxALUIncDec.Name = "componentsCheckBoxALUIncDec";
            this.componentsCheckBoxALUIncDec.Size = new System.Drawing.Size(254, 17);
            this.componentsCheckBoxALUIncDec.TabIndex = 20;
            this.componentsCheckBoxALUIncDec.Text = "Accumulator incrementation and decrementation";
            this.componentsCheckBoxALUIncDec.UseVisualStyleBackColor = true;
            // 
            // componentsCheckBoxALULogical
            // 
            this.componentsCheckBoxALULogical.AutoSize = true;
            this.componentsCheckBoxALULogical.Location = new System.Drawing.Point(25, 73);
            this.componentsCheckBoxALULogical.Name = "componentsCheckBoxALULogical";
            this.componentsCheckBoxALULogical.Size = new System.Drawing.Size(134, 17);
            this.componentsCheckBoxALULogical.TabIndex = 19;
            this.componentsCheckBoxALULogical.Text = "Logic operators in ALU";
            this.componentsCheckBoxALULogical.UseVisualStyleBackColor = true;
            // 
            // componentsCheckBoxALUExtended
            // 
            this.componentsCheckBoxALUExtended.AutoSize = true;
            this.componentsCheckBoxALUExtended.Location = new System.Drawing.Point(25, 96);
            this.componentsCheckBoxALUExtended.Name = "componentsCheckBoxALUExtended";
            this.componentsCheckBoxALUExtended.Size = new System.Drawing.Size(206, 17);
            this.componentsCheckBoxALUExtended.TabIndex = 18;
            this.componentsCheckBoxALUExtended.Text = "Extended arithmetic operations in ALU";
            this.componentsCheckBoxALUExtended.UseVisualStyleBackColor = true;
            // 
            // componentsCheckBoxStack
            // 
            this.componentsCheckBoxStack.AutoSize = true;
            this.componentsCheckBoxStack.Location = new System.Drawing.Point(25, 119);
            this.componentsCheckBoxStack.Name = "componentsCheckBoxStack";
            this.componentsCheckBoxStack.Size = new System.Drawing.Size(97, 17);
            this.componentsCheckBoxStack.TabIndex = 17;
            this.componentsCheckBoxStack.Text = "Stack handling";
            this.componentsCheckBoxStack.UseVisualStyleBackColor = true;
            // 
            // componentsCheckBoxRegisterX
            // 
            this.componentsCheckBoxRegisterX.AutoSize = true;
            this.componentsCheckBoxRegisterX.Location = new System.Drawing.Point(329, 27);
            this.componentsCheckBoxRegisterX.Name = "componentsCheckBoxRegisterX";
            this.componentsCheckBoxRegisterX.Size = new System.Drawing.Size(75, 17);
            this.componentsCheckBoxRegisterX.TabIndex = 16;
            this.componentsCheckBoxRegisterX.Text = "Register X";
            this.componentsCheckBoxRegisterX.UseVisualStyleBackColor = true;
            // 
            // componentsCheckBoxRegisterY
            // 
            this.componentsCheckBoxRegisterY.AutoSize = true;
            this.componentsCheckBoxRegisterY.Location = new System.Drawing.Point(329, 50);
            this.componentsCheckBoxRegisterY.Name = "componentsCheckBoxRegisterY";
            this.componentsCheckBoxRegisterY.Size = new System.Drawing.Size(75, 17);
            this.componentsCheckBoxRegisterY.TabIndex = 15;
            this.componentsCheckBoxRegisterY.Text = "Register Y";
            this.componentsCheckBoxRegisterY.UseVisualStyleBackColor = true;
            // 
            // componentsCheckBoxINT
            // 
            this.componentsCheckBoxINT.AutoSize = true;
            this.componentsCheckBoxINT.Location = new System.Drawing.Point(329, 73);
            this.componentsCheckBoxINT.Name = "componentsCheckBoxINT";
            this.componentsCheckBoxINT.Size = new System.Drawing.Size(84, 17);
            this.componentsCheckBoxINT.TabIndex = 14;
            this.componentsCheckBoxINT.Text = "Interruptions";
            this.componentsCheckBoxINT.UseVisualStyleBackColor = true;
            // 
            // componentsCheckBoxIO
            // 
            this.componentsCheckBoxIO.AutoSize = true;
            this.componentsCheckBoxIO.Location = new System.Drawing.Point(329, 96);
            this.componentsCheckBoxIO.Name = "componentsCheckBoxIO";
            this.componentsCheckBoxIO.Size = new System.Drawing.Size(87, 17);
            this.componentsCheckBoxIO.TabIndex = 13;
            this.componentsCheckBoxIO.Text = "Input/Output";
            this.componentsCheckBoxIO.UseVisualStyleBackColor = true;
            // 
            // componentsCheckBoxExtendedFlags
            // 
            this.componentsCheckBoxExtendedFlags.AutoSize = true;
            this.componentsCheckBoxExtendedFlags.Location = new System.Drawing.Point(329, 119);
            this.componentsCheckBoxExtendedFlags.Name = "componentsCheckBoxExtendedFlags";
            this.componentsCheckBoxExtendedFlags.Size = new System.Drawing.Size(97, 17);
            this.componentsCheckBoxExtendedFlags.TabIndex = 12;
            this.componentsCheckBoxExtendedFlags.Text = "Additional flags";
            this.componentsCheckBoxExtendedFlags.UseVisualStyleBackColor = true;
            // 
            // componentsCheckBoxBusConnection
            // 
            this.componentsCheckBoxBusConnection.AutoSize = true;
            this.componentsCheckBoxBusConnection.Location = new System.Drawing.Point(25, 27);
            this.componentsCheckBoxBusConnection.Name = "componentsCheckBoxBusConnection";
            this.componentsCheckBoxBusConnection.Size = new System.Drawing.Size(124, 17);
            this.componentsCheckBoxBusConnection.TabIndex = 11;
            this.componentsCheckBoxBusConnection.Text = "Inter-Bus connection";
            this.componentsCheckBoxBusConnection.UseVisualStyleBackColor = true;
            // 
            // groupBoxArchitectureType
            // 
            this.groupBoxArchitectureType.Controls.Add(this.architectureRadioButtonL);
            this.groupBoxArchitectureType.Controls.Add(this.architectureRadioButtonEW);
            this.groupBoxArchitectureType.Controls.Add(this.architectureRadioButtonPI);
            this.groupBoxArchitectureType.Controls.Add(this.architectureRadioButtonWp);
            this.groupBoxArchitectureType.Controls.Add(this.architectureRadioButtonW);
            this.groupBoxArchitectureType.Location = new System.Drawing.Point(14, 17);
            this.groupBoxArchitectureType.Name = "groupBoxArchitectureType";
            this.groupBoxArchitectureType.Size = new System.Drawing.Size(468, 110);
            this.groupBoxArchitectureType.TabIndex = 0;
            this.groupBoxArchitectureType.TabStop = false;
            this.groupBoxArchitectureType.Text = "Type";
            // 
            // architectureRadioButtonL
            // 
            this.architectureRadioButtonL.AutoSize = true;
            this.architectureRadioButtonL.Location = new System.Drawing.Point(330, 30);
            this.architectureRadioButtonL.Name = "architectureRadioButtonL";
            this.architectureRadioButtonL.Size = new System.Drawing.Size(31, 17);
            this.architectureRadioButtonL.TabIndex = 9;
            this.architectureRadioButtonL.TabStop = true;
            this.architectureRadioButtonL.Text = "L";
            this.architectureRadioButtonL.UseVisualStyleBackColor = true;
            // 
            // architectureRadioButtonEW
            // 
            this.architectureRadioButtonEW.AutoSize = true;
            this.architectureRadioButtonEW.Location = new System.Drawing.Point(330, 68);
            this.architectureRadioButtonEW.Name = "architectureRadioButtonEW";
            this.architectureRadioButtonEW.Size = new System.Drawing.Size(43, 17);
            this.architectureRadioButtonEW.TabIndex = 8;
            this.architectureRadioButtonEW.TabStop = true;
            this.architectureRadioButtonEW.Text = "EW";
            this.architectureRadioButtonEW.UseVisualStyleBackColor = true;
            // 
            // architectureRadioButtonPI
            // 
            this.architectureRadioButtonPI.AutoSize = true;
            this.architectureRadioButtonPI.Location = new System.Drawing.Point(190, 43);
            this.architectureRadioButtonPI.Name = "architectureRadioButtonPI";
            this.architectureRadioButtonPI.Size = new System.Drawing.Size(35, 17);
            this.architectureRadioButtonPI.TabIndex = 7;
            this.architectureRadioButtonPI.TabStop = true;
            this.architectureRadioButtonPI.Text = "PI";
            this.architectureRadioButtonPI.UseVisualStyleBackColor = true;
            // 
            // architectureRadioButtonWp
            // 
            this.architectureRadioButtonWp.AutoSize = true;
            this.architectureRadioButtonWp.Location = new System.Drawing.Point(26, 68);
            this.architectureRadioButtonWp.Name = "architectureRadioButtonWp";
            this.architectureRadioButtonWp.Size = new System.Drawing.Size(42, 17);
            this.architectureRadioButtonWp.TabIndex = 6;
            this.architectureRadioButtonWp.TabStop = true;
            this.architectureRadioButtonWp.Text = "W+";
            this.architectureRadioButtonWp.UseVisualStyleBackColor = true;
            // 
            // architectureRadioButtonW
            // 
            this.architectureRadioButtonW.AutoSize = true;
            this.architectureRadioButtonW.Location = new System.Drawing.Point(26, 30);
            this.architectureRadioButtonW.Name = "architectureRadioButtonW";
            this.architectureRadioButtonW.Size = new System.Drawing.Size(36, 17);
            this.architectureRadioButtonW.TabIndex = 5;
            this.architectureRadioButtonW.TabStop = true;
            this.architectureRadioButtonW.Text = "W";
            this.architectureRadioButtonW.UseVisualStyleBackColor = true;
            // 
            // tabPageAdresses
            // 
            this.tabPageAdresses.Controls.Add(this.groupBoxIODevices);
            this.tabPageAdresses.Controls.Add(this.groupBoxINTProc);
            this.tabPageAdresses.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdresses.Name = "tabPageAdresses";
            this.tabPageAdresses.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdresses.Size = new System.Drawing.Size(498, 343);
            this.tabPageAdresses.TabIndex = 2;
            this.tabPageAdresses.Text = "Addresses";
            this.tabPageAdresses.UseVisualStyleBackColor = true;
            // 
            // groupBoxIODevices
            // 
            this.groupBoxIODevices.Controls.Add(this.textBox9);
            this.groupBoxIODevices.Controls.Add(this.textBox8);
            this.groupBoxIODevices.Controls.Add(this.textBox7);
            this.groupBoxIODevices.Controls.Add(this.textBox6);
            this.groupBoxIODevices.Controls.Add(this.textBox5);
            this.groupBoxIODevices.Controls.Add(this.textBox4);
            this.groupBoxIODevices.Controls.Add(this.label17);
            this.groupBoxIODevices.Controls.Add(this.label18);
            this.groupBoxIODevices.Controls.Add(this.label19);
            this.groupBoxIODevices.Controls.Add(this.label20);
            this.groupBoxIODevices.Controls.Add(this.label21);
            this.groupBoxIODevices.Controls.Add(this.label16);
            this.groupBoxIODevices.Controls.Add(this.label15);
            this.groupBoxIODevices.Controls.Add(this.label14);
            this.groupBoxIODevices.Controls.Add(this.label12);
            this.groupBoxIODevices.Controls.Add(this.label13);
            this.groupBoxIODevices.Location = new System.Drawing.Point(14, 175);
            this.groupBoxIODevices.Name = "groupBoxIODevices";
            this.groupBoxIODevices.Size = new System.Drawing.Size(465, 154);
            this.groupBoxIODevices.TabIndex = 1;
            this.groupBoxIODevices.TabStop = false;
            this.groupBoxIODevices.Text = "IO Devices";
            // 
            // groupBoxINTProc
            // 
            this.groupBoxINTProc.Controls.Add(this.textBoxINT4Addr);
            this.groupBoxINTProc.Controls.Add(this.textBoxINT3Addr);
            this.groupBoxINTProc.Controls.Add(this.textBoxINT2Addr);
            this.groupBoxINTProc.Controls.Add(this.textBoxINT1Addr);
            this.groupBoxINTProc.Controls.Add(this.label7);
            this.groupBoxINTProc.Controls.Add(this.label6);
            this.groupBoxINTProc.Controls.Add(this.label5);
            this.groupBoxINTProc.Controls.Add(this.label4);
            this.groupBoxINTProc.Controls.Add(this.label3);
            this.groupBoxINTProc.Controls.Add(this.label1);
            this.groupBoxINTProc.Location = new System.Drawing.Point(16, 12);
            this.groupBoxINTProc.Name = "groupBoxINTProc";
            this.groupBoxINTProc.Size = new System.Drawing.Size(463, 144);
            this.groupBoxINTProc.TabIndex = 0;
            this.groupBoxINTProc.TabStop = false;
            this.groupBoxINTProc.Text = "Interrupt Handlers";
            // 
            // textBoxINT4Addr
            // 
            this.textBoxINT4Addr.Location = new System.Drawing.Point(156, 115);
            this.textBoxINT4Addr.Name = "textBoxINT4Addr";
            this.textBoxINT4Addr.Size = new System.Drawing.Size(82, 20);
            this.textBoxINT4Addr.TabIndex = 9;
            this.textBoxINT4Addr.Text = "4";
            this.textBoxINT4Addr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxINT3Addr
            // 
            this.textBoxINT3Addr.Location = new System.Drawing.Point(156, 93);
            this.textBoxINT3Addr.Name = "textBoxINT3Addr";
            this.textBoxINT3Addr.Size = new System.Drawing.Size(82, 20);
            this.textBoxINT3Addr.TabIndex = 8;
            this.textBoxINT3Addr.Text = "3";
            this.textBoxINT3Addr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxINT2Addr
            // 
            this.textBoxINT2Addr.Location = new System.Drawing.Point(156, 70);
            this.textBoxINT2Addr.Name = "textBoxINT2Addr";
            this.textBoxINT2Addr.Size = new System.Drawing.Size(82, 20);
            this.textBoxINT2Addr.TabIndex = 7;
            this.textBoxINT2Addr.Text = "2";
            this.textBoxINT2Addr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxINT1Addr
            // 
            this.textBoxINT1Addr.Location = new System.Drawing.Point(156, 47);
            this.textBoxINT1Addr.Name = "textBoxINT1Addr";
            this.textBoxINT1Addr.Size = new System.Drawing.Size(82, 20);
            this.textBoxINT1Addr.TabIndex = 6;
            this.textBoxINT1Addr.Text = "1";
            this.textBoxINT1Addr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(58, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "4";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(58, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(58, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(58, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(154, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Memory address";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(34, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Interruption";
            // 
            // tabPagePaths
            // 
            this.tabPagePaths.Controls.Add(this.groupBox2);
            this.tabPagePaths.Controls.Add(this.groupBox1);
            this.tabPagePaths.Location = new System.Drawing.Point(4, 22);
            this.tabPagePaths.Name = "tabPagePaths";
            this.tabPagePaths.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePaths.Size = new System.Drawing.Size(498, 343);
            this.tabPagePaths.TabIndex = 3;
            this.tabPagePaths.Text = "Paths";
            this.tabPagePaths.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxBaseInst);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(15, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(462, 63);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Base instruction set";
            // 
            // textBoxBaseInst
            // 
            this.textBoxBaseInst.Location = new System.Drawing.Point(117, 26);
            this.textBoxBaseInst.Name = "textBoxBaseInst";
            this.textBoxBaseInst.Size = new System.Drawing.Size(340, 20);
            this.textBoxBaseInst.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Instructions .lst file";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxPathMatrix);
            this.groupBox1.Controls.Add(this.textBoxPathJoystick);
            this.groupBox1.Controls.Add(this.textBoxPathSensors);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(15, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(463, 129);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scripts";
            // 
            // textBoxPathMatrix
            // 
            this.textBoxPathMatrix.Location = new System.Drawing.Point(117, 100);
            this.textBoxPathMatrix.Name = "textBoxPathMatrix";
            this.textBoxPathMatrix.Size = new System.Drawing.Size(340, 20);
            this.textBoxPathMatrix.TabIndex = 6;
            // 
            // textBoxPathJoystick
            // 
            this.textBoxPathJoystick.Location = new System.Drawing.Point(117, 64);
            this.textBoxPathJoystick.Name = "textBoxPathJoystick";
            this.textBoxPathJoystick.Size = new System.Drawing.Size(340, 20);
            this.textBoxPathJoystick.TabIndex = 5;
            // 
            // textBoxPathSensors
            // 
            this.textBoxPathSensors.Location = new System.Drawing.Point(117, 29);
            this.textBoxPathSensors.Name = "textBoxPathSensors";
            this.textBoxPathSensors.Size = new System.Drawing.Size(340, 20);
            this.textBoxPathSensors.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 103);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Matrix Handler";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Sensors Handler";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Joystick Handler";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(117, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(340, 20);
            this.textBox1.TabIndex = 4;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(117, 64);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(340, 20);
            this.textBox2.TabIndex = 5;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(117, 100);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(340, 20);
            this.textBox3.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label12.Location = new System.Drawing.Point(156, 29);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 13);
            this.label12.TabIndex = 11;
            this.label12.Text = "Memory address";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label13.Location = new System.Drawing.Point(36, 29);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 13);
            this.label13.TabIndex = 10;
            this.label13.Text = "Device";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(36, 58);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 13);
            this.label14.TabIndex = 12;
            this.label14.Text = "Console input";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(36, 86);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(78, 13);
            this.label15.TabIndex = 13;
            this.label15.Text = "Console output";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(36, 116);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 13);
            this.label16.TabIndex = 14;
            this.label16.Text = "Matrix LED";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(265, 116);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(48, 13);
            this.label17.TabIndex = 19;
            this.label17.Text = "Pressure";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(265, 86);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(47, 13);
            this.label18.TabIndex = 18;
            this.label18.Text = "Humidity";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(265, 58);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(67, 13);
            this.label19.TabIndex = 17;
            this.label19.Text = "Temperature";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label20.Location = new System.Drawing.Point(375, 29);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(84, 13);
            this.label20.TabIndex = 16;
            this.label20.Text = "Memory address";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label21.Location = new System.Drawing.Point(265, 29);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(40, 13);
            this.label21.TabIndex = 15;
            this.label21.Text = "Sensor";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(158, 55);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(81, 20);
            this.textBox4.TabIndex = 20;
            this.textBox4.Text = "1";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(158, 83);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(81, 20);
            this.textBox5.TabIndex = 21;
            this.textBox5.Text = "2";
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(158, 109);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(81, 20);
            this.textBox6.TabIndex = 22;
            this.textBox6.Text = "6";
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(378, 55);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(81, 20);
            this.textBox7.TabIndex = 23;
            this.textBox7.Text = "3";
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(378, 109);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(81, 20);
            this.textBox8.TabIndex = 24;
            this.textBox8.Text = "5";
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(378, 81);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(81, 20);
            this.textBox9.TabIndex = 25;
            this.textBox9.Text = "4";
            this.textBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FormProjectOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(530, 422);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormProjectOptions";
            this.Text = "Project Options";
            this.TopMost = true;
            this.tabControl.ResumeLayout(false);
            this.tabPageArchitecture.ResumeLayout(false);
            this.groupBoxMachineWord.ResumeLayout(false);
            this.groupBoxMachineWord.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCodeBits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAddresBits)).EndInit();
            this.tabPageComponents.ResumeLayout(false);
            this.groupBoxComponents.ResumeLayout(false);
            this.groupBoxComponents.PerformLayout();
            this.groupBoxArchitectureType.ResumeLayout(false);
            this.groupBoxArchitectureType.PerformLayout();
            this.tabPageAdresses.ResumeLayout(false);
            this.groupBoxIODevices.ResumeLayout(false);
            this.groupBoxIODevices.PerformLayout();
            this.groupBoxINTProc.ResumeLayout(false);
            this.groupBoxINTProc.PerformLayout();
            this.tabPagePaths.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageArchitecture;
        private System.Windows.Forms.GroupBox groupBoxMachineWord;
        private System.Windows.Forms.NumericUpDown numericUpDownCodeBits;
        private System.Windows.Forms.NumericUpDown numericUpDownAddresBits;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelAdrBits;
        private System.Windows.Forms.TabPage tabPageComponents;
        private System.Windows.Forms.TabPage tabPageAdresses;
        private System.Windows.Forms.GroupBox groupBoxComponents;
        private System.Windows.Forms.GroupBox groupBoxArchitectureType;
        private System.Windows.Forms.GroupBox groupBoxIODevices;
        private System.Windows.Forms.GroupBox groupBoxINTProc;
        private ComponentsCheckBox componentsCheckBoxBusConnection;
        private ArchitectureRadioButton architectureRadioButtonW;
        private ArchitectureRadioButton architectureRadioButtonL;
        private ArchitectureRadioButton architectureRadioButtonEW;
        private ArchitectureRadioButton architectureRadioButtonPI;
        private ArchitectureRadioButton architectureRadioButtonWp;
        private ComponentsCheckBox componentsCheckBoxExtendedIO;
        private ComponentsCheckBox componentsCheckBoxALUIncDec;
        private ComponentsCheckBox componentsCheckBoxALULogical;
        private ComponentsCheckBox componentsCheckBoxALUExtended;
        private ComponentsCheckBox componentsCheckBoxStack;
        private ComponentsCheckBox componentsCheckBoxRegisterX;
        private ComponentsCheckBox componentsCheckBoxRegisterY;
        private ComponentsCheckBox componentsCheckBoxINT;
        private ComponentsCheckBox componentsCheckBoxIO;
        private System.Windows.Forms.TextBox textBoxINT4Addr;
        private System.Windows.Forms.TextBox textBoxINT3Addr;
        private System.Windows.Forms.TextBox textBoxINT2Addr;
        private System.Windows.Forms.TextBox textBoxINT1Addr;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private ComponentsCheckBox componentsCheckBoxExtendedFlags;
        private System.Windows.Forms.TabPage tabPagePaths;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxBaseInst;
        private System.Windows.Forms.TextBox textBoxPathMatrix;
        private System.Windows.Forms.TextBox textBoxPathJoystick;
        private System.Windows.Forms.TextBox textBoxPathSensors;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
    }
}