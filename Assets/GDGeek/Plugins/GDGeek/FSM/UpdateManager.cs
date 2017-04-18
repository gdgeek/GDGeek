using UnityEngine;
using System.Collections;

public class UpdateManager : GDGeek.Singleton<UpdateManager> {

	public delegate void UpdateFunction(float d);
	public event UpdateFunction onUpdate;



	// Update is called once per frame
	void Update () {
		if (onUpdate != null) {
			onUpdate (Time.deltaTime);
		}
	}
}
