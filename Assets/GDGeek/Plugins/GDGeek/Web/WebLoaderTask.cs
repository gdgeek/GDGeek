using UnityEngine;
using System.Collections;
using System;


namespace GDGeek{

	[Serializable]
	public abstract class DataInfo{

		[SerializeField]
		public bool succeed = false;
		[SerializeField]
		public string message = "";
		[SerializeField]
		public double epoch = 0;
		
	}

/*public interface DataInfoLoader<T>{
		T load (string json);
	}
*/
	public class WebLoaderTask<T> : Task where T:DataInfo {
		public delegate void Succeed (T info);
		public delegate void Error (string msg);
		public event Succeed onSucceed;
		public event Error onError;
		private WebPack pack_ = null;
		public WebPack pack{
			get{
				return pack_;
			}
		}
	
		private void succeed(string json){
			Debug.Log(json);
			T t = JsonUtility.FromJson<T> (json);
			if (t.succeed) {
				WebTimestamp.GetInstance().synchro(t.epoch);
				if (onSucceed != null) {
					onSucceed (t);
				}
			} else {
				error (t.message);
			}

		}
		private void error(string msg){
			if (onError != null)
				onError (msg);
		}
		public WebLoaderTask(string url){
			pack_ = new WebPack (url);
			pack_.addField("a", "b");
			bool isOver = false;
		

			this.init = delegate {
				
				Task web = WebTaskFactory.Instance.create(pack,succeed,error);
				TaskManager.PushFront(web, delegate{
					isOver = false;
				});
				TaskManager.PushBack (web, delegate{
					isOver = true;
				});
				this.isOver = delegate {
					return isOver;
				};

			
				TaskManager.Run (web);
			};

		

		}
		

	}
}
