using MaszynaPi.MachineLogic;
using System;
using System.Windows.Forms;

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

            InitializeChecksArchitectures();
            InitializeChecksComponents();

            Defines.Components activeComponents = ArchitectureSettings.GetActiveComponents();
            Defines.Architectures currentArchitecture = ArchitectureSettings.GetArchitecture();

            foreach (object checkBox in groupBoxComponents.Controls) {
                if (typeof(ComponentsCheckBox) == checkBox.GetType())
                    if (activeComponents.HasFlag((checkBox as ComponentsCheckBox).Component))
                        (checkBox as ComponentsCheckBox).Checked = true;
            }

            foreach (object radioButton in groupBoxArchitectureType.Controls) {
                if (typeof(ArchitectureRadioButton) == radioButton.GetType())
                    if ((int)activeComponents == (int)(radioButton as ArchitectureRadioButton).Architecture)
                        (radioButton as ArchitectureRadioButton).Checked = true; //set Architecture radio button only if  coressponding componens set
            }
            



        }

        public uint GetOldAddressSpace() { return OldAddressSpace; }

        private void SetArchitectureSettings() {
            ArchitectureSettings.SetAddressSpace((uint)numericUpDownAddresBits.Value);
            ArchitectureSettings.SetCodeBits((uint)numericUpDownCodeBits.Value);
        }

        private void SetComponents() {
            Defines.Components activatedComponents = 0;
            foreach(object checkBox in groupBoxComponents.Controls) {
                if (typeof(ComponentsCheckBox) == checkBox.GetType())
                    if((checkBox as ComponentsCheckBox).Checked)
                        activatedComponents |= (checkBox as ComponentsCheckBox).Component;
            }
            ArchitectureSettings.SetActiveComponents(activatedComponents);
        }

        private void SetArchitectures() {
            Defines.Architectures activatedArchitecture = 0;
            foreach (object radioButton in groupBoxArchitectureType.Controls) {
                if (typeof(ArchitectureRadioButton) == radioButton.GetType())
                    if ((radioButton as ArchitectureRadioButton).Checked)
                        activatedArchitecture |= (radioButton as ArchitectureRadioButton).Architecture;
            }
            ArchitectureSettings.SetArchitecture(activatedArchitecture);
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

        }
        private void InitializeChecksArchitectures() {
            architectureRadioButtonW.Architecture = Defines.Architectures.MachineW;
            architectureRadioButtonWp.Architecture = Defines.Architectures.MachineWp;
            architectureRadioButtonL.Architecture = Defines.Architectures.MachineL;
            architectureRadioButtonEW.Architecture = Defines.Architectures.MachineEW;
            architectureRadioButtonPI.Architecture = Defines.Architectures.MachinePI;
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
