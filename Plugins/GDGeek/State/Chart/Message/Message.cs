using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek {
    public class Message {

        [SerializeField]
        private string _message;

        public string message
        {

            get
            {
                return _message;
            }
            set
            {
                _message = value;
            }
        }


        public virtual FSMEvent evt{
            get{
                FSMEvent e = new FSMEvent(_message);
                return e;
            }
        }

    }
}