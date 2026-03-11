using Library.Model.Contracts;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Adapters
{
    class DrawingState : IDrawingState
    {
        public GraphicsState GraphicsState { get; }

        public DrawingState(GraphicsState graphicsState)
        {
            GraphicsState = graphicsState;
        }
    }
}
