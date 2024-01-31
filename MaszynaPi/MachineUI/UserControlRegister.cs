using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MaszynaPi.MachineLogic;
using MaszynaPi.CommonOperations;
using MaszynaPi.MachineLogic.Architecture;
using MaszynaPi.MachineAssembler;

namespace MaszynaPi.MachineUI {

    public enum RegisterMode { Dec, Signed, Hex, Bin, Instruction}

    public partial class UserControlRegister : TextBox {
        const string DIVIDER = ":";
        public string RegisterName { get; set; }
        RegisterMode Mode { get; set; } = RegisterMode.Dec;
        private int PreviousValue { get; set; } = -1;

        private Register UnitRegister;

        private Timer RefreshTimer = null;

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

        public void SetDisplayMode(RegisterMode mode) {
            Mode = mode;
        }
        
        private void ControlDoubleClick(object sender, MouseEventArgs args) {
            string response = UnitRegister.GetValue().ToString();
            Point location = PointToClient(Location);
            InputDialog.ShowInputDialog(ref response, title:"Register "+RegisterName, subtitle:"Current value", x:location.X, y:location.Y);
            if (response.Length != 0)
                UnitRegister.SetValue((uint)int.Parse(response));
            Refresh();
        }
        //protected override void OnPaint(PaintEventArgs e) {
        //    base.Refresh();
        //    base.OnPaint(e);
        //}
        public override void Refresh() {
            if (PreviousValue == UnitRegister.GetValue())
                return; // no need to refresh

            string DisplayValue = "";
            if (Mode == RegisterMode.Dec) {
                 DisplayValue =  UnitRegister.GetValue().ToString();
            }else if(Mode == RegisterMode.Signed) {
                var signed = Bitwise.ConvertToSigned(UnitRegister.GetValue(), ArchitectureSettings.GetWordBits());
                DisplayValue = signed.ToString();
                if (signed != UnitRegister.GetValue()) DisplayValue = "-"+DisplayValue;
            } else if(Mode == RegisterMode.Hex) {
                DisplayValue = "0x"+UnitRegister.GetValue().ToString("X");
            } else if (Mode == RegisterMode.Bin) {
                DisplayValue = "0b"+Convert.ToString(UnitRegister.GetValue(), 2);
            } else if(Mode == RegisterMode.Instruction) {
                Dictionary<string, uint> avaibleInstructions = InstructionLoader.GetInstructionsNamesOpcodes();
                uint opcode = Bitwise.DecodeInstructionOpcode(UnitRegister.GetValue());
                uint arg = Bitwise.DecodeIntructionArgument(UnitRegister.GetValue());
                string name = avaibleInstructions.FirstOrDefault(x => x.Value == opcode).Key;
                DisplayValue = UnitRegister.GetValue().ToString() + " ( " +name+" "+arg.ToString()+" )";
            }
            Text = RegisterName + " " + DIVIDER + " " + DisplayValue;
            PreviousValue = (int)UnitRegister.GetValue();
            base.Refresh();
        }

        public void StartRefreshing(int interval = 1000) {
            RefreshTimer = new Timer();
            RefreshTimer.Interval = interval;
            RefreshTimer.Tick += new EventHandler(RefreshControl);
            RefreshTimer.Start();
        }

        private void RefreshControl(object sender, EventArgs e) {
            Refresh();
        }

    }
}

