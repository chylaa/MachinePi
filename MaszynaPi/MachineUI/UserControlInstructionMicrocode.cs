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
using MaszynaPi.MachineAssembler;

namespace MaszynaPi.MachineUI {

    public partial class UserControlInstructionMicrocode : TextBox {

        public UserControlInstructionMicrocode() {
            InitializeComponent();
            ReadOnly = true;
            BackColor = Color.White;
        }

        public void DisplaySelectedInstructionMicrocode(string SelectedInstructionName) { 
            var instructionsMicrocode = InstructionLoader.GetInstructionsLines()[SelectedInstructionName];
            Text = string.Join(Environment.NewLine, instructionsMicrocode);
            
        }
    }
}
