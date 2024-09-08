using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;


namespace GDGeek{

	public class CacheFile{
		static public byte[] GetBytes(string key){
			BinaryReader br = null;
			FileStream fs = null;
			byte[] read = null;
			try
			{
				fs = new FileStream(GetFileName(key), FileMode.Open, FileAccess.Read);
				br = new BinaryReader(fs);
				read = br.ReadBytes ((int)fs.Length);
			}
			catch (IOException e)
			{
				Console.WriteLine(e.Message + "Cannot open file.");
				return null;
			}

			return read;
		}
		public static string GetPath()
		{
			return Application.streamingAssetsPath + "/";

		}

		public static string GetFileName(string key)
		{
			return GetPath() + key + ".zip";
		}

		static public void SetBytes(string key, byte[] val){


			BinaryWriter bw;

			try
			{
				bw = new BinaryWriter(new FileStream(GetFileName(key), FileMode.Create));
				bw.Write (val);
			}
			catch (IOException e)
			{
				Console.WriteLine(e.Message + "Cannot create file.");
				return;
			}

			bw.Close();

			
		}
		//public static void SetToFile(string key, string value)
		//{
		//	CompressToFile(value, GetFileName(key));
		//}
		//public static string GetFromFile(string key)
		//{
			//return DecompressFromFile(GetFileName(key));
		//	}
		public static bool FileHas(string key)
		{
			FileInfo file = new FileInfo(GetFileName(key));
			return file.Exists;
		}


		static public bool HasKey(string key){
			return FileHas (key);
		}

	}
	public class CacheWeb{
		
	}

	public class Cache {
		static public bool HasKey(string key){
			return CacheFile.HasKey (key);
		}
		static public void SetBytes(string key, byte[] val){
			CacheFile.SetBytes (key, val);

		}
		static public byte[] GetBytes(string key){
			return CacheFile.GetBytes (key);
		
		}
		/*

		public static string GetPath()
		{
			return Application.streamingAssetsPath + "/";

		}

		public static string GetFileName(string key)
		{
			return GetPath() + key + ".zip";
		}
		public static void SetToFile(string key, string value)
		{
			CompressToFile(value, GetFileName(key));
		}
		public static string GetFromFile(string key)
		{
			return DecompressFromFile(GetFileName(key));
		}
		public static bool FileHas(string key)
		{
			FileInfo file = new FileInfo(GetFileName(key));
			return file.Exists;
		}

		public static void CompressToFile(string text, string outFile)
		{
			byte[] bytData = Encoding.UTF8.GetBytes(text);
			byte[]  compress = ZipFile.Compression (bytData);

			BinaryWriter bw;

			try
			{
				bw = new BinaryWriter(new FileStream(outFile, FileMode.Create));
				bw.Write (compress);
			}
			catch (IOException e)
			{
				Console.WriteLine(e.Message + "Cannot create file.");
				return;
			}

			bw.Close();

		}

		public static string DecompressFromFile(string filename)
		{

			BinaryReader br = null;
			FileStream fs = null;
			byte[] read = null;
			try
			{
				fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
				br = new BinaryReader(fs);
				read = br.ReadBytes ((int)fs.Length);
			}
			catch (IOException e)
			{
				Console.WriteLine(e.Message + "Cannot open file.");
				return null;
			}


			byte[] decompressed = ZipFile.Decompressed (read);
			return Encoding.UTF8.GetString(decompressed);


		}
		*/

	}
}