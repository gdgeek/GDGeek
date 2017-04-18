using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


namespace GDGeek{

	public class PoolObject : MonoBehaviour {
		private Stack<GameObject> pool_ = null;
		public Stack<GameObject> pool{
			set{ 
				pool_ = value;
			}
		}
		public void OnDisable(){
			if (pool_ != null) {
				pool_.Push (this.gameObject);
			}
		}

	
	}
}
