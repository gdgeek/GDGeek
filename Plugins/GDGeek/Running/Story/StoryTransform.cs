using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDGeek;
namespace GDGeek
{
    public class StoryTransform : Story
    {


        [SerializeField]
        private float _time;

        [SerializeField]
        private GameObject _target;
        [SerializeField]
        private Transform _to;


        override protected Task createTask()
        {

            TweenTask task = new TweenTask(
                delegate
                {
                    return TweenTransform.Begin(_target, _time, _to);
                });
                return task;
            
        }
    }
}