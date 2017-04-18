using UnityEngine;
//using UnityEditor;
using GDGeek;
using System.IO;
using System.Collections.Generic;
using System;
using System.Text;


namespace GDGeek{
	public class VoxelNormal{
		public VectorInt3 shifting;
		public VoxelStruct vs;


	}
	[Serializable]
	public class VoxelStruct
	{

        public enum ReversalAxis
        {
            XAxis = 1,
            YAxis = 2,
            ZAxis = 4
        }


        public List<VoxelData> datas = new List<VoxelData>();
        public static VoxelStruct Reversal(VoxelStruct st, int reversal) {
            if (reversal == 0) {
                return st;
            }
            VectorInt3 min = new VectorInt3(9999, 9999, 9999);
            VectorInt3 max = new VectorInt3(-9999, -9999, -9999);

            for (int i = 0; i < st.datas.Count; ++i)
            {
                VectorInt3 pos = st.datas[i].pos;
                min.x = Mathf.Min(pos.x, min.x);
                min.y = Mathf.Min(pos.y, min.y);
                min.z = Mathf.Min(pos.z, min.z);
                max.x = Mathf.Max(pos.x, max.x);
                max.y = Mathf.Max(pos.y, max.y);
                max.z = Mathf.Max(pos.z, max.z);
            }


            VoxelStruct ret = new VoxelStruct();
            for (int i = 0; i < st.datas.Count; ++i) {
                var data = st.datas[i];
                VectorInt3 pos = data.pos;
                if ((reversal & (int)(ReversalAxis.XAxis)) != 0) {
                    pos.x = max.x - pos.x -1 + min.x;
                }

                if ((reversal & (int)(ReversalAxis.YAxis)) != 0)
                {
                    pos.y = max.y - pos.y - 1 + min.y;
                }
                if ((reversal & (int)(ReversalAxis.ZAxis)) != 0)
                {
                    pos.z = max.z - pos.z - 1 + min.z;
                }

                ret.datas.Add(new VoxelData(pos, data.color));
                    
            }
            return ret;
        }
		public static VoxelStruct Unusual(VectorInt3 shifting, VoxelStruct st){

			VoxelStruct ret = new VoxelStruct ();
			for (int i = 0; i < st.datas.Count; ++i) {
				VoxelData data = st.datas [i];
				data.pos += shifting;
				ret.datas.Add (data);
			}

			return ret;
		}
		public static VoxelNormal Normal(VoxelStruct st){
			VoxelNormal normal = new VoxelNormal ();
			VectorInt3 min = new VectorInt3(9999, 9999, 9999);
			VectorInt3 max = new VectorInt3(-9999, -9999,-9999);

			for (int i = 0; i < st.datas.Count; ++i) {
				VectorInt3 pos = st.datas [i].pos;
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
				data.pos -= min;
				normal.vs.datas.Add (data);
			}

			normal.shifting = min;
			return normal;

		}
		static public Bounds CreateBounds(VoxelStruct st){
			VectorInt3 min = new VectorInt3(9999, 9999, 9999);
			VectorInt3 max = new VectorInt3(-9999, -9999,-9999);

			for (int i = 0; i < st.datas.Count; ++i) {
				VectorInt3 pos = st.datas [i].pos;
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
	
		static public HashSet<VectorInt3> Different(VoxelStruct v1, VoxelStruct v2){

			Dictionary<VectorInt3, Color> dict = new Dictionary<VectorInt3, Color> ();
			HashSet<VectorInt3> ret = new HashSet<VectorInt3> ();
			foreach (var data in v2.datas) {
				dict.Add (data.pos, data.color);
			}
			foreach (var data in v1.datas) {
				if (dict.ContainsKey (data.pos)) {
					var a = data.color;
					var b = dict [data.pos];

					float r = Mathf.Sqrt (
						Mathf.Pow (a.r - b.r, 2) +
						Mathf.Pow (a.g - b.g, 2) +
						Mathf.Pow (a.b - b.b, 2)
					);

					if (r > 0.1f) {
						
						ret.Add (data.pos);
					}
					dict.Remove (data.pos);
				} else {
					ret.Add (data.pos);
				}
			}
			foreach (var data in dict) {
				ret.Add (data.Key);
			}
			return ret;

		}

		static public VoxelStruct Create(HashSet<VectorInt3> hs, Color color){
			VoxelStruct vs = new VoxelStruct ();
			foreach (var data in hs) {
				vs.datas.Add (new VoxelData (data, color));
			}
			return vs;

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
			public VectorInt3 box;

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

		public MagicaVoxel(VoxelStruct vs){
			arrange (vs);
		}

		private void arrange(VoxelStruct st, bool normal = false){
			vs_ = st;
			HashSet<Color> palette = new HashSet<Color>();

			VectorInt3 min = new VectorInt3(9999, 9999, 9999);
			VectorInt3 max = new VectorInt3(-9999, -9999,-9999);

			for (int i = 0; i < st.datas.Count; ++i) {
				palette.Add (st.datas[i].color);

				VectorInt3 pos = st.datas [i].pos;

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
					data.pos -= min;
					st.datas [i]= data;//.pos = pos - min;

				}
				min = new VectorInt3 (0, 0, 0);
			}

			this.main = new MagicaVoxel.Main ();
			this.main.name = "MAIN";
			this.main.size = 0;


			this.size = new MagicaVoxel.Size ();
			this.size.name = "SIZE";
			this.size.size = 12;
			this.size.chunks = 0;

			this.size.box = new VectorInt3 ();


			this.size.box.x = max.x - min.x +1;
			this.size.box.y = max.y - min.y +1;
			this.size.box.z = max.z - min.z +1;


			this.rgba = new MagicaVoxel.Rgba ();

			int size = Mathf.Max (palette.Count, 256);
			this.rgba.palette = new VectorInt4[size];
			int n = 0;
			foreach (Color c in palette)
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
