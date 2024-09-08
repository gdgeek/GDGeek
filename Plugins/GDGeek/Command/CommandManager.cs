using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek { 
    /*
    public class CommandManager : Singleton<CommandManager>
    {
        public void execute(ICommand command) {

            Debug.LogError("EEE");
            command.todo();
            push(command);
        }
        public void push(ICommand command) {
            
            command.clock();
            _done.Push(command);
            _store.Clear();
        }
     
        public void redo()
        {
            if (_store.Count != 0)
            {
                ICommand command = _store.Pop();
                command.todo();
                _done.Push(command);

                if (_store.Count != 0)
                {
                    ICommand next = _store.Peek();
                    long d = System.Math.Abs(next.stamp.Ticks - command.stamp.Ticks);

                    if (d < 80000)
                    {
                        redo();
                    }
                }
            }
        }
        public void undo() {
            if (_done.Count != 0)
            {
                ICommand command = _done.Pop();
                command.undo();
                _store.Push(command);
                if (_done.Count != 0) {
                    ICommand next = _done.Peek();
                    long d = System.Math.Abs(next.stamp.Ticks - command.stamp.Ticks);
                    if (d < 80000) {
                        undo();
                    }
                }
            }
        }
        private Stack<ICommand> _done = new Stack<ICommand>();
        private Stack<ICommand> _store = new Stack<ICommand>();
    }*/
}