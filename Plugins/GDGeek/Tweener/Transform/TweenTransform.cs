using UnityEngine;
using System.Collections;
namespace GDGeek
{
    public class TweenTransform : Tween
    {
        
      //  [SerializeField]
       // public Space space = Space.World;

        [SerializeField]
        private TransformData fromData_ = null;

        [SerializeField]
        private bool _tweenPosition = true;
        [SerializeField]
        private bool _tweenScale = true;
        [SerializeField]
        private bool _tweenRotation = true;
        
        public TransformData fromData {

            get {
                if (_from != null)
                {

                    return new TransformData(_from, Space.World);
                }
                else {
                    return this.fromData_;
                }
            }
        }

        public override void onStarted()
        {
            this.fromData_ = new TransformData(target.transform, Space.World);
            base.onStarted();
        }
      
        [SerializeField]
        private Transform _from;
        [SerializeField]
        private Transform _to;


        override protected void onUpdate(float factor, bool isFinished)
        {
          
            if (_to )
            {
                if (_tweenPosition)
                {
                  
                    target.transform.position = fromData.position * (1f - factor) + _to.position * factor;
                }

                if (_tweenRotation)
                {
                    target.transform.rotation = Quaternion.Slerp(fromData.rotation, _to.rotation, factor);
                }

                if (_tweenScale)
                {
                    target.transform.setGlobalScale(fromData.scale * (1f - factor) + _to.lossyScale * factor);
                }

            }
            if (isFinished)
            {
                fromData_ = null;
            }
        }


        /// <summary>
        /// Start the tweening operation.
        /// </summary>

        static public TweenTransform Begin(GameObject go, float duration, Transform to)
        {
            TweenTransform component = Tween.Begin<TweenTransform>(go, duration);
            component._to = to;

            if (duration <= 0f)
            {
                component.sample(1f, true);
                component.enabled = false;
            }
            return component;
        }
    }
}