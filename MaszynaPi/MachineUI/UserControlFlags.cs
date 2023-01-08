using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaszynaPi.MachineLogic.Architecture;

namespace MaszynaPi.MachineUI {
    public partial class UserControlFlags : TextBox {

        const string DIVIDER = ":";
        public string RegisterName { get; set; }

        public UserControlFlags() {
            InitializeComponent();
            this.TextAlign = HorizontalAlignment.Center;
            this.ReadOnly = true;
            this.BackColor = Color.White;
                
        }
        public Func<ALUFlags> FlagsValueRequest;

        public override void Refresh() {
            ALUFlags activeFlags = FlagsValueRequest();
            Text = RegisterName + " " + DIVIDER + " " + activeFlags.ToString();
        }

    }
}
