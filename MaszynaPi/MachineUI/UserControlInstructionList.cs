using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaszynaPi.MachineLogic;
using MaszynaPi.MachineAssembler;

namespace MaszynaPi.MachineUI {

    public partial class UserControlInstructionList : TextBox  {
        const string ITEM_SELECTED = " ";
        string SelectedInstructionName;

        UserControlInstructionMicrocode microcodeViewHandle;

        public UserControlInstructionList() {
            InitializeComponent();
            ReadOnly = true;
            BackColor = Color.White;
            SelectedInstructionName = "";
            WordWrap = false;

            Enter += HideCursorSetOnEnter;
            MouseClick += HandleItemClicked;
        }

        // Temporarily disabling and then re-enabling can disable the cursor the text box whenever it receives the focus
        void HideCursorSetOnEnter(object sender, EventArgs e) {
            Enabled = false; Enabled = true;
        }

        public void SetMicrocodeViewHandle(UserControlInstructionMicrocode handle) {
            microcodeViewHandle = handle;
        }

        public string GetSelectedInstruction() { return SelectedInstructionName; }

        public void DisplayAvaibleInstructionsList(int selectionIdx=0) {
            var namesList = ArchitectureSettings.GetAvaibleInstructions();
            namesList[selectionIdx] = ITEM_SELECTED + namesList[selectionIdx];
            Text = string.Join(Environment.NewLine, namesList);
        }

        void SetSelectedInstructionName(int selectedIndex=0) {
            SelectedInstructionName = InstructionLoader.GetInstructionsLines().Keys.ToList()[selectedIndex];
        }

        private void HandleItemClicked(object sender, MouseEventArgs args) {
            if (args.Button != MouseButtons.Left) return;
            if (microcodeViewHandle == null) throw new Exception("[UserControl - Instruction List - Error] Instructions Microcode view Handle not set!");

            var selectedIndex = GetLineFromCharIndex(GetCharIndexFromPosition(args.Location));
            SetSelectedInstructionName(selectedIndex);
            DisplayAvaibleInstructionsList(selectedIndex);
            microcodeViewHandle.DisplaySelectedInstructionMicrocode(SelectedInstructionName);

        }

        public override void Refresh() {
            try {
                DisplayAvaibleInstructionsList();
                if (SelectedInstructionName.Length == 0) SetSelectedInstructionName();
                microcodeViewHandle.DisplaySelectedInstructionMicrocode(SelectedInstructionName);
            } catch (Exception ex) { MessageBox.Show("Error while formatting Instruction List items: " + ex.Message + ". \nStack trace: " + ex.StackTrace); }
            base.Refresh();
        }
    }
}
