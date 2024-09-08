using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek { 
    public class TriggerManager : Singleton<TriggerManager> {
       private Dictionary<string, ITrigger> triggers_ = new Dictionary<string, ITrigger>();
        public void addTrigger(ITrigger trigger) {
            Debug.Log(">>>"+trigger.triggerName);
            triggers_.Add(trigger.triggerName, trigger);
        }
        public void removeTrigger(ITrigger trigger) {
            triggers_.Remove(trigger.triggerName);
        }


        [SerializeField]
        private Dictionary<string, ITrigger> opened_ = new Dictionary<string, ITrigger>();

        [SerializeField]
        private Dictionary<string, ITrigger> target_ = new Dictionary<string, ITrigger>();

        [SerializeField]
        private HashSet<string> busy_ = new HashSet<string>();


        public void trigging(string triggerName) {
            if (triggers_.ContainsKey(triggerName) && !target_.ContainsKey(triggerName)) {
                target_.Add(triggerName, triggers_[triggerName]);
            }
        }
        public void close(string groupName) {
            if (opened_.ContainsKey(groupName)) {
                ITrigger trigger = opened_[groupName];
                trigging(trigger.triggerName);
            }
        }


        void Update()
        {
            List<string> remove = new List<string>();
            foreach (var target in target_) {
                if (busy_.Contains(target.Value.groupName)) {
                    continue;
                }
                if (opened_.ContainsKey(target.Value.groupName))
                {
                    if (opened_[target.Value.groupName].triggerName == target.Value.triggerName)
                    {
                        close(target.Value);
                    }
                    else
                    {
                        openAndClose(target.Value, opened_[target.Value.groupName]);
                    }
                }
                else {
                    open(target.Value);
                }
                remove.Add(target.Key);
            }
            foreach (string key in remove) {
                target_.Remove(key);
            }
          
        }

        private void open(ITrigger trigger)
        {
            Task task = trigger.open();

            TaskManager.PushFront(task, delegate
            {
                busy_.Add(trigger.groupName);
            });
            TaskManager.PushBack(task, delegate {
                busy_.Remove(trigger.groupName);
                opened_.Add(trigger.groupName, trigger);
            });
            TaskManager.Run(task);

        }


        private void openAndClose(ITrigger oTrigger, ITrigger cTrigger)
        {
            Task open = oTrigger.open();
            Task close = cTrigger.close();
            TaskSet ts =  new TaskSet();
            ts.push(open);
            ts.push(close);

            TaskManager.PushFront(ts, delegate
            {
                busy_.Add(oTrigger.groupName);
            });
            TaskManager.PushBack(ts, delegate {
                busy_.Remove(oTrigger.groupName);
                opened_[oTrigger.groupName] = oTrigger;
                /*
                opened_.Add(oTrigger.groupName, oTrigger);
                opened_.Remove(cTrigger.groupName);*/
            });
            TaskManager.Run(ts);
        }

        private void close(ITrigger trigger)
        {
            Task task = trigger.close();

            TaskManager.PushFront(task, delegate
            {
                busy_.Add(trigger.groupName);
            });
            TaskManager.PushBack(task, delegate {
                busy_.Remove(trigger.groupName);
                opened_.Remove(trigger.groupName);
            });
            TaskManager.Run(task);
        }

        
    }
}