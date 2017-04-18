using UnityEngine;
using UnityEditor;
using System.Collections;
namespace GDGeek{
	
	public class VoxelEditor : Editor {

       
        [MenuItem ("GDGeek/Voxel/Create Voxel Maker")]
		static void CreateMesh(){
			GameObject obj = new GameObject("VoxelMaker");
			obj.AddComponent<VoxelMaker> ();
            
		}
	}
}
