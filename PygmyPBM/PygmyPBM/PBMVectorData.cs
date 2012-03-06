using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class PBMVectorData
    {
        // Vector Image Packet in Pygmy PBM File uses 3 MSb for Type and 5 LSb for vertices count
        public int Type;
        public int Length;
        //public int Width, Height;
        public const int TypeBrush = 0, TypeText = 1, TypeRaster = 2, TypePixel = 3, TypePoly = 4, TypeArc = 5, TypeSpline = 6; 
        public int[,] Vertices;
        public int RasterWidth, RasterHeight;
        public Color[,] RasterData;
        public Color BrushColor;
        public Color OriginColor;
        public Color VerticeColor;
        public string Text;

        public PBMVectorData()
        {
            RasterWidth = 0;
            RasterHeight = 0;
            Text = "";
            Length = 0;
            Vertices = new int[1, 2];
        }

        public PBMVectorData(int[,] verticeArray)
        {
            BrushColor = Color.FromArgb(0xFF, 0xFF, 0xFF);
            OriginColor = Color.FromArgb(0xFF, 0x00, 0x00);
            VerticeColor = Color.FromArgb(0xA0, 0x00, 0xA0);
            Vertices = verticeArray;
            Length = 0;
            Vertices = new int[1, 2];
        }

        public PBMVectorData(int iType, int[,] verticeArray)
        {
            BrushColor = Color.FromArgb(0xFF, 0xFF, 0xFF);
            OriginColor = Color.FromArgb(0xFF, 0x00, 0x00);
            VerticeColor = Color.FromArgb(0xA0, 0x00, 0xA0);
            Vertices = verticeArray;
            Type = iType;
            Length = 0;
            Vertices = new int[1, 2];
        }

        public int GetLength()
        {
            return (Length);
        }

        public void AddRaster(int iWidth, int iHeight, Color[,] iRaster)
        {
            RasterWidth = iWidth;
            RasterHeight = iHeight;
            if (iRaster.Length != iWidth * iHeight)
            {
                return;
            }
            RasterData = new Color[iWidth, iHeight];
            for (int y = 0; y < iHeight; y++)
            {
                for (int x = 0; x < iWidth; x++)
                {
                    RasterData[x, y] = iRaster[x,y];
                }
            }
        }

        public void AddVertice(int x, int y)
        {
            int[,] tmpVertices;
            
            tmpVertices = new int[Length+1, 2];
            
            for( int i = 0; i < Length; i++ )
            {
                tmpVertices[i, 0] = Vertices[i, 0];
                tmpVertices[i, 1] = Vertices[i, 1];
            }
            tmpVertices[Length, 0] = x;
            tmpVertices[Length, 1] = y;
            ++Length;
            Vertices = tmpVertices;
        }

        public void InsertVertice(int offset, int x, int y)
        {
            int[,] tmpVertices;
            tmpVertices = new int[++Length, 2];

            for (int i = 0; i <Length; i++)
            {
                if (i == offset)
                {
                    tmpVertices[i, 0] = x;
                    tmpVertices[i, 1] = y;
                }
                else if (i > offset)
                {
                    tmpVertices[i, 0] = Vertices[i-1, 0];
                    tmpVertices[i, 1] = Vertices[i-1, 1];
                }
                else
                {
                    tmpVertices[i, 0] = Vertices[i, 0];
                    tmpVertices[i, 1] = Vertices[i, 1];
                }
            }
            
            Vertices = tmpVertices;
        }

        public bool IsOriginVertice(int xSeek, int ySeek)
        {
            if (Vertices[0, 0] == xSeek && Vertices[0, 1] == ySeek)
            {
                return (true);
            }

            return (false);
        }

        public bool IsVertice(int xSeek, int ySeek)
        {
            for (int i = 0; i < Length; i++)
            {
                if (Vertices[i, 0] == xSeek && Vertices[i, 1] == ySeek)
                {
                    return (true);
                }
            }

            return (false);
        }

        public void InsertVerticeAfter(int x, int y, int xSeek, int ySeek )
        {
            int[,] tmpVertices;
            tmpVertices = new int[++Length, 2];
            bool foundSeek = false;

            for (int i = 0; i < Length; i++)
            {
                if ( Vertices[i,0] == xSeek && Vertices[i,1] == ySeek )
                {
                    tmpVertices[i, 0] = x;
                    tmpVertices[i, 1] = y;
                    foundSeek = true;
                }
                else if (foundSeek)
                {
                    tmpVertices[i, 0] = Vertices[i - 1, 0];
                    tmpVertices[i, 1] = Vertices[i - 1, 1];
                }
                else
                {
                    tmpVertices[i, 0] = Vertices[i, 0];
                    tmpVertices[i, 1] = Vertices[i, 1];
                }
            }
            Vertices = tmpVertices;
        }

        public void RemoveVertice( )
        {
            // Removes the last Vertice added
            if( Vertices.Length == 0 )
            {
                return;
            }
            int[,] tmpVertices;
            tmpVertices = new int[--Length, 2];

            for( int i = 0; i < Length; i++)
            {
                tmpVertices[i, 0] = Vertices[i, 0];
                tmpVertices[i, 1] = Vertices[i, 1];
            }
            Vertices = tmpVertices;
        }

        public void RemoveVertice(int offset)
        {
            if( Length == 0 || offset >= Length )
            {
                return;
            }
            int[,] tmpVertices;
            tmpVertices = new int[--Length, 2];
            bool foundSeek = false;

            for (int i = 0; i < Length; i++)
            {
                if ( i == offset )
                {
                    foundSeek = true;
                }
                if (foundSeek)
                {
                    tmpVertices[i, 0] = Vertices[i + 1, 0];
                    tmpVertices[i, 1] = Vertices[i + 1, 1];
                }
                else
                {
                    tmpVertices[i, 0] = Vertices[i, 0];
                    tmpVertices[i, 1] = Vertices[i, 1];
                }
            }
            Vertices = tmpVertices;
        }
        public void RemoveVerticeAfter(int xSeek, int ySeek)
        {
            if( Length == 0 )
            {
                return;
            }
            int[,] tmpVertices;
            tmpVertices = new int[--Length, 2];
            bool foundSeek = false;

            for (int i = 0; i < Length; i++)
            {
                if ( Vertices[i,0] == xSeek && Vertices[i,1] == ySeek )
                {
                    foundSeek = true;
                }
                if (foundSeek)
                {
                    tmpVertices[i, 0] = Vertices[i + 1, 0];
                    tmpVertices[i, 1] = Vertices[i + 1, 1];
                }
                else
                {
                    tmpVertices[i, 0] = Vertices[i, 0];
                    tmpVertices[i, 1] = Vertices[i, 1];
                }
            }
            Vertices = tmpVertices;
        }
    }
}
