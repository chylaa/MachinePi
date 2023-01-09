using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
