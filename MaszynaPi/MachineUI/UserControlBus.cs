using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaszynaPi.MachineLogic.Architecture;

namespace MaszynaPi.MachineUI {
    public partial class UserControlBus : TextBox {
        const string TEXT = "";
        private Bus UnitBus;
        private ToolTip toolTip = new ToolTip();

        readonly Color COLOR_ACTIVE = Color.Red;
        readonly Color COLOR_EMPTY = SystemColors.Control;

        public UserControlBus() {
            InitializeComponent();
            BackColor = SystemColors.Control;
            ReadOnly = true;
            Text = TEXT;
            toolTip.Active = true;
        }

        public void SetSourceBus(Bus sourceBus) {
            UnitBus = sourceBus;
        }

        protected override void OnPaint(PaintEventArgs e) {
            if (UnitBus.IsEmpty() == false) {
                toolTip.SetToolTip(this, UnitBus.GetValue().ToString());
                //BackColor = COLOR_ACTIVE;
                toolTip.AutomaticDelay = 0;
            } else {
                //BackColor = COLOR_EMPTY;
                toolTip.SetToolTip(this, null);
            }
            base.Refresh();
            base.OnPaint(e);
        }

    }
}
