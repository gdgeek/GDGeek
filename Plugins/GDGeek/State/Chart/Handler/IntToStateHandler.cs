using System.Collections;
using System.Collections.Generic;
using GDGeek;
using GDGeek.StateChart;
using UnityEngine;

namespace GDGeek.StateChart
{
    public class IntToStateHandler : MessageHandler
    {
        [SerializeField]
        private LeafState[] _states;
        public override LeafState invoke(FSMEvent evt)
        {
            int msg = (int)(evt.obj);
            if(msg >=0 && msg < _states.Length) {
                return _states[msg];
            }

            return null;
        }


    }
}