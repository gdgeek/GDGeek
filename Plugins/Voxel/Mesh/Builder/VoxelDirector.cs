using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace GDGeek{
    /*
	public class VoxelDirectorFile{
		public static VoxelGeometry.MeshData LoadFromFile(string key){
			VoxelGeometry.MeshData data = null;
			if (GK7Zip.FileHas (key)) {
				var json = GK7Zip.GetFromFile (key);
				data = JsonUtility.FromJson<VoxelGeometry.MeshData> (json);
			}

			return data;
		}
		public static string GetKey(string md5){
			return "vs7z_" + md5;;
		}
		public static void SaveToFile(string key, VoxelGeometry.MeshData data){
			GK7Zip.SetToFile (key, JsonUtility.ToJson(data));

		}

	}
    */
	/*

	public class VoxelDirector{

		public static VoxelGeometry.MeshData BuildMeshData(VoxelStruct vs){
			VoxelProduct product = new VoxelProduct();
			VoxelData[] datas = vs.datas.ToArray ();
			Build.Run (new VoxelData2Point (datas), product);
			Build.Run (new VoxelSplitSmall (new VectorInt3(8, 8, 8)), product);
			Build.Run (new VoxelMeshBuild (), product);
			Build.Run (new VoxelRemoveSameVertices (), product);
			Build.Run (new VoxelRemoveFace (), product);
			var data = product.getMeshData ();
			return data;
		}

		public static VoxelGeometry.MeshData BuildMeshDataCache(VoxelStruct vs){

			string md5 = MagicaVoxelFormater.GetMd5 (vs);
			VoxelGeometry.MeshData data = VoxelDirectorFile.LoadFromFile (VoxelDirectorFile.GetKey(md5));

			if(data == null){
				data = BuildMeshData (vs);
				VoxelDirectorFile.SaveToFile (VoxelDirectorFile.GetKey(md5), data);
			}
			return data;
		}



		public static VoxelMesh BuildVoxelMesh(string name, VoxelGeometry.MeshData data, GameObject obj, Material material){

			VoxelFilter vf = obj.GetComponent<VoxelFilter> ();
			if (vf != null) {
				return VoxelGeometry.Draw (name, vf.filter(data), obj, material);
			} else {
				return VoxelGeometry.Draw (name, data, obj, material);
			}

		}


		public static VoxelMesh BuildVoxelMesh(string name, VoxelStruct vs, VoxelGeometry.MeshData data,  GameObject obj, Material material){

			VoxelMesh mesh = BuildVoxelMesh (name, data, obj, material);
			return mesh;

		}

		public static MeshFilter BuildMeshFilter(string name, VoxelGeometry.MeshData data,  GameObject obj, Material material){

			return VoxelGeometry.DrawOnlyMesh (name, data, obj, material);

		}

	
		public static VoxelMesh BuildVoxelMesh (VoxelStruct vs, GameObject obj, Material material)
		{

			VoxelGeometry.MeshData data = BuildMeshDataCache(vs);
			VoxelMesh mesh = BuildVoxelMesh ("Mesh", data, obj, material);
			return mesh;

		}
		public static MeshFilter BuildMeshFilter(VoxelStruct vs, GameObject obj, Material material){
			VoxelGeometry.MeshData data = BuildMeshDataCache(vs);
			MeshFilter filter = BuildMeshFilter ("Mesh", data, obj, material);
			return filter;

		}

	}




	public class VoxelDirectorTask{

		public delegate void GeometryResult(VoxelMesh geometry);
		public delegate void MeshDataResult(VoxelGeometry.MeshData data);


		public static Task BuildMeshDataTask(VoxelStruct vs, MeshDataResult result){
			VoxelProduct product = new VoxelProduct ();
			TaskList tl = new TaskList ();
			VoxelData[] datas = vs.datas.ToArray ();
			tl.push(TaskLog.Logger(Build.Task (new VoxelData2Point (datas), product),"d2p"));
			tl.push(TaskLog.Logger(Build.Task (new VoxelSplitSmall (new VectorInt3(8, 8, 8)), product),"vss"));
			tl.push(TaskLog.Logger(Build.Task (new VoxelMeshBuild (), product),"vmb"));//43%
			tl.push(TaskLog.Logger(Build.Task (new VoxelRemoveSameVertices (), product),"vrv"));
			tl.push(TaskLog.Logger(Build.Task (new VoxelRemoveFace (), product),"vrf"));//47%
			TaskManager.PushBack (tl, delegate {
				result(product.getMeshData());	
			});
			return tl;
		}
		public static Task BuildTask(string name, VoxelStruct vs, GameObject obj, Material material, GeometryResult cb){


			VoxelGeometry.MeshData data = null;
			TaskPack tp = new TaskPack(delegate(){
			//	vs.arrange ();
				string md5 = MagicaVoxelFormater.GetMd5 (vs);
				data = VoxelDirectorFile.LoadFromFile (VoxelDirectorFile.GetKey(md5));
				if(data == null){
					return BuildMeshDataTask(vs, delegate(VoxelGeometry.MeshData result) {
						data = result;
						VoxelDirectorFile.SaveToFile (VoxelDirectorFile.GetKey(md5), data);
					});
				}
				return new Task();

			});


			TaskManager.PushBack (tp, delegate {
				if(obj.GetComponent<VoxelMesh>() == null){
					obj.AddComponent<VoxelMesh>();
				}
				VoxelMesh mesh = VoxelDirector.BuildVoxelMesh(name, data, obj, material);
				//mesh.vs = vs;
				cb(mesh);
			});
			return tp;
		}

	}
*/

}