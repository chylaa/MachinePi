using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace MaszynaPi.MachineUI {
    public partial class UserControlCharacterOutput : TextBox {

        List<char> CharactersBuffer; // Handle to Control Unit's CharactersInput IO Device

        public UserControlCharacterOutput() {
            InitializeComponent();
            ReadOnly = true;
            BackColor = Color.White;
            Multiline = true;
        }

        public void SetCharactersBufferSource(List<char> charBuffer) {
            CharactersBuffer = charBuffer;
        }

        public void OnCharacterPushed() {
            Text = string.Join("", CharactersBuffer);
            Refresh();
        }

        public void Reset() {
            Clear();
            CharactersBuffer.Clear();
        }
        

    }
}
