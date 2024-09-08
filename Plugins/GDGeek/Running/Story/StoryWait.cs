using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek
{
    public class StoryWait : Story
    {
        [SerializeField]
        private float _time = 1f;

        override protected Task createTask()
        {
            return new TaskWait(_time);
            

        }



    }
}