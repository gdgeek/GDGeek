using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek.StateChart
{
    public class SampleState : LeafState
    {



        [SerializeField]
        private EventCondition[] _conditions;

        public override void building(FSM fsm, string father = null)
        {
            GDGeek.State state = new GDGeek.State();
            state.onStart += delegate
            {
                Debug.Log("State:" + this.stateName);
                if (this._onStart != null)
                {

                    this._onStart.Invoke();
                }
            };


            state.onOver += delegate
            {
                if (this._onOver != null)
                {
                    this._onOver.Invoke();
                }
            };
            foreach (EventCondition condition in _conditions)
            {
                condition.building(state);
            }
            if (string.IsNullOrEmpty(father))
            {
                fsm.addState(this.stateName, state);
            }
            else
            {

                fsm.addState(this.stateName, state, father);
            }

          
        }
    }
}