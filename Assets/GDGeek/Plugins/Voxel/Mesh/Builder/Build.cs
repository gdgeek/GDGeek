using UnityEngine;
using System.Collections;
using GDGeek;
using System.Collections.Generic;
namespace GDGeek{
	public class Build  {

		static public void Run(VoxelData2Point d2p, VoxelProduct product){
			d2p.build(product);
		}
		static public Task Task(VoxelData2Point d2p, VoxelProduct product){
			Task task = new Task ();
			TaskManager.PushFront(task, delegate {
				Build.Run(d2p, product);
			});
			return task;
		}


		static public void Run(VoxelSplitSmall vss, VoxelProduct product){
			vss.build(product);
		}
		static public Task Task(VoxelSplitSmall vss, VoxelProduct product){
			Task task = new Task ();
			TaskManager.PushFront(task, delegate {
				Build.Run(vss, product);
			});
			return task;
		}

		static public void Run(VoxelMeshBuild vmb, VoxelProduct product){
			vmb.build(product);
		}
		static public Task Task(VoxelMeshBuild vmb, VoxelProduct product){

			TaskPack tp = new TaskPack (delegate() {
				if(product.sub != null){
					TaskList tl = new TaskList();

					for(int i =0; i < product.sub.Length; ++i){
						tl.push(Build.Task(vmb, product.sub[i], product.main.voxels));
					}
					return tl;
				}else{

					return Build.Task(vmb, product.main, product.main.voxels);
				}

			});
			return tp;



		}


		static public Task Task(VoxelMeshBuild vmb, VoxelProduct.Product main, Dictionary<VectorInt3, VoxelHandler> all){
			Task task = new Task ();
			TaskManager.PushFront (task, delegate {

				vmb.build(main, all);
			});
		

			return task;
		}
		static public void Run(VoxelRemoveSameVertices rsv, VoxelProduct product){
			rsv.build(product);
		}
		static public Task Task(VoxelRemoveSameVertices rsv, VoxelProduct product){
			Task task = new Task ();
			TaskManager.PushFront(task, delegate {
				rsv.build(product);
			});
			return task;
		}

		static public Task Task(VoxelRemoveFace rsv, VoxelProduct.Product main){
			Task task = new Task ();
			TaskManager.PushFront(task, delegate {
				rsv.build(main);
			});
			return task;
		}
		static public void Run(VoxelRemoveFace vrf, VoxelProduct product){
			vrf.build(product);
		}
		static public Task Task(VoxelRemoveFace vrf, VoxelProduct product){
			
			TaskPack tp = new TaskPack (delegate() {
				if(product.sub != null){
					TaskList tl = new TaskList();
					for(int i = 0; i<product.sub.Length; ++i){
						tl.push(Build.Task(vrf, product.sub[i]));
					}

					return tl;
				}else{
					
					return Build.Task(vrf, product.main);
				}

			});
			return tp;

		}




	}

}