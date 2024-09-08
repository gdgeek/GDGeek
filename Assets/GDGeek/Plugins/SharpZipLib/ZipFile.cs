using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ICSharpCode.SharpZipLib.GZip;
using System.IO;
using System.Text;
using System;

namespace GDGeek{
	public class ZipFile{


		public static byte[] Compression(byte[] data)
		{
			MemoryStream o = new MemoryStream();
			Stream s = new ICSharpCode.SharpZipLib.GZip.GZipOutputStream(o);
			s.Write(data, 0, data.Length);
			s.Close();
			o.Flush();
			o.Close();
			return o.ToArray();
		}
		public static byte[] Decompressed(byte[] data)
		{
			MemoryStream o = new MemoryStream();
			Stream s = new GZipInputStream(new MemoryStream(data));
			try
			{
				int size = 0;
				byte[] buf = new byte[1024];
				while ((size = s.Read(buf, 0, buf.Length))> 0)
				{
					o.Write(buf, 0, size);
				}
			}
			finally
			{
				o.Close();
			}
			return o.ToArray();
		}



	
		public static string Decompressed(string text)
		{

			//(text);
			byte[] bytData = System.Convert.FromBase64String(text);
			byte[] decompressedData = Decompressed(bytData);
			return Encoding.UTF8.GetString(decompressedData);

		}

		public static string Compression(string text)
		{

			byte[] bytData = Encoding.UTF8.GetBytes(text);
			byte[] compressedData = Compression(bytData);
			return Convert.ToBase64String(compressedData);

		}
		/*
		public static void CompressToFile(string text, string outFile)
		{
			byte[] bytData = Encoding.UTF8.GetBytes(text);
			byte[]  compress = CompressBytes (bytData);

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

		
			byte[] decompressed = DecompressBytes (read);
			return Encoding.UTF8.GetString(decompressed);


		}


	*/
	}


}