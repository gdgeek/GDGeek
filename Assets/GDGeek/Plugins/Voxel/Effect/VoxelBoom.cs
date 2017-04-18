using UnityEngine;
using System.Collections;
using GDGeek;
/*
public class VoxelBoom : MonoBehaviour {
	public VoxelParticlePool _pool = null;
	public static Color RandomColor (VoxelMesh mesh){


		Color color = mesh.filter.mesh.colors[Random.Range(0,mesh.filter.mesh.colors.Length)];
		return color;
		//return Color.white;
	}

	public static Vector3 RandomPosition (VoxelMesh mesh){

		var box = mesh.collider;
		return (box.bounds.center + new Vector3(Random.Range(-box.bounds.size.x/2, box.bounds.size.x/2), 
			Random.Range(-box.bounds.size.y/2, box.bounds.size.y/2),
			Random.Range(-box.bounds.size.z/2, box.bounds.size.z/2)));

	}


	void Awake(){
		_instance = this;
		if (_pool == null) {
			_pool = this.gameObject.GetComponent<VoxelParticlePool> ();
			if (_pool == null) {
				_pool = this.gameObject.AddComponent<VoxelParticlePool> ();
			}
		}

	}
	public static VoxelBoom _instance = null;
	public static VoxelBoom GetInstance(){
		if (_instance == null) {
			GameObject obj = GameObject.Find ("Common");
			if(obj  == null){
				obj = new GameObject ("Common");
			}
			_instance = obj.AddComponent<VoxelBoom> ();
		}
		return _instance;
	}

	public void emission(VoxelMesh mesh, int count = 80, GDGeek.Tween.Method method = GDGeek.Tween.Method.easeOutQuad){
		for(int i= 0; i< count; ++i){
			GameObject obj = _pool.create();
			Voxel voxel = obj.GetComponent<Voxel>();
			obj.SetActive (true);

			VoxelProperty begin = new VoxelProperty ();
			begin.color = RandomColor(mesh);
			begin.position = RandomPosition (mesh);
			begin.scale = Vector3.one * 2.0f;

			VoxelProperty end = new VoxelProperty ();
			end.color = begin.color;
			end.position = begin.position + Random.insideUnitSphere * 100.0f;
			end.scale = Vector3.one * 4.0f;
				
			voxel.property = begin;
			Tween tween = TweenVoxel.Begin(obj, Random.Range(0.5f, 0.9f), end);
			tween.method = method;
			tween.onFinished = delegate {
				obj.SetActive(false);
			};

		}

	}

}
*/