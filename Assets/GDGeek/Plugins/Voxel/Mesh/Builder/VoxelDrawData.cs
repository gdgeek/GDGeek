using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GDGeek
{
	public class VoxelDrawData
	{
		public class Vertice
		{
			public Vector3 position;
			public Color color;
			public Vector3 normal;
		}

		public List<int> triangles = new List<int> ();
		public List<Vertice> vertices = new List<Vertice>();
		


		private Vector3[] postions_ = null;
		private Color[] colors_ = null;
	//	private Vector2[] uv1s_ = null;
	//	private Vector2[] uv2s_ = null;
		public void refresh(){
			List<int> list = new List<int> ();

			postions_ = new Vector3[vertices.Count];
			colors_ = new Color[vertices.Count];
		//	uv1s_ = new Vector2[vertices.Count];
		//	uv2s_ = new Vector2[vertices.Count];
			for (int i = 0; i < vertices.Count; ++i) {
				postions_ [i] = vertices [i].position;
				colors_ [i] = vertices [i].color;
		//		uv1s_ [i] = vertices [i].uv1;
		//		uv2s_ [i] = vertices [i].uv2;
			}


		}
		public Vector3[] postions
		{
			get {
				//refresh ();
				return postions_;
			}
		}

		public Color[] colors
		{
			get {
				//refresh ();
				return colors_;
			}
		}

	
	}



}