using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek.Snapshot { 

    [Serializable]
    public class PhotoTransform
    {
        [SerializeField]
        public Vector3 localPosition;
        [SerializeField]
        public Quaternion localRotation;
        [SerializeField]
        public Vector3 localScale;
        [SerializeField]
        public string name;

        public PhotoTransform()
        {
            localScale = Vector3.one;
            localPosition = Vector3.zero;
        }

        public PhotoTransform(Transform transform)
        {
            localPosition = transform.localPosition;
            localScale = transform.localScale;
            localRotation = transform.localRotation;
            name = transform.name;
        }

        internal void readTo(Transform parent, Transform transform)
        {
            transform.SetParent(parent);
            transform.localPosition = this.localPosition;
            transform.localRotation = this.localRotation;
            transform.localScale = this.localScale;
            transform.name = this.name;
        }
    }
         
}