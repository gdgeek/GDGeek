
using UnityEngine;

namespace GDGeek.FileDownload
{

    public class Texture2DManager : ArchiveManager<Texture2D, Texture2DReader, Texture2DWeb, string>
    {
        public static Texture2DManager Singleton
        {
            get;
            private set;
        } = null;
        protected void Awake()
        {
            if (Texture2DManager.Singleton)
            {
                Debug.LogError("More Texture2DManager in Scene");
                
            }

            Texture2DManager.Singleton = this;
        }

        protected void OnDestroy()
        {
            Texture2DManager.Singleton = null;
        }
    };
   
}