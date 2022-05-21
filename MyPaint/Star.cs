using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPaint
{
    public partial class Star : Form
    {
        public Color CurColor = Color.Black;

        public Star()
        {
            InitializeComponent();
        }

        public int Vertex {
            get {
                return int.Parse(textBox1.Text);
            }
            set {
                textBox1.Text = value.ToString();
            }
        }

        public int ExternalR
        {
            get
            {
                return int.Parse(textBox2.Text);
            }
            set
            {
                textBox2.Text = value.ToString();
            }
        }

        public int InternalR
        {
            get
            {
                return int.Parse(textBox3.Text);
            }
            set
            {
                textBox3.Text = value.ToString();
            }
        }

        public int Turnaround
        {
            get
            {
                return int.Parse(textBox4.Text);
            }
            set
            {
                textBox4.Text = value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                CurColor = cd.Color;
                button1.BackColor = CurColor;
            }
        }

        private void Star_FormClosing(object sender, FormClosingEventArgs e)
        {
            int a = 0;
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || textBox3.Text.Length == 0 || textBox4.Text.Length == 0 ||
                !(int.TryParse(textBox2.Text, out a)) || !(int.TryParse(textBox1.Text, out a)) || !(int.TryParse(textBox3.Text, out a)) || !(int.TryParse(textBox4.Text, out a)))
            {
                MessageBox.Show("Значение должно быть целым числом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
            }
        }
    }
}
