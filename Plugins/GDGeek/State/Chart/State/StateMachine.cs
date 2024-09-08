using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek.StateChart { 
    public class StateMachine : MonoBehaviour {
        [SerializeField]
        private State[] _states;
        [SerializeField]
        private LeafState _begin;
#if UNITY_EDITOR
        [SerializeField]
        private string _stateName;
        public void Update()
        {
            _stateName = fsm_.getCurrSubState().name;
        }
#endif
        private FSM fsm_ = new FSM();

        public FSM fsm {

            get {

                return fsm_;
            }
        }
        public void init()
        {
            if (_begin != null) { 
                fsm_.init(_begin.stateName);
            }
        }


        public FSM create() {
            fsm_ = new FSM();
            foreach (State state in _states) {
                state.building(fsm_);
            }
            return fsm_;
        }
	
     

    }
}