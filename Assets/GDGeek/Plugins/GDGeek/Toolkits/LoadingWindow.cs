
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

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

namespace GDGeek{
	public class LoadingWindow : Singleton<LoadingWindow> {
		public CanvasGroup _cg = null;
	
		public float _wait = 0.3f;
		public void Awake(){

		}
		private Task packTask(Task task){
			Task pack = new Task();
			float allTime = 0.0f;
			bool isOver = false;
			bool isLoading = false;
			bool isLoaded = false;
			pack.init = delegate{
				allTime = 0;
				isOver = false;
				isLoading = false;
				isLoaded = false;
				TaskManager.PushBack (task, delegate{
					isOver = true;
					
				});
				TaskManager.Run (task);
			};
			pack.update = delegate(float d) {
				allTime += d;
				if(allTime>_wait && !isLoading){

					isLoading = true;
					Task show = this.show ();
					TaskManager.PushBack (show, delegate{
						//Debug.LogError("???");
						isLoaded = true;
					});
					TaskManager.Run (show);
				}
			};
			
			
			pack.isOver = delegate {
				if (isOver) {
					if(!isLoading){//Debug.LogError("error");
						return true;
					}
					if(isLoaded){//Debug.LogError("erro2r");
						return true;
					}
					
				}
				return false;
				
			};
			return pack;
		}
		public Task run(Task task){


			TaskList tl = new TaskList ();


			tl.push (packTask(task));
			tl.push (hide ());

			TaskManager.PushFront (tl, delegate {
				_cg.alpha = 0f;
				this.gameObject.SetActive(true);
			});
			return tl;
		}
		private Task show(){
			TweenTask task = new TweenTask (
				delegate{
					this.gameObject.SetActive(true);
					TweenGroupAlpha alpha = TweenGroupAlpha.Begin(this.gameObject,0.3f, 1.0f);
					alpha.cachedGroup = _cg;
					return alpha;
				}
			);
			TaskManager.PushBack (task, delegate {
				_cg.alpha = 1.0f;
			});
			return task;
		}
		private Task hide(){
			TweenTask task = new TweenTask
				(
				delegate{
				TweenGroupAlpha alpha = TweenGroupAlpha.Begin(this.gameObject,0.15f, 0.0f);
				return alpha;
			}
			);
		
	

			TaskManager.PushBack (task, delegate {
				if(_cg != null) _cg.alpha = 0.0f;
				if(this != null && this.gameObject != null) this.gameObject.SetActive(false);
			});
			return task;
		}
	}

}
