using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
		static private T instance_ = null;
		static public T Instance {
			get{
				if (instance_ == null) {
					if (FindObjectsOfType<T>().Length > 1) {
						Debug.LogError ("Single instance"+ typeof(T).Name +" can not have multi instance!!!!");
					}
					instance_ = FindObjectOfType<T> ();
					if (instance_ == null) {
						var common = GameObject.Find ("Common");
						if (common == null) {
							Debug.LogWarning ("The Instance " + typeof(T).Name + "is not found on scene, but we will create one on Common");
							common = new GameObject ("Common");
						}
						instance_ = common.AddComponent<T> ();
					}
				}
				return instance_;
			}
		}
	}
}