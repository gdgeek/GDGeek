using System.Collections;
using GDGeek;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using GDGeek.FileDownload;
namespace GDGeek.FileDownload
{
    /*
    public class StreamWeb : ArchiveWeb<Stream, string>
    {
        private UnityWebRequest web_ = null;
        public IEnumerator readFromUrl(string url, System.Action<Stream> ret)
        {

            //Debug.LogError(url);
            web_ = UnityWebRequest.Get(url);
            // web_.CacheOrDownload
            web_.timeout = 600;//设置请求时常为五分钟
            yield return web_.SendWebRequest();
            ret(new MemoryStream(web_.downloadHandler.data));

        }

        public override DataTask<Stream> read(string url, string type = null)
        {
            DataTask<Stream> task = new DataTask<Stream>();
            TaskManager.PushFront(
                task,
                () => StartCoroutine(
                        readFromUrl(
                            url,
                            (Stream stream) => task.resolve(stream)
                        )
                )
            );

            return task;

        }
    }*/
}