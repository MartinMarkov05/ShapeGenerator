using Library.Model.Contracts;
using Library.Model.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Model.Contracts
{
    interface IShape
    {
      void Move(PointF offset);
      void Edit(DrawingColor newColor);
      void Delete();
      void Draw(IDrawingUI g);
      double CalculateArea();
      double CalculatePerimeter();
       
    }
}
