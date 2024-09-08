using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek { 
    public class StoryManager : Story, IExecute
    {


        public enum Type{
            Set,
            List,
            Circle,
        }
        [SerializeField]
        private Type _type = Type.Set;

        [SerializeField]
        private List<Story> _storys;

        public List<Story> storys {
            get {
                return _storys;
            }

        }
      
        override protected Task createTask()
        {
            if (_type == Type.Set)
            {
                TaskSet ts = new TaskSet();
                foreach (Story story in _storys)
                {
                    if (story != null)
                    {
                        ts.push(story.task);
                    }
                }
                return ts;
            }
            else if (_type == Type.List)
            {

                TaskList tl = new TaskList();
                foreach (Story story in _storys)
                {
                    if (story != null)
                    {
                        tl.push(story.task);
                    }
                }
                return tl;
            }
            else if (_type == Type.Circle) {
                TaskCircle tc = new TaskCircle();
                foreach (Story story in _storys)
                {
                    if (story != null)
                    {
                        tc.push(story.task);
                    }
                }
                return tc;
            }

            return new Task();

        }

        public void execute()
        {

            _storys = new List<Story>();
            for (int i = 0; i < this.transform.childCount; ++i) {
                Transform c = this.transform.GetChild(i);
                Story story = c.GetComponent<Story>();
                if (story != null) {

                    _storys.Add(story);
                }
            }
           
        }
    }
}