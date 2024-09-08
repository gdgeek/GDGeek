using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace GDGeek { 
    public class StoryEvent : Story
    {
        public UnityEvent _event;

        override protected Task createTask()
        {
           
            Task t = new Task();
                TaskManager.PushFront(t, delegate {
                    if (_event != null)
                    {
                        _event.Invoke();
                    }
                });
                return t;

            

        }
    }
}