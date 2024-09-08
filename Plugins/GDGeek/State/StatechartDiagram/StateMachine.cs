using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek.StatechartDiagram
{

    public class StateMachineData {
        public List<StateData> children = new List<StateData>();
    }
    public class StateData
    {
        public string name;
        public string start;
        public string over;
        public List<StateData> children = new List<StateData>();
        public List<TransitionData> transitions = new List<TransitionData>();
    }

    public class TransitionData{
        public string evt;
        public string action;
    }


    public class StateMachine : MonoBehaviour
    {

        [SerializeField]
        private List<State> _states = new List<State>();
        public StateMachineData save() {
            StateMachineData data = new StateMachineData();
            foreach (State state in _states) {
                data.children.Add(state.save());
            }
            return data;
        }
        public void load(StateMachineData data) {
            foreach (StateData stateData in data.children) {
                _states.Add(this.createState(stateData));
            }
        }

        private State createState(StateData data)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(this.transform);
            State state = obj.AddComponent<State>();
            state.load(data);
            return state;
        }

        public void building(FSM fsm) {
            foreach (State state in _states)
            {
                state.building(fsm);
            }
            fsm.init(_states[0].gameObject.longName());
            //return fsm;

        }
        
      
    }
}
