using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek{
	public class VoxelTaskBuilder : MonoBehaviour {

		//public delegate void TaskInit();
		public delegate void Struct2DataReturn(VoxelMeshData data);
		public static Task Struct2Data(VoxelStruct vs, Struct2DataReturn ret){
			TaskList tl = new TaskList ();
			VoxelProduct product = new VoxelProduct();
			VoxelData[] datas = vs.datas.ToArray ();
			tl.push(Build.Task (new VoxelData2Point (datas), product));
			tl.push(Build.Task (new VoxelSplitSmall (new VectorInt3(8, 8, 8)), product));
			tl.push(Build.Task (new VoxelMeshBuild (), product));
			tl.push(Build.Task (new VoxelRemoveSameVertices (), product));
			tl.push(Build.Task (new VoxelRemoveFace (), product));
			tl.push(Build.Task (new VoxelRemoveSameVertices (), product));
			TaskManager.PushBack (tl, delegate {
				ret(product.getMeshData ());
			});

			return tl;

		}





		public delegate void Data2MeshReturn(Mesh mesh);
		public static Task Data2Mesh(VoxelMeshData data, Data2MeshReturn ret){//创建mesh
			Task task = new Task();
			TaskManager.PushFront (task, delegate() {
				ret(VoxelBuilder.Data2Mesh(data));
			});
			return task;
		}


		public delegate void Mesh2FilterReturn(MeshFilter filter);
		public static Task Mesh2Filter(Mesh mesh, Mesh2FilterReturn ret){
			Task task = new Task();
			TaskManager.PushFront (task, delegate() {
				ret(VoxelBuilder.Mesh2Filter(mesh));
			});
			return task;
		}

		public delegate void FilterAddRendererReturn(MeshRenderer filter);
		public static Task FilterAddRenderer(MeshFilter filter, Material material, FilterAddRendererReturn ret){
			Task task = new Task();
			TaskManager.PushFront (task, delegate() {
				ret(VoxelBuilder.FilterAddRenderer(filter, material));

			});
			return task;
		
		}

		public delegate void FilterAddColliderReturn(MeshCollider filter);
		public static Task FilterAddCollider(MeshFilter filter, FilterAddColliderReturn ret){

			Task task = new Task();
			TaskManager.PushFront (task, delegate() {
				ret(VoxelBuilder.FilterAddCollider(filter));

			});
			return task;


		}


	}
	public class VoxelTaskBuilderHelper{
		/*public static VoxelMeshData Struct2DataInCache(VoxelStruct vs){
			return VoxelBuilder.Struct2Data(vs);

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
		}*/


	}
}
