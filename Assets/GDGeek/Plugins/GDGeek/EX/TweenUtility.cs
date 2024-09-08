using UnityEngine;
using System.Collections;
namespace GDGeek
{
    public static class TweenUtility
    {
        public static TaskList AddAnimation(this TaskList list, TweenTask.Maker maker, TaskInit init=null, TaskShutdown shutdown=null)
        {
            var tt = new TweenTask(
                maker
            );

            if (init != null)
            {
                tt.init = init;
            }

            if (shutdown != null)
            {
                tt.shutdown = shutdown;
            }

            list.push(tt);
            return list;
        }

        public static TaskSet AddAnimation(this TaskSet set, TweenTask.Maker maker, TaskInit init = null, TaskShutdown shutdown = null)
        {
            var tt = new TweenTask(
                maker
            );

            if (init != null)
            {
                tt.init = init;
            }

            if (shutdown != null)
            {
                tt.shutdown = shutdown;
            }

            set.push(tt);
            return set;
        }

        public static TaskList Wait(this TaskList list, float seconds)
        {
            var tw = TaskWait.Create(seconds, ()=> { });
            list.push(tw);
            return list;
        }

        public static TaskList AddParallelAnimations(this TaskList list, TweenTask.Maker[] tweenTaskMethods)
        {
            TaskSet ts = new TaskSet();
            foreach (var tweenTaskMethod in tweenTaskMethods)
            {
                ts.push(new TweenTask(tweenTaskMethod));
            }
            list.push(ts);
            return list;
        }

        public static Tween SetMethod(this Tween tween, Tween.Method method)
        {
            tween.method = method;
            return tween;
        }
    }
}
