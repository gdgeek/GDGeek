using System.Collections;
using GDGeek;
using UnityEngine;
using UnityEngine.Networking;

namespace GDGeek.FileDownload
{
    public class Texture2DWeb : ArchiveWeb<Texture2D, string>
    {
        private UnityWebRequest web_ = null;
        /*
           public IEnumerator readFromUrl(string url, System.Action<Texture2D> ret)
           {
              Debug.LogError(url);
               web_ = UnityWebRequestTexture.GetTexture(url);
               Texture2D tex = new Texture2D(2, 2);
               // web_.CacheOrDownload
               web_.timeout = 600;//设置请求时常为五分钟
               yield return web_.SendWebRequest();
   
               if (web_.isNetworkError || web_.isHttpError)
               {
                   ret(null);
               }
               else
               {
                   Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, -1, true);
                   DownloadHandlerTexture handler = ((DownloadHandlerTexture)web_.downloadHandler);
                   
                  Debug.Log(handler.texture.width);
                //   texture.LoadImage(handler.data);
                   ret(handler.texture);
               }
            
   
           }
   
           public override DataTask<Texture2D> read(string url, string context)
           {
               DataTask<Texture2D> task = new DataTask<Texture2D>();
               TaskManager.PushFront(task,
                   () => StartCoroutine(
                       readFromUrl(
                           url,
                           (texture) => task.resolve(texture)
                       )
                   )
               );
   
               return task;
   
           }
           */
        private UnityWebRequestAsyncOperation operation_ = null;
        public override void get(string url, string context)
        {
            web_ = UnityWebRequestTexture.GetTexture(url);
            web_.timeout = 300;
            operation_ =  web_.SendWebRequest();


        }

        public override bool done => operation_.isDone;
       

        public override Texture2D data
        {
            get
            {
                DownloadHandlerTexture handler = ((DownloadHandlerTexture)web_.downloadHandler);

                return handler.texture;
            }
        }
    }
}