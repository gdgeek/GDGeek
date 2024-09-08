using System.Collections;
using System.Collections.Generic;
using GDGeek.StateChart;
using UnityEngine;
using UnityEngine.Events;

namespace GDGeek { 
    public class EventMessageHandler : MessageHandler
    {
        [SerializeField]
        private UnityEvent _event;
        [SerializeField]
        private LeafState _next;
        public override LeafState invoke(FSMEvent evt)
        {
            if (_event != null) {
                _event.Invoke();
            }
            return _next;
        }

    }
}