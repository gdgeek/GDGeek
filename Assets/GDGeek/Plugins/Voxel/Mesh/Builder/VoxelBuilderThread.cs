using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;


namespace GDGeek{
	public class VoxelBuilderThread : Singleton<VoxelBuilderThread> {

		private VoxelStruct vs_;
		private Action<VoxelProduct> ret_;
		public void product(){
			VoxelProduct product = new VoxelProduct();
			Debug.Log ("!??!");
			VoxelData[] datas = VoxelBuilderThread.Instance.vs_.toArray ();
			Build.Run (new VoxelData2Point (datas), product);
			Build.Run (new VoxelSplitSmall (new Vector3Int(8, 8, 8)), product);
			Build.Run (new VoxelMeshBuild (), product);
			Build.Run (new VoxelRemoveSameVertices (), product);
			Build.Run (new VoxelRemoveFace (), product);
			Build.Run (new VoxelRemoveSameVertices (), product);
			Debug.Log ("!!!!");
			ret_ (product);
			//var data = product.getMeshData ();
			//VoxelBuilderThread.Instance.ret_ (data);
		
		}
		public void struct2Data(VoxelStruct vs, Action<VoxelMeshData> ret){

			ret_ = delegate(VoxelProduct product) {
				ret (product.getMeshData ());
			};
			vs_ = vs;


		
		
		}


	}
}