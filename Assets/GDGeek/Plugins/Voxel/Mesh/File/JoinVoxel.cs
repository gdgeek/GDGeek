using UnityEngine;
//using UnityEditor;
using GDGeek;
using System.IO;
using System.Collections.Generic;


namespace GDGeek{
	public class JoinVoxel
	{
		public struct Packed
		{
			public VoxelStruct vs;
			public VectorInt3 offset;

		}
		private Dictionary<VectorInt3, VoxelData> dictionary_ = new Dictionary<VectorInt3, VoxelData>();
		private List<Packed> list_ = new List<Packed>();

		public void addVoxel(VoxelStruct vs, VectorInt3 offset){
			Packed packed = new Packed ();
			packed.vs = vs;
			packed.offset = offset;
			list_.Add (packed);
		}
		public void clear(){
			dictionary_.Clear ();
		}
		public void readIt(Packed packed){
			for (int i = 0; i < packed.vs.datas.Count; ++i) {
				dictionary_ [packed.vs.datas [i].pos +packed.offset ] = packed.vs.datas [i];
			}

		}
		public List<VoxelData> getDatas(){
			List<VoxelData> datas = new List<VoxelData>();
			int i = 0;
			foreach(KeyValuePair<VectorInt3, VoxelData> item in dictionary_){
				VoxelData data = new VoxelData ();
				data.color = item.Value.color;
				data.pos.x = item.Key.x;
				data.pos.y = item.Key.y;
				data.pos.z = item.Key.z;

//				data.id = i;
				datas.Add (data);
				++i;
			}
			return datas;
		}
		public Task doTask(){
			Task task = new Task ();
			return task;

		}
		public VoxelStruct doIt(){

			this.clear ();
			for (int i = 0; i < list_.Count; ++i) {
				Packed p = this.list_ [i];
//				Debug.Log ("p vs data is" + p.vs.datas.Count);
//				Debug.Break ();
				this.readIt(p);
			}
			VoxelStruct vs = new VoxelStruct();
			vs.datas = this.getDatas ();
			return vs;

		}
	}
}

