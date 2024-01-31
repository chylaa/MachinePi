using System.Windows.Forms;

namespace MaszynaPi 
{
    internal partial class ArchitectureRadioButton : RadioButton 
    {
        public Defines.Architecture Architecture { get; set; }

        public ArchitectureRadioButton(){
            InitializeComponent();
        }
    }
}
