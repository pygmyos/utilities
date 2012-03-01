using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class FormRawData : Form
    {
        /*public FormRawData()
        {
            InitializeComponent();
        }*/

        public FormRawData(string FileName)
        {
            InitializeComponent();

            if (!File.Exists(FileName))
            {
                
            }
            else
            {
                BinaryReader ImageFile = new BinaryReader(File.OpenRead(@FileName));
                
                dataGridViewImage.ColumnCount = 16;

                for (int i = 0; i < 16; i++)
                {
                    DataGridViewColumn column = dataGridViewImage.Columns[i];
                    column.Width = 36;
                }
                for( int i = 0; i < (ImageFile.BaseStream.Length / 16)+1; i++ )
                {
                    dataGridViewImage.Rows.Add( i.ToString() );
                }
                
                for (int i = 0; i < ImageFile.BaseStream.Length; i++)
                {
                    int ImageByte = ImageFile.ReadByte();

                    //dataGridViewImage[i % 16, i / 16].Value = String.Format("{0:0x2X}", ImageByte);// ImageByte.ToString();
                    dataGridViewImage[i % 16, i / 16].Value = ImageByte.ToString();
                        //dataGridViewImage[col, row].ValueType = ;
                }
               
            }
        }

        private void FormRawData_Load(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
