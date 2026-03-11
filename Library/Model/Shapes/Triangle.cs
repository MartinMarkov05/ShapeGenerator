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
   public class Triangle : Shape
    {
        private PointF _pointA;
        private PointF _pointB;
        private PointF _pointC;
        private int _base;
        private int _height;
        [JsonProperty]
        public int Base
        {
            get => _base;
            set
            {
                _base = value;
                UpdatePoints();
            }
        }
        [JsonProperty]
        public int Height { get => _height;
            set {
                _height = value;
                UpdatePoints();
            } 
        }
        [JsonProperty]
        public PointF PointA
        {
            get => _pointA;
            set
            {
                _pointA = value;

            }
        }
        [JsonProperty]
        public PointF PointB
        {
            get => _pointB;
            set
            {
                _pointB = value;

            }
        }
        [JsonProperty]
        public PointF PointC
        {
            get => _pointC;
            set
            {
                _pointC = value;

            }
        }
        private void UpdatePoints()
        {
            PointA = new PointF(X, Y);
            PointB = new PointF(X - Base / 2, Y + Height);
            PointC = new PointF(X + Base / 2, Y + Height);
        }

        public Triangle(int baseLength, int height,DrawingColor color, string name , bool isFilled) : base(3, color, name, isFilled)
        {
            IsFilled = IsFilled;
            Name = name;
            Base = baseLength;
            Height = height;
            X = 150;
            Y = 100;
            UpdatePoints();
            Color = color;
        }
        public override double CalculateArea()
        {
         
            double area = 0.5 * Math.Abs(
                PointA.X * (PointB.Y - PointC.Y) +
                PointB.X * (PointC.Y - PointA.Y) +
                PointC.X * (PointA.Y - PointB.Y)
            );

            return area;
        }
        
        public override double CalculatePerimeter()
        {
            double a = CalculateSide(PointB, PointC);
            double b = CalculateSide(PointA, PointC);
            double c = CalculateSide(PointA, PointB);

            return a + b + c;
        }

        private double CalculateSide(PointF point1, PointF point2)
        {
            return Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
        }

        public override void Draw(IDrawingUI g)
        {
            float centerX = (PointA.X + PointB.X + PointC.X) / 3f;
            float centerY = (PointA.Y + PointB.Y + PointC.Y) / 3f;

            var state = g.Save();

            g.TranslateTransform(centerX, centerY);
            g.RotateTransform(Rotation);
            g.TranslateTransform(-centerX, -centerY);

            PointF[] points = { PointA, PointB, PointC };
            if (IsFilled)
            {
                
                
                    g.FillPolygon(Color, points);
                
            }

            DrawingPen pen = new DrawingPen(Color, 2);
                
            g.DrawPolygon(pen, points);
                

            g.Restore(state);
        }


        public override bool Contains(PointF p)
        {
            return p.X >= X && p.X <= X + Base && p.Y >= Y && p.Y <= Y + Height;
        }

        public override void Move(PointF offset)
        {
            base.Move(offset);
            UpdatePoints();
        }

        public override Shape Clone()
        {
            return new Triangle(Base, Height, Color, Name, IsFilled);
        }
    }


}
