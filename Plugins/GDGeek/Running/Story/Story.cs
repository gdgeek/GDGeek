using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDGeek { 
    public abstract class Story : MonoBehaviour, IStory {

        [SerializeField]
        private UnityEvent _onStart;
        [SerializeField]
        private UnityEvent _onOver;
      
        protected abstract Task createTask();
        public Task task
        {
            get {
                Task task = this.createTask();
                if (_onStart != null) {
                    TaskManager.PushFront(task, delegate
                    {
                        _onStart.Invoke();
                    });
                }
                if (_onOver != null)
                {
                    TaskManager.PushBack(task, delegate
                    {
                        _onOver.Invoke();
                    });
                }

                return task;

            }
        }

    }
}