using UnityEngine;
using System.Collections;
namespace GDGeek{


    /// <summary>
    /// 单件方法，
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    /// 

    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
		static T instance_ = null;
        static public bool IsInitialized {
            get
            {
				if (instance_ == null) {
					T[] types = FindObjectsOfType<T> ();
					if (types == null || types.Length == 0) {
						return false;
					}
					instance_ = types [0];
                    Debug.Log("!!!!!");
					return true;

				}
				return true;
            }
        }

       
      
        public virtual void OnApplicationQuit()
        {
            instance_ = null;
        }
        static private T FindOrCreateInstance() {
           
            T[] types = FindObjectsOfType<T>();
            if (types.Length > 1)
            {
                Debug.LogError("Single instance" + typeof(T).Name + " can not have multi instance!!!!");
            }



            if (types.Length == 1) {
                return types[0];
            }else
            {
                var common = GameObject.Find("Common");
                if (common == null)
                {
                    Debug.LogWarning("The Instance " + typeof(T).Name + "is not found on scene, but we will create one on Common");
                    common = new GameObject("Common");
				} 

				Debug.LogWarning("The Instance " + typeof(T).Name + "is not found on scene, but we will create one on Common");

                return common.AddComponent<T>();
            }
        }
        static public T Instance {
			get{
				if (instance_ == null) {
                    instance_ = FindOrCreateInstance();
                }
				return instance_;
			}
           
		}
	}
}