using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


namespace GDGeek{
[Serializable]
public class VoxelHandler {
	public int id = 0;
	public Color color = Color.red;
	public List<VectorInt4> vertices = new List<VectorInt4> (); 
	public VectorInt3 position;

}
}