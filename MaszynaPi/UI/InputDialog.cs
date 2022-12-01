using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaszynaPi.MachineUI {
    static class InputDialog {

        public static DialogResult ShowInputDialog(ref string input, string title = "Input", string subtitle = "Subtitle", int x = 50, int y = 50) {
            
            System.Drawing.Size size = new System.Drawing.Size(200, 90);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = title;
            inputBox.Location = new System.Drawing.Point(x,y);
            inputBox.MinimizeBox = false;
            inputBox.MaximizeBox = false;

            Label label = new Label();
            label.Text = subtitle;
            label.Size = new System.Drawing.Size(size.Width - 10, 13);
            label.Location = new System.Drawing.Point(5, 3);
            inputBox.Controls.Add(label);

            TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 20);
            textBox.Text = input;
            textBox.KeyPress += HandleInput;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 90 - 90, 50);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 90, 50);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }
        /*Allows to only write numbers*/
        private static void HandleInput(object sender, KeyPressEventArgs e) {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
