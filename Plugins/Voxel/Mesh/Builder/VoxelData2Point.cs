using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GDGeek
{
	public class VoxelData2Point
	{
		private List<VoxelData> data_ = null;

		private bool adjust_ = false;
	
		public VoxelData2Point(){
		}


		public VoxelData2Point(List<VoxelData> data){
			data_ = data;
		}
		

		private VoxelHandler data2Handler (VoxelData data)
		{

			VoxelHandler handler = new VoxelHandler();

			handler.position = new Vector3Int(data.position.x, data.position.y, data.position.z);
			handler.color = data.color;

			return handler;

		}
		public Task task(VoxelProduct product){
			Task task = new Task ();
			task.init = delegate {
				build(product);
			};
			return task;
		}
		public void build(VoxelProduct product){

			product.min = new Vector3Int(999, 999, 999);
			product.max = new Vector3Int(-999, -999, -999);
			product.main.voxels = new Dictionary<Vector3Int, VoxelHandler>();

			for (int i=0; i<data_.Count; ++i) {
				VoxelData d = data_ [i];
				var min = product.min;
				var max = product.max;

				min.x = Mathf.Min (min.x, d.position.x);
				min.y = Mathf.Min (min.y, d.position.y);
				min.z = Mathf.Min (min.z, d.position.z);
				max.x = Mathf.Max (max.x, d.position.x);
				max.y = Mathf.Max (max.y, d.position.y);
				max.z = Mathf.Max (max.z, d.position.z);
				product.min = min;
				product.max = max;

			}
			for (int i=0; i<data_.Count; ++i) {
				VoxelHandler handler = data2Handler(data_[i]);
				product.main.voxels.Add (handler.position, handler);
			}

		}
/*
		private VoxelHandler data2HandlerAdjust(VoxelData data, Vector3Int min, Vector3Int max)
		{
			VoxelHandler handler = new VoxelHandler();
			handler.position = new Vector3Int(
					(max.x - data.position.x) + min.x,
					data.position.y,
					(max.z - data.position.z) + min.z
				);
			handler.color = data.color;
			return handler;
		}*/
	}


}