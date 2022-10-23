
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
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.TopLeftPanel = new System.Windows.Forms.Panel();
            this.MicrocontrollerPanel = new System.Windows.Forms.Panel();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.TopRightPanel = new System.Windows.Forms.Panel();
            this.ProgramPanel = new System.Windows.Forms.Panel();
            this.programTextBox = new System.Windows.Forms.TextBox();
            this.MemoryControl = new System.Windows.Forms.ListBox();
            this.TopLeftPanel.SuspendLayout();
            this.MicrocontrollerPanel.SuspendLayout();
            this.TopRightPanel.SuspendLayout();
            this.ProgramPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 494);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(1443, 180);
            this.BottomPanel.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 491);
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
            this.TopLeftPanel.Size = new System.Drawing.Size(779, 491);
            this.TopLeftPanel.TabIndex = 2;
            // 
            // MicrocontrollerPanel
            // 
            this.MicrocontrollerPanel.Controls.Add(this.MemoryControl);
            this.MicrocontrollerPanel.Location = new System.Drawing.Point(1, 1);
            this.MicrocontrollerPanel.Name = "MicrocontrollerPanel";
            this.MicrocontrollerPanel.Size = new System.Drawing.Size(775, 487);
            this.MicrocontrollerPanel.TabIndex = 0;
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(779, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 491);
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
            this.TopRightPanel.Size = new System.Drawing.Size(661, 491);
            this.TopRightPanel.TabIndex = 4;
            // 
            // ProgramPanel
            // 
            this.ProgramPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgramPanel.Controls.Add(this.programTextBox);
            this.ProgramPanel.Location = new System.Drawing.Point(1, 2);
            this.ProgramPanel.Name = "ProgramPanel";
            this.ProgramPanel.Size = new System.Drawing.Size(657, 487);
            this.ProgramPanel.TabIndex = 0;
            // 
            // programTextBox
            // 
            this.programTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.programTextBox.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.programTextBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.programTextBox.Location = new System.Drawing.Point(16, 4);
            this.programTextBox.MaxLength = 65355;
            this.programTextBox.Multiline = true;
            this.programTextBox.Name = "programTextBox";
            this.programTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.programTextBox.Size = new System.Drawing.Size(640, 480);
            this.programTextBox.TabIndex = 0;
            this.programTextBox.WordWrap = false;
            // 
            // MemoryControl
            // 
            this.MemoryControl.ColumnWidth = 20;
            this.MemoryControl.FormattingEnabled = true;
            this.MemoryControl.Location = new System.Drawing.Point(549, 119);
            this.MemoryControl.MultiColumn = true;
            this.MemoryControl.Name = "MemoryControl";
            this.MemoryControl.Size = new System.Drawing.Size(159, 251);
            this.MemoryControl.TabIndex = 1;
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
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.TopLeftPanel.ResumeLayout(false);
            this.MicrocontrollerPanel.ResumeLayout(false);
            this.TopRightPanel.ResumeLayout(false);
            this.ProgramPanel.ResumeLayout(false);
            this.ProgramPanel.PerformLayout();
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
        private System.Windows.Forms.TextBox programTextBox;
        private System.Windows.Forms.ListBox MemoryControl;
    }
}

