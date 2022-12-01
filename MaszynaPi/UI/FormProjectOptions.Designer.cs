
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
            this.componentsCheckBoxExtendedFlags = new MaszynaPi.ComponentsCheckBox();
            this.componentsCheckBoxIO = new MaszynaPi.ComponentsCheckBox();
            this.componentsCheckBoxINT = new MaszynaPi.ComponentsCheckBox();
            this.componentsCheckBoxRegisterY = new MaszynaPi.ComponentsCheckBox();
            this.componentsCheckBoxRegisterX = new MaszynaPi.ComponentsCheckBox();
            this.componentsCheckBoxStack = new MaszynaPi.ComponentsCheckBox();
            this.componentsCheckBoxALUExtended = new MaszynaPi.ComponentsCheckBox();
            this.componentsCheckBoxALULogical = new MaszynaPi.ComponentsCheckBox();
            this.componentsCheckBoxALUIncDec = new MaszynaPi.ComponentsCheckBox();
            this.componentsCheckBoxExtendedIO = new MaszynaPi.ComponentsCheckBox();
            this.tabControl.SuspendLayout();
            this.tabPageArchitecture.SuspendLayout();
            this.groupBoxMachineWord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCodeBits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAddresBits)).BeginInit();
            this.tabPageComponents.SuspendLayout();
            this.groupBoxComponents.SuspendLayout();
            this.groupBoxArchitectureType.SuspendLayout();
            this.tabPageAdresses.SuspendLayout();
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
            this.buttonCancel.Text = "Anuluj";
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
            this.tabPageArchitecture.Text = "Architektura";
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
            this.groupBoxMachineWord.Text = "Słowo maszynowe";
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
            this.label2.Location = new System.Drawing.Point(27, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Liczba bitów kodu";
            // 
            // labelAdrBits
            // 
            this.labelAdrBits.AutoSize = true;
            this.labelAdrBits.Location = new System.Drawing.Point(27, 34);
            this.labelAdrBits.Name = "labelAdrBits";
            this.labelAdrBits.Size = new System.Drawing.Size(126, 13);
            this.labelAdrBits.TabIndex = 0;
            this.labelAdrBits.Text = "Liczba bitów adresowych";
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
            this.tabPageComponents.Text = "Składniki";
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
            // componentsCheckBoxBusConnection
            // 
            this.componentsCheckBoxBusConnection.AutoSize = true;
            this.componentsCheckBoxBusConnection.Location = new System.Drawing.Point(25, 27);
            this.componentsCheckBoxBusConnection.Name = "componentsCheckBoxBusConnection";
            this.componentsCheckBoxBusConnection.Size = new System.Drawing.Size(176, 17);
            this.componentsCheckBoxBusConnection.TabIndex = 11;
            this.componentsCheckBoxBusConnection.Text = "Połączenie międzymagistralowe";
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
            this.tabPageAdresses.Text = "Adresy";
            this.tabPageAdresses.UseVisualStyleBackColor = true;
            // 
            // groupBoxIODevices
            // 
            this.groupBoxIODevices.Location = new System.Drawing.Point(14, 175);
            this.groupBoxIODevices.Name = "groupBoxIODevices";
            this.groupBoxIODevices.Size = new System.Drawing.Size(465, 154);
            this.groupBoxIODevices.TabIndex = 1;
            this.groupBoxIODevices.TabStop = false;
            this.groupBoxIODevices.Text = "Urządzenia We/Wy";
            // 
            // groupBoxINTProc
            // 
            this.groupBoxINTProc.Location = new System.Drawing.Point(16, 12);
            this.groupBoxINTProc.Name = "groupBoxINTProc";
            this.groupBoxINTProc.Size = new System.Drawing.Size(463, 144);
            this.groupBoxINTProc.TabIndex = 0;
            this.groupBoxINTProc.TabStop = false;
            this.groupBoxINTProc.Text = "Procedury obsługi przerwań";
            // 
            // componentsCheckBoxExtendedFlags
            // 
            this.componentsCheckBoxExtendedFlags.AutoSize = true;
            this.componentsCheckBoxExtendedFlags.Location = new System.Drawing.Point(329, 119);
            this.componentsCheckBoxExtendedFlags.Name = "componentsCheckBoxExtendedFlags";
            this.componentsCheckBoxExtendedFlags.Size = new System.Drawing.Size(128, 17);
            this.componentsCheckBoxExtendedFlags.TabIndex = 12;
            this.componentsCheckBoxExtendedFlags.Text = "Dodatkowe znaczniki";
            this.componentsCheckBoxExtendedFlags.UseVisualStyleBackColor = true;
            // 
            // componentsCheckBoxIO
            // 
            this.componentsCheckBoxIO.AutoSize = true;
            this.componentsCheckBoxIO.Location = new System.Drawing.Point(329, 96);
            this.componentsCheckBoxIO.Name = "componentsCheckBoxIO";
            this.componentsCheckBoxIO.Size = new System.Drawing.Size(106, 17);
            this.componentsCheckBoxIO.TabIndex = 13;
            this.componentsCheckBoxIO.Text = "Wejście/Wyjście";
            this.componentsCheckBoxIO.UseVisualStyleBackColor = true;
            // 
            // componentsCheckBoxINT
            // 
            this.componentsCheckBoxINT.AutoSize = true;
            this.componentsCheckBoxINT.Location = new System.Drawing.Point(329, 73);
            this.componentsCheckBoxINT.Name = "componentsCheckBoxINT";
            this.componentsCheckBoxINT.Size = new System.Drawing.Size(78, 17);
            this.componentsCheckBoxINT.TabIndex = 14;
            this.componentsCheckBoxINT.Text = "Przerwania";
            this.componentsCheckBoxINT.UseVisualStyleBackColor = true;
            // 
            // componentsCheckBoxRegisterY
            // 
            this.componentsCheckBoxRegisterY.AutoSize = true;
            this.componentsCheckBoxRegisterY.Location = new System.Drawing.Point(329, 50);
            this.componentsCheckBoxRegisterY.Name = "componentsCheckBoxRegisterY";
            this.componentsCheckBoxRegisterY.Size = new System.Drawing.Size(69, 17);
            this.componentsCheckBoxRegisterY.TabIndex = 15;
            this.componentsCheckBoxRegisterY.Text = "Rejestr Y";
            this.componentsCheckBoxRegisterY.UseVisualStyleBackColor = true;
            // 
            // componentsCheckBoxRegisterX
            // 
            this.componentsCheckBoxRegisterX.AutoSize = true;
            this.componentsCheckBoxRegisterX.Location = new System.Drawing.Point(329, 27);
            this.componentsCheckBoxRegisterX.Name = "componentsCheckBoxRegisterX";
            this.componentsCheckBoxRegisterX.Size = new System.Drawing.Size(69, 17);
            this.componentsCheckBoxRegisterX.TabIndex = 16;
            this.componentsCheckBoxRegisterX.Text = "Rejestr X";
            this.componentsCheckBoxRegisterX.UseVisualStyleBackColor = true;
            // 
            // componentsCheckBoxStack
            // 
            this.componentsCheckBoxStack.AutoSize = true;
            this.componentsCheckBoxStack.Location = new System.Drawing.Point(25, 119);
            this.componentsCheckBoxStack.Name = "componentsCheckBoxStack";
            this.componentsCheckBoxStack.Size = new System.Drawing.Size(95, 17);
            this.componentsCheckBoxStack.TabIndex = 17;
            this.componentsCheckBoxStack.Text = "Obsługa stosu";
            this.componentsCheckBoxStack.UseVisualStyleBackColor = true;
            // 
            // componentsCheckBoxALUExtended
            // 
            this.componentsCheckBoxALUExtended.AutoSize = true;
            this.componentsCheckBoxALUExtended.Location = new System.Drawing.Point(25, 96);
            this.componentsCheckBoxALUExtended.Name = "componentsCheckBoxALUExtended";
            this.componentsCheckBoxALUExtended.Size = new System.Drawing.Size(220, 17);
            this.componentsCheckBoxALUExtended.TabIndex = 18;
            this.componentsCheckBoxALUExtended.Text = "Rozszerzone operacje arytmetczne wJAL";
            this.componentsCheckBoxALUExtended.UseVisualStyleBackColor = true;
            // 
            // componentsCheckBoxALULogical
            // 
            this.componentsCheckBoxALULogical.AutoSize = true;
            this.componentsCheckBoxALULogical.Location = new System.Drawing.Point(25, 73);
            this.componentsCheckBoxALULogical.Name = "componentsCheckBoxALULogical";
            this.componentsCheckBoxALULogical.Size = new System.Drawing.Size(143, 17);
            this.componentsCheckBoxALULogical.TabIndex = 19;
            this.componentsCheckBoxALULogical.Text = "Operacje logiczne w JAL";
            this.componentsCheckBoxALULogical.UseVisualStyleBackColor = true;
            // 
            // componentsCheckBoxALUIncDec
            // 
            this.componentsCheckBoxALUIncDec.AutoSize = true;
            this.componentsCheckBoxALUIncDec.Location = new System.Drawing.Point(25, 50);
            this.componentsCheckBoxALUIncDec.Name = "componentsCheckBoxALUIncDec";
            this.componentsCheckBoxALUIncDec.Size = new System.Drawing.Size(232, 17);
            this.componentsCheckBoxALUIncDec.TabIndex = 20;
            this.componentsCheckBoxALUIncDec.Text = "Inkrementacja i dekrementacja akumulatora";
            this.componentsCheckBoxALUIncDec.UseVisualStyleBackColor = true;
            // 
            // componentsCheckBoxExtendedIO
            // 
            this.componentsCheckBoxExtendedIO.AutoSize = true;
            this.componentsCheckBoxExtendedIO.Location = new System.Drawing.Point(127, 152);
            this.componentsCheckBoxExtendedIO.Name = "componentsCheckBoxExtendedIO";
            this.componentsCheckBoxExtendedIO.Size = new System.Drawing.Size(212, 17);
            this.componentsCheckBoxExtendedIO.TabIndex = 21;
            this.componentsCheckBoxExtendedIO.Text = "Dodatkowe urządzenia wejścia/wyjścia";
            this.componentsCheckBoxExtendedIO.UseVisualStyleBackColor = true;
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
            this.Text = "Opcje Projektu";
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
        private ComponentsCheckBox componentsCheckBoxExtendedFlags;
    }
}