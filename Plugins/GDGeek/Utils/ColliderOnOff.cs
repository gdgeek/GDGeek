using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek {


    public class ColliderOnOff : MonoBehaviour
    {
        private bool recode_ = false;
        
        private Collider collider_ = null;
        void Awake()
        {
            collider_ = this.gameObject.GetComponent<Collider>();
            if (collider_) {
                recode_ = collider_.enabled;
            }
        }
        void OnDestroy() {
            if (collider_){
                collider_.enabled = recode_;
            }
        }
        public void close(){
            if (collider_){
                collider_.enabled = false;
            }

        }

        public void open()
        {
            if (collider_)
            {
                collider_.enabled = true;
            }

        }
    }
}