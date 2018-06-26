using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek
{
    public class StorySound : MonoBehaviour, IStory
    {


        [SerializeField]
        private AudioSource _source;


        public GameObject target
        {
            get { 
                return _source.gameObject;
            }
        }
        public Task task
        {
            get {
                Task task = new Task();
                TaskManager.PushFront(task, delegate
                {
                    _source.Play();

                });
                TaskManager.AddAndIsOver(task, delegate
                {
                    return !_source.isPlaying;
                });
                TaskManager.PushBack(task, delegate
                {
                    Debug.Log("isOver");
                });
                return task;
            }
        }



    }
}