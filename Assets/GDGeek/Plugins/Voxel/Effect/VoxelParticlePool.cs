using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class VoxelParticlePool : MonoBehaviour {
		private Pool pool_ = new Pool();
		public Material _material;

		private static VoxelParticlePool instance_ = null;

		public static VoxelParticlePool GetInstance(){
			return VoxelParticlePool.instance_;
		}
		
		private GameObject createPrototype(){

			GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
			cube.transform.parent = this.transform;
			Voxel vox = cube.AddComponent<Voxel> ();
			MeshRenderer renderer = cube.GetComponent<MeshRenderer> ();

			renderer.material = _material;
			return cube;
		}

		void Awake(){
			VoxelParticlePool.instance_ = this;
			pool_.init (createPrototype());

		}
		public GameObject create(){
			return pool_.create ();
		}

	}
}