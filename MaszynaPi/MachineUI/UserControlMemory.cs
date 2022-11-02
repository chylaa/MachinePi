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

namespace MaszynaPi.MachineUI {
    public partial class UserControlMemory : ListBox {
        private const int NUM_OF_SPACES = 12;
        private const int MAX_NUMBER_STRING_LENGTH = 3;
        public UserControlMemory() {
            InitializeComponent();
        }

        private string GetSpacing() {
            return new string(' ', (int)NUM_OF_SPACES);
        }

        private string CreateStringChunk(string s) {
            return (s + GetSpacing().Remove(0, s.Length)) + new string(' ',MAX_NUMBER_STRING_LENGTH-s.Length);
        }

        protected override void OnClick(EventArgs e) {
            base.OnClick(e);
        }
        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
        }

        protected override void OnValueMemberChanged(EventArgs e) {
            base.OnValueMemberChanged(e);
            
        }
        public void SetItems(List<uint> newitems) {
            Items.Clear();
            foreach (var item in newitems) Items.Add(item);
        }
        public void AddItems(List<uint> newitems) {
            foreach (var item in newitems) Items.Add(item);
        }

        private string FormatItem(int i, object item) {
            if((item is uint) == false) { throw new Exception("Error while tring to format memory content item " + item.ToString() + ". Element is not uint type."); }
            Dictionary<string, uint> avaibleInstructions = InstructionLoader.GetInstructionsNamesOpcodes();
            uint opcode = Arithmetics.DecodeInstructionOpcode((uint)item);
            uint arg = Arithmetics.DecodeIntructionArgument((uint)item);
            string name = avaibleInstructions.FirstOrDefault(x => x.Value == opcode).Key;
            string formatted = CreateStringChunk(i.ToString()) + CreateStringChunk(item.ToString()) + name + " " + arg;//i.ToString() + " " + item.ToString() + " " + name + " " + arg; 
            return formatted;
        }

        protected void FormatItems() {
            for (int i = 0; i < Items.Count; i++)
                Items[i] = FormatItem(i, Items[i]);
        }

        public override void Refresh() {
            FormatItems();
            base.Refresh();
        }

    }
}
