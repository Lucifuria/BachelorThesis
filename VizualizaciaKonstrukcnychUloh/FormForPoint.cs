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
    /// Class for new form for creating a step for new point.
    /// </summary>
    public partial class FormForPoint : Form
    {
        public string text = "";
        public static Visualizer visualizer;
        public bool error = true;

        private Color pureColor = Color.FromArgb(255, 128, 128);
        private Color colorOnTabPage = Color.FromArgb(100, 255, 128, 128);
        private Color colorOnButton = Color.FromArgb(200, 255, 128, 128);

        public FormForPoint()
        {
            InitializeComponent();
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
            this.label11.BackColor = Color.Transparent;
            this.label12.BackColor = Color.Transparent;
            this.label13.BackColor = Color.Transparent;
            this.label14.BackColor = Color.Transparent;

            this.tabPage1.BackColor = colorOnTabPage;
            this.tabPage2.BackColor = colorOnTabPage;
            this.tabPage3.BackColor = colorOnTabPage;
            this.tabPage4.BackColor = colorOnTabPage;
            this.tabPage5.BackColor = colorOnTabPage;
            this.tabPage6.BackColor = colorOnTabPage;

            this.button1.BackColor = colorOnButton;
            this.button2.BackColor = colorOnButton;
            this.button3.BackColor = colorOnButton;
            this.button4.BackColor = colorOnButton;
            this.button5.BackColor = colorOnButton;
            this.button6.BackColor = colorOnButton;
        }

        /// <summary>
        /// Proper closing the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormForPoint_FormClosing(object sender, FormClosingEventArgs e)
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
        /// Step for new point.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox1.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else
            {
                text = "bod " + textBox1.Text;
                error = false;
                this.Close();
                visualizer.Enabled = true;
            }
        }        

        /// <summary>
        /// Step for new point with coordinates.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            double x;
            double y;
            if (textBox2.Text == "" || textBox2.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else if (!Double.TryParse(textBox3.Text, out x))
                MessageBox.Show("Neplatný zápis pre x-ovú súradnicu bodu.");
            else if (!Double.TryParse(textBox4.Text, out y))
                MessageBox.Show("Neplatný zápis pre y-ovú súradnicu bodu.");
            else
            {
                text = "bod " + textBox2.Text + "(" + x + ";" + y + ")";
                error = false;
                this.Close();
                visualizer.Enabled = true;
            }
        }

        /// <summary>
        /// Step for new point in distance from another point.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == "" || textBox7.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else if (textBox6.Text == "" || textBox6.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else if (textBox5.Text == "" || textBox5.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre vzdialenosť bodu.");
            else
            {
                text = "bod " + textBox7.Text + ", |" + textBox6.Text + "," + textBox7.Text + "|=" + textBox5.Text;
                error = false;
                this.Close();
                visualizer.Enabled = true;
            }
        }

        /// <summary>
        /// Step for new point lying on another object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox10.Text == "" || textBox10.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else if (textBox9.Text == "" || textBox9.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno objektu.");
            else
            {
                text = "bod " + textBox10.Text + " na " + textBox9.Text;
                error = false;
                this.Close();
                visualizer.Enabled = true;
            }
        }

        /// <summary>
        /// Step for new point which doesn't lie on the given object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox11.Text == "" || textBox11.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else if (textBox8.Text == "" || textBox8.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno objektu.");
            else
            {
                text = "bod " + textBox11.Text + " nie na " + textBox8.Text;
                error = false;
                this.Close();
                visualizer.Enabled = true;
            }
        }

        /// <summary>
        /// Step for new intersection of two objects.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox13.Text == "" || textBox13.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else if (textBox12.Text == "" || textBox12.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno objektu.");
            else if (textBox14.Text == "" || textBox14.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno objektu.");
            else
            {
                text = "bod " + textBox13.Text + " na " + textBox12.Text + " a " + textBox14.Text;
                error = false;
                this.Close();
                visualizer.Enabled = true;
            }
        }
    }        
}
