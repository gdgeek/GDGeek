using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace GDGeek
{
    public static class TransformUtility
    {


        public static void load(this Transform transform, TransformData local, Space type = Space.World)
        {
            local.write(ref transform, type);
        }


        private static Bounds GetBounds(BoxCollider collider)
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.transform.SetParent(collider.transform.parent);

            var scale = collider.transform.lossyScale;
            obj.transform.setGlobalScale(new Vector3(collider.size.x * scale.x, collider.size.y * scale.y, collider.size.z * scale.z));

            var position = collider.center + collider.transform.position;
            obj.transform.position = new Vector3(position.x * scale.x, position.y * scale.y, position.z * scale.z);
            obj.transform.rotation = collider.transform.rotation;
            Bounds ret = obj.GetComponent<Renderer>().bounds;
            GameObject.Destroy(obj);
            return ret;
        }


        public static void resetPosition(this Transform transform)
        {
            transform.position = Vector3.zero;
        }

        public static void resetLocalPosition(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
        }


        public static void resetRotation(this Transform transform)
        {
            transform.rotation = Quaternion.identity;
        }
        public static void resetLocalRotation(this Transform transform)
        {
            transform.localRotation = Quaternion.identity;
        }

        public static void setGlobalScale(this Transform transform, Vector3 globalScale)
        {
            bool active = transform.gameObject.activeSelf;
            transform.gameObject.SetActive(true);
            transform.localScale = Vector3.one;
            transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
            transform.gameObject.SetActive(active);
        }

        public static void resetScale(this Transform transform)
        {
            transform.setGlobalScale(Vector3.one);
        }
        public static void resetLocalScale(this Transform transform)
        {
            transform.localScale = Vector3.one;
        }

        public static void resetAll(this Transform transform)
        {
            transform.resetPosition();
            transform.resetRotation();
            transform.resetScale();
        }

        public static void setX(this Transform transform, float x)
        {
            Vector3 newPosition =
                new Vector3(x, transform.position.y, transform.position.z);

            transform.position = newPosition;
        }

        public static void setY(this Transform transform, float y)
        {
            Vector3 newPosition =
                new Vector3(transform.position.x, y, transform.position.z);

            transform.position = newPosition;
        }

        public static void setZ(this Transform transform, float z)
        {
            Vector3 newPosition =
                new Vector3(transform.position.x, transform.position.y, z);

            transform.position = newPosition;
        }

        public static void setLocalX(this Transform transform, float x)
        {
            Vector3 newPosition =
                new Vector3(x, transform.localPosition.y, transform.localPosition.z);

            transform.localPosition = newPosition;
        }

        public static void setLocalY(this Transform transform, float y)
        {
            Vector3 newPosition =
                new Vector3(transform.localPosition.x, y, transform.localPosition.z);

            transform.localPosition = newPosition;
        }

        public static void setLocalZ(this Transform transform, float z)
        {
            Vector3 newPosition =
                new Vector3(transform.localPosition.x, transform.localPosition.y, z);

            transform.localPosition = newPosition;
        }

        public static void plusX(this Transform transform, float x)
        {
            Vector3 newPosition =
                new Vector3(transform.position.x + x, transform.position.y, transform.position.z);

            transform.position = newPosition;
        }

        public static void plusY(this Transform transform, float y)
        {
            Vector3 newPosition =
                new Vector3(transform.position.x, transform.position.y + y, transform.position.z);

            transform.position = newPosition;
        }

        public static void plusZ(this Transform transform, float z)
        {
            Vector3 newPosition =
                new Vector3(transform.position.x, transform.position.y, transform.position.z + z);

            transform.position = newPosition;
        }

        public static void minusX(this Transform transform, float x)
        {
            Vector3 newPosition =
                new Vector3(transform.position.x - x, transform.position.y, transform.position.z);

            transform.position = newPosition;
        }

        public static void minusY(this Transform transform, float y)
        {
            Vector3 newPosition =
                new Vector3(transform.position.x, transform.position.y - y, transform.position.z);

            transform.position = newPosition;
        }

        public static void minusZ(this Transform transform, float z)
        {
            Vector3 newPosition =
                new Vector3(transform.position.x, transform.position.y, transform.position.z - z);

            transform.position = newPosition;
        }





    }
}
