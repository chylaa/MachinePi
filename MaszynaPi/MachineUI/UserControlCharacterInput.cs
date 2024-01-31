using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MaszynaPi.MachineUI 
{
    internal partial class UserControlCharacterInput : TextBox 
    {

        Queue<char> CharactersBuffer; // Handle to Control Unit's CharactersInput IO Device

        public UserControlCharacterInput() {
            InitializeComponent();
        }

        public void SetCharactersBufferSource(Queue<char> charBuffer) {
            CharactersBuffer = charBuffer;
        }

        public void OnCharacterFetched() {
            Text = string.Join("", CharactersBuffer);
        }

        private void UpdateCharactersBuffer() {
            if (CharactersBuffer == null) throw new Exception("Character buffer source in UserControlCharacerInput not set. Use SetCharacterBufferSource() method.");
            CharactersBuffer.Clear();
            foreach (char c in Text.ToCharArray())  
                CharactersBuffer.Enqueue(c); 
        }

        protected override void OnTextChanged(EventArgs e) {
            base.OnTextChanged(e);
            UpdateCharactersBuffer();
        }

    }
}
