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

namespace MaszynaPi.MachineUI {

    public partial class UserControlRegister : TextBox {
        const string DIVIDER = ":";
        public string RegisterName { get; set; }
        public uint RegisterValue { get; set; }

        public UserControlRegister(){
            InitializeComponent();
        }
        
        protected override void OnClick(EventArgs e) {
            base.OnClick(e);
        }
        protected override void OnPaint(PaintEventArgs e) {
            DisplayValues();
            base.OnPaint(e);
        }
        public void DisplayValues() {
            Text = RegisterName +" "+DIVIDER+" "+RegisterValue.ToString();
        }

    }
}

