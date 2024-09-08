using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek
{
    public class StorySound : Story
    {


        [SerializeField]
        private AudioSource _source;


      
        override protected Task createTask()
        {
            Task task = new Task();
                TaskManager.PushFront(task, delegate
                {
                    _source.Play();

                });
                TaskManager.AddIsOver(task, delegate
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