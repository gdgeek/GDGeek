using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek.FileDownload
{
    [Serializable]
    public class FileData
    {
    

        public string md5;
        public string type;
        public string url;
        public string key;
        
        //https://7dgame-public-1251022382.cos.ap-nanjing.myqcloud.com
        //https://data.7dgame.com
        public string cdn => ReplaceUrlPart(url);
        
        static string ReplaceUrlPart(string url)
        {
            if (url != null)
            {
                return url.Replace(
                    "https://7dgame-public-1251022382.cos.ap-nanjing.myqcloud.com", //cos地址
                    "https://data.7dgame.com"//cdn地址
                );
            }

            return url;

            // 使用 Replace 方法替换指定部分
         
        }
        
      //  public Uri uri => new Uri(this.url);


    }



}