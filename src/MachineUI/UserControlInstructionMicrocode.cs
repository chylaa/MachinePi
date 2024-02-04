using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MaszynaPi.MachineAssembler;

namespace MaszynaPi.MachineUI 
{
    internal partial class UserControlInstructionMicrocode : TextBox 
    {
        const string SELECT_LEFT = "< ";
        const string SELECT_RIGHT = " >";

        public UserControlInstructionMicrocode() {
            InitializeComponent();
            ReadOnly = true;
            BackColor = Color.White;
        }

        public void DisplaySelectedInstructionMicrocode(string SelectedInstructionName) { 
            var instructionsMicrocode = InstructionLoader.GetInstructionsLines()[SelectedInstructionName];
            Text = string.Join(Environment.NewLine, instructionsMicrocode);
            
        }

        public void ClearSelected() {
            Text=Text.Replace(SELECT_LEFT, "").Replace(SELECT_RIGHT, "");
            SelectionLength = 0;
        }

        public void SelectText(string text="", int idx=-1) {
            if (text.Length > 0)
                Text=Text.Replace(text, SELECT_LEFT + text + SELECT_RIGHT);
                if(idx>=0 )
                    Select(idx, text.Length);
        }

        public void SelectActiveMicroinstructions(List<string> active) {
            ClearSelected();
            string line = string.Join(" ", active).TrimEnd(Defines.LINE_END, ' ').TrimStart();
            int idx = Text.IndexOf(line);
            SelectText(line, idx);
            Refresh();
            Focus();
        } 
    }
}
