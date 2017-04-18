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
namespace GDGeek{


	public class TaskState{
		//public 
		//private TaskDelegate.Factory _creater;
		protected static int index_ =  0; 
		//public delegate string NextState();

		static public State Create(TaskFactory creater, FSM fsm, State.StateAction nextState){
			string over = "over" + index_.ToString();

			index_++;
			State state = new State ();
			Task task = null;
			state.onStart += delegate {
				task = creater();
				TaskManager.PushBack (task, delegate {
					fsm.post(over);
				});
				TaskManager.Run (task);
			};
			state.onOver += delegate {
				task.isOver = delegate{
					return true;
				};
			};
			state.addAction (over, nextState);
			return state;
		}

		static public State Create( TaskFactory creater, FSM fsm, string nextState){
			return Create (creater, fsm, delegate {
				return nextState;
			});
		}

		
	}
}