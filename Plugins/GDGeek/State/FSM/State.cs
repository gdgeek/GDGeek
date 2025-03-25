/*-----------------------------------------------------------------------------
The MIT License (MIT)

This source file is part of GDGeek
    (Game Develop & Game Engine Extendable Kits)
For the latest info, see http://gdgeek.com/

Copyright (c) 2014-2021 GDGeek Software Ltd

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
using System;

namespace GDGeek{
		

	public class State : StateBase {

		private Dictionary<string, Func<FSMEvent, string>> actionMap_ = new Dictionary<string, Func<FSMEvent, string>>();
	
		public Action onOver { set;  get; }
		public Action onStart { set;  get; }
		public State addStart(Action act)
		{
			onStart += act;
			return this;
		}

		public State addOver(Action act)
		{
			onOver += act;
			return this;
		}

		//public State(string name) => this.name = name;
		
		public State(){}


		public State addAction(string evt, string nextState)
		{
			return addAction (evt, (FSMEvent)=>nextState);
		}
	


		public State addAction(string evt, Func<FSMEvent, string> action){
			
			
			if (!actionMap_.ContainsKey (evt)) {
				actionMap_.Add (evt, action);		
			} else {

				Func<FSMEvent, string> old = actionMap_[evt];
				actionMap_[evt] = (FSMEvent e) =>{
					string ret = null;
					ret = old(e);
					if(!string.IsNullOrEmpty(ret)){
						return ret;
					}
					return action(e);
				};
			}
			return this;
		}
		public void addAction(string evt, Action<FSMEvent> action){
			
			this.addAction (evt, (FSMEvent e) =>{
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
			=> onStart?.Invoke();

		public override void over ()
			=> onOver?.Invoke();
		
	}
}