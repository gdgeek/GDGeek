using UnityEngine;
using System.Collections;
using System;
using System.Text;

namespace GDGeek{
	
	public class VoxelBuilder{

      


        public static VoxelMeshData Struct2Data(VoxelStruct vs){
			VoxelProduct product = new VoxelProduct();
			VoxelData[] datas = vs.datas.ToArray ();
			Build.Run (new VoxelData2Point (datas), product);
			Build.Run (new VoxelSplitSmall (new VectorInt3(8, 8, 8)), product);
			Build.Run (new VoxelMeshBuild (), product);
			Build.Run (new VoxelRemoveSameVertices (), product);
			Build.Run (new VoxelRemoveFace (), product);
			Build.Run (new VoxelRemoveSameVertices (), product);
			var data = product.getMeshData ();
			return data;
		}

       
        public static Mesh Data2Mesh(VoxelMeshData data){//创建mesh

			Mesh mesh = new Mesh();
			mesh.name = "ScriptedMesh";
			mesh.SetVertices (data.vertices);
			mesh.SetColors (data.colors);

			mesh.SetUVs (0, data.uvs);
			mesh.SetTriangles(data.triangles, 0);
			mesh.RecalculateNormals();

			return mesh;
		}
		public static MeshFilter Mesh2Filter(Mesh mesh){
			GameObject obj = new GameObject();
			MeshFilter filter = obj.AddComponent<MeshFilter>();
			filter.sharedMesh = mesh;

			return filter;
		}

		public static MeshRenderer FilterAddRenderer(MeshFilter filter, Material material){
			MeshRenderer renderer = filter.gameObject.AddComponent<MeshRenderer>();
			renderer.material = material;
			return renderer;
		}

		public static MeshCollider FilterAddCollider(MeshFilter filter){
			MeshCollider collider = filter.gameObject.AddComponent<MeshCollider>();
			collider.sharedMesh = filter.mesh;
			return collider;

		}

		
	}


	public class VoxelBuilderHelper{

		public static string GetKey(string md5){
			return "gzip_" + md5;
		}

		public static VoxelMeshData LoadFromCache(string key){


			VoxelMeshData data = null;
			if (Cache.HasKey (key)) {
				string json = Encoding.UTF8.GetString(ZipFile.Decompressed((Cache.GetBytes (key))));
				data = JsonUtility.FromJson<VoxelMeshData> (json);
			}

			return data;
		}
		public static void SaveToCache(string key, VoxelMeshData data){
			string json = JsonUtility.ToJson (data);
			GDGeek.Cache.SetBytes (key, GDGeek.ZipFile.Compression(Encoding.UTF8.GetBytes(json)));
			//GK7Zip.SetToFile (key, JsonUtility.ToJson(data));
			//JsonUtility.ToJson(data)
		}

		public static VoxelMeshData Struct2DataInCache(VoxelStruct vs){


			Debug.Log ("!!!!");
			string md5 = MagicaVoxelFormater.GetMd5 (vs);
			VoxelMeshData data = LoadFromCache (GetKey(md5));

			if(data == null){

				Debug.Log ("???");
				data =  VoxelBuilder.Struct2Data(vs);
				SaveToCache (GetKey(md5), data);
			}
			return data;
           
        
        }
		public static MeshFilter Struct2Filter(VoxelStruct vs){
			var data = Struct2DataInCache (vs);
			var mesh = VoxelBuilder.Data2Mesh (data);
			var filter = VoxelBuilder.Mesh2Filter (mesh);
			return filter;
		}

		public static MeshFilter Data2Filter(VoxelMeshData data){
			var mesh = VoxelBuilder.Data2Mesh (data);
			var filter = VoxelBuilder.Mesh2Filter (mesh);
			return filter;
		}

	}
}
