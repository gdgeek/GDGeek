using UnityEngine;
using System.Collections;
namespace GDGeek
{
    public class IsBoxChanged
    {

        private TransformData data_ = new TransformData();
        private Vector3 center_;
        private Vector3 size_;
        public bool hasChanged(Transform transform)
        {
            TransformData data = new TransformData(transform);
            if (data_ != data)
            {
                refresh(transform);
                return true;
            }
            BoxCollider bc = transform.gameObject.GetComponent<BoxCollider>();
            if (bc) {
                if (bc.center != center_ || bc.size != size_) {
                    refresh(transform);
                    return true;
                }
            }

            return false;
        }
        private void refresh(Transform transform) {

            data_ = new TransformData(transform);
            BoxCollider bc = transform.gameObject.GetComponent<BoxCollider>();
            if (bc) {
                center_ = bc.center;
                size_ = bc.size;
            }
        }


    }
}