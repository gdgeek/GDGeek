using UnityEngine;
namespace GDGeek
{
    public class TweenTransformData : Tween
    {
     
        private TransformData from {
            set;
            get;
        }
        private TransformData to {
            set;
            get;
        }
        [SerializeField]
        private Space space = Space.World;
      
        //private Transform trans_;

       // public Transform cachedTransform { get { if (trans_ == null) trans_ = target.transform; return trans_; } }
    
        override protected void onUpdate(float factor, bool isFinished)
        {
            if (this.space == Space.World)
            {
                transform.position = from.position * (1f - factor) + to.position * factor;
                transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, factor);
                transform.setGlobalScale(from.scale * (1f - factor) + to.scale * factor);
            }
            else {

                transform.localPosition = from.position * (1f - factor) + to.position * factor;
                transform.localRotation = Quaternion.Lerp(from.rotation, to.rotation, factor);
                transform.localScale = (from.scale * (1f - factor) + to.scale * factor);
            }
        }


        /// <summary>
        /// Start the tweening operation.
        /// </summary>
        static public TweenTransformData Begin(GameObject go, float duration, TransformData data, Space space = Space.World) {
            TweenTransformData component = Tween.Begin<TweenTransformData>(go, duration);
            component.space = space;
            component.from = new TransformData(component.target.transform, space);
            component.to = data;
            if (duration <= 0f)
            {
                component.sample(1f, true);
                component.enabled = false;
            }
            return component;

        }
      
    }
}