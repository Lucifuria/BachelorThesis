using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Visualization
{
    /// <summary>
    /// Class for interface.
    /// </summary>
    public partial class Visualizer : Form
    {
        /// <summary>
        /// Folder which is the macros saved in.
        /// </summary>
        const string pathOfFileWithMacros = "macros/";
        /// <summary>
        /// Folder which is the constructions saved in.
        /// </summary>
        const string pathOfFileWithConstruction = "constructions/";

        /// <summary>
        /// Original width of text box.
        /// </summary>
        public static int sizeOfTextBoxWidth;

        /// <summary>
        /// True if construction from file shoud be printed en block, false if it should be printed step by step.
        /// </summary>
        public static bool printConstructionEnBlock = true;

        /// <summary>
        /// True if construction shoud be drawn en block, false if it should be drawn step by step.
        /// </summary>
        public static bool drawConstructionEnBlock = true;
        
        /// <summary>
        /// It is true if mouse is pressed, otherwise false.
        /// </summary>
        private bool isMouseDown = false;

        /// <summary>
        /// Boolean for buttons for a creating of new object - true if the button is pressed.
        /// </summary>
        private bool tempmouseDown = false;

        /// <summary>
        /// Old X coordinates for finding location.
        /// </summary>
        private int oldX;
        /// <summary>
        /// Old Y coordinates for finding location.
        /// </summary>
        private int oldY;

        /// <summary>
        /// Constanta for zoom.
        /// </summary>
        const double constantForZoom = 1.05;

        /// <summary>
        /// Name of macro.
        /// </summary>
        static string nameOfMacro;
        /// <summary>
        /// Lines in input of macro.
        /// </summary>
        static string[] inputsLines;
        /// <summary>
        /// Lines in input during the saving of macro.
        /// </summary>
        static string[] inputInSavingMacro = null;
        /// <summary>
        /// Lines of commands of macro.
        /// </summary>
        static string[] commandLines;
        /// <summary>
        /// Lines in commands during the saving of macro.
        /// </summary>
        static string[] commandInSavingMacro = null;
        /// <summary>
        /// Lines in output of macro.
        /// </summary>
        static string[] outputsLines;
        /// <summary>
        /// Lines in output during the saving of macro.
        /// </summary>
        static string[] outputInSavingMacro = null;
        /// <summary>
        /// Saved description for returning in saving of macro.
        /// </summary>
        static string descriptionInSavingMacro = "";

        /// <summary>
        /// All lines of construction.
        /// </summary>
        static string[] lines;
        /// <summary>
        /// Order of current line if user is drawing construction step by step.
        /// </summary>
        int currentLine = 0;

        /// <summary>
        /// Tells if objects should be drawn.
        /// </summary>
        public enum DrawingObjects
        {
            /// <summary>
            /// Object shouldn't be drawn.
            /// </summary>
            NotDraw,

            /// <summary>
            /// Object should be drawn by hrey color.
            /// </summary>
            DrawGrey,

            /// <summary>
            /// Object should be drawn.
            /// </summary>
            Draw
        };

        /// <summary>
        /// Current part of macro during saving the macro.
        /// </summary>
        static Macros.PartsOfMacro currentPart = Macros.PartsOfMacro.Inputs;

        /// <summary>
        /// Tells what should be done with second objects in construction.
        /// </summary>
        public static DrawingObjects secondObjectsInConstruction = DrawingObjects.NotDraw;
        /// <summary>
        /// Tells what should be done with temp objects in macro.
        /// </summary>
        public static DrawingObjects tempObjectsInMacro = DrawingObjects.NotDraw;

        /// <summary>
        /// It is true if there are printed names and descriptions of all macros, otherwise false.
        /// </summary>
        public static bool printedAllMacros = false;
        /// <summary>
        /// It is true if the user is saving a macro, otherwise false.
        /// </summary>
        public static bool savingOfMacro = false;

        /// <summary>
        /// Thread for zooming with the buttons.
        /// </summary>
        Thread t;

        /// <summary>
        /// List of variables for saving a macro.
        /// </summary>
        static Dictionary<string, string> tempVariables;

        /// <summary>
        /// Procedure for concatenation of arrays.
        /// </summary>
        /// <typeparam name="T">Type of arrays.</typeparam>
        /// <param name="list">List of arrays.</param>
        /// <returns>Array ehich is concatenation of list of arrays.</returns>
        public static T[] ConcatArrays<T>(params T[][] list)
        {
            var result = new T[list.Sum(a => a.Length)];
            int offset = 0;
            for (int x = 0; x < list.Length; x++)
            {
                list[x].CopyTo(result, offset);
                offset += list[x].Length;
            }
            return result;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Visualizer()
        {
            InitializeComponent();
            Drawing.visualizer = this;
            Macros.visualizer = this;
            FormForPoint.visualizer = this;
            FormForLine.visualizer = this;
            FormForCircle.visualizer = this;
            FormForAngle.visualizer = this;
            FormForMacros.visualizer = this;
            
            sizeOfTextBoxWidth = this.textBox.Size.Width;

            Point.upRight.x = pictureBox.Size.Width;
            Point.downLeft.y = pictureBox.Size.Height;
            Point.downRight.x = pictureBox.Size.Width;
            Point.downRight.y = pictureBox.Size.Height;

            Line.up = new Line(Point.upLeft, Point.upRight);
            Line.down = new Line(Point.downLeft, Point.downRight);
            Line.left = new Line(Point.upLeft, Point.downLeft);
            Line.right = new Line(Point.upRight, Point.downRight);

            Directory.CreateDirectory(pathOfFileWithConstruction);
            Directory.CreateDirectory(pathOfFileWithMacros);

            pictureBox.MouseWheel += new MouseEventHandler(pictureBox_OnScroll);
        }

        /// <summary>
        /// Procedure for redrawing objects when window is resized. It also handles the buttons for new macro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Visualizer_ResizeEnd(object sender, System.EventArgs e)
        {
            Point.upRight.x = pictureBox.Size.Width;
            Point.downLeft.y = pictureBox.Size.Height;
            Point.downRight.x = pictureBox.Size.Width;
            Point.downRight.y = pictureBox.Size.Height;

            Line.up = new Line(Point.upLeft, Point.upRight);
            Line.down = new Line(Point.downLeft, Point.downRight);
            Line.right = new Line(Point.upRight, Point.downRight);
            Line.left = new Line(Point.upLeft, Point.downLeft);
            
            this.pictureBox.Enabled = false;
            this.pictureBox.Enabled = true;
            foreach (GeometricObject o in Reader.allObjects)
            {
                Drawing.DrawObject(o);
            }
            foreach (GeometricObject o in Reader.allObjects)
            {
                if (o.GetFirstName() == o.GetSecondName())
                    Drawing.DrawObject(o);
            }

            Size temps = new Size(this.button3.Size.Width, (this.pictureBox.Size.Height - 3) / 2);
            this.button3.Size = temps;
            this.button4.Size = temps;
            this.button4.Location = new System.Drawing.Point(button3.Location.X, button.Location.Y - 8 - temps.Height);

            if (printedAllMacros)
            {
                textBox.Size = new Size(pictureBox.Location.X + pictureBox.Size.Width - textBox.Location.X, pictureBox.Location.Y + pictureBox.Size.Height - textBox.Location.Y);
            }
            if (savingOfMacro)
            {
                textBox.Size = new Size(pictureBox.Location.X + pictureBox.Size.Width - textBox.Location.X - temps.Width, pictureBox.Location.Y + pictureBox.Size.Height - textBox.Location.Y);
            }
        }

        #region get_Tools;   

        /// <summary>
        /// Procedure for getting picture box.
        /// </summary>
        /// <returns>pictureBox_construction</returns>
        public PictureBox GetPictureBox()
        {
            return this.pictureBox;
        }

        #endregion

        /// <summary>
        /// Procedure for saving construction by clicking on particular item in menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uložiťKonštrukciuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = pathOfFileWithConstruction;

            saveFileDialog.FileName = "konstrukcia";            

            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                    sw.WriteLine(textBox.Text);
                MessageBox.Show("Konštrukcia bola uložená.");
            }
        }

        /// <summary>
        /// Procedure for opening and printing construction by clicking on particular item in menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void načítaťKonštrukciuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printConstructionEnBlock == false)
            {
                button.Text = "VYPÍSAŤ ĎALŠÍ KROK KONŠTRUKCIE";
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = pathOfFileWithConstruction;

            openFileDialog.FileName = "konstrukcia";                      

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                using (Stream s = openFileDialog.OpenFile())
                {
                    var sr = new StreamReader(s);
                    if (printConstructionEnBlock)
                        textBox.Text = sr.ReadToEnd();
                    else
                    {
                        textBox.Text = sr.ReadLine() + '\r';
                        currentLine = 0;
                        string allLines = sr.ReadToEnd();
                        char[] newLine = { '\n' };
                        lines = allLines.Split(newLine, StringSplitOptions.RemoveEmptyEntries);
                    }
                }
            }
        }

        /// <summary>
        /// Procedure for settings by clicking on particular item in menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void postupneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.narazToolStripMenuItem.Checked)
            {
                printConstructionEnBlock = false;
                this.narazToolStripMenuItem.Checked = false;
            }
        }

        /// <summary>
        /// Procedure for settings by clicking on particular item in menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void narazToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.postupneToolStripMenuItem.Checked)
            {
                printConstructionEnBlock = true;
                this.postupneToolStripMenuItem.Checked = false;
            }
        }

        /// <summary>
        /// Procedure for settings by clicking on particular item in menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void postupneToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            button.Text = "VYKRESLIŤ PRVÝ KROK KONŠTRUKCIE";
            if (this.narazToolStripMenuItem1.Checked)
            {
                drawConstructionEnBlock = false;
                this.narazToolStripMenuItem1.Checked = false;
            }
        }

        /// <summary>
        /// Procedure for settings by clicking on particular item in menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void narazToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            button.Text = "VYKRESLIŤ KONŠTRUKCIU";
            if (this.postupneToolStripMenuItem1.Checked)
            {
                drawConstructionEnBlock = true;
                this.postupneToolStripMenuItem1.Checked = false;
            }
        }

        /// <summary>
        /// Procedure for settings by clicking on particular item in menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void vykresľovaťToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.vykresľovaťŠedoToolStripMenuItem.Checked || this.nevykresľovaťToolStripMenuItem.Checked)
            {
                secondObjectsInConstruction = DrawingObjects.Draw;
                this.vykresľovaťŠedoToolStripMenuItem.Checked = false;
                this.nevykresľovaťToolStripMenuItem.Checked = false;
            }
        }

        /// <summary>
        /// Procedure for settings by clicking on particular item in menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void vykresľovaťŠedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.vykresľovaťToolStripMenuItem.Checked || this.nevykresľovaťToolStripMenuItem.Checked)
            {
                secondObjectsInConstruction = DrawingObjects.DrawGrey;
                this.vykresľovaťToolStripMenuItem.Checked = false;
                this.nevykresľovaťToolStripMenuItem.Checked = false;
            }
        }

        /// <summary>
        /// Procedure for settings by clicking on particular item in menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nevykresľovaťToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.vykresľovaťŠedoToolStripMenuItem.Checked || this.vykresľovaťToolStripMenuItem.Checked)
            {
                secondObjectsInConstruction = DrawingObjects.NotDraw;
                this.vykresľovaťŠedoToolStripMenuItem.Checked = false;
                this.vykresľovaťToolStripMenuItem.Checked = false;
            }
        }

        /// <summary>
        /// Procedure for creating a new macro by clicking on particular item in menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uložiťNovéMakroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savingOfMacro = true;
            inputInSavingMacro = null;
            outputInSavingMacro = null;
            commandInSavingMacro = null;
            descriptionInSavingMacro = "";
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = true;
            button4.Visible = true;
            currentPart = Macros.PartsOfMacro.Name;
            textBox.Size = new Size(pictureBox.Location.X + pictureBox.Size.Width - textBox.Location.X - button3.Size.Width, pictureBox.Location.Y + pictureBox.Size.Height - textBox.Location.Y);
            textBox.Clear();
            textBox.Text = "Názov makra:\r\n";
            button.Text = "ULOŽ NÁZOV MAKRA";
            this.BackColor = Color.Gold;
            this.menu.Visible = false;
        }

        /// <summary>
        /// Procedure for printing of all macros by clicking on particular item in menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void vypísaťVšetkyMakráToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printedAllMacros = true;
            button.Text = "OK";
            button1.Visible = false;
            button2.Visible = false;
            // Macros are in folder which name is saved as pathOfFile.
            string[] files = Directory.GetFiles(pathOfFileWithMacros);
            textBox.Clear();
            textBox.Size = new Size(pictureBox.Location.X + pictureBox.Size.Width - textBox.Location.X, pictureBox.Location.Y + pictureBox.Size.Height - textBox.Location.Y);

            foreach (string f in files)
            {
                lines = File.ReadAllLines(f);
                List<string> inps = new List<string>();
                List<string> outps = new List<string>();
                int index = 0;
                int indexspace = 0;
                string description = "";
                Macros.PartsOfMacro current = Macros.PartsOfMacro.Inputs;
                foreach (string l in lines)
                {
                    if (string.IsNullOrWhiteSpace(l))
                    {
                        current++;
                    }
                    else if (current == Macros.PartsOfMacro.Inputs)
                    {
                        if (l.Contains(','))
                        {
                            indexspace = l.IndexOf(' ');
                            char[] sep = { '(', ')', ',', '|' };
                            var points = l.Substring(indexspace + 1).Split(sep, StringSplitOptions.RemoveEmptyEntries);
                            string name = l[indexspace + 1].ToString();
                            foreach (string p in points)
                            {
                                index = p.IndexOf('_') + 1;
                                name += p.Substring(index) + ',';
                            }
                            name = name.Substring(0, name.Length - 1) + l[l.Length - 1];
                            inps.Add(l.Substring(0, indexspace) + ' ' + name);
                        }
                        else
                        {
                            index = l.IndexOf('_') + 1;
                            indexspace = l.IndexOf(' ');
                            inps.Add(l.Substring(0, indexspace) + ' ' + l.Substring(index));
                        }
                    }
                    else if (current == Macros.PartsOfMacro.Outputs)
                    {
                        if (l.Contains(','))
                        {
                            indexspace = l.IndexOf(' ');
                            char[] sep = { '(', ')', ',', '|' };
                            var points = l.Substring(indexspace + 1).Split(sep, StringSplitOptions.RemoveEmptyEntries);
                            string name = l[indexspace + 1].ToString();
                            foreach (string p in points)
                            {
                                index = p.IndexOf('_') + 1;
                                name += p.Substring(index) + ',';
                            }
                            name = name.Substring(0, name.Length - 1) + l[l.Length - 1];
                            outps.Add(l.Substring(0, indexspace) + ' ' + name);
                        }
                        else
                        {
                            index = l.IndexOf('_') + 1;
                            indexspace = l.IndexOf(' ');
                            outps.Add(l.Substring(0, indexspace) + ' ' + l.Substring(index));
                        }
                    }
                    else if (current == Macros.PartsOfMacro.Description)
                    {
                        description = l;
                    }
                }
                string inp = "";
                foreach (string i in inps)
                {
                    inp += ";" + i;
                }
                string outp = "";
                foreach (string o in outps)
                {
                    outp += ";" + o;
                }
                if (inp.Length > 0)
                    inp = inp.Substring(1);
                if (outp.Length > 0)
                    outp = outp.Substring(1);
                if (description.Length > 0)
                    description = " - " + description;
                textBox.AppendText((f + '\n').Substring(pathOfFileWithMacros.Length)+"("+inp+"-"+outp+")"+description+'\n');
            }
        }

        /// <summary>
        /// Procedure for clicking on button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, EventArgs e)
        {
            // All macros are printed, user confirms that he saw them and returns all to original shape.
            if (printedAllMacros)
            {
                textBox.Clear();
                textBox.Size = new Size(sizeOfTextBoxWidth, textBox.Size.Height);
                button1.Visible = true;
                button2.Visible = true;
                printedAllMacros = false;
                pictureBox.Enabled = true;
                if (printConstructionEnBlock)
                    button.Text = "VYKRESLIŤ KONŠTRUKCIU";
                else
                    button.Text = "VYKRESLIŤ PRVÝ KROK KONŠTRUKCIE";
            }
            // Creating and saving macro step by step, on the end returns to original shape.
            else if (savingOfMacro)
            {
                string[] lines = textBox.Lines;
                char[] sepspace = { ' ' }; 
                if (currentPart == Macros.PartsOfMacro.Name)
                {
                    if (lines.Length != 2 || lines[1].Split(sepspace).Length != 1)
                    {
                        MessageBox.Show("Nie je zadaný žiadny správny názov makra.");
                    }
                    else
                    {
                        nameOfMacro = lines[1];
                        currentPart++;
                        textBox.Clear();
                        if (inputInSavingMacro == null)
                            textBox.Text = "Vstupné parametre makra v tvare \"typ_objektu meno_objektu\":\r\n";
                        else
                        {
                            string temp = "";
                            foreach (var l in inputInSavingMacro)
                            {
                                temp += l + "\r\n";
                            }
                            textBox.Text = temp;
                        }
                        button.Text = "ULOŽ VSTUPNÉ PARAMETRE";
                    }
                }        
                else if (currentPart == Macros.PartsOfMacro.Inputs)
                {
                    if (lines.Length > 0)
                    {
                        string[] templines = new string[lines.Length - 1];
                        for (int i = 1; i < lines.Length; i++)
                        {
                            templines[i - 1] = lines[i];
                        }
                        lines = templines;
                    }
                    tempVariables = Macros.SaveMacro(nameOfMacro, lines, out inputsLines, currentPart, new Dictionary<string, string>());
                    if (tempVariables != null)
                    {
                        currentPart++;
                        textBox.Clear();
                        if (commandInSavingMacro == null)
                            textBox.Text = "Príkazy makra:\r\n";
                        else
                        {
                            string temp = "";
                            foreach (var l in commandInSavingMacro)
                            {
                                temp += l + "\r\n";
                            }
                            textBox.Text = temp;
                        }
                        button.Text = "ULOŽ PRÍKAZY MAKRA";
                    }
                }
                else if (currentPart == Macros.PartsOfMacro.Commands)
                {
                    if (lines.Length > 0)
                    {
                        string[] templines = new string[lines.Length - 1];
                        for (int i = 1; i < lines.Length; i++)
                        {
                            templines[i - 1] = lines[i];
                        }
                        lines = templines;
                    }
                    tempVariables = new Dictionary<string, string>();
                    string[] help = new string[0];
                    if (inputInSavingMacro != null && inputInSavingMacro.Length > 0)
                        tempVariables = Macros.SaveMacro(nameOfMacro, inputInSavingMacro.Skip(1).ToArray(), out inputsLines, Macros.PartsOfMacro.Inputs, new Dictionary<string, string>());
                    else
                        tempVariables = Macros.SaveMacro(nameOfMacro, help, out inputsLines, Macros.PartsOfMacro.Inputs, new Dictionary<string, string>());
                    var tempTempVariables = new Dictionary<string, string>(tempVariables);
                    var ttempVariables = Macros.SaveMacro(nameOfMacro, lines, out commandLines, currentPart, tempTempVariables);
                    if (ttempVariables != null)
                    {
                        tempVariables = ttempVariables;
                        currentPart++;
                        textBox.Clear();
                        if (outputInSavingMacro == null)
                            textBox.Text = "Výstupné parametre makra v tvare \"typ_objektu meno_objektu\":\r\n";
                        else
                        {
                            string temp = "";
                            foreach (var l in outputInSavingMacro)
                            {
                                temp += l + "\r\n";
                            }
                            textBox.Text = temp;
                        }
                        button.Text = "ULOŽ VÝSTUPNÉ PARAMETRE";
                    }
                }
                else if (currentPart == Macros.PartsOfMacro.Outputs)
                {
                    if (lines.Length > 0)
                    {
                        string[] templines = new string[lines.Length - 1];
                        for (int i = 1; i < lines.Length; i++)
                        {
                            templines[i - 1] = lines[i];
                        }
                        lines = templines;
                    }
                    var tempTempVariables = new Dictionary<string, string>(tempVariables);
                    var ttempVariables = Macros.SaveMacro(nameOfMacro, lines, out outputsLines, currentPart, tempTempVariables);
                    if (ttempVariables != null)
                    {
                        tempVariables = ttempVariables;
                        currentPart++;
                        textBox.Clear();
                        if (descriptionInSavingMacro == "")
                            textBox.Text = "Popis makra na jednom riadku:\r\n";
                        else
                            textBox.Text = descriptionInSavingMacro;
                        button.Text = "ULOŽ POPIS MAKRA";
                    }
                }
                else if (currentPart == Macros.PartsOfMacro.Description)
                {
                    string[] newLine = { "" };
                    if (lines.Length < 2)
                    {
                        File.WriteAllLines(pathOfFileWithMacros + nameOfMacro, ConcatArrays(inputsLines,newLine,commandLines,newLine,outputsLines));
                    }
                    else
                    {
                        string[] l1 = { lines[1] };
                        File.WriteAllLines(pathOfFileWithMacros + nameOfMacro, ConcatArrays(inputsLines, newLine, commandLines, newLine, outputsLines, newLine, l1));
                        currentPart++;
                    }
                    textBox.Clear();
                    textBox.Size = new Size(sizeOfTextBoxWidth, textBox.Size.Height);
                    pictureBox.Enabled = true;
                    savingOfMacro = false;
                    button1.Visible = true;
                    button2.Visible = true;
                    button3.Visible = false;
                    button4.Visible = false;
                    if (printConstructionEnBlock)
                        button.Text = "VYKRESLIŤ KONŠTRUKCIU";
                    else
                        button.Text = "VYKRESLIŤ PRVÝ KROK KONŠTRUKCIE";
                    this.BackColor = Color.Lavender;
                    this.menu.Visible = true;
                    MessageBox.Show("Makro " + nameOfMacro + " bolo uložené.");
                }
            }
            // Printing of construction according settings in menu.
            else if (printConstructionEnBlock == false && button.Text == "VYPÍSAŤ ĎALŠÍ KROK KONŠTRUKCIE")
            {
                if (lines.Length == currentLine + 1)
                {
                    textBox.AppendText('\n' + lines[currentLine]);
                    currentLine = 0;
                    if (drawConstructionEnBlock)
                        button.Text = "VYKRESLIŤ KONŠTRUKCIU";
                    else
                        button.Text = "VYKRESLIŤ PRVÝ KROK KONŠTRUKCIE";
                }
                else
                {
                    textBox.AppendText('\n'+lines[currentLine]);
                    currentLine++;
                }
            }
            // Drawing of all commands in construction.
            else
            {
                if (drawConstructionEnBlock)
                {
                    //Clears pictureBox_construction and prepares it for a new construction.
                    pictureBox.Enabled = false;
                    pictureBox.Enabled = true;

                    lines = textBox.Lines;

                    // Clears geometric objects in list and prepares it for a new construction.
                    Reader.allObjects.Clear();
                    Reader.counter = 0;
                    Reader.noError = true;

                    int tempi = 0;
                    foreach (string l in lines)
                    {
                        // Works with each line in process of construction.
                        tempi++;
                        if (!(tempi == lines.Length && l == ""))
                            Reader.ReadLine(l);
                        if (Reader.noError == false)
                            break;
                    }
                }
                else
                {
                    if (button.Text == "VYKRESLIŤ PRVÝ KROK KONŠTRUKCIE")
                    {
                        //Clears pictureBox_construction and prepares it for a new construction.
                        pictureBox.Enabled = false;
                        pictureBox.Enabled = true;

                        lines = textBox.Lines;

                        // Clears geometric objects in list and prepares it for a new construction.
                        Reader.allObjects.Clear();
                        Reader.counter = 0;
                        Reader.noError = true;

                        currentLine = 0;
                        button.Text = "VYKRESLIŤ ĎALŠÍ KROK KONŠTRUKCIE";
                    }
                    // Works with each line in process of construction.
                    if (lines.Length == currentLine + 1)
                    {
                        button.Text = "VYKRESLIŤ PRVÝ KROK KONŠTRUKCIE";
                    }
                    Reader.noError = true;
                    Reader.ReadLine(lines[currentLine]);
                    if (Reader.noError != false)
                        currentLine++;                    
                }
            }
        }

        /// <summary>
        /// Procedure for settings by clicking on particular item in menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void vykresľovaťToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.vykresľovaťŠedoToolStripMenuItem1.Checked || this.nevykresľovaťToolStripMenuItem1.Checked)
            {
                tempObjectsInMacro = DrawingObjects.Draw;
                this.vykresľovaťŠedoToolStripMenuItem1.Checked = false;
                this.nevykresľovaťToolStripMenuItem1.Checked = false;
            }
        }

        /// <summary>
        /// Procedure for settings by clicking on particular item in menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void vykresľovaťŠedoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.nevykresľovaťToolStripMenuItem1.Checked || this.vykresľovaťToolStripMenuItem1.Checked)
            {
                tempObjectsInMacro = DrawingObjects.DrawGrey;
                this.nevykresľovaťToolStripMenuItem1.Checked = false;
                this.vykresľovaťToolStripMenuItem1.Checked = false;
            }
        }

        /// <summary>
        /// Procedure for settings by clicking on particular item in menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nevykresľovaťToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.vykresľovaťŠedoToolStripMenuItem1.Checked || this.vykresľovaťToolStripMenuItem1.Checked)
            {
                tempObjectsInMacro = DrawingObjects.NotDraw;
                this.vykresľovaťŠedoToolStripMenuItem1.Checked = false;
                this.vykresľovaťToolStripMenuItem1.Checked = false;
            }
        }

        /// <summary>
        /// Procedure for moving with mouse on pictureBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                int deltaX = MousePosition.X - oldX;
                int deltaY = MousePosition.Y - oldY;

                Drawing.xForModifying += deltaX;
                Drawing.yForModyfying -= deltaY;

                oldX = MousePosition.X;
                oldY = MousePosition.Y;

                pictureBox.Enabled = false;
                pictureBox.Enabled = true;
                foreach (GeometricObject o in Reader.allObjects)
                {
                    Drawing.DrawObject(o);
                }
                foreach (GeometricObject o in Reader.allObjects)
                {
                    if (o.GetFirstName() == o.GetSecondName())
                        Drawing.DrawObject(o);
                }
            }             
        }

        /// <summary>
        /// Procedure for mouse pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!isMouseDown)
                {
                    oldX = MousePosition.X;
                    oldY = MousePosition.Y;
                }
                isMouseDown = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                Drawing.xForModifying = 0;
                Drawing.yForModyfying = 0;
                Drawing.zoomForModifying = 1;

                pictureBox.Enabled = false;
                pictureBox.Enabled = true;
                foreach (GeometricObject o in Reader.allObjects)
                {
                    Drawing.DrawObject(o);
                }
                foreach (GeometricObject o in Reader.allObjects)
                {
                    if (o.GetFirstName() == o.GetSecondName())
                        Drawing.DrawObject(o);
                }
            }
            
        }
        
        /// <summary>
        /// Procedure for mouse realised.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        /// <summary>
        /// Procedure for mouse wheel scrolled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_OnScroll(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                Drawing.zoomForModifying *= constantForZoom;
                Drawing.xForModifying = e.Location.X + (Drawing.xForModifying - e.Location.X) * constantForZoom;
                Drawing.yForModyfying = (GetPictureBox().Height - e.Location.Y) + (Drawing.yForModyfying - (GetPictureBox().Height - e.Location.Y)) * constantForZoom;
            }
            if (e.Delta < 0)
            {
                Drawing.zoomForModifying /= constantForZoom;
                Drawing.xForModifying = e.Location.X + (Drawing.xForModifying - e.Location.X) * (1 / constantForZoom);
                Drawing.yForModyfying = GetPictureBox().Height - e.Location.Y + (Drawing.yForModyfying - (GetPictureBox().Height - e.Location.Y)) * (1 / constantForZoom);
            }
            pictureBox.Enabled = false;
            pictureBox.Enabled = true;
            foreach (GeometricObject o in Reader.allObjects)
            {
                Drawing.DrawObject(o);
            }
            foreach (GeometricObject o in Reader.allObjects)
            {
                if (o.GetFirstName() == o.GetSecondName())
                    Drawing.DrawObject(o);
            }
        }

        /// <summary>
        /// Pseudo-button for new point, it starts a new form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!tempmouseDown)
                {
                    pictureBox3.Image = Properties.Resources.pointsmall;
                }
                tempmouseDown = true;
            }
        }

        /// <summary>
        /// Pseudo-button for new point, it starts a new form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            bool isAbove = e.X <= pictureBox3.Size.Width && e.X >= 0;
            isAbove = isAbove && e.Y <= pictureBox3.Size.Height && e.Y >= 0;

            if (isAbove && tempmouseDown)
            {
                pictureBox3.Image = Properties.Resources.point;
                tempmouseDown = false;

                this.Enabled = false;
                FormForPoint ffp = new FormForPoint();
                ffp.ShowDialog(this);

                if (!ffp.error)
                    textBox.AppendText(ffp.text + "\r\n");
            }
            else if (tempmouseDown)
            {
                pictureBox3.Image = Properties.Resources.point;
                tempmouseDown = false;
            }
        }

        /// <summary>
        /// Pseudo-button for new line, it starts a new form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!tempmouseDown)
                {
                    pictureBox1.Image = Properties.Resources.linesmall;
                }
                tempmouseDown = true;
            }
        }

        /// <summary>
        /// Pseudo-button for new line, it starts a new form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            bool isAbove = e.X <= pictureBox1.Size.Width && e.X >= 0;
            isAbove = isAbove && e.Y <= pictureBox1.Size.Height && e.Y >= 0;

            if (isAbove && tempmouseDown)
            {
                pictureBox1.Image = Properties.Resources.line;
                tempmouseDown = false;

                this.Enabled = false;
                FormForLine ffl = new FormForLine(TypeOfLine.line);
                ffl.ShowDialog(this);

                if (!ffl.error)
                    textBox.AppendText(ffl.text + "\r\n");
            }
            else if (tempmouseDown)
            {
                pictureBox1.Image = Properties.Resources.line;
                tempmouseDown = false;
            }
        }

        /// <summary>
        /// Pseudo-button for new line segment, it starts a new form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!tempmouseDown)
                {
                    pictureBox2.Image = Properties.Resources.linesegmentsmall;
                }
                tempmouseDown = true;
            }
        }

        /// <summary>
        /// Pseudo-button for new line segment, it starts a new form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            bool isAbove = e.X <= pictureBox2.Size.Width && e.X >= 0;
            isAbove = isAbove && e.Y <= pictureBox2.Size.Height && e.Y >= 0;

            if (isAbove && tempmouseDown)
            {
                pictureBox2.Image = Properties.Resources.linesegment;
                tempmouseDown = false;

                this.Enabled = false;
                FormForLine ffl = new FormForLine(TypeOfLine.lineSegment);
                ffl.ShowDialog(this);

                if (!ffl.error)
                    textBox.AppendText(ffl.text + "\r\n");
            }
            else if (tempmouseDown)
            {
                pictureBox2.Image = Properties.Resources.linesegment;
                tempmouseDown = false;
            }
        }

        /// <summary>
        /// Pseudo-button for new half line segment, it starts a new form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!tempmouseDown)
                {
                    pictureBox4.Image = Properties.Resources.halflinesmall;
                }
                tempmouseDown = true;
            }
        }

        /// <summary>
        /// Pseudo-button for new half line segment, it starts a new form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            bool isAbove = e.X <= pictureBox4.Size.Width && e.X >= 0;
            isAbove = isAbove && e.Y <= pictureBox4.Size.Height && e.Y >= 0;

            if (isAbove && tempmouseDown)
            {
                pictureBox4.Image = Properties.Resources.halfline;
                tempmouseDown = false;

                this.Enabled = false;
                FormForLine ffl = new FormForLine(TypeOfLine.halfLine);
                ffl.ShowDialog(this);

                if (!ffl.error)
                    textBox.AppendText(ffl.text + "\r\n");
            }
            else if (tempmouseDown)
            {
                pictureBox4.Image = Properties.Resources.halfline;
                tempmouseDown = false;
            }
        }

        /// <summary>
        /// Pseudo-button for new circle, it starts a new form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox5_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!tempmouseDown)
                {
                    pictureBox5.Image = Properties.Resources.circlesmall;
                }
                tempmouseDown = true;
            }
        }

        /// <summary>
        /// Pseudo-button for new circle, it starts a new form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox5_MouseUp(object sender, MouseEventArgs e)
        {
            bool isAbove = e.X <= pictureBox5.Size.Width && e.X >= 0;
            isAbove = isAbove && e.Y <= pictureBox5.Size.Height && e.Y >= 0;

            if (isAbove && tempmouseDown)
            {
                pictureBox5.Image = Properties.Resources.circle;
                tempmouseDown = false;

                this.Enabled = false;
                FormForCircle ffc = new FormForCircle(TypeOfCircle.full);
                ffc.ShowDialog(this);

                if (!ffc.error)
                    textBox.AppendText(ffc.text + "\r\n");
            }
            else if (tempmouseDown)
            {
                pictureBox5.Image = Properties.Resources.circle;
                tempmouseDown = false;
            }
        }

        /// <summary>
        /// Pseudo-button for new part circle, it starts a new form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox6_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!tempmouseDown)
                {
                    pictureBox6.Image = Properties.Resources.partofcirclesmall;
                }
                tempmouseDown = true;
            }
        }

        /// <summary>
        /// Pseudo-button for new part circle, it starts a new form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox6_MouseUp(object sender, MouseEventArgs e)
        {
            bool isAbove = e.X <= pictureBox6.Size.Width && e.X >= 0;
            isAbove = isAbove && e.Y <= pictureBox6.Size.Height && e.Y >= 0;

            if (isAbove && tempmouseDown)
            {
                pictureBox6.Image = Properties.Resources.partofcircle;
                tempmouseDown = false;

                this.Enabled = false;
                FormForCircle ffc = new FormForCircle(TypeOfCircle.partial);
                ffc.ShowDialog(this);

                if (!ffc.error)
                    textBox.AppendText(ffc.text + "\r\n");
            }
            else if (tempmouseDown)
            {
                pictureBox6.Image = Properties.Resources.partofcircle;
                tempmouseDown = false;
            }
        }

        /// <summary>
        /// Pseudo-button for new angle, it starts a new form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox7_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!tempmouseDown)
                {
                    pictureBox7.Image = Properties.Resources.anglesmall;
                }
                tempmouseDown = true;
            }
        }

        /// <summary>
        /// Pseudo-button for new angle, it starts a new form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox7_MouseUp(object sender, MouseEventArgs e)
        {
            bool isAbove = e.X <= pictureBox7.Size.Width && e.X >= 0;
            isAbove = isAbove && e.Y <= pictureBox7.Size.Height && e.Y >= 0;

            if (isAbove && tempmouseDown)
            {
                pictureBox7.Image = Properties.Resources.angle;
                tempmouseDown = false;

                this.Enabled = false;
                FormForAngle ffa = new FormForAngle();
                ffa.ShowDialog(this);

                if (!ffa.error)
                    textBox.AppendText(ffa.text + "\r\n");
            }
            else if (tempmouseDown)
            {
                pictureBox7.Image = Properties.Resources.angle;
                tempmouseDown = false;
            }
        }

        /// <summary>
        /// Pseudo-button for new macro, it starts a new form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox8_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!tempmouseDown)
                {
                    pictureBox8.Image = Properties.Resources.macrossmall;
                }
                tempmouseDown = true;
            }
        }

        /// <summary>
        /// Pseudo-button for new macro, it starts a new form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox8_MouseUp(object sender, MouseEventArgs e)
        {
            bool isAbove = e.X <= pictureBox8.Size.Width && e.X >= 0;
            isAbove = isAbove && e.Y <= pictureBox8.Size.Height && e.Y >= 0;

            if (isAbove && tempmouseDown)
            {
                pictureBox8.Image = Properties.Resources.macros;
                tempmouseDown = false;

                this.Enabled = false;
                FormForMacros ffm = new FormForMacros();
                ffm.ShowDialog(this);

                if (!ffm.error)
                    textBox.AppendText(ffm.text + "\r\n");
            }
            else if (tempmouseDown)
            {
                pictureBox8.Image = Properties.Resources.macros;
                tempmouseDown = false;
            }
        }

        /// <summary>
        /// Procedure for start a thread when mouse is put down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            ThreadStart del = new ThreadStart(ZoomIn);
            t = new Thread(del);
            t.Start();
        }

        /// <summary>
        /// Procedure for abortion of thread when mouse is taken away.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            t.Abort();
        }

        /// <summary>
        /// Zooming in of canvas.
        /// </summary>
        private void ZoomIn()
        {
            while (true)
            {
                System.Drawing.Point center = new System.Drawing.Point(pictureBox.Size.Width / 2, pictureBox.Size.Height / 2);
                Drawing.zoomForModifying /= constantForZoom;
                Drawing.xForModifying = center.X + (Drawing.xForModifying - center.X) * (1 / constantForZoom);
                Drawing.yForModyfying = GetPictureBox().Height - center.Y + (Drawing.yForModyfying - (GetPictureBox().Height - center.Y)) * (1 / constantForZoom);

                Drawing.ClearCanvas(pictureBox.Size.Width, pictureBox.Size.Height);
                foreach (GeometricObject o in Reader.allObjects)
                {
                    Drawing.DrawObject(o);
                }
                foreach (GeometricObject o in Reader.allObjects)
                {
                    if (o.GetFirstName() == o.GetSecondName())
                        Drawing.DrawObject(o);
                }
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Zooming out of canvas.
        /// </summary>
        private void ZoomOut()
        {
            while (true)
            {
                System.Drawing.Point center = new System.Drawing.Point(pictureBox.Size.Width / 2, pictureBox.Size.Height / 2);
                Drawing.zoomForModifying *= constantForZoom;
                Drawing.xForModifying = center.X + (Drawing.xForModifying - center.X) * constantForZoom;
                Drawing.yForModyfying = (GetPictureBox().Height - center.Y) + (Drawing.yForModyfying - (GetPictureBox().Height - center.Y)) * constantForZoom;

                Drawing.ClearCanvas(pictureBox.Size.Width, pictureBox.Size.Height);
                foreach (GeometricObject o in Reader.allObjects)
                {
                    Drawing.DrawObject(o);
                }
                foreach (GeometricObject o in Reader.allObjects)
                {
                    if (o.GetFirstName() == o.GetSecondName())
                        Drawing.DrawObject(o);
                }
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Procedure for start of thread when mouse is put down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            ThreadStart del = new ThreadStart(ZoomOut);
            t = new Thread(del);
            t.Start();
        }

        /// <summary>
        /// Procedure for abbortion of thread when mouse is taken away.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            t.Abort();
        }
        
        /// <summary>
        /// Button for back step during the saving of the macro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            string temp = "";
            switch (currentPart)
            {
                case Macros.PartsOfMacro.Name:
                    button4_Click(sender, e);
                    break;
                case Macros.PartsOfMacro.Inputs:
                    currentPart--;
                    textBox.Clear();
                    textBox.Text = "Názov makra:\r\n" + nameOfMacro;
                    button.Text = "ULOŽ NÁZOV MAKRA";
                    break;
                case Macros.PartsOfMacro.Commands:
                    currentPart--;
                    textBox.Clear();
                    foreach (var l in inputInSavingMacro)
                    {
                        temp += l + "\r\n";
                    }
                    textBox.Text = temp;
                    tempVariables = new Dictionary<string, string>();
                    button.Text = "ULOŽ VSTUPNÉ PARAMETRE";
                    break;
                case Macros.PartsOfMacro.Outputs:
                    currentPart--;
                    textBox.Clear();
                    foreach (var l in commandInSavingMacro)
                    {
                        temp += l + "\r\n";
                    }
                    tempVariables = new Dictionary<string, string>();
                    string[] help = new string[0];
                    if (inputInSavingMacro != null && inputInSavingMacro.Length > 0)
                        tempVariables = Macros.SaveMacro(nameOfMacro, inputInSavingMacro.Skip(1).ToArray(), out inputsLines, Macros.PartsOfMacro.Inputs, new Dictionary<string, string>());
                    else
                        tempVariables = Macros.SaveMacro(nameOfMacro, help, out inputsLines, Macros.PartsOfMacro.Inputs, new Dictionary<string, string>());
                    textBox.Text = temp;
                    button.Text = "ULOŽ PRÍKAZY MAKRA";
                    break;
                case Macros.PartsOfMacro.Description:
                    currentPart--;
                    textBox.Clear();
                    foreach (var l in outputInSavingMacro)
                    {
                        temp += l + "\r\n";
                    }
                    textBox.Text = temp;
                    button.Text = "ULOŽ VÝSTUPNÉ PARAMETRE";
                    break;
            }
        }

        /// <summary>
        /// Procedure for canceling the saving of new macro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            textBox.Clear();
            textBox.Size = new Size(sizeOfTextBoxWidth, textBox.Size.Height);
            pictureBox.Enabled = true;
            savingOfMacro = false;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = false;
            button4.Visible = false;
            if (printConstructionEnBlock)
                button.Text = "VYKRESLIŤ KONŠTRUKCIU";
            else
                button.Text = "VYKRESLIŤ PRVÝ KROK KONŠTRUKCIE";
            this.BackColor = Color.Lavender;
            this.menu.Visible = true;
        }
        
        /// <summary>
        /// Procedure for saving lines from saving the macro when they are changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (savingOfMacro)
            {
                switch (currentPart)
                {
                    case Macros.PartsOfMacro.Inputs:
                        inputInSavingMacro = textBox.Lines;
                        break;
                    case Macros.PartsOfMacro.Commands:
                        commandInSavingMacro = textBox.Lines;
                        break;
                    case Macros.PartsOfMacro.Outputs:
                        outputInSavingMacro = textBox.Lines;
                        break;
                    case Macros.PartsOfMacro.Description:
                        descriptionInSavingMacro = textBox.Text;
                        break;
                }
            }
        }
    }
}
