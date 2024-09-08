using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek
{
    public class StoryTween : MonoBehaviour, IStory, IExecute
    {

        [SerializeField]
        private float _time = 0.3f;
        [SerializeField]
        private Tween _tween;



        [SerializeField]
        private Tween.Style _style = Tween.Style.Once;


        public Tween tween
        {
            set
            {
                _tween = value;
            }
        }

        public GameObject target
        {
            get { 
                return _tween.gameObject;
            }
        }
        public void execute()
        {
            _tween.sample(0, false);
            _tween.enabled = false;

        }
        public Task task
        {
            get { 
                TweenTask task = new TweenTask(
                delegate
                {

                    _tween.style = _style;
                    _tween.duration = _time;
                    _tween.Reset();
                    //_tween.sample(0, false);
                    _tween.enabled = true;


                    return _tween;
                });
                return task;
            }
        }
    }
}