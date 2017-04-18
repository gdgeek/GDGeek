using UnityEngine;
using System.Collections;
using GDGeek;
using System;
using System.Collections.Generic;

public class VoxelFilterColor : VoxelFilter {
	[Serializable]
	public struct Mapped{
		public Color key;
		public Color color;
		public float glow;

	}
	public float _deviation = 0.01f;
	public Mapped[] _colors;
	private Dictionary<Color, Mapped> map_ = null;


	private bool colorComp(Color ac, Color bc, float distance) 
	{ 
		/*float r = ac.r - bc.r; 
		float g = ac.g - bc.g; 
		float b = ac.b - bc.b; 
*/
		if( Vector3.Distance(new Vector3(ac.r, ac.g, ac.b),new Vector3(bc.r, bc.g, bc.b)) <distance) 
			return true; 
		return false; 
	}


	public override VoxelMeshData filter(VoxelMeshData data){
		
		VoxelMeshData ret = (VoxelMeshData)(data.Clone ());
		//Debug.Log ("!!1");
		for (int j = 0; j < ret.colors.Count; ++j) {
			for (int i = 0; i < _colors.Length; ++i) {
				if (colorComp (_colors [i].key, ret.colors [j], _deviation)) {
					ret.colors [j] = _colors [i].color;
					ret.uvs [j] = new Vector2(_colors [i].glow, 0.0f);
				}
			}
		}
		return ret;
	}
}
