using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GDGeek{
	public class VoxelGroups  {


		public void init (){
			
		}
		public class Point{
			//public Vector3 position;
			public HashSet<Point> link = new HashSet<Point>();
			public VoxelDrawData.Vertice vertice;
			public int index;
		}
		public class Triangle{
			private Point a_ = null;
			private Point b_ = null;
			private Point c_ = null;
			public Triangle(Point a, Point b, Point c){
				a_ = a;
				b_ = b;
				c_ = c;
			}
			public List<int> getIndex(){
				List<int> indexes = new List<int> ();
				Vector3 ab = a_.vertice.position - b_.vertice.position;
				Vector3 ac = a_.vertice.position - c_.vertice.position;
				Vector3 normal =Vector3.Cross(ab, ac);
				if (normal.normalized == a_.vertice.normal.normalized) {
					indexes.Add (a_.index);
					indexes.Add (b_.index);
					indexes.Add (c_.index);
				} else {

					indexes.Add (a_.index);
					indexes.Add (c_.index);
					indexes.Add (b_.index);
				}
				return indexes;
			}
		}
		private Dictionary<Vector3, Dictionary<Color, Dictionary<Vector3, Point>>> mesh_ = null;// = new Dictionary<Vector3, Point>();

		public static void GetTriangle(Point point, List<Triangle> trx){

			//a[normal][color][position]
			HashSet<Point> link = new HashSet<Point>();
			foreach (Point o in point.link) {
				link.Add (o);
			}
			foreach (Point o in point.link)
			{

				foreach(Point l in o.link){
					
					if (link.Contains (l)) {
						trx.Add(new Triangle (point, o, l));

					}

				}
				link.Remove(o);
				o.link.Remove (point);



			}
			point.link.Clear ();

		}


		public static void Mesh2Draw(Dictionary<Vector3, Dictionary<Color, Dictionary<Vector3, Point>>> mesh, VoxelDrawData draw){
			//VoxelDrawData draw = new VoxelDrawData ();

			draw.vertices.Clear ();
			draw.triangles.Clear ();
			List<Vector3> nKeys = new List<Vector3> (mesh.Keys);
			List<Triangle> trx = new List<Triangle> ();

			for (int i = 0; i < nKeys.Count; ++i) {
				Dictionary<Color, Dictionary<Vector3, Point>> colors = mesh [nKeys [i]];

				List<Color> cKeys = new List<Color> (colors.Keys);
				for (int j = 0; j < cKeys.Count; ++j) {
					Dictionary<Vector3, Point> points = colors [cKeys [j]];

					List<Vector3> pKeys = new List<Vector3> (points.Keys);
					for (int m = 0; m < pKeys.Count; ++m) {
						Point point = points [pKeys [m]];
						GetTriangle (point, trx);
						point.index = draw.vertices.Count;
						draw.vertices.Add (point.vertice);
						points.Remove (pKeys [m]);/**/
					}

				}
			}
			Debug.Log (trx.Count);
			for (int i = 0; i < trx.Count; ++i) {
				draw.triangles.AddRange (trx[i].getIndex());
			}
			
		}
		public static Dictionary<Vector3, Dictionary<Color, Dictionary<Vector3, Point>>>  Draw2Mesh (VoxelDrawData draw){

			Dictionary<Vector3, Dictionary<Color, Dictionary<Vector3, Point>>> mesh = new Dictionary<Vector3, Dictionary<Color, Dictionary<Vector3, Point>>>();// = new Dictionary<Vector3, Point>();

			List<VoxelDrawData.Vertice> vex = draw.vertices;

			for (int i = 0; i < vex.Count; ++i) {
				VoxelDrawData.Vertice v = vex[i];
				if (!mesh.ContainsKey (v.normal)) {
					mesh [v.normal] = new Dictionary<Color, Dictionary<Vector3, Point>> ();
				}


				if (!mesh [v.normal].ContainsKey (v.color)) {
					mesh [v.normal] [v.color]  = new Dictionary<Vector3, Point> ();

				}
				Point point = new Point ();
				point.vertice = v;
				mesh [v.normal] [v.color][v.position] = point;

			}


			List<int> trx = draw.triangles;

			for (int i = 0; i < trx.Count; i += 3) {
				VoxelDrawData.Vertice a = vex [trx [i]];
				VoxelDrawData.Vertice b = vex [trx [i+1]];
				VoxelDrawData.Vertice c = vex [trx [i+2]];
				Dictionary<Vector3, Point> m = mesh [a.normal] [a.color];

				Point A = m [a.position];
				Point B = m [b.position];
				Point C = m [c.position];

				A.link.Add (B);
				A.link.Add (C);

				B.link.Add (A);
				B.link.Add (C);

				C.link.Add (A);
				C.link.Add (B);
			}
			return mesh;

		}

		public void build(VoxelProduct product){
			mesh_ = VoxelGroups.Draw2Mesh (product.main.draw);

			VoxelGroups.Mesh2Draw (mesh_, product.main.draw);
		}
		public Task task(VoxelProduct product){
			Task task = new Task ();
			task.init = delegate {
				build(product);
			};
			return task;
		}
	}
}