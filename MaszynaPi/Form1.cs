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
using MaszynaPi.MachineAssembler;
using MaszynaPi.MachineUI;
using MaszynaPi.MachineAssembler.FilesHandling;

namespace MaszynaPi {
    public partial class Form1 : Form {
        //==================================< External DLL Functions for SenseHat >=======================================================
        //public const string SenseDLL = @"..\..\Debug\SenseHatController.dll";
        //[DllImport(SenseDLL, CallingConvention = CallingConvention.Cdecl)]
        //public static extern int GetJoystickState();
        //================================================================================================================================

        /*Sterowanie ręczne maszyną -> każdy aktywowany sygnał dodaje jego nazwę do listy, która jest następnie sortowana i
         *przekazywana do wykonania Maszynie (metoda ManualTick) (możliwe wykonanie tylko kroku "Takt" przy sterowaniu ręcznym)*/
        ControlUnit Machine;

        public Form1() {
            InitializeComponent();
            InstructionLoader.LoadBaseInstructions();
            Machine = new ControlUnit();
            MemoryControl.SetItems(Machine.GetWholeMemoryContent());
            MemoryControl.Refresh();
            RegisterAControl.DisplayValues();
            //Testy dopiero jak dodam InstructionSetDecoder->LoadSetFromFile->BasicSet a wtedy to już i compiler i CodeEditor można xD
            //Machine.SetMemoryContent(new List<uint> { 33, 5, 0 }); 
        }
        private void Form1_Load(object sender, EventArgs e) {
            //this.Controls.Add(architectureControl);
            //architectureControl.Show();
            //architectureControl.Visible = true;
            //MemoryControl.
            
             
        }

        private void UpdateMemoryContentView() {
            //this.MemoryControl.Items.Clear();
            //MemoryControl.Items.Add(Machine.GetWholeMemoryContent().ToArray());
            
        }

        private void LeftUpPanel_Paint(object sender, PaintEventArgs e) {

        }

        private void button1_Click_1(object sender, EventArgs e) {
            //int result = GetJoystickState();
            MessageBox.Show("C++ DLL function returns nothighxD", "CHECK");
        }

        private void MicrocontrollerPanel_Paint(object sender, PaintEventArgs e) {

        }

        private void CompileItemToolStrip_Click(object sender, EventArgs e) {
            try {
                Compiler.ProcessCodeFromEditor(codeLines: CodeEditorTextBox.Text.Split(Environment.NewLine.ToCharArray()).ToList());
            } catch (CompilerException) {

            } catch (Exception){

            }
        }
    }
}
