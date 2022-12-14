using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaszynaPi.MachineUI {
    public partial class UserControlSignalWire : PictureBox {

        private bool STATE_CHANGED = false;
        private readonly Color COLOR_UNACTIVE = Color.Black;
        private readonly Color COLOR_ACTIVE = Color.Red;

        public bool Active;

        [Category("Appearance")]
        [Description("Name of signal associated with control.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string SignalName { get; set; }

        [Category("Appearance")]
        [Description("Rotaton of image - Rotation == 0 means that image will be horizontal facing right: ------>.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public int Rotation { get; set; } 

        public UserControlSignalWire() {
            InitializeComponent();
            SizeMode = PictureBoxSizeMode.StretchImage;
            Active = false;
        }

        private void Recolor(PaintEventArgs pe) {
            if (Image == null) throw new Exception("Code Error - Wire Image of " + SignalName + " not set");
            Graphics g = pe.Graphics;
            Color oldColor, newColor;
            if (Active) { oldColor = COLOR_UNACTIVE; newColor = COLOR_ACTIVE; 
            } else { oldColor = COLOR_ACTIVE; newColor = COLOR_UNACTIVE; }
            
            using (Bitmap bmp = new Bitmap(this.Image)) {
                ColorMap[] colorMap = new ColorMap[1];
                colorMap[0] = new ColorMap();
                colorMap[0].OldColor = oldColor;
                colorMap[0].NewColor = newColor;
                ImageAttributes attr = new ImageAttributes();
                attr.SetRemapTable(colorMap);
                // Draw using the color map
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                g.DrawImage(bmp, rect, 0, 0, rect.Width, rect.Height, GraphicsUnit.Pixel, attr);
            }
            STATE_CHANGED = false;
        }

        public void Activate() {
            Active = true;
            STATE_CHANGED = true;
        }

        public void Deactivate() {
            Active = false;
            STATE_CHANGED = true;
        }

        protected override void OnPaint(PaintEventArgs pe) {
            if (STATE_CHANGED) Recolor(pe);
            base.OnPaint(pe);
        }
    }
}
