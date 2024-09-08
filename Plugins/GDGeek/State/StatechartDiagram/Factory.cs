using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek.StatechartDiagram { 
    public abstract class Factory : GDGeek.Singleton<Factory>
    {
        public abstract Action loadAction(string serialize);
        public abstract Method loadMethod(string serialize);
        public abstract string saveAction(Action action);
        public abstract string saveMethod(Method method);
    }
}