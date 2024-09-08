using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDGeek.StateChart
{
    public abstract class State : MonoBehaviour
    {
        

        public string stateName {

            get {

                string ret = this.name;
                Transform parent = this.transform.parent;
                while (parent != null) {
                    ret = parent.name +"." + ret;
                    parent = parent.parent;
                }
                return ret;
            }
        }

    

        [SerializeField]
        protected UnityEvent _onStart;
        [SerializeField]
        protected UnityEvent _onOver;

     

        public abstract void building(GDGeek.FSM fsm, string father = null);

    



    }
}
