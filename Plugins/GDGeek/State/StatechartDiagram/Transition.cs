

using System;
using UnityEngine;

namespace GDGeek.StatechartDiagram
{
    [System.Serializable]
    public class Transition
    {
        [SerializeField]
        private string _evt;
        [SerializeField]
        private Action _action;

        internal void building(GDGeek.State state)
        {
            state.addAction(_evt, delegate (FSMEvent evt)
            {


                if (_action != null) {
                    string json = (string)evt.obj;
                    string next = _action.execute(json);
                    return next;
                }
                return "";
              
            });

        }

        public TransitionData save()
        {

            TransitionData data = new TransitionData();
            data.evt = _evt;
            data.action = _action.serialize;
            return data;
        }
    }

}