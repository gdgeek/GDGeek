using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace GDGeek { 
    public class StoryEvent : MonoBehaviour,IStory {
        public UnityEvent _event;
        public GameObject target {
            get {
                return null;
            }
        }
        public Task task {
            get {
                Task task = new Task();
                TaskManager.PushFront(task, delegate {
                    if (_event != null)
                    {
                        _event.Invoke();
                    }
                });
                return task;

            }

        }
    }
}