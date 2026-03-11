using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Graphics
{
    public class DrawingPen
    {

        public DrawingColor Color { get; set; }
        public float Width { get; set; } = 1;

        public DrawingPen(DrawingColor Color, float Width)
        {
            this.Color = Color;
            this.Width = Width;
        }
    }
}
