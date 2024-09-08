using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek { 
    
    public class Print : MonoBehaviour {
        public void log(string msg) {
            Debug.Log(msg);
        }
        public void error(string msg) {
            Debug.LogError(msg);
        }
        public void warning(string msg) {
            Debug.LogWarning(msg);
        }
    }
}