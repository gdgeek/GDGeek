using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GDGeek;
namespace GDGeek
{
    public class StoryRunning : MonoBehaviour
    {


        [SerializeField]
        private float _time = 1.0f;

        // Use this for initialization
        void Start() { 
            IStory story = this.gameObject.GetComponent<IStory>();
            if (story != null)
            {
                TaskList tl = new TaskList();
                tl.push(new TaskWait(_time));
                tl.push(story.task);
                TaskManager.Run(tl);
            }
        }


    }
}
