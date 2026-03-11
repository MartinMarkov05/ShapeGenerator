using Library.Model.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Controller;
using ICommand = WinFormsApp1.Model.Contracts.ICommand;

namespace WinFormsApp1.Model.Commands
{

    public class RotateShapeCommand : ICommand
    {
        private Shape shape;
        private float previousAngle;
        private float newAngle;

        public RotateShapeCommand(Shape shape, float angle)
        {
            this.shape = shape;
            this.previousAngle = shape.Rotation; 
            this.newAngle = angle;
        }

        public void Execute()
        {
            shape.Rotation = newAngle;
        }

        public void Undo()
        {
            shape.Rotation = previousAngle;
        }
    }
}
