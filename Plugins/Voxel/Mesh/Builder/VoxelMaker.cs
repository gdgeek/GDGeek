using UnityEngine;
using System.Collections;
using System.IO;


namespace GDGeek{
	[ExecuteInEditMode]
	public class VoxelMaker : MonoBehaviour {
		public TextAsset _voxFile = null;
		public bool _building = true;
		public Material _material = null;
		[SerializeField]
		public float _size = 0.005f;
		void init ()
		{
			

			#if UNITY_EDITOR
			if(_material == null){

				_material = UnityEditor.AssetDatabase.LoadAssetAtPath<Material>("Assets/GdGeek/Media/Voxel/Material/VoxelMesh.mat");
			
				//_material.SetTexture();
			}
			_material.SetVector("_Tiling", new Vector2(1f/_size,1f/_size) );
			#endif
		}
		
	

		// Update is called once per frame
		void Update () {
			if (_building  && _voxFile) {

				init();
				if (_voxFile) {
					var vs = MagicaVoxelFormater.ReadFromFile (_voxFile).vs;
		
					var data = VoxelBuilder.Struct2Data (vs, _size);
					
					var mesh = VoxelBuilder.Data2Mesh (data);
					var filter = VoxelBuilder.Mesh2Filter (mesh);
					VoxelBuilder.FilterAddRenderer (filter, this._material);
					filter.transform.SetParent (this.transform);
					filter.transform.localEulerAngles = Vector3.zero;
					filter.transform.localPosition = data.offset * _size- new Vector3(0.5f, 0.5f, 0.5f);
					filter.gameObject.layer = this.gameObject.layer;
					filter.name = "Voxel";
				}
				_building = false;	
			}
		}
	}

}