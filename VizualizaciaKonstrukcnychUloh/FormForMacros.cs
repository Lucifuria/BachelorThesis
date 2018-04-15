using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visualization
{
    /// <summary>
    /// Class for new form for creating a step for using a macro.
    /// </summary>
    public partial class FormForMacros : Form
    {
        const string pathOfFile = "macros/";

        int partsOfInputOutput = 0;
        int currentPartOfInputOutput = -1;
        List<string> allInputs = new List<string>();
        List<string> allOutputs = new List<string>();
        string info = "";

        public string text = "";
        public static Visualizer visualizer;
        public bool error = true;

        private Color pureColor = Color.FromArgb(128, 255, 200);
        private Color colorOnTabPage = Color.FromArgb(100, 128, 255, 200);
        private Color colorOnButton = Color.FromArgb(200, 128, 255, 200);

        public FormForMacros()
        {
            InitializeComponent();

            this.button1.Enabled = false;
            this.button3.Enabled = false;
            label3.Text = "";

            // Macros (files) are saved in folder with name saved is pathOfFile.
            string[] files = Directory.GetFiles(pathOfFile);

            foreach (string f in files)
            {
                comboBox1.Items.Add(f.Substring(pathOfFile.Length));
            }

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.Text = comboBox1.Items[0].ToString();
                LoadParameters(comboBox1.Text);
                label3.Text = info;
            }

            label2.Visible = false;
            textBox1.Visible = false;
            
            this.BackColor = pureColor;

            this.label1.BackColor = Color.Transparent;
            this.label2.BackColor = Color.Transparent;

            this.button1.BackColor = colorOnButton;
            this.button2.BackColor = colorOnButton;
            this.button3.BackColor = colorOnButton;
            this.button4.BackColor = colorOnButton;            
        }
        
        /// <summary>
        /// Loads parameters from file.
        /// </summary>
        /// <param name="nameOfFile">Name of file, where the parameters are.</param>
        private void LoadParameters(string nameOfFile)
        {
            string[] lines = new string[0];
            if (File.Exists(pathOfFile+nameOfFile))
                lines = File.ReadAllLines(pathOfFile + nameOfFile);
            Macros.PartsOfMacro currentPart = Macros.PartsOfMacro.Inputs;
            partsOfInputOutput = 0;
            currentPartOfInputOutput = -1;
            allInputs = new List<string>();
            allOutputs = new List<string>();
            info = "";

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "")
                {
                    currentPart++;
                }                   
                else
                {
                    if (currentPart == Macros.PartsOfMacro.Inputs)
                    {
                        allInputs.Add(lines[i]);
                    }
                    else if (currentPart == Macros.PartsOfMacro.Outputs)
                    {
                        allOutputs.Add(lines[i]);
                    }
                    else if (currentPart == Macros.PartsOfMacro.Description)
                    {
                        info = lines[i];
                    }
                }
            }

            partsOfInputOutput = allInputs.Count + allOutputs.Count;
        }

        /// <summary>
        /// Proper closing of form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormForMacros_FormClosing(object sender, FormClosingEventArgs e)
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
        /// It is OK button - for all parts during the saving construction, for drawing the construction or writing the steps of construction.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            char[] space = { ' ' };
            if (textBox1.Visible && textBox1.Text.Split(space).Length != 2)
                MessageBox.Show("Nesprávny zápis parametra.");
            else
            {
                if (allOutputs.Count != 0)
                    allOutputs[currentPartOfInputOutput - allInputs.Count] = textBox1.Text;
                else if (allInputs.Count != 0)
                    allInputs[currentPartOfInputOutput] = textBox1.Text;

                text = comboBox1.Text + '(';
                foreach (var s in allInputs)
                {
                    text += s + ';';
                }
                if (allInputs.Count > 0)
                    text = text.Substring(0, text.Length - 1) + '-';
                else
                    text += '-';
                foreach (var s in allOutputs)
                {
                    text += s + ';';
                }
                if (allOutputs.Count > 0)
                    text = text.Substring(0, text.Length - 1) + ')';
                else
                    text += ')';

                error = false;
                this.Close();
                visualizer.Enabled = true;
            }
        }

        /// <summary>
        /// Reseting the creating of new step when the macro is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }

        /// <summary>
        /// Canceling the creating the step for using the macro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            LoadParameters(comboBox1.Text);
            label2.Visible = false;
            textBox1.Visible = false;
            label3.Text = info;
            button1.Enabled = false;
            button3.Enabled = false;
            button2.Enabled = true;
            button4.Enabled = true;
        }

        /// <summary>
        /// Returning back during the creating the step for using the macro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            currentPartOfInputOutput--;
            if (currentPartOfInputOutput == -1)
                button2_Click(sender, e);
            else
            {
                if (currentPartOfInputOutput < allInputs.Count)
                {
                    label2.Text = "Vstupný parameter:";
                    textBox1.Text = allInputs[currentPartOfInputOutput];
                }
                else
                {
                    label2.Text = "Výstupný parameter:";
                    textBox1.Text = allOutputs[currentPartOfInputOutput - allInputs.Count];
                }
                button4.Enabled = true;
                button1.Enabled = false;
            }
        }

        /// <summary>
        /// Going forth during the creating the step for using the macro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            char[] space = { ' ' };
            if (textBox1.Visible && textBox1.Text.Split(space).Length != 2)
                MessageBox.Show("Nesprávny zápis parametra.");
            else
            {
                if (currentPartOfInputOutput == -1)
                {
                    button3.Enabled = true;
                    currentPartOfInputOutput++;
                    label2.Visible = true;
                    textBox1.Visible = true;

                    if (allInputs.Count > 0)
                    {
                        label2.Text = "Vstupný parameter:";
                        textBox1.Text = allInputs[currentPartOfInputOutput].Substring(0, allInputs[currentPartOfInputOutput].IndexOf(' '));
                    }
                    else if (allOutputs.Count > 0)
                    {
                        label2.Text = "Výstupný parameter:";
                        textBox1.Text = allOutputs[currentPartOfInputOutput].Substring(0, allOutputs[currentPartOfInputOutput].IndexOf(' '));
                    }
                    else
                    {
                        button1.Enabled = true;
                        button4.Enabled = false;
                        label2.Visible = false;
                        textBox1.Visible = false;
                    }
                }
                else
                {
                    if (currentPartOfInputOutput <= allInputs.Count)
                        allInputs[currentPartOfInputOutput] = textBox1.Text;
                    else
                        allOutputs[currentPartOfInputOutput - allInputs.Count] = textBox1.Text;
                    currentPartOfInputOutput++;

                    if (currentPartOfInputOutput < allInputs.Count)
                    {
                        label2.Text = "Vstupný parameter:";
                        textBox1.Text = allInputs[currentPartOfInputOutput].Substring(0, allInputs[currentPartOfInputOutput].IndexOf(' '));
                    }
                    else if (currentPartOfInputOutput < allInputs.Count + allOutputs.Count)
                    {
                        label2.Text = "Výstupný parameter:";
                        textBox1.Text = allOutputs[currentPartOfInputOutput - allInputs.Count].Substring(0, allOutputs[currentPartOfInputOutput - allInputs.Count].IndexOf(' '));
                        if (currentPartOfInputOutput == allInputs.Count + allOutputs.Count - 1)
                        {
                            button1.Enabled = true;
                            button4.Enabled = false;
                        }
                    }
                    else
                    {
                        button1.Enabled = true;
                        button4.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// Detecting mouse down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            button1.Location = new System.Drawing.Point(button1.Location.X + 2, button1.Location.Y + 2);

            button1.Height = 56;
            button1.Width = 56;

            button1.Image = Properties.Resources.oksmall;
        }

        /// <summary>
        /// Detecting mouse up and reacting on that.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            button1.Location = new System.Drawing.Point(button1.Location.X - 2, button1.Location.Y - 2);

            button1.Height = 60;
            button1.Width = 60;

            button1.Image = Properties.Resources.ok;
        }

        /// <summary>
        /// Detecting mouse down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            button2.Location = new System.Drawing.Point(button2.Location.X + 2, button2.Location.Y + 2);

            button2.Height = 56;
            button2.Width = 56;

            button2.Image = Properties.Resources.cancelsmall;
        }

        /// <summary>
        /// Detecting mouse up and reacting on that.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            button2.Location = new System.Drawing.Point(button2.Location.X - 2, button2.Location.Y - 2);

            button2.Height = 60;
            button2.Width = 60;

            button2.Image = Properties.Resources.cancel;
        }

        /// <summary>
        /// Detecting mouse down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            button3.Location = new System.Drawing.Point(button3.Location.X + 2, button3.Location.Y + 2);

            button3.Height = 56;
            button3.Width = 56;

            button3.Image = Properties.Resources.backsmall;
        }

        /// <summary>
        /// Detecting mouse up and reacting on that.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            button3.Location = new System.Drawing.Point(button3.Location.X - 2, button3.Location.Y - 2);

            button3.Height = 60;
            button3.Width = 60;

            button3.Image = Properties.Resources.back;
        }

        /// <summary>
        /// Detecting mouse down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            button4.Location = new System.Drawing.Point(button4.Location.X + 2, button4.Location.Y + 2);

            button4.Height = 56;
            button4.Width = 56;

            button4.Image = Properties.Resources.forthsmall;
        }

        /// <summary>
        /// Detecting mouse up and reacting on that.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_MouseUp(object sender, MouseEventArgs e)
        {
            button4.Location = new System.Drawing.Point(button4.Location.X - 2, button4.Location.Y - 2);

            button4.Height = 60;
            button4.Width = 60;

            button4.Image = Properties.Resources.forth
                ;
        }
    }
}
