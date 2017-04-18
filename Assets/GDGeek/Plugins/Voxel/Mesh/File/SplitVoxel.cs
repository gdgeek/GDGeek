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
			public  VectorInt3 _min;
			public  VectorInt3 _max;
			public Box(VectorInt3 min, VectorInt3 size){
				_min = min;
				_max = min + size;
			}
			public bool contain(VectorInt3 pos){
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
		public void addBox(VectorInt3 min, VectorInt3 size){

			list_.Add (new Box (min, size));
		}
		public VoxelStruct[] doIt(){

			VoxelStruct[] vs = new VoxelStruct[list_.Count];
			for (int i = 0; i < vs.Length; ++i) {
				vs [i] = new VoxelStruct ();
			}

			for (int i = 0; i < vs_.datas.Count; ++i) {

				for (int j = 0; j < vs.Length; ++j) {

					if(list_ [j].contain (vs_.datas[i].pos)){
						vs [j].datas.Add (vs_.datas[i]);
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
