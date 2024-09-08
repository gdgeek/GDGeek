using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDGeek
{
    
    public class EditorOrNot : MonoBehaviour
    {
        public UnityEvent _editor;
        public UnityEvent _not;
        // Use this for initialization

#if UNITY_EDITOR
        void Awake()
        {
            if (_editor != null) {
                _editor.Invoke();
            }
        }
#else 
        void Awake()
        {
            if (_not != null) {
                _not.Invoke();
            }
        }
#endif

    }
}