using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GDGeek
{
	public class VoxelRemoveFace
	{



		private List<Vertex> vertices;

		public VoxelRemoveFace() {
		}

		/**
		 * do reduce faces number of product
		 */
		public void build(VoxelProduct product) {
			if (product.sub != null) {
				for (int i = 0; i < product.sub.Length; ++i) {
					build (product.sub [i]);
				}
			} else {

				build (product.main);
			}
		}
		public void build(VoxelProduct.Product main) {
			
			vertices = new List<Vertex>();
			initVertices(main.draw);
			removeFaces();
			updateVerticesAndTriangleOfProduct(main);
		}
		//private 
		private void initVertices0(VoxelDrawData draw_data){
			for (int i = 0, count = draw_data.vertices.Count; i < count; ++i) {
				Vertex new_vertex = new Vertex(draw_data.vertices[i], i);
				vertices.Add(new_vertex);
			}
		}
		private void initVertices1(VoxelDrawData draw_data){
			for (int i = 0, count = draw_data.triangles.Count; i < count; i += 3) {
				Vertex v0 = vertices[draw_data.triangles[i]];
				Vertex v1 = vertices[draw_data.triangles[i + 1]];
				Vertex v2 = vertices[draw_data.triangles[i + 2]];
				Triangle new_triangle = new Triangle(v0, v1, v2);

				v0.neighbor_vertices.Add(v1);
				v0.neighbor_vertices.Add(v2);
				v0.adjacent_faces.Add(new_triangle);
				v1.neighbor_vertices.Add(v2);
				v1.neighbor_vertices.Add(v0);
				v1.adjacent_faces.Add(new_triangle);
				v2.neighbor_vertices.Add(v0);
				v2.neighbor_vertices.Add(v1);
				v2.adjacent_faces.Add(new_triangle);
			}

		}

		private void initVertices2(){
			for (int i = 0, count = vertices.Count; i < count; ++i) {
				distinct(vertices[i].neighbor_vertices);
				distinct(vertices[i].adjacent_faces);
			}

		}

		private void initVertices3(){

			float total_internal_angle = 0;
			for (int i = 0, count = vertices.Count; i < count; ++i) {
				if (vertices[i].neighbor_vertices.Count - vertices[i].adjacent_faces.Count > 1) {
					vertices[i].is_continuous_polygon = false;
				}

				total_internal_angle = computeSumOfInternalAngle(vertices[i]);
				if (!(Mathf.Approximately(total_internal_angle, 360f) || total_internal_angle < 180.01f) || Mathf.Approximately(total_internal_angle, 90f)) {
					vertices[i].can_move = false;
				}
			}

		}
		private void initVertices4(){


			for (int i = 0, count = vertices.Count; i < count; ++i) {
				computeEdgeCollapseCostAtVertex(vertices[i]);
			}

		}

		private void initVertices(VoxelDrawData draw_data) {
			initVertices0 (draw_data);
			initVertices1 (draw_data);
			initVertices2 ();
			initVertices3 ();
			initVertices4 ();
		}

		private float computeSumOfInternalAngle(Vertex v) {
			float sum_of_angle = 0;
			for (int i = 0, count = v.adjacent_faces.Count; i < count; ++i) {
				sum_of_angle += v.adjacent_faces[i].computeInternalAngle(v);
			}

			return sum_of_angle * Mathf.Rad2Deg;
		}

		private Task removeFacesTask() {
			int can_collapse_index = 0;

			TaskCircle tc = new TaskCircle ();
			TaskManager.PushFront (tc, delegate {
				can_collapse_index = getCanCollapseVertexIndex();
			});
			Task task = new Task ();
			task.init = delegate() {
				for (int i = 0; i < 200; ++i) {

					collapse (vertices [can_collapse_index], vertices [can_collapse_index].collapse);
					can_collapse_index = getCanCollapseVertexIndex ();
					if (can_collapse_index == -1) {
						break;
					}
				}

			};
			TaskManager.PushBack (task, delegate {
				
				if(can_collapse_index == -1){
					tc.forceQuit();
				}
			});


			tc.push (task);
			return tc;

		}


		private void removeFaces() {
			int can_collapse_index = getCanCollapseVertexIndex();
			while (can_collapse_index != -1) {
				collapse(vertices[can_collapse_index], vertices[can_collapse_index].collapse);
				can_collapse_index = getCanCollapseVertexIndex();
			}
		}

		private int getCanCollapseVertexIndex() {
			for (int i = 0, count = vertices.Count; i < count; ++i) {
				if (vertices[i].cost < int.MaxValue) {
					return i;
				}
			}
			return -1;
		}

		private void updateVerticesAndTriangleOfProduct(VoxelProduct.Product main) {
			List<VoxelDrawData.Vertice> temp_draw_vertices = new List<VoxelDrawData.Vertice>();
			List<Triangle> temp_triangles_list = new List<Triangle>();
			Dictionary<int, int> id_to_vertex_index = new Dictionary<int, int>();
			for (int i = 0, vertices_count = vertices.Count; i < vertices_count; ++i) {
				temp_draw_vertices.Add(main.draw.vertices[vertices[i].id]);
				id_to_vertex_index[vertices[i].id] = i;
				for (int j = 0, faces_count = vertices[i].adjacent_faces.Count; j < faces_count; ++j) {
					temp_triangles_list.Add(vertices[i].adjacent_faces[j]);
				}
			}
			main.draw.vertices = temp_draw_vertices;

			distinct(temp_triangles_list);
			List<int> triangles = new List<int>();
			for (int i = 0, faces_count = temp_triangles_list.Count; i < faces_count; ++i) {
				for (int j = 0; j < 3; ++j) {
					triangles.Add(id_to_vertex_index[temp_triangles_list[i].triangle_vertices[j].id]);
				}
			}
			main.draw.triangles = triangles;
		}


		/**
		 * compute collapse cost of u->{ neighbor_vertices_of_u }
		 */
		private void computeEdgeCollapseCostAtVertex(Vertex v) {
			v.cost = int.MaxValue;
			v.collapse = null;

			if (!v.can_move) {
				return;
			}

			if (v.neighbor_vertices.Count == 0) {
				return;
			}

			for (int i = 0, count = v.neighbor_vertices.Count; i < count; ++i) {
				float c = computeEdgeCollapseCostFromUToV(v, v.neighbor_vertices[i]);
				if (c < v.cost) {
					v.collapse = v.neighbor_vertices[i];
					v.cost = c;
				}
			}
		}

		private float computeEdgeCollapseCostFromUToV(Vertex u, Vertex v) {
			if (!u.is_continuous_polygon) {
				return int.MaxValue;
			}

			if (willChangeShapeUToV(u, v)) {
				return int.MaxValue;
			}

			return u.distance(v);
		}

		private bool willChangeShapeUToV(Vertex u, Vertex v) {
			List<Triangle> temp_triangles_list = new List<Triangle>();
			for (int i = 0, count = u.adjacent_faces.Count; i < count; ++i) {
				if (!u.adjacent_faces[i].has(v)) {
					Vertex[] t_vertices = u.adjacent_faces[i].triangle_vertices;
					Triangle new_t = new Triangle(t_vertices[0], t_vertices[1], t_vertices[2]);
					for (int j = 0; j < 3; ++j) {
						if (new_t.triangle_vertices[j].Equals(u)) {
							new_t.triangle_vertices[j] = v;
							break;
						}
					}

					temp_triangles_list.Add(new_t);
				}
			}

			bool u_in_none_triangle = true;
			for (int i = 0, count = temp_triangles_list.Count; i < count; ++i) {
				if (vertexInTriangle(u, temp_triangles_list[i])) {
					u_in_none_triangle = false;
					break;
				}
			}
			if (u_in_none_triangle) {
				return true;
			}

			float before_area = computeVertexAdjacentFacesArea(u);
			float after_area = 0;
			for (int i = 0, count = temp_triangles_list.Count; i < count; ++i) {
				after_area += temp_triangles_list[i].computeArea();
			}
			if (Mathf.Approximately(before_area, after_area)) {
				return false;
			}

			return true;
		}

		private float computeVertexAdjacentFacesArea(Vertex v) {
			float total_area = 0;
			for (int i = 0, count = v.adjacent_faces.Count; i < count; ++i) {
				total_area += v.adjacent_faces[i].computeArea();
			}
			return total_area;
		}

		private bool vertexInTriangle(Vertex v, Triangle t) {
			Vector3 tv0 = t.triangle_vertices[0].position;
			Vector3 tv1 = t.triangle_vertices[1].position;
			Vector3 tv2 = t.triangle_vertices[2].position;
			Vector3 vp = v.position;

			float area = 0.5f * (Vector3.Cross(tv0 - vp, tv1 - vp).magnitude
				+ Vector3.Cross(tv0 - vp, tv2 - vp).magnitude
				+ Vector3.Cross(tv1 - vp, tv2 - vp).magnitude);

			return Mathf.Approximately(area, t.computeArea());
		}


		/**
		 * collapse vertex u to vertex v
		 */
		private void collapse(Vertex u, Vertex v) {
			// remove the adjacent face of both vertex u and vertex v
			for (int i = 0; i < u.adjacent_faces.Count; ++i) {
				if (u.adjacent_faces[i].has(v)) {
					for (int j = 0; j < 3; ++j) {
						Vertex v_temp = u.adjacent_faces[i].triangle_vertices[j];
						if (!v_temp.Equals(u)) {
							for (int k = 0; k < v_temp.adjacent_faces.Count; ++k) {
								if (v_temp.adjacent_faces[k].Equals(u.adjacent_faces[i])) {
									v_temp.adjacent_faces.RemoveAt(k);
									break;
								}
							}
						}
					}
					u.adjacent_faces.RemoveAt(i);
					--i;
				}
			}

			// replace vertex u with vertex v in adjacent faces of neighbor vertices of vertex u
			for (int i = 0, i_count = u.neighbor_vertices.Count; i < i_count; ++i) {
				Vertex v_temp = u.neighbor_vertices[i];
				for (int j = 0, j_count = v_temp.adjacent_faces.Count; j < j_count; ++j) {
					if (v_temp.adjacent_faces[j].has(u)) {
						v_temp.adjacent_faces[j].replaceVertex(u, v);
					}
				}
			}

			// replace vertex u with vertex v in adjacent faces of vertex u
			for (int i = 0, count = u.adjacent_faces.Count; i < count; ++i) {
				u.adjacent_faces[i].replaceVertex(u, v);
			}

			// remove vertex u
			vertices.RemoveAt(vertices.IndexOf(u));

			// remove vertex u in neighbor vertices of vertex u
			// add neighbor vertices of vertex u to vertex v
			// add vertex v to neighbor vertices of vertex u
			// update collapse cost at neighbor vertices of vertex u
			for (int i = 0, count = u.neighbor_vertices.Count; i < count; ++i) {
				u.neighbor_vertices[i].neighbor_vertices.Remove(u);
				if (!u.neighbor_vertices[i].Equals(v)) {
					v.neighbor_vertices.Add(u.neighbor_vertices[i]);
					u.neighbor_vertices[i].neighbor_vertices.Add(v);
					distinct(u.neighbor_vertices[i].neighbor_vertices);
				}
				computeEdgeCollapseCostAtVertex(u.neighbor_vertices[i]);
			}
			distinct(v.neighbor_vertices);

			// add new faces to vertex v and update collapse cost of vertex v
			for (int i = 0, count = u.adjacent_faces.Count; i < count; ++i) {
				v.adjacent_faces.Add(u.adjacent_faces[i]);
			}
			computeEdgeCollapseCostAtVertex(v);
		}



		/**
		 * Vertex
		 */
		private class Vertex
		{
			public int id;
			public Vector3 position;
			public Color color;
			public Vector3 normal;
			public List<Vertex> neighbor_vertices;
			public List<Triangle> adjacent_faces;
			public float cost;
			public Vertex collapse;
			public bool is_continuous_polygon;
			public bool can_move;

			public Vertex() {}

			public Vertex(VoxelDrawData.Vertice v, int input_id) {
				id = input_id;
				position = v.position;
				color = v.color;
				normal = v.normal;
				neighbor_vertices = new List<Vertex>();
				adjacent_faces = new List<Triangle>();
				cost = int.MaxValue;
				collapse = null;
				is_continuous_polygon = true;
				can_move = true;
			}

			public bool Equals(Vertex other) {
				return (id == other.id);
			}

			public float distance(Vertex other) {
				return Vector3.Distance(position, other.position);
			}

		}



		/**
		 * Triangle
		 */
		private class Triangle
		{
			public Vertex[] triangle_vertices;
			public Vector3 normal;

			public Triangle() {}

			public Triangle(Vertex v0, Vertex v1, Vertex v2) {
				triangle_vertices = new Vertex[3] { v0, v1, v2 };
				normal = v1.normal;
			}

			public bool Equals(Triangle other) {
				return (has(other.triangle_vertices[0]) && has(other.triangle_vertices[1]) && has(other.triangle_vertices[2]));
			}

			public bool has(Vertex v) {
				for (int i = 0; i < 3; ++i) {
					if (triangle_vertices[i].Equals(v)) {
						return true;
					}
				}
				return false;
			}

			public void replaceVertex(Vertex u, Vertex v) {
				for (int i = 0; i < 3; ++i) {
					if (triangle_vertices[i].Equals(u)) {
						triangle_vertices[i] = v;
						return;
					}
				}
			}

			public float computeArea() {
				Vector3 ab = triangle_vertices[1].position - triangle_vertices[0].position;
				Vector3 ac = triangle_vertices[2].position - triangle_vertices[0].position;

				return 0.5f * Vector3.Cross(ab, ac).magnitude;
			}

			public static float computeArea(Vector3 v0, Vector3 v1, Vector3 v2) {
				Vector3 ab = v1 - v0;
				Vector3 ac = v2 - v0;

				return 0.5f * Vector3.Cross(ab, ac).magnitude;
			}

			public float computeInternalAngle(Vertex v) {
				Vector3 a = triangle_vertices[0].position;
				Vector3 b = triangle_vertices[1].position;
				Vector3 c = triangle_vertices[2].position;
				Vector3 side1 = new Vector3(0, 0, 0);
				Vector3 side2 = new Vector3(0, 0, 0);

				if (triangle_vertices[0].Equals(v)) {
					side1 = b - a;
					side2 = c - a;
				}
				else if (triangle_vertices[1].Equals(v)) {
					side1 = a - b;
					side2 = c - b;
				}
				else {
					side1 = a - c;
					side2 = b - c;
				}

				return Mathf.Acos(Vector3.Dot(side1, side2) / (side1.magnitude * side2.magnitude));
			}

		}



		/**
		 * distinct list
		 */
		private void distinct(List<Vertex> vl) {
			for (int i = 0, count = vl.Count; i < count; ++i) {
				for (int j = i + 1; j < count; ++j) {
					if (vl[i].Equals(vl[j])) {
						vl.RemoveAt(j);
						--j;
						--count;
					}
				}
			}
		}

		private void distinct(List<Triangle> tl) {
			for (int i = 0, count = tl.Count; i < count; ++i) {
				for (int j = i + 1; j < count; ++j) {
					if (tl[i].Equals(tl[j])) {
						tl.RemoveAt(j);
						--j;
						--count;
					}
				}
			}
		}

	}
}
