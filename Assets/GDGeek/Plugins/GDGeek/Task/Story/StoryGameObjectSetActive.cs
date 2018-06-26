using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek
{
    public class StoryGameObjectSetActive : MonoBehaviour, IStory
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

        public GameObject target
        {
            get {
                return this._target;
            }
            set {

                this._target = value;
            }
            
        }
      
        public Task task
        {

            get {

                Task task = new Task();
                TaskManager.PushBack(task, delegate
                {
                    _target.SetActive(_active);
                });
                return task;
            }
          

        }



    }
}