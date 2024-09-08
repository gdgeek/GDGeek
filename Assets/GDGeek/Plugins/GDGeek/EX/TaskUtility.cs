using UnityEngine;
using System.Collections;
using GDGeek;
using System;
namespace GDGeek{
	public static class TaskUtility 
	{
	    public static TaskList AddTask(this TaskList list, Task task)
	    {
	        list.push(task);
	        return list;
	    }

	    public static TaskSet AddTask(this TaskSet set, Task task)
	    {
	        set.push(task);
	        return set;
	    }

	    public static Task Back(this Task task, GDGeek.TaskShutdown frontTask)
	    {
	        TaskManager.PushBack(task, frontTask);
	        return task;
	    }

	    public static Task Front(this Task task, GDGeek.TaskInit frontTask)
	    {
	        TaskManager.PushFront(task ,frontTask);
	        return task;
	    }

	    public static Task Shutdown(this Task task, GDGeek.TaskShutdown shudownAction)
	    {
	        task.shutdown += shudownAction;
	        return task;
	    }

	    public static Task Init(this Task task, GDGeek.TaskInit initAction)
	    {
	        task.init += initAction;
	        return task;
	    }

	    public static Task Update(this Task task, GDGeek.TaskUpdate updateAction)
	    {
	        task.update += updateAction;
	        return task;
	    }
	    public static Task Over(this Task task, GDGeek.TaskIsOver over)
	    {
	        task.isOver += over;
	        return task;
	    }
	}
}