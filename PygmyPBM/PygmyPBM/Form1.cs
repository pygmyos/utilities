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
    

    public partial class Form1 : Form
    {
        PBMVector Vectors = new PBMVector();
        PBMRaster Raster = new PBMRaster();
        public int Zoom, Index, ImageWidth, ImageHeight, BPP, Header, ImageFrames, ImageCurrentFrame;
        public bool DrawActive;
        public Color CurrentColor;
        public string ImageName, ImageType;
        PictureBox[] frames;
        private PictureBox _selectedPicture;

        public void Draw()
        {
            if (ImageType == "Vector")
            {
                pictureBox1.Image = Vectors.Draw(GetZoom());
            }
            else
            {
                panelFrames.Controls.Clear();
                Raster.LoadAll(ImageName, 1);
                frames = new PictureBox[Raster.ImageFrames];
                //comboBoxFrames.Text = Raster.ImageFrames.ToString();
                int Height = panelFrames.Height-20;
                for (int i = 0; i < Raster.ImageFrames; i++)
                {
                    frames[i] = new PictureBox();
                    frames[i].BackgroundImageLayout = ImageLayout.Zoom;
                    frames[i].SetBounds(Height * i, 0, Height, Height);
                    Raster.Load(ImageName, i);
                    frames[i].BackgroundImage = Raster.Draw(1);
                    frames[i].Visible = true;
                    frames[i].BorderStyle = BorderStyle.FixedSingle;
                    frames[i].Name = i.ToString();
                    frames[i].Click += new EventHandler(picture_click);
                    panelFrames.Controls.Add(frames[i]);
                    
                }
                panelFrames.Refresh();
                if (comboBoxFrames.Text != "")
                {
                    Raster.Load(ImageName, int.Parse(comboBoxFrames.Text));
                }
                else
                {
                    Raster.Load(ImageName, 0);
                }
                pictureBox1.Image = Raster.Draw(GetZoom());
            }
        }

        public void SetZoom(int iZoom)
        {
            Zoom = iZoom;
            Draw();
        }

        public int GetZoom()
        {
            return (Zoom);
        }

    
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AddOwnedForm(new FormNewImage() );
            CurrentColor = Color.FromArgb(0x00, 0x00, 0x00);
            buttonColor.BackColor = CurrentColor;
            SetZoom(1);
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            ImageName = openFileDialog1.FileName;

            comboBoxFrames.SelectedItem = 0;
            Draw();
            /*if (ImageType == "Vector")
            {
                pictureBox1.Image = Vectors.Draw(GetZoom());
            }
            else
            {
                Raster.LoadAll(ImageName, 1);
                Raster.Load(ImageName, 0);
                pictureBox1.Image = Raster.Draw(GetZoom());
            }*/
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            ImageName = saveFileDialog1.FileName;

            Draw();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            
        }

       
        private void toolStripZoom100_Click(object sender, EventArgs e)
        {
            SetZoom(1);  
        }

        private void toolStripZoom200_Click(object sender, EventArgs e)
        {
            SetZoom(2);
        }

        private void toolStripZoom300_Click(object sender, EventArgs e)
        {
            SetZoom(3);
        }

        private void toolStripZoom400_Click(object sender, EventArgs e)
        {
            SetZoom(4);
        }

        private void toolStripZoom800_Click(object sender, EventArgs e)
        {
            SetZoom(8);
        }

        private void toolStripZoom1600_Click(object sender, EventArgs e)
        {
            SetZoom(16);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            DrawActive = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int Zoom = GetZoom();
            int GridX = ((int)e.X / Zoom);
            int GridY = ((int)e.Y / Zoom);

            toolStripStatusLabel1.Text = GridX + ", " + GridY;
            if (DrawActive == true)
            {

                if (GridX < ImageWidth && GridY < ImageHeight)
                {
                    if (ImageType == "Vector")
                    {
                        Vectors.AddVertice(GridX, GridY); // Default type is Poly
                    }
                    else
                    {
                        Raster.SetPixel(GridX, GridY, CurrentColor);
                    }
                    Draw();
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            DrawActive = false;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            DrawActive = false;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            DrawActive = false;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                FormToolTipMenu formToolTipMenu = new FormToolTipMenu();
                formToolTipMenu.Location = new Point(e.X, e.Y);
                
                formToolTipMenu.ShowDialog();
                return;
            }

            int Zoom = GetZoom();
            int GridX = ((int)e.X / Zoom);
            int GridY = ((int)e.Y / Zoom);

            if (GridX < ImageWidth && GridY < ImageHeight)
            {
                if (ImageType == "Vector")
                {   
                    Vectors.AddVertice(GridX, GridY);    
                }
                else
                {
                    Raster.SetPixel( GridX, GridY, CurrentColor );
                }
                Draw();
            }
        }

        private void comboBoxFrames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if( ImageType == "Vector" )
            {
                pictureBox1.Image = Vectors.Draw(GetZoom());
            }
            else if (ImageName != null && comboBoxFrames.SelectedIndex < ImageFrames)
            {
                Raster.Load(ImageName, comboBoxFrames.SelectedIndex );
                pictureBox1.Image = Raster.Draw(GetZoom());
            }
        }

        private void saveAsStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            CurrentColor = colorDialog1.Color;
            buttonColor.BackColor = CurrentColor;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNewImage formNewImage = new FormNewImage();
            formNewImage.ShowDialog(this);
            ImageName = "";
            
            ImageType = formNewImage.ImageType;
            BPP = formNewImage.ImageBPP;
            ImageWidth = formNewImage.ImageWidth;
            ImageHeight = formNewImage.ImageHeight;
            ImageCurrentFrame = formNewImage.ImageFrame;

            if (comboBoxFrames.Items.Count > 0)
            {
                comboBoxFrames.SelectedIndex = ImageCurrentFrame;
            }
            else
            {
                comboBoxFrames.Items.Clear();
                comboBoxFrames.Items.Add("0");
            }
            if (ImageType == "Vector")
            {
                Vectors.SetSize(ImageWidth, ImageHeight);
            }
            else
            {
                Raster.New(formNewImage.ImageWidth, formNewImage.ImageHeight, BPP);
            }
            Draw();
        }

        private void rawDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRawData formRawImageData = new FormRawData( ImageName);
            formRawImageData.Show();
        }

        private void comboBoxBPP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxBPP.SelectedIndex == 0 )
            {
                BPP = 1;
            }
            else if( comboBoxBPP.SelectedIndex == 1 )
            {
                BPP = 4;
            }
            else if (comboBoxBPP.SelectedIndex == 2)
            {
                BPP = 8;
            }
            else if (comboBoxBPP.SelectedIndex == 3)
            {
                BPP = 12;
            }
            else if (comboBoxBPP.SelectedIndex == 4)
            {
                BPP = 16;
            }
            else if(comboBoxBPP.SelectedIndex == 5 )
            {
                BPP = 24;
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void buttonNewPoly_Click(object sender, EventArgs e)
        {
            Vectors.AddPoly();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists(ImageName))
            {
                saveFileDialog1.ShowDialog();
                return;
            }

            if (ImageType == "Vector")
            {
                Vectors.Export(ImageName);
            }
            else
            {
                Raster.Export(ImageName, BPP);
            }
        }

        private void panelFrames_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void panelFrames_DoubleClick(object sender, EventArgs e)
        {
            
        }

        void picture_click(object sender, EventArgs e)
        {
            if (_selectedPicture != null)
            {
                
                _selectedPicture.BorderStyle = BorderStyle.FixedSingle;
            }
            _selectedPicture = (PictureBox)sender;
            _selectedPicture.BorderStyle = BorderStyle.Fixed3D;
            comboBoxFrames.Text = _selectedPicture.Name;
            Raster.Load(ImageName, int.Parse(_selectedPicture.Name));
            pictureBox1.Image = Raster.Draw(GetZoom());
        }
    }
}
