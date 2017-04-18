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
	public class TaskWait : Task {

		private float allTime_ = 0f;
		private float time_ = 0f;
		public TaskWait(){
			this.init = initImpl;
			this.update = updateImpl;
			this.isOver = isOverImpl;
		}

		
		public TaskWait(float time){
			this.init = initImpl;
			this.update = updateImpl;
			this.isOver = isOverImpl;
			this.setAllTime (time);
		}
		public void forceQuit(){
			time_ = allTime_;
		}
		public static TaskWait Create(float time, TaskShutdown shutdown){
			TaskWait wait = new TaskWait (time);
			TaskManager.PushBack (wait, shutdown);
			return wait;
		}

		public void setAllTime(float allTime){
			allTime_ = allTime;
		}

		public void initImpl(){
			time_ = 0f;
		}

		public void updateImpl(float d){
			time_ += d;
		}

		public bool isOverImpl(){
			if (time_ >= allTime_) {
				return true;		
			}
			return false;
		}

	}
}
