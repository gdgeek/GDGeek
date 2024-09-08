using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GDGeek
{
	public class VoxelMeshBuild 
	{


		
		
		public VoxelMeshBuild()
		{
		}
		public class Vector3Quad
		{
			private Vector3 vector0_;
			private Vector3 vector1_;
			private Vector3 vector2_;
			private Vector3 vector3_;

	

			public Vector3 offset { set; get; }


		

			public Vector3Int location
			{
				get;
				set;
			} = Vector3Int.zero;

			public Vector3 p0 => rotation * (vector0_ * size)+ offset * size;
			public Vector3 p1 =>  rotation * (vector1_ * size) + offset * size;
			public Vector3 p2 =>  rotation * (vector2_ * size) + offset * size;
			public Vector3 p3 =>  rotation * (vector3_ * size) + offset * size;
			public Vector3Int normal
			{
				get
				{
					Vector3 normalized = Vector3.Cross(p2 - p0, p3 - p0).normalized;
					return new Vector3Int(Mathf.RoundToInt(normalized.x), Mathf.RoundToInt(normalized.y), Mathf.RoundToInt(normalized.z));
				}
			}

			public Vector3Quad(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3)
			{
				vector0_ = v0;
				vector1_ = v1;
				vector2_ = v2;
				vector3_ = v3;
			}



			public float size
			{
				private get;
				set;
			}  = 0.005f;

			public Quaternion rotation
			{
				get;
				set;
			} = Quaternion.identity;

		}

		private void addVertix (VoxelProduct.Product main, Vector3 position, Color32 c, Vector3Int normal, Vector2 uv1){
			VoxelVertex vtx = new VoxelVertex ();
			vtx.position = position;
			vtx.normal = normal;
			vtx.color = c;
			vtx.uv =  uv1;
			main.draw.vertices.Add (vtx);
		}

		private Vector3Quad getPoints(Quaternion rotation, Vector3Int location, float size, Vector3 offset)
		{
			
			Vector3 p0 =  new Vector3 (0.5f, -0.5f, 0.5f);//左上 1
			Vector3 p1 =  new Vector3 (-0.5f, -0.5f, 0.5f);//右上 0
			Vector3 p2 =  new Vector3 (0.5f, 0.5f, 0.5f) ;//左下 3
			Vector3 p3 =  new Vector3 (-0.5f, 0.5f, 0.5f);//右下 2

			Vector3Quad quad =  new Vector3Quad(p0, p1, p2, p3);
			quad.rotation = rotation;
			quad.location = location;
			quad.size = size;
			quad.offset = location + offset;
			return quad;
		}

		
		private void addRect(VoxelProduct.Product main, Vector3Quad quad, Color32 color){
			
			
			main.draw.triangles.Add (main.draw.vertices.Count + 0);//0
			main.draw.triangles.Add (main.draw.vertices.Count + 2);//2
			main.draw.triangles.Add (main.draw.vertices.Count + 3);//3
			main.draw.triangles.Add (main.draw.vertices.Count + 0);//0
			main.draw.triangles.Add (main.draw.vertices.Count + 3);//3
			main.draw.triangles.Add (main.draw.vertices.Count + 1);//1

;

	
			this.addVertix (main,  quad.p0, color, quad.normal, new Vector2(0, 0));
			this.addVertix (main, quad.p1, color, quad.normal, new Vector2(1, 0));
			this.addVertix (main, quad.p2, color, quad.normal, new Vector2(0, 1));
			this.addVertix (main, quad.p3, color, quad.normal, new Vector2(1, 1));
			


		}
		private void build(VoxelProduct.Product main, int from, int to, Dictionary<Vector3Int, VoxelHandler> voxs, Dictionary<Vector3Int, VoxelHandler> all,float size, Vector3 offset){

//			Debug.LogError(size);
			List<Vector3Int> keys = new List<Vector3Int> (voxs.Keys); 
			for (int i = from; i < to; ++i) {
				Vector3Int key = keys [i];
				VoxelHandler value = voxs [key];
				if(!all.ContainsKey(key + Vector3Int.forward)){
					
					addRect(main, getPoints(Quaternion.AngleAxis(0f, Vector3.up), key,size, offset), value.color);
				}
				if(!all.ContainsKey(key + Vector3Int.up)){
			
					addRect(main,getPoints(Quaternion.AngleAxis(-90f, Vector3.right), key,size, offset), value.color);
				}
				
				if(!all.ContainsKey(key + Vector3Int.back)){
				
					addRect(main, getPoints( Quaternion.AngleAxis(-180f, Vector3.right),key,size, offset), value.color);
				}
				if(!all.ContainsKey(key + Vector3Int.down)){
					addRect(main,getPoints(Quaternion.AngleAxis(-270f, Vector3.right),key,size, offset), value.color);
				}
				if(!all.ContainsKey(key + Vector3Int.left))
				{
					addRect(main, getPoints(Quaternion.AngleAxis(-90f, Vector3.up), key,size, offset), value.color);
				}
				
				if(!all.ContainsKey(key + Vector3Int.right)){
					addRect(main,getPoints(Quaternion.AngleAxis(90f,Vector3.up), key,size, offset), value.color);
				}
			
			}
		}

		public void build(VoxelProduct product){
			
				var products = product.products;
				for (int i = 0; i < products.Count; ++i) {
					build (products [i], product.main.voxels , product.size, product.offset);
					
				}
			

		}
		public void build(VoxelProduct.Product main, Dictionary<Vector3Int, VoxelHandler> all, float size, Vector3 offset){
		
			main.draw = new VoxelDrawData ();
			build (main, 0, main.voxels.Count, main.voxels, all, size, offset);
		
		}
	}


}