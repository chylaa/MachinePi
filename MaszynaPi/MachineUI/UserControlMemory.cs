using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaszynaPi.CommonOperations;
using MaszynaPi.MachineAssembler;

namespace MaszynaPi.MachineUI {
    public partial class UserControlMemory : TextBox {

        private const int NUM_OF_SPACES = 12;
        private const int MAX_NUMBER_STRING_LENGTH = 3;

        private List<uint> UnitMemory; //handle to CentralUnit Memory object
        int SelectedIndex;

        public UserControlMemory() {
            Multiline = true;
            ReadOnly = true;
            BackColor = Color.White;
            ScrollBars = ScrollBars.None;//ScrollBars = ScrollBars.Vertical;
            WordWrap = false;
            //ContextMenu = new ContextMenu(); // "Disable" contex menu of TextBox [errors in rasbian :c]
            InitializeComponent();
            //Enter += HideCursorSetOnEnter; //Causes the scrollbar to "escape"(returns to its previous position)
            MouseDoubleClick += HandleDoubleItemClicked;
            //MouseClick += HandleDoubleItemClicked;
        }

        // Temporarily disabling and then re-enabling can disable the cursor the text box whenever it receives the focus
        void HideCursorSetOnEnter (object sender, EventArgs e) {
            Enabled = false; Enabled = true;
        }

        public void SetItemsValueSource(List<uint> unitMemory) {
            UnitMemory = unitMemory;
        }

        private void HandleDoubleItemClicked(object sender, MouseEventArgs args) {
            try {
                if (args.Button != MouseButtons.Left) return;
                SelectedIndex = GetLineFromCharIndex(GetCharIndexFromPosition(args.Location));
                if (SelectedIndex < 0 || SelectedIndex > UnitMemory.Count) return;
                if (Environment.OSVersion.Platform == PlatformID.Unix) SelectedIndex--;

                string response = UnitMemory[SelectedIndex].ToString();
                Point location = PointToClient(this.Location);
                InputDialog.ShowInputDialog(ref response, title: "PaO ", subtitle: "Aktualna wartość [" + SelectedIndex.ToString() + "]", x: location.X, y: location.Y);
                if (response == null) MessageBox.Show("Response is null");
                if (response.Length != 0)
                    UnitMemory[SelectedIndex] = Bitwise.HandleOverflow((uint)int.Parse(response));
                SetLine(SelectedIndex,CreateFormattedItem(SelectedIndex));
                (sender as TextBox).SelectionLength = 0; //try to prevent selection bug in Rasbian
            } catch (Exception ex) { throw new Exception("Menu Item Handler Error: "+ex.Message); }

        }
        private void SetLine(int index, string text) {
            var lines = Text.Split(Environment.NewLine.ToCharArray()).Where(x => !string.IsNullOrEmpty(x)).ToArray();
            lines[index] = text;
            Text = string.Join(Environment.NewLine, lines);
        }

        private string GetSpacing() {
            return new string(' ', (int)NUM_OF_SPACES);
        }
        private string CreateStringChunk(string s) {
            return (s + GetSpacing().Remove(0, s.Length)) + new string(' ', MAX_NUMBER_STRING_LENGTH - s.Length);
        }
        private string CreateFormattedItem(int i) {
            Dictionary<string, uint> avaibleInstructions = InstructionLoader.GetInstructionsNamesOpcodes();
            uint opcode = Bitwise.DecodeInstructionOpcode(UnitMemory[i]);
            uint arg = Bitwise.DecodeIntructionArgument(UnitMemory[i]);
            string name = avaibleInstructions.FirstOrDefault(x => x.Value == opcode).Key;
            string formatted = CreateStringChunk(i.ToString()) + CreateStringChunk(UnitMemory[i].ToString()) + name + " " + arg;//i.ToString() + " " + item.ToString() + " " + name + " " + arg; 
            return formatted;
        }

        protected void FormatItems() {
            Text = "";
            List<string> lines = new List<string>();
            for (int i = 0; i < UnitMemory.Count; i++)
                lines.Add(CreateFormattedItem(i));
            Text = string.Join(Environment.NewLine, lines);
        }

        public override void Refresh() {
            try { FormatItems(); } catch (Exception ex) { MessageBox.Show("Error while formatting items: " + ex.Message + ". \nStack trace: " + ex.StackTrace); }
            base.Refresh();
        }
    }
}
