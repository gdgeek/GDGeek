using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek { 
    public class StoryHolder : MonoBehaviour {

        [System.Serializable]
        public class Item
        {
            public string name;
            public Story story;
            public bool ignore = false;
        }
        [SerializeField]
        private List<Item> _items = new List<Item>();
        private bool busy_ = false;

        private Queue<Item> queue_ = new Queue<Item>();

        public void open(string name) {

            Item item = _items.Find(delegate (Item i) { return i.name.Equals(name); });
            if (item != null) {
                queue_.Enqueue(item);
            }
        }
        private void Update()
        {
            if (!busy_) {
                if (queue_.Count != 0) {
                    Story story = this.getNextStory();
                    if (story != null) { 
                    Task task = story.task;
                    TaskManager.PushFront(task, delegate
                    {
                        busy_ = true;
                    });
                    TaskManager.PushBack(task, delegate
                    {
                        busy_ = false;
                    });
                    TaskManager.Run(task);
                    }
                }
            }

        }

        private Story getNextStory()
        {

            while (queue_.Count > 0) {
                Item item = queue_.Dequeue();
                if (!item.ignore || queue_.Count == 0) {
                    return item.story;
                }

            }

            return null;

        }
    }

}
