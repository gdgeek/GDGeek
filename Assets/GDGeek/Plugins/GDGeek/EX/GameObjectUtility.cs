using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek {

    public static class GameObjectUtility {
        public static void enableBehaviour<T>(this GameObject obj) where T : MonoBehaviour
        {
            T[] list = obj.GetComponents<T>();
            foreach (T t in list)
            {
                t.enabled = true;
            }

        }

        public static void disableBehaviour<T>(this GameObject obj) where T : MonoBehaviour
        {
            T[] list = obj.GetComponents<T>();
            foreach (T t in list)
            {
                t.enabled = false;
            }

        }
        public static void destroyComponent<T>(this GameObject obj) where T : Component
        {

            T[] list = obj.GetComponents<T>();
            foreach (T t in list) {
                GameObject.DestroyImmediate(t);
            }

        }
        public static T AskComponent<T>(this GameObject obj) where T:Component  
	    {
            
		    T component = obj.GetComponent<T>();
		    if (component == null) {
			    component = obj.AddComponent<T> ();
		    }
		    return component;
	
	    }

	    public static void show(this GameObject obj) {
		    obj.SetActive(true);	
	    }

	    public static void hide(this GameObject obj) {
		    obj.SetActive(false);
	    }

        public static Renderer getRenderer(this GameObject obj)
        {
                return obj.gameObject.GetComponent<Renderer>();

        }

        public static Collider getCollider(this GameObject obj) {

             return obj.gameObject.GetComponent<Collider>();
        }
	    /*
	    public static bool changeActive(this GameObject obj) {
		    if (obj.activeSelf)
		    {
			    obj.Hide();
		    }
		    else
		    {
			    obj.Show();
		    }

		    return obj.activeSelf;
	    }*/

    }
}