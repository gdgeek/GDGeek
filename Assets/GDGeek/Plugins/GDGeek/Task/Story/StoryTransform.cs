using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDGeek;
namespace GDGeek
{
    public class StoryTransform : MonoBehaviour, IStory
    {


        [SerializeField]
        private float _time;

        [SerializeField]
        private GameObject _target;
        [SerializeField]
        private Transform _to;



        public GameObject target
        {
            get { 
                return _target;
            }
        }
        public Task task
        {
            get { 

                TweenTask task = new TweenTask(
                delegate
                {
                    return TweenTransform.Begin(_target, _time, _to);
                });
                return task;
            }
        }
    }
}