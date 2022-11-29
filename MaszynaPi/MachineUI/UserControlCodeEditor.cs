using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaszynaPi.MachineAssembler;

namespace MaszynaPi.MachineUI {
    public partial class UserControlCodeEditor : TextBox {

        List<string> CodeLines;

        public UserControlCodeEditor() {
            InitializeComponent();
            ScrollBars = ScrollBars.Both;
            Multiline = true;
            TextChanged += CodeLinesChanged;
        }

        void CodeLinesChanged(object sender, EventArgs e) {
            ActualizeCodeLines();
        }

        void ActualizeCodeLines() {
            CodeLines.Clear();
            CodeLines.AddRange(Text.Split(Environment.NewLine.ToCharArray()).ToList());
        }

        public void SetCodeLinesHandle(List<string> editorHandle) {
            CodeLines = editorHandle;
        }

        public void SetText(string content) {
            Text = content;
        }

        public void SetExecutedLine(int position, int lineEnd) {
            Select(position, lineEnd - position);
            Focus();
        }
    }
}
