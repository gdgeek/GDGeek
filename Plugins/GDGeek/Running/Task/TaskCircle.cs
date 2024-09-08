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
namespace GDGeek{
	
	public class TaskCircle:Task{
		public new static TaskCircle T => new TaskCircle();
		private List<Task> list_ = new List<Task>(); 
		private bool isOver_ = false;
		private bool forceExit_ = false;
		private int index_ = 0;
		private int times_ = 0;
		public void forceQuit(){
			forceExit_ = true;
		}
		public TaskCircle(int times){
			this.init = this.initImpl;
			this.isOver = this.isOverImpl;
			this.times_ = times;
		}
		private TaskRunner runner_ = null;
		public TaskCircle(TaskRunner runner = null){
			runner_ = runner;
			this.init = this.initImpl;
			this.isOver = this.isOverImpl;
			
			//this.cancel = this.cancelImpl;
		}
		/*
		 void cancelImpl(){
			for(int i = 0; i < other_.Count; ++i){
				other_[i].cancel();
			}
		}
		*/
		public TaskCircle push(Task task){
			//task.runner = this.runner;
			TaskManager.PushBack (task, delegate {
				index_++;
				if (times_ > 0 && index_ / list_.Count >= times_)
				{
					forceExit_ = true;
				}

				runTask();
			});
			list_.Add(task);
			return this;
		}
		/*
		public override TaskRunner runner { get => base.runner;
			set
			{
				base.runner = this.runner;
				for(int i = 0; i < this.list_.Count; ++i){
					this.list_[i].runner = this.runner;
				}
			}
		} */
		void runTask ()
		{
			if(forceExit_){
				isOver_ = true;
			}else{
				Task task = list_ [index_ % list_.Count];
				task.run();
			}
		}
		
		public void initImpl(){
			this.isOver_ = false;
			forceExit_ = false;
			index_ = 0;
			runTask ();
		}
		
		
		public bool isOverImpl(){
			return this.isOver_;
		}
		public void clear()
		{
			this.list_.Clear();
		}
	};
}
