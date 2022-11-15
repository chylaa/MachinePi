using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaszynaPi.MachineUI {
    public partial class UserControlCharacterInput : TextBox {

        List<char> CharactersBuffer; // Handle to Central Unit's CharactersInput IO Device

        public UserControlCharacterInput() {
            InitializeComponent();
        }

        public void SetCharactersBufferSource(List<char> charBuffer) {
            CharactersBuffer = charBuffer;
        }
        public override void Refresh() {
            if (CharactersBuffer == null) throw new Exception("Character buffer source in UserControlCharacerInput not set. Use SetCharacterBufferSource() method.");
            Text = string.Join("",CharactersBuffer);
            base.Refresh();
        }
    }
}
