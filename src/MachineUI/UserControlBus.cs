using System.Drawing;
using System.Windows.Forms;
using MaszynaPi.MachineLogic.Architecture;

namespace MaszynaPi.MachineUI {
    internal partial class UserControlBus : TextBox
    {
        
        private Bus UnitBus;
        private ToolTip toolTip;

        readonly static Color COLOR_ACTIVE = Color.Red;
        readonly static Color COLOR_EMPTY = SystemColors.Control;

        public UserControlBus() {
            InitializeComponent();
            BackColor = COLOR_EMPTY;
            ReadOnly = true;
            Text = string.Empty;
        }

        public void SetBusValueToolTip(ToolTip tt) {
            if (tt != null) {
                toolTip = tt;
                toolTip.SetToolTip(this, string.Empty);
            }
        }

        public void SetSourceBus(Bus sourceBus) {
            UnitBus = sourceBus;
        }

        public override void Refresh()
        {
            if (UnitBus.IsEmpty())
            {
                toolTip?.SetToolTip(this, string.Empty);
                BackColor = COLOR_EMPTY;
                BorderStyle = BorderStyle.Fixed3D;

            } 
            else
            { 
                toolTip?.SetToolTip(this, $"{UnitBus.Name} Bus: {UnitBus.GetValue()}");
                BackColor = COLOR_ACTIVE;
                BorderStyle = BorderStyle.None;
            }
            base.Refresh();
        }

    }
}
