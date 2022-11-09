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

            //Must Be First!  [TODO Handle exception with loading for Raspbian -> allow user to select diferent instruction set]
            try { InstructionLoader.LoadBaseInstructions(); } catch (InstructionLoaderException ex) {
                MessageBox.Show("Failed to load base instruction set. " + InstructionLoader.BASE_INSTRUCTION_SET_FILENAME + InstructionLoader.INSTRUCTION_SET_FILE_EXTENSION
                    + " file corrupted. Load another instruction set to use aplication. Details: " + ex.Message);
                Close();
            }
            InitializeComponent();
            
            Machine = new ControlUnit();
            MemoryControl.SetItemsValueSource(Machine.GetWholeMemoryContent());
            MemoryControl.Refresh();
            UserControlRegisterA.SetSourceRegister(Machine.A);
            UserControlRegisterA.Refresh();
            UserControlRegisterS.SetSourceRegister(Machine.S);
            UserControlRegisterS.Refresh();
        }
        private void Form1_Load(object sender, EventArgs e) {
            if(Environment.OSVersion.Platform != PlatformID.Unix) {
                unixCodeEditorMenuStrip.Enabled = false;
                unixCodeEditorMenuStrip.Visible = false;
            }
            
             
        }

        private void RefreshUserControls() {
            
        }

        private void UpdateMemoryContentView() {
            //this.userControlMem1.Items.Clear();
            //userControlMem1.Items.Add(Machine.GetWholeMemoryContent().ToArray());
            
        }

        private void LeftUpPanel_Paint(object sender, PaintEventArgs e) {

        }


        private void MicrocontrollerPanel_Paint(object sender, PaintEventArgs e) {

        }

        //=====================< Central Unit Manual Constrol >============================== 

        private void programToolStripMenuItem1_Click(object sender, EventArgs e) {
            try {
                Machine.ManualProgram();
                MemoryControl.Refresh();
                System.Media.SystemSounds.Exclamation.Play();
                MessageBox.Show("Program został zakończony.", "Maszyna Pi");
            } catch (CentralUnitException cEx) {
                MessageBox.Show(cEx.Message.Replace(GetErrorType(cEx.Message), ""), GetErrorType(cEx.Message));
            } catch (Exception ex) {
                MessageBox.Show("Unknown error while executing programm: " + ex.Message.Replace(GetErrorType(ex.Message), ""), GetErrorType(ex.Message));
            }

        }

        // ================ CodeEditor =======================================
        private string GetErrorType(string errMsg) {
            int starttype = errMsg.IndexOf("[");
            int endtype = errMsg.IndexOf("]");
            if (starttype != -1 && starttype < endtype) return errMsg.Substring(starttype + 1, endtype);
            return "Error";
        }

        private void Compile() {
            try {
                MachineAssembler.Editors.CodeEditor.CodeLines = CodeEditorTextBox.Text.Split(Environment.NewLine.ToCharArray()).ToList();
                List<uint> code = Compiler.CompileCode(MachineAssembler.Editors.CodeEditor.FormatCodeForCompiler());
                Machine.SetMemoryContent(code);
                Machine.ResetRegisters();
                MemoryControl.Refresh();

            } catch (CompilerException ex) {
                MessageBox.Show(ex.Message, GetErrorType(ex.Message));
            } catch (Exception ex) {
                MessageBox.Show("Unexpected Compilation Error from " + ex.Source + ": " + ex.Message + ". Stack: " + ex.StackTrace);
            }
        }

        private void CompileItemToolStrip_Click(object sender, EventArgs e) {
            Compile();
        }
        // Code Editor unix toolstrip
        private void kompilujToolStripMenuItem_Click(object sender, EventArgs e) {
            Compile();
        }

        private void wklejToolStripMenuItem_Click(object sender, EventArgs e) { CodeEditorTextBox.Paste(); }

        private void kopiujToolStripMenuItem_Click(object sender, EventArgs e) { CodeEditorTextBox.Copy(); }

        private void wytnijToolStripMenuItem_Click(object sender, EventArgs e) { CodeEditorTextBox.Cut();}


    }
}
