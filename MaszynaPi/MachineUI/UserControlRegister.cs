using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        RegisterMode Mode { get; set; }
        private Register UnitRegister;

        Timer RefreshTimer = null;

        public UserControlRegister(){
            InitializeComponent();
            this.TextAlign = HorizontalAlignment.Center;
            this.ReadOnly = true;
            this.BackColor = Color.White;
            //this.RegisterValue = Defines.DEFAULT_REG_VAL;
            this.MouseDoubleClick += ControlDoubleClick;
            Mode = RegisterMode.Dec;
        }

        public void SetSourceRegister(Register sourceReg) {
            this.UnitRegister = sourceReg;
        }

        public void SetDisplayMode(RegisterMode mode) {
            Mode = mode;
        }
        
        private void ControlDoubleClick(object sender, MouseEventArgs args) {
            string response = UnitRegister.GetValue().ToString();
            Point location = PointToClient(this.Location);
            InputDialog.ShowInputDialog(ref response, title:"Register "+RegisterName, subtitle:"Current value", x:location.X, y:location.Y);
            if (response.Length != 0)
                UnitRegister.SetValue((uint)int.Parse(response));
            Refresh();
        }
        protected override void OnPaint(PaintEventArgs e) {
            base.Refresh();
            base.OnPaint(e);
        }
        public override void Refresh() {
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
            base.Refresh();
        }

        public void StartRefreshing(int interval = 1000) {
            RefreshTimer = new Timer();
            RefreshTimer.Interval = 1000; // co sekundę
            RefreshTimer.Tick += new EventHandler(RefreshControl);
            RefreshTimer.Start();
        }

        private void RefreshControl(object sender, EventArgs e) {
            Refresh();
        }

    }
}

