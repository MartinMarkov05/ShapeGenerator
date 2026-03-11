using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Model.Contracts;

namespace WinFormsApp1.Model.Commands
{
    public class CommandManager
    {
        private Stack<ICommand> undoStack = new Stack<ICommand>();
        private Stack<ICommand> redoStack = new Stack<ICommand>();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            undoStack.Push(command);
            redoStack.Clear(); 
        }

        public void Undo()
        {
            if (undoStack.Any())
            {
                var command = undoStack.Pop();
                command.Undo();
                redoStack.Push(command);
            }
        }

        public void Redo()
        {
            if (redoStack.Any())
            {
                var command = redoStack.Pop();
                command.Execute();
                undoStack.Push(command);
            }
        }
    }
}
