using UnityEngine;
using System.Collections;
using ICSharpCode.SharpZipLib.GZip;
using System.IO;
using System.Text;
using System;
public class GZip
{
	/// <summary>
	/// 实时压缩目标流，WEB输出专用功能
	/// </summary>
	//public static Stream BaseStream
	//{
	//   get
	//   {
	//        return HttpContext.Current.Items["OutStream"] as Stream;
	//   }
	//}
	/// <summary>
	/// 将字节数组写入目标压缩流，WEB输出专用功能
	/// </summary>
	/// <param name="buffer">要写入的字节数组</param>
	// public static void Write(byte[] buffer)
	// {
	//     if (HttpContext.Current.Response.ContentType.IndexOf(';') == -1 && HttpContext.Current.Response.ContentType.StartsWith("text", true, null))
	//    {
	//        HttpContext.Current.Response.ContentType += "; charset=" + HttpContext.Current.Response.ContentEncoding.WebName;
	//    }
	//    ((Stream)HttpContext.Current.Items["OutStream"]).Write(buffer,0,buffer.Length);
	// }
	/// <summary>
	/// 清除当前压缩流中的所有内容，WEB输出专用功能
	/// </summary>


	/// <summary>
	/// 将XML对象写入目标压缩流，WEB输出专用功能
	/// </summary>
	/// <param name="xml">XmlDocument对象</param>

	/// <summary>
	/// 将一个可转换为字符串的object对象写入目标压缩流，WEB输出专用功能
	/// </summary>
	/// <param name="num">要写入的对象</param>
	public static void Write(object num)
	{
		Write(num.ToString());
	}
	/// <summary>
	/// 将一个字符串写入目标压缩流，WEB输出专用功能
	/// </summary>
	/// <param name="str">要写入的字符串</param>

	/// <summary>
	/// 将一个流压缩到目标压缩流，WEB输出专用功能
	/// </summary>
	/// <param name="stream">要写入的流</param>
	public static void Write(Stream stream)
	{
		int len = (int)stream.Length;
		byte[] data = new byte[len];
		stream.Read(data, 0, len);
		Write(data);
	}
	/// <summary>
	/// 将一个文件压缩到目标压缩流，WEB输出专用功能
	/// </summary>
	/// <param name="filepath">要写入的文件路径</param>

	/// <summary>
	/// 将字符串压缩为字节数组
	/// 返回：已压缩的字节数组
	/// </summary>
	/// <param name="data">待压缩的字符串</param>
	/// <returns></returns>
	public static byte[] Compress(string stringToCompress)
	{
		byte[] bytData = Encoding.UTF8.GetBytes(stringToCompress);
		return CompressBytes(bytData);
	}
	/// <summary>
	/// 解压缩字节数组到字符串
	/// 返回：已解压的字符串（慎用）
	/// </summary>
	/// <param name="bytData">待解压缩的字节数组</param>
	/// <returns></returns>
	public static string DeCompress(byte[] bytData)
	{
		byte[] decompressedData = DecompressBytes(bytData);
		return Encoding.UTF8.GetString(decompressedData);
	}
	/// <summary>
	/// 压缩字符串
	/// 返回：已压缩的字符串
	/// </summary>
	/// <param name="bytData">待压缩的字符串组</param>
	/// <returns></returns>
	public static string CompressString(string stringToCompress)
	{
		byte[] bytData = Encoding.UTF8.GetBytes(stringToCompress);
		byte[] compressedData = CompressBytes(bytData);
		return Convert.ToBase64String(compressedData);
	}
	/// <summary>
	/// 解压缩字符串
	/// 返回：已解压的字符串
	/// </summary>
	/// <param name="bytData">待解压缩的字符串</param>
	/// <returns></returns>
	public static string DeCompressString(string CompressTostring)
	{
		//Debug.Log ("e" + CompressTostring);
		byte[] bytData = System.Text.Encoding.UTF8.GetBytes(CompressTostring);
		//for(int i=0;i<bytData.Length; ++i){
		//	Debug.Log( bytData[i]);
		//}
		//Debug.Log ("f");
		byte[] decompressedData = DecompressBytes(bytData);
		//Debug.Log ("g");
		return Encoding.UTF8.GetString(decompressedData);
		//Debug.Log ("h");
	}
	/// <summary>
	/// 压缩字节数组
	/// 返回：已压缩的字节数组
	/// </summary>
	/// <param name="bytData">待压缩的字节数组</param>
	/// <returns></returns>
	public static byte[] CompressBytes(byte[] data)
	{
		MemoryStream o = new MemoryStream();
		Stream s = new ICSharpCode.SharpZipLib.GZip.GZipOutputStream(o);
		s.Write(data, 0, data.Length);
		s.Close();
		o.Flush();
		o.Close();
		return o.ToArray();
	}
	/// <summary>
	/// 解压缩字节数组
	/// 返回：已解压的字节数组
	/// </summary>
	/// <param name="bytData">待解压缩的字节数组</param>
	/// <returns></returns>
	public static byte[] DecompressBytes(byte[] data)
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
}
