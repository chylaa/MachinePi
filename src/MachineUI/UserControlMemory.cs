﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MaszynaPi.CommonOperations;
using MaszynaPi.MachineAssembler;

namespace MaszynaPi.MachineUI 
{
    internal partial class UserControlMemory : TextBox 
    {

        private const int SPACING_STR_LEN = 12;
        private const int MAX_NUMBER_STRING_LENGTH = 6; // max number of digits in address index

        /// <summary>Lines of memory generated in control when <see cref="PartiallySupressRefreshing"/> enabled.</summary>
        private const int VISIBLE_MEMORY_SIZE = 18;

        private List<uint> UnitMemory; //handle to CentralUnit Memory object
        int SelectedIndex;

        /// <summary>Sets flag that indicates if <see cref="Refresh"/> overload should refresh only visible parts of memory control.</summary>
        public bool PartiallySupressRefreshing { get; set; } = false;

        public UserControlMemory() {
            Multiline = true;
            ReadOnly = true;
            BackColor = Color.White;
            ScrollBars = ScrollBars.None;//ScrollBars = ScrollBars.Vertical;
            WordWrap = false;
            //ContextMenu = new ContextMenu(); // "Disable" contex menu of TextBox [errors in rasbian]
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

                string memaddr = SelectedIndex.ToString();
                string memcontent = UnitMemory[SelectedIndex].ToString();
                Point location = PointToClient(Location);
                var result = InputDialog.ShowDoubleInputDialog(ref memaddr, ref memcontent, title: "RAM ", subtitle:"Address ", subtitle2: "Value ", x: location.X, y: location.Y);
                if (memcontent == null || memaddr == null || result == DialogResult.Cancel) return;
                SelectedIndex = (int)Bitwise.HandleOverflow(uint.Parse(memaddr), MachineLogic.ArchitectureSettings.GetAddressSpace());
                if (memcontent.Length != 0)
                    UnitMemory[SelectedIndex] = Bitwise.HandleOverflow(uint.Parse(memcontent));
                SetLine(SelectedIndex, CreateFormattedItem(SelectedIndex).TrimEnd(Environment.NewLine.ToCharArray()));
                (sender as TextBox).SelectionLength = 0; //try to prevent selection bug in Rasbian
            } catch (Exception ex) { throw new Exception("Menu Item Handler Error: "+ex.Message); }

        }
        private void SetLine(int index, string text) {
            var lines = Text.Split(Environment.NewLine.ToCharArray()).Where(x => !string.IsNullOrEmpty(x)).ToArray();
            lines[index] = text;
            Text = string.Join(Environment.NewLine, lines);
        }

        private string CreateStringChunk(uint val) {
            string s = val.ToString();
            return (s + new string(' ', SPACING_STR_LEN - s.Length) + new string(' ', MAX_NUMBER_STRING_LENGTH - s.Length));
        }
        private string CreateFormattedItem(int i) {
            Dictionary<string, uint> avaibleInstructions = InstructionLoader.GetInstructionsNamesOpcodes();
            uint opcode = Bitwise.DecodeInstructionOpcode(UnitMemory[i]);
            uint arg = Bitwise.DecodeIntructionArgument(UnitMemory[i]);
            string name = avaibleInstructions.FirstOrDefault(x => x.Value == opcode).Key;
            return $"{CreateStringChunk((uint)i)}{CreateStringChunk(UnitMemory[i])}{name} {arg}{Environment.NewLine}";//i.ToString() + " " + item.ToString() + " " + name + " " + arg; 
        }

        protected void FormatItems() {
            string lines = string.Empty;
            int stop = PartiallySupressRefreshing ? VISIBLE_MEMORY_SIZE : UnitMemory.Count;
            for (int i = 0; i < stop; i++)
                lines += (CreateFormattedItem(i));
            Text = lines;
        }

        public override void Refresh()
        {
            try { FormatItems(); } catch (Exception ex) { MessageBox.Show("Error while formatting items: " + ex.Message + ". \nStack trace: " + ex.StackTrace); }
            base.Refresh();
        }
    }
}
