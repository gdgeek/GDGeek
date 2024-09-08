using UnityEngine;
using System.Collections;
using GDGeek;

public class VoxelData2PointTask{

	static public Task Build(VoxelData2Point vd2p, VoxelProduct product){
		Task task = new Task ();
		task.init = delegate {
			vd2p.build(product);
		};
		return task;

	}
}
