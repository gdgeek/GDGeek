using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class TweenTransformPosition : Tween {
		public Vector3 from;
		public Transform to;

		Transform mTrans;

		public Transform cachedTransform { get { if (mTrans == null) mTrans = transform; return mTrans; } }
		public Vector3 position { get { return cachedTransform.position; } set { cachedTransform.position = value; } }

		override protected void OnUpdate (float factor, bool isFinished) { cachedTransform.position = from * (1f - factor) + to.position * factor; }

		/// <summary>
		/// Start the tweening operation.
		/// </summary>

		static public TweenTransformPosition Begin (GameObject go, float duration, Transform transform)
		{
			TweenTransformPosition comp = Tween.Begin<TweenTransformPosition>(go, duration);
			comp.from = comp.position;
			comp.to = transform;

			if (duration <= 0f)
			{
				comp.Sample(1f, true);
				comp.enabled = false;
			}
			return comp;
		}
	}
}