namespace WindowsFormsApplication1
{
    partial class FormToolTipMenu
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
            this.buttonDeleteVertice = new System.Windows.Forms.Button();
            this.buttonInsertVertice = new System.Windows.Forms.Button();
            this.buttonDeletePoly = new System.Windows.Forms.Button();
            this.buttonMoveVertice = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonInsertText = new System.Windows.Forms.Button();
            this.buttonInsertRaster = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonDeleteVertice
            // 
            this.buttonDeleteVertice.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDeleteVertice.Location = new System.Drawing.Point(1, 2);
            this.buttonDeleteVertice.Name = "buttonDeleteVertice";
            this.buttonDeleteVertice.Size = new System.Drawing.Size(136, 24);
            this.buttonDeleteVertice.TabIndex = 0;
            this.buttonDeleteVertice.Text = "Delete Vertice";
            this.buttonDeleteVertice.UseVisualStyleBackColor = true;
            this.buttonDeleteVertice.Click += new System.EventHandler(this.buttonDeleteVertice_Click);
            // 
            // buttonInsertVertice
            // 
            this.buttonInsertVertice.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonInsertVertice.Location = new System.Drawing.Point(1, 24);
            this.buttonInsertVertice.Name = "buttonInsertVertice";
            this.buttonInsertVertice.Size = new System.Drawing.Size(136, 24);
            this.buttonInsertVertice.TabIndex = 1;
            this.buttonInsertVertice.Text = "Insert Vertice";
            this.buttonInsertVertice.UseVisualStyleBackColor = true;
            this.buttonInsertVertice.Click += new System.EventHandler(this.buttonInsertVertice_Click);
            // 
            // buttonDeletePoly
            // 
            this.buttonDeletePoly.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDeletePoly.Location = new System.Drawing.Point(1, 72);
            this.buttonDeletePoly.Name = "buttonDeletePoly";
            this.buttonDeletePoly.Size = new System.Drawing.Size(136, 24);
            this.buttonDeletePoly.TabIndex = 2;
            this.buttonDeletePoly.Text = "Delete Poly";
            this.buttonDeletePoly.UseVisualStyleBackColor = true;
            this.buttonDeletePoly.Click += new System.EventHandler(this.buttonDeletePoly_Click);
            // 
            // buttonMoveVertice
            // 
            this.buttonMoveVertice.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonMoveVertice.Location = new System.Drawing.Point(1, 48);
            this.buttonMoveVertice.Name = "buttonMoveVertice";
            this.buttonMoveVertice.Size = new System.Drawing.Size(136, 24);
            this.buttonMoveVertice.TabIndex = 3;
            this.buttonMoveVertice.Text = "Move Vertice";
            this.buttonMoveVertice.UseVisualStyleBackColor = true;
            this.buttonMoveVertice.Click += new System.EventHandler(this.buttonMoveVertice_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCancel.Location = new System.Drawing.Point(1, 144);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(136, 24);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonInsertText
            // 
            this.buttonInsertText.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonInsertText.Location = new System.Drawing.Point(1, 96);
            this.buttonInsertText.Name = "buttonInsertText";
            this.buttonInsertText.Size = new System.Drawing.Size(136, 24);
            this.buttonInsertText.TabIndex = 5;
            this.buttonInsertText.Text = "Insert Text";
            this.buttonInsertText.UseVisualStyleBackColor = true;
            this.buttonInsertText.Click += new System.EventHandler(this.buttonInsertText_Click);
            // 
            // buttonInsertRaster
            // 
            this.buttonInsertRaster.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonInsertRaster.Location = new System.Drawing.Point(1, 120);
            this.buttonInsertRaster.Name = "buttonInsertRaster";
            this.buttonInsertRaster.Size = new System.Drawing.Size(136, 24);
            this.buttonInsertRaster.TabIndex = 6;
            this.buttonInsertRaster.Text = "Insert Raster";
            this.buttonInsertRaster.UseVisualStyleBackColor = true;
            this.buttonInsertRaster.Click += new System.EventHandler(this.buttonInsertRaster_Click);
            // 
            // FormToolTipMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(139, 169);
            this.Controls.Add(this.buttonInsertRaster);
            this.Controls.Add(this.buttonInsertText);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonMoveVertice);
            this.Controls.Add(this.buttonDeletePoly);
            this.Controls.Add(this.buttonInsertVertice);
            this.Controls.Add(this.buttonDeleteVertice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormToolTipMenu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonDeleteVertice;
        private System.Windows.Forms.Button buttonInsertVertice;
        private System.Windows.Forms.Button buttonDeletePoly;
        private System.Windows.Forms.Button buttonMoveVertice;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonInsertText;
        private System.Windows.Forms.Button buttonInsertRaster;
    }
}