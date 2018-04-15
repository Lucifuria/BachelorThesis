using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visualization
{
    /// <summary>
    /// Types of circle - this could be form for new circle or for new part of circle.
    /// </summary>
    public enum TypeOfCircle
    {
        full,
        partial
    }

    /// <summary>
    /// Class for new form for creating a step for new circle.
    /// </summary>
    public partial class FormForCircle : Form
    {
        public string text = "";
        public static Visualizer visualizer;
        public bool error = true;

        private Color pureColor = Color.FromArgb(179, 128, 255);
        private Color colorOnTabPage = Color.FromArgb(100, 179, 128, 255);
        private Color colorOnButton = Color.FromArgb(200, 179, 128, 255);
        private TypeOfCircle circleType = TypeOfCircle.full;

        public FormForCircle(TypeOfCircle toc)
        {
            InitializeComponent();
            circleType = toc;

            if (circleType == TypeOfCircle.full)
            {
                pureColor = Color.FromArgb(179, 128, 255);
                colorOnTabPage = Color.FromArgb(100, 179, 128, 255);
                colorOnButton = Color.FromArgb(200, 179, 128, 255);

                tabControl1.TabPages.Remove(tabPage3);

                this.Text = "Kružnica";
            }
            else if (circleType == TypeOfCircle.partial)
            {
                pureColor = Color.FromArgb(128, 128, 255);
                colorOnTabPage = Color.FromArgb(100, 128, 128, 255);
                colorOnButton = Color.FromArgb(200, 128, 128, 255);

                tabControl1.TabPages.Remove(tabPage1);
                tabControl1.TabPages.Remove(tabPage2);

                this.Text = "Kružnicový oblúk";
            }            

            this.BackColor = pureColor;

            this.label1.BackColor = Color.Transparent;
            this.label2.BackColor = Color.Transparent;
            this.label3.BackColor = Color.Transparent;
            this.label4.BackColor = Color.Transparent;
            this.label5.BackColor = Color.Transparent;
            this.label6.BackColor = Color.Transparent;
            this.label7.BackColor = Color.Transparent;
            this.label8.BackColor = Color.Transparent;
            this.label9.BackColor = Color.Transparent;
            this.label10.BackColor = Color.Transparent;

            this.tabPage1.BackColor = colorOnTabPage;
            this.tabPage2.BackColor = colorOnTabPage;
            this.tabPage3.BackColor = colorOnTabPage;

            this.button1.BackColor = colorOnButton;
            this.button2.BackColor = colorOnButton;
            this.button3.BackColor = colorOnButton;
        }

        /// <summary>
        /// Proper closing the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormForCircle_FormClosing(object sender, FormClosingEventArgs e)
        {
            visualizer.Enabled = true;
            if (e.CloseReason == CloseReason.UserClosing && error)
            {
                error = true;
            }
            else
            {
                error = false;
            }
        }

        /// <summary>
        /// Step for new circle given by center and radius.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox1.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno kružnice.");
            else if (textBox2.Text == "" || textBox2.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else if (textBox3.Text == "" || textBox3.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre polomer.");
            else
            {
                text = "kruznica " + textBox1.Text + "(" + textBox2.Text + "," + textBox3.Text+")";
                error = false;
                this.Close();
                visualizer.Enabled = true;
            }
        }

        /// <summary>
        /// Step for new circle given by center and point.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "" || textBox4.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno kružnice.");
            else if (textBox5.Text == "" || textBox5.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else if (textBox6.Text == "" || textBox6.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else
            {
                text = "kruznica " + textBox4.Text + "(" + textBox5.Text + "," + textBox6.Text + ")";
                error = false;
                this.Close();
                visualizer.Enabled = true;
            }
        }

        /// <summary>
        /// Step for new part circle given by three points.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == "" || textBox7.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno kružnicového oblúku.");
            else if (textBox8.Text == "" || textBox8.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else if (textBox9.Text == "" || textBox9.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else if (textBox10.Text == "" || textBox10.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else
            {
                text = "obluk " + textBox7.Text + "(" + textBox8.Text + "," + textBox9.Text + "," + textBox10.Text + ")";
                error = false;
                this.Close();
                visualizer.Enabled = true;
            }
        }
    }
}
