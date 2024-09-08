using System;
using System.IO;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace GDGeek.FileDownload
{
    public class Texture2DReader : ArchiveReader<Texture2D, string>
    {

        public override bool can(bool cache, string context)
        {
            if (cache)
            {
                switch (context)
                {
                    case "image/jpeg":
                        return true;
                    case "image/png":
                        return true;
                    case "image/tga":
                        return true;
                }
            }
            return false;
        }

        public override System.Threading.Tasks.Task objectToStream(Stream stream, Texture2D texture, string context)
        {        
           byte[] data = null;
           switch (context)
           {
               case "image/jpeg":
                   data = texture.EncodeToJPG();
                   break;
               case "image/png":
                   data = texture.EncodeToPNG();
                   break;
               case "image/tga":
                   data = texture.EncodeToTGA();
                   break;
               default:
                   data = texture.EncodeToJPG();
                   break;
                   
           }

            System.Threading.Tasks.Task t = new System.Threading.Tasks.Task(() =>
            {
                try
                {
                    stream.Write(data, 0, data.Length);
                    stream.Flush();
                    stream.Close();
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                }
            });
            t.Start();
            return t;
           
        }

        public override System.Threading.Tasks.Task<Texture2D> streamToObject(Stream stream, string context)
        {

                        
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);
            Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, -1, true);
            texture.LoadImage(data);
            
            System.Threading.Tasks.Task<Texture2D> t = new System.Threading.Tasks.Task<Texture2D>(() =>
            {
               return texture;
            });
            t.Start();
            return t;
            
           
        }
    }
}