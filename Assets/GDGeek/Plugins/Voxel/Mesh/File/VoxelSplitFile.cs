using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;


namespace GDGeek{
	[ExecuteInEditMode]
	public class VoxelSplitFile : MonoBehaviour {

		[Serializable]
		public class Item{

			public delegate void PullVoxelStruct();
			PullVoxelStruct _pull;
			public string _MD5 = null;
			private VoxelStruct vs_ = null;
			public VoxelStruct voxel{
				get{ 
					
					return vs_;
				}
				set{ 
					vs_ = value;
				}

			}
			private VoxelMeshData data_ = null;

		
			public VoxelMeshData data{
				get{ 
					
					return data_;
				}
				set{ 
					data_ = value;
				}

			}
		}
		public bool _build = false;
		public TextAsset _voxFile = null;
		public int _hashcode = 0;
		public List<Item> _items;
		public List<SplitVoxel.Box> _boxes;

        /*
		public static VoxelGeometry.MeshData CreateData(string md5, VoxelStruct vs){
			string key = VoxelDirectorFile.GetKey(md5);
			VoxelGeometry.MeshData data = null;
			if (!GK7Zip.FileHas (key)) {
				if (vs == null) {
					return null;
				}
				data = VoxelBuilder.Struct2Data(vs);
				VoxelDirectorFile.SaveToFile (key, data);
			} else {
				data = VoxelDirectorFile.LoadFromFile (key);
			}
			return data;
		}*/
		public void pull(){
			this.readVoxFile (_voxFile);
		}

		private void readVoxFile(TextAsset voxFile){

			Stream sw = new MemoryStream(voxFile.bytes);
			System.IO.BinaryReader br = new System.IO.BinaryReader (sw); 
			MagicaVoxel magic = null;
			var vs =  MagicaVoxelFormater.ReadFromBinary (br).vs;
			SplitVoxel split = new SplitVoxel (vs);
			for (int i = 0; i < _boxes.Count; ++i) {
				split.addBox (_boxes[i]);
			}
			VoxelStruct[] vses = split.doIt ();
			_items.Clear ();
			for (int i = 0; i < vses.Length; ++i) {
//				vses [i].arrange ();
				string md5 = MagicaVoxelFormater.GetMd5 (vses [i]);
				Item item = new Item ();
				item.voxel = vses [i];
				item._MD5 = md5;
				//item.data = CreateData (md5, vses [i]);
				_items.Add (item);
			}
		
		}

		#if UNITY_EDITOR
		// Update is called once per frame
		void Update () {
			if (!Application.isPlaying) {

				if (_build) {
					_build = false;

					_boxes = new List<SplitVoxel.Box> ();

					_boxes.Add(new SplitVoxel.Box(new VectorInt3(0,54,0), new VectorInt3(16,16,3)));//3
					_boxes.Add(new SplitVoxel.Box(new VectorInt3(0,0,0), new VectorInt3(16,16,3)));//0
					_boxes.Add(new SplitVoxel.Box(new VectorInt3(0,34,0), new VectorInt3(16,16,3)));//2
					_boxes.Add(new SplitVoxel.Box(new VectorInt3(0,17,0), new VectorInt3(16,16,3)));//1


					_boxes.Add(new SplitVoxel.Box(new VectorInt3(54,54,0), new VectorInt3(16,16,3)));//15
					_boxes.Add(new SplitVoxel.Box(new VectorInt3(54,0,0), new VectorInt3(16,16,3)));//12
					_boxes.Add(new SplitVoxel.Box(new VectorInt3(54,34,0), new VectorInt3(16,16,3)));//14
					_boxes.Add(new SplitVoxel.Box(new VectorInt3(54,17,0), new VectorInt3(16,16,3)));//13

					_boxes.Add(new SplitVoxel.Box(new VectorInt3(20,54,0), new VectorInt3(16,16,3)));//7
					_boxes.Add(new SplitVoxel.Box(new VectorInt3(20,0,0), new VectorInt3(16,16,3)));//4
					_boxes.Add(new SplitVoxel.Box(new VectorInt3(20,34,0), new VectorInt3(16,16,3)));//6
					_boxes.Add(new SplitVoxel.Box(new VectorInt3(20,17,0), new VectorInt3(16,16,3)));//5


					_boxes.Add(new SplitVoxel.Box(new VectorInt3(37,54,0), new VectorInt3(16,16,3)));//11
					_boxes.Add(new SplitVoxel.Box(new VectorInt3(37,0,0), new VectorInt3(16,16,3)));//8
					_boxes.Add(new SplitVoxel.Box(new VectorInt3(37,34,0), new VectorInt3(16,16,3)));//10
					_boxes.Add(new SplitVoxel.Box(new VectorInt3(37,17,0), new VectorInt3(16,16,3)));//9



					this.readVoxFile (_voxFile);
				}
			}
		}
		#endif
	}
}