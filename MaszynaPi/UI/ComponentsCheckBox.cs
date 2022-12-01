using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaszynaPi.MachineLogic;

namespace MaszynaPi {
    public partial class ComponentsCheckBox : CheckBox {

        public Defines.Components Component { get; set; }

        public ComponentsCheckBox() {
            InitializeComponent();
        }

    }
}
