using UnityEngine;
using System.Collections;
namespace GDGeek
{
    public class TweenTransformLocalData : Tween
    {
        public class TransformData
        {
            public Vector3 localPosition;
            public Quaternion localRotation;
            public Vector3 localScale;
        };
        public TransformData from;
        public TransformData to;

        private Transform trans_;

        public Transform cachedTransform { get { if (trans_ == null) trans_ = transform; return trans_; } }
        public Vector3 position { get { return cachedTransform.position; } set { cachedTransform.position = value; } }

        override protected void onUpdate(float factor, bool isFinished)
        {
            cachedTransform.localPosition = from.localPosition * (1f - factor) + to.localPosition * factor;
            //cachedTransform.localRotation = Quaternion.Slerp(from.rotation, from.rotation, factor);
            cachedTransform.localRotation = Quaternion.Slerp(Quaternion.Euler(from.localRotation.eulerAngles), Quaternion.Euler(to.localRotation.eulerAngles), factor);
            cachedTransform.localScale = from.localScale * (1f - factor) + to.localScale * factor;
        }

        /// <summary>
        /// Start the tweening operation.
        /// </summary>

        static public TweenTransformLocalData Begin(GameObject go, float duration, Vector3 localPosition, Quaternion localRotation, Vector3 localScale)
        {
            TweenTransformLocalData component = Tween.Begin<TweenTransformLocalData>(go, duration);
            component.from = new TransformData();
            component.from.localPosition = component.transform.localPosition;
            component.from.localScale = component.transform.localScale;
            component.from.localRotation = component.transform.localRotation;
            component.to = new TransformData(); ;
            component.to.localPosition = localPosition;
            component.to.localScale = localScale;
            component.to.localRotation = localRotation;

            if (duration <= 0f)
            {
                component.sample(1f, true);
                component.enabled = false;
            }
            return component;
        }
    }
}