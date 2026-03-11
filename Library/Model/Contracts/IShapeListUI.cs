using Library.Model.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Contracts
{
    public interface IShapeListUI
    {
        void RemoveShape(Shape shape);
        void AddShape(Shape shape);
    }
}
