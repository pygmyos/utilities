using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class PBMRaster
    {
        Bitmap[] Images;
        Bitmap ImageBitmap;
        public int ImageWidth, ImageHeight, ImageBPP, ImageFrames;
        public Color[,] Pixels;

        public Bitmap GetImage(int Index)
        {
            return( Images[ Index ] );
        }

        public void SetPixel(int x, int y, Color color)
        {
            Pixels[x, y] = color;
        }

        public void New(int Width, int Height, int BPP)
        {
            ImageWidth = Width;
            ImageHeight = Height;
            ImageBPP = BPP;
            Pixels = new Color[ImageWidth, ImageHeight];
        }

        public int GetFrames( BinaryReader br )
        {
            int Frames, PygmyHeader;

            PygmyHeader = GetHeader( br );

            if ((PygmyHeader & (PygmyPBMHeader.PYGMY_PBM_IMAGESTRIP | PygmyPBMHeader.PYGMY_PBM_FONT)) == 0)
            {
                //ImageFrames = 0;
                return (0);
            }
            // uiGetHeader leaves the file at the frame count
            Frames = (br.ReadByte() << 8)|br.ReadByte();

            return (Frames);
        }

        public int GetHeader( BinaryReader br )
        {
            int Header;

            br.BaseStream.Seek(0, SeekOrigin.Begin);
            Header = (br.ReadByte() << 8)|br.ReadByte();

            return (Header);
        }

       public int GetFrame(BinaryReader br, int Index)
        {
            // This function seeks for image in imagestrip or font based on provided index
            int PixelIndex;
            int Frames, PygmyHeader;

            PygmyHeader = GetHeader(br);
            Frames = GetFrames(br);
            if ((PygmyHeader & (PygmyPBMHeader.PYGMY_PBM_IMAGESTRIP | PygmyPBMHeader.PYGMY_PBM_FONT)) == 0)
            {
                return (0);
            }
            if (Frames == 0 || Index > Frames)
            {
                return (0); // index past entries
            }
            if ((PygmyHeader & PygmyPBMHeader.PYGMY_PBM_TABLE32) != 0)
            {
                // Table Entries are 32bit
                br.BaseStream.Seek(Index * 4, SeekOrigin.Begin);
                PixelIndex = (int)br.ReadByte() << 24;
                PixelIndex |= (int)br.ReadByte() << 16;
                PixelIndex |= (int)br.ReadByte() << 8;
                PixelIndex |= (int)br.ReadByte();
            }
            else
            {
                // Table Entries are 16bit
                br.BaseStream.Seek(4 + (Index * 2), SeekOrigin.Begin);

                PixelIndex = (int)br.ReadByte() << 8;
                PixelIndex |= (int)br.ReadByte();
            }

            // The index returned is the absolute offset from the start of file
            return (PixelIndex);
        }

        public void LoadAll(string FileName, int Zoom)
        {
            BinaryReader br;

            if( !File.Exists( FileName ))
            {
                return;
            }
            br = new BinaryReader(File.OpenRead(@FileName));
            ImageFrames = GetFrames( br );
            Images = new Bitmap[ImageFrames];
            //br.BaseStream.Seek(0, SeekOrigin.Begin);
            br.Close();
            for (int i = 0; i < ImageFrames; i++)
            {
                Load(FileName, i);
                Images[i] = Draw(Zoom);
            }
        }

        public void Load(string FileName, int Index )
        {
            BinaryReader br;
            int PixelIndex, Len, Count, PygmyHeader, Packet;
            int X = 0, Y = 0;
            byte Byte1 = 0, Byte2 = 0, Byte3 = 0, byteR = 0, byteG = 0, byteB = 0;
            Color Pixel;

            br = new BinaryReader(File.OpenRead(@FileName));
            ImageFrames = GetFrames( br );
            Images = new Bitmap[ImageFrames];
            br.BaseStream.Seek(0, SeekOrigin.Begin);

            
                PixelIndex = GetFrame(br, Index);
                br.BaseStream.Seek(PixelIndex, SeekOrigin.Begin);
                /*if (comboBoxFrames.SelectedIndex != 0 || comboBoxFrames.SelectedIndex > ImageFrames)
                {
                    comboBoxFrames.Items.Clear();
                    for (int i = 0; i < ImageFrames + 1; i++)
                    {
                        comboBoxFrames.Items.Add(i.ToString());
                    }
                    comboBoxFrames.SelectedIndex = 0;
                }*/

                // Warning! ulPixelIndex is reused below

                PygmyHeader = (br.ReadByte() << 8)|br.ReadByte();
                if ((PygmyHeader & PygmyPBMHeader.PYGMY_PBM_16BITD) != 0)
                {
                    // Determine 8 or 16 bit Width and Height fields
                    ImageWidth = (br.ReadByte() << 8)|br.ReadByte();
                    ImageHeight = (br.ReadByte() << 8)|br.ReadByte();
                }
                else
                {
                    ImageWidth = br.ReadByte();
                    ImageHeight = br.ReadByte();
                } 
                
                Pixels = new Color[ImageWidth, ImageHeight];
                Len = ImageWidth * ImageHeight;
                ImageBPP = PygmyHeader & 0x000F;
                /*
                treeView1.Nodes.Clear();    // Clear any existing items
                treeView1.BeginUpdate();    // prevent overhead and flicker

                if ((iPygmyInfo & PygmyPBMHeader.PYGMY_PBM_IMAGE) != 0)
                {
                    treeView1.Nodes.Add("Type: Image");
                }
                else if ((iPygmyInfo & PygmyPBMHeader.PYGMY_PBM_IMAGESTRIP) != 0)
                {
                    treeView1.Nodes.Add("Type: ImageStrip");
                }
                else if ((iPygmyInfo & PygmyPBMHeader.PYGMY_PBM_FONT) != 0)
                {
                    treeView1.Nodes.Add("Type: Font");
                }
                else if ((iPygmyInfo & PygmyPBMHeader.PYGMY_PBM_VECTOR) != 0)
                {
                    treeView1.Nodes.Add("Type: Vector");
                }
                treeView1.Nodes.Add("Pixels: " + iLen.ToString());
                treeView1.Nodes.Add("Width: " + iWidth.ToString());
                treeView1.Nodes.Add("Height: " + iHeight.ToString());
                treeView1.Nodes.Add("Frames: " + ImageFrames.ToString());
                if (iBPP == PygmyPBMHeader.PYGMY_PBM_1BPP)
                {
                    BPP = 1;
                    comboBoxBPP.SelectedIndex = 0;
                    treeView1.Nodes.Add("BPP: 1BPP");
                }
                else if (iBPP == PygmyPBMHeader.PYGMY_PBM_4BPP)
                {
                    BPP = 4;
                    comboBoxBPP.SelectedIndex = 1;
                    treeView1.Nodes.Add("BPP: 4BPP");
                }
                else if (iBPP == PygmyPBMHeader.PYGMY_PBM_8BPP)
                {
                    BPP = 8;
                    comboBoxBPP.SelectedIndex = 2;
                    treeView1.Nodes.Add("BPP: 8BPP");
                }
                else if (iBPP == PygmyPBMHeader.PYGMY_PBM_12BPP)
                {
                    BPP = 12;
                    comboBoxBPP.SelectedIndex = 3;
                    treeView1.Nodes.Add("BPP: 12BPP");
                }
                else if (iBPP == PygmyPBMHeader.PYGMY_PBM_16BPP)
                {
                    BPP = 16;
                    comboBoxBPP.SelectedIndex = 4;
                    treeView1.Nodes.Add("BPP: 16BPP");
                }
                else if (iBPP == PygmyPBMHeader.PYGMY_PBM_24BPP)
                {
                    BPP = 24;
                    comboBoxBPP.SelectedIndex = 5;
                    treeView1.Nodes.Add("BPP: 24BPP");
                }

                treeView1.EndUpdate();      // re-enable the tree
                treeView1.Refresh();        // refresh the treeview display
                */
                Pixel = Color.FromArgb(0xFF, 0xFF, 0xFF);

                for (PixelIndex = 0; PixelIndex < Len && (br.BaseStream.Position < br.BaseStream.Length); )
                {
                    Packet = br.ReadByte();
                    Count = (Packet & 0x3F) + 1;
                    if (ImageBPP == PygmyPBMHeader.PYGMY_PBM_1BPP && (Packet & PygmyPBMHeader.PYGMY_PACKET_RLE) == 0)
                    {
                        Count = 7;
                    } // if
                    PixelIndex += Count;

                    for (int i = 0, ii = 0; i < Count; i++)
                    {
                        if (i == 0 || (Packet & PygmyPBMHeader.PYGMY_PACKET_RLE) == 0)
                        {
                            if (ImageBPP == PygmyPBMHeader.PYGMY_PBM_1BPP)
                            {
                                if ((Packet & PygmyPBMHeader.PYGMY_PACKET_RLE) == 0)
                                {
                                    if ((Packet & (PygmyPBMHeader.PYGMY_PACKET_SET >> i)) != 0)
                                    {
                                        Packet |= PygmyPBMHeader.PYGMY_PACKET_SET;
                                    }
                                    else
                                    {
                                        Packet &= ~PygmyPBMHeader.PYGMY_PACKET_SET;
                                    }
                                }
                            }
                            else if (ImageBPP == PygmyPBMHeader.PYGMY_PBM_4BPP)
                            {
                                if (ii == 0)
                                {
                                    Byte1 = br.ReadByte();
                                    byteR = (byte)((int)Byte1 >> 4);
                                    ii = 1;
                                }
                                else
                                {
                                    byteR = (byte)((int)Byte1 & 0x0F);
                                    ii = 0;
                                }
                                byteG = byteR;
                                byteB = byteR;
                            }
                            else if (ImageBPP == PygmyPBMHeader.PYGMY_PBM_12BPP)
                            {
                                if (ii == 0)
                                {
                                    Byte1 = br.ReadByte();
                                    Byte2 = br.ReadByte();
                                    byteR = Byte1;
                                    byteG = (byte)((int)Byte2 >> 4);
                                    byteB = (byte)((int)Byte2 & 0x0F);
                                }
                                else
                                {
                                    Byte3 = br.ReadByte();
                                    byteR = (byte)((int)Byte2 & 0x0F);
                                    byteG = (byte)((int)Byte3 >> 4);
                                    byteB = (byte)((int)Byte3 & 0x0F);
                                }
                            }
                            else if (ImageBPP == PygmyPBMHeader.PYGMY_PBM_8BPP)
                            {
                                Byte1 = br.ReadByte();
                                byteR = (byte)((int)Byte1 >> 5);
                                byteG = (byte)((int)(Byte1 & 0x18) >> 3);
                                byteB = (byte)((int)Byte1 & 0x07);
                                byteR <<= 4;
                                byteG <<= 4;
                                byteB <<= 4;
                            }
                            else if (ImageBPP == PygmyPBMHeader.PYGMY_PBM_16BPP)
                            {
                                Byte1 = br.ReadByte();
                                Byte2 = br.ReadByte();
                                byteR = (byte)((int)Byte1 >> 3);
                                byteG = (byte)((int)((Byte1 & 0x07) << 3) | (Byte2 >> 5));
                                byteB = (byte)((int)Byte2 << 3);//& 0x1F);
                                byteR <<= 3;
                                byteG <<= 2;
                                //byteB <<= 3;
                            }
                            else if (ImageBPP == PygmyPBMHeader.PYGMY_PBM_24BPP)
                            {
                                byteR = br.ReadByte();
                                byteG = br.ReadByte();
                                byteB = br.ReadByte();
                            }
                        } // if
                        if ((Packet & PygmyPBMHeader.PYGMY_PACKET_SET) != 0)
                        {
                            if ((PygmyHeader & PygmyPBMHeader.PYGMY_PBM_FONT) != 0 && (PygmyHeader & PygmyPBMHeader.PYGMY_PBM_1BPP) != 0)
                            {
                                Pixel = Color.FromArgb(0xFF, 0xFF, 0xFF);
                            }
                            else if (ImageBPP != PygmyPBMHeader.PYGMY_PBM_1BPP)
                            {
                                Pixel = Color.FromArgb(byteB, byteG, byteR);
                            }
                            else
                            {
                                //Pixel = Color.FromArgb(byteR, byteG, byteB);
                                Pixel = Color.FromArgb(0xFF, 0xFF, 0xFF);
                            }
                            if (X >= ImageWidth || Y >= ImageHeight)
                            {
                                return;
                            }
                            Pixels[X, Y] = Pixel;
                        }
                        else if ((PygmyHeader & PygmyPBMHeader.PYGMY_PBM_ALPHA) == 0)
                        {
                            if ((PygmyHeader & PygmyPBMHeader.PYGMY_PBM_FONT) != 0 && (PygmyHeader & PygmyPBMHeader.PYGMY_PBM_1BPP) != 0)
                            {
                                Pixel = Color.FromArgb(0x00, 0x00, 0x00);
                            }
                            else
                            {
                                Pixel = Color.FromArgb(0x00, 0x00, 0x00);
                            }
                            if (X >= ImageWidth || Y >= ImageHeight)
                            {
                                return;
                            }
                            Pixels[X, Y] = Pixel;
                        }
                        if (++X == ImageWidth)
                        {
                            X = 0;
                            ++Y;
                        }
                    }
                }
                br.Close();
                //Images[Index] = DrawImage(1);
            } 

            //myBrush.Dispose();
        //}

        public Bitmap Draw( int Zoom )
        {
            System.Drawing.Pen myPen;
            myPen = new System.Drawing.Pen(System.Drawing.Color.Black);
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

            Rectangle rect = new Rectangle();
            ImageBitmap = new Bitmap(ImageWidth * Zoom, ImageHeight * Zoom, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            System.Drawing.Graphics formGraphics = Graphics.FromImage(ImageBitmap);

            if (Pixels != null)
            {
                rect.Width = Zoom;
                rect.Height = Zoom;
                for (int y = 0; y < ImageHeight; y++)
                {
                    for (int x = 0; x < ImageWidth; x++)
                    {
                        myBrush.Color = Pixels[x, y];

                        rect.X = (x * Zoom);
                        rect.Y = (y * Zoom);
                        formGraphics.FillRectangle(myBrush, rect);
                    }
                }
            }
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

        public void Export(string FileName, int BPP)
        {
            int[] ucBitMask = { 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01 };
            int i, ii, ulLen;
            bool Dim16Bit = false;
            int PBMHeader, uiPixelRep, uiRawPixels;
            Color[] tmpPixels;
            Color Pixel, LastPixel;
            PygmyPBMHeader PBM = new PygmyPBMHeader();
            FileStream fs = File.Create(FileName);
            UTF8Encoding utf8 = new UTF8Encoding();

            BinaryWriter bw = new BinaryWriter(fs, utf8);

            //toolStripStatusLabel1.Text = BPP.ToString();
            PBMHeader = PBM.ConvertBPP(BPP); Dim16Bit = false;
            if (ImageWidth > 255 || ImageHeight > 255)
            {
                PBMHeader |= PygmyPBMHeader.PYGMY_PBM_16BITD;
                Dim16Bit = true;
            }

            bw.Write((byte)(PBMHeader >> 8));
            bw.Write((byte)PBMHeader);
            if (Dim16Bit == true)
            {
                bw.Write((byte)(ImageWidth >> 8));
                bw.Write((byte)(ImageWidth));
                bw.Write((byte)(ImageHeight >> 8));
                bw.Write((byte)(ImageHeight));
            }
            else
            {
                bw.Write((byte)ImageWidth);
                bw.Write((byte)ImageHeight);
            }
            i = 0;
            ulLen = ImageWidth * ImageHeight;
            tmpPixels = new Color[ulLen];
            for (int y = 0; y < ImageHeight; y++)
            {
                for (int x = 0; x < ImageWidth; x++, i++)
                {
                    tmpPixels[i] = Pixels[x, y];
                }
            }

            for (i = 0; i < ulLen; )
            {
                LastPixel = tmpPixels[i];
                for (ii = 0, uiPixelRep = 0; i < ulLen; uiPixelRep++, ++i)// range 0-63
                {
                    Pixel = tmpPixels[i];
                    if (++ii == 65)
                    {
                        break;
                    }
                    if (Pixel.R != LastPixel.R || Pixel.G != LastPixel.G || Pixel.B != LastPixel.B)
                    {
                        break;
                    }
                }
                if (BPP == PygmyPBMHeader.PYGMY_PBM_1BPP)
                {
                    if (uiPixelRep > 7)
                    {
                        // RLE
                        if (LastPixel.R > 0 || LastPixel.G > 0 || LastPixel.B > 0)
                        {
                            bw.Write((byte)((uiPixelRep - 1) | PygmyPBMHeader.PYGMY_PBM_RLE | PygmyPBMHeader.PYGMY_PBM_PIXELON)); // BIT7 set for RLE, BIT6 set for pixel ON
                        }
                        else
                        {
                            bw.Write((byte)((uiPixelRep - 1) | PygmyPBMHeader.PYGMY_PBM_RLE)); //BIT7 set for RLE ON, BIT6 clear for pixel off/transparent
                        }
                    }
                    else
                    {
                        // RAW
                        for (ii = 1, i -= uiPixelRep, uiRawPixels = 0; ii < 8 && i < ulLen; i++, ii++)
                        {
                            Pixel = tmpPixels[i];
                            if (Pixel.R > 0 || Pixel.G > 0 || Pixel.B > 0)
                            {
                                uiRawPixels |= ucBitMask[ii];
                            }
                        }
                        bw.Write((byte)uiRawPixels);
                    }
                }
                else if (BPP == PygmyPBMHeader.PYGMY_PBM_4BPP)
                {
                    if (uiPixelRep > 0)
                    {
                        bw.Write((byte)(uiPixelRep - 1) | PygmyPBMHeader.PYGMY_PBM_RLE | PygmyPBMHeader.PYGMY_PBM_PIXELON);
                        bw.Write((byte)tmpPixels[i - 1].R << 4);
                    }
                    else
                    {
                        bw.Write((byte)(uiPixelRep - 1) | PygmyPBMHeader.PYGMY_PBM_PIXELON);
                        for (ii = 0, i -= uiPixelRep; ii < ((uiPixelRep / 2) * 2) && i < ulLen; )
                        {
                            bw.Write((byte)(tmpPixels[i++].R << 4) | (tmpPixels[i++].R & 0x0F));
                            ii += 2;
                        } // for
                        if (ii < uiPixelRep)
                        {
                            bw.Write((byte)(tmpPixels[i++].R << 4));
                        }
                    }
                }
                else if (BPP == PygmyPBMHeader.PYGMY_PBM_8BPP)
                {
                    // ToDo: Figure 8BPP
                    if (uiPixelRep > 0)
                    {
                        bw.Write((byte)(uiPixelRep - 1) | PygmyPBMHeader.PYGMY_PBM_RLE | PygmyPBMHeader.PYGMY_PBM_PIXELON);
                        bw.Write((byte)((LastPixel.R) & 0xE0) | (((LastPixel.G >> 8) & 0xC0) >> 3) | ((LastPixel.B & 0xE0) >> 5));
                    }
                    else
                    {
                        bw.Write((byte)(uiPixelRep - 1) | PygmyPBMHeader.PYGMY_PBM_PIXELON);
                        for (ii = 0, i -= uiPixelRep; ii < uiPixelRep && i < ulLen; ii++, i++)
                        {
                            bw.Write((byte)(tmpPixels[i].R & 0xE0) | ((tmpPixels[i].G & 0xC0) >> 3) |
                                ((tmpPixels[i].B & 0xE0) >> 5));
                        }
                    }
                }
                else if (BPP == PygmyPBMHeader.PYGMY_PBM_12BPP)
                {
                    if (uiPixelRep > 0)
                    {
                        bw.Write((byte)(uiPixelRep - 1) | PygmyPBMHeader.PYGMY_PBM_RLE | PygmyPBMHeader.PYGMY_PBM_PIXELON);
                        bw.Write((byte)(LastPixel.R >> 4) & 0x000F);
                        bw.Write((byte)((LastPixel.G) & 0x00F0) | ((LastPixel.B >> 4) & 0x000F));
                    }
                    else
                    {
                        bw.Write((byte)(uiPixelRep - 1) | PygmyPBMHeader.PYGMY_PBM_PIXELON);
                        for (ii = 0, i -= uiPixelRep; ii < ((uiPixelRep / 2) * 2) && i < ulLen; )
                        {
                            bw.Write((byte)(tmpPixels[i].R & 0xF0) | ((tmpPixels[i].G >> 4) & 0x0F));
                            bw.Write((byte)(tmpPixels[i].B & 0xF0) | ((tmpPixels[i + 1].R >> 4) & 0x0F));
                            bw.Write((byte)(tmpPixels[i + 1].G & 0xF0) | ((tmpPixels[i + 1].B >> 4) & 0x0F));
                            ii += 2;
                            i += 2;
                        } // for
                        if (ii < uiPixelRep)
                        {
                            bw.Write((byte)(tmpPixels[i].R & 0xF0) | ((tmpPixels[i].G >> 4) & 0x0F));
                            bw.Write((byte)(tmpPixels[i].B & 0xF0));
                            ++i;
                        } // if 
                    } // else    
                }
                else if (BPP == PygmyPBMHeader.PYGMY_PBM_16BPP)
                {
                    if (uiPixelRep == 0)
                    {
                        bw.Write((byte)PygmyPBMHeader.PYGMY_PBM_PIXELON);

                    }
                    else
                    {
                        bw.Write((byte)((uiPixelRep - 1) | (PygmyPBMHeader.PYGMY_PBM_RLE | PygmyPBMHeader.PYGMY_PBM_PIXELON)));
                    }
                    bw.Write((byte)((LastPixel.B & 0xF8) | (LastPixel.G >> 5)));
                    bw.Write((byte)(((LastPixel.G & 0x1C) << 3) | ((LastPixel.R >> 3) & 0x1F)));

                    /*    bw.Write((byte)((tmpPixels[ i-1 ].R & 0xF8 ) | (tmpPixels[ i-1 ].G >> 5 ) ) );
                        bw.Write((byte)((tmpPixels[ i-1 ].G & 0x1C ) << 3 ) | (( tmpPixels[ i-1 ].B >> 3 ) & 0x1F ) );
                    } 
                    else
                    { 
                        bw.Write((byte)(uiPixelRep-1) | PYGMY_PBM_PIXELON );
                        for( ii = 0, i -= uiPixelRep; ii < uiPixelRep && i < ulLen; ii++, i++ )
                        {
                            bw.Write((byte)(tmpPixels[ i ].R & 0xF8 ) | (tmpPixels[ i ].G >> 5 ) );
                            bw.Write((byte) ((tmpPixels[ i ].G & 0x1C ) << 3 ) | ((tmpPixels[ i ].B >> 3 ) & 0x1F ) );
                        } // for
                    } // else
                    */
                }
                else if (BPP == PygmyPBMHeader.PYGMY_PBM_24BPP)
                {
                    if (uiPixelRep > 0)
                    {
                        bw.Write((byte)(uiPixelRep - 1) | PygmyPBMHeader.PYGMY_PBM_RLE | PygmyPBMHeader.PYGMY_PBM_PIXELON);
                    }
                    else
                    {
                        bw.Write((byte)PygmyPBMHeader.PYGMY_PBM_PIXELON);
                    }
                    bw.Write((byte)(LastPixel.R));
                    bw.Write((byte)(LastPixel.G));
                    bw.Write((byte)(LastPixel.B));
                    /*} 
                    else
                    { 
                        bw.Write((byte)(uiPixelRep-1) | PYGMY_PBM_PIXELON );
                        //for( ii = 0, i -= uiPixelRep; ii < uiPixelRep && i < ulLen; ii++, i++ )
                        for (ii = 0, i -= uiPixelRep; ii < uiPixelRep && i < ulLen; ii++, i++)
                        {
                            bw.Write((byte)tmpPixels[ i ].R );
                            bw.Write((byte)tmpPixels[ i ].G );
                            bw.Write((byte)tmpPixels[ i ].B );
                        } 
                    } */
                }
            }
            bw.Flush();
            bw.Close();
            fs.Close();
        }
    }
}
