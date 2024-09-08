using System.Collections.Generic;
using UnityEngine;  

namespace GDGeek.FileDownload
{
    public class AudioClipManager : ArchiveManager<AudioClip, AudioClipReader, AudioClipWeb, AudioType> {
        public static AudioClipManager Singleton {
            get;
            set;
        }
        protected void Awake() {
            AudioClipManager.Singleton = this;
        }

        protected void OnDestroy() {
            AudioClipManager.Singleton = null;
        }   
    };
   
}

