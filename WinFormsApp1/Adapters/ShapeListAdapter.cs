using Library.Model.Contracts;
using Library.Model.Shapes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Adapters
{
    public class ShapeListAdapter : IShapeListUI
    {
        private readonly ListBox listBox;
        private readonly BindingList<Shape> shapes;

        public ShapeListAdapter(ListBox listBox)
        {
            this.listBox = listBox;
            this.shapes = new BindingList<Shape>();
            this.listBox.DataSource = shapes;

        }

        public void RemoveShape(Shape shape) => shapes.Remove(shape);
        public void AddShape(Shape shape) => shapes.Add(shape);
    }

}
