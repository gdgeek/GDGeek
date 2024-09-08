using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek.StatechartDiagram { 
    public class State : MonoBehaviour {
        [SerializeField]
        private Method _start;
        [SerializeField]
        private Method _over;

        [SerializeField]
        private List<Transition> _transitions = new List<Transition>();

        [SerializeField]
        private List<State> _children = new List<State>(); 

        public string stateName {

            get {
                return this.gameObject.longName();
            }
        }
        public void load(StateData data)
        {
            this.name = data.name;
            _start = Factory.Instance.loadMethod(data.start);
            _start.name = "Start";
            _start.transform.SetParent(this.transform);
            _over = Factory.Instance.loadMethod(data.over);
            _over.transform.SetParent(this.transform);
            _start.name = "Over";

            foreach (StateData sd in data.children) {

            }

        }
        public StateData save()
        {
            StateData data = new StateData();
            data.name = this.name;
            data.start = Factory.Instance.saveMethod(_start);
            data.over = Factory.Instance.saveMethod(_over); //_over.serialize;
            foreach (State child in _children)
            {
                data.children.Add(child.save());
            }
            foreach (Transition transition in _transitions) {
                data.transitions.Add(transition.save());
            }
            return data;
        }

        internal void building(GDGeek.FSM fsm, string father = null)
        {
         
            GDGeek.State state = new GDGeek.State();
            state.onStart += delegate
            {
                if (this._start != null)
                {
                    _start.invoke();
                }
            };

            state.onOver += delegate
            {
                if (this._over != null)
                {
                    this._over.invoke();
                }
            };
           
            foreach (Transition transition in _transitions)
            {
                transition.building(state);
            }
            foreach (State child in _children) {
                child.building(fsm, this.stateName);
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