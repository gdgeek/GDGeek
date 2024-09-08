using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek
{
    public class StoryTweens : Story, IExecute
    {
        [SerializeField]
        private List<Tween> _tweens;




        public List<Tween> tweens
        {
            set
            {
                _tweens = value;
            }
        }

      
        public void execute()
        {
            foreach (Tween tween in _tweens) {
                tween.sample(0, false);
                tween.enabled = false;
            }

        }
        override protected Task createTask()
        {
            TweensTask task = new TweensTask(
                delegate
                {
                    foreach (Tween tween in _tweens)
                    {
                        tween.execute();
                    }

                    return _tweens.ToArray();
                });
            return task;

        }
    }
}