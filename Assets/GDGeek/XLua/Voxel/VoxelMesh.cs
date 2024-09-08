using UnityEngine;
using System.Collections;
//using XLua;


namespace GDGeek{
	namespace Lua{
//		[LuaCallCSharp]
		public class VoxelMesh : MonoBehaviour {

			public void load(string name){
				
				//var vs = MagicaVoxelFormater.ReadFromUrl (name).vs;
			/*	var data = VoxelBuilder.Struct2Data (vs);
				var mesh = VoxelBuilder.Data2Mesh (data);
				var filter = VoxelBuilder.Mesh2Filter (mesh);
				VoxelBuilder.FilterAddRenderer (filter, this._material);
				filter.transform.SetParent (this.transform);
				filter.transform.localEulerAngles = Vector3.zero;
				filter.transform.localPosition = data.offset;
				filter.gameObject.layer = this.gameObject.layer;
				filter.name = "Voxel";*/

				Debug.Log (Application.persistentDataPath +"/" + name);
			}
		}
	}
}
