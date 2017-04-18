using UnityEngine;
using System.Collections;
using System.IO;
using GDGeek;
using System;
using System.Collections.Generic;
/*
public class VoxelTest : MonoBehaviour {



	public VoxelDirector _director = null;
	public void initMesh ()
	{
		if(_director == null){
			this._director = this.gameObject.GetComponent<VoxelDirector>();
		}
		if(_director == null){
			this._director = this.gameObject.AddComponent<VoxelDirector>();

		}


		#if UNITY_EDITOR
		if(this._director._material == null){
			this._director._material = UnityEditor.AssetDatabase.LoadAssetAtPath<Material>("Assets/GdGeek/Media/Material/VoxelMesh.mat");
		}
		#endif
	}



	// Use this for initialization

	private Dictionary<string, int> _log = new Dictionary<string, int>();

	private void logIt(string key, int time){
//		Debug.Log (time);
		if (_log.ContainsKey (key)) {
			_log[key] += time;
		} else {
			_log[key] = time;
		}


	}
	private void  readFile(){
		int tick = ConvertDateTimeInt(DateTime.Now);
		FileStream sr2 = new FileStream ("map.vox", FileMode.OpenOrCreate, FileAccess.Read);
		System.IO.BinaryReader br2 = new System.IO.BinaryReader (sr2); 
		vs2 = VoxelFormater.ReadFromMagicaVoxel (br2);
		sr2.Close ();
		System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();   


		logIt("readFile", ConvertDateTimeInt(DateTime.Now) - tick);
	

	}
	public static int ConvertDateTimeInt(System.DateTime time)  
	{  
		System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));  
		return (int)(time - startTime).Ticks;  
	}  

	private void arrange(){

		int tick = ConvertDateTimeInt (DateTime.Now);
		vs2.arrange ();
		logIt("arrange", ConvertDateTimeInt(DateTime.Now) - tick);
	}
	void print(string key, float time){
		foreach (KeyValuePair<string, int> kv in _log) {
			Debug.Log (kv.Key + ":" + kv.Value / time * 100.0f + "%");
		
		}

	}

	//VoxelStruct vs = null;
	VoxelStruct vs2 = null;
	Task readFileTask(){
		Task task = new Task ();
		task.init = delegate {
			this.readFile ();
		};
		return task;
	}
	/*
	Task joinItTask(){
		Task task = new Task ();
		task.init = delegate {
			this.joinIt ();
		};
		return task;
	}
	private VoxelProduct product_ = null;
	void create(){

		int tick = ConvertDateTimeInt (DateTime.Now);
//		this._director.clear();//.arrange ();

		product_ = new VoxelProduct();
		VoxelData2Point vd2p = new VoxelData2Point();
		vd2p.setup (this.vs2.datas.ToArray ());
		vd2p.build(product_);









		logIt("create", ConvertDateTimeInt(DateTime.Now) - tick);
	}

	Task shadow(){

		int tick = 0;
		Task task = new Task ();
		task.init = delegate {
			tick = ConvertDateTimeInt (DateTime.Now);
			VoxelShadowBuild vsb = new VoxelShadowBuild ();
			vsb.build(product_);



		};
		task.shutdown = delegate {

			logIt("shadow", ConvertDateTimeInt(DateTime.Now) - tick);
		};
		return task ;
	}


	Task meshBuild(){

		int tick = 0;
		Task task = new Task ();
		task.init = delegate {
			tick = ConvertDateTimeInt (DateTime.Now);
			VoxelMeshBuild vmb = new VoxelMeshBuild ();
			vmb.build(product_);


		};
		task.shutdown = delegate {
			logIt("meshBuild", ConvertDateTimeInt(DateTime.Now) - tick);
		};
		return task ;
	}
	Task subTask(){
		int tick = 0;
		Task task = new Task ();
		task.init = delegate {
			tick = ConvertDateTimeInt (DateTime.Now);
			VoxelRemoveSameVertices vmb = new VoxelRemoveSameVertices ();
			vmb.build(product_);


		};
		task.shutdown = delegate {

			logIt("sub", ConvertDateTimeInt(DateTime.Now) - tick);
		};
		return task ;
	
	}
	Task geoemtryTask(){
		int tick = 0;
		Task task = new Task ();
		task.init = delegate {
			tick = ConvertDateTimeInt (DateTime.Now);
		//	this._director._geometry = new VoxelGeometry();
		//	this._director._geometry.draw (product_, this.gameObject, this._director._material);


		};
		task.shutdown = delegate {

			logIt("geoemtry", ConvertDateTimeInt(DateTime.Now) - tick);
		};
		return task ;
	}

	Task createTask(){
		Task task = new Task ();
		task.init = delegate {
			create();
		};
		return task;
	
	}
	Task arrangeTask(){
		Task task = new Task ();
		task.init = delegate {
			this.arrange ();
		};
		return task;
	}
	void Start () {
		int tick = ConvertDateTimeInt (DateTime.Now);

		VoxelStruct vs = null;
		TaskList tl = new TaskList ();

		TaskManager.PushFront (tl, delegate {
			this.initMesh();
			Debug.Log (DateTime.Now.Second);
		});
	
		tl.push (readFileTask());

		TaskManager.PushBack (tl, delegate {
			print ("all", ConvertDateTimeInt (DateTime.Now) - tick);
			Debug.Log (DateTime.Now.Second);
		});
		
		TaskManager.Run (tl);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
*/