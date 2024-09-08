using UnityEngine;
using UnityEditor;
//using NUnit.Framework;
using GDGeek;
using System.IO;
using GDGeek;
using System.Collections.Generic;
using System.Text;
using System;


public class MyZipTest : MonoBehaviour {
    /*
	public void Stream(){
	
		string str = "Testing 1-2-3";             //convert string 2 stream            
		byte[] array = Encoding.UTF8.GetBytes(str);       


		MemoryStream stream = new MemoryStream(array);             //convert stream 2 string      
		StreamReader reader = new StreamReader(stream);
		string text = reader.ReadToEnd();
		Console.WriteLine(text); 
		Console.ReadLine(); 
	}

	public static string GetPath(){
		string path = "";
		#if UNITY_EDITOR
		path = Application.dataPath +"/StreamingAssets"+"/";

		#elif UNITY_IPHONE
		path = Application.dataPath +"/Raw"+"/";

		#elif UNITY_ANDROID
		path = "jar:file://" + Application.dataPath + "!/assets/"+"/;

		#endif
		return path;
	}

	[Test]
	public void File(){
		string json = "asdfasfasfasfasdfasfasfsadf";
		FileInfo mydir = new FileInfo( GetPath () + "test.zip");
		Debug.Log (mydir.Exists);
		CompressToFile (json, GetPath () + "test.zip");
		string json2 = DecompressFromFile (GetPath () + "test.zip");
		Assert.AreEqual (json, json2);
		Debug.Log (json2);

	}
	private static string DecompressFromFile(string inFile)
	{
		SevenZip.Compression.LZMA.Decoder coder = new SevenZip.Compression.LZMA.Decoder();
		FileStream input = new FileStream(inFile, FileMode.Open);
		MemoryStream output = new MemoryStream ();;//FileStream output = new FileStream(outFile, FileMode.Create);

		// Read the decoder properties
		byte[] properties = new byte[5];
		input.Read(properties, 0, 5);

		// Read in the decompress file size.
		byte [] fileLengthBytes = new byte[8];
		input.Read(fileLengthBytes, 0, 8);
		long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);

		// Decompress the file.
		coder.SetDecoderProperties(properties);
		coder.Code(input, output, input.Length, fileLength, null);

		output.Flush();

		string ret=System.Text.Encoding.UTF8.GetString(output.ToArray());
		output.Close();
		input.Close();
		return ret;


	}

	private static void CompressToFile(string json, string outFile)
	{
		SevenZip.Compression.LZMA.Encoder coder = new SevenZip.Compression.LZMA.Encoder();

		byte[] array = Encoding.UTF8.GetBytes(json); 
		MemoryStream input = new MemoryStream (array);

		FileStream output = new FileStream(outFile, FileMode.Create);

		// Write the encoder properties
		coder.WriteCoderProperties(output);

		// Write the decompressed file size.
		output.Write(BitConverter.GetBytes(input.Length), 0, 8);

		// Encode the file.
		coder.Code(input, output, input.Length, -1, null);
		output.Flush();
		output.Close();
		input.Close();
	}


	[Test]
	public void Zip () {
		SevenZip.Compression.LZMA.Encoder coder = new SevenZip.Compression.LZMA.Encoder();
		string from = " i am here ,so cool !asdfasfsafsafsfsdfsdf";

		byte[] array = Encoding.UTF8.GetBytes(from); 
		MemoryStream input = new MemoryStream (array);
		Debug.Log (Application.dataPath + "/2.zip");
		FileStream output = new FileStream(Application.dataPath+"/2.zip", FileMode.Create);



		// Write the encoder properties
		coder.WriteCoderProperties(output);

		// Write the decompressed file size.
		output.Write(BitConverter.GetBytes(input.Length), 0, 8);

		// Encode the file.
		coder.Code(input, output, input.Length, -1, null);
		output.Flush();
		output.Close();
		input.Close();

	}

	[Test]
	public void Zip5 () {
		string from = "avsdfsadfjasdlfkjasdf";
		string to = Decompressed(Compression (from));
		Assert.AreEqual (from, to);

	}
	public static string Compression(string text){
		SevenZip.Compression.LZMA.Encoder coder = new SevenZip.Compression.LZMA.Encoder();	
		byte[] array = Encoding.UTF8.GetBytes(text); 
		MemoryStream input = new MemoryStream (array);
		MemoryStream output = new MemoryStream ();

		coder.WriteCoderProperties(output);
		output.Write(BitConverter.GetBytes(input.Length), 0, 8);
		coder.Code(input, output, input.Length, -1, null);
		output.Flush();
		string ret =  System.Convert.ToBase64String (output.GetBuffer ());

		output.Close();
		input.Close();
		//Debug.Log ("ret:" + ret);
		return ret;
	}
	public static string Decompressed(string text){
		
		SevenZip.Compression.LZMA.Decoder coder = new SevenZip.Compression.LZMA.Decoder();


		byte[] array = System.Convert.FromBase64String(text); 
		MemoryStream input = new MemoryStream (array);
		MemoryStream output = new MemoryStream ();


		byte[] properties = new byte[5];
		input.Read(properties, 0, 5);

		// Read in the decompress file size.
		byte [] fileLengthBytes = new byte[8];
		input.Read(fileLengthBytes, 0, 8);
		long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);

		coder.SetDecoderProperties(properties);
		coder.Code(input, output, input.Length, fileLength, null);

		output.Flush();
		//output.
		//StringReader sr = new StringReader();

		string ret=System.Text.Encoding.UTF8.GetString(output.ToArray());
		//string ret =  System.Convert.FromBase64String (output.GetBuffer ());
		output.Close();
		input.Close();
//		Debug.Log ("ret:" + ret);
		return ret;
	}
	[Test]
	public void Zip4 () {
		
	
	}

	[Test]
	public void Zip3 () {
		SevenZip.Compression.LZMA.Encoder coder = new SevenZip.Compression.LZMA.Encoder();
		string from = " i am here ,so cool !asdfasfsafsafsfsdfsdf";
		from += " i am here ,so cool !asdfasfsafsafsfsdfsdf";
		from += " i am here ,so cool !asdfasfsafsafsfsdfsdf";
	

		byte[] array = Encoding.UTF8.GetBytes(from); 
		MemoryStream input = new MemoryStream (array);
		Debug.Log (Application.dataPath + "/2.zip");
		MemoryStream output = new MemoryStream ();


		// Write the encoder properties
		coder.WriteCoderProperties(output);

		// Write the decompressed file size.
		output.Write(BitConverter.GetBytes(input.Length), 0, 8);
		//PlayerPrefs.
		// Encode the file.
		coder.Code(input, output, input.Length, -1, null);
		//System.Text.Encoding.
		var b = output.GetBuffer ();

		Debug.Log (from.Length);
		Debug.Log (System.Convert.ToBase64String (b).Length);


		var bb  = System.Convert.ToByte (System.Convert.ToBase64String (b));
		output.Flush();
		output.Close();
		input.Close();

	}



	[Test]
	public void Zip2()
	{
		SevenZip.Compression.LZMA.Decoder coder = new SevenZip.Compression.LZMA.Decoder();
		FileStream input = new FileStream(Application.dataPath+"/2.zip", FileMode.Open);

		MemoryStream output = new MemoryStream ();

		//FileStream output = new FileStream(outFile, FileMode.Create);

		// Read the decoder properties
		byte[] properties = new byte[5];
		input.Read(properties, 0, 5);

		// Read in the decompress file size.
		byte [] fileLengthBytes = new byte[8];
		input.Read(fileLengthBytes, 0, 8);
		long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);

		// Decompress the file.
		coder.SetDecoderProperties(properties);
		coder.Code(input, output, input.Length, fileLength, null);
		output.Flush();
		var b = output.GetBuffer ();
		var ret = System.Text.Encoding.UTF8.GetString (b);
		//StreamReader reader = new StreamReader(output);
		Debug.Log (ret);
		output.Close();
		input.Close();
	}
    */
}
