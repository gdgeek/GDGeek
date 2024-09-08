using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek.Snapshot
{
    /// <summary>
    /// 将目标对象保存为json以及反序列化。
    /// </summary>
    public class Lens
    {
        [Serializable]
        public class Photo
        {
            [SerializeField]
            public PhotoTransform transform;
            [SerializeField]
            public string type;
            [SerializeField]
            public string json;
            // public 
        }

        public static void Clear()
        {

            GDGeek.Snapshot.Location.Instance.deleteAllTarget();
        }

        [Serializable]
        public class Photos
        {
            [SerializeField]
            public Photo[] list = null;
        }
        static public string TakePhoto()
        {
            Target[] targets = Location.Instance.GetComponentsInChildren<Target>();

            Photos photos = new Photos();
            photos.list = new Photo[targets.Length];
            Debug.Log(targets.Length);
            for (int i = 0; i < targets.Length; ++i)
            {

                Target target = targets[i];
                Photo photo = new Photo();
                photo.type = target.type;
                photo.json = FactoriesManager.Instance.serialize(target);
                photo.transform = new PhotoTransform(target.transform);
                photos.list[i] = photo;

            }
            return JsonUtility.ToJson(photos);
        }


        internal static Target Create(string type, string json, PhotoTransform pt)
        {

            Target target = FactoriesManager.Instance.unserialize(type, json);
            pt.readTo(Location.Instance.transform, target.transform);
            return target;
        }
        /*internal static Target Create(string type, Target.IParameter data, PhotoTransform pt)
        {
            return Create(type, data.toJson(), pt);
        }*/
        /*
        public static TargetCreateTask CreateTask(string type, string json, PhotoTransform transform)
        {

            var createTargetTask = FactoriesManager.Instance.unserializeCreateTask(type, json, transform);
            createTargetTask.shutdown += delegate
            {
                transform.readTo(Root.Instance.transform, createTargetTask._target.transform);
            };
                  
            return createTargetTask;
        }*/
        /*
        public static TargetCreateTask CreateTask(string type, Target.IParameter data, PhotoTransform transform)
        {
            return CreateTask(type, data.toJson(), transform);
        }*/

        static public List<Target> DevelopPhoto(string json)
        {
            Photos photos = JsonUtility.FromJson<Photos>(json);

            List<Target> targets = new List<Target>();
            for (int i = 0; i < photos.list.Length; ++i)
            {
                Photo photo = photos.list[i];
                targets.Add(Create(photo.type, photo.json, photo.transform));
            }
            return targets;
        }

        public class TargetCreateTask : GDGeek.Task
        {
            public Target _target;
        }
        /*
        static public List<TargetCreateTask> DevelopPhotoTaskList(string json)
        {
            Photos photos = JsonUtility.FromJson<Photos>(json);

            List<TargetCreateTask> createTargetList = new List<TargetCreateTask>();
            for (int i = 0; i < photos.list.Length; ++i)
            {
                Photo photo = photos.list[i];
                createTargetList.Add(CreateTask(photo.type, photo.json, photo.transform));
            }
            return createTargetList;
        }
        */
    }
}
