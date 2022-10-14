using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaszynaPi {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        // $Command:  dotnet build/publish --runtime linux-arm --self-contained
        /*
         W przypadku korzystania z programu Visual Studio wdróż aplikację w folderze lokalnym.
        Przed opublikowaniem wybierz pozycję Edytuj w podsumowaniu profilu publikowania i wybierz kartę Ustawienia.
        Upewnij się, że w trybie wdrażania ustawiono opcję Samodzielne, a środowisko uruchomieniowe target jest ustawione na linux-arm.
         */
        private void button1_Click(object sender, EventArgs e) {
            MessageBox.Show("Succes!\nrura","O udało się, japierdole ;-;");
        }
    }
}
