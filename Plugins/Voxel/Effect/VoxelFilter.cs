using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class VoxelFilter : MonoBehaviour {

		public virtual VoxelGeometry.MeshData filter(VoxelGeometry.MeshData data){
			return (VoxelGeometry.MeshData)(data.Clone ());
		}
	}
}