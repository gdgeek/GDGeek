using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek
{
    public class Tween2StoryDistance : MonoBehaviour, IExecute
    {

        [SerializeField]
        private Transform[] _list = null;
        public void execute()
        {
            StoryDistance ss = this.gameObject.GetComponent<StoryDistance>();
            ss.storys.Clear();

            foreach (Transform transform in _list) {
                foreach (Transform child in transform)
                {
                    GameObject obj = new GameObject();
                    obj.transform.SetParent(this.transform);
                    obj.name = child.name;
                    StoryGameObjectSetActive sgosa = obj.AddComponent<StoryGameObjectSetActive>();
                    sgosa.target = child.gameObject;
                    sgosa.active = true;

                    ss.storys.Add(sgosa.gameObject);
                }

            }
          
        }
    }
}