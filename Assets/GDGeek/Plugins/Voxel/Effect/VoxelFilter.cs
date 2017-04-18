using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class VoxelFilter : MonoBehaviour {

		public virtual VoxelMeshData filter(VoxelMeshData data){
			return (VoxelMeshData)(data.Clone ());
		}
	}
}