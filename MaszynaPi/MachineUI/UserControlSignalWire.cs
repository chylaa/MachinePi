using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MaszynaPi.MachineUI {
    internal partial class UserControlSignalWire : UserControl 
    {

        private readonly static Brush BRUSH_UNACTIVE = Brushes.Black;
        private readonly static Brush BRUSH_ACTIVE = Brushes.Red;

        private readonly static Font FONT = new Font("Microsoft Sans Serif", FONT_SIZE, FontStyle.Regular, GraphicsUnit.Pixel);

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
        public int Rotation { get; set; }

        [Category("Appearance")]
        [Description("Type of line cap.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public LineCap Cap { get; set; }

        public bool Active { get; set; }

        static public bool ManualControl { get; set; }

        public UserControlSignalWire() {
            InitializeComponent();
            Active = false;
            ManualControl = false;

            if (Rotation != 0 && Rotation != 90 && Rotation != 180 && Rotation != 270) 
                throw new Exception("Invalid 'Rotation' property value: " + Rotation.ToString() + ". Only valid: [0, 90, 180, 270].");        
        }
        
        private void DrawText(PaintEventArgs pe, Brush brush, float x, float y) {
            pe.Graphics.DrawString(SignalName, FONT, brush, new PointF(x, y));
        }

        private void DrawWire(PaintEventArgs pe) {
            Graphics g = pe.Graphics;
            Brush brush = Active ? BRUSH_ACTIVE : BRUSH_UNACTIVE;
            Pen pen = new Pen(brush, PENSIZE) { StartCap = Cap };
            if (Rotation == 0) {// Right
                g.DrawLine(pen, x2: (Size.Width/2), y2: (Size.Height / 2), x1: Size.Width, y1: (Size.Height/2));
                DrawText(pe, brush, x: Math.Min(0,(Size.Width / 4) - (SignalName.Length-2)), y: (Size.Height / 2) - (FONT_SIZE / 2)-(PENSIZE));
            }
            if (Rotation == 90) { // Down
                g.DrawLine(pen, x1: (Size.Width / 2), y1: (Size.Height), x2: (Size.Width / 2), y2: 0);
                DrawText(pe, brush, x: (Size.Width / 2)+ PENSIZE + PADDING, y: (Size.Height / 2)-(FONT_SIZE / 2));
            }
            if (Rotation == 180) { // Left
                g.DrawLine(pen, x2: (Size.Width/2), y2: (Size.Height / 2), x1: 0, y1: (Size.Height / 2));
                DrawText(pe, brush, x: (Size.Width / 2) + PADDING, y: (Size.Height / 2)- (FONT_SIZE / 2) - (PENSIZE));
            }
            if (Rotation == 270) { // Up
                g.DrawLine(pen, x1: (Size.Width / 2), y1: 0, x2: (Size.Width / 2) , y2: Size.Height);
                DrawText(pe, brush, x: (Size.Width / 2) + PENSIZE + PADDING, y: (Size.Height / 2) - (FONT_SIZE/2));
            }
        }

        public void Activate() {
            Active = true;
            Refresh();
        }

        public void Deactivate() {
            Active = false;
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e) {
            DrawWire(e);
        }

        private void UserControlSignalWire_MouseClick(object sender, MouseEventArgs e) {
            if(ManualControl && e.Button == MouseButtons.Left) {
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