using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek { 
    public class Rotater : MonoBehaviour {
        [SerializeField]
        private Vector3 _eulerAngles;
        [SerializeField]
        private float _speed = 1f;

        public void setSpeed(float speed)
        {
            _speed = speed;
        }

        public Vector3 eulerAngles {
            set {
                _eulerAngles = value;
            }
            get {
                return _eulerAngles;
            }
        }
        // Update is called once per frame
        void Update () {
            this.transform.Rotate(_eulerAngles * _speed * Time.deltaTime);
	    }
    }
}