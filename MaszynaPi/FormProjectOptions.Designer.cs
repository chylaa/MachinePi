
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
            this.tabPageAdresses = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.tabPageArchitecture.SuspendLayout();
            this.groupBoxMachineWord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCodeBits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAddresBits)).BeginInit();
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
            this.tabPageComponents.Location = new System.Drawing.Point(4, 22);
            this.tabPageComponents.Name = "tabPageComponents";
            this.tabPageComponents.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageComponents.Size = new System.Drawing.Size(525, 362);
            this.tabPageComponents.TabIndex = 1;
            this.tabPageComponents.Text = "Składniki";
            this.tabPageComponents.UseVisualStyleBackColor = true;
            // 
            // tabPageAdresses
            // 
            this.tabPageAdresses.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdresses.Name = "tabPageAdresses";
            this.tabPageAdresses.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdresses.Size = new System.Drawing.Size(525, 362);
            this.tabPageAdresses.TabIndex = 2;
            this.tabPageAdresses.Text = "Adresy";
            this.tabPageAdresses.UseVisualStyleBackColor = true;
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
    }
}