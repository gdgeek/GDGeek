using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek
{
    public class StoryTween : Story, IExecute
    {
        [SerializeField]
        private Tween _tween;




        public Tween tween
        {
            set
            {
                _tween = value;
            }
            get {

                if (_tween == null) {
                    _tween = this.gameObject.GetComponent<Tween>();
                }
                return _tween;
            }
        }

    
        public void execute()
        {
            tween.sample(0, false);
            tween.enabled = false;

        }
        override protected Task createTask()
        {
            TweenTask task = new TweenTask(
                delegate
                {
                    tween.execute();


                    return tween;
                });
                return task;
            
        }
    }
}