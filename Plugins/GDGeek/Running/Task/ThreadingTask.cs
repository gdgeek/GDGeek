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


namespace GDGeek
{
	public class ThreadingTask : DataTask
	{

		private Func<System.Threading.Tasks.Task> creater { get; }
		private System.Threading.Tasks.Task threading { get; set; }


		public ThreadingTask(Func<System.Threading.Tasks.Task> taskCreator)
		{
			creater = taskCreator ?? throw new ArgumentNullException(nameof(taskCreator));
			this.init = initImpl;
			this.update = updateImpl;
		}

		private void initImpl()
		{
			Debug.LogWarning("init!!!");
			threading = creater();
		}


		private void updateImpl(float deltaTime)
		{
			if (threading == null) return;

			if (threading.IsCompletedSuccessfully)
			{
				this.resolve();
				disposeTask();
			}
			else if (threading.IsCanceled)
			{
				Debug.LogError("Task was canceled.");
				this.reject(new OperationCanceledException("Task was canceled."));
				disposeTask();
			}
			else if (threading.IsFaulted)
			{
				Debug.LogError("Task faulted.");
				this.reject(threading.Exception?.InnerException ?? threading.Exception);
				disposeTask();
			}
		}

		private void disposeTask()
		{
			threading?.Dispose();
			threading = null;
		}
	}
	public class ThreadingTask<T> : DataTask<T>
	{



		private Func<System.Threading.Tasks.Task<T>> creater { get; set; }
		private System.Threading.Tasks.Task<T> threading { get; set; }


		public ThreadingTask(Func<Task<T>> taskCreator)
		{
			creater = taskCreator ?? throw new ArgumentNullException(nameof(taskCreator));
			this.init = initImpl;
			this.update = updateImpl;
		}



		private void initImpl()
		{
			threading = creater();

		}


		private void updateImpl(float deltaTime)
		{

			if (threading == null) return;

			if (threading.IsCompletedSuccessfully)
			{
				try
				{
					var result = threading.Result; // 如果 Task 成功，访问 Result 不会抛出异常
					this.resolve(result);
				}
				catch (Exception ex)
				{
					this.reject(ex);
				}
				finally
				{
					disposeTask();
				}
			}
			else if (threading.IsCanceled)
			{
				this.reject(new OperationCanceledException("Task was canceled."));
				disposeTask();
			}
			else if (threading.IsFaulted)
			{
				this.reject(threading.Exception?.InnerException ?? threading.Exception);
				disposeTask();
			}

		}
		private void disposeTask()
		{
			threading?.Dispose();
			threading = null;
		}
	}

}