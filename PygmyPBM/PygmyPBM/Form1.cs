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
        /*const int PYGMY_PBM_TYPEMASK				= 0xF000;
        const int PYGMY_PBM_IMAGE				    = 0x0000;
        const int PYGMY_PBM_IMAGESTRIP			    = 0x1000;
        const int PYGMY_PBM_ANIMATION			    = 0x2000;
        const int PYGMY_PBM_VIDEO				    = 0x3000;
        const int PYGMY_PBM_AUDIO				    = 0x4000;
        const int PYGMY_PBM_BINARY				    = 0x5000;
        const int PYGMY_PBM_TEXT					= 0x6000;
        const int PYGMY_PBM_EXECUTABLE			    = 0x7000;
        const int PYGMY_PBM_BYTECODE				= 0x8000;
        const int PYGMY_PBM_LIBRARY				    = 0x9000;
        const int PYGMY_PBM_GUI					    = 0xA000;
        const int PYGMY_PBM_SCRIPT				    = 0xB000;
        const int PYGMY_PBM_DRIVER				    = 0xC000;
        const int PYGMY_PBM_ARCHIVE				    = 0xD000;
        const int PYGMY_PBM_STREAM				    = 0xE000;
        const int PYGMY_PBM_FONT  				    = 0xF000;	

        const int PYGMY_PBM_TABLEMASK			    = 0x0300;
        const int PYGMY_PBM_TABLE16				    = 0x0000;
        const int PYGMY_PBM_TABLE32				    = 0x0400;
        const int PYGMY_PBM_DIMENSIONSIZEMASK	    = 0x0100;
        const int PYGMY_PBM_8BITD				    = 0x0000;
        const int PYGMY_PBM_16BITD				    = 0x0100;
        const int PYGMY_PBM_DRAWSTYLEMASK		    = 0x00C0;
        const int PYGMY_PBM_RAW					    = 0x0000;
        const int PYGMY_PBM_ALPHA				    = 0x0040;
        const int PYGMY_PBM_SCATTER				    = 0x0080;
        const int PYGMY_PBM_VECTOR				    = 0x00C0;
        const int PYGMY_PBM_CRCMASK				    = 0x0020;
        const int PYGMY_PBM_CRC					    = 0x0020;
        const int PYGMY_PBM_VERSIONMASK			    = 0x0010;
        const int PYGMY_PBM_VERSION				    = 0x0010;
        const int PYGMY_PBM_BPPMASK				    = 0x000F;
        const int PYGMY_PBM_1BPP					= 0x0000;
        const int PYGMY_PBM_4BPP					= 0x0001;
        const int PYGMY_PBM_8BPP					= 0x0002;
        const int PYGMY_PBM_12BPP                   = 0x0003;
        const int PYGMY_PBM_16BPP				    = 0x0004;
        const int PYGMY_PBM_24BPP				    = 0x0005;
        const int PYGMY_PBM_32BPP				    = 0x0006;
        const int PYGMY_PACKET_RLE                  = 0x80;
        const int PYGMY_PACKET_SET                  = 0x40;
        const int PYGMY_PBM_PIXELON                 = 0x40;
        const int PYGMY_PBM_RLE                     = 0x80;

        const int PYGMY_VECTOR_MASK                 = 0xE0;
        const int PYGMY_VECTOR_END                  = 0x00;
        const int PYGMY_VECTOR_COLOR                = 0x20;
        const int PYGMY_VECTOR_POLY                 = 0x40;
        const int PYGMY_VECTOR_ARC                  = 0x60;
        const int PYGMY_VECTOR_SPLINE               = 0x80;
        const int PYGMY_VECTOR_TEXT                 = 0xA0;
        const int PYGMY_VECTOR_RASTER               = 0xC0;*/

        //PBMVectorData[] vectorData = new PBMVectorData[1]; 
        PBMVector Vectors = new PBMVector();
        private System.Drawing.Bitmap ImageBitmap;
        BinaryReader imageFile;
        public int Zoom, Index, ImageWidth, ImageHeight, BPP, Header, ImageFrames, ImageCurrentFrame;
        public bool DrawActive;
        public Color[,] Pixels;
        public Color CurrentColor;
        public string ImageName, ImageType;

        

        int guiGetEntries()
        {
            int iEntries, iPygmyInfo;

            iPygmyInfo = guiGetHeader();

            if ( (iPygmyInfo & (PygmyPBMHeader.PYGMY_PBM_IMAGESTRIP | PygmyPBMHeader.PYGMY_PBM_FONT)) == 0)
            {
                ImageFrames = 0;
                return (0);
            } 
            // uiGetHeader leaves the file at the frame count
            iEntries = (int)imageFile.ReadByte() << 8;
            iEntries |= (int)imageFile.ReadByte();
            ImageFrames = iEntries;

            return (iEntries);
        }

        int guiGetHeader()
        {
            imageFile.BaseStream.Seek(0, SeekOrigin.Begin );
            Header = (int)imageFile.ReadByte() << 8;
            Header |= (int)imageFile.ReadByte();

            return (Header);
        }

        int guiGetImage( int uiIndex)
        {
            // This function seeks for image in imagestrip or font based on provided index
            int iPixelIndex;
            int iEntries, iPygmyInfo;

            iPygmyInfo = guiGetHeader();
            iEntries = guiGetEntries();
            if ((iPygmyInfo & (PygmyPBMHeader.PYGMY_PBM_IMAGESTRIP | PygmyPBMHeader.PYGMY_PBM_FONT))==0)
            {
                return (0);
            } 
            if (iEntries == 0 || uiIndex > iEntries)
            {
                return (0); // index past entries
            } 
            if ((iPygmyInfo & PygmyPBMHeader.PYGMY_PBM_TABLE32) != 0)
            {				
                // Table Entries are 32bit
                imageFile.BaseStream.Seek( uiIndex*4, SeekOrigin.Begin );
                
                iPixelIndex =  (int)imageFile.ReadByte( ) << 24;
                iPixelIndex |= (int)imageFile.ReadByte( ) << 16;
                iPixelIndex |= (int)imageFile.ReadByte( ) << 8;
                iPixelIndex |= (int)imageFile.ReadByte( );
            }
            else
            { 										
                // Table Entries are 16bit
                imageFile.BaseStream.Seek(4 + (uiIndex * 2), SeekOrigin.Begin);
                
                iPixelIndex =  (int)imageFile.ReadByte( ) << 8;
                iPixelIndex |= (int)imageFile.ReadByte( );
            }

            // The index returned is the absolute offset from the start of file
            return (iPixelIndex);
        }
        public void LoadImage( string FileName, int iIndex )
        {
            int iPixelIndex, iLen, iCount, iPygmyInfo, iPacket, iWidth, iHeight, iBPP;
            int X = 0, Y = 0;
            byte Byte1=0, Byte2=0, Byte3=0, byteR=0, byteG=0, byteB=0;
            Color Pixel;
            int Zoom = GetZoom();

            toolStripStatusLabel2.Text = "Loading" + FileName;
            if (!File.Exists(FileName))
            {
                toolStripStatusLabel2.Text = "File Failed to Open!";
            }
            else
            {
                imageFile = new BinaryReader( File.OpenRead(@FileName));
                
                iPixelIndex = guiGetImage( iIndex );
                imageFile.BaseStream.Seek( iPixelIndex, SeekOrigin.Begin);
                if (comboBoxFrames.SelectedIndex != 0 || comboBoxFrames.SelectedIndex > ImageFrames )
                {
                    comboBoxFrames.Items.Clear();
                    for (int i = 0; i < ImageFrames+1; i++)
                    {
                        comboBoxFrames.Items.Add(i.ToString());
                    }
                    comboBoxFrames.SelectedIndex = 0;
                }
                
                // Warning! ulPixelIndex is reused below

                iPygmyInfo = (int)imageFile.ReadByte( ) << 8;
                iPygmyInfo |= (int)imageFile.ReadByte( );

                if ( (iPygmyInfo & PygmyPBMHeader.PYGMY_PBM_16BITD) != 0)
                {		// Determine 8 or 16 bit Width and Height fields
                    iWidth = (int)imageFile.ReadByte() << 8;
                    iWidth |= (int)imageFile.ReadByte();
                    iHeight = (int)imageFile.ReadByte() << 8;
                    iHeight |= (int)imageFile.ReadByte();
                }
                else
                {
                    iWidth = (int)imageFile.ReadByte();
                    iHeight = (int) imageFile.ReadByte(); ;
                } // else
                ImageWidth = iWidth;
                ImageHeight = iHeight;
                Pixels = new Color[iWidth,iHeight];
                iLen = iWidth * iHeight;
                iBPP = iPygmyInfo & 0x000F;

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
                treeView1.Nodes.Add("Pixels: " + iLen.ToString() );
                treeView1.Nodes.Add("Width: " + iWidth.ToString());
                treeView1.Nodes.Add("Height: " + iHeight.ToString());
                treeView1.Nodes.Add("Frames: " + ImageFrames.ToString());
                if( iBPP == PygmyPBMHeader.PYGMY_PBM_1BPP )
                {
                    BPP = 1;
                    comboBoxBPP.SelectedIndex = 0;
                    treeView1.Nodes.Add("BPP: 1BPP" );
                }
                else if( iBPP == PygmyPBMHeader.PYGMY_PBM_4BPP )
                {
                    BPP = 4;
                    comboBoxBPP.SelectedIndex = 1;
                    treeView1.Nodes.Add("BPP: 4BPP" );
                } 
                else if( iBPP == PygmyPBMHeader.PYGMY_PBM_8BPP )
                {
                    BPP = 8;
                    comboBoxBPP.SelectedIndex = 2;
                    treeView1.Nodes.Add("BPP: 8BPP" );
                }
                else if( iBPP == PygmyPBMHeader.PYGMY_PBM_12BPP )
                {
                    BPP = 12;
                    comboBoxBPP.SelectedIndex = 3;
                    treeView1.Nodes.Add( "BPP: 12BPP" );
                } 
                else if( iBPP == PygmyPBMHeader.PYGMY_PBM_16BPP )
                {
                    BPP = 16;
                    comboBoxBPP.SelectedIndex = 4;
                    treeView1.Nodes.Add("BPP: 16BPP" );
                } 
                else if( iBPP == PygmyPBMHeader.PYGMY_PBM_24BPP )
                {
                    BPP = 24;
                    comboBoxBPP.SelectedIndex = 5;
                    treeView1.Nodes.Add( "BPP: 24BPP" );
                } 
                
                treeView1.EndUpdate();      // re-enable the tree
                treeView1.Refresh();        // refresh the treeview display

                Pixel = Color.FromArgb(0xFF, 0xFF, 0xFF);
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(Pixel);
                System.Drawing.Graphics formGraphics = this.CreateGraphics();

                for (iPixelIndex = 0; iPixelIndex < iLen && ( imageFile.BaseStream.Position < imageFile.BaseStream.Length ) ; )
                {

                    iPacket = imageFile.ReadByte();
                    iCount = (iPacket & 0x3F) + 1;
                    if (iBPP == PygmyPBMHeader.PYGMY_PBM_1BPP && (iPacket & PygmyPBMHeader.PYGMY_PACKET_RLE) == 0)
                    {
                        iCount = 7;
                    } // if

                    iPixelIndex += iCount;

                    for (int i = 0, ii = 0; i < iCount; i++)
                    {
                        if (i == 0 || (iPacket & PygmyPBMHeader.PYGMY_PACKET_RLE) == 0)
                        {
                            if (iBPP == PygmyPBMHeader.PYGMY_PBM_1BPP)
                            {
                                if ((iPacket & PygmyPBMHeader.PYGMY_PACKET_RLE) == 0)
                                {
                                    if ((iPacket & (PygmyPBMHeader.PYGMY_PACKET_SET >> i)) != 0)
                                    {
                                        iPacket |= PygmyPBMHeader.PYGMY_PACKET_SET;
                                    }
                                    else
                                    {
                                        iPacket &= ~PygmyPBMHeader.PYGMY_PACKET_SET;
                                    } // else

                                } // if
                            }
                            else if (iBPP == PygmyPBMHeader.PYGMY_PBM_4BPP)
                            {
                                if ( ii == 0 )
                                {
                                    Byte1 = imageFile.ReadByte();
                                    byteR = (byte)((int)Byte1 >> 4);
                                    ii = 1;
                                }
                                else
                                {
                                    byteR = (byte)((int)Byte1 & 0x0F);
                                    ii = 0;
                                } // else
                                byteG = byteR;
                                byteB = byteR;
                            }
                            else if (iBPP == PygmyPBMHeader.PYGMY_PBM_12BPP)
                            {
                                if (ii == 0)
                                {
                                    Byte1 = imageFile.ReadByte();
                                    Byte2 = imageFile.ReadByte();
                                    byteR = Byte1;
                                    byteG = (byte)((int)Byte2 >> 4);
                                    byteB = (byte)((int)Byte2 & 0x0F);
                                }
                                else
                                {
                                    Byte3 = imageFile.ReadByte();
                                    byteR = (byte)((int)Byte2 & 0x0F);
                                    byteG = (byte)((int)Byte3 >> 4);
                                    byteB = (byte)((int)Byte3 & 0x0F);
                                } // else
                            }
                            else if (iBPP == PygmyPBMHeader.PYGMY_PBM_8BPP)
                            {
                                Byte1 = imageFile.ReadByte();
                                byteR = (byte)((int)Byte1 >> 5);
                                byteG = (byte)((int)(Byte1 & 0x18) >> 3);
                                byteB = (byte)((int)Byte1 & 0x07);
                                byteR <<= 4;
                                byteG <<= 4;
                                byteB <<= 4;
                            }
                            else if (iBPP == PygmyPBMHeader.PYGMY_PBM_16BPP)
                            {
                                Byte1 = imageFile.ReadByte();
                                Byte2 = imageFile.ReadByte();
                                byteR = (byte)((int)Byte1 >> 3);
                                byteG = (byte)((int)((Byte1 & 0x07) << 3) | (Byte2 >> 5));
                                byteB = (byte)((int)Byte2 <<3);//& 0x1F);
                                byteR <<= 3;
                                byteG <<= 2;
                                //byteB <<= 3;
                            }
                            else if (iBPP == PygmyPBMHeader.PYGMY_PBM_24BPP)
                            {
                                byteR = imageFile.ReadByte();
                                byteG = imageFile.ReadByte();
                                byteB = imageFile.ReadByte();
                            } // else if
                                   
                        } // if
                        if ( (iPacket & PygmyPBMHeader.PYGMY_PACKET_SET) != 0)
                        {
                            if ((iPygmyInfo & PygmyPBMHeader.PYGMY_PBM_FONT) != 0 && (iPygmyInfo & PygmyPBMHeader.PYGMY_PBM_1BPP) != 0)
                            {
                                Pixel = Color.FromArgb(0xFF, 0xFF, 0xFF);
                            }
                            else if (iBPP != PygmyPBMHeader.PYGMY_PBM_1BPP)
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
                        else if ((iPygmyInfo & PygmyPBMHeader.PYGMY_PBM_ALPHA) == 0)
                        {
                            if ((iPygmyInfo & PygmyPBMHeader.PYGMY_PBM_FONT) != 0 && (iPygmyInfo & PygmyPBMHeader.PYGMY_PBM_1BPP) != 0)
                            {
                                Pixel = Color.FromArgb(0x00, 0x00, 0x00);//guiApplyFontBackColor();
                            }
                            else
                            {
                                Pixel = Color.FromArgb(0x00, 0x00, 0x00);//guiApplyBackColor();
                            }
                            if (X >= ImageWidth || Y >= ImageHeight)
                            {
                                return;
                            }
                            Pixels[X, Y] = Pixel;
                        } 
                        if (++X == iWidth)
                        {
                            X = 0;
                            ++Y;
                        } // if
                    } // for
                } // for
                
                imageFile.Close();
                myBrush.Dispose();
                formGraphics.Dispose();
            }
        }

        public void DrawImage()
        {
            System.Drawing.Pen myPen;
            myPen = new System.Drawing.Pen(System.Drawing.Color.Black);
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            
            Rectangle rect = new Rectangle();
            int Zoom = GetZoom();
            ImageBitmap = new Bitmap( ImageWidth * Zoom, ImageHeight * Zoom, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            System.Drawing.Graphics formGraphics = Graphics.FromImage( ImageBitmap);
            
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
                for (int x = 0, GridWidth = ImageWidth * Zoom; x < ImageWidth+1; x++)
                {
                    formGraphics.DrawLine(myPen, (int)(x * Zoom), (int)0, (int)(x * Zoom), (int)GridWidth);
                }
                for (int y = 0, GridHeight = ImageHeight * Zoom; y < ImageHeight+1; y++)
                {
                    formGraphics.DrawLine(myPen, (int)0, (int)(y * Zoom), (int)GridHeight, (int)(y * Zoom));
                }
            }
            pictureBox1.Image = ImageBitmap;
            myPen.Dispose();
            myBrush.Dispose();
            formGraphics.Dispose();
            //panel1.Invalidate();
        }

        public void exportPBM( string FileName, int BPP )
        {
            int[] ucBitMask = { 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01 };
            int i, ii, ulLen;
            bool Dim16Bit = false;
            int PBMHeader, uiPixelRep, uiRawPixels;
            Color[] tmpPixels;
            Color Pixel, LastPixel;
            PygmyPBMHeader PBM = new PygmyPBMHeader();
            FileStream fs = File.Create( FileName );
            UTF8Encoding utf8 = new UTF8Encoding();

            BinaryWriter bw = new BinaryWriter(fs, utf8);

            toolStripStatusLabel1.Text = BPP.ToString();
            PBMHeader = PBM.ConvertBPP(BPP);Dim16Bit = false;
            if (ImageWidth > 255 || ImageHeight > 255)
            {
                PBMHeader |= PygmyPBMHeader.PYGMY_PBM_16BITD;
                Dim16Bit = true;
            }
            
            bw.Write( (byte)(PBMHeader >> 8) );
            bw.Write( (byte)PBMHeader );
            if ( Dim16Bit == true)
            {
                bw.Write((byte)(ImageWidth>>8));
                bw.Write((byte)(ImageWidth));
                bw.Write((byte)(ImageHeight>>8));
                bw.Write((byte)(ImageHeight));
            }
            else
            {
                bw.Write((byte)ImageWidth);
                bw.Write((byte)ImageHeight);
            }
            i = 0;
            ulLen = ImageWidth*ImageHeight;
            tmpPixels = new Color[ulLen];
            for( int y = 0; y < ImageHeight; y++ )
            {
                for( int x = 0; x < ImageWidth; x++, i++ )
                {
                    tmpPixels[i] = Pixels[x, y];
                }
            }
            
            for( i = 0; i < ulLen; ) 
            {
                LastPixel = tmpPixels[ i ];
                for (ii = 0, uiPixelRep = 0; i < ulLen; uiPixelRep++, ++i)// range 0-63
                {	
                    Pixel = tmpPixels[ i ];
                    if( ++ii == 65 )
                    {
                        break;
                    }  
                    if( Pixel.R != LastPixel.R || Pixel.G != LastPixel.G || Pixel.B != LastPixel.B )
                    {
                        break;
                    }
		        }
                if( BPP == PygmyPBMHeader.PYGMY_PBM_1BPP )
                {
                    if( uiPixelRep > 7 ) 
                    {
                        // RLE
                        if( LastPixel.R > 0 || LastPixel.G > 0 || LastPixel.B > 0 )
                        {
                            bw.Write((byte)((uiPixelRep-1) | PygmyPBMHeader.PYGMY_PBM_RLE | PygmyPBMHeader.PYGMY_PBM_PIXELON)); // BIT7 set for RLE, BIT6 set for pixel ON
                        } 
                        else
                        {
                            bw.Write((byte)((uiPixelRep-1) | PygmyPBMHeader.PYGMY_PBM_RLE )); //BIT7 set for RLE ON, BIT6 clear for pixel off/transparent
                        } 
                    } 
                    else 
                    {
                        // RAW
                        for( ii = 1, i-=uiPixelRep, uiRawPixels = 0; ii < 8 && i < ulLen; i++, ii++)
                        {
                            Pixel = tmpPixels[i];
                            if (Pixel.R > 0 || Pixel.G > 0 || Pixel.B > 0)
                            {
                                uiRawPixels |= ucBitMask[ii];
                            } 
                        } 
                        bw.Write((byte)uiRawPixels ); 
                    } 
                } 
                else if( BPP == PygmyPBMHeader.PYGMY_PBM_4BPP )
                {
                    if( uiPixelRep > 0 )
                    {
                        bw.Write((byte)(uiPixelRep-1) | PygmyPBMHeader.PYGMY_PBM_RLE | PygmyPBMHeader.PYGMY_PBM_PIXELON);
                        bw.Write((byte)tmpPixels[ i-1 ].R << 4 );    
                    } 
                    else
                    {
                        bw.Write((byte)(uiPixelRep-1) | PygmyPBMHeader.PYGMY_PBM_PIXELON );
                        for( ii = 0, i -= uiPixelRep; ii < ( ( uiPixelRep / 2 ) * 2 ) && i < ulLen; )
                        {
                            bw.Write((byte)(tmpPixels[ i++ ].R << 4) | ( tmpPixels[ i++ ].R & 0x0F ));
                            ii += 2;
                        } // for
                        if( ii < uiPixelRep )
                        {
                            bw.Write((byte)(tmpPixels[ i++ ].R << 4 )); 
                        }
                    } 
                } 
                else if( BPP == PygmyPBMHeader.PYGMY_PBM_8BPP )
                {
                    // ToDo: Figure 8BPP
                    if( uiPixelRep > 0 )
                    {
                        bw.Write((byte)(uiPixelRep-1) | PygmyPBMHeader.PYGMY_PBM_RLE | PygmyPBMHeader.PYGMY_PBM_PIXELON );
                        bw.Write((byte)((LastPixel.R) & 0xE0 ) | (((LastPixel.G >> 8) & 0xC0 ) >> 3) | (( LastPixel.B & 0xE0) >> 5) );
                    } 
                    else
                    { 
                        bw.Write((byte)(uiPixelRep-1) | PygmyPBMHeader.PYGMY_PBM_PIXELON );
                        for( ii = 0, i -= uiPixelRep; ii < uiPixelRep && i < ulLen; ii++, i++ )
                        {
                            bw.Write((byte)(tmpPixels[ i ].R & 0xE0 ) | ((tmpPixels[ i ].G & 0xC0 ) >> 3) | 
                                (( tmpPixels[ i ].B & 0xE0) >> 5) );
                        } 
                    } 
                } 
                else if( BPP == PygmyPBMHeader.PYGMY_PBM_12BPP )
                {
                    if( uiPixelRep > 0 )
                    {
                        bw.Write((byte)(uiPixelRep-1) | PygmyPBMHeader.PYGMY_PBM_RLE | PygmyPBMHeader.PYGMY_PBM_PIXELON );
                        bw.Write((byte)(LastPixel.R>>4)&0x000F );
                        bw.Write((byte) ((LastPixel.G)&0x00F0)|((LastPixel.B>>4)&0x000F) );
                    } 
                    else
                    {
                        bw.Write((byte)(uiPixelRep-1) | PygmyPBMHeader.PYGMY_PBM_PIXELON );
                        for( ii = 0, i -= uiPixelRep; ii < ( ( uiPixelRep / 2 ) * 2 ) && i < ulLen; )
                        {
                            bw.Write((byte)(tmpPixels[ i ].R & 0xF0 ) | ( (tmpPixels[ i ].G>>4) & 0x0F ) );
                            bw.Write((byte)(tmpPixels[ i ].B & 0xF0 ) | ( (tmpPixels[ i+1 ].R>>4) & 0x0F ) ); 
                            bw.Write((byte)(tmpPixels[ i+1 ].G & 0xF0 ) | ( (tmpPixels[ i+1 ].B>>4) & 0x0F ) );
                            ii += 2;
                            i += 2;
                        } // for
                        if( ii < uiPixelRep )
                        {
                            bw.Write((byte)(tmpPixels[ i ].R & 0xF0 ) | ( (tmpPixels[ i ].G>>4) & 0x0F ) );
                            bw.Write((byte)(tmpPixels[ i ].B & 0xF0 ) ); 
                            ++i;
                        } // if 
                    } // else    
                } 
                else if( BPP == PygmyPBMHeader.PYGMY_PBM_16BPP ) 
                {
                    if( uiPixelRep == 0   )
                    {
                        bw.Write((byte)PygmyPBMHeader.PYGMY_PBM_PIXELON );
                        
                    }
                    else
                    {
                        bw.Write((byte)((uiPixelRep-1) | (PygmyPBMHeader.PYGMY_PBM_RLE | PygmyPBMHeader.PYGMY_PBM_PIXELON) )); 
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
                else if( BPP == PygmyPBMHeader.PYGMY_PBM_24BPP ) 
                {
                    if( uiPixelRep > 0 )
                    {
                        bw.Write((byte)(uiPixelRep-1) | PygmyPBMHeader.PYGMY_PBM_RLE | PygmyPBMHeader.PYGMY_PBM_PIXELON );
                    }
                    else
                    {
                        bw.Write((byte) PygmyPBMHeader.PYGMY_PBM_PIXELON );
                    }
                        bw.Write((byte)(LastPixel.R) );
                        bw.Write((byte)(LastPixel.G) );
                        bw.Write((byte)(LastPixel.B) );
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

        public void SetZoom(int iZoom)
        {
            Zoom = iZoom;
        }

        public int GetZoom()
        {
            return (Zoom);
        }

    
        public Form1()
        {
            InitializeComponent();
            
        }

        /*private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            //Graphics formGraphics = e.Graphics;
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            System.Drawing.Graphics formGraphics = this.CreateGraphics();
            formGraphics.FillRectangle(myBrush, new Rectangle(0, 0, 200, 300));
            myBrush.Dispose();
            formGraphics.Dispose();
        }*/

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AddOwnedForm(new FormNewImage() );
            CurrentColor = Color.FromArgb(0x00, 0x00, 0x00);
            buttonColor.BackColor = CurrentColor;
            SetZoom(1);
            //propertyGrid1.Controls.Add( imgProperties );
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
            if (ImageType == "Vector")
            {
                ImageBitmap = Vectors.Draw(ImageWidth, ImageHeight, GetZoom());
                pictureBox1.Image = ImageBitmap;
            }
            else
            {
                LoadImage(ImageName, 0);
                DrawImage();
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            ImageName = saveFileDialog1.FileName;

            exportPBM(ImageName, BPP);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //propertyGrid1.Width = this.Width - propertyGrid1.Location.X;
            //propertyGrid1.Height = this.Height - statusStrip1.Height;
            
        }

       
        private void toolStripZoom100_Click(object sender, EventArgs e)
        {
            SetZoom(1);
            if (ImageType == "Vector")
            {
                ImageBitmap = Vectors.Draw(ImageWidth, ImageHeight, 1);
                pictureBox1.Image = ImageBitmap;
            }
            else
            {
                DrawImage();
            }
        }

        private void toolStripZoom200_Click(object sender, EventArgs e)
        {
            SetZoom(2);
            if (ImageType == "Vector")
            {
                ImageBitmap = Vectors.Draw(ImageWidth, ImageHeight, 2);
                pictureBox1.Image = ImageBitmap;
            }
            else
            {
                DrawImage();
            }
        }

        private void toolStripZoom300_Click(object sender, EventArgs e)
        {
            SetZoom(3);
            if (ImageType == "Vector")
            {
                ImageBitmap = Vectors.Draw(ImageWidth, ImageHeight, 3);
                pictureBox1.Image = ImageBitmap;
            }
            else
            {
                DrawImage();
            }
        }

        private void toolStripZoom400_Click(object sender, EventArgs e)
        {
            SetZoom(4);
            if (ImageType == "Vector")
            {
                ImageBitmap = Vectors.Draw(ImageWidth, ImageHeight, 4);
                pictureBox1.Image = ImageBitmap;
            }
            else
            {
                DrawImage();
            }
        }

        private void toolStripZoom800_Click(object sender, EventArgs e)
        {
            SetZoom(8);
            if (ImageType == "Vector")
            {
                ImageBitmap = Vectors.Draw(ImageWidth, ImageHeight, 8);
                pictureBox1.Image = ImageBitmap;
            }
            else
            {
                DrawImage();
            }
        }

        private void toolStripZoom1600_Click(object sender, EventArgs e)
        {
            SetZoom(16);
            if (ImageType == "Vector")
            {
                ImageBitmap = Vectors.Draw(ImageWidth, ImageHeight, 16);
                pictureBox1.Image = ImageBitmap;
            }
            else
            {
                DrawImage();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            DrawActive = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int Zoom = GetZoom();
            int iGridX = ((int)e.X / Zoom);
            int iGridY = ((int)e.Y / Zoom);

            toolStripStatusLabel1.Text = iGridX + ", " + iGridY;
            if (DrawActive == true)
            {

                if (iGridX < ImageWidth && iGridY < ImageHeight)
                {
                    if (ImageType == "Vector")
                    {
                        Vectors.AddVertice(iGridX, iGridY);
                        //vectorData[0].AddVertice(iGridX, iGridY);
                        //vectorData[0].Type = PBMVectorData.TypePoly;
                        ImageBitmap = Vectors.Draw(ImageWidth, ImageHeight, Zoom);
                        pictureBox1.Image = ImageBitmap;
                    }
                    else
                    {
                        Pixels[iGridX, iGridY] = CurrentColor;
                        DrawImage();
                    }
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
            int iGridX = ((int)e.X / Zoom);
            int iGridY = ((int)e.Y / Zoom);

            if (iGridX < ImageWidth && iGridY < ImageHeight)
            {
                if (ImageType == "Vector")
                {
                    //vectorData[0] = new PBMVectorData();
                    //vectorData[0].Type = PBMVectorData.TypePoly;
                    
                    Vectors.AddVertice(iGridX, iGridY);
                    
                    ImageBitmap = Vectors.Draw(ImageWidth, ImageHeight, GetZoom());
                    pictureBox1.Image = ImageBitmap;
                }
                else
                {
                    Pixels[iGridX, iGridY] = CurrentColor;
                    DrawImage();
                }
            }
        }

        private void comboBoxFrames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if( ImageType == "Vector" )
            {
                ImageBitmap = Vectors.Draw(ImageWidth, ImageHeight, GetZoom());
                pictureBox1.Image = ImageBitmap;
            }
            else if (ImageName != null && comboBoxFrames.SelectedIndex < ImageFrames)
            {

                LoadImage(ImageName, comboBoxFrames.SelectedIndex );
                DrawImage();
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
            ImageWidth = formNewImage.ImageWidth;
            ImageHeight = formNewImage.ImageHeight;
            Pixels = new Color[ImageWidth , ImageHeight];
            ImageType = formNewImage.ImageType;
            labelImageType.Text = ImageType;
            BPP = formNewImage.ImageBPP;
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
            toolStripStatusLabelText.Text = "Created Image " + ImageWidth.ToString() + " " + ImageHeight.ToString();
            if (ImageType == "Vector")
            {
                ImageBitmap = Vectors.Draw(ImageWidth, ImageHeight, GetZoom());
                pictureBox1.Image = ImageBitmap;
            }
            else
            {
                DrawImage();
            }
            
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
            if (ImageType == "Vector")
            {
                Vectors.Export(ImageName);
            }
        }

        
    }
}
