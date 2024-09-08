using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using GDGeek;
using UnityEngine;
using UnityEngine.Networking;
/*
using GDGeek.FileDownload;
namespace GDGeek.FileDownload
{ 
    public class WebFile : MonoBehaviour {


        private UnityWebRequest web_ = null; 
        public IEnumerator readFromUrl(string url, System.Action<Stream> ret)
        {
            
         
            web_ = UnityWebRequest.Get(url);
            web_.timeout = 600;//设置请求时常为五分钟
            yield return web_.SendWebRequest();
            ret(new MemoryStream(web_.downloadHandler.data));

        }

       
        public DataTask<Stream> read(string url)
        {
            DataTask<Stream> task = new DataTask<Stream>();
            TaskManager.PushFront(
                task,
                () => StartCoroutine(
                    readFromUrl(
                        url,
                        (stream) => task.resolve(stream))
                    )
                );

            return task;
                
        }
    }
}*/