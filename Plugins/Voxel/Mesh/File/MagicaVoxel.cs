using UnityEngine;
//using UnityEditor;
using GDGeek;
using System.IO;
using System.Collections.Generic;
using System;
using System.Text;


namespace GDGeek{
	public class VoxelNormal{
		public Vector3Int shifting;
		public VoxelStruct vs;

	}
	[Serializable]
	public class VoxelStruct
	{
		public List<VoxelData> datas
		{
			get;
			set;
		} = new List<VoxelData>();

		public VoxelStruct(List<VoxelData> list)
		{
			this.datas = list;
		}
		public VoxelStruct()
		{
		}

		public static VoxelStruct Unusual(Vector3Int shifting, VoxelStruct st){

			VoxelStruct ret = new VoxelStruct ();
			for (int i = 0; i < st.datas.Count; ++i) {
				VoxelData data = st.datas [i];
				data.position += shifting;
				ret.datas.Add (data);
			}

			return ret;
		}
		public static VoxelNormal Normal(VoxelStruct st){
			VoxelNormal normal = new VoxelNormal ();
			Vector3Int min = new Vector3Int(9999, 9999, 9999);
			Vector3Int max = new Vector3Int(-9999, -9999,-9999);

			for (int i = 0; i < st.datas.Count; ++i) {
				Vector3Int pos = st.datas [i].position;
				min.x = Mathf.Min (pos.x, min.x);
				min.y = Mathf.Min (pos.y, min.y);
				min.z = Mathf.Min (pos.z, min.z);
				max.x = Mathf.Max (pos.x, max.x);
				max.y = Mathf.Max (pos.y, max.y);
				max.z = Mathf.Max (pos.z, max.z);
			}
			normal.vs = new VoxelStruct ();
			for (int i = 0; i < st.datas.Count; ++i) {
				VoxelData data = st.datas [i];
				data.position -= min;
				normal.vs.datas.Add (data);
			}

			normal.shifting = min;
			return normal;

		}
		static public Bounds CreateBounds(VoxelStruct st){
			Vector3Int min = new Vector3Int(9999, 9999, 9999);
			Vector3Int max = new Vector3Int(-9999, -9999,-9999);

			for (int i = 0; i < st.datas.Count; ++i) {
				Vector3Int pos = st.datas [i].position;
				min.x = Mathf.Min (pos.x, min.x);
				min.y = Mathf.Min (pos.y, min.y);
				min.z = Mathf.Min (pos.z, min.z);
				max.x = Mathf.Max (pos.x, max.x);
				max.y = Mathf.Max (pos.y, max.y);
				max.z = Mathf.Max (pos.z, max.z);
			}
			Vector3 size = new Vector3 (max.x-min.x+1, max.y-min.y+1, max.z-min.z +1);
			Bounds bounds = new Bounds (size/2, size);

			return bounds;
		}

		static public void  Different(VoxelStruct v1, VoxelStruct v2,  HashSet<Vector3Int>  diff, HashSet<Vector3Int> same = null){

			Dictionary<Vector3Int, Color> dict = new Dictionary<Vector3Int, Color> ();
			//HashSet<VectorInt3> ret = new HashSet<VectorInt3> ();
			foreach (var data in v2.datas) {
				dict.Add (data.position, data.color);
			}
			foreach (var data in v1.datas) {
				if (dict.ContainsKey (data.position)) {
					var a = data.color;
					var b = dict [data.position];

					float r = Mathf.Sqrt (
						Mathf.Pow (a.r - b.r, 2) +
						Mathf.Pow (a.g - b.g, 2) +
						Mathf.Pow (a.b - b.b, 2)
					);

					if (r > 0.1f) {
						
						diff.Add (data.position);
					}
					else
					{
						if (same != null)
						{
							same.Add(data.position);
						}
					}

					dict.Remove (data.position);
				} else {
					diff.Add (data.position);
				}
			}
			foreach (var data in dict) {
				diff.Add (data.Key);
			}
			//return deff;

		}

