using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GDGeek
{
	public class VoxelMeshBuild 
	{

		private void addVertix (VoxelProduct.Product main, Vector3 p, Color c, Vector3 normal){
			VoxelDrawData.Vertice v = new VoxelDrawData.Vertice ();
			v.position = p;
			v.normal = normal;
			v.color = c;
			main.draw.vertices.Add (v);

		}
		private void addRect(VoxelProduct.Product main, Vector3 direction, VectorInt3 position, Color color){
			
			Vector3 offset = new Vector3(position.x, position.y, position.z);

			main.draw.triangles.Add (main.draw.vertices.Count + 0);
			main.draw.triangles.Add (main.draw.vertices.Count + 1);
			main.draw.triangles.Add (main.draw.vertices.Count + 2);
			main.draw.triangles.Add (main.draw.vertices.Count + 1);
			main.draw.triangles.Add (main.draw.vertices.Count + 3);
			main.draw.triangles.Add (main.draw.vertices.Count + 2);

		

			Vector3 p0 = new Vector3 ();//
			Vector3 p1 = new Vector3 ();//
			Vector3 p2 = new Vector3 ();//
			Vector3 p3 = new Vector3 ();//
			if (direction == Vector3.up) {
				
				p0 =  (new Vector3 (-0.5f, 0.5f, 0.5f) + offset);
				p1 =  (new Vector3 (0.5f, 0.5f, 0.5f) + offset);
				p2 =  (new Vector3 (-0.5f, 0.5f, -0.5f) + offset);
				p3 =  (new Vector3 (0.5f, 0.5f, -0.5f) + offset);

			}if (direction == Vector3.down) {
				p0 =  (new Vector3 (-0.5f, -0.5f, -0.5f) + offset);
				p1 =  (new Vector3 (0.5f, -0.5f, -0.5f) + offset);
				p2 =  (new Vector3 (-0.5f, -0.5f, 0.5f) + offset);
				p3 = (new Vector3 (0.5f, -0.5f, 0.5f) + offset);

			}else if (direction == Vector3.back) {
				p0 =  (new Vector3 (-0.5f, 0.5f, -0.5f) + offset);
				p1 =  (new Vector3 (0.5f, 0.5f, -0.5f) + offset);
				p2 =  (new Vector3 (-0.5f, -0.5f, -0.5f) + offset);
				p3 =  (new Vector3 (0.5f, -0.5f, -0.5f) + offset);


			}else if (direction == Vector3.forward) {
				p0 =  (new Vector3 (-0.5f, -0.5f, 0.5f) + offset);
				p1 =  (new Vector3 (0.5f, -0.5f, 0.5f) + offset);
				p2 =  (new Vector3 (-0.5f, 0.5f, 0.5f) + offset);
				p3 =  (new Vector3 (0.5f, 0.5f, 0.5f) + offset);


			}else if (direction == Vector3.left) {
				p0 =  (new Vector3 (0.5f, -0.5f, 0.5f) + offset);
				p1 =  (new Vector3 (0.5f, -0.5f, -0.5f) + offset);
				p2 =  (new Vector3 (0.5f, 0.5f, 0.5f) + offset);
				p3 =  (new Vector3 (0.5f, 0.5f, -0.5f) + offset);
			}else if (direction == Vector3.right) {

				p0 =  (new Vector3 (-0.5f, -0.5f, -0.5f) + offset);
				p1 =  (new Vector3 (-0.5f, -0.5f, 0.5f) + offset);
				p2 =  (new Vector3 (-0.5f, 0.5f, -0.5f) + offset);
				p3 =  (new Vector3 (-0.5f, 0.5f, 0.5f) + offset);
			}
		
			this.addVertix (main, p0, color, direction.normalized);
			this.addVertix (main, p1, color, direction.normalized);
			this.addVertix (main, p2, color, direction.normalized);
			this.addVertix (main, p3, color, direction.normalized);
		}

		private void build(VoxelProduct.Product main, int from, int to, Dictionary<VectorInt3, VoxelHandler> voxs, Dictionary<VectorInt3, VoxelHandler> all){

			List<VectorInt3> keys = new List<VectorInt3> (voxs.Keys); 
			for (int i = from; i < to; ++i) {
				VectorInt3 key = keys [i];

				VoxelHandler value = voxs [key];
				if(!all.ContainsKey(key + new VectorInt3(0,  0, -1))){
					addRect (main, Vector3.back, key, value.color);

				}

				if(!all.ContainsKey(key + new VectorInt3(0, 0, 1))){
					addRect (main, Vector3.forward, key,  value.color);

				}

				if(!all.ContainsKey(key + new VectorInt3(0, 1, 0))){
					addRect (main, Vector3.up, key, value.color);

				}


				if(!all.ContainsKey(key + new VectorInt3(0, -1, 0))){
					addRect (main, Vector3.down, key, value.color);

				}


				if(!all.ContainsKey(key + new VectorInt3(1, 0, 0))){
					addRect (main, Vector3.left, key, value.color);

				}


				if(!all.ContainsKey(key + new VectorInt3(-1, 0, 0))){
					addRect (main, Vector3.right, key,  value.color);

				}
			}
		}

		public void build(VoxelProduct product){
			if (product.sub != null) {
				for (int i = 0; i < product.sub.Length; ++i) {
					build (product.sub [i], product.main.voxels);
				}
			} else {
				build (product.main, product.main.voxels);
			}
			//

		}
		public void build(VoxelProduct.Product main, Dictionary<VectorInt3, VoxelHandler> all){
			main.draw = new VoxelDrawData ();
			for (int i = 0; i < main.voxels.Count; i+=1000) {
				build (main, i, Mathf.Min(i + 1000, main.voxels.Count), main.voxels, all);
			}
		}
	}


}