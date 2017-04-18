using UnityEngine;
using System.Collections;

public class VoxelProperty{

	
	public Vector3 position = Vector3.zero;
	public Color color = Color.white;
	public Vector3 scale = Vector3.one;

	public VoxelProperty add (VoxelProperty prop)
	{
		VoxelProperty ret = new VoxelProperty();
		ret.position = this.position + prop.position;
		ret.color = this.color + prop.color;
		ret.scale = this.scale + prop.scale;
		return ret;
	
	}

	public VoxelProperty mul (float f)
	{
		VoxelProperty ret = new VoxelProperty();
		ret.position = this.position * f;
		ret.color = this.color * f;
		ret.scale = this.scale * f;
		return ret;
	}


}
