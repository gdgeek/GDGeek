using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

namespace GDGeek.FileDownload
{
    public class LocalFile : GDGeek.Singleton<LocalFile>
    {
        [SerializeField]

        private bool _usingLocalFile = false;
        private string persistentDataPath { get; set; }
    
        private string streamingAssetsPath { get; set; }


        private void Awake()
        {
            persistentDataPath = Application.persistentDataPath;
            streamingAssetsPath = Application.streamingAssetsPath;
        }


        private string getStreamingAssetsPath(string fileName)
        {
            string name = Path.GetFileName(fileName);
            string path = Path.Combine(streamingAssetsPath, "C", name);
            return path;
        }
        private string getPersistentDataPath(string fileName)
        {
            string name = Path.GetFileName(fileName);
            string path = Path.Combine(persistentDataPath, "Cache", name);
            return path;
        }

        private string getPathFile(string fileName)
        {
            if (_usingLocalFile && existsStreamingAssets(fileName))
            {
                return getStreamingAssetsPath(fileName);
            }
            return getPersistentDataPath(fileName);
        }



        private bool existsStreamingAssets(string fileName)
            => System.IO.File.Exists(getStreamingAssetsPath(fileName));

        private bool existsPersistentData(string fileName)
        {
            return System.IO.File.Exists(getPersistentDataPath(fileName));
        }
       

        public bool exists(string fileName)
        {
            if(_usingLocalFile && existsStreamingAssets(fileName))
            {
                return true;
            }else if (existsPersistentData(fileName))
            {
                return true;
            }
            return false;
        }

        public bool exists(string fileName, string md5)
        {
            if(_usingLocalFile &&  existsStreamingAssets(fileName) && BuildFileMd5(getStreamingAssetsPath(fileName)) == md5)
            {
                return true;
            }else if (existsPersistentData(fileName) && BuildFileMd5(getPersistentDataPath(fileName)) == md5)
            {
                return true;
            }
           
            return false;
        }

        private static string FormatMD5(Byte[] data)
            => System.BitConverter.ToString(data).Replace("-", "").ToLower();//将byte[]装换成字符串

        private static String BuildFileMd5(String path)
        {
          
            try
            {
                using (var fileStream = System.IO.File.OpenRead(path))//!!!!!!!!!
                {
                    var md5 = MD5.Create();
                    var fileMD5Bytes = md5.ComputeHash(fileStream);//计算指定Stream 对象的哈希值                                     
                    String filemd5 = FormatMD5(fileMD5Bytes);
                    return filemd5;
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
            }

            return null;
        }

        public FileStream read(string fileName)
        {
            var stream = System.IO.File.OpenRead(getPathFile(fileName));
            return stream;
        }
        

        public void delete(string fileName) {
            if (existsPersistentData(fileName))
            {
                System.IO.File.Delete(getPersistentDataPath(fileName));
            }
            
        }
        public FileStream write(string fileName)
        {
            string path = getPersistentDataPath(fileName);

            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory)) {
                Directory.CreateDirectory(directory);
            }
            return System.IO.File.OpenWrite(path);
        }
    }

}