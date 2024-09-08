using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek.Snapshot
{
    public class Location : GDGeek.Singleton<Location>
    {
        public void deleteAllTarget()
        {
            Target[] targets = this.gameObject.GetComponentsInChildren<Target>();
            for (int i = 0; i < targets.Length; ++i) {
                    
                DestroyImmediate(targets[i].gameObject);
            }
        }
     
    }
}
