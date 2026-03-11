using Library.Model.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Model.Contracts;

namespace WinFormsApp1.Model.Commands
{
    public class EditCommand : ICommand
    {
        private Shape selectedShape;
        private Shape newShape;
        private Shape oldShape;

       

        public EditCommand(Shape selectedShape, Shape newShape)
        {
            this.selectedShape = selectedShape;
            oldShape = selectedShape.Clone();
            this.newShape = newShape;
        }

      
        public void Execute()
        {
            ApplyShapeProperties(selectedShape, newShape);

        }
        public void Undo()
        {
            ApplyShapeProperties(selectedShape, oldShape);
        }

        private void ApplyShapeProperties(Shape target, Shape source)
        {
                target.Name = source.Name;
                target.Color = source.Color;
                target.IsFilled = source.IsFilled;
                target.Rotation = source.Rotation;

            if (target is Circle targetCircle && source is Circle sourceCircle)
            {
                targetCircle.Radius = sourceCircle.Radius;
            }
            else if (target is Rectangle targetRectangle && source is Rectangle sourceRectangle)
            {
                targetRectangle.Width = sourceRectangle.Width;
                targetRectangle.Height = sourceRectangle.Height;
            }
            else if (target is Triangle targerTriangle && source is Triangle sourceTriangle)
            {
                targerTriangle.Height = sourceTriangle.Height;
                targerTriangle.Base = sourceTriangle.Base;
            }
        }

      
    }
}
