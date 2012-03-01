using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    [DefaultPropertyAttribute("FileName")]
    public class ImageProperties
    {
        /*private string Type;
        private string FileName;
        private int FileLength;
        private int BPP;
        private int Pixels;
        private int Width;
        private int Height;
        private int Frames;
        private int CurrentFrame;*/
        
        // Name property with category attribute and 
        // description attribute added 
        [DescriptionAttribute("FileName")]
        public string FileName
        {
            get
            {
                return FileName;
            } 
            set 
            {
                FileName = value;
            }
        } 
    
    }
}
