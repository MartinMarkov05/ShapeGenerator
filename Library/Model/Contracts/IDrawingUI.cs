using Library.Model.Graphics;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Model;

namespace Library.Model.Contracts
{
    public interface IDrawingUI
    {
       

        void FillRectangle(DrawingColor color, float x, float y, float width, float height);
        void DrawRectangle(DrawingColor color, float thickness, float x, float y, float width, float height);
        void FillPolygon(DrawingColor color, PointF[] points);
        void DrawPolygon(DrawingPen pen, PointF[] points);
        void DrawEllipse(DrawingColor color, float thickness, float x, float y, float width, float height);
        void FillEllipse(DrawingColor color, float x, float y, float width, float height);
        IDrawingState Save();
        void Restore(IDrawingState state);

        void TranslateTransform(float x, float y);
        void RotateTransform(float angle);
    }
}
