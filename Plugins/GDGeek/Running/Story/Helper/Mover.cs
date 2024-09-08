using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek { 
    public class Mover : MonoBehaviour {
        [SerializeField]
        private Vector3 _offset;
        [SerializeField]
        private float _speed = 1f;
        
        [SerializeField]
        private bool _limit = false;
        [SerializeField]
        private float _limitDistance = 10f;

        private Vector3 origin_;

        void Start()
        {
            origin_ = this.transform.position;
        }

        public void setSpeed(float speed)
        {
            _speed = speed;
        }

        public Vector3 offset {
            set {
                _offset = value;
            }
            get {
                return _offset;
            }
        }
        // Update is called once per frame
        void Update () {
            this.transform.position = this.transform.position + _offset * _speed * Time.deltaTime;
            if (_limit)
            {
               if( Vector3.Distance(origin_, this.transform.position)> _limitDistance)
                {
                    this.transform.position = origin_;
                } 
            }
        }
    }
}