using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek{




	public class WorldVoxel{

		public WorldVoxel(VoxelStruct vs){
			vs_ = vs;
		}
		private VoxelStruct vs_;
		public VoxelStruct vs{
			get{ 
				return vs_;
			}

		}


	}


}