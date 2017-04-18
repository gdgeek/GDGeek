using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDGeek;
using System.IO;

public class VoxelFromCode : MonoBehaviour {
	public Material _material = null;
	//public TextAsset _file = null;
	// Use this for initialization
	void Start () {
		
		//这里用程序创建5个voxel
		VoxelData[] datas = new VoxelData[5];


		//分别设置颜色和坐标
		datas [0] = new VoxelData (new VectorInt3 (0, 0, 0), Color.white);
		datas [1] = new VoxelData (new VectorInt3 (0, 1, 0), Color.green);
		datas [2] = new VoxelData (new VectorInt3 (0, -1, 0), Color.yellow);
		datas [3] = new VoxelData (new VectorInt3 (1, 0, 0), Color.red);
		datas [4] = new VoxelData (new VectorInt3 (-1, 0, 0), Color.blue);


		//创建体素“产品”
		VoxelProduct product = new VoxelProduct();

		//将数据写入产品
		(new VoxelData2Point (datas)).build (product);
		//切成8x8x8立方体，为了等下减面的时间可控，如果不切割的话，对于大模型减面时间太长
		(new VoxelSplitSmall (new VectorInt3(8, 8, 8))).build (product);
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


}
