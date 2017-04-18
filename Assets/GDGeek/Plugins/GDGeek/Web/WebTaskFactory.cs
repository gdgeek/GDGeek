using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class WebTaskFactory : Singleton<WebTaskFactory>{
		public delegate void Callback ();
		public delegate void Handler (string s);



		public static WebTaskFactory GetInstance(){
			return WebTaskFactory.Instance;
		}
	
		private IEnumerator linked(WWW www, Handler succeed, Handler error, Callback doOver){

			yield return www;
			doHandle(www, succeed, error);
			doOver();
			
		}
		public Task create(WebPack pack, Handler succeed, Handler error){
			WWW www = pack.www() as WWW;
			Task task = new Task ();
			bool over = false;
			task.init = delegate {
				over = false;  
				StartCoroutine(WebTaskFactory.GetInstance().linked(www, succeed, error, delegate{
					over = true;
				}));
			};

			task.isOver = delegate{
				return over;
			};
			return task;

		}


		public void doHandle(WWW www, Handler succeed, Handler error){
			
			if(www.error != null) {
				error(www.error);
				Debug.Log(":"+www.error);
				return;
			}
			var text = "";
			if(www.responseHeaders.ContainsKey("CONTENT-ENCODING") && www.responseHeaders["CONTENT-ENCODING"] == "gzip")
			{
				Debug.Log("a zip");
				Debug.Log(www.text);
			
				#if UNITY_IPHONE
				text = www.text;

				#else
					Debug.Log("not iphone");
					//text = JsonData.GZip.DeCompress(www.bytes);  
				#endif
			}else{
				
				Debug.Log("no zip" + www.text);
				text = www.text;
			} 
			
			
			//Debug.Log(url_); 
			succeed(text); 
			
		}
	
	}
}