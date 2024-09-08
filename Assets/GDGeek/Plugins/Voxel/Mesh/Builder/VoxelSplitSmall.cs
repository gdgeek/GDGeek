using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GDGeek
{
	public class VoxelSplitSmall {
		private Vector3Int box_;
	
		public VoxelSplitSmall(Vector3Int box){
			box_ = box;
		}
		public void build(VoxelProduct product){
			Dictionary<Vector3Int, Dictionary<Vector3Int, VoxelHandler> > dict = new Dictionary<Vector3Int, Dictionary<Vector3Int, VoxelHandler> >();
			foreach (var kv in product.main.voxels) {
				Vector3Int offset = new Vector3Int ();
				offset.x = kv.Key.x/ box_.x;
				offset.y = kv.Key.y/ box_.y;
				offset.z = kv.Key.z/ box_.z;
				if (!dict.ContainsKey (offset)) {
					dict [offset] = new Dictionary<Vector3Int, VoxelHandler> ();
				}
				dict [offset].Add (kv.Key, kv.Value);
			}
			List<VoxelProduct.Product> list = new List<VoxelProduct.Product>();
			foreach (var o in dict) {
				var p = new VoxelProduct.Product ();
				p.voxels = o.Value;
				list.Add (p);
			
			}
			product.sub = list.ToArray ();
		}
		public Task task(VoxelProduct product){
			Task task = new Task ();
			TaskManager.PushFront (task, delegate {
				build(product);	
			});
			return task;
		}
	
	}
}