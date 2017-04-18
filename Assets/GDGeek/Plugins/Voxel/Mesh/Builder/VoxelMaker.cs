using UnityEngine;
using System.Collections;
using System.IO;


namespace GDGeek{
	[ExecuteInEditMode]
	public class VoxelMaker : MonoBehaviour {
		public TextAsset _voxFile = null;
		public bool _building = true;
		public Material _material = null;
     

        void init ()
		{
			

			#if UNITY_EDITOR
			if(_material == null){

				_material = UnityEditor.AssetDatabase.LoadAssetAtPath<Material>("Assets/GdGeek/Media/Voxel/Material/VoxelMesh.mat");
			}

			#endif
		}
		public static Task BuildingTask(GameObject obj, System.IO.BinaryReader br, Material material, int reversal = 0){
			return new Task ();
		
		}
		public static GameObject Building(GameObject obj, System.IO.BinaryReader br, Material material, int reversal = 0)
        {

			MagicaVoxel magica = MagicaVoxelFormater.ReadFromBinary (br);
            VoxelStruct vs = VoxelStruct.Reversal(magica.vs, reversal);// VoxelBuilder.Reversal(magica.vs, reversal);
			var data = VoxelBuilderHelper.Struct2DataInCache (vs);
			var mesh = VoxelBuilder.Data2Mesh (data);
			var filter = VoxelBuilder.Mesh2Filter (mesh);

			VoxelBuilder.FilterAddRenderer (filter, material);


			filter.transform.SetParent (obj.transform);
			filter.transform.localEulerAngles = Vector3.zero;
			filter.transform.localPosition = data.offset;
			filter.gameObject.layer = obj.layer;
			filter.name = "Voxel";
			return filter.gameObject;

		}
		// Update is called once per frame
		void Update () {
			if (_building == true && _voxFile != null) {

				init();
				if (_voxFile != null) {
					VoxelMaker.Building (this.gameObject, VoxelReader.ReadFromFile (_voxFile), this._material);
				}

				_building = false;	
			}
		}
	}

}