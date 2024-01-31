using System.Windows.Forms;

namespace MaszynaPi 
{
    internal partial class ComponentsCheckBox : CheckBox 
    {

        public Defines.Components Component { get; set; }

        public ComponentsCheckBox() {
            InitializeComponent();
        }

    }
}
