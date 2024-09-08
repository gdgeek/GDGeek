using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek
{
    public class StoryWait : MonoBehaviour, IStory
    {
        [SerializeField]
        private float _time = 1f;

        public GameObject target
        {
            get { 
                return null;
            }
        }
        public Task task
        {
            get {
                return new TaskWait(_time);
            }

        }



    }
}