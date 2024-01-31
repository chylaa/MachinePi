using System;
using System.Windows.Forms;

namespace MaszynaPi {
    static class Program {
        /// <summary>
        /// Main entry.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try {
                Application.Run(new Form1());
            } catch (Exception ex) {
                Console.WriteLine(ex.Message + " " + ex.StackTrace);
                return;
            }
        }
    }
}
