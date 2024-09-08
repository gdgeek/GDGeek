using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek
{
    public class StoryGameObjectSetActive : Story
    {

        [SerializeField]
        private bool _active = false;


        public bool active
        {
            set
            {
                this._active = value;
            }

        }


        [SerializeField]
        private GameObject _target = null;

      
        public void setTarget(GameObject target) {
            this._target = target;
        }

        override protected Task createTask()
        {
            Task task = new Task();
                TaskManager.PushBack(task, delegate
                {
                    _target.SetActive(_active);
                });
                return task;
          

        }



    }
}