using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek { 
    public class ExecuteInUpdate : MonoBehaviour {

        private IExecute[] executes_;
	    // Use this for initialization
	    void Start () {
            executes_ = this.gameObject.GetComponents<IExecute>();

        }
	
	    // Update is called once per frame
	    void LateUpdate () {
            foreach (IExecute e in executes_) {
                e.execute();
            }
	    }
    }
}