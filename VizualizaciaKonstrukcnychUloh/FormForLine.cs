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
    /// Type of line - for type of form - this could be form for line, line segment or half line
    /// </summary>
    public enum TypeOfLine
    {
        line,
        lineSegment,
        halfLine
    }

    /// <summary>
    /// Class for new form for creating a step for new line.
    /// </summary>
    public partial class FormForLine : Form
    {
        public string text = "";
        public static Visualizer visualizer;
        public bool error = true;

        private Color pureColor = Color.FromArgb(255, 128, 178);
        private Color colorOnTabPage = Color.FromArgb(100, 255, 128, 178);
        private Color colorOnButton = Color.FromArgb(200, 255, 128, 178);
        private TypeOfLine lineType = TypeOfLine.line;

        public FormForLine(TypeOfLine tol)
        {
            InitializeComponent();
            lineType = tol;

            if (lineType == TypeOfLine.line)
            {
                pureColor = Color.FromArgb(255, 128, 178);
                colorOnTabPage = Color.FromArgb(100, 255, 128, 178);
                colorOnButton = Color.FromArgb(200, 255, 128, 178);

                tabControl1.TabPages.Remove(tabPage4);
                tabControl1.TabPages.Remove(tabPage5);
                tabControl1.TabPages.Remove(tabPage6);

                toolTip2.Active = false;
                toolTip3.Active = false;

                this.Text = "Priamka";
            }
            else if (lineType == TypeOfLine.lineSegment)
            {
                pureColor = Color.FromArgb(255, 128, 229);
                colorOnTabPage = Color.FromArgb(100, 255, 128, 229);
                colorOnButton = Color.FromArgb(200, 255, 128, 229);

                tabControl1.TabPages.Remove(tabPage1);
                tabPage2.Text = "Úsečka daná bodmi";
                tabPage3.Text = "Úsečka daná bodmi a menom";

                toolTip1.Active = false;
                toolTip2.Active = false;

                this.Text = "Úsečka";
            }
            else if (lineType == TypeOfLine.halfLine)
            {
                pureColor = Color.FromArgb(229, 128, 255);
                colorOnTabPage = Color.FromArgb(100, 229, 128, 255);
                colorOnButton = Color.FromArgb(200, 229, 128, 255);

                tabControl1.TabPages.Remove(tabPage1);
                tabPage2.Text = "Polpriamka daná bodmi";
                tabPage3.Text = "Polpriamka daná bodmi a menom";
                tabControl1.TabPages.Remove(tabPage4);
                tabControl1.TabPages.Remove(tabPage5);
                tabControl1.TabPages.Remove(tabPage6);

                toolTip1.Active = false;
                toolTip3.Active = false;

                this.Text = "Polpriamka";
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
            this.label11.BackColor = Color.Transparent;
            this.label12.BackColor = Color.Transparent;
            this.label13.BackColor = Color.Transparent;
            this.label14.BackColor = Color.Transparent;
            this.label15.BackColor = Color.Transparent;

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
        private void FormForLine_FormClosing(object sender, FormClosingEventArgs e)
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
        /// Step for new line.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox1.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno priamky.");
            else
            {
                text = "priamka " + textBox1.Text;
                error = false;
                this.Close();
                visualizer.Enabled = true;
            }
        }

        /// <summary>
        /// Step for new line given by two points.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox2.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else if (textBox3.Text == "" || textBox3.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else
            {
                if (lineType == TypeOfLine.line)
                {
                    text = "priamka (" + textBox2.Text + "," + textBox3.Text + ")";
                    error = false;
                    this.Close();
                    visualizer.Enabled = true;
                }
                else if (lineType == TypeOfLine.halfLine)
                {
                    text = "polpriamka |" + textBox2.Text + "," + textBox3.Text + ")";
                    error = false;
                    this.Close();
                    visualizer.Enabled = true;
                }
                else if (lineType == TypeOfLine.lineSegment)
                {
                    text = "usecka |" + textBox2.Text + "," + textBox3.Text + "|";
                    error = false;
                    this.Close();
                    visualizer.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Step for new line with name given by two points.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "" || textBox6.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno priamky.");
            else if (textBox5.Text == "" || textBox5.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else if (textBox4.Text == "" || textBox4.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else
            {
                if (lineType == TypeOfLine.line)
                {
                    text = "priamka " + textBox6.Text + "=(" + textBox5.Text + "," + textBox4.Text + ")";
                    error = false;
                    this.Close();
                    visualizer.Enabled = true;
                }
                else if (lineType == TypeOfLine.halfLine)
                {
                    text = "polpriamka " + textBox6.Text + "=|" + textBox5.Text + "," + textBox4.Text + ")";
                    error = false;
                    this.Close();
                    visualizer.Enabled = true;
                }
                else if (lineType == TypeOfLine.lineSegment)
                {
                    text = "usecka " + textBox6.Text + "=|" + textBox5.Text + "," + textBox4.Text + "|";
                    error = false;
                    this.Close();
                    visualizer.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Step for new line segment given by name and length.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == "" || textBox7.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno priamky.");
            else if (textBox8.Text == "" || textBox8.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre dĺžku.");
            else
            {
                text = "usecka " + textBox7.Text + ", " +textBox7.Text + "=" + textBox8.Text;
                error = false;
                this.Close();
                visualizer.Enabled = true;
            }
        }

        /// <summary>
        /// Step for new line segment given by point and length.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox10.Text == "" || textBox10.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre bod.");
            else if (textBox9.Text == "" || textBox9.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else if (textBox11.Text == "" || textBox11.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre dĺžku.");
            else
            {
                text = "usecka |" + textBox10.Text + "," + textBox9.Text + "|, |"+textBox10.Text+","+textBox9.Text+"|=" + textBox11.Text;
                error = false;
                this.Close();
                visualizer.Enabled = true;
            }
        }

        /// <summary>
        /// Step for new line segment with name given by point and length.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox15.Text == "" || textBox15.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno úsečky.");
            else if (textBox14.Text == "" || textBox14.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre bod.");
            else if (textBox13.Text == "" || textBox13.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre meno bodu.");
            else if (textBox12.Text == "" || textBox12.Text.Contains(' '))
                MessageBox.Show("Neplatný zápis pre dĺžku.");
            else
            {
                text = "usecka " + textBox15.Text + "=|" + textBox14.Text + "," + textBox13.Text + "|, "+ textBox15.Text + "=" + textBox12.Text;
                error = false;
                this.Close();
                visualizer.Enabled = true;
            }
        }
    }
}
