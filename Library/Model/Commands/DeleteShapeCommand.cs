using Library.Model.Contracts;
using Library.Model.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Controller;
using WinFormsApp1.Model.Contracts;
using ICommand = WinFormsApp1.Model.Contracts.ICommand;

namespace WinFormsApp1.Model.Commands
{
    public class DeleteShapeCommand : ICommand
    {
        private Scene scene;
        private Shape shape;
        private readonly IShapeListUI shapeList;

        public DeleteShapeCommand(Scene scene, Shape shape, IShapeListUI shapeList)
        {
            this.scene = scene;
            this.shape = shape;
            this.shapeList = shapeList;
           
        }

        public void Execute()
        {
            scene.Delete(shape);
            shapeList.RemoveShape(shape);
        }

        public void Undo()
        {
            scene.AddShape(shape);
            shapeList.AddShape( shape);
        }
    }
}
