using Library.Model.Contracts;
using Library.Model.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Model.Contracts;

namespace Library.Model.Shapes
{
    public abstract class Shape : IShape
    {
        [JsonProperty]
        public  string  Name { get; set; }
        [JsonProperty]
        public DrawingColor Color { get; set; }
        [JsonProperty]
        public float X { get; set; }
        [JsonProperty]
        public float Y { get;  set; }
        [JsonProperty]
        public  float Rotation { get; set; } = 0f;
        [JsonProperty]
        public bool IsFilled { get; set; }


        protected Shape(int numberOfPoints, DrawingColor color, string name, bool isFilled) 
        {
            IsFilled = isFilled;
            Name = name;
            X = 10;
            Y = 10;
        }

        
        public delegate void ShapeEventHandler(object sender, EventArgs e);
        public event ShapeEventHandler OnEdit;
        public event ShapeEventHandler OnDelete;

        public virtual bool Contains(PointF p)
        {
            return false; 
        }


        public virtual void Move(PointF offset)
        {
            X += offset.X; 
            Y += offset.Y;
        }

        public virtual void Edit(DrawingColor newColor)
        {
            Color = newColor;
            OnEdit?.Invoke(this, EventArgs.Empty);
        }

        public void Delete()
        {
            OnDelete?.Invoke(this, EventArgs.Empty);
        }

        public override string ToString()
        {
            return Name; 
        }
        public abstract void Draw(IDrawingUI g);
        public abstract double CalculateArea();
        public abstract double CalculatePerimeter();

        public abstract Shape Clone();

    }
}
