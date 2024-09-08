using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek.Snapshot
{

    public class FactoriesManager : GDGeek.Singleton<FactoriesManager>
    {
         
        private List<Target> _phototypes = new List<Target>();

        public void addPhototype(Target target) {
            target.transform.SetParent(this.transform);
            _phototypes.Add(target);

        }

        /*
        public Target create(string type, Target.IParameter parameter) {
            Target target = find(type);
            if (target != null)
            {
                return target.create(parameter.toJson());
            }
            return null;
        }*/
        public Target create(string type, string json)
        {
            Target target = find(type);
            if(target != null) {
                return target.load(json);
            }
            return null;
        }
            
        internal string serialize(Target target)
        {
            if (target != null)
            {
                return target.save(target);
            }
            return null;
        }
          
        private Target find(string type)
        {
            Debug.Log("type:" + type);
            for (int i = 0; i < _phototypes.Count; ++i)
            {
                if (_phototypes[i].type == type)
                {
                    return _phototypes[i];
                }
            }
            return null;
        }
        internal Target unserialize(string type, string json)
        {
            Target target = this.find(type);
            if (target != null) {
                return target.load(json);
            }
            return null;
        }
        /*
        internal Lens.TargetCreateTask unserializeCreateTask(string type, string json, PhotoTransform photoTransform)
        {
            Target target = this.find(type);
            if (target != null)
            {
                return target.createTask(json, photoTransform);
            }
            return null;
        }*/

    }
}