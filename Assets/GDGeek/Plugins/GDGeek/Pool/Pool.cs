using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


namespace GDGeek{


	public class Pool{

		private GameObject prototype_ = null;


		private Stack<GameObject> pool_ = new Stack<GameObject>();

		public void init(GameObject prototype, int reserve = 1){
			
			prototype_ = prototype;
			prototype_.gameObject.SetActive (false);
			if (prototype_.GetComponent<PoolObject> () == null) {
				prototype_.AddComponent<PoolObject> ();
			}
			GameObject[] list = new GameObject[reserve];
			for (int i =0; i <reserve; ++i) {
				list[i] = this.create();	
				list[i].SetActive(true);	
			}
			for (int i =0; i <reserve; ++i) {
				list[i].SetActive(false);		
			}

		}
		public GameObject create(){
			GameObject obj = null;
			if (pool_.Count == 0) {
				
				obj = GameObject.Instantiate (prototype_);
				PoolObject gp = obj.GetComponent<PoolObject> ();
				obj.transform.parent = prototype_.transform.parent;
				gp.pool = this.pool_;
			} else {
				obj = pool_.Pop ();
			}
			return obj;
		}  
	}
}