using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaszynaPi.MachineLogic;

namespace MaszynaPi {
    public partial class Form1 : Form {
        //==================================< External DLL Functions for SenseHat >=======================================================
        //public const string SenseDLL = @"..\..\Debug\SenseHatController.dll";
        //[DllImport(SenseDLL, CallingConvention = CallingConvention.Cdecl)]
        //public static extern int GetJoystickState();
        //================================================================================================================================

        CentralUnit Machine;

        public Form1() {
            InitializeComponent();
            Machine = new MachineW();

        }
        private void Form1_Load(object sender, EventArgs e) {
            //this.Controls.Add(architectureControl);
            //architectureControl.Show();
            //architectureControl.Visible = true;
            MemoryControl.
            
        }

        private void LeftUpPanel_Paint(object sender, PaintEventArgs e) {

        }

        private void button1_Click_1(object sender, EventArgs e) {
            //int result = GetJoystickState();
            MessageBox.Show("C++ DLL function returns nothighxD", "CHECK");
        }
    }
}