		static public VoxelStruct Create(HashSet<Vector3Int> hs, Color32 color){
			VoxelStruct vs = new VoxelStruct ();
			foreach (var data in hs) {
				vs.datas.Add (new VoxelData (data, color));
			}
			return vs;

		}

		public VoxelStruct sub(HashSet<Vector3Int> diff)
		{
			List<VoxelData> list = new List<VoxelData>();
			foreach (VoxelData data in datas)
			{
				if (!diff.Contains(data.position))
				{
					list.Add(data);
				}
			}
			
			return new VoxelStruct(list);
		}
	}


	public class WorldVoxel{

		public WorldVoxel(VoxelStruct vs){
			vs_ = vs;
		}
		private VoxelStruct vs_;
		public VoxelStruct vs{
			get{ 
				return vs_;
			}

		}


	}
	public class MagicaVoxel{
		public class Main
		{
			public int size;
			public string name;
			public int chunks;
		}

		public class Size
		{
			public int size;
			public string name;
			public int chunks;
			public Vector3Int box;

		}
		public class Rgba
		{
			public int size;
			public string name;
			public int chunks;
			public VectorInt4[] palette;
		}

		public int version = 0;
		public Main main = null;
		public Size size = null;
		public Rgba rgba = null;
		public string md5 = null;

		public MagicaVoxel processor(IVoxelProcessor processors){
		
			VoxelStruct vs = new VoxelStruct();
           
			
			foreach(var vox in this.vs.datas)
			{
				if (processors.include(this, vox))
				{
					vs.datas.Add(vox);
				}
			}
            
			return new MagicaVoxel(vs);
		}

		public MagicaVoxel(VoxelStruct vs){
			arrange (vs);
		}

		private void arrange(VoxelStruct st, bool normal = false){
			vs_ = st;
			HashSet<Color32> palette = new HashSet<Color32>();

			Vector3Int min = new Vector3Int(9999, 9999, 9999);
			Vector3Int max = new Vector3Int(-9999, -9999,-9999);

			for (int i = 0; i < st.datas.Count; ++i) {
				palette.Add (st.datas[i].color);

				Vector3Int pos = st.datas [i].position;

				min.x = Mathf.Min (pos.x, min.x);
				min.y = Mathf.Min (pos.y, min.y);
				min.z = Mathf.Min (pos.z, min.z);
				max.x = Mathf.Max (pos.x, max.x);
				max.y = Mathf.Max (pos.y, max.y);
				max.z = Mathf.Max (pos.z, max.z);

			}

			if (normal) {
				max = max - min;
				for (int i = 0; i < st.datas.Count; ++i) {
					palette.Add (st.datas[i].color);
					var data = st.datas [i];
					data.position -= min;
					st.datas [i]= data;//.pos = pos - min;

				}
				min = new Vector3Int (0, 0, 0);
			}

			this.main = new MagicaVoxel.Main ();
			this.main.name = "MAIN";
			this.main.size = 0;


			this.size = new MagicaVoxel.Size ();
			this.size.name = "SIZE";
			this.size.size = 12;
			this.size.chunks = 0;

			this.size.box = new Vector3Int ();


			this.size.box.x = max.x - min.x +1;
			this.size.box.y = max.y - min.y +1;
			this.size.box.z = max.z - min.z +1;


			this.rgba = new MagicaVoxel.Rgba ();

			int size = Mathf.Max (palette.Count, 256);
			this.rgba.palette = new VectorInt4[size];
			int n = 0;
			foreach (Color32 c in palette)
			{
				this.rgba.palette [n] = MagicaVoxelFormater.Color2Bytes (c);
				++n;
			}




			this.rgba.size = this.rgba.palette.Length * 4;
			this.rgba.name = "RGBA";
			this.rgba.chunks = 0;

			this.version = 150;

			this.main.chunks = 52 + this.rgba.palette.Length *4 + st.datas.Count *4;

		}

		private VoxelStruct vs_;
		public VoxelStruct vs{
			get{ 
				return vs_;
			}

		}

	}


}
