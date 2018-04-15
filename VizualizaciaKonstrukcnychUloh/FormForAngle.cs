using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Visualization
{
    /// <summary>
    /// Class for new form for creating a step for new angle.
    /// </summary>
    public partial class FormForAngle : Form
    {
        public string text = "";
        public static Visualizer visualizer;
        public bool error = true;

        private Color pureColor = Color.FromArgb(128, 229, 255);
        private Color colorOnTabPage = Color.FromArgb(100, 128, 229, 255);
        private Color colorOnButton = Color.FromArgb(200, 128, 229, 255);

        public FormForAngle()
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
            this.label15.BackColor = Color.Transparent;
            this.label16.BackColor = Color.Transparent;

            this.tabPage1.BackColor = colorOnTabPage;
            this.tabPage2.BackColor = colorOnTabPage;
            this.tabPage3.BackColor = colorOnTabPage;
            this.tabPage4.BackColor = colorOnTabPage;

            this.button1.BackColor = colorOnButton;
            this.button2.BackColor = colorOnButton;
            this.button3.BackColor = colorOnButton;
            this.button4.BackColor = colorOnButton;
        }

        /// <summary>
        /// Proper closing the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormForAngle_FormClosing(object sender, FormClosingEventArgs e)
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
        /// Step for new angle given by three points.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox1.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else if (textBox2.Text == "" || textBox2.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else if (textBox3.Text == "" || textBox3.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else
            {
                text = "uhol " + textBox1.Text + "," + textBox2.Text + "," + textBox3.Text;
                error = false;
                this.Close();
                visualizer.Enabled = true;
            }
        }

        /// <summary>
        /// Step for new angle with name given by three points.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "" || textBox4.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno uhla.");
            else if (textBox5.Text == "" || textBox5.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else if (textBox6.Text == "" || textBox6.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else if (textBox7.Text == "" || textBox7.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else
            {
                text = "uhol " + textBox4.Text + "=" + textBox5.Text + "," + textBox6.Text + "," + textBox7.Text;
                error = false;
                this.Close();
                visualizer.Enabled = true;
            }
        }

        /// <summary>
        /// Step for new angle given by two points and size.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox8.Text == "" || textBox8.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else if (textBox9.Text == "" || textBox9.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else if (textBox10.Text == "" || textBox10.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else if (textBox11.Text == "" || textBox11.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre veľkosť uhla.");
            else
            {
                text = "uhol " + textBox8.Text + "," + textBox9.Text + "," + textBox10.Text + ", |" + textBox8.Text + "," + textBox9.Text + "," + textBox10.Text + "|=" + textBox11.Text;
                error = false;
                this.Close();
                visualizer.Enabled = true;
            }
        }

        /// <summary>
        /// Step for new angle with name given by two points and size.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox12.Text == "" || textBox12.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno uhla.");
            else if (textBox13.Text == "" || textBox13.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else if (textBox14.Text == "" || textBox14.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else if (textBox15.Text == "" || textBox15.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else if (textBox16.Text == "" || textBox16.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre veľkosť uhla.");
            else
            {
                text = "uhol " + textBox12.Text + "=" + textBox13.Text + "," + textBox14.Text + "," + textBox15.Text + ", " + textBox12.Text + "=" + textBox16.Text;
                error = false;
                this.Close();
                visualizer.Enabled = true;
            }
        }
    }
}
