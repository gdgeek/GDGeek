using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GDGeek{
	public class WebPack
	{	
		private WWWForm form_ = new WWWForm(); 
	//	private string allString_ = "";
		//private string sugar_ = "";
		private string uuid_ = "";
		private string url_ = "";

		public WebPack(string url){
			url_ = url;
		}
		public void addBinaryData(string key, byte[] val){
			if(val != null){
				form_.AddBinaryData(key, val);
			}else{
				Debug.LogWarning("no value!");
			}
		}
		public void addField(string key, IList list){
			for(int i =0; i<list.Count; ++i){
				form_.AddField(key+"[]", list[i].ToString());
			}
		}
		public void addField(string key, string val){
			
			
			if(!string.IsNullOrEmpty(val)){
				//allString_ += val;
				form_.AddField(key, val);
			}else{
				Debug.LogWarning("no value!");
			}
		}	

		public void setUser(string uuid, string hash){
			this.addField("uuid", uuid);
			uuid_ = uuid;
			this.addField("hash", hash);
		}

		public WWW www(){

			var headers = this.form_.headers;

			headers["Accept-Encoding"] = "gzip";
			

			string uuid = "";
			if(!string.IsNullOrEmpty(uuid_)){
				uuid = "&uuid=" + uuid_;
				
			}
			string debug = "";
			if(Debug.isDebugBuild){
				debug = "&debug=1";
			}
			string tUrl =  url_ + uuid + debug + "&version=";
			Debug.LogWarning("url !" +tUrl);
			WWW www = new WWW(url_, this.form_);
			return www;
		}
		
	};
}


