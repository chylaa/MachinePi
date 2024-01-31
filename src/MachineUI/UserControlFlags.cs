using System;
using System.Drawing;
using System.Windows.Forms;
using MaszynaPi.MachineLogic.Architecture;

namespace MaszynaPi.MachineUI 
{
    internal partial class UserControlFlags : TextBox 
    {

        const char DIVIDER = ':';
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
            Text = RegisterName + ' ' + DIVIDER + ' ' + activeFlags.ToString();
        }

    }
}
