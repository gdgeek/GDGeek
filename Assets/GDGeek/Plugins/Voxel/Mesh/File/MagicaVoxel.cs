using UnityEngine;
//using UnityEditor;
using GDGeek;
using System.IO;
using System.Collections.Generic;
using System;
using System.Text;
using System.Collections;


namespace GDGeek{
	

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
			public Vector4Int[] palette;
		}

		public int version = 0;
		public Main main = null;
		public Size size = null;
		public Rgba rgba = null;
		public string md5 = null;
		public MagicaVoxel(VoxelStruct vs, Main main, Size size, Rgba rgba, int version = 0){
			this.structure_ = vs;
			this.main = main;
			this.size = size;
			this.rgba = rgba;
			this.version = version;
		}
		public MagicaVoxel(VoxelStruct vs){
			arrange (vs);
		}

		private void arrange(VoxelStruct st, bool normal = false){
			structure_ = st;
			HashSet<Color> palette = new HashSet<Color>();

			Vector3Int min = new Vector3Int(9999, 9999, 9999);
			Vector3Int max = new Vector3Int(-9999, -9999,-9999);

			for (int i = 0; i < st.count; ++i) {
				palette.Add (st.getData(i).color);

				Vector3Int pos = st.getData(i).position;

				min.x = Mathf.Min (pos.x, min.x);
				min.y = Mathf.Min (pos.y, min.y);
				min.z = Mathf.Min (pos.z, min.z);
				max.x = Mathf.Max (pos.x, max.x);
				max.y = Mathf.Max (pos.y, max.y);
				max.z = Mathf.Max (pos.z, max.z);

			}

			if (normal) {
				max = max - min;
				for (int i = 0; i < st.count; ++i) {
					palette.Add (st.getData(i).color);
					var data = st.getData(i);
					data.position -= min;
					st.setData(i,data);//.pos = pos - min;

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
			this.rgba.palette = new Vector4Int[size];
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

			this.main.chunks = 52 + this.rgba.palette.Length *4 + st.count *4;

		}


		private VoxelStruct structure_;
		public VoxelStruct structure{
			get{ 
				return structure_;
			}

		}

	}


}
