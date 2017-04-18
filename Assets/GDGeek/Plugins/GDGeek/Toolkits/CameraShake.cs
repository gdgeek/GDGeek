using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class CameraShake : MonoBehaviour {
		public struct Parameter{
			public Vector3 amplitude;
			public Tween.Method methon;
			public int times;
			public float time;


		}
		private Task move (Tween.Method method, Vector3 to, float time)
		{
			TweenTask tt = new TweenTask (delegate{
				Tween tween = TweenLocalPosition.Begin(this.gameObject, time, to);
				tween.method = method;
				return tween;
			});
			return tt;
		}

		private Task shake(Tween.Method method, int times, float time, Vector3 original, Vector3 border1, Vector3 border2){
			TaskList tl = new TaskList ();
			int index = 0;
			float step = time / (float)(times);
			Debug.LogWarning (step);
			while (times > 0) {
				switch(index){
				case 0:
					index = 1;
					
					times -= 1;
					tl.push (move(method, border1, step));
					break;
				case 1:
					if(times >=2){
						index -= 2;
						index = -1;
						tl.push (move(method, border2, 2 * step));
					}else{
						times-= 1;
						index = 0;
						tl.push (move(method,  original, 2 * step));
					}
					break;
				case -1:
					if(times >= 2){
						times -= 2;
						index = 1;
						tl.push (move(method, border1, 2 * step));
					}else{
						times -= 1;
						index = 0;
						
						tl.push (move(method, original,  step));
					}
					break;
					
				}
			}
			return tl;
		}


		public Task shake (Parameter p)
		{

			return shake (p.methon, p.times *2, p.time, Vector3.zero, p.amplitude,-p.amplitude);

		}

		// Use this for initialization
		void Start () {
		/*	TaskList tl = new TaskList ();
			tl.push (new TaskWait (0.5f));
			Parameter p = new Parameter();

			tl.push (this.shake (p));
			TaskManager.Run (tl);*/
		}
		
	
	}
}