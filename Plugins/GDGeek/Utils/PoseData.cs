using System;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek
{
    [Serializable]
    public class PoseData
    {
        public PoseData() { }

        public static bool operator ==(PoseData lhs, PoseData rhs)
        {
            if (null == (object)(lhs) && null == (object)rhs)
            {
                return true;
            }
            if (null == (object)lhs || null == (object)rhs)
            {
                return false;
            }
            if (lhs.Equals(rhs))
            {
                return true;
            }

            return false;

        }

          public void write(ref Transform transform, Space type = Space.World)
        {
            if (type == Space.Self)
            {
                transform.localPosition = position;
                transform.localRotation = rotation;
             
            }
            else
            {
                transform.position = position;
                transform.rotation = rotation;
            }
        }
        public void write(ref Pose pose)
        {
            pose.position = position;
            pose.rotation = rotation;

        }

        public static bool operator !=(PoseData lhs, PoseData rhs)
        {
            return !(lhs == rhs);
        }
        public Pose data => new Pose(position, rotation);
        public PoseData(Pose pose)
        {
            position = pose.position;
            rotation = pose.rotation;
        }
        public PoseData(PoseData pose)
        {
            position = Vector3.zero;
            rotation = Quaternion.identity;
        }

        public PoseData(Vector3 p, Quaternion r)
        {
            position = p;
            rotation = r;
        }

        [SerializeField]
        public Vector3 _position = Vector3.zero;
        public Vector3 position { get => _position; set => _position = value; }
        [SerializeField]
        public Quaternion _rotation = Quaternion.identity;
        public Quaternion rotation { get => _rotation; set => _rotation = value; }


        public override bool Equals(object obj)
        {
            var data = obj as TransformData;
            return data != null
                && position.Equals(data.position)
                && rotation.Equals(data.rotation);
        }

        public override int GetHashCode()
        {
            var hashCode = -1285106862;
            hashCode =
                hashCode * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(position);
            hashCode =
                hashCode * -1521134295 + EqualityComparer<Quaternion>.Default.GetHashCode(rotation);
            return hashCode;
        }
    }
}
