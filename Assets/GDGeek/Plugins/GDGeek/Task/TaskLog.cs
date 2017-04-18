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