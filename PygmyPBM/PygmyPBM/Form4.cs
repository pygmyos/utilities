using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class FormToolTipMenu : Form
    {
        public const int ClickCancel = 0;
        public const int ClickDeleteVertice = 1;
        public const int ClickInsertVertice = 2;
        public const int ClickMoveVertice = 3;
        public const int ClickDeletePoly = 4;
        public const int ClickInsertText = 5;
        public const int ClickInsertRaster = 6;
        public int Clicked;

        public FormToolTipMenu()
        {
            InitializeComponent();
            Clicked = 0;
        }

        private void buttonDeleteVertice_Click(object sender, EventArgs e)
        {
            Clicked = ClickDeleteVertice;
        }

        private void buttonInsertVertice_Click(object sender, EventArgs e)
        {
            Clicked = ClickInsertRaster;
        }

        private void buttonMoveVertice_Click(object sender, EventArgs e)
        {
            Clicked = ClickMoveVertice;
        }

        private void buttonDeletePoly_Click(object sender, EventArgs e)
        {
            Clicked = ClickDeletePoly;
        }

        private void buttonInsertText_Click(object sender, EventArgs e)
        {
            Clicked = ClickInsertText;
        }

        private void buttonInsertRaster_Click(object sender, EventArgs e)
        {
            Clicked = ClickInsertRaster;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Clicked = ClickCancel;
            this.Close();
        }

    }
}
