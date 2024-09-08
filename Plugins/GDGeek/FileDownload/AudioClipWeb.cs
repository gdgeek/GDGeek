using GDGeek;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

using GDGeek.FileDownload;
namespace GDGeek.FileDownload
{
    public class AudioClipWeb : ArchiveWeb<AudioClip, AudioType>
    {

        private UnityWebRequest web_ = null;
       

        private UnityWebRequestAsyncOperation operation_ = null;
        public override void get(string url, AudioType type = AudioType.WAV)
        {
            web_ = UnityWebRequestMultimedia.GetAudioClip(url, type);
           
            operation_ = web_.SendWebRequest();
         

        }

        public override bool done => operation_.isDone;
        public override AudioClip data =>DownloadHandlerAudioClip.GetContent(web_);
       

        
    }
   
}

