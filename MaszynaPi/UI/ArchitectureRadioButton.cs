using System.Windows.Forms;

namespace MaszynaPi {
    public partial class ArchitectureRadioButton : RadioButton {
        public Defines.Architecture Architecture { get; set; }

        public ArchitectureRadioButton(){
            InitializeComponent();
        }
    }
}
