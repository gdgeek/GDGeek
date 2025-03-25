using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GDGeek{
	
	public class VoxelBuilder{
		public static VoxelGeometry.MeshData Struct2Data(VoxelStruct vs, float size){
			VoxelProduct product = new VoxelProduct();
			List<VoxelData> datas = vs.datas;
			product.offset = new Vector3(0.5f, 0.5f, 0.5f);
			product.size = size;
			Build.Run (new VoxelData2Point (datas), product);
			Build.Run (new VoxelMeshBuild (), product);
			Build.Run (new VoxelSimplify (), product);
			

			var data = product.getMeshData ();
			return data;
		}

		//private int i = 0;
		public static Mesh Data2Mesh(VoxelGeometry.MeshData data){//创建mesh

			Mesh mesh = new Mesh();
			mesh.name = "ScriptedMesh";
			mesh.SetVertices (data.vertices);
			mesh.SetNormals(data.normals);
			mesh.SetColors(data.colors);
			mesh.SetUVs (0, data.uvs);
		
			mesh.SetTriangles(data.triangles, 0);
			//mesh.RecalculateNormals();
			mesh.RecalculateTangents();
			mesh.RecalculateBounds();
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

		public static MeshCollider FilterAddCollider(MeshFilter filter, int layerOverridePriority = 0){
			MeshCollider collider = filter.gameObject.AddComponent<MeshCollider>();
			collider.layerOverridePriority = layerOverridePriority;
			collider.sharedMesh = filter.mesh;
			return collider;

		}

		
	}
	public class VoxelBuilderHelper{
		public static VoxelGeometry.MeshData Struct2DataInCache(VoxelStruct vs){
            return VoxelBuilder.Struct2Data(vs, 0.005f);
            
        }
		public static MeshFilter Struct2Filter(VoxelStruct vs){
			var data = Struct2DataInCache (vs);
			var mesh = VoxelBuilder.Data2Mesh (data);
			var filter = VoxelBuilder.Mesh2Filter (mesh);
			return filter;
		}

		public static MeshFilter Data2Filter(VoxelGeometry.MeshData data){
			var mesh = VoxelBuilder.Data2Mesh (data);
			var filter = VoxelBuilder.Mesh2Filter (mesh);
			return filter;
		}
/*		public static AddFilterToGameObject(Transform parent, Transform son, VoxelGeometry.MeshData data){
			
		}*/
	}
}
