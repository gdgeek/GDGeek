using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GDGeek
{
	public class VoxelSplitSmall {
		private VectorInt3 box_;
	
		public VoxelSplitSmall(VectorInt3 box){
			box_ = box;
		}
		public void build(VoxelProduct product){
			Dictionary<VectorInt3, Dictionary<VectorInt3, VoxelHandler> > dict = new Dictionary<VectorInt3, Dictionary<VectorInt3, VoxelHandler> >();
			foreach (var kv in product.main.voxels) {
				VectorInt3 offset = new VectorInt3 ();
				offset.x = kv.Key.x/ box_.x;
				offset.y = kv.Key.y/ box_.y;
				offset.z = kv.Key.z/ box_.z;
				if (!dict.ContainsKey (offset)) {
					dict [offset] = new Dictionary<VectorInt3, VoxelHandler> ();
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