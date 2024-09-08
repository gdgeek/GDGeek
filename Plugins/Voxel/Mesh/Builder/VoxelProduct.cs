using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GDGeek{
	
	public class VoxelProduct{
		
		public class Product{
			public Dictionary<Vector3Int, VoxelHandler> voxels = null;
			public VoxelDrawData draw = null;
		}
	
		public float size
		{
			get;
			set;
		} = 0.005f;
		public Vector3 offset
		{
			get;
			set;
		} = new Vector3(0.5f, 0.5f, 0.5f);
		public Vector3 min
		{
			get;
			set;
		} = new Vector3(999, 999, 999);
	
		public Vector3 max
		{
			get;
			set;
		}= new Vector3(-999, -999, -999);


		public Product main
		{
			get;
			set;
		} = new Product();



		public List<Product> products
		{
			get
			{
				
				if(this.sub != null){
					return new List<Product>(this.sub);
				}
				else
				{
					return new List<Product>
					{
						main
					};
				}
			}
		
		} 

		public Product[] sub
		{
			get;
			set;
		} = null;


		//private VoxelGeometry.MeshData data_ = null;
		public VoxelGeometry.MeshData getMeshData()
		{

			VoxelGeometry.MeshData data =  new VoxelGeometry.MeshData ();
		
				
			data.max = this.max;
			data.min = this.min;
			var list = this.products;
			
			List<int> triangles = new List<int>();
			for (int i = 0; i < list.Count; ++i) {

				int offset = data.vertices.Count;
				for (int j = 0; j < list [i].draw.vertices.Count; ++j) {
					data.addPoint (
						list[i].draw.vertices [j].position, 
						list [i].draw.vertices [j].color,
						list [i].draw.vertices [j].normal,
						list [i].draw.vertices [j].uv
					);
				}

				for (int n = 0; n < list [i].draw.triangles.Count; ++n) {
					data.triangles.Add (list [i].draw.triangles[n]+ offset);
				}
			}
			return data;
		}

	}

}