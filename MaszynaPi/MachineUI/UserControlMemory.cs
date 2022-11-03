﻿using System;
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

        private List<uint> UnformattedItems;
        public UserControlMemory() {
            UnformattedItems = new List<uint>();
            InitializeComponent();
            MouseDoubleClick += HandleItemDoubleClicked;
        }

        private string GetSpacing() {
            return new string(' ', (int)NUM_OF_SPACES);
        }

        private string CreateStringChunk(string s) {
            return (s + GetSpacing().Remove(0, s.Length)) + new string(' ',MAX_NUMBER_STRING_LENGTH-s.Length);
        }


        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
        }

        protected override void OnValueMemberChanged(EventArgs e) {
            base.OnValueMemberChanged(e);
            
        }

        private void HandleItemDoubleClicked(object sender, MouseEventArgs args) {
            string response = UnformattedItems[SelectedIndex].ToString();
            Point location = PointToClient(this.Location);
            InputDialog.ShowInputDialog(ref response, title: "PaO ", subtitle: "Aktualna wartość ["+SelectedIndex.ToString()+"]", x: location.X, y: location.Y);
            if (response.Length != 0)
                UnformattedItems[SelectedIndex] = Arithmetics.ShrinkToWordLength((uint)int.Parse(response));
                this.Items[SelectedIndex] = CreateFormattedItem(SelectedIndex, UnformattedItems[SelectedIndex]);
            
        }

        public void SetItems(List<uint> newitems) {
            UnformattedItems.Clear();
            foreach (var item in newitems) UnformattedItems.Add(item);
        }
        public void AddItems(List<uint> newitems) {
            foreach (var item in newitems) UnformattedItems.Add(item);
        }
        
        //private uint GetValueFromItem(object item) {
        //    string itemStr = (item.ToString());
        //    const int valuePosition = 1;
        //    var segments = itemStr.Split(' ').ToList();
        //    segments.RemoveAll(string.IsNullOrWhiteSpace);
        //    return (uint)int.Parse(segments[valuePosition]);
        //}

        private string CreateFormattedItem(int i, object item) {
            if((item is uint) == false) { throw new Exception("Error while tring to format memory content item " + item.ToString() + ". Element is not uint type."); }
            Dictionary<string, uint> avaibleInstructions = InstructionLoader.GetInstructionsNamesOpcodes();
            uint opcode = Arithmetics.DecodeInstructionOpcode((uint)item);
            uint arg = Arithmetics.DecodeIntructionArgument((uint)item);
            string name = avaibleInstructions.FirstOrDefault(x => x.Value == opcode).Key;
            string formatted = CreateStringChunk(i.ToString()) + CreateStringChunk(item.ToString()) + name + " " + arg;//i.ToString() + " " + item.ToString() + " " + name + " " + arg; 
            return formatted;
        }

        protected void FormatItems() {
            Items.Clear();
            for (int i = 0; i < UnformattedItems.Count; i++)
                Items.Add(CreateFormattedItem(i, UnformattedItems[i]));
        }

        public override void Refresh() {
            FormatItems();
            base.Refresh();
        }

    }
}
