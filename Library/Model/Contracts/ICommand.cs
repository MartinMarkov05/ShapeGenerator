using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Model.Contracts
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}
