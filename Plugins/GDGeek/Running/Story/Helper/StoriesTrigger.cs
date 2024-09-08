using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDGeek;
using System;


namespace GDGeek
{
    public class StoriesTrigger : MonoBehaviour
    {

        [SerializeField]
        private bool _reclickToClose = false;
     
        public enum Type
        {
            set,
            list,
        }

        public Type _type = Type.set;



        [SerializeField]
        private List<StoriesPair> _items = new List<StoriesPair>();

        private StoriesPair none_;


        [SerializeField]
        private int state_ = -1;
        [SerializeField]
        private int target_ = -1;

        [SerializeField]
        private bool busy_ = false;

        //退出这个场景
        public void quit()
        {
            StoriesPair state = getItem(state_);
            Task close = state.first.task;
            TaskManager.Run(close);
        }


        public void open(int target)
        {

            if (_reclickToClose && target_ == target)
            {
                target_ = -1;
            }
            else
            {


                target_ = target;
            }
        }
        private void Awake()
        {
            none_ = this.gameObject.AskComponent<StoriesPair>();
            none_.first = this.gameObject.AskComponent<NoneStory>();
            none_.second = this.gameObject.AskComponent<NoneStory>();
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
        private StoriesPair getItem(int index)
        {
            if(index >= 0 && index < this._items.Count) { 
                return this._items[index];
            }
          
            return none_;
        }
        private Task running()
        {
            StoriesPair target = getItem(target_);
            StoriesPair state = getItem(state_);

            int targetIndex = target_;

            Task openTask = target.first.task;
            Task closeTask = state.second.task;
            if (_type == Type.set)
            {
                TaskSet ts = new TaskSet();
                ts.push(openTask);
                ts.push(closeTask);
                TaskManager.PushBack(ts, delegate {
                    state_ = targetIndex;
                });
                return ts;
            }
            else if (_type == Type.list)
            {
                TaskList tl = new TaskList();
                tl.push(closeTask);
                tl.push(openTask);
                TaskManager.PushBack(tl, delegate {
                    state_ = targetIndex;
                });
                return tl;
            }
            return new Task();
        }
    }
}