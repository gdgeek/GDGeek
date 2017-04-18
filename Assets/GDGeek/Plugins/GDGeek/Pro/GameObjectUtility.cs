using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectUtility {

	public static void show(this GameObject obj) {
		obj.SetActive(true);	
	}

	public static void hide(this GameObject obj) {
		obj.SetActive(false);
	}

	public static bool changeActive(this GameObject obj) {
		if (obj.activeSelf)
		{
			obj.hide();
		}
		else
		{
			obj.show();
		}

		return obj.activeSelf;
	}

}
