using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MaszynaPi.MachineUI 
{
    internal partial class UserControlCodeEditor : TextBox 
    {
        const string SELECT_LEFT = "< ";
        const string SELECT_RIGHT = " >";

        bool BLOCK_ACTUALIZE = false;

        List<string> CodeLines;

        public UserControlCodeEditor() {
            InitializeComponent();
            ScrollBars = ScrollBars.Both;
            Multiline = true;
            TextChanged += CodeLinesChanged;
            GotFocus += CodeLinesChanged;
        }

        void CodeLinesChanged(object sender, EventArgs e) {
            if(BLOCK_ACTUALIZE==false) ActualizeCodeLines();
        }

        void ActualizeCodeLines() {
            CodeLines.Clear();
            CodeLines.AddRange(Text.Split(Environment.NewLine.ToCharArray()).ToList());
            ClearSelected();
        }

        public void SetCodeLinesHandle(List<string> editorHandle) {
            CodeLines = editorHandle;
        }

        public void SetText(string content) {
            Text = content;
        }

        public void ClearSelected() {
            Text=Text.Replace(SELECT_LEFT, "").Replace(SELECT_RIGHT, "");
            SelectionLength = 0;
        }

        public void SelectText( int pos = -1, string text="") {
            if (pos >= 0) {
                //Select(pos, text.Length);
                var Begining = Text.Substring(0, pos);
                var Selected = Text.Substring(pos);

                int selectLeftPos = Selected.IndexOf(text, StringComparison.OrdinalIgnoreCase);
                int selectRightPos = selectLeftPos + text.Length + SELECT_LEFT.Length;

                Selected = Selected.Insert(selectLeftPos, SELECT_LEFT).Insert(selectRightPos, SELECT_RIGHT);
                //var Selected = Text.Substring(pos).ToLower().Replace(text, SELECT_LEFT + text + SELECT_RIGHT);
                Text = Begining + Selected;
            }

        }

        public void SetExecutedLine(int position, string line) {
            BLOCK_ACTUALIZE = true;
            ClearSelected();
            SelectText(position, line);
            Refresh();
            BLOCK_ACTUALIZE = false;
        }
    }
}
