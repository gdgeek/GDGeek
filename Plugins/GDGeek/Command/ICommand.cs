using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek { 
    public interface ICommand
    {
        void todo();
        void undo();
        void clock();
        System.DateTime stamp { set; get; }
    }
    public abstract class Command:ICommand {
        public abstract void todo();
        public abstract void undo();
        public void clock() {
            stamp = System.DateTime.Now;
        }
        public System.DateTime stamp { set; get; }
    }
}