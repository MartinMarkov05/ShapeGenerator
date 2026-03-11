using Library.Model.Contracts;
using Library.Model.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Shapes
{
    public class Circle : Shape
    {
      
        private int radius;
        public Circle(int radius, DrawingColor color , string name, bool isFilled) : base(1, color, name,isFilled)  
        {
            IsFilled = isFilled;
            Name = name;
            this.radius = radius;
            Color = color;
            X = Radius; 
            Y = Radius;
        }
        [JsonProperty]
        public int Radius
        {
            get { return radius; } 
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Radius must be greater than zero.");
                }
                radius = value;  
            }
        }


        public override double CalculateArea()
        {
            return Math.PI * Math.Pow(Radius, 2);
        }

        public override double CalculatePerimeter()
        {
            return 2 * Math.PI * Radius;
        }

        public override void Draw(IDrawingUI g)
        {
            if (IsFilled)
            {
                
                
                    g.FillEllipse(Color, (float)(X - Radius), (float)(Y - Radius), Radius * 2, Radius * 2);
                
            }
           

            
            
                g.DrawEllipse(Color, 2, (float)(X - Radius), (float)(Y - Radius), Radius * 2, Radius * 2);
            
        }

        public override void Move(PointF offset)
        {
            base.Move(offset);
        }

        public override bool Contains(PointF p)
        {
            double distance = Math.Sqrt(Math.Pow(p.X - X, 2) + Math.Pow(p.Y - Y, 2));
            return distance <= Radius;
        }

        public override Shape Clone()
        {
            return new Circle(radius, Color, Name, IsFilled);
        }

    }
}
