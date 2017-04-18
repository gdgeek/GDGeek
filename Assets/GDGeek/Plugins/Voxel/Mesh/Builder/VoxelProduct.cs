using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GDGeek{
	
	public class VoxelProduct{
		
		public class Product{
			public Dictionary<VectorInt3, VoxelHandler> voxels = null;
			public VoxelDrawData draw = null;
		}

		private Vector3 min_ = new Vector3(999, 999, 999);
		public Vector3 min{
			get{ 
				return min_;
			}
			set{ 
				min_ = value;
			}
		}
		private Vector3 max_ = new Vector3(-999, -999, -999);

		public Vector3 max{
			get{ 
				return max_;
			}
			set{ 
				max_ = value;
			}
		}

		private Product main_ = new Product();

		public Product main{
			get{ 
				return main_;
			}
			set{ 
				main_ = value;


			}
		}



		public Product[] sub_ = null;


		public Product[] sub{
			get{ 
				return sub_;
			}
			set{ 
				sub_ = value;
			}
		}


		private VoxelMeshData data_ = null;
		public VoxelMeshData getMeshData(){
			
			if (data_ == null) {
				data_ = new VoxelMeshData ();
			}
				
			data_.max = this.max;
			data_.min = this.min;
			if (this.sub != null) {

			
				List<int> triangles = new List<int>();
				for (int i = 0; i < this.sub.Length; ++i) {

					int offset = data_.vertices.Count;


					for (int j = 0; j < this.sub [i].draw.vertices.Count; ++j) {
						
						data_.addPoint (this.sub [i].draw.vertices [j].position, this.sub [i].draw.vertices [j].color);

					}

					for (int n = 0; n < this.sub [i].draw.triangles.Count; ++n) {
						data_.triangles.Add (this.sub [i].draw.triangles[n]+ offset);
					}
				}



			} else {
				for (int i = 0; i < main.draw.vertices.Count; ++i) {

					data_.addPoint (main.draw.vertices [i].position, main.draw.vertices [i].color);

				}
				data_.triangles = main.draw.triangles;
			}
		
			return data_;
		}

	}

}