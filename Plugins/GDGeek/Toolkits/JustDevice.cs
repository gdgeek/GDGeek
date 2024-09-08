using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDGeek
{ 
    public class JustDevice : MonoBehaviour {
#if UNITY_EDITOR
        // Use this for initialization
        void Awake () {
            
            DestroyImmediate(this.gameObject);
        }
#endif

    }
}