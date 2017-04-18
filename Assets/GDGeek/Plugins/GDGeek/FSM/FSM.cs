/*-----------------------------------------------------------------------------
The MIT License (MIT)

This source file is part of GDGeek
    (Game Develop & Game Engine Extendable Kits)
For the latest info, see http://gdgeek.com/

Copyright (c) 2014-2015 GDGeek Software Ltd

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
-----------------------------------------------------------------------------
*/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace GDGeek
{
    public class FSM
    {

        private Dictionary<string, StateBase> states_ = new Dictionary<string, StateBase>();
        private List<StateBase> currState_ = new List<StateBase>();
        private bool debug_ = false;
        public string _name;
        public FSM(bool debug = false)
        {
            debug_ = debug;
            StateBase root = new StateBase();
            root.name = "root";
            this.states_["root"] = root;
            this.currState_.Add(root);
        }

        public FSM(string name, bool debug = false)
        {
            this._name = name;
            debug_ = debug;
            StateBase root = new StateBase();
            root.name = "root";
            this.states_["root"] = root;
            this.currState_.Add(root);
        }


        public void addState(StateBase state)
        {

            this.addState(state.name, (StateBase)(state));
        }

        public void addState(string stateName, StateBase state)
        {

            this.addState(stateName, state, "");
        }


        public void addState(StateBase state, string fatherName)
        {

            this.addState(state.name, (StateBase)(state), fatherName);
        }
        /*
		public void addState(string defSubState, State state){
			this.addState (state.stateName, (StateBase)(state), "");		
			state.defSubState = defSubState;
		}
		public void addState(string stateName, string defSubState, StateBase state){
			this.addState (stateName, state, "");		
			state.defSubState = defSubState;
		}
		public void addState(string stateName, string defSubState, StateBase state, string fatherName){
			this.addState (stateName, state, fatherName);		
			state.defSubState = defSubState;
		}
*/
        public void addState(string stateName, StateBase state, string fatherName)
        {

            if (fatherName == "")
            {
                state.fatherName = "root";
            }
            else
            {
                state.fatherName = fatherName;
            }
            state.getCurrState = delegate (string name)
            {
                for (int i = 0; i < this.currState_.Count; ++i)
                {
                    StateBase s = this.currState_[i] as StateBase;
                    if (s.name == name)
                    {
                        return s;
                    }
                }
                return null;
            };
            if (this.states_.ContainsKey(state.fatherName))
            {
                if (string.IsNullOrEmpty(this.states_[state.fatherName].defSubState))
                {
                    this.states_[state.fatherName].defSubState = stateName;
                }
            }
            state.name = stateName;
            this.states_[stateName] = state;

        }


        public void translation(string name)
        {


            if (!this.states_.ContainsKey(name))//if no target return!
            {
                return;
            }

            StateBase target = this.states_[name];//target state
            while (!string.IsNullOrEmpty(target.defSubState) && this.states_.ContainsKey(target.defSubState))
            {
                target = this.states_[target.defSubState];
            }

            //if current, reset
            if (target == this.currState_[this.currState_.Count - 1])
            {
                target.over();
                target.start();
                return;
            }



            StateBase publicState = null;

            List<StateBase> stateList = new List<StateBase>();

            StateBase tempState = target;
            string fatherName = tempState.fatherName;

            //do loop 
            while (tempState != null)
            {
                //reiterator current list
                for (var i = this.currState_.Count - 1; i >= 0; i--)
                {
                    StateBase state = this.currState_[i] as StateBase;
                    //if has public 
                    if (state == tempState)
                    {
                        publicState = state;
                        break;
                    }
                }

                //end
                if (publicState != null)
                {
                    break;
                }

                //else push state_list
                stateList.Insert(0, tempState);
                //state_list.unshift(temp_state);

                if (fatherName != "")
                {
                    tempState = this.states_[fatherName] as StateBase;
                    fatherName = tempState.fatherName;
                }
                else
                {
                    tempState = null;
                }

            }
            //if no public return
            if (publicState == null)
            {
                return;
            }

            List<StateBase> newCurrState = new List<StateBase>();
            bool under = true;
            //-- 析构状态
            for (int i2 = this.currState_.Count - 1; i2 >= 0; --i2)
            {
                StateBase state2 = this.currState_[i2] as StateBase;
                if (state2 == publicState)
                {
                    under = false;
                }
                if (under)
                {
                    state2.over();
                }
                else
                {
                    newCurrState.Insert(0, state2);
                }

            }


            //-- 构建状态
            for (int i3 = 0; i3 < stateList.Count; ++i3)
            {
                StateBase state3 = stateList[i3] as StateBase;
                state3.start();

                newCurrState.Add(state3);
            }

            this.currState_ = newCurrState;
            string outs = "";
            for (int i = 0; i < currState_.Count; ++i)
            {
                outs += ":" + currState_[i].name;
            }

            //			Debug.LogWarning (outs);

        }



        public StateBase getCurrSubState()
        {
            var self = this;
            return self.currState_[self.currState_.Count - 1];

        }

        public StateBase getCurrState(string name)
        {
            var self = this;
            for (var i = 0; i < self.currState_.Count; ++i)
            {
                StateBase state = self.currState_[i] as StateBase;
                if (state.name == name)
                {
                    return state;
                }
            }

            return null;

        }

        public void init(string state_name)
        {
            var self = this;

            self.translation(state_name);
        }


        public void shutdown()
        {
            for (int i = this.currState_.Count - 1; i >= 0; --i)
            {
                StateBase state = this.currState_[i] as StateBase;
                state.over();
            }
            this.currState_ = null;
        }

        public void post(string msg)
        {
            FSMEvent evt = new FSMEvent();

            evt.msg = msg;
            this.postEvent(evt);
        }

        public void post(string msg, object obj)
        {

            this.postEvent(new FSMEvent(msg, obj));
        }
        public void postEvent(FSMEvent evt)
        {

            string outs = "";
            for (int i = 0; i < currState_.Count; ++i)
            {
                outs += ":" + currState_[i].name;
            }

            //			Debug.LogWarning (outs);


            for (int i = 0; i < this.currState_.Count; ++i)
            {
                StateBase state = this.currState_[i] as StateBase;
                if (debug_)
                {
                    Debug.Log("msg_post" + evt.msg + state.name);
                }
                string stateName = state.postEvent(evt) as string;
                if (stateName != "")
                {
                    this.translation(stateName);
                    break;
                }
            }
        }
    }
}
