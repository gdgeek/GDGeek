using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek { 
    public class StoryTaskByName : Story
    {
        [SerializeField]
        public GameObject _target = null;
        [SerializeField]
        public string _taskName;

        protected override Task createTask()
        {
            ITaskByName[] tbns = _target.GetComponentsInChildren<ITaskByName>();
            TaskSet ts = new TaskSet();
            foreach (ITaskByName tbn in tbns) {

                Task task = tbn.getTask(_taskName);
                if (task != null) {
                    ts.push(task);
                }
            }
            return ts;

        }

      
    }
}