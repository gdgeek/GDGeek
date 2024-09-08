using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek { 
    public class TaskByName : MonoBehaviour, ITaskByName {
        [System.Serializable]
        public class NameStory {
            [SerializeField]
            public string name;
            [SerializeField]
            public Story story;
        }
        [SerializeField]
        private List<NameStory> _storys = new List<NameStory>();

        private Dictionary<string, IStory> map_ = new Dictionary<string, IStory>();

        private void Awake()
        {
            foreach (NameStory ns in _storys) {
                map_[ns.name] = ns.story;
            }
        }

        public Task getTask(string name)
        {

            if (map_.ContainsKey(name)) {
                return map_[name].task;
            }
            return null;
        }

      
    }
}