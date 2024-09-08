using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDGeek;
using System;


namespace GDGeek
{
    public class Trigger : MonoBehaviour
    {
        [SerializeField]
        private bool _reclickToClose = false;
        [Serializable]
        public class Item 
        {

            [SerializeField]
            public string name = null;
            [SerializeField]
            public Story open = null;
            [SerializeField]
            public Story close = null;
        }
        public enum Type
        {
            set,
            list,
        }

        public Type _type = Type.set;

        [SerializeField]
        private List<Item> _items = new List<Item>();

        private Item none_ = new Item();

        [SerializeField]
        private string state_ = null;
        [SerializeField]
        private string target_ = null;

        [SerializeField]
        private bool busy_ = false;

        //退出这个场景
        public void quit()
        {
            Item state = getItem(state_);
            Task close = state.close.task;
            TaskManager.Run(close);
        }
        /*
        public void open(AsgardInt ai) {
            open(ai.data);
        }*/
        public void open(int i) {
            open(_items[i].name);
        }
        public void open(string target) {

            if (_reclickToClose && target_ == target)
            {
                target_ = null;
            }
            else {


                target_ = target;
            }
        }
        private void Awake()
        {
            none_.name = "None_";
            none_.open = this.gameObject.AskComponent<NoneStory>();
            none_.close = this.gameObject.AskComponent<NoneStory>();
        }
        private void Update()
        {
            if (!busy_ && target_ != state_)
            {
                Task task = this.running();
                TaskManager.PushFront(task, delegate {
                    busy_ = true;
                  
                });
                TaskManager.PushBack(task, delegate {
                    busy_ = false;
                  
                });
                TaskManager.Run(task);
            }
        }
        private Item getItem(string name) {
            foreach (Item item in this._items) {
                if (item.name == name) {
                    return item;
                }
            }

            return none_;
        }
        private Task running()
        {
            Item target = getItem(target_);
            Item state = getItem(state_);
            
            string tar = target_;
            
            Task open = target.open.task;

            Task close = state.close.task;
            if (_type == Type.set)
            {
                TaskSet ts = new TaskSet();
                ts.push(open);
                ts.push(close);
                TaskManager.PushBack(ts, delegate {
                    state_ = tar;
                });
                return ts;
            }else if (_type == Type.list)
            {
                TaskList tl = new TaskList();
                tl.push(close);
                tl.push(open);
                TaskManager.PushBack(tl, delegate {
                    state_ = tar;
                });
                return tl;
            }
            return new Task();
        }
    }
}