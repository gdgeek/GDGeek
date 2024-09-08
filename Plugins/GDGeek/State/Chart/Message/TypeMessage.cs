using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek
{
    public class TypeMessage<T> : Message
    {

        [SerializeField]
        private T _value;
        public T value
        {

            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public override FSMEvent evt
        {
            get
            {
                FSMEvent e = base.evt;
                e.obj = this.value;
                return e;
            }
        }

    }
}


