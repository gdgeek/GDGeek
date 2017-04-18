using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GDGeek
{
	public class VoxelRemoveSameVertices
	{
		
		private struct Pack
		{
			public int index;
			public VoxelDrawData.Vertice vertice;
			public Pack(int i, VoxelDrawData.Vertice v){
				index = i;
				vertice = v;
			}
		
		}



		public void build(VoxelProduct.Product main){
			var draw = main.draw;

			List<VoxelDrawData.Vertice> vertices = draw.vertices;

			List<int> triangles = draw.triangles;

			List<VoxelDrawData.Vertice> tVertices = new List<VoxelDrawData.Vertice>();


			Dictionary<Vector3, Pack> board = new Dictionary<Vector3, Pack>(); 
			List<Pack> temp = new List<Pack> ();

			List<int> tTriangles = new List<int>();
			Dictionary<int, int> ht = new Dictionary<int, int>(); 


			List<Pack> all = new List<Pack> ();
			for(int i =0; i<vertices.Count; ++i){
				all.Add (new Pack (i, vertices [i]));
			}

			while (all.Count != 0) {
				for (int i = 0; i < all.Count; ++i) {
					if (board.ContainsKey (all [i].vertice.position)) {
						Pack ver = board [all[i].vertice.position];

						if (ver.vertice.color != all [i].vertice.color || ver.vertice.normal != all [i].vertice.normal) {
							temp.Add (all [i]);
						} else {
							ht [all [i].index] = ht[ver.index];
						}

					} else {
						board [all [i].vertice.position] = all[i];
						tVertices.Add (all[i].vertice);
						ht [all [i].index] = tVertices.Count - 1;
					}
				}

				board.Clear ();

				all = temp;


				temp = new List<Pack> ();

			}

			for(int i = 0; i<triangles.Count; ++i){

				int oldIndex = triangles[i];
				int newIndex = ht[oldIndex];
				tTriangles.Add(newIndex);
			}

			main.draw.triangles = tTriangles;
			main.draw.vertices = tVertices;


		}
		public void build(VoxelProduct product){
			if (product.sub != null) {
				for (int i = 0; i < product.sub.Length; ++i) {
					build (product.sub [i]);
				}
			} else {

				build (product.main);
			}

		}

	}


}