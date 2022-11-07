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

            //Must Be First!
            try { InstructionLoader.LoadBaseInstructions(); } catch (InstructionLoaderException ex) {
                MessageBox.Show("Failed to load base instruction set. " + InstructionLoader.BASE_INSTRUCTION_SET_FILENAME + InstructionLoader.INSTRUCTION_SET_FILE_EXTENSION
                    + " file corrupted. Load another instruction set to use aplication. Details: " + ex.Message);
            }
             
            Machine = new ControlUnit();
            //MemoryControl.SetItemsValueSource(Machine.GetWholeMemoryContent());
            //MemoryControl.Refresh();
            UserControlRegisterA.SetSourceRegister(Machine.A);
            UserControlRegisterA.Refresh();
            UserControlRegisterS.SetSourceRegister(Machine.S);
            UserControlRegisterS.Refresh();
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

        // ================ CodeEditor =======================================
        private string GetErrorType(string errMsg) {
            int starttype = errMsg.IndexOf("[");
            int endtype = errMsg.IndexOf("]");
            if (starttype != 0 && starttype < endtype) return errMsg.Substring(starttype + 1, endtype);
            return "Error";
        }

        private void CompileItemToolStrip_Click(object sender, EventArgs e) {
            try {
                MachineAssembler.Editors.CodeEditor.CodeLines = CodeEditorTextBox.Text.Split(Environment.NewLine.ToCharArray()).ToList();
                List<uint> code = Compiler.CompileCode(MachineAssembler.Editors.CodeEditor.FormatCodeForCompiler());
                Machine.SetMemoryContent(code);
                //MemoryControl.Refresh();

            } catch (CompilerException ex) {
                MessageBox.Show("Compiler Error: " + ex.Message);
            } catch (Exception ex){ 
                MessageBox.Show("Unexpected Compilation Error from " + ex.Source + ": " + ex.Message + ". Stack: "+ex.StackTrace);
            }
        }

        //=====================< Central Unit Manual Constrol >============================== 
        
        private void programToolStripMenuItem1_Click(object sender, EventArgs e) {
            try {
                Machine.ManualProgram();
            } catch (CentralUnitException cEx) {
                MessageBox.Show(cEx.Message.Replace(GetErrorType(cEx.Message),""), GetErrorType(cEx.Message));
            } catch (Exception ex) {
                MessageBox.Show("Unknown error while executing programm: " + ex.Message.Replace(GetErrorType(ex.Message), ""), GetErrorType(ex.Message));
            }

        }

        private void wklejToolStripMenuItem_Click(object sender, EventArgs e) { CodeEditorTextBox.Paste(); }

        private void kopiujToolStripMenuItem_Click(object sender, EventArgs e) { CodeEditorTextBox.Copy(); }

        private void wytnijToolStripMenuItem_Click(object sender, EventArgs e) { CodeEditorTextBox.Cut();}

    }
}
