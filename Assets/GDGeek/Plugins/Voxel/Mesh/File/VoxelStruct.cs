using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace GDGeek{

	[Serializable]
	public class VoxelStruct:IEnumerable
	{

		private List<VoxelData> datas_ = null;

		public Dictionary<Vector3Int, Color> createMap(){
			Dictionary<Vector3Int, Color> dict = new Dictionary<Vector3Int, Color> ();
			foreach(VoxelData data in datas_){
				dict [data.position] = data.color;
			}
			return dict;
		}

		#region IEnumerable implementation

		IEnumerator IEnumerable.GetEnumerator()
		{
			return (IEnumerator) GetEnumerator();
		}

		public List<VoxelData>.Enumerator GetEnumerator()
		{
			return datas_.GetEnumerator ();
		}


		#endregion

		public enum ReversalAxis
		{
			XAxis = 1,
			YAxis = 2,
			ZAxis = 4
		}


		public VoxelStruct(List<VoxelData> list){
			datas_ = list;
		}
		public VoxelStruct(){
			datas_ = new List<VoxelData>();
		}

		public void setData(int n, VoxelData data){
			datas_ [n] = data;
		}
		public VoxelData getData(int n){
			return datas_ [n];
		}
		public VoxelData[] toArray(){
			return datas_.ToArray ();
		}
		public int count{
			get{
				return datas_.Count;

			}
		}

		public void addData(VoxelData data){
			datas_.Add (data);

		}

		public void feed(BoundsInt box, VoxelStruct datas){

			Vector3Int size = datas.bounds.size;
			Dictionary<Vector3Int, Color> topMap = datas.createMap();
			for(int x = box.min.x; x<box.max.x; ++x){
				for(int y = box.min.y; y<box.max.y; ++y){

					for(int z = box.min.z; z<box.max.z; ++z){
						Vector3Int p = new Vector3Int (x, y, z);
						Vector3Int t = new Vector3Int ((x-box.min.x)% size.x, (y-box.min.y)% size.y, (z-box.min.z)% size.z);
						if (topMap.ContainsKey (t)) {
							this.addData (new VoxelData(p, topMap[t]));
						}
					}
				}
			}
		
		}
		public static VoxelStruct Reversal(VoxelStruct st, int reversal) {
			if (reversal == 0) {
				return st;
			}
			Vector3Int min = new Vector3Int(9999, 9999, 9999);
			Vector3Int max = new Vector3Int(-9999, -9999, -9999);

			for (int i = 0; i < st.datas_.Count; ++i)
			{
				Vector3Int pos = st.datas_[i].position;
				min.x = Mathf.Min(pos.x, min.x);
				min.y = Mathf.Min(pos.y, min.y);
				min.z = Mathf.Min(pos.z, min.z);
				max.x = Mathf.Max(pos.x, max.x);
				max.y = Mathf.Max(pos.y, max.y);
				max.z = Mathf.Max(pos.z, max.z);
			}


			VoxelStruct ret = new VoxelStruct();
			for (int i = 0; i < st.datas_.Count; ++i) {
				var data = st.datas_[i];
				Vector3Int pos = data.position;
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

				ret.datas_.Add(new VoxelData(pos, data.color));

			}
			return ret;
		}
		public static VoxelStruct Unusual(Vector3Int shifting, VoxelStruct st){

			VoxelStruct ret = new VoxelStruct ();
			for (int i = 0; i < st.datas_.Count; ++i) {
				VoxelData data = st.datas_ [i];
				data.position += shifting;
				ret.datas_.Add (data);
			}

			return ret;
		}
		public static VoxelNormal Normal(VoxelStruct st){
			VoxelNormal normal = new VoxelNormal ();
			Vector3Int min = new Vector3Int(9999, 9999, 9999);
			Vector3Int max = new Vector3Int(-9999, -9999,-9999);

			for (int i = 0; i < st.datas_.Count; ++i) {
				Vector3Int pos = st.datas_ [i].position;
				min.x = Mathf.Min (pos.x, min.x);
				min.y = Mathf.Min (pos.y, min.y);
				min.z = Mathf.Min (pos.z, min.z);
				max.x = Mathf.Max (pos.x, max.x);
				max.y = Mathf.Max (pos.y, max.y);
				max.z = Mathf.Max (pos.z, max.z);
			}
			normal.vs = new VoxelStruct ();
			for (int i = 0; i < st.datas_.Count; ++i) {
				VoxelData data = st.datas_ [i];
				data.position -= min;
				normal.vs.datas_.Add (data);
			}

			normal.shifting = min;
			return normal;

		}
		public BoundsInt bounds{
			get{ 
				return CreateBounds (this);
			}

		}
		static public BoundsInt CreateBounds(VoxelStruct st){
			if (st == null || st.count == 0) {
				return new BoundsInt ();
			}

			Vector3Int min = new Vector3Int(9999, 9999, 9999);
			Vector3Int max = new Vector3Int(-9999, -9999,-9999);

			for (int i = 0; i < st.datas_.Count; ++i) {
				Vector3Int pos = st.datas_ [i].position;
				min.x = Mathf.Min (pos.x, min.x);
				min.y = Mathf.Min (pos.y, min.y);
				min.z = Mathf.Min (pos.z, min.z);
				max.x = Mathf.Max (pos.x, max.x);
				max.y = Mathf.Max (pos.y, max.y);
				max.z = Mathf.Max (pos.z, max.z);
			}
			Vector3Int size = new Vector3Int (max.x-min.x+1, max.y-min.y+1, max.z-min.z +1);
			Vector3Int center = new Vector3Int (size.x/2, size.y/2, size.z/2);
			BoundsInt bounds = new BoundsInt (center, size);

			return bounds;
		}

		static public HashSet<Vector3Int> Different(VoxelStruct v1, VoxelStruct v2){

			Dictionary<Vector3Int, Color> dict = new Dictionary<Vector3Int, Color> ();
			HashSet<Vector3Int> ret = new HashSet<Vector3Int> ();
			foreach (var data in v2.datas_) {
				dict.Add (data.position, data.color);
			}
			foreach (var data in v1.datas_) {
				if (dict.ContainsKey (data.position)) {
					var a = data.color;
					var b = dict [data.position];

					float r = Mathf.Sqrt (
						Mathf.Pow (a.r - b.r, 2) +
						Mathf.Pow (a.g - b.g, 2) +
						Mathf.Pow (a.b - b.b, 2)
					);

					if (r > 0.1f) {

						ret.Add (data.position);
					}
					dict.Remove (data.position);
				} else {
					ret.Add (data.position);
				}
			}
			foreach (var data in dict) {
				ret.Add (data.Key);
			}
			return ret;

		}

		static public VoxelStruct Create(HashSet<Vector3Int> hs, Color color){
			VoxelStruct vs = new VoxelStruct ();
			foreach (var data in hs) {
				vs.datas_.Add (new VoxelData (data, color));
			}
			return vs;

		}
	}

}