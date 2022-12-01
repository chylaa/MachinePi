using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaszynaPi.MachineLogic;

namespace MaszynaPi {
    public partial class ArchitectureRadioButton : RadioButton {
        public Defines.Architectures Architecture { get; set; }

        public ArchitectureRadioButton(){
            InitializeComponent();
        }
    }
}
