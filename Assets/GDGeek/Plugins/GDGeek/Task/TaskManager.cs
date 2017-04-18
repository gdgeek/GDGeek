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

	public class TaskManager : Singleton<TaskManager> {

		//public TaskFactories _factories = null;
		public TaskRunner _runner = null;
		
		private TaskRunner partRunner_  = null;
		//private static TaskManager instance_ = null; 
		//private static Hashtable reserve_ = new Hashtable();
		
		public TaskRunner partRunner{
			set{this.partRunner_ = value as TaskRunner;}
		}
		
		void Awake(){

			//TaskManager.instance_ = this;
			if (_runner == null) {
				_runner = this.gameObject.GetComponent<TaskRunner>();			
			}
			if (_runner == null) {
				_runner = this.gameObject.AddComponent<TaskRunner>();	
			}
		}

		public static TaskManager GetInstance(){

			return Singleton<TaskManager>.Instance;
		}

		public TaskRunner globalRunner{
			get{return _runner;}
		}
		
		public TaskRunner runner{
			get{
					if(partRunner_ != null){
						return partRunner_;
					}
					return _runner;
				}

		}
		
		public static void AddIsOver(Task task, TaskIsOver func){
			TaskIsOver oIsOver = task.isOver;
			task.isOver = delegate(){
				return (oIsOver() || func());
			};
		}
		public static void AddUpdate(Task task, TaskUpdate func){
			TaskUpdate update = task.update;
			task.update = delegate(float d){
				update(d);
				func(d);
			};
		}

		public static void PushBack(Task task, TaskShutdown func){
			TaskShutdown oShutdown = task.shutdown;
			task.shutdown = delegate (){
				oShutdown();
				func();
			};
		}
	
		public static void Run(Task task){
			TaskManager.GetInstance().runner.addTask(task);
		}

		public static void PushFront(Task task, TaskInit func){
			TaskInit oInit = task.init;
			task.init = delegate(){
				func();
				oInit();
			};
		}
	}
}
