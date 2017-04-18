using UnityEngine;
using System.Collections;
using System;

namespace GDGeek{
	[Serializable]
	public struct VoxelData{
		
		public VoxelData(VectorInt3 p, Color c){
			pos = p;
			color = c;

		}
		public VectorInt3 pos;
		public Color color;

	}
}