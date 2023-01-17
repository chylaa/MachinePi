using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public void OnCharacterFetched() {
            Text = string.Join("", CharactersBuffer);
        }

        private void UpdateCharactersBuffer() {
            if (CharactersBuffer == null) throw new Exception("Character buffer source in UserControlCharacerInput not set. Use SetCharacterBufferSource() method.");
            CharactersBuffer.Clear();
            CharactersBuffer.AddRange(Text.Select(c => c).ToList());
        }

        protected override void OnTextChanged(EventArgs e) {
            base.OnTextChanged(e);
            UpdateCharactersBuffer();
        }

    }
}
