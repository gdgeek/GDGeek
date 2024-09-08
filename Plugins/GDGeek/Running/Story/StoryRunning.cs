using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek
{
    public class StoryRunning : MonoBehaviour
    {


        [SerializeField]
        private float _time = 1.0f;

        // Use this for initialization
        void Start() {
            Story story = this.gameObject.GetComponent<Story>();
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
