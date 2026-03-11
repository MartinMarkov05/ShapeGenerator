using Library.Model.Contracts;
using Library.Model.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Adapters
{
    class DrawingAdapter : IDrawingUI
    {

        private Graphics g;

        public DrawingAdapter(Graphics g)
        {
            this.g = g;
        }

        private Pen ConvertPen(DrawingPen pen)
        {
            var color = Color.FromArgb(pen.Color.R, pen.Color.G, pen.Color.B);
            return new Pen(color, pen.Width);
        }
            

        public IDrawingState Save()
        {
            return new DrawingState(g.Save());
        }

        public void Restore(IDrawingState state)
        {
            if (state is DrawingState gState)
            {
                g.Restore(gState.GraphicsState);
            }
        }

        public void TranslateTransform(float x, float y)
        {
            g.TranslateTransform(x, y);
        }

        public void RotateTransform(float angle)
        {
            g.RotateTransform(angle);
        }

        public void FillRectangle(DrawingColor color, float x, float y, float width, float height)
        {
            using (Brush brush = new SolidBrush(Color.FromArgb(color.A,color.R, color.G, color.B)))
            {
                g.FillRectangle(brush, x, y, width, height);
            }
        }

        public void DrawRectangle(DrawingColor color, float thickness, float x, float y, float width, float height)
        {
            using (Pen pen = new Pen(Color.FromArgb(color.R, color.G, color.B), thickness))
            {
                g.DrawRectangle(pen, x, y, width, height);
            }
        }

        public void DrawPolygon(DrawingPen pen, Library.Model.Graphics.PointF[] points)
        {
            using (var sysPen = ConvertPen(pen))
            {
                var sysPoints = points.Select(p => new System.Drawing.PointF(p.X, p.Y)).ToArray();
                g.DrawPolygon(sysPen, sysPoints);
            }
        }

        public void FillPolygon(DrawingColor color, Library.Model.Graphics.PointF[] points)
        {
            using (var solidBrush = new SolidBrush(Color.FromArgb(color.R, color.G, color.B)))
            {
              
                var drawingPoints = points.Select(p => new System.Drawing.PointF(p.X, p.Y)).ToArray();

                g.FillPolygon(solidBrush, drawingPoints);
            }
        }

        public void DrawEllipse(DrawingColor color, float thickness, float x, float y, float width, float height)
        {
            using (var pen = new Pen(Color.FromArgb(color.R, color.G, color.B), thickness))
            {
                g.DrawEllipse(pen, x, y, width, height);
            }
        }

        public void FillEllipse(DrawingColor color, float x, float y, float width, float height)
        {
            using (var brush = new SolidBrush(Color.FromArgb(color.R, color.G, color.B)))
            {
                g.FillEllipse(brush,x,y,width,height);
            }
        }
    }
}
