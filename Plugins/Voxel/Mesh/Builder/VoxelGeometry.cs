using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


namespace GDGeek
{
	public class VoxelGeometry
	{

		[Serializable]
		public class MeshData : ICloneable
		{
			public List<Vector3> vertices = new List<Vector3> ();//顶点信息
			public List<Color32> colors = new List<Color32> ();//颜色信息
			public List<int> triangles = new List<int> ();//三角片
			public List<Vector2> uvs = new List<Vector2> ();//uvs
			public List<Vector3> normals = new List<Vector3> ();//uvs

			public Vector3Int min;
			public Vector3Int max;

			public void addPoint(Vector3 position, Color32 color, Vector3 normal, Vector2 uv){
				vertices.Add (position);
				colors.Add (color);
				normals.Add (normal);
				uvs.Add(uv);
			}
			public object Clone()
			{
				MeshData data = new MeshData();
				this.vertices.ForEach(i => data.vertices.Add(i));

				this.colors.ForEach(i => data.colors.Add(i));
				this.triangles.ForEach(i => data.triangles.Add(i));
				this.uvs.ForEach(i => data.uvs.Add(i));
				data.min = this.min;
				data.max = this.max;

				return data;
			}

			public Vector3Int size => this.max - this.min;
			public Vector3 offset{
				get{ 
					return -((Vector3)(size) /2.0f + (Vector3)this.min);
				}
			}
			public MeshData add(MeshData other){
				min = new Vector3Int(Mathf.Min (min.x, other.min.x),Mathf.Min (min.y, other.min.y),Mathf.Min (min.z, other.min.z));
				max = new Vector3Int(Mathf.Min (max.x, other.max.x),Mathf.Min (max.y, other.max.y),Mathf.Min (max.z, other.max.z));

				int offset = vertices.Count;
				for (int i = 0; i < other.vertices.Count; ++i) {
					vertices.Add (other.vertices [i]);
					colors.Add (other.colors [i]);
				}

				for (int i = 0; i < other.triangles.Count; ++i) {
					triangles.Add (other.triangles [i] + offset);
				}
				return this;
			}

		}
	
	}


}