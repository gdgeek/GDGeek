
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace GDGeek.StatechartDiagram
{

    public abstract class  Action : MonoBehaviour
    {

        public abstract string serialize { get; set; }
        public abstract string execute(string json = null);
     
    }
}
