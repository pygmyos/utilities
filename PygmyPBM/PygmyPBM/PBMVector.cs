using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class PBMVector
    {
        PBMVectorData[] vectorData;
        Bitmap ImageBitmap;
        public int Length;
        int ImageWidth, ImageHeight;
        int SelectedIndex;

        public PBMVector()
        {
            Length = 0;
            SelectedIndex = 0;
            AddPoly();
        }

        public void SetSize(int Width, int Height)
        {
            ImageWidth = Width;
            ImageHeight = Height;
        }

        public void SelectIndex(int offset)
        {
            if( offset >= Length )
            {
                return;
            }
            SelectedIndex = offset;
        }

        public int GetIndex()
        {
            return (SelectedIndex);
        }

        public int GetLength()
        {
            return( Length );
        }

        public bool IsVector(int xSeek, int ySeek)
        {
            for (int i = 0; i < Length; i++)
            {
                if (vectorData[i].IsOriginVertice(xSeek, ySeek) == true)
                {
                    return (true);
                }
            }

            return(false );
        }

        public void AddPacket(int Type)
        {
            AddPoly(Type);
        }

        public void AddPoly()
        {
            PBMVectorData[] tmpVectorData;
            tmpVectorData = new PBMVectorData[Length+1];

            for (int i = 0; i < Length; i++)
            {
                tmpVectorData[i] = vectorData[i];
            }
            tmpVectorData[Length] = new PBMVectorData();
            tmpVectorData[Length].Type = PBMVectorData.TypePoly;
            ++Length;
            vectorData = tmpVectorData;
            SelectedIndex = Length - 1;
        }

        public void AddPoly(int Type)
        {
            PBMVectorData[] tmpVectorData;
            tmpVectorData = new PBMVectorData[Length + 1];

            for (int i = 0; i < Length; i++)
            {
                tmpVectorData[i] = vectorData[i];
            }
            tmpVectorData[Length] = new PBMVectorData();
            tmpVectorData[Length].Type = Type;
            ++Length;
            vectorData = tmpVectorData;
            SelectedIndex = Length - 1;
        }

        public void AddPoly(int x, int y)
        {
            PBMVectorData[] tmpVectorData;
            tmpVectorData = new PBMVectorData[Length + 1];

            for (int i = 0; i < Length; i++)
            {
                tmpVectorData[i] = vectorData[i];
            }
            tmpVectorData[Length] = new PBMVectorData();
            tmpVectorData[Length].AddVertice(x, y);
            ++Length;
            vectorData = tmpVectorData;
            SelectedIndex = Length - 1;
        }

        public void RemovePoly()
        {
            PBMVectorData[] tmpVectorData;
            tmpVectorData = new PBMVectorData[--Length];

            for (int i = 0; i < Length; i++)
            {
                tmpVectorData[i] = vectorData[i];
            }
            vectorData = tmpVectorData;
            SelectedIndex = Length - 1;
        }

        public void RemovePoly( int offset )
        {
            if (offset >= Length)
            {
                return;
            }
            PBMVectorData[] tmpVectorData;
            tmpVectorData = new PBMVectorData[--Length];
            int found = 0;

            for (int i = 0; i < Length; i++)
            {
                if (i == offset)
                {
                    found = 1;
                }
                tmpVectorData[i] = vectorData[i+found];
            }
            vectorData = tmpVectorData;
            SelectedIndex = Length - 1;
        }
        
        public void InsertPoly( int offset )
        {
            if (offset > Length)
            {
                return;
            }
            PBMVectorData[] tmpVectorData;
            tmpVectorData = new PBMVectorData[--Length];
            int found = 0;

            for (int i = 0; i < Length; i++)
            {
                if (i == offset)
                {
                    found = 1;
                    tmpVectorData[i] = new PBMVectorData();
                }
                tmpVectorData[i+found] = vectorData[i];
            }
            vectorData = tmpVectorData;
            SelectedIndex = Length - 1;
        }

        public void InsertPoly(int offset, int x, int y)
        {
            if (offset > Length)
            {
                return;
            }
            PBMVectorData[] tmpVectorData;
            tmpVectorData = new PBMVectorData[--Length];
            int found = 0;

            for (int i = 0; i < Length; i++)
            {
                if (i == offset)
                {
                    found = 1;
                    tmpVectorData[i] = new PBMVectorData();
                    tmpVectorData[i].AddVertice(x, y);
                }
                tmpVectorData[i + found] = vectorData[i];
            }
            vectorData = tmpVectorData;
            SelectedIndex = Length - 1;
        }

        //
        // Wrappers for PBMVectorData sub class follow
        //
        public void SetType( int Type )
        {
            vectorData[SelectedIndex].Type = Type;
        }

        public int GetType( int Type )
        {
            return( vectorData[SelectedIndex].Type );
        }

        public bool IsVertice(int xSeek, int ySeek)
        {
            return( vectorData[SelectedIndex].IsVertice( xSeek, ySeek));
        }

        public int GetVertices()
        {
            return (vectorData[SelectedIndex].GetLength());
        }

        public void RemoveVertice()
        {
            vectorData[SelectedIndex].RemoveVertice();
        }

        public void RemoveVertice(int offset)
        {
            vectorData[SelectedIndex].RemoveVertice(offset);
        }

        public void RemoveVerticeAfter(int xSeek, int ySeek)
        {
            vectorData[SelectedIndex].RemoveVerticeAfter(xSeek, ySeek);
        }

        public void AddVertice(int x, int y)
        {
            
            vectorData[SelectedIndex].AddVertice(x, y);
        }

        public void InsertVertice(int offset, int x, int y)
        {
            vectorData[SelectedIndex].InsertVertice(offset, x, y);
        }

        public void InsertVerticeAfter(int x, int y, int xSeek, int ySeek)
        {
            vectorData[SelectedIndex].InsertVerticeAfter(x, y, xSeek, ySeek);
        }

        public Bitmap Draw(int Zoom)
        {
            if (ImageWidth < 1 || ImageWidth > 65535) 
            {
                ImageWidth = 1;
            }
            if(ImageHeight < 1 || ImageHeight > 65535)
            {
                ImageHeight = 1;
            }
            if(Zoom < 1 || Zoom > 32)
            {
                Zoom = 1;
            }
            System.Drawing.Pen myPen;
            myPen = new System.Drawing.Pen(System.Drawing.Color.Black);
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

            Rectangle rect = new Rectangle();
            //new Bitmap(
            ImageBitmap = new Bitmap(ImageWidth * Zoom, ImageHeight * Zoom, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            System.Drawing.Graphics formGraphics = Graphics.FromImage(ImageBitmap);

            for (int i = 0; i < vectorData.Length; i++)
            {
                if (vectorData[i] == null)
                {
                    break;
                }
                if (vectorData[i].Type == PBMVectorData.TypeArc)
                {

                }
                else if (vectorData[i].Type == PBMVectorData.TypeBrush)
                {

                }
                else if (vectorData[i].Type == PBMVectorData.TypeText)
                {

                }
                else if (vectorData[i].Type == PBMVectorData.TypePixel)
                {

                }
                else if (vectorData[i].Type == PBMVectorData.TypePoly)
                {
                    if (vectorData[i].GetLength() > 0 )
                    {
                        myBrush.Color = Color.FromArgb(0xFF, 0x00, 0x00);
                        rect.X = (vectorData[i].Vertices[0, 0] * Zoom);
                        rect.Y = (vectorData[i].Vertices[0, 1] * Zoom);
                        rect.Width = Zoom;
                        rect.Height = Zoom;
                        formGraphics.FillRectangle(myBrush, rect);
                    }
                    if (vectorData[i].Vertices.Length >= 4)
                    {
                        
                        for (int ii = 0; ii < vectorData[i].GetLength() - 1; ii++)
                        {
                            myBrush.Color = Color.FromArgb(0xFF, 0xFF, 0x00);
                            myPen.Color = Color.FromArgb(0xFF, 0x00, 0xFF);
                            formGraphics.DrawLine(myPen,
                                (int)(vectorData[i].Vertices[ii, 0] * Zoom) + (Zoom / 2),
                                (int)(vectorData[i].Vertices[ii, 1] * Zoom) + (Zoom / 2),
                                (int)(vectorData[i].Vertices[ii + 1, 0] * Zoom) + (Zoom / 2),
                                (int)(vectorData[i].Vertices[ii + 1, 1] * Zoom) + (Zoom / 2));
                            if (ii > 0)
                            {
                                rect.X = (vectorData[i].Vertices[ii, 0] * Zoom);
                                rect.Y = (vectorData[i].Vertices[ii, 1] * Zoom);
                                formGraphics.FillRectangle(myBrush, rect);
                            }
                        }
                        myBrush.Color = Color.FromArgb(0xFF, 0x00, 0x00);
                        rect.X = (vectorData[i].Vertices[vectorData[i].GetLength() - 1, 0] * Zoom);
                        rect.Y = (vectorData[i].Vertices[vectorData[i].GetLength() - 1, 1] * Zoom);
                        formGraphics.FillRectangle(myBrush, rect);
                    }
                    
                }
                else if (vectorData[i].Type == PBMVectorData.TypeRaster)
                {

                }
                else if (vectorData[i].Type == PBMVectorData.TypeSpline)
                {

                }
            }
            myPen.Color = Color.FromArgb(0x90, 0x90, 0x90);
            if (Zoom > 4)
            {
                for (int x = 0, GridWidth = ImageWidth * Zoom; x < ImageWidth + 1; x++)
                {
                    formGraphics.DrawLine(myPen, (int)(x * Zoom), (int)0, (int)(x * Zoom), (int)GridWidth);
                }
                for (int y = 0, GridHeight = ImageHeight * Zoom; y < ImageHeight + 1; y++)
                {
                    formGraphics.DrawLine(myPen, (int)0, (int)(y * Zoom), (int)GridHeight, (int)(y * Zoom));
                }
            }
            //pictureBox1.Image = ImageBitmap;
            myPen.Dispose();
            myBrush.Dispose();
            formGraphics.Dispose();

            return (ImageBitmap);
        }

        public void Export(string FileName)//, int ImageWidth, int ImageHeight)
        {
            int PygmyHeader;
            bool Dim16Bit;
            FileStream fs = File.Create(FileName);
            UTF8Encoding utf8 = new UTF8Encoding();

            BinaryWriter bw = new BinaryWriter(fs, utf8);

            PygmyHeader = PygmyPBMHeader.PYGMY_PBM_VECTOR;
            PygmyHeader |= PygmyPBMHeader.PYGMY_PBM_24BPP;
            if (ImageWidth > 255 || ImageHeight > 255)
            {
                PygmyHeader |= PygmyPBMHeader.PYGMY_PBM_16BITD;
                Dim16Bit = true;
            }
            else
            {
                Dim16Bit = false;
            }
            bw.Write((byte)(PygmyHeader >> 8));
            bw.Write((byte)PygmyHeader);
            if (Dim16Bit == true)
            {
                // if 16Bit Dimensions enabled, then vector packet count is 32 bit
                bw.Write((byte)(ImageWidth >> 8));
                bw.Write((byte)ImageWidth);
                bw.Write((byte)(ImageHeight >> 8));
                bw.Write((byte)ImageHeight);
            }
            else
            {
                bw.Write((byte)ImageWidth);
                bw.Write((byte)ImageHeight);
            }
            for (int i = 0; i < Length; i++)
            {
                if( vectorData[i].Type == PBMVectorData.TypeArc )
                {
                   bw.Write((byte)( PygmyPBMHeader.PYGMY_VECTOR_ARC|vectorData[i].Length));
                }
                else if( vectorData[i].Type == PBMVectorData.TypeBrush )
                {
                    bw.Write((byte)(PygmyPBMHeader.PYGMY_VECTOR_BRUSH | vectorData[i].Length));
                }
                else if( vectorData[i].Type == PBMVectorData.TypeText )
                {
                    bw.Write((byte)(PygmyPBMHeader.PYGMY_VECTOR_TEXT | vectorData[i].Length));
                }
                else if( vectorData[i].Type == PBMVectorData.TypePixel )
                {
                    // Not supported as yet
                }
                else if( vectorData[i].Type == PBMVectorData.TypePoly )
                {
                    bw.Write((byte)(PygmyPBMHeader.PYGMY_VECTOR_POLY | vectorData[i].Length));
                }
                else if( vectorData[i].Type == PBMVectorData.TypeRaster )
                {
                    bw.Write((byte)(PygmyPBMHeader.PYGMY_VECTOR_RASTER | vectorData[i].Length));
                }
                else if( vectorData[i].Type == PBMVectorData.TypeSpline )
                {
                    bw.Write((byte)(PygmyPBMHeader.PYGMY_VECTOR_SPLINE | vectorData[i].Length));
                }
                for( int ii = 0; ii < vectorData[i].Length; ii++)
                {
                    if (Dim16Bit == true)
                    {
                        bw.Write((byte)(vectorData[i].Vertices[ii,0]>>8));
                        bw.Write((byte)vectorData[i].Vertices[ii,0]);
                        bw.Write((byte)(vectorData[i].Vertices[ii,1]>>8));
                        bw.Write((byte)vectorData[i].Vertices[ii, 1]);
                    }
                    else
                    {
                        bw.Write((byte)vectorData[i].Vertices[ii,0]);
                        bw.Write((byte)vectorData[i].Vertices[ii,1]);
                    }
                }
                bw.Write((byte)PygmyPBMHeader.PYGMY_VECTOR_END);
            }
            fs.Close();
        }

        public void Load(string FileName)
        {
            int PygmyHeader;
            bool Dim16Bit;
            PygmyPBMHeader PBM = new PygmyPBMHeader();
            FileStream fs = File.Create(FileName);
            UTF8Encoding utf8 = new UTF8Encoding();

            BinaryReader br = new BinaryReader(fs, utf8);

            // Re-Init the Vectors array
            Length = 0;
            SelectedIndex = 0;
            AddPoly();
            // End Re-Init the Vectors Array

            PygmyHeader = br.ReadByte() << 8;
            PygmyHeader |= br.ReadByte();
            Dim16Bit = false;
            if ((PygmyHeader & PygmyPBMHeader.PYGMY_PBM_16BITD) != 0)
            {
                Dim16Bit = true;
            }
            if (Dim16Bit == true)
            {
                ImageWidth = br.ReadByte() << 8;
                ImageWidth |= br.ReadByte();
                ImageHeight = br.ReadByte() << 8;
                ImageHeight |= br.ReadByte();
            }
            else
            {
                ImageWidth = br.ReadByte();
                ImageHeight = br.ReadByte();
            }
            for (int i = 0; ; i++)
            {
                int VectorPacketHeader = br.ReadByte();
                int VectorType = PBM.ExtractVectorPacketType(VectorPacketHeader);
                int VerticeCount = PBM.ExtractVerticeCount(VectorPacketHeader);
                if (VectorType == PygmyPBMHeader.PYGMY_VECTOR_END)
                {
                    break;
                }
                AddPacket(VectorType);
                for( int ii = 0; ii < VerticeCount; ii++ )
                {
                    int x, y;
                    if (Dim16Bit == true)
                    {
                        x = ( br.ReadByte() << 8 ) | br.ReadByte();
                        y = ( br.ReadByte() << 8 ) | br.ReadByte();
                    }
                    else
                    {
                        x = br.ReadByte();
                        y = br.ReadByte();
                    }
                    AddVertice(x, y);
                }
            }
            fs.Close();
        }
    }
}
