using UnityEngine;
using System.Collections;
using System;

namespace GDGeek{
	[Serializable]
	public struct VoxelData{
		
		public VoxelData(Vector3Int p, Color32 c){
			position = p;
			color = c;

		}
		public Vector3Int position;
		public Color32 color;

	}
}