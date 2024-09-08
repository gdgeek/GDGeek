using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


namespace GDGeek{
[Serializable]
public class VoxelHandler {
	public int id = 0;
	public Color color = Color.red;
	public List<Vector4Int> vertices = new List<Vector4Int> (); 
	public Vector3Int position;

}
}