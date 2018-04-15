using System.Windows.Forms;

namespace Visualization
{
    partial class Visualizer
    {
        /// <summary>
        /// Required constructioner variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form constructioner generated code

        /// <summary>
        /// Required method for constructioner support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Visualizer));
            this.textBox = new System.Windows.Forms.TextBox();
            this.button = new System.Windows.Forms.Button();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.konštrukciaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uložiťKonštrukciuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.načítaťKonštrukciuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.nastaveniaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.načítavanieKonštrukcieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.postupneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.narazToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vykresľovanieKonštrukcieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.postupneToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.narazToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ďalšieBodyAObjektyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vykresľovaťToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vykresľovaťŠedoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nevykresľovaťToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uložiťNovéMakroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vypísaťVšetkyMakráToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.nastaveniaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ďalšieObjektyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vykresľovaťToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.vykresľovaťŠedoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.nevykresľovaťToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.menu.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox.Location = new System.Drawing.Point(152, 42);
            this.textBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox.Size = new System.Drawing.Size(354, 522);
            this.textBox.TabIndex = 13;
            this.textBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyUp);
            // 
            // button
            // 
            this.button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button.BackColor = System.Drawing.Color.MintCream;
            this.button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button.ForeColor = System.Drawing.Color.Black;
            this.button.Location = new System.Drawing.Point(8, 574);
            this.button.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(1259, 69);
            this.button.TabIndex = 14;
            this.button.Text = "VYKRESLIŤ KONŠTRUKCIU";
            this.button.UseVisualStyleBackColor = false;
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.Lavender;
            this.menu.ForeColor = System.Drawing.Color.Red;
            this.menu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.konštrukciaToolStripMenuItem,
            this.makroToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menu.Size = new System.Drawing.Size(1280, 35);
            this.menu.TabIndex = 15;
            this.menu.Text = "menu";
            // 
            // konštrukciaToolStripMenuItem
            // 
            this.konštrukciaToolStripMenuItem.BackColor = System.Drawing.Color.Lavender;
            this.konštrukciaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uložiťKonštrukciuToolStripMenuItem,
            this.načítaťKonštrukciuToolStripMenuItem,
            this.toolStripSeparator1,
            this.nastaveniaToolStripMenuItem});
            this.konštrukciaToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.konštrukciaToolStripMenuItem.Name = "konštrukciaToolStripMenuItem";
            this.konštrukciaToolStripMenuItem.Size = new System.Drawing.Size(115, 29);
            this.konštrukciaToolStripMenuItem.Text = "Konštrukcia";
            // 
            // uložiťKonštrukciuToolStripMenuItem
            // 
            this.uložiťKonštrukciuToolStripMenuItem.BackColor = System.Drawing.Color.Lavender;
            this.uložiťKonštrukciuToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.uložiťKonštrukciuToolStripMenuItem.Name = "uložiťKonštrukciuToolStripMenuItem";
            this.uložiťKonštrukciuToolStripMenuItem.Size = new System.Drawing.Size(250, 30);
            this.uložiťKonštrukciuToolStripMenuItem.Text = "Uložiť konštrukciu";
            this.uložiťKonštrukciuToolStripMenuItem.Click += new System.EventHandler(this.uložiťKonštrukciuToolStripMenuItem_Click);
            // 
            // načítaťKonštrukciuToolStripMenuItem
            // 
            this.načítaťKonštrukciuToolStripMenuItem.BackColor = System.Drawing.Color.Lavender;
            this.načítaťKonštrukciuToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.načítaťKonštrukciuToolStripMenuItem.Name = "načítaťKonštrukciuToolStripMenuItem";
            this.načítaťKonštrukciuToolStripMenuItem.Size = new System.Drawing.Size(250, 30);
            this.načítaťKonštrukciuToolStripMenuItem.Text = "Načítať konštrukciu";
            this.načítaťKonštrukciuToolStripMenuItem.Click += new System.EventHandler(this.načítaťKonštrukciuToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.BackColor = System.Drawing.Color.Lavender;
            this.toolStripSeparator1.ForeColor = System.Drawing.Color.Black;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripSeparator1.Size = new System.Drawing.Size(247, 6);
            // 
            // nastaveniaToolStripMenuItem
            // 
            this.nastaveniaToolStripMenuItem.BackColor = System.Drawing.Color.Lavender;
            this.nastaveniaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.načítavanieKonštrukcieToolStripMenuItem,
            this.vykresľovanieKonštrukcieToolStripMenuItem,
            this.ďalšieBodyAObjektyToolStripMenuItem});
            this.nastaveniaToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.nastaveniaToolStripMenuItem.Name = "nastaveniaToolStripMenuItem";
            this.nastaveniaToolStripMenuItem.Size = new System.Drawing.Size(250, 30);
            this.nastaveniaToolStripMenuItem.Text = "Nastavenia";
            // 
            // načítavanieKonštrukcieToolStripMenuItem
            // 
            this.načítavanieKonštrukcieToolStripMenuItem.BackColor = System.Drawing.Color.Lavender;
            this.načítavanieKonštrukcieToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.postupneToolStripMenuItem,
            this.narazToolStripMenuItem});
            this.načítavanieKonštrukcieToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.načítavanieKonštrukcieToolStripMenuItem.Name = "načítavanieKonštrukcieToolStripMenuItem";
            this.načítavanieKonštrukcieToolStripMenuItem.Size = new System.Drawing.Size(302, 30);
            this.načítavanieKonštrukcieToolStripMenuItem.Text = "Vypisovanie konštrukcie";
            // 
            // postupneToolStripMenuItem
            // 
            this.postupneToolStripMenuItem.BackColor = System.Drawing.Color.Lavender;
            this.postupneToolStripMenuItem.CheckOnClick = true;
            this.postupneToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.postupneToolStripMenuItem.Name = "postupneToolStripMenuItem";
            this.postupneToolStripMenuItem.Size = new System.Drawing.Size(171, 30);
            this.postupneToolStripMenuItem.Text = "Postupne";
            this.postupneToolStripMenuItem.Click += new System.EventHandler(this.postupneToolStripMenuItem_Click);
            // 
            // narazToolStripMenuItem
            // 
            this.narazToolStripMenuItem.BackColor = System.Drawing.Color.Lavender;
            this.narazToolStripMenuItem.Checked = true;
            this.narazToolStripMenuItem.CheckOnClick = true;
            this.narazToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.narazToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.narazToolStripMenuItem.Name = "narazToolStripMenuItem";
            this.narazToolStripMenuItem.Size = new System.Drawing.Size(171, 30);
            this.narazToolStripMenuItem.Text = "Naraz";
            this.narazToolStripMenuItem.Click += new System.EventHandler(this.narazToolStripMenuItem_Click);
            // 
            // vykresľovanieKonštrukcieToolStripMenuItem
            // 
            this.vykresľovanieKonštrukcieToolStripMenuItem.BackColor = System.Drawing.Color.Lavender;
            this.vykresľovanieKonštrukcieToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.postupneToolStripMenuItem1,
            this.narazToolStripMenuItem1});
            this.vykresľovanieKonštrukcieToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.vykresľovanieKonštrukcieToolStripMenuItem.Name = "vykresľovanieKonštrukcieToolStripMenuItem";
            this.vykresľovanieKonštrukcieToolStripMenuItem.Size = new System.Drawing.Size(302, 30);
            this.vykresľovanieKonštrukcieToolStripMenuItem.Text = "Vykresľovanie konštrukcie";
            // 
            // postupneToolStripMenuItem1
            // 
            this.postupneToolStripMenuItem1.BackColor = System.Drawing.Color.Lavender;
            this.postupneToolStripMenuItem1.CheckOnClick = true;
            this.postupneToolStripMenuItem1.ForeColor = System.Drawing.Color.Black;
            this.postupneToolStripMenuItem1.Name = "postupneToolStripMenuItem1";
            this.postupneToolStripMenuItem1.Size = new System.Drawing.Size(171, 30);
            this.postupneToolStripMenuItem1.Text = "Postupne";
            this.postupneToolStripMenuItem1.Click += new System.EventHandler(this.postupneToolStripMenuItem1_Click);
            // 
            // narazToolStripMenuItem1
            // 
            this.narazToolStripMenuItem1.BackColor = System.Drawing.Color.Lavender;
            this.narazToolStripMenuItem1.Checked = true;
            this.narazToolStripMenuItem1.CheckOnClick = true;
            this.narazToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.narazToolStripMenuItem1.ForeColor = System.Drawing.Color.Black;
            this.narazToolStripMenuItem1.Name = "narazToolStripMenuItem1";
            this.narazToolStripMenuItem1.Size = new System.Drawing.Size(171, 30);
            this.narazToolStripMenuItem1.Text = "Naraz";
            this.narazToolStripMenuItem1.Click += new System.EventHandler(this.narazToolStripMenuItem1_Click);
            // 
            // ďalšieBodyAObjektyToolStripMenuItem
            // 
            this.ďalšieBodyAObjektyToolStripMenuItem.BackColor = System.Drawing.Color.Lavender;
            this.ďalšieBodyAObjektyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vykresľovaťToolStripMenuItem,
            this.vykresľovaťŠedoToolStripMenuItem,
            this.nevykresľovaťToolStripMenuItem});
            this.ďalšieBodyAObjektyToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.ďalšieBodyAObjektyToolStripMenuItem.Name = "ďalšieBodyAObjektyToolStripMenuItem";
            this.ďalšieBodyAObjektyToolStripMenuItem.Size = new System.Drawing.Size(302, 30);
            this.ďalšieBodyAObjektyToolStripMenuItem.Text = "Ďalšie body a objekty";
            // 
            // vykresľovaťToolStripMenuItem
            // 
            this.vykresľovaťToolStripMenuItem.BackColor = System.Drawing.Color.Lavender;
            this.vykresľovaťToolStripMenuItem.CheckOnClick = true;
            this.vykresľovaťToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.vykresľovaťToolStripMenuItem.Name = "vykresľovaťToolStripMenuItem";
            this.vykresľovaťToolStripMenuItem.Size = new System.Drawing.Size(236, 30);
            this.vykresľovaťToolStripMenuItem.Text = "Vykresľovať";
            this.vykresľovaťToolStripMenuItem.Click += new System.EventHandler(this.vykresľovaťToolStripMenuItem_Click);
            // 
            // vykresľovaťŠedoToolStripMenuItem
            // 
            this.vykresľovaťŠedoToolStripMenuItem.BackColor = System.Drawing.Color.Lavender;
            this.vykresľovaťŠedoToolStripMenuItem.CheckOnClick = true;
            this.vykresľovaťŠedoToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.vykresľovaťŠedoToolStripMenuItem.Name = "vykresľovaťŠedoToolStripMenuItem";
            this.vykresľovaťŠedoToolStripMenuItem.Size = new System.Drawing.Size(236, 30);
            this.vykresľovaťŠedoToolStripMenuItem.Text = "Vykresľovať šedo";
            this.vykresľovaťŠedoToolStripMenuItem.Click += new System.EventHandler(this.vykresľovaťŠedoToolStripMenuItem_Click);
            // 
            // nevykresľovaťToolStripMenuItem
            // 
            this.nevykresľovaťToolStripMenuItem.BackColor = System.Drawing.Color.Lavender;
            this.nevykresľovaťToolStripMenuItem.Checked = true;
            this.nevykresľovaťToolStripMenuItem.CheckOnClick = true;
            this.nevykresľovaťToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.nevykresľovaťToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.nevykresľovaťToolStripMenuItem.Name = "nevykresľovaťToolStripMenuItem";
            this.nevykresľovaťToolStripMenuItem.Size = new System.Drawing.Size(236, 30);
            this.nevykresľovaťToolStripMenuItem.Text = "Nevykresľovať";
            this.nevykresľovaťToolStripMenuItem.Click += new System.EventHandler(this.nevykresľovaťToolStripMenuItem_Click);
            // 
            // makroToolStripMenuItem
            // 
            this.makroToolStripMenuItem.BackColor = System.Drawing.Color.Lavender;
            this.makroToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uložiťNovéMakroToolStripMenuItem,
            this.vypísaťVšetkyMakráToolStripMenuItem,
            this.toolStripSeparator2,
            this.nastaveniaToolStripMenuItem1});
            this.makroToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.makroToolStripMenuItem.Name = "makroToolStripMenuItem";
            this.makroToolStripMenuItem.Size = new System.Drawing.Size(75, 29);
            this.makroToolStripMenuItem.Text = "Makro";
            // 
            // uložiťNovéMakroToolStripMenuItem
            // 
            this.uložiťNovéMakroToolStripMenuItem.BackColor = System.Drawing.Color.Lavender;
            this.uložiťNovéMakroToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.uložiťNovéMakroToolStripMenuItem.Name = "uložiťNovéMakroToolStripMenuItem";
            this.uložiťNovéMakroToolStripMenuItem.Size = new System.Drawing.Size(266, 30);
            this.uložiťNovéMakroToolStripMenuItem.Text = "Vytvoriť nové makro";
            this.uložiťNovéMakroToolStripMenuItem.Click += new System.EventHandler(this.uložiťNovéMakroToolStripMenuItem_Click);
            // 
            // vypísaťVšetkyMakráToolStripMenuItem
            // 
            this.vypísaťVšetkyMakráToolStripMenuItem.BackColor = System.Drawing.Color.Lavender;
            this.vypísaťVšetkyMakráToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.vypísaťVšetkyMakráToolStripMenuItem.Name = "vypísaťVšetkyMakráToolStripMenuItem";
            this.vypísaťVšetkyMakráToolStripMenuItem.Size = new System.Drawing.Size(266, 30);
            this.vypísaťVšetkyMakráToolStripMenuItem.Text = "Vypísať všetky makrá";
            this.vypísaťVšetkyMakráToolStripMenuItem.Click += new System.EventHandler(this.vypísaťVšetkyMakráToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.BackColor = System.Drawing.Color.Lavender;
            this.toolStripSeparator2.ForeColor = System.Drawing.Color.Black;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(263, 6);
            // 
            // nastaveniaToolStripMenuItem1
            // 
            this.nastaveniaToolStripMenuItem1.BackColor = System.Drawing.Color.Lavender;
            this.nastaveniaToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ďalšieObjektyToolStripMenuItem});
            this.nastaveniaToolStripMenuItem1.ForeColor = System.Drawing.Color.Black;
            this.nastaveniaToolStripMenuItem1.Name = "nastaveniaToolStripMenuItem1";
            this.nastaveniaToolStripMenuItem1.Size = new System.Drawing.Size(266, 30);
            this.nastaveniaToolStripMenuItem1.Text = "Nastavenia";
            // 
            // ďalšieObjektyToolStripMenuItem
            // 
            this.ďalšieObjektyToolStripMenuItem.BackColor = System.Drawing.Color.Lavender;
            this.ďalšieObjektyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vykresľovaťToolStripMenuItem1,
            this.vykresľovaťŠedoToolStripMenuItem1,
            this.nevykresľovaťToolStripMenuItem1});
            this.ďalšieObjektyToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.ďalšieObjektyToolStripMenuItem.Name = "ďalšieObjektyToolStripMenuItem";
            this.ďalšieObjektyToolStripMenuItem.Size = new System.Drawing.Size(235, 30);
            this.ďalšieObjektyToolStripMenuItem.Text = "Pomocné objekty";
            // 
            // vykresľovaťToolStripMenuItem1
            // 
            this.vykresľovaťToolStripMenuItem1.BackColor = System.Drawing.Color.Lavender;
            this.vykresľovaťToolStripMenuItem1.CheckOnClick = true;
            this.vykresľovaťToolStripMenuItem1.ForeColor = System.Drawing.Color.Black;
            this.vykresľovaťToolStripMenuItem1.Name = "vykresľovaťToolStripMenuItem1";
            this.vykresľovaťToolStripMenuItem1.Size = new System.Drawing.Size(236, 30);
            this.vykresľovaťToolStripMenuItem1.Text = "Vykresľovať";
            this.vykresľovaťToolStripMenuItem1.Click += new System.EventHandler(this.vykresľovaťToolStripMenuItem1_Click);
            // 
            // vykresľovaťŠedoToolStripMenuItem1
            // 
            this.vykresľovaťŠedoToolStripMenuItem1.BackColor = System.Drawing.Color.Lavender;
            this.vykresľovaťŠedoToolStripMenuItem1.CheckOnClick = true;
            this.vykresľovaťŠedoToolStripMenuItem1.ForeColor = System.Drawing.Color.Black;
            this.vykresľovaťŠedoToolStripMenuItem1.Name = "vykresľovaťŠedoToolStripMenuItem1";
            this.vykresľovaťŠedoToolStripMenuItem1.Size = new System.Drawing.Size(236, 30);
            this.vykresľovaťŠedoToolStripMenuItem1.Text = "Vykresľovať šedo";
            this.vykresľovaťŠedoToolStripMenuItem1.Click += new System.EventHandler(this.vykresľovaťŠedoToolStripMenuItem1_Click);
            // 
            // nevykresľovaťToolStripMenuItem1
            // 
            this.nevykresľovaťToolStripMenuItem1.BackColor = System.Drawing.Color.Lavender;
            this.nevykresľovaťToolStripMenuItem1.Checked = true;
            this.nevykresľovaťToolStripMenuItem1.CheckOnClick = true;
            this.nevykresľovaťToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.nevykresľovaťToolStripMenuItem1.ForeColor = System.Drawing.Color.Black;
            this.nevykresľovaťToolStripMenuItem1.Name = "nevykresľovaťToolStripMenuItem1";
            this.nevykresľovaťToolStripMenuItem1.Size = new System.Drawing.Size(236, 30);
            this.nevykresľovaťToolStripMenuItem1.Text = "Nevykresľovať";
            this.nevykresľovaťToolStripMenuItem1.Click += new System.EventHandler(this.nevykresľovaťToolStripMenuItem1_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Lavender;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.pictureBox3);
            this.flowLayoutPanel1.Controls.Add(this.pictureBox1);
            this.flowLayoutPanel1.Controls.Add(this.pictureBox2);
            this.flowLayoutPanel1.Controls.Add(this.pictureBox4);
            this.flowLayoutPanel1.Controls.Add(this.pictureBox5);
            this.flowLayoutPanel1.Controls.Add(this.pictureBox6);
            this.flowLayoutPanel1.Controls.Add(this.pictureBox7);
            this.flowLayoutPanel1.Controls.Add(this.pictureBox8);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(8, 42);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(134, 522);
            this.flowLayoutPanel1.TabIndex = 16;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(4, 5);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(90, 92);
            this.pictureBox3.TabIndex = 19;
            this.pictureBox3.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox3, "Bod.");
            this.pictureBox3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox3_MouseDown);
            this.pictureBox3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox3_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Visualization.Properties.Resources.line;
            this.pictureBox1.Location = new System.Drawing.Point(4, 107);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(90, 92);
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox1, "Priamka.");
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::Visualization.Properties.Resources.linesegment;
            this.pictureBox2.Location = new System.Drawing.Point(4, 209);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(90, 92);
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox2, "Úsečka.");
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDown);
            this.pictureBox2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseUp);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.Image = global::Visualization.Properties.Resources.halfline;
            this.pictureBox4.Location = new System.Drawing.Point(4, 311);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(90, 92);
            this.pictureBox4.TabIndex = 22;
            this.pictureBox4.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox4, "Polpriamka.");
            this.pictureBox4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox4_MouseDown);
            this.pictureBox4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox4_MouseUp);
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox5.Image = global::Visualization.Properties.Resources.circle;
            this.pictureBox5.Location = new System.Drawing.Point(4, 413);
            this.pictureBox5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(90, 92);
            this.pictureBox5.TabIndex = 23;
            this.pictureBox5.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox5, "Kružnica.");
            this.pictureBox5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox5_MouseDown);
            this.pictureBox5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox5_MouseUp);
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox6.Image = global::Visualization.Properties.Resources.partofcircle;
            this.pictureBox6.Location = new System.Drawing.Point(4, 515);
            this.pictureBox6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(90, 92);
            this.pictureBox6.TabIndex = 24;
            this.pictureBox6.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox6, "Kružnicový oblúk.");
            this.pictureBox6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox6_MouseDown);
            this.pictureBox6.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox6_MouseUp);
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox7.Image = global::Visualization.Properties.Resources.angle;
            this.pictureBox7.Location = new System.Drawing.Point(4, 617);
            this.pictureBox7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(90, 92);
            this.pictureBox7.TabIndex = 25;
            this.pictureBox7.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox7, "Uhol.");
            this.pictureBox7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox7_MouseDown);
            this.pictureBox7.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox7_MouseUp);
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox8.Image = global::Visualization.Properties.Resources.macros;
            this.pictureBox8.Location = new System.Drawing.Point(4, 719);
            this.pictureBox8.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(90, 92);
            this.pictureBox8.TabIndex = 26;
            this.pictureBox8.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox8, "Makro.");
            this.pictureBox8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox8_MouseDown);
            this.pictureBox8.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox8_MouseUp);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.AliceBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(1227, 477);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 40);
            this.button1.TabIndex = 17;
            this.button1.Text = "+";
            this.toolTip1.SetToolTip(this.button1, "Priblížiť nákresňu");
            this.button1.UseVisualStyleBackColor = false;
            this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
            this.button1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button1_MouseUp);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.AliceBlue;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(1227, 523);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(40, 40);
            this.button2.TabIndex = 18;
            this.button2.Text = "-";
            this.toolTip1.SetToolTip(this.button2, "Vzdialiť nákresňu");
            this.button2.UseVisualStyleBackColor = false;
            this.button2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button2_MouseDown);
            this.button2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button2_MouseUp);
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BackColor = System.Drawing.Color.Snow;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox.Location = new System.Drawing.Point(516, 42);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(751, 521);
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.BackColor = System.Drawing.Color.MintCream;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.Location = new System.Drawing.Point(1171, 307);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(96, 259);
            this.button4.TabIndex = 20;
            this.button4.Text = "Zruš ukladanie";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.Color.MintCream;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(1171, 42);
            this.button3.Name = "button3";
            this.button3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button3.Size = new System.Drawing.Size(96, 259);
            this.button3.TabIndex = 19;
            this.button3.Text = "Vráť sa o krok späť";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Visualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(1280, 649);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.button);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.pictureBox);
            this.ForeColor = System.Drawing.Color.White;
            this.MainMenuStrip = this.menu;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Visualizer";
            this.Text = "Vizualizátor";
            this.ResizeEnd += new System.EventHandler(this.Visualizer_ResizeEnd);
            this.Resize += new System.EventHandler(this.Visualizer_ResizeEnd);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox;
        private Button button;
        private MenuStrip menu;
        private ToolStripMenuItem vykresľovanieKonštrukcieToolStripMenuItem;
        private ToolStripMenuItem ďalšieBodyAObjektyToolStripMenuItem;
        private ToolStripMenuItem uložiťNovéMakroToolStripMenuItem;
        private ToolStripMenuItem vypísaťVšetkyMakráToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem nastaveniaToolStripMenuItem1;
        private ToolStripMenuItem makroToolStripMenuItem;
        private ToolStripMenuItem načítavanieKonštrukcieToolStripMenuItem;
        private ToolStripMenuItem nastaveniaToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem načítaťKonštrukciuToolStripMenuItem;
        private ToolStripMenuItem konštrukciaToolStripMenuItem;
        private ToolStripMenuItem ďalšieObjektyToolStripMenuItem;
        private ToolStripMenuItem narazToolStripMenuItem;
        private ToolStripMenuItem postupneToolStripMenuItem;
        private ToolStripMenuItem postupneToolStripMenuItem1;
        private ToolStripMenuItem narazToolStripMenuItem1;
        private ToolStripMenuItem vykresľovaťToolStripMenuItem;
        private ToolStripMenuItem vykresľovaťŠedoToolStripMenuItem;
        private ToolStripMenuItem nevykresľovaťToolStripMenuItem;
        private ToolStripMenuItem vykresľovaťToolStripMenuItem1;
        private ToolStripMenuItem vykresľovaťŠedoToolStripMenuItem1;
        private ToolStripMenuItem nevykresľovaťToolStripMenuItem1;
        private ToolStripMenuItem uložiťKonštrukciuToolStripMenuItem;
        private TextBox textBox;
        private FlowLayoutPanel flowLayoutPanel1;
        private PictureBox pictureBox3;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
        private PictureBox pictureBox6;
        private PictureBox pictureBox7;
        private PictureBox pictureBox8;
        private ToolTip toolTip1;
        private Button button1;
        private Button button2;
        private Button button4;
        private Button button3;
    }
}

