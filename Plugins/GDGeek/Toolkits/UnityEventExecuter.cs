using GDGeek;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDGeek { 
    public class UnityEventExecuter : MonoBehaviour, IExecute {
        [SerializeField]
        private UnityEvent _onEvent;
        public void execute()
        {
            Debug.Log("?");
            if (_onEvent != null)
            {
                Debug.Log("!");
                _onEvent.Invoke();
            }
        }

      
    }
}