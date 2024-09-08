using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek.StateChart
{
    public class StoryState : LeafState
    {

        [SerializeField]
        public Story _story;
        [SerializeField]
        public LeafState _next;

        public override void building(FSM fsm, string father = null)
        {
            GDGeek.State state = TaskState.Create(delegate
            {
                return _story.task;
            }, fsm, _next.stateName);
            
            state.onStart += delegate
            {
                if (this._onStart != null)
                {
                    this._onStart.Invoke();
                }
            };


            state.onOver += delegate
            {
                if (this._onOver != null)
                {
                    this._onStart.Invoke();
                }
            };

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