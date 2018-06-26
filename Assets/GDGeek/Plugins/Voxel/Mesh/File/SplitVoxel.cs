using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


namespace GDGeek{
	public class SplitVoxel {
		[Serializable]
		public class Box{

			public override int GetHashCode()
			{
				return _min.GetHashCode() ^ _max.GetHashCode();
			}
			public  Vector3Int _min;
			public  Vector3Int _max;
			public Box(Vector3Int min, Vector3Int size){
				_min = min;
				_max = min + size;
			}
			public bool contain(Vector3Int pos){
				if (pos.x >= _min.x &&
					pos.y >= _min.y &&
					pos.z >= _min.z &&
					pos.x < _max.x &&
					pos.y < _max.y &&
					pos.z < _max.z 
				) {
					return true;
				}
					
				return false;
			}
		};
		private VoxelStruct vs_ = null;
		private List<Box> list_ = new List<Box>();
		public SplitVoxel(VoxelStruct vs){
			vs_ = vs;
		}
		public void addBox(Box box){
		
			list_.Add (box);
		
		}
		public void addBoxes(List<Box> boxes){
			list_ = boxes;
		}
		public void addBox(Vector3Int min, Vector3Int size){

			list_.Add (new Box (min, size));
		}
		public VoxelStruct[] doIt(){

			VoxelStruct[] vs = new VoxelStruct[list_.Count];
			for (int i = 0; i < vs.Length; ++i) {
				vs [i] = new VoxelStruct ();
			}

			for (int i = 0; i < vs_.count; ++i) {

				var data = vs_.getData (i);
				for (int j = 0; j < vs.Length; ++j) {
					
					if(list_ [j].contain (data.position)){
						vs [j].addData (data);
					}
					/*//vs[j]*/
				}
			}
			//for (int i = 0; i < vs.Length; ++i) {
		//		vs [i].arrange(true);
		//	}
			return vs;
		}
	}
}
