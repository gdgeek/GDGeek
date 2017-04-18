using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class VoxelFilterGlow : VoxelFilter {

		public override VoxelMeshData filter(VoxelMeshData data){
			VoxelMeshData clone = (VoxelMeshData)data.Clone ();
			for (int i = 0; i < clone.colors.Count; ++i) {

				Color c = clone.colors [i];
				Vector2 uv = Vector2.zero;

				uv.x =1.0f;

				clone.uvs [i] = uv;
			}
			return clone;
		}
	}
}