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
        public const int TypeBrush = 0, TypeFont = 1, TypeRaster = 2, TypePixel = 3, TypePoly = 4, TypeArc = 5, TypeSpline = 6; 
        public int[,] Vertices;
        public int RasterWidth, RasterHeight;
        public Color[,] RasterData;
        public Color BrushColor;
        public Color OriginColor;
        public Color VerticeColor;
        public string Text;

        public PBMVectorData()
        {
            
        }

        public PBMVectorData(int[,] verticeArray)
        {
            BrushColor = Color.FromArgb(0xFF, 0xFF, 0xFF);
            OriginColor = Color.FromArgb(0xFF, 0x00, 0x00);
            VerticeColor = Color.FromArgb(0xA0, 0x00, 0xA0);
            Vertices = verticeArray;
        }

        public PBMVectorData(int iType, int[,] verticeArray)
        {
            BrushColor = Color.FromArgb(0xFF, 0xFF, 0xFF);
            OriginColor = Color.FromArgb(0xFF, 0x00, 0x00);
            VerticeColor = Color.FromArgb(0xA0, 0x00, 0xA0);
            Vertices = verticeArray;
            Type = iType;
        }

        public void AddVertice(int x, int y)
        {
            int[,] tmpVertices = new int[ (Vertices.Length / 2 ) + 1, 2];

            for( int i = 0; i < (tmpVertices.Length/2); i++ )
            {
                tmpVertices[i, 0] = Vertices[i, 0];
                tmpVertices[i, 1] = Vertices[i, 1];
            }
            tmpVertices[Vertices.Length / 2, 0] = x;
            tmpVertices[Vertices.Length / 2, 1] = y;
            Vertices = tmpVertices;
        }

        public void InsertVertice(int offset, int x, int y)
        {
            int[,] tmpVertices = new int[(Vertices.Length / 2) + 1, 2];

            for (int i = 0; i <(tmpVertices.Length/2); i++)
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

        public void InsertVerticeAfter(int x, int y, int xSeek, int ySeek,)
        {
            int[,] tmpVertices = new int[(Vertices.Length / 2) + 1, 2];
            bool foundSeek = false;

            for (int i = 0; i < (tmpVertices.Length/2); i++)
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
            if( Vertices.Length < 1 )
            {
                return;
            }
            int[,] tmpVertices = new int[(Vertices.Length / 2) - 1, 2];

            for( int i = 0; i < tmpVertices.Length/2; i++)
            {
                tmpVertices[i, 0] = Vertices[i, 0];
                tmpVertices[i, 1] = Vertices[i, 1];
            }
            Vertices = tmpVertices;
        }

        public void RemoveVertice(int offset)
        {
            if( Vertices.Length < 1 || offset >= Vertices.Length )
            {
                return;
            }
            int[,] tmpVertices = new int[(Vertices.Length / 2) - 1, 2];
            bool foundSeek = false;

            for (int i = 0; i < tmpVertices.Length/2; i++)
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
            if( Vertices.Length < 1 )
            {
                return;
            }
            int[,] tmpVertices = new int[(Vertices.Length / 2) - 1, 2];
            bool foundSeek = false;

            for (int i = 0; i < tmpVertices.Length/2; i++)
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
