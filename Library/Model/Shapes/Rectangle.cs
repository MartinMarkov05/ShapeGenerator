using Library.Model.Contracts;
using Library.Model.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Shapes
{
     public class Rectangle : Shape
    {
        [JsonProperty]
        public int Width { get; set; }
        [JsonProperty]
        public int Height { get; set; }



        public Rectangle(int width, int height,DrawingColor color, string name, bool isFilled) : base(1, color, name, isFilled)
        {
            Width = width;
            Height = height;
            Color = color;
            IsFilled = isFilled;
            Name = name;
        }





        public override void Draw(IDrawingUI g)
        {
            var state = g.Save();

            float centerX = X + Width / 2f;
            float centerY = Y + Height / 2f;

            g.TranslateTransform(centerX, centerY);
            g.RotateTransform(Rotation);
            g.TranslateTransform(-centerX, -centerY);

           
            if (IsFilled)
            {
                
                
                    g.FillRectangle(Color, X, Y, Width, Height);
                
            }

        
            
                g.DrawRectangle(Color, 2f, X, Y, Width, Height);
            
            
            g.Restore(state);
        }



        public override double CalculateArea()
        {
           return Width * Height;
        }

        public override double CalculatePerimeter()
        {
            return 2 * (Width + Height);
        }

        public override void Move(PointF offset)
        {
            base.Move(offset);
        }

        public override bool Contains(PointF p)
        {
            return p.X >= X && p.X <= X + Width && p.Y >= Y && p.Y <= Y + Height;
        }

        public override Shape Clone()
        {
            return new Rectangle(Width, Height,Color, Name, IsFilled);
        }

    }
}
