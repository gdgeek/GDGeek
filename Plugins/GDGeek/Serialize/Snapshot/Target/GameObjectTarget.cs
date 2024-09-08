using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek.Snapshot
{
    public class GameObjectTarget : Target
    {
       

        public override Target load(string json)
        {
            return GameObject.Instantiate(this);
        }

        public override string save(Target obj)
        {
            return JsonUtility.ToJson(null);
        }
        /*
        internal override Lens.TargetCreateTask createTask(string json, PhotoTransform photoTransform)
        {
            throw new System.NotImplementedException();
        }*/
    }
}