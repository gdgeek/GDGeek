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

namespace GDGeek{
		

	public class State : StateBase {
		public delegate void Action();
		public delegate void EvtAction(FSMEvent evt);
		public delegate string StateAction(FSMEvent evt);

		private Dictionary<string, StateAction> actionMap_ = new Dictionary<string,StateAction>();
	
		public event Action onOver;
		public event Action onStart;
		public State(string name){
			this.name = name;
		}
		public State(){
		}
		public void addAction(string evt, string nextState){
			addAction (evt, delegate {
							return nextState;
					});
		}

		//upate
		//fsm.pause();
		//onPause();
		//.play()
		//fsm -> name;


		// {state:}
		/*public State s = new State("", "");
		void Enter_Play(){
			
		}
		void Levase_Play(){
			
		}*/
		public void addAction(string evt, StateAction action){
			if (!actionMap_.ContainsKey (evt)) {
				actionMap_.Add (evt, action);		
			} else {
				StateAction old = actionMap_[evt];
				actionMap_[evt] = delegate(FSMEvent e) {
					string ret = null;
					ret = old(e);
					if(!string.IsNullOrEmpty(ret)){
						return ret;
					}
					return action(e);

				};
			}
		}
		public void addAction(string evt, EvtAction action){
			this.addAction (evt, delegate(FSMEvent e) {
				action(e);
				return "";
			});
		}
	
		public override string postEvent(FSMEvent evt){
		
			string ret = "";
			if(actionMap_.ContainsKey(evt.msg)){
				ret = actionMap_[evt.msg](evt);
			}
			return ret;			

		}
		public override void start ()
		{
			if(onStart != null)
				onStart ();
		}
		public override void over ()
		{
			if(onOver != null)
				onOver ();
		}
	}
}