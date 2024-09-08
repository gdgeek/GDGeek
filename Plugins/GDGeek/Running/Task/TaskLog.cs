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
using GDGeek;
using System.Collections.Generic;
using System;


namespace GDGeek{
	public class TaskLog : MonoBehaviour {

		public static TaskLog _instance = null;
		private Dictionary<string, long> dict_ = new Dictionary<string, long>();
		public void OnDestroy(){
				long all = 0;
			foreach (var kv in dict_) {
				all += kv.Value;
			}
			float a = (float)all / 100000000.0f;


			foreach (var kv in dict_) {
				float time = (float) kv.Value/100000000.0f;
				Debug.Log (kv.Key + ":" + (time/a)* 100.0f + "%");
			}

			Debug.Log ("game over!" + a +"s");
		}
		public static Task Logger(Task task, string taskName){
			#if DEBUG
			if(_instance == null){
				GameObject go = new GameObject();
				go.name = "TaskLogger";
				_instance = go.AddComponent<TaskLog>();

			}
			long begin = 0;
			TaskManager.PushFront(task, delegate() {
//				Debug.Log(taskName);
				begin = DateTime.Now.Ticks;
			});
			TaskManager.PushBack(task, delegate() {


				long all = DateTime.Now.Ticks - begin; 
//				Debug.Log("end"+taskName+all);
				if(_instance.dict_.ContainsKey(taskName)){
					_instance.dict_[taskName] += all;
				}else{
					_instance.dict_[taskName] = all;
				}
				//Debug.Log(taskName + ":" + all);
			});
			#endif
			return task;

		}
	}
}