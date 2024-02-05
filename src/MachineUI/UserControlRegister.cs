using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MaszynaPi.MachineLogic;
using MaszynaPi.CommonOperations;
using MaszynaPi.MachineLogic.Architecture;
using MaszynaPi.MachineAssembler;
using System.Text.RegularExpressions;

namespace MaszynaPi.MachineUI 
{
    internal partial class UserControlRegister : TextBox 
    {
        public enum ValueMode { Dec, Signed, Hex, Bin}
        public enum RegisterType { Basic, Instruction, Interruption, Flag }
        
        const char DIVIDER = ':';
        ValueMode Mode { get; set; } = ValueMode.Dec;
        RegisterType Type { get; set; } = RegisterType.Basic;

        public string RegisterName { get; set; }
        private Register UnitRegister { get; set; }
        private Timer RefreshTimer { get; set; } = null;
        private int PreviousValue { get; set; } = -1;

        public UserControlRegister(){
            InitializeComponent();
            TextAlign = HorizontalAlignment.Center;
            ReadOnly = true;
            BackColor = Color.White;
            MouseDoubleClick += ControlDoubleClick;
        }

        public void SetSourceRegister(Register sourceReg) {
            UnitRegister = sourceReg;
        }

        public void SetDisplayMode(ValueMode mode) {
            Mode = mode;
        }
        public void SetRegisterType(RegisterType type) {
            Type = type;
        }
        
        private void ControlDoubleClick(object sender, MouseEventArgs args) {
            string response = UnitRegister.GetValue().ToString();
            Point location = PointToClient(Location);
            InputDialog.ShowInputDialog(ref response, title:"Register "+RegisterName, subtitle:"Current value", x:location.X, y:location.Y);
            if (response.Length != 0)
                UnitRegister.SetValue((uint)int.Parse(response));
            Refresh();
        }

        public override void Refresh() {
            uint registerValue;
            if (PreviousValue == (registerValue = UnitRegister.GetValue()))
                return; // no need to refresh

            string DisplayValue = null;
            switch (Mode)
            {
                case ValueMode.Dec:
                    DisplayValue = registerValue.ToString();
                    break;
                case ValueMode.Signed:
                    var signed = Bitwise.ConvertToSigned(registerValue, ArchitectureSettings.GetWordBits());
                    DisplayValue = signed.ToString();
                    if (signed != registerValue) DisplayValue = '-' + DisplayValue;
                    break;
                case ValueMode.Hex:
                    DisplayValue = "0x" + registerValue.ToString("X");
                    break;
                case ValueMode.Bin:
                    DisplayValue = "0b" + Convert.ToString(registerValue, 2);
                    break;
                default:
                    goto case ValueMode.Dec;
            }

            switch (Type)
            {
                case RegisterType.Basic:
                case RegisterType.Interruption:
                    Text = $"{RegisterName} {DIVIDER} {DisplayValue}";
                    break;
                case RegisterType.Flag:
                    if (registerValue == 0) goto case RegisterType.Basic;
                    else Text = $"{RegisterName} {DIVIDER} {(ALUFlags)(registerValue)} ({DisplayValue})"; 
                    break;
                case RegisterType.Instruction:
                    uint opcode = Bitwise.DecodeInstructionOpcode(registerValue);
                    uint arg = Bitwise.DecodeIntructionArgument(registerValue);
                    string name = InstructionLoader.GetInstructionsNamesOpcodes().FirstOrDefault(x => x.Value == opcode).Key;
                    Text = $"{RegisterName} {DIVIDER} {DisplayValue} ( {name} {arg} )";
                    break;
                default:
                    goto case RegisterType.Basic;
            }             
            
            PreviousValue = (int)registerValue;
            base.Refresh();
        }

        public void StartRefreshing(int interval = 1000) {
            if (Type == RegisterType.Interruption)
            {
                RefreshTimer?.Dispose();
                RefreshTimer = new Timer { Interval = interval };
                RefreshTimer.Tick += new EventHandler(RefreshControl);
                RefreshTimer.Start();
            }
        }

        private void RefreshControl(object sender, EventArgs e) {
            Refresh();
        }

    }
}

