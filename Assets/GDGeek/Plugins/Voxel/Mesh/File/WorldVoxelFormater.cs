using UnityEngine;
//using UnityEditor;
using GDGeek;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using System;


namespace GDGeek{
    /*
	public class WorldVoxelFormater
	{
		

		public static WorldVoxel ReadFromWorldVoxelFormater(System.IO.BinaryReader br){

			string json = GK7Zip.Decompressed (br);

			VoxelStruct vs = JsonUtility.FromJson<VoxelStruct> (json);
			WorldVoxel world = new WorldVoxel (vs);
		
			return world;

		}
		public static string GetMd5(VoxelStruct vs){

			MemoryStream memoryStream = new MemoryStream ();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			WriteToWorldVoxel (vs, binaryWriter);
			byte[] data = memoryStream.GetBuffer();
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] result = md5.ComputeHash(data);
			string fileMD5 = "";
			foreach (byte b in result)
			{
				fileMD5 += Convert.ToString(b, 16);
			}
			return fileMD5; 
		}
		public static void WriteToWorldVoxel(VoxelStruct vs, System.IO.BinaryWriter bw){

			string json = JsonUtility.ToJson (vs);
			GK7Zip.Compression (json, bw);
		}
	}*/
}

