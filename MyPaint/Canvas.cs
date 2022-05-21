using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPaint
{
    public partial class Canvas : Form
    {
        private int oldX, oldY;
        private Bitmap bmp;
        private string ImageName;

        public bool isDrawStar = false;

        public int Vertex, ExternalR, InternalR, Turnaround;
        public Color CurColor;

        public bool isDrawing = true;

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Graphics g = Graphics.FromImage(bmp);
                g.DrawLine(new Pen(MainForm.CurColor, MainForm.CurWidth), oldX, oldY, e.X, e.Y);
                oldX = e.X;
                oldY = e.Y;
                pictureBox1.Invalidate();
                isDrawing = true;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            oldX = e.X;
            oldY = e.Y;
        }

        public Canvas()
        {
            InitializeComponent();
            bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
            pictureBox1.Image = bmp;
            ImageName = null;

        }

        public int CanvasWidth
        {
            get
            {
                return pictureBox1.Width;
            }
            set
            {
                pictureBox1.Width = value;
                Bitmap tbmp = new Bitmap(value, pictureBox1.Width);
                Graphics g = Graphics.FromImage(tbmp);
                g.Clear(Color.White);
                g.DrawImage(bmp, new Point(0, 0));
                bmp = tbmp;
                pictureBox1.Image = bmp;
            }
        }
        public int CanvasHeight
        {
            get
            {
                return pictureBox1.Height;
            }
            set
            {
                pictureBox1.Height = value;
                Bitmap tbmp = new Bitmap(pictureBox1.Width, value);
                Graphics g = Graphics.FromImage(tbmp);
                g.Clear(Color.White);
                g.DrawImage(bmp, new Point(0, 0));
                bmp = tbmp;
                pictureBox1.Image = bmp;
            }
        }

        public void SaveAs()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.AddExtension = true;
            dlg.Filter = "Windows Bitmap (*.bmp)|*.bmp| Файлы JPEG (*.jpg)|*.jpg";
            ImageFormat[] ff = { ImageFormat.Bmp, ImageFormat.Jpeg };
            dlg.FileName = this.Text;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ImageName = dlg.FileName;
                bmp.Save(dlg.FileName);
                isDrawing = true;
            }
        }

        public void SaveImage()
        {
            if (string.IsNullOrEmpty(ImageName))
            {
                SaveAs();
            }
            else {
                bmp.Save(ImageName);
                isDrawing = false;
            }
        }

        private void DrawStar(int x, int y) {
            if (isDrawStar)
            {
                int n = Vertex;        // число вершин
                double R = ExternalR, r = InternalR;   // радиусы
                double alpha = Turnaround;        // поворот

                PointF[] points = new PointF[2 * n + 1];

                double a = alpha, da = Math.PI / n, l;
                for (int k = 0; k < 2 * n + 1; k++)
                {
                    l = k % 2 == 0 ? r : R;
                    points[k] = new PointF((float)(x + l * Math.Cos(a)), (float)(y + l * Math.Sin(a)));
                    a += da;
                }
                Graphics g = Graphics.FromImage(bmp);
                SolidBrush brush = new SolidBrush(CurColor);
                g.FillPolygon(brush, points); //цвет
                isDrawStar = false;
                isDrawing = true;
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            DrawStar(e.Location.X, e.Location.Y);
        }

        private void Canvas_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isDrawing)
            {
                DialogResult result = MessageBox.Show("Вы хотите сохранить изменения в текущем файле?", "Уведомление", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    SaveImage();
                }
                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        public Canvas(String FileName)
        {
            InitializeComponent();
            bmp = new Bitmap(FileName);
            Graphics g = Graphics.FromImage(bmp);
            pictureBox1.Width = bmp.Width;
            pictureBox1.Height = bmp.Height;
            pictureBox1.Image = bmp;
        }
    }
}
