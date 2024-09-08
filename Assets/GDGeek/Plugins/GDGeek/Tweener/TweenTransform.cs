using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class TweenTransform : Tween {
		public class TransformData
		{
			public Vector3 position;
			public Quaternion rotation;
			public Vector3 scale;

		};
        [SerializeField]
		private TransformData _fromData;


        [SerializeField]
        private TransformData fromData {
            get {
                if (_fromData == null) {
                    this._fromData = new TransformData();
                    this._fromData.position = this.transform.position;
                    this._fromData.scale = this.transform.lossyScale;
                    this._fromData.rotation = this.transform.rotation;
                }
                return _fromData;
            }
        }

        [SerializeField]
        private Transform _from;
        [SerializeField]
        private Transform _to;

        private Transform trans_;


        public Transform cachedTransform { get { if (trans_ == null) trans_ = transform; return trans_; } }
		public Vector3 position { get { return cachedTransform.position; } set { cachedTransform.position = value; } }

    

		override protected void onUpdate (float factor, bool isFinished) {

            if (_from != null)
            {

                cachedTransform.position = _from.position * (1f - factor) + _to.position * factor;
                cachedTransform.rotation = Quaternion.Slerp(_from.rotation, _to.rotation, factor);
                cachedTransform.setGlobalScale(_from.lossyScale * (1f - factor) + _to.lossyScale * factor);
            }
            else{ 
			    cachedTransform.position = fromData.position * (1f - factor) + _to.position * factor;
                cachedTransform.rotation = Quaternion.Slerp(fromData.rotation, _to.rotation, factor);
                cachedTransform.setGlobalScale(fromData.scale * (1f - factor) + _to.lossyScale * factor);
            }
        }



        static public TweenTransform Begin(GameObject go, float duration, Transform from, Transform to)
        {
            TweenTransform component = Tween.Begin<TweenTransform>(go, duration);
            component._from = from;
            component._to = to;

            if (duration <= 0f)
            {
                component.sample(1f, true);
                component.enabled = false;
            }
            return component;
        }


        /// <summary>
        /// Start the tweening operation.
        /// </summary>

        static public TweenTransform Begin (GameObject go, float duration, Transform to)
		{
			TweenTransform component = Tween.Begin<TweenTransform>(go, duration);
			component._fromData = new TransformData ();
			component._fromData.position = component.transform.position;
			component._fromData.scale = component.transform.lossyScale;
			component._fromData.rotation = component.transform.rotation;
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