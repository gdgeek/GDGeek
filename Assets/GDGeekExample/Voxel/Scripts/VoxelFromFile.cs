using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDGeek;
using System.IO;

public class VoxelFromFile : MonoBehaviour {
	public Material _material = null;
	public TextAsset _file = null;
	// Use this for initialization
	void Start () {

		//读入文件数据
		Stream sw = new MemoryStream(_file.bytes);
		System.IO.BinaryReader br = new System.IO.BinaryReader (sw); 

		//从文件中解析MagicaVoxel文件，并转化成通用格式
		MagicaVoxel magica = MagicaVoxelFormater.ReadFromBinary (br);
        VoxelStruct vs = magica.structure;//.vs;


		//拿出体素数据
		VoxelData[] datas = vs.toArray();


		//创建体素“产品”
		VoxelProduct product = new VoxelProduct();

		//将数据写入产品
		(new VoxelData2Point (datas)).build (product);
		//切成8x8x8立方体，为了等下减面的时间可控，如果不切割的话，对于大模型减面时间太长
		(new VoxelSplitSmall (new Vector3Int(8, 8, 8))).build (product);
		//构造模型
		(new VoxelMeshBuild ()).build (product);
		//减去重复的顶点
		(new VoxelRemoveSameVertices ()).build (product);

		//减去重复的面
		(new VoxelRemoveFace ()).build (product);
		//减去重复的顶点（在减面过程中出现的重复顶点）
		(new VoxelRemoveSameVertices ()).build (product);

		//得到模型数据
		VoxelMeshData data = product.getMeshData ();

		//以上是通过体素生成模型数据的代码，如果有别的算法生成，可以直接走下面代码

		//通过模型数据生成Mesh 和 MeshFilter。
		Mesh mesh = VoxelBuilder.Data2Mesh (data);
		MeshFilter filter = VoxelBuilder.Mesh2Filter (mesh);

		//别忘了加上材质。
		VoxelBuilder.FilterAddRenderer (filter, _material);


		//设置模型的坐标和名称等。
		filter.transform.SetParent (this.transform);
		filter.transform.localEulerAngles = Vector3.zero;
		filter.transform.localPosition = data.offset;
		filter.name = "Voxel";






	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
