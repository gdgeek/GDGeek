using UnityEngine;
using System.Collections;
using System.IO;
namespace GDGeek{
	public class VoxelReader: Singleton<VoxelReader>{
		public static System.IO.BinaryReader ReadFromFile(TextAsset file){
			Stream sw = new MemoryStream(file.bytes);
			System.IO.BinaryReader br = new System.IO.BinaryReader (sw); 
			return br;

		}
		public static System.IO.BinaryReader ReadFromPath(string path){
			return null;
		}
		public Task ReadFromUrl(string path, out BinaryReader br){

			br = null;
			return null;

			//Co
		}
		public static System.IO.BinaryReader ReadFromUrl(string path){

			//www.
			return null;
		}
		public IEnumerator ReadFromUrl(string path,System.Action<BinaryReader> brret){
			//over = false;
			#if UNITY_EDITOR || UNITY_IOS  
			path = "file://" + path;  
			#endif  

			WWW www = new WWW (path);  

			yield return www;  
			var br = new BinaryReader (new MemoryStream (www.bytes));
			brret (br);

		}

		public static VoxelReader ReadFromFile(string url){
			return null;
		}
			
	}
}