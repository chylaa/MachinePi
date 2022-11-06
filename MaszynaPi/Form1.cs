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
            InstructionLoader.LoadBaseInstructions(); //First!
            Machine = new ControlUnit();
            MemoryControl.SetItemsValueSource(Machine.GetWholeMemoryContent());
            MemoryControl.Refresh();
            UserControlRegisterA.SetSourceRegister(Machine.A);
            UserControlRegisterA.Refresh();
            UserControlRegisterS.SetSourceRegister(Machine.S);
            UserControlRegisterS.Refresh();
            //Testy dopiero jak dodam InstructionSetDecoder->LoadSetFromFile->BasicSet a wtedy to już i compiler i CodeEditor można xD
            //Machine.SetMemoryContent(new List<uint> { 33, 5, 0 }); 
        }
        private void Form1_Load(object sender, EventArgs e) {
            //this.Controls.Add(architectureControl);
            //architectureControl.Show();
            //architectureControl.Visible = true;
            //MemoryControl.
            
             
        }

        private void RefreshUserControls() {
            
        }

        private void UpdateMemoryContentView() {
            //this.MemoryControl.Items.Clear();
            //MemoryControl.Items.Add(Machine.GetWholeMemoryContent().ToArray());
            
        }

        private void LeftUpPanel_Paint(object sender, PaintEventArgs e) {

        }


        private void MicrocontrollerPanel_Paint(object sender, PaintEventArgs e) {

        }

        private void CompileItemToolStrip_Click(object sender, EventArgs e) {
            try {
                MachineAssembler.Editors.CodeEditor.CodeLines = CodeEditorTextBox.Text.Split(Environment.NewLine.ToCharArray()).ToList();
                List<uint> code = Compiler.CompileCode(MachineAssembler.Editors.CodeEditor.FormatCodeForCompiler());
                Machine.SetMemoryContent(code);
                MemoryControl.Refresh();
                /* //----<Test>----//
                this.Machine.SetMemoryContent(2,69);
                MemoryControl.Refresh(); //!!!! After setting internal remember to refresh!
                MessageBox.Show("Content at [5] = " + this.Machine.GetMemoryContent(5).ToString()); // If content set in view - ControlUnit Mem set wihout refreshing 
                */
            } catch (CompilerException ex) {
                MessageBox.Show("Compiler Error: " + ex.Message);
            } catch (Exception ex){
                MessageBox.Show("Unexpected Compilation Error from " + ex.Source + ": " + ex.Message + ". Stack: "+ex.StackTrace);
            }
        }

        private void wklejToolStripMenuItem_Click(object sender, EventArgs e) {
            CodeEditorTextBox.Paste();
        }
    }
}
