using MaszynaPi.MachineLogic;
using System;
using System.Windows.Forms;

namespace MaszynaPi {
    public partial class FormProjectOptions : Form {
        private bool BLOCK_RADIOBTN_UPDATE = false;
        private bool BLOCK_CHECKBOX_UPDATE = false;

        private uint OldAddressSpace;
        private Defines.Components SelectedComponents;
        private Defines.Architectures SelectedArchitecture;

        public FormProjectOptions() {
            InitializeComponent();
            numericUpDownAddresBits.Maximum = (decimal)Defines.ADDRESS_BITS_MAX;
            numericUpDownAddresBits.Minimum = (decimal)Defines.ADDRESS_BITS_MIN;
            numericUpDownCodeBits.Maximum = (decimal)Defines.CODE_BITS_MAX;
            numericUpDownCodeBits.Minimum = (decimal)Defines.CODE_BITS_MIN;

            OldAddressSpace = ArchitectureSettings.GetAddressSpace();
            numericUpDownAddresBits.Value = (decimal)OldAddressSpace;
            numericUpDownCodeBits.Value = (decimal)ArchitectureSettings.GetCodeBits();

            InitializeChecksArchitectures();
            InitializeChecksComponents();

            SelectedComponents = ArchitectureSettings.GetActiveComponents();
            SelectedArchitecture = ArchitectureSettings.GetArchitecture();

            UpdateComponentsCheckBoxes();
            UpdateArchitecureTypeRadioButtons();
           
        }

        private void UpdateArchitecureTypeRadioButtons() {
            foreach (object radioButton in groupBoxArchitectureType.Controls) {
                if (typeof(ArchitectureRadioButton) == radioButton.GetType()) {
                    (radioButton as ArchitectureRadioButton).Checked = false;
                    if ((int)SelectedComponents == (int)(radioButton as ArchitectureRadioButton).Architecture)
                        (radioButton as ArchitectureRadioButton).Checked = true; //set Architecture radio button only if  coressponding componens set
                }
            }
        }

        private void UpdateComponentsCheckBoxes() {
            foreach (object checkBox in groupBoxComponents.Controls) {
                if (typeof(ComponentsCheckBox) == checkBox.GetType()) {
                    (checkBox as ComponentsCheckBox).Checked = false;
                    if (SelectedComponents.HasFlag((checkBox as ComponentsCheckBox).Component))
                        (checkBox as ComponentsCheckBox).Checked = true;
                }
            }
        }

        private void UpdateComponentsOnArchitectureChanged(object sender, EventArgs e) {
            if (BLOCK_CHECKBOX_UPDATE) return;
            SelectedArchitecture = (sender as ArchitectureRadioButton).Architecture;
            SelectedComponents = (Defines.Components)SelectedArchitecture;
            
            BLOCK_RADIOBTN_UPDATE = true;
            UpdateComponentsCheckBoxes();
            BLOCK_RADIOBTN_UPDATE = false;
        }

        private void UpdateArchitectureOnComponentChanged(object sender, EventArgs e) {
            if (BLOCK_RADIOBTN_UPDATE) return;
            SetComponents();

            BLOCK_CHECKBOX_UPDATE = true;
            UpdateArchitecureTypeRadioButtons();
            BLOCK_CHECKBOX_UPDATE = false;
        }

        public uint GetOldAddressSpace() { return OldAddressSpace; }

        private void SetArchitectureSettings() {
            ArchitectureSettings.SetAddressSpace((uint)numericUpDownAddresBits.Value);
            ArchitectureSettings.SetCodeBits((uint)numericUpDownCodeBits.Value);
        }

        private void SetComponents() {
            SelectedComponents = Defines.Components.Basic;
            foreach(object checkBox in groupBoxComponents.Controls) {
                if (typeof(ComponentsCheckBox) == checkBox.GetType())
                    if((checkBox as ComponentsCheckBox).Checked)
                        SelectedComponents |= (checkBox as ComponentsCheckBox).Component;
            }
            ArchitectureSettings.SetActiveComponents(SelectedComponents);
        }

        private void SetArchitectures() {
            SelectedArchitecture = 0;
            foreach (object radioButton in groupBoxArchitectureType.Controls) {
                if (typeof(ArchitectureRadioButton) == radioButton.GetType())
                    if ((radioButton as ArchitectureRadioButton).Checked)
                        SelectedArchitecture |= (radioButton as ArchitectureRadioButton).Architecture;
            }
            ArchitectureSettings.SetArchitecture(SelectedArchitecture);
        }


        private void SetAdresses() { }

        private void InitializeChecksComponents() {
            componentsCheckBoxBusConnection.Component = Defines.Components.BusConnection;
            componentsCheckBoxALUIncDec.Component = Defines.Components.ALUIncrementations;
            componentsCheckBoxALULogical.Component = Defines.Components.ALULogical;
            componentsCheckBoxALUExtended.Component = Defines.Components.ALUArythmetical;
            componentsCheckBoxStack.Component = Defines.Components.Stack;
            componentsCheckBoxRegisterX.Component = Defines.Components.RegisterX;
            componentsCheckBoxRegisterY.Component = Defines.Components.RegisterY;
            componentsCheckBoxINT.Component = Defines.Components.Interuptions;
            componentsCheckBoxIO.Component = Defines.Components.IO;
            componentsCheckBoxExtendedFlags.Component = Defines.Components.Flags;
            componentsCheckBoxExtendedIO.Component = Defines.Components.ExtendedIO;

            foreach (object checkBox in groupBoxComponents.Controls) {
                if (typeof(ComponentsCheckBox) == checkBox.GetType())
                    (checkBox as ComponentsCheckBox).CheckedChanged += UpdateArchitectureOnComponentChanged;
            }

        }
        private void InitializeChecksArchitectures() {
            architectureRadioButtonW.Architecture = Defines.Architectures.MachineW;
            architectureRadioButtonWp.Architecture = Defines.Architectures.MachineWp;
            architectureRadioButtonL.Architecture = Defines.Architectures.MachineL;
            architectureRadioButtonEW.Architecture = Defines.Architectures.MachineEW;
            architectureRadioButtonPI.Architecture = Defines.Architectures.MachinePI;

            foreach (object radioButton in groupBoxArchitectureType.Controls) {
                if (typeof(ArchitectureRadioButton) == radioButton.GetType())
                    (radioButton as ArchitectureRadioButton).CheckedChanged += UpdateComponentsOnArchitectureChanged;
            }
        }




        private void buttonOK_Click(object sender, EventArgs e) {
            SetArchitectureSettings();
            SetComponents();
            SetArchitectures();
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
