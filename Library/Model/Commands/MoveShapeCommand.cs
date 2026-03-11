using Library.Model.Graphics;
using Library.Model.Shapes;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Model.Contracts;

namespace WinFormsApp1.Model.Commands
{
    public class MoveShapeCommand : ICommand
    {
        private readonly Shape shape;
        private readonly PointF oldPosition;
        private readonly PointF newPosition;

        public MoveShapeCommand(Shape shape, PointF oldPosition, PointF newPosition)
        {
            this.shape = shape;
            this.oldPosition = oldPosition;
            this.newPosition = newPosition;
        }
        public void Execute()
        {
            shape.X = newPosition.X;
            shape.Y = newPosition.Y;
        }

        public void Undo()
        {
            shape.X = oldPosition.X;
            shape.Y = oldPosition.Y;
        }
    }
}
