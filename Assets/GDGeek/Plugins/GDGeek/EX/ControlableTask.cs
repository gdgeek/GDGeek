using UnityEngine;
using System.Collections;
using GDGeek;
using System;

public class ControlableTask {
	public Task _task = new Task();
	public Action onPause;
	public Action onContiunePlay;
	private bool isStopTask = true;
	public void stop (){
		isStopTask = true;
	}
	public void pause(){
		if (onPause != null) {
			onPause ();
		}
	}
	public void contiunePlay(){
		if (onContiunePlay != null) {
			onContiunePlay ();
		}
	}
	public bool isPlaying(){
		return !isStopTask;
	}
	public void start(){
		if (!isStopTask) {
			return;
		}
		_task.isOver += delegate {
			return isStopTask;	
		};
		isStopTask = false;
		TaskManager.Run (_task);
	}
	public ControlableTask(TaskUpdate updateFunc, TaskInit initFunc = null, TaskShutdown shutdownFunc = null, Action pauseFunc = null, Action continuePlayFunc = null){
		_task.update = updateFunc;
		if (initFunc != null) {
			_task.init = initFunc;
		}
		if (shutdownFunc != null) {
			_task.shutdown = shutdownFunc;
		}
		if (pauseFunc != null) {
			onPause = pauseFunc;
		}
		if (continuePlayFunc != null) {
			onContiunePlay = continuePlayFunc;
		}
	}
	public ControlableTask(Task t){
		_task = t;
	}
}
