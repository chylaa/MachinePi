using System;
using System.Drawing;
using System.Windows.Forms;
using MaszynaPi.MachineLogic.Architecture;

namespace MaszynaPi.MachineUI {
    public partial class UserControlFlags : TextBox {

        const string DIVIDER = ":";
        public string RegisterName { get; set; }

        public UserControlFlags() {
            InitializeComponent();
            TextAlign = HorizontalAlignment.Center;
            ReadOnly = true;
            BackColor = Color.White;
                
        }
        public Func<ALUFlags> FlagsValueRequest;

        public override void Refresh() {
            ALUFlags activeFlags = FlagsValueRequest();
            Text = RegisterName + " " + DIVIDER + " " + activeFlags.ToString();
        }

    }
}
