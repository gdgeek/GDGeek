using System;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek
{
    [Serializable]
    public class TransformData
    {
        public TransformData() { }

       
		
        public static Vector3 R2L(Vector3 lh)
        {
            return new Vector3(-lh.x, lh.y, lh.z);
        }
        
        public static Quaternion R2LQ(Vector3 v)
        {
           
            var qx = Quaternion.AngleAxis(v.x, Vector3.right);
            var qy = Quaternion.AngleAxis(-v.y, Vector3.up);
            var qz = Quaternion.AngleAxis(-v.z, Vector3.forward);
            var qq =qx* qy * qz ;
            return qq;
           
        }


        public static Quaternion EulerToQuaternionZXY(Vector3 eulerAngles)
        {
            // 将欧拉角从度数转换为弧度，并将其减半以适应四元数计算
            float z = eulerAngles.z * Mathf.Deg2Rad * 0.5f;
            float x = eulerAngles.x * Mathf.Deg2Rad * 0.5f;
            float y = eulerAngles.y * Mathf.Deg2Rad * 0.5f;

            // 计算每个轴上的正弦和余弦值
            float cosZ = Mathf.Cos(z),
                sinZ = Mathf.Sin(z);
            float cosX = Mathf.Cos(x),
                sinX = Mathf.Sin(x);
            float cosY = Mathf.Cos(y),
                sinY = Mathf.Sin(y);

            Quaternion qq = new Quaternion(
                // 四元数的X分量
                sinX * cosY * cosZ
                    + cosX * sinY * sinZ,
                // 四元数的Y分量
                cosX * sinY * cosZ
                    - sinX * cosY * sinZ,
                // 四元数的Z分量
                cosX * cosY * sinZ
                    + sinX * sinY * cosZ,
                // 四元数的W分量（旋转量）
                cosX * cosY * cosZ
                    - sinX * sinY * sinZ
            );

            // 依据ZXY顺序计算四元数，同时保持额外的180度旋转
            return qq * Quaternion.AngleAxis(180, Vector3.forward);
        }

        public static bool operator ==(TransformData lhs, TransformData rhs)
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

        public static Vector3 NotZero(Vector3 v)
        {
            if (v.x == 0)
            {
                v.x = 0.000001f;
            }
            if (v.y == 0)
            {
                v.y = 0.000001f;
            }
            if (v.z == 0)
            {
                v.z = 0.000001f;
            }
            return v;
        }

        public void write(ref Transform transform, Space type = Space.World)
        {
            if (type == Space.Self)
            {
                transform.localPosition = position;
                transform.localRotation = rotation;
                transform.localScale = scale;
            }
            else
            {
                transform.position = position;
                transform.rotation = rotation;
                transform.setGlobalScale(scale);
            }
        }

        public static bool operator !=(TransformData lhs, TransformData rhs)
        {
            return !(lhs == rhs);
        }

        public TransformData(Transform transform, Space type = Space.World)
        {
            if (type == Space.Self)
            {
                position = transform.localPosition;
                rotation = transform.localRotation;
                scale = transform.localScale;
            }
            else
            {
                position = transform.position;
                rotation = transform.rotation;
                scale = transform.lossyScale;
            }
        }

        public TransformData(Vector3 p, Quaternion r, Vector3 s)
        {
            position = p;
            rotation = r;
            scale = s;
        }

        [SerializeField]
        public Vector3 _position = Vector3.zero;
        public Vector3 position { get =>_position; set => _position =value; }
        [SerializeField]
        public Quaternion _rotation = Quaternion.identity;
        public Quaternion rotation { get =>_rotation; set => _rotation =value; }
        [SerializeField]
        public Vector3 _scale = Vector3.one;
        public Vector3 scale
        {
            get => _scale;
            set => _scale = NotZero(value);
        }

        public override bool Equals(object obj)
        {
            var data = obj as TransformData;
            return data != null
                && position.Equals(data.position)
                && rotation.Equals(data.rotation)
                && scale.Equals(data.scale);
        }

        public override int GetHashCode()
        {
            var hashCode = -1285106862;
            hashCode =
                hashCode * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(position);
            hashCode =
                hashCode * -1521134295 + EqualityComparer<Quaternion>.Default.GetHashCode(rotation);
            hashCode =
                hashCode * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(scale);
            return hashCode;
        }
    }
}
