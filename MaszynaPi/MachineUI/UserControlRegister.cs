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

namespace MaszynaPi.MachineUI {

    public enum RegisterMode { Dec, Signed, Hex, Bin}

    public partial class UserControlRegister : TextBox {
        const string DIVIDER = ":";
        public string RegisterName { get; set; }
        RegisterMode Mode { get; set; }
        private Register UnitRegister;

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
                bool isNegative = (UnitRegister.GetValue() & 1<<((int)ArchitectureSettings.GetWordBits()-1)) != 0;
                if (isNegative) { 
                    DisplayValue = "-" + ((int)(UnitRegister.GetValue() - (ArchitectureSettings.GetMaxWord() + 1) / 2)).ToString();
                } else {
                    DisplayValue = UnitRegister.GetValue().ToString(); 
                }
            } else if(Mode == RegisterMode.Hex) {
                DisplayValue = "0x"+UnitRegister.GetValue().ToString("X");
            } else if (Mode == RegisterMode.Bin) {
                DisplayValue = "0b"+Convert.ToString(UnitRegister.GetValue(), 2);
            }
            Text = RegisterName + " " + DIVIDER + " " + DisplayValue;
            base.Refresh();
        }

    }
}

