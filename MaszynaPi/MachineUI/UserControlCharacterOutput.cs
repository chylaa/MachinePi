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
    public partial class UserControlCharacterOutput : TextBox {

        List<char> CharactersBuffer; // Handle to Central Unit's CharactersInput IO Device

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
