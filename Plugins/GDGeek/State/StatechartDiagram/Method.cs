using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek.StatechartDiagram {
    public abstract class Method : MonoBehaviour {

        public abstract void invoke();
        public abstract string serialize { get; set; }
    }
}