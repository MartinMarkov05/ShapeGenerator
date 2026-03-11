using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Graphics
{
   public struct DrawingColor
    {
        public byte A, R, G, B;
        
        public DrawingColor(byte a,byte r, byte g, byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

    }
}

