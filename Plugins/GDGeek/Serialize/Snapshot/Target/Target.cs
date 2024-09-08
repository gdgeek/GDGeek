using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek.Snapshot
{
    public abstract class Target : MonoBehaviour//, ITarget
    {
      
        
        public string type {
            get {
                return this.GetType().ToString();
            }
        }
        public abstract Target load(string json);
        public abstract string save(Target target);
            

  
    }



}
