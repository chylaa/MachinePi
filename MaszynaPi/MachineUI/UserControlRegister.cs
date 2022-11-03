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

namespace MaszynaPi.MachineUI {

    public partial class UserControlRegister : TextBox {
        const string DIVIDER = ":";
        public string RegisterName { get; set; }
        public uint RegisterValue { get; set; }

        public UserControlRegister(){
            InitializeComponent();
            this.TextAlign = HorizontalAlignment.Center;
            this.ReadOnly = true;
            this.BackColor = Color.White;
            this.RegisterValue = Defines.DEFAULT_REG_VAL;
            this.MouseDoubleClick += ControlDoubleClick;
        }
        
        private void ControlDoubleClick(object sender, MouseEventArgs args) {
            string response = RegisterValue.ToString();
            Point location = PointToClient(this.Location);
            InputDialog.ShowInputDialog(ref response, title:"Rejestr "+RegisterName, subtitle:"Aktualna wartość", x:location.X, y:location.Y);
            if(response.Length != 0)
                RegisterValue = Arithmetics.ShrinkToWordLength((uint)int.Parse(response));
            Refresh();
        }
        protected override void OnPaint(PaintEventArgs e) {
            base.Refresh();
            base.OnPaint(e);
        }
        public override void Refresh() {
            Text = RegisterName + " " + DIVIDER + " " + RegisterValue.ToString();
            base.Refresh();
        }

    }
}

