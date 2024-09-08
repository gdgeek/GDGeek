using UnityEngine;
using System.Collections;
using UnityEditor;
//using SevenZip.Compression.LZMA;
using System.IO;
using System;
/*
public class Test : Editor {

	[MenuItem ("MyMenu/CompressFile")]
	static void CompressFile () 
	{
		//压缩文件
		CompressFileLZMA(Application.dataPath+"/1.jpg",Application.dataPath+"/2.zip");
		AssetDatabase.Refresh();

	}
	[MenuItem ("MyMenu/DecompressFile")]
	static void DecompressFile () 
	{
		//解压文件
		DecompressFileLZMA(Application.dataPath+"/2.zip",Application.dataPath+"/3.jpg");
		AssetDatabase.Refresh();
	}



	private static void CompressFileLZMA(string inFile, string outFile)
	{
		SevenZip.Compression.LZMA.Encoder coder = new SevenZip.Compression.LZMA.Encoder();
		FileStream input = new FileStream(inFile, FileMode.Open);
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

	private static void DecompressFileLZMA(string inFile, string outFile)
	{
		SevenZip.Compression.LZMA.Decoder coder = new SevenZip.Compression.LZMA.Decoder();
		FileStream input = new FileStream(inFile, FileMode.Open);
		FileStream output = new FileStream(outFile, FileMode.Create);

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
		output.Close();
		input.Close();
	}


}*/