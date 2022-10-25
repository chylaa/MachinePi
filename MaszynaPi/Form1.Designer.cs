
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
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.TopLeftPanel = new System.Windows.Forms.Panel();
            this.MicrocontrollerPanel = new System.Windows.Forms.Panel();
            this.MemoryControl = new System.Windows.Forms.ListBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.TopRightPanel = new System.Windows.Forms.Panel();
            this.ProgramPanel = new System.Windows.Forms.Panel();
            this.CodeEditorTextBox = new System.Windows.Forms.TextBox();
            this.CodeEditorContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CompileItemToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveItemToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.kopiujToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wytnijToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wklejToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TopLeftPanel.SuspendLayout();
            this.MicrocontrollerPanel.SuspendLayout();
            this.TopRightPanel.SuspendLayout();
            this.ProgramPanel.SuspendLayout();
            this.CodeEditorContextMenu.SuspendLayout();
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
            this.MicrocontrollerPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MicrocontrollerPanel_Paint);
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
            this.ProgramPanel.Controls.Add(this.CodeEditorTextBox);
            this.ProgramPanel.Location = new System.Drawing.Point(1, 2);
            this.ProgramPanel.Name = "ProgramPanel";
            this.ProgramPanel.Size = new System.Drawing.Size(657, 487);
            this.ProgramPanel.TabIndex = 0;
            // 
            // CodeEditorTextBox
            // 
            this.CodeEditorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CodeEditorTextBox.ContextMenuStrip = this.CodeEditorContextMenu;
            this.CodeEditorTextBox.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CodeEditorTextBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CodeEditorTextBox.Location = new System.Drawing.Point(16, 4);
            this.CodeEditorTextBox.MaxLength = 65355;
            this.CodeEditorTextBox.Multiline = true;
            this.CodeEditorTextBox.Name = "CodeEditorTextBox";
            this.CodeEditorTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.CodeEditorTextBox.Size = new System.Drawing.Size(640, 480);
            this.CodeEditorTextBox.TabIndex = 0;
            this.CodeEditorTextBox.WordWrap = false;
            // 
            // CodeEditorContextMenu
            // 
            this.CodeEditorContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CompileItemToolStrip,
            this.SaveItemToolStrip,
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
            // SaveItemToolStrip
            // 
            this.SaveItemToolStrip.Name = "SaveItemToolStrip";
            this.SaveItemToolStrip.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveItemToolStrip.Size = new System.Drawing.Size(168, 22);
            this.SaveItemToolStrip.Text = "Zapisz";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(165, 6);
            // 
            // kopiujToolStripMenuItem
            // 
            this.kopiujToolStripMenuItem.Name = "kopiujToolStripMenuItem";
            this.kopiujToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.kopiujToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.kopiujToolStripMenuItem.Text = "Kopiuj";
            // 
            // wytnijToolStripMenuItem
            // 
            this.wytnijToolStripMenuItem.Name = "wytnijToolStripMenuItem";
            this.wytnijToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.wytnijToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.wytnijToolStripMenuItem.Text = "Wytnij";
            // 
            // wklejToolStripMenuItem
            // 
            this.wklejToolStripMenuItem.Name = "wklejToolStripMenuItem";
            this.wklejToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.wklejToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.wklejToolStripMenuItem.Text = "Wklej";
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
            this.CodeEditorContextMenu.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox CodeEditorTextBox;
        private System.Windows.Forms.ListBox MemoryControl;
        private System.Windows.Forms.ContextMenuStrip CodeEditorContextMenu;
        private System.Windows.Forms.ToolStripMenuItem CompileItemToolStrip;
        private System.Windows.Forms.ToolStripMenuItem SaveItemToolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem wytnijToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kopiujToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wklejToolStripMenuItem;
    }
}

