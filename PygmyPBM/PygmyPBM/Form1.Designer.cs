namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripZoom100 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripZoom200 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripZoom300 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripZoom400 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripZoom800 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripZoom1600 = new System.Windows.Forms.ToolStripMenuItem();
            this.rawDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelText = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelImageType = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxFrames = new System.Windows.Forms.ComboBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonNewLine = new System.Windows.Forms.Button();
            this.buttonNewPoly = new System.Windows.Forms.Button();
            this.buttonColor = new System.Windows.Forms.Button();
            this.comboBoxBPP = new System.Windows.Forms.ComboBox();
            this.panelFrames = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 230400;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(586, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsStripMenuItem1,
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsStripMenuItem1
            // 
            this.saveAsStripMenuItem1.Name = "saveAsStripMenuItem1";
            this.saveAsStripMenuItem1.Size = new System.Drawing.Size(114, 22);
            this.saveAsStripMenuItem1.Text = "Save As";
            this.saveAsStripMenuItem1.Click += new System.EventHandler(this.saveAsStripMenuItem1_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomToolStripMenuItem,
            this.rawDataToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // zoomToolStripMenuItem
            // 
            this.zoomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripZoom100,
            this.toolStripZoom200,
            this.toolStripZoom300,
            this.toolStripZoom400,
            this.toolStripZoom800,
            this.toolStripZoom1600});
            this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            this.zoomToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.zoomToolStripMenuItem.Text = "Zoom";
            // 
            // toolStripZoom100
            // 
            this.toolStripZoom100.Name = "toolStripZoom100";
            this.toolStripZoom100.Size = new System.Drawing.Size(108, 22);
            this.toolStripZoom100.Text = "100%";
            this.toolStripZoom100.Click += new System.EventHandler(this.toolStripZoom100_Click);
            // 
            // toolStripZoom200
            // 
            this.toolStripZoom200.Name = "toolStripZoom200";
            this.toolStripZoom200.Size = new System.Drawing.Size(108, 22);
            this.toolStripZoom200.Text = "200%";
            this.toolStripZoom200.Click += new System.EventHandler(this.toolStripZoom200_Click);
            // 
            // toolStripZoom300
            // 
            this.toolStripZoom300.Name = "toolStripZoom300";
            this.toolStripZoom300.Size = new System.Drawing.Size(108, 22);
            this.toolStripZoom300.Text = "300%";
            this.toolStripZoom300.Click += new System.EventHandler(this.toolStripZoom300_Click);
            // 
            // toolStripZoom400
            // 
            this.toolStripZoom400.Name = "toolStripZoom400";
            this.toolStripZoom400.Size = new System.Drawing.Size(108, 22);
            this.toolStripZoom400.Text = "400%";
            this.toolStripZoom400.Click += new System.EventHandler(this.toolStripZoom400_Click);
            // 
            // toolStripZoom800
            // 
            this.toolStripZoom800.Name = "toolStripZoom800";
            this.toolStripZoom800.Size = new System.Drawing.Size(108, 22);
            this.toolStripZoom800.Text = "800%";
            this.toolStripZoom800.Click += new System.EventHandler(this.toolStripZoom800_Click);
            // 
            // toolStripZoom1600
            // 
            this.toolStripZoom1600.Name = "toolStripZoom1600";
            this.toolStripZoom1600.Size = new System.Drawing.Size(108, 22);
            this.toolStripZoom1600.Text = "1600%";
            this.toolStripZoom1600.Click += new System.EventHandler(this.toolStripZoom1600_Click);
            // 
            // rawDataToolStripMenuItem
            // 
            this.rawDataToolStripMenuItem.Name = "rawDataToolStripMenuItem";
            this.rawDataToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.rawDataToolStripMenuItem.Text = "Raw Data";
            this.rawDataToolStripMenuItem.Click += new System.EventHandler(this.rawDataToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabelText,
            this.toolStripStatusLabelImageType});
            this.statusStrip1.Location = new System.Drawing.Point(0, 411);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(586, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(22, 17);
            this.toolStripStatusLabel1.Text = "0,0";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripStatusLabelText
            // 
            this.toolStripStatusLabelText.Name = "toolStripStatusLabelText";
            this.toolStripStatusLabelText.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabelImageType
            // 
            this.toolStripStatusLabelImageType.Name = "toolStripStatusLabelImageType";
            this.toolStripStatusLabelImageType.Size = new System.Drawing.Size(66, 17);
            this.toolStripStatusLabelImageType.Text = "ImageType";
            this.toolStripStatusLabelImageType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "PygmyPBM|*.pbm|All Files|*.*";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.Location = new System.Drawing.Point(0, 75);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(136, 87);
            this.treeView1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(-1, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(0, 0);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(142, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(444, 293);
            this.panel1.TabIndex = 5;
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            // 
            // comboBoxFrames
            // 
            this.comboBoxFrames.FormattingEnabled = true;
            this.comboBoxFrames.Location = new System.Drawing.Point(1, 23);
            this.comboBoxFrames.Name = "comboBoxFrames";
            this.comboBoxFrames.Size = new System.Drawing.Size(135, 21);
            this.comboBoxFrames.TabIndex = 6;
            this.comboBoxFrames.SelectedIndexChanged += new System.EventHandler(this.comboBoxFrames_SelectedIndexChanged);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonNewLine);
            this.panel2.Controls.Add(this.buttonNewPoly);
            this.panel2.Controls.Add(this.buttonColor);
            this.panel2.Location = new System.Drawing.Point(3, 168);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(133, 240);
            this.panel2.TabIndex = 7;
            // 
            // buttonNewLine
            // 
            this.buttonNewLine.Image = ((System.Drawing.Image)(resources.GetObject("buttonNewLine.Image")));
            this.buttonNewLine.Location = new System.Drawing.Point(80, 0);
            this.buttonNewLine.Name = "buttonNewLine";
            this.buttonNewLine.Size = new System.Drawing.Size(40, 40);
            this.buttonNewLine.TabIndex = 2;
            this.buttonNewLine.UseVisualStyleBackColor = true;
            // 
            // buttonNewPoly
            // 
            this.buttonNewPoly.Image = ((System.Drawing.Image)(resources.GetObject("buttonNewPoly.Image")));
            this.buttonNewPoly.Location = new System.Drawing.Point(40, 0);
            this.buttonNewPoly.Name = "buttonNewPoly";
            this.buttonNewPoly.Size = new System.Drawing.Size(40, 40);
            this.buttonNewPoly.TabIndex = 1;
            this.buttonNewPoly.UseVisualStyleBackColor = true;
            this.buttonNewPoly.Click += new System.EventHandler(this.buttonNewPoly_Click);
            // 
            // buttonColor
            // 
            this.buttonColor.Location = new System.Drawing.Point(0, 0);
            this.buttonColor.Name = "buttonColor";
            this.buttonColor.Size = new System.Drawing.Size(40, 40);
            this.buttonColor.TabIndex = 0;
            this.buttonColor.UseVisualStyleBackColor = true;
            this.buttonColor.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBoxBPP
            // 
            this.comboBoxBPP.FormattingEnabled = true;
            this.comboBoxBPP.Items.AddRange(new object[] {
            "1BPP",
            "4BPP",
            "8BPP",
            "12BPP",
            "16BPP",
            "24BPP"});
            this.comboBoxBPP.Location = new System.Drawing.Point(1, 51);
            this.comboBoxBPP.Name = "comboBoxBPP";
            this.comboBoxBPP.Size = new System.Drawing.Size(134, 21);
            this.comboBoxBPP.TabIndex = 8;
            this.comboBoxBPP.SelectedIndexChanged += new System.EventHandler(this.comboBoxBPP_SelectedIndexChanged);
            // 
            // panelFrames
            // 
            this.panelFrames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFrames.AutoScroll = true;
            this.panelFrames.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFrames.Location = new System.Drawing.Point(142, 323);
            this.panelFrames.Name = "panelFrames";
            this.panelFrames.Size = new System.Drawing.Size(444, 88);
            this.panelFrames.TabIndex = 10;
            this.panelFrames.WrapContents = false;
            this.panelFrames.DoubleClick += new System.EventHandler(this.panelFrames_DoubleClick);
            this.panelFrames.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panelFrames_MouseDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 433);
            this.Controls.Add(this.panelFrames);
            this.Controls.Add(this.comboBoxBPP);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.comboBoxFrames);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Pygmy Image Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripZoom100;
        private System.Windows.Forms.ToolStripMenuItem toolStripZoom200;
        private System.Windows.Forms.ToolStripMenuItem toolStripZoom300;
        private System.Windows.Forms.ToolStripMenuItem toolStripZoom400;
        private System.Windows.Forms.ToolStripMenuItem toolStripZoom800;
        private System.Windows.Forms.ToolStripMenuItem toolStripZoom1600;
        private System.Windows.Forms.ComboBox comboBoxFrames;
        private System.Windows.Forms.ToolStripMenuItem saveAsStripMenuItem1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonColor;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelText;
        private System.Windows.Forms.ComboBox comboBoxBPP;
        private System.Windows.Forms.ToolStripMenuItem rawDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelImageType;
        private System.Windows.Forms.Button buttonNewPoly;
        private System.Windows.Forms.Button buttonNewLine;
        private System.Windows.Forms.FlowLayoutPanel panelFrames;

    }
}

