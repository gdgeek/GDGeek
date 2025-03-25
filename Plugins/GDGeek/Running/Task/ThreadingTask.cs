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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using GDGeek.FileDownload;


namespace GDGeek{
	public class ThreadingTask : DataTask
	{

		private Func<System.Threading.Tasks.Task> creater { get; }
		private System.Threading.Tasks.Task threading { get; set; }
		
		private bool run_ = true;
		public ThreadingTask(Func<System.Threading.Tasks.Task> func, bool run = true)
		{
			run_ = run;
			creater = func;
			this.init = this.initImpl;
			this.update = this.updateImpl;
		}
		
		private void initImpl(){
			Debug.LogWarning("init!!!");
			threading = creater ();
		}


		private void updateImpl(float d){
			if (threading.IsCompleted)
			{
				this.resolve();
			}else if (threading.IsCanceled)
			{
				this.reject(new Exception("Threading Canceled"));
			}else if (threading.IsFaulted)
			{
				this.reject(threading.Exception);
			}

		}
	}
	public class ThreadingTask<T> : DataTask<T>
	{

		private Func<System.Threading.Tasks.Task<T>> creater { get; set; }
		private System.Threading.Tasks.Task<T> threading { get; set; }
		private bool run_ = true;
		public ThreadingTask(Func<System.Threading.Tasks.Task<T>> func, bool run = true){
			run_ = run;
			creater = func;
			this.init = this.initImpl;
			this.update = this.updateImpl;
		}
		
		private void initImpl(){
			threading = creater ();

		}


		private void updateImpl(float d){
			
			if (threading.IsCompleted)
			{
				var result = default(T);
				try
				{
					result = threading.Result;
					this.resolve(result);
				}catch (Exception e)
				{
					
					//threading.Dispose();
					this.reject(e);//这里调用了reject方法
				}

				
				
			}else if (threading.IsCanceled)
			{
				threading.Dispose();
				this.reject(new Exception("Threading Canceled"));
			}else if (threading.IsFaulted)
			{
				Debug.LogError("3");
				this.reject(threading.Exception);
			}

		}
	}
	
}