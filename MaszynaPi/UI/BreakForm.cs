using System;
using System.Windows.Forms;

namespace MaszynaPi.UI {
    public partial class BreakForm : Form {
        public BreakForm() {
            InitializeComponent();
        }
        public void ForceClose() {
            Close();
        }

        private void button1_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
