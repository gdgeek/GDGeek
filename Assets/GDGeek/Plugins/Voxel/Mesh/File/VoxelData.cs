using UnityEngine;
using System.Collections;
using System;

namespace GDGeek{
	[Serializable]
	public struct VoxelData{
		
			public VoxelData(Vector3Int p, Color c){
			position = p;
			color = c;

		}
		public Vector3Int position;
		public Color color;

	}
}