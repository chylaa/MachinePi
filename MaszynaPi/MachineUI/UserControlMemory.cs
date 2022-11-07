using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaszynaPi.MachineAssembler.FilesHandling;
using MaszynaPi.CommonOperations;
using MaszynaPi.MachineLogic;

namespace MaszynaPi.MachineUI {
    public partial class UserControlMemory : ListBox {
        private const int NUM_OF_SPACES = 12;
        private const int MAX_NUMBER_STRING_LENGTH = 3;

        private List<uint> UnitMemory; //handle to CentralUnit Memory object
        public UserControlMemory() {
            InitializeComponent();
            MouseDoubleClick += HandleItemDoubleClicked;
        }

        private string GetSpacing() {
            return new string(' ', (int)NUM_OF_SPACES);
        }

        private string CreateStringChunk(string s) {
            return (s + GetSpacing().Remove(0, s.Length)) + new string(' ', MAX_NUMBER_STRING_LENGTH - s.Length);
        }


        private void HandleItemDoubleClicked(object sender, MouseEventArgs args) {
            string response = UnitMemory[SelectedIndex].ToString();
            Point location = PointToClient(this.Location);
            InputDialog.ShowInputDialog(ref response, title: "PaO ", subtitle: "Aktualna wartość [" + SelectedIndex.ToString() + "]", x: location.X, y: location.Y);
            if (response.Length != 0)
                UnitMemory[SelectedIndex] = Arithmetics.ShrinkToWordLength((uint)int.Parse(response));
            this.Items[SelectedIndex] = CreateFormattedItem(SelectedIndex, UnitMemory[SelectedIndex]);

        }

        public void SetItemsValueSource(List<uint> unitMemory) {
            UnitMemory = unitMemory;
            //UnitMemory.Clear();
            //foreach (var item in newitems) UnitMemory.Add(item);
        }
        //public void AddItems(List<uint> newitems) {
        //    foreach (var item in newitems) UnitMemory.Add(item);
        //}

        //private uint GetValueFromItem(object item) {
        //    string itemStr = (item.ToString());
        //    const int valuePosition = 1;
        //    var segments = itemStr.Split(' ').ToList();
        //    segments.RemoveAll(string.IsNullOrWhiteSpace);
        //    return (uint)int.Parse(segments[valuePosition]);
        //}

        private string CreateFormattedItem(int i, object item) {
            if ((item is uint) == false) { throw new Exception("Error while tring to format memory content item " + item.ToString() + ". Element is not uint type."); }
            Dictionary<string, uint> avaibleInstructions = InstructionLoader.GetInstructionsNamesOpcodes();
            uint opcode = Arithmetics.DecodeInstructionOpcode((uint)item);
            uint arg = Arithmetics.DecodeIntructionArgument((uint)item);
            string name = avaibleInstructions.FirstOrDefault(x => x.Value == opcode).Key;
            string formatted = CreateStringChunk(i.ToString()) + CreateStringChunk(item.ToString()) + name + " " + arg;//i.ToString() + " " + item.ToString() + " " + name + " " + arg; 
            return formatted;
        }

        protected void FormatItems() { 
            if(Items==null) return;
            if(Items.Count > 0) Items.Clear();
            for (int i = 0; i < UnitMemory.Count; i++) 
                Items.Add(CreateFormattedItem(i, UnitMemory[i]));
        }

        public override void Refresh() {
            try { FormatItems(); } 
            catch(Exception ex) {
                if(Environment.OSVersion.Platform != PlatformID.Unix)
                    MessageBox.Show("Error while formatting items: " + ex.Message + ". \nStack trace: " + ex.StackTrace); }
            
            base.Refresh();
        }

    }
}
