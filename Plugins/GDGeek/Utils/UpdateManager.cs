using System;
using UnityEngine;
using System.Collections;
namespace GDGeek { 
    public class UpdateManager : GDGeek.Singleton<UpdateManager> {

	  //  public delegate void UpdateFunction(float d);
		public Action<float> onUpdate { get; set; }

	    // Update is called once per frame
	    void Update () {
		    onUpdate?.Invoke(Time.deltaTime);
	    }
    }
}