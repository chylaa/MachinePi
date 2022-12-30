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
    public partial class UserControlSignalWire : UserControl {

        private readonly Brush BRUSH_UNACTIVE = Brushes.Black;
        private readonly Brush BRUSH_ACTIVE = Brushes.Red;

        private const int FONT_SIZE = 12;
        private const int PADDING = 3;
        private const int PENSIZE = 3;

        [Category("Appearance")]
        [Description("Name of signal associated with control.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue("Signal")]
        public string SignalName { get; set; }

        [Category("Appearance")]
        [Description("Rotaton of image - Rotation == 0 means that image will be horizontal facing right: ------>.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(90)]
        public int Rotation { get; set; }

        public bool Active { get; set; }

        public UserControlSignalWire() {
            InitializeComponent();
            Active = false;

            if (Rotation != 0 && Rotation != 90 && Rotation != 180 && Rotation != 270) throw new Exception("Invalid 'Rotation' property value: " + Rotation.ToString() + ". Only valid: [0, 90, 180, 270].");
            Rotation = Rotation % 360; // get values only from 0 to 360
                        
        }
        
        private void DrawText(PaintEventArgs pe, Brush brush, float x, float y) {
            using (Font font1 = new Font("Microsoft Sans Serif", FONT_SIZE, FontStyle.Regular, GraphicsUnit.Pixel)) {
                PointF pointF1 = new PointF(x, y);
                pe.Graphics.DrawString(SignalName, font1, brush, pointF1);
            }
        }

        private void DrawWire(PaintEventArgs pe) {
            Graphics g = pe.Graphics;
            Brush brush = BRUSH_UNACTIVE;
            if (IsActive()) brush = BRUSH_ACTIVE;
            Pen pen = new Pen(brush,PENSIZE);
            pen.StartCap = LineCap.ArrowAnchor;
            if (Rotation == 0) {// Right
                g.DrawLine(pen, x2: (this.Size.Width/2), y2: (this.Size.Height / 2), x1: this.Size.Width, y1: (this.Size.Height/2));
                DrawText(pe, brush, x: (Size.Width / 4), y: (this.Size.Height / 2) - (FONT_SIZE / 2)-(PENSIZE));
            }
            if (Rotation == 90) { // Down +
                g.DrawLine(pen, x1: (this.Size.Width / 2), y1: 0, x2: this.Size.Height, y2: (this.Size.Width / 2));
                DrawText(pe, brush, x: (Size.Width / 2)+ PENSIZE + PADDING, y: (this.Size.Height / 2)-(FONT_SIZE / 2));
            }
            if (Rotation == 180) { // Left
                g.DrawLine(pen, x1: (this.Size.Width), y1: (this.Size.Height / 2), x2: (this.Size.Width / 2), y2: (this.Size.Height / 2));
                DrawText(pe, brush, x: (Size.Width / 2) + PADDING, y: (this.Size.Height / 2)- (FONT_SIZE / 2) - (PENSIZE));
            }
            if (Rotation == 270) { // Up +
               g.DrawLine(pen, x1: (this.Size.Height), y1: (this.Size.Width / 2), x2: (this.Size.Width / 2), y2: 0);
                DrawText(pe, brush, x: (Size.Width / 2) + PENSIZE + PADDING, y: (this.Size.Height / 2) - (FONT_SIZE/2));
            }
        }


        public bool IsActive() {
            return Active;
        }

        public void Activate() {
            Active = true;
        }

        public void Deactivate() {
            Active = false;
        }


        protected override void OnPaint(PaintEventArgs e) {
            DrawWire(e);
            base.OnPaint(e);
        }

        private void UserControlSignalWire_MouseClick(object sender, MouseEventArgs e) {
            if(e.Button == MouseButtons.Left) {
                Active = !Active;
                Refresh();
            }
        }
    }
}



/*
 
         private void Recolor(PaintEventArgs pe) {
            if (this.pictureBox.Image == null) throw new Exception("Code Error - Wire Image of " + SignalName + " not set");
            Graphics g = pe.Graphics;
            Color oldColor, newColor;
            if (IsActive()) { oldColor = COLOR_UNACTIVE; newColor = COLOR_ACTIVE; 
            } else { oldColor = COLOR_ACTIVE; newColor = COLOR_UNACTIVE; }
            
            using (Bitmap bmp = new Bitmap(this.pictureBox.Image)) {
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
        }*/