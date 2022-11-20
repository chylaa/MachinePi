﻿using System;
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
using MaszynaPi.FilesHandling;
using MaszynaPi.MachineAssembler.Editors;

namespace MaszynaPi {
    public partial class Form1 : Form {
        //==================================< External DLL Functions for SenseHat >=======================================================
        //public const string SenseDLL = @"..\..\Debug\SenseHatController.dll";
        //[DllImport(SenseDLL, CallingConvention = CallingConvention.Cdecl)]
        //public static extern int GetJoystickState();
        //================================================================================================================================

        /*Sterowanie ręczne maszyną -> każdy aktywowany sygnał dodaje jego nazwę do listy, która jest następnie sortowana i
         *przekazywana do wykonania Maszynie (metoda ManualTick / Ustawianie "ActiveSignals") 
         *(możliwe wykonanie tylko kroku "Takt" przy sterowaniu ręcznym)*/
        CodeEditor codeEditor;
        ControlUnit Machine;
        Debugger Debugger;

        public Form1() {

            //Must Be First!  [TODO Handle exception with loading for Raspbian -> allow user to select diferent instruction set]
            try { InstructionLoader.LoadBaseInstructions(); } catch (InstructionLoaderException ex) {
                MessageBox.Show("Failed to load base instruction set. " + InstructionLoader.BASE_INSTRUCTION_SET_FILENAME + InstructionLoader.INSTRUCTION_SET_FILE_EXTENSION
                    + " file corrupted. Load another instruction set to use aplication. Details: " + ex.Message);
                Close();
            }
            InitializeComponent();

            codeEditor = new CodeEditor(CodeEditorTextBox);
            Debugger = new Debugger(CodeEditorTextBox);       
            Machine = new ControlUnit(Debugger);


            MemoryControl.SetItemsValueSource(Machine.GetMemoryContentHandle());
            MemoryControl.Refresh();
            UserControlRegisterA.SetSourceRegister(Machine.A);
            UserControlRegisterA.Refresh();
            UserControlRegisterS.SetSourceRegister(Machine.S);
            UserControlRegisterS.Refresh();

            // IO's
            UserControlCharacterInput.SetCharactersBufferSource(Machine.GetTextInputBufferHandle()) ;
        }
        private void Form1_Load(object sender, EventArgs e) {
            if (Environment.OSVersion.Platform != PlatformID.Unix) {
                unixCodeEditorMenuStrip.Enabled = false;
                unixCodeEditorMenuStrip.Visible = false;
            }

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
                codeEditor.SetCodeLinesFromEditorContent();
                List<uint> code = Compiler.CompileCode(codeEditor.FormatCodeForCompiler());
                Machine.SetMemoryContent(code);
                Machine.ResetRegisters();
                MemoryControl.Refresh();

            } catch (CompilerException ex) {
                MessageBox.Show(ex.Message, GetErrorType(ex.Message));
            } catch (Exception ex) {
                MessageBox.Show("Unexpected Compilation Error from " + ex.Source + ": " + ex.Message + ". Stack: " + ex.StackTrace, "Error");
            }
        }



        // Menu Bar things
        private void ładujListęRozkazówToolStripMenuItem_Click(object sender, EventArgs e) {
            string filepath = "";
            string lstFileContent = "";
            string filter = "pliki rozkazów (*.lst)|*.lst";
            if (FilesHandler.PointFileAndGetText(filter, out filepath, out lstFileContent)) {
                try {
                    uint oldAddressSpace = ArchitectureSettings.GetAddressSpace();
                    InstructionLoader.LoadInstructions(lstFileContent);
                    Machine.ExpandMemory(oldAddressSpace);
                    Machine.SetComponentsBitsizes();
                }catch(InstructionLoaderException ex) {
                    MessageBox.Show("Error while loading .lst file "+filepath+"\n"+ex.Message,"Instruction Loader Error");
                }
                    MemoryControl.Refresh();
            }
        }

        private void otwórzToolStripMenuItem_Click(object sender, EventArgs e) {
            string filepath = "";
            string fileContent = "";
            string filer = "pliki rozkazów (*.rzk)|*.lst|pliki programów (*.prg)|*.prg|wszystkie pliki (*.*)|*.*";

            if (FilesHandler.PointFileAndGetText(filer, out filepath, out fileContent)) {
                string ext = System.IO.Path.GetExtension(filepath);
                if (ext == ".rzk") { return; } // TODO
                if (ext == ".prg") { codeEditor.SetCodeEditorViewContent(fileContent); }
            }
        }

        private void CompileItemToolStrip_Click(object sender, EventArgs e) { Compile(); }
        // Code Editor unix toolstrip
        private void kompilujToolStripMenuItem_Click(object sender, EventArgs e) { Compile(); }

        private void wklejToolStripMenuItem_Click(object sender, EventArgs e) { CodeEditorTextBox.Paste(); }

        private void kopiujToolStripMenuItem_Click(object sender, EventArgs e) { CodeEditorTextBox.Copy(); }

        private void wytnijToolStripMenuItem_Click(object sender, EventArgs e) { CodeEditorTextBox.Cut(); }
    }
}
