using UnityEngine;
using System.Collections;
namespace GDGeek{


    /// <summary>
    /// 单件方法，
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    /// 

    public abstract class Location<S, T> : MonoBehaviour 
    where S : Singleton<S>
    where T : Location<S, T>
    {

       
        static public T Instance {
            get{
                if (Singleton<S>.Instance == null) {
                    return null;
                }
                return Singleton<S>.Instance.gameObject.GetComponentInChildren<T>();
            }
           
        }
    }
}