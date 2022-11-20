using System;
using System.Windows.Forms;
using MaszynaPi.MachineLogic;

namespace MaszynaPi {
    public partial class FormProjectOptions : Form {

        private uint OldAddressSpace;
        public FormProjectOptions() {
            InitializeComponent();
            numericUpDownAddresBits.Maximum = (decimal)Defines.ADDRESS_BITS_MAX;
            numericUpDownAddresBits.Minimum = (decimal)Defines.ADDRESS_BITS_MIN;
            numericUpDownCodeBits.Maximum = (decimal)Defines.CODE_BITS_MAX;
            numericUpDownCodeBits.Minimum = (decimal)Defines.CODE_BITS_MIN;

            OldAddressSpace = ArchitectureSettings.GetAddressSpace();
            numericUpDownAddresBits.Value = (decimal)OldAddressSpace;
            numericUpDownCodeBits.Value = (decimal)ArchitectureSettings.GetCodeBits();
        }

        public uint GetOldAddressSpace() { return OldAddressSpace; }

        private void SetArchitecture() {
            ArchitectureSettings.SetAddressSpace((uint)numericUpDownAddresBits.Value);
            ArchitectureSettings.SetCodeBits((uint)numericUpDownCodeBits.Value);
        }
        private void SetComponents() { }
        private void SetAdresses() { }

        private void buttonOK_Click(object sender, EventArgs e) {
            SetArchitecture();
            SetComponents();
            SetAdresses();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
