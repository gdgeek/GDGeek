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
	public class TaskRunner : MonoBehaviour {
		//public int _lots = 1;
		private Filter filter_ = new Filter();
		private List<Task> tasks_ = new List<Task>();
		private List<Task> shutdown_ = new List<Task>();

		public static TaskRunner Create(GameObject obj){
			TaskRunner runner = obj.GetComponent<TaskRunner>();
			if(runner == null){
				runner = obj.AddComponent<TaskRunner>();
			}
			return runner;
		}


		public void update(float d){
			
			var tasks = new List<Task>();
			for (int i = 0; i < this.shutdown_.Count; ++i) {
				this.shutdown_ [i].shutdown ();
			}
			this.shutdown_.Clear ();
			for(var i=0; i< this.tasks_.Count; ++i){
				Task task = this.tasks_[i] as Task;
				task.update(d);
				if(!task.isOver()){
					tasks.Add(task);
				}else{
					shutdown_.Add (task);
				}
			}
			this.tasks_ = tasks;
		}

		

		public void addTask(Task task){
			task.init();
			this.tasks_.Add(task);
		}
		
		protected virtual void Update() { 
			float d = filter_.interval(Time.deltaTime);
			//float lots = (float)_lots;
			//for (int i = 0; i < _lots; ++i) {
				this.update (d);
			//}
		}
	}
}
