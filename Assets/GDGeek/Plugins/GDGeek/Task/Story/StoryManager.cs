using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek { 
    public class StoryManager : MonoBehaviour, IStory {
        public enum Type{
            Set,
            List,
        }
        [SerializeField]
        private Type _type = Type.Set;

        [SerializeField]
        private List<GameObject> _storys;

        public List<GameObject> storys {
            get {
                return _storys;
            }

        }
        public GameObject target
        {
            get {
                return null;
            }
            
        }
        public Task task {
            get {
                if (_type == Type.Set)
                {
                    TaskSet ts = new TaskSet();
                    foreach (GameObject obj in _storys)
                    {
                        IStory story = obj.GetComponent<IStory>();
                        if (story != null)
                        {
                            ts.push(story.task);
                        }
                    }
                    return ts;
                }
                else{

                    TaskList tl = new TaskList();
                    foreach (GameObject obj in _storys)
                    {
                        IStory story = obj.GetComponent<IStory>();
                        if (story != null)
                        {
                            tl.push(story.task);
                        }
                    }
                    return tl;
                }
               
            }

        }


    }
}