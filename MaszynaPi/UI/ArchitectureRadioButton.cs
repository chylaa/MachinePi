using System.Windows.Forms;

namespace MaszynaPi {
    public partial class ArchitectureRadioButton : RadioButton {
        public Defines.Architectures Architecture { get; set; }

        public ArchitectureRadioButton(){
            InitializeComponent();
        }
    }
}
