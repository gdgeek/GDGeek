using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek
{

    public class CollidersOnOff : MonoBehaviour
    {
       
        private List<Collider> colliders_ = new List<Collider>();

        public void open()
        {
           foreach (Collider collider in colliders_)
            {
                collider.enabled = true;
            }
            colliders_.Clear();
            
        }
        public void close()
        {
            Collider[] colliders = this.GetComponentsInChildren<Collider>();
            foreach (Collider collider in colliders)
            {
                if (collider.enabled)
                {
                    colliders_.Add(collider);
                    collider.enabled = false;
                }
            }
        }

        public static void Open(GameObject obj)
        {
            CollidersOnOff collidersOnOff = obj.AskComponent<CollidersOnOff>();
            collidersOnOff.open();
        }
        public static void Close(GameObject obj)
        {
            CollidersOnOff collidersOnOff = obj.AskComponent<CollidersOnOff>();
            collidersOnOff.close();
        }
    }
}