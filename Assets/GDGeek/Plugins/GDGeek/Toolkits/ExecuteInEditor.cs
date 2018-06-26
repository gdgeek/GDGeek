using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek { 
    [ExecuteInEditMode]
    public class ExecuteInEditor : MonoBehaviour {
        public bool _execute;

	    // Update is called once per frame
	    void Update () {
            if (_execute) {
                IExecute[] executes = this.gameObject.GetComponents<IExecute>();
                foreach (var exe in executes) {
                    exe.execute();
                }

                _execute = false;
            }
	    }
    }
}