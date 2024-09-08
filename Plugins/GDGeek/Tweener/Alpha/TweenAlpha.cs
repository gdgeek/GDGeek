using GDGeek;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek
{
    public class TweenAlpha : Tween
    {


        [SerializeField]
        private float _from = 1.0f;
        [SerializeField]
        private float _to = 1.0f;


        [SerializeField]
        private float _alpha = 1.0f;
        public float alpha
        {
            get
            {

                return getAlpha();
            }
            set
            {

                setAlpha(value);
            }
        }
        public void setAlpha(float alpha)
        {
            _alpha = alpha;
            //Debug.Log(alpha);
            refresh();
        }
        public float getAlpha()
        {
            return _alpha;
        }
        private Renderer[] renderers_;

        public Renderer[] renderers
        {

            get
            {

                if (renderers_ == null)
                {
                    renderers_ = this.target.GetComponentsInChildren<Renderer>();
                }
                return renderers_;
            }
        }
        private CanvasGroup[] groups_;


        public CanvasGroup[] groups
        {

            get
            {

                if (groups_ == null)
                {
                    groups_ = this.target.GetComponentsInChildren<CanvasGroup>();
                }
                return groups_;
            }
        }
        protected override void Start()
        {
            base.Start();
            refresh();
        }

        private void refresh()
        {
            foreach (Renderer render in renderers)
            {
                if (render.material.HasProperty("_Color"))
                {
                    Color color = render.material.color;
                    color.a = _alpha;
                    render.material.color = color;
                }
            }

            foreach (CanvasGroup cg in groups)
            {
                cg.alpha = _alpha;
            }
        }



        override protected void onUpdate(float factor, bool isFinished)
        {
            alpha = _from * (1f - factor) + _to * factor;

        }

        /// <summary>
        /// Start the tweening operation.
        /// </summary>

        static public TweenAlpha Begin(GameObject go, float duration, float alpha)
        {
            TweenAlpha component = Tween.Begin<TweenAlpha>(go, duration);
           // Debug.LogError(component.alpha);
            component._from = component.alpha;
            component._to = alpha;

            if (duration <= 0f)
            {
                component.sample(1f, true);
                component.enabled = false;
            }
            return component;
        }


        
    }
}