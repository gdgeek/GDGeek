using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GDGeek
{
	

	[Serializable]
	public class VoxelMeshData : ICloneable
	{
		public List<Vector3> vertices = new List<Vector3> ();//顶点信息
		public List<Color> colors = new List<Color> ();//颜色信息
		public List<int> triangles = new List<int> ();//三角片
		public List<Vector2> uvs = new List<Vector2> ();//uvs

		public Vector3 min;
		public Vector3 max;

		public void addPoint(Vector3 position, Color color){
			vertices.Add (position);
			colors.Add (color);
			uvs.Add (Vector2.zero);
		}
		public object Clone()
		{
			VoxelMeshData data = new VoxelMeshData();
			this.vertices.ForEach(i => data.vertices.Add(i));

			this.colors.ForEach(i => data.colors.Add(i));
			this.triangles.ForEach(i => data.triangles.Add(i));
			this.uvs.ForEach(i => data.uvs.Add(i));
			data.min = this.min;
			data.max = this.max;

			return data;
		}
		public Vector3 size{
			get{ 
				return  this.max - this.min;
			}
		}
		public Vector3 offset{
			get{ 
				return -(size /2.0f + this.min);
			}
		}
		public VoxelMeshData add(VoxelMeshData other){
			min = new Vector3(Mathf.Min (min.x, other.min.x),Mathf.Min (min.y, other.min.y),Mathf.Min (min.z, other.min.z));
			max = new Vector3(Mathf.Min (max.x, other.max.x),Mathf.Min (max.y, other.max.y),Mathf.Min (max.z, other.max.z));

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