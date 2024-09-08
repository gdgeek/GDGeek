using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek
{
    public class StoryDistance : MonoBehaviour, IStory
    {
        [SerializeField]
        private float _time = 1.0f;
        [SerializeField]
        private List<GameObject> _storys;

        public List<GameObject> storys
        {
            get
            {
                return _storys;
            }
        }
        [SerializeField]
        private Transform _center;

        private List<IStory> _list = new List<IStory>();
        //private 
        void Awake()
        {
            foreach (GameObject story in _storys)
            {
                IStory s = story.GetComponent<IStory>();
                if (s != null)
                {
                    _list.Add(s);
                }
            }
        }
        public GameObject target
        {
            get { 
                return null;
            }
        }

        public Task task
        {
            get { 
                TaskSet ts = new TaskSet();
                float max = 0f;
                Debug.Log(_list.Count);
                foreach (IStory story in _list)
                {
                    if (story.target != null) {
                        float disance = Vector3.Distance(_center.position, story.target.transform.position);

                        max = Mathf.Max(max, disance);
                    }
                   

                }

                if (max == 0.0f) {
                    max = 0.00001f;
                }
                foreach (IStory story in _list)
                {

                    float disance = 0f;
                    if (story.target != null)
                    {
                        disance = Vector3.Distance(_center.position, story.target.transform.position);
                    }
                    TaskList tl = new TaskList();
                    if (disance != 0f) { 
                        tl.push(new TaskWait(_time * (disance / max)));
                    }
                    tl.push(story.task);
                    ts.push(tl);
                }
                return ts;
            }
        }
    }
}