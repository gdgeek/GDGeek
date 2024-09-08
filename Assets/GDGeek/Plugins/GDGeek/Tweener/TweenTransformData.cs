using UnityEngine;
namespace GDGeek
{
    public class TweenTransformData : Tween
    {
        public class TransformData
        {
            public Vector3 position;
            public Quaternion rotation;
            public Vector3 scale;
        };
        private TransformData from {
            set;
            get;
        }
        private TransformData to {
            set;
            get;
        }

        private Transform trans_;

        public Transform cachedTransform { get { if (trans_ == null) trans_ = transform; return trans_; } }
    
        override protected void onUpdate(float factor, bool isFinished)
        {
            cachedTransform.position = from.position * (1f - factor) + to.position * factor;
            cachedTransform.rotation = Quaternion.Slerp(from.rotation, to.rotation, factor);
            cachedTransform.setGlobalScale(from.scale * (1f - factor) + to.scale * factor);
        }

        /// <summary>
        /// Start the tweening operation.
        /// </summary>

        static public TweenTransformData Begin(GameObject go, float duration, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            TweenTransformData component = Tween.Begin<TweenTransformData>(go, duration);
            component.from = new TransformData();
            component.from.position = component.transform.position;
            component.from.scale = component.transform.lossyScale;
            component.from.rotation = component.transform.rotation;
            component.to = new TransformData(); ;
            component.to.position = position;
            component.to.scale = scale;
            component.to.rotation = rotation;

            if (duration <= 0f)
            {
                component.sample(1f, true);
                component.enabled = false;
            }
            return component;
        }
    }
}