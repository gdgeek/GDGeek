﻿/*-----------------------------------------------------------------------------
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
	public class TaskSet : Task {

		public new static TaskSet T => new TaskSet();
		private List<Task> list_ = new List<Task>();
		private int overCount_ = 0;
		//private TaskRunner runner_ = null;
		public TaskSet(TaskRunner runner = null){ 
			//runner_ = runner;
			/*this.cancel = delegate(){
				for(int i = 0; i < this.tasks_.Count; ++i){
					this.tasks_[i].cancel();
				}
			};*/
			this.init = delegate() {
				overCount_ = 0;
				for(int i = 0; i < this.list_.Count; ++i){
					Task task = this.list_[i] as Task;
					task.run();
				}
			}; 
			this.isOver = delegate(){
				return (this.overCount_ == this.list_.Count);
			};
			
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
		} 
*/
		public TaskSet push(Task task){
		//	task.runner = this.runner;
			this.list_.Add (task);
			TaskManager.PushBack(task, delegate(){overCount_++;});
			return this;
		}
		public void clear()
		{
			this.list_.Clear();
		}
	}
}