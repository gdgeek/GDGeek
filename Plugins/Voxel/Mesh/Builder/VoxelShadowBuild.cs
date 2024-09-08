
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GDGeek
{
	/*
	public class VoxelShadowBuild: IVoxelBuilder
	{
		private Dictionary<VectorInt3, VoxelHandler> voxels = null;

		public void init(){
			voxels = null;
		}
		private void shadowTest(VoxelHandler handler, VectorInt3 shadow, VectorInt3 light, byte index, Vector3 face){
			
			if (voxels.ContainsKey (handler.position + shadow)) {
				handler.shadowAdd (index, face);			
			} else 	if (!voxels.ContainsKey (handler.position + light)) {
				handler.lightAdd (index, face);			
			}
		}

		public void build(VoxelProduct product){
			voxels = product.main.voxels;
			foreach (KeyValuePair<VectorInt3, VoxelHandler> kv in voxels) {
				kv.Value.lightShadowBegin ();


				shadowTest (kv.Value, new VectorInt3 (-1, -1, 1), new VectorInt3 (-1, 0, 1), 0, Vector3.back);
				shadowTest (kv.Value, new VectorInt3 (0, -1, 1), new VectorInt3 (0, 0, 1), 1, Vector3.back);
				shadowTest (kv.Value, new VectorInt3 (1, -1, 1), new VectorInt3 (1, 0, 1), 2, Vector3.back);


				shadowTest (kv.Value, new VectorInt3 (-1, -1, 0), new VectorInt3 (-1, 0, 0), 3, Vector3.back);
				shadowTest (kv.Value, new VectorInt3 (1, -1, 0), new VectorInt3 (1, 0, 0), 4, Vector3.back);


				shadowTest (kv.Value, new VectorInt3 (-1, -1, -1), new VectorInt3 (-1, 0, -1), 5, Vector3.back);
				shadowTest (kv.Value, new VectorInt3 (0, -1, -1), new VectorInt3 (0, 0, -1), 6, Vector3.back);
				shadowTest (kv.Value, new VectorInt3 (1, -1, -1), new VectorInt3 (1, 0, -1), 7, Vector3.back);

				//=========================



				shadowTest (kv.Value, new VectorInt3 (-1, 1, -1), new VectorInt3 (-1, 0, -1), 0, Vector3.forward);
				shadowTest (kv.Value, new VectorInt3 (0, 1, -1), new VectorInt3 (0, 0, -1), 1, Vector3.forward);
				shadowTest (kv.Value, new VectorInt3 (1, 1, -1), new VectorInt3 (1, 0, -1), 2, Vector3.forward);


				shadowTest (kv.Value, new VectorInt3 (-1, 1, 0), new VectorInt3 (-1, 0, 0), 3, Vector3.forward);
				shadowTest (kv.Value, new VectorInt3 (1, 1, 0), new VectorInt3 (1, 0, 0), 4, Vector3.forward);


				shadowTest (kv.Value, new VectorInt3 (-1, 1, 1), new VectorInt3 (-1, 0, 1), 5, Vector3.forward);
				shadowTest (kv.Value, new VectorInt3 (0, 1, 1), new VectorInt3 (0, 0, 1), 6, Vector3.forward);
				shadowTest (kv.Value, new VectorInt3 (1, 1, 1), new VectorInt3 (1, 0, 1), 7, Vector3.forward);
				//=========================


				shadowTest (kv.Value, new VectorInt3 (-1, -1, -1), new VectorInt3 (-1, -1, 0), 0, Vector3.down);
				shadowTest (kv.Value, new VectorInt3 (0, -1, -1), new VectorInt3 (0, -1, 0), 1, Vector3.down);
				shadowTest (kv.Value, new VectorInt3 (1, -1, -1), new VectorInt3 (1, -1, 0), 2, Vector3.down);

				shadowTest (kv.Value, new VectorInt3 (-1, 0, -1), new VectorInt3 (-1, 0, 0), 3, Vector3.down);
				shadowTest (kv.Value, new VectorInt3 (1, 0, -1), new VectorInt3 (1, 0, 0), 4, Vector3.down);

				shadowTest (kv.Value, new VectorInt3 (-1, 1, -1), new VectorInt3 (-1, 1, 0), 5, Vector3.down);
				shadowTest (kv.Value, new VectorInt3 (0, 1, -1), new VectorInt3 (0, 1, 0), 6, Vector3.down);
				shadowTest (kv.Value, new VectorInt3 (1, 1, -1), new VectorInt3 (1, 1, 0), 7, Vector3.down);



				shadowTest (kv.Value, new VectorInt3 (-1, 1, 1), new VectorInt3 (-1, 1, 0), 0, Vector3.up);
				shadowTest (kv.Value, new VectorInt3 (0, 1, 1), new VectorInt3 (0, 1, 0), 1, Vector3.up);
				shadowTest (kv.Value, new VectorInt3 (1, 1, 1), new VectorInt3 (1, 1, 0), 2, Vector3.up);

				shadowTest (kv.Value, new VectorInt3 (-1, 0, 1), new VectorInt3 (-1, 0, 0), 3, Vector3.up);
				shadowTest (kv.Value, new VectorInt3 (1, 0, 1), new VectorInt3 (1, 0, 0), 4, Vector3.up);

				shadowTest (kv.Value, new VectorInt3 (-1, -1, 1), new VectorInt3 (-1, -1, 0), 5, Vector3.up);
				shadowTest (kv.Value, new VectorInt3 (0, -1, 1), new VectorInt3 (0, -1, 0), 6, Vector3.up);
				shadowTest (kv.Value, new VectorInt3 (1, -1, 1), new VectorInt3 (1, -1, 0), 7, Vector3.up);



				//=========================



				shadowTest (kv.Value, new VectorInt3 (1, 1, -1), new VectorInt3 (0, 1, -1), 0, Vector3.left);
				shadowTest (kv.Value, new VectorInt3 (1, 0, -1), new VectorInt3 (0, 0, -1), 1, Vector3.left);
				shadowTest (kv.Value, new VectorInt3 (1, -1, -1), new VectorInt3 (0, -1, -1), 2, Vector3.left);


				shadowTest (kv.Value, new VectorInt3 (1, 1, 0), new VectorInt3 (0, 1, 0), 3, Vector3.left);
				shadowTest (kv.Value, new VectorInt3 (1, -1, 0), new VectorInt3 (0, -1, 0), 4, Vector3.left);


				shadowTest (kv.Value, new VectorInt3 (1, 1, 1), new VectorInt3 (0, 1, 1), 5, Vector3.left);
				shadowTest (kv.Value, new VectorInt3 (1, 0, 1), new VectorInt3 (0, 0, 1), 6, Vector3.left);
				shadowTest (kv.Value, new VectorInt3 (1, -1, 1), new VectorInt3 (0, -1, 1), 7, Vector3.left);



				//=========================


				shadowTest (kv.Value, new VectorInt3 (-1, -1, -1), new VectorInt3 (0, -1, -1), 0, Vector3.right);
				shadowTest (kv.Value, new VectorInt3 (-1, 0, -1), new VectorInt3 (0, 0, -1), 1, Vector3.right);
				shadowTest (kv.Value, new VectorInt3 (-1, 1, -1), new VectorInt3 (0, 1, -1), 2, Vector3.right);


				shadowTest (kv.Value, new VectorInt3 (-1, -1, 0), new VectorInt3 (0, -1, 0), 3, Vector3.right);
				shadowTest (kv.Value, new VectorInt3 (-1, 1, 0), new VectorInt3 (0, 1, 0), 4, Vector3.right);

				shadowTest (kv.Value, new VectorInt3 (-1, -1, 1), new VectorInt3 (0, -1, 1), 5, Vector3.right);
				shadowTest (kv.Value, new VectorInt3 (-1, 0, 1), new VectorInt3 (0, 0, 1), 6, Vector3.right);
				shadowTest (kv.Value, new VectorInt3 (-1, 1, 1), new VectorInt3 (0, 1, 1), 7, Vector3.right);

				

				kv.Value.lightShadowEnd ();
			}
		}
	}
*/

}