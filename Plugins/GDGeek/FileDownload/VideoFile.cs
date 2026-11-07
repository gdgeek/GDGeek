using System.IO;
using GDGeek;
using GDGeek.FileDownload;
using UnityEngine;

namespace GDGeek.FileDownload
{
    /*
    public class VideoFile
    {
      

        public static string GetFilePath(FileData data) {
            if (FileManager.Instance.hasFile(data)){
                return FileManager.Instance.getPath(data);
            }
            return null;
        }

        public static DataTask<Stream> Cacheing(FileData file) {

            return FileManager.Instance.load(file, true);
           
        }
    };
/*
    public class VideoWeb : ArchiveWeb<Video>
    {
        public override DataTask<Video> read(string url, string type)
        {
            return DataTask<Video>.T;
        }
    }

    public class VideoReader : ArchiveReader<Video>
    {
        public override void objectToStream(Stream stream, Video data)
        {
            return;
        }

        public override Video streamToObject(Stream stream, string type)
        {
            return new Video();
        }
    }

    public class Video
    {
    }*/
}
