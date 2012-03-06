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
    public partial class FormNewImage : Form
    {
        public int ImageWidth, ImageHeight, ImageFrame, ImageBPP;
        public string ImageType;

        public FormNewImage()
        {
            InitializeComponent();
        }

        private void FormNewImage_Load(object sender, EventArgs e)
        {
            comboBoxImageType.SelectedIndex = 0;
            comboBoxInsert.SelectedIndex = 0;
            comboBoxBPP.SelectedIndex = 0;
            textBoxWidth.Text = "0";
            textBoxHeight.Text = "0";
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ImageWidth = int.Parse( textBoxWidth.Text );
            ImageHeight = int.Parse( textBoxHeight.Text );
            if (comboBoxBPP.SelectedText == "1BPP")
            {
                ImageBPP = 1;
            }
            else if( comboBoxBPP.SelectedText == "4BPP" )
            {
                ImageBPP = 4;
            }
            else if (comboBoxBPP.SelectedText == "8BPP")
            {
                ImageBPP = 8;
            }
            else if (comboBoxBPP.SelectedText == "16BPP")
            {
                ImageBPP = 16;
            }
            else if(comboBoxBPP.SelectedText == "24BPP")
            {
                ImageBPP = 24;
            }
            if (comboBoxBPP.SelectionLength > 0)
            {
                ImageFrame = int.Parse(comboBoxInsert.SelectedText);
            }
            else
            {
                ImageFrame = 0;
            }
            ImageType = comboBoxImageType.Items[comboBoxImageType.SelectedIndex].ToString();
            //ImageType = comboBoxImageType.Items[ comboBoxImageType.SelectedIndex ].ToString();
            
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
        }

        private void comboBoxImageType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
