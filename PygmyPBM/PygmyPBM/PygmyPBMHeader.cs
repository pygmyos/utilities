using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class PygmyPBMHeader
    {
        public const int PYGMY_PBM_TYPEMASK = 0xF000;
        public const int PYGMY_PBM_IMAGE = 0x0000;
        public const int PYGMY_PBM_IMAGESTRIP = 0x1000;
        public const int PYGMY_PBM_ANIMATION = 0x2000;
        public const int PYGMY_PBM_VIDEO = 0x3000;
        public const int PYGMY_PBM_AUDIO = 0x4000;
        public const int PYGMY_PBM_BINARY = 0x5000;
        public const int PYGMY_PBM_TEXT = 0x6000;
        public const int PYGMY_PBM_EXECUTABLE = 0x7000;
        public const int PYGMY_PBM_BYTECODE = 0x8000;
        public const int PYGMY_PBM_LIBRARY = 0x9000;
        public const int PYGMY_PBM_GUI = 0xA000;
        public const int PYGMY_PBM_SCRIPT = 0xB000;
        public const int PYGMY_PBM_DRIVER = 0xC000;
        public const int PYGMY_PBM_ARCHIVE = 0xD000;
        public const int PYGMY_PBM_STREAM = 0xE000;
        public const int PYGMY_PBM_FONT = 0xF000;

        public const int PYGMY_PBM_TABLEMASK = 0x0300;
        public const int PYGMY_PBM_TABLE16 = 0x0000;
        public const int PYGMY_PBM_TABLE32 = 0x0400;
        public const int PYGMY_PBM_DIMENSIONSIZEMASK = 0x0100;
        public const int PYGMY_PBM_8BITD = 0x0000;
        public const int PYGMY_PBM_16BITD = 0x0100;
        public const int PYGMY_PBM_DRAWSTYLEMASK = 0x00C0;
        public const int PYGMY_PBM_RAW = 0x0000;
        public const int PYGMY_PBM_ALPHA = 0x0040;
        public const int PYGMY_PBM_SCATTER = 0x0080;
        public const int PYGMY_PBM_VECTOR = 0x00C0;
        public const int PYGMY_PBM_CRCMASK = 0x0020;
        public const int PYGMY_PBM_CRC = 0x0020;
        public const int PYGMY_PBM_VERSIONMASK = 0x0010;
        public const int PYGMY_PBM_VERSION = 0x0010;
        public const int PYGMY_PBM_BPPMASK = 0x000F;
        public const int PYGMY_PBM_1BPP = 0x0000;
        public const int PYGMY_PBM_4BPP = 0x0001;
        public const int PYGMY_PBM_8BPP = 0x0002;
        public const int PYGMY_PBM_12BPP = 0x0003;
        public const int PYGMY_PBM_16BPP = 0x0004;
        public const int PYGMY_PBM_24BPP = 0x0005;
        public const int PYGMY_PBM_32BPP = 0x0006;
        public const int PYGMY_PACKET_RLE = 0x80;
        public const int PYGMY_PACKET_SET = 0x40;
        public const int PYGMY_PBM_PIXELON = 0x40;
        public const int PYGMY_PBM_RLE = 0x80;

        public const int PYGMY_VECTOR_MASK = 0xE0;
        public const int PYGMY_VECTOR_END = 0x00;
        public const int PYGMY_VECTOR_BRUSH = 0x20;
        public const int PYGMY_VECTOR_POLY = 0x40;
        public const int PYGMY_VECTOR_ARC = 0x60;
        public const int PYGMY_VECTOR_SPLINE = 0x80;
        public const int PYGMY_VECTOR_TEXT = 0xA0;
        public const int PYGMY_VECTOR_RASTER = 0xC0;

        public int ConvertBPP(int BPP)
        {
            if (BPP == 1)
            {
                return( PygmyPBMHeader.PYGMY_PBM_1BPP );
            }
            else if (BPP == 4)
            {
                return( PygmyPBMHeader.PYGMY_PBM_4BPP );
            }
            else if (BPP == 8)
            {
                return( PygmyPBMHeader.PYGMY_PBM_8BPP );
            }
            else if (BPP == 12)
            {
                return( PygmyPBMHeader.PYGMY_PBM_12BPP );
            }
            else if (BPP == 16)
            {
                return( PygmyPBMHeader.PYGMY_PBM_16BPP );
            }
            else if (BPP == 24)
            {
                return( PygmyPBMHeader.PYGMY_PBM_24BPP );
            }
            else if (BPP == 32)
            {
                return (PygmyPBMHeader.PYGMY_PBM_32BPP);
            }
            else
            {
                return (0);
            }
            
        }

        public int ExtractVerticeCount(int PacketHeader)
        {
            return (PacketHeader & ~PYGMY_VECTOR_MASK);
        }

        public int ExtractVectorPacketType(int PacketHeader)
        {
            return (PacketHeader & PYGMY_VECTOR_MASK);
        }
    }
}
