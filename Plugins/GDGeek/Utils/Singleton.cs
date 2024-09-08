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
					return true;

				}
				return true;
            }
        }

       
      
        public virtual void OnApplicationQuit()
        {
            instance_ = null;
        }
        static private T CreateInstance() {
            var common = GameObject.Find("Common");
            if (common == null)
            {
                common = new GameObject("Common");
            }

            Debug.LogWarning("The Instance " + typeof(T).Name + " is not found on scene, but we will create one on Common");

            return common.AddComponent<T>();
        }
        static private T FindInstance() {

            T[] types = FindObjectsOfType<T>();
            if (types.Length > 1)
            {
                Debug.LogError("Single instance " + typeof(T).Name + " can not have multi instance!!!!");
            }
            
            



            if (types.Length >= 1)
            {
                return types[0];
            }
            return null;
        }
        static private T FindOrCreateInstance() {
            T t = FindInstance();
            if (t == null)
            {
                return CreateInstance();
            }
            else {
                return t;
            }
          
        }

        public static T Create(string name)
        {
            if (IsInitialized)
            {
                return Instance;
            }
            else {

                GameObject obj = new GameObject(name);
                return obj.AddComponent<T>();
            }
        }
        static public T GetOrCreateInstance {
            get
            {
                if (instance_ == null)
                {
                    instance_ = FindOrCreateInstance();
                }
                return instance_;
            }

        }
        static public T Instance {
			get{
                if (instance_ == null)
                {
                    instance_ = FindInstance();
                }
                return instance_;
			}
           
		}
	}
}