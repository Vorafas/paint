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
    public partial class CanvasSize : Form
    {
        public CanvasSize()
        {
            InitializeComponent();
        }

        public int CanvasWidth
        {
            get 
            {
                return int.Parse(textBox1.Text);
            }
            set
            {
                textBox1.Text = value.ToString();
            }
        }

        public int CanvasHeight
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0) {
                return;
            }
            try
            {
                int.Parse(textBox1.Text);
            }
            catch 
            {
                MessageBox.Show("Значение должн быть целым числом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0)
            {
                return;
            }
            try
            {
                int.Parse(textBox2.Text);
            }
            catch
            {
                MessageBox.Show("Значение должн быть целым числом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CanvasSize_FormClosing(object sender, FormClosingEventArgs e)
        {
            int a = 0;
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0 ||
                !(int.TryParse(textBox2.Text, out a)) || !(int.TryParse(textBox1.Text, out a)))
            {
                MessageBox.Show("Значение должно быть целым числом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
            }
        }
    }
}
