namespace WindowsFormsApplication1
{
    partial class FormNewImage
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
            this.labelWidth = new System.Windows.Forms.Label();
            this.labelHeight = new System.Windows.Forms.Label();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.comboBoxImageType = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelInsert = new System.Windows.Forms.Label();
            this.labelImageType = new System.Windows.Forms.Label();
            this.labelBPP = new System.Windows.Forms.Label();
            this.comboBoxBPP = new System.Windows.Forms.ComboBox();
            this.comboBoxInsert = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(1, 104);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(35, 13);
            this.labelWidth.TabIndex = 0;
            this.labelWidth.Text = "Width";
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(1, 130);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(38, 13);
            this.labelHeight.TabIndex = 1;
            this.labelHeight.Text = "Height";
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(103, 101);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(82, 20);
            this.textBoxWidth.TabIndex = 2;
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Location = new System.Drawing.Point(103, 127);
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.Size = new System.Drawing.Size(82, 20);
            this.textBoxHeight.TabIndex = 3;
            // 
            // comboBoxImageType
            // 
            this.comboBoxImageType.FormattingEnabled = true;
            this.comboBoxImageType.Items.AddRange(new object[] {
            "Raster",
            "Vector",
            "Font"});
            this.comboBoxImageType.Location = new System.Drawing.Point(103, 19);
            this.comboBoxImageType.Name = "comboBoxImageType";
            this.comboBoxImageType.Size = new System.Drawing.Size(119, 21);
            this.comboBoxImageType.TabIndex = 4;
            this.comboBoxImageType.SelectedIndexChanged += new System.EventHandler(this.comboBoxImageType_SelectedIndexChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(118, 173);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(76, 26);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(200, 173);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(72, 25);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelInsert
            // 
            this.labelInsert.AutoSize = true;
            this.labelInsert.Location = new System.Drawing.Point(1, 76);
            this.labelInsert.Name = "labelInsert";
            this.labelInsert.Size = new System.Drawing.Size(90, 13);
            this.labelInsert.TabIndex = 7;
            this.labelInsert.Text = "Insert Frame After";
            // 
            // labelImageType
            // 
            this.labelImageType.AutoSize = true;
            this.labelImageType.Location = new System.Drawing.Point(1, 22);
            this.labelImageType.Name = "labelImageType";
            this.labelImageType.Size = new System.Drawing.Size(63, 13);
            this.labelImageType.TabIndex = 8;
            this.labelImageType.Text = "Image Type";
            // 
            // labelBPP
            // 
            this.labelBPP.AutoSize = true;
            this.labelBPP.Location = new System.Drawing.Point(1, 49);
            this.labelBPP.Name = "labelBPP";
            this.labelBPP.Size = new System.Drawing.Size(68, 13);
            this.labelBPP.TabIndex = 10;
            this.labelBPP.Text = "Bits Per Pixel";
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
            this.comboBoxBPP.Location = new System.Drawing.Point(103, 46);
            this.comboBoxBPP.Name = "comboBoxBPP";
            this.comboBoxBPP.Size = new System.Drawing.Size(119, 21);
            this.comboBoxBPP.TabIndex = 11;
            // 
            // comboBoxInsert
            // 
            this.comboBoxInsert.FormattingEnabled = true;
            this.comboBoxInsert.Location = new System.Drawing.Point(103, 73);
            this.comboBoxInsert.Name = "comboBoxInsert";
            this.comboBoxInsert.Size = new System.Drawing.Size(119, 21);
            this.comboBoxInsert.TabIndex = 12;
            // 
            // FormNewImage
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(284, 211);
            this.ControlBox = false;
            this.Controls.Add(this.comboBoxInsert);
            this.Controls.Add(this.comboBoxBPP);
            this.Controls.Add(this.labelBPP);
            this.Controls.Add(this.labelImageType);
            this.Controls.Add(this.labelInsert);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBoxImageType);
            this.Controls.Add(this.textBoxHeight);
            this.Controls.Add(this.textBoxWidth);
            this.Controls.Add(this.labelHeight);
            this.Controls.Add(this.labelWidth);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormNewImage";
            this.Text = "New Image";
            this.Load += new System.EventHandler(this.FormNewImage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.ComboBox comboBoxImageType;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelInsert;
        private System.Windows.Forms.Label labelImageType;
        private System.Windows.Forms.Label labelBPP;
        private System.Windows.Forms.ComboBox comboBoxBPP;
        private System.Windows.Forms.ComboBox comboBoxInsert;
    }
}