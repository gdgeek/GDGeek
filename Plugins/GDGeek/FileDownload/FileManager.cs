using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace GDGeek.FileDownload
{ 


    public class FileManager : GDGeek.Singleton<FileManager> {

      
        [SerializeField]
        private DataDownload<Stream, string> _download;
        private Dictionary<string, DataDownload<Stream, string>> downloads_ = new Dictionary<string, DataDownload<Stream, string>>();
        //private HashTable
      
          

        class TaskPack {
            public DataTask<Stream> task;
            public FileData data;
            public TaskPack(DataTask<Stream> task, FileData data) {
                this.task = task;
                this.data = data;
            }
        }
        private void Update()
        {
            foreach (var fd in this.downloads_) {

                if (fd.Value.queue.Count != 0 && !fd.Value.busy)
                {
                    fd.Value.busy = true;
                    DataDownload<Stream, string>.TaskPack pack = fd.Value.queue.Dequeue();
                    DataTask<Stream>  task =  loadImpl(pack.data, pack.cache);
                    task.then(stream=>pack.task.resolve(stream)).error(e=> pack.task.reject(e));
                  
                    
                    task.pushBack(() =>
                    {
                        fd.Value.busy = false;
                    });
                    task.run();
                }
            }
         
        }
        public DataTask<Stream> load(FileData data, bool cache = true) {

            DataTask<Stream> task = new DataTask<Stream>();
            TaskManager.PushFront(task, delegate
            {
                DataDownload<Stream, string> download = this.getDownload(data);
                download.queue.Enqueue(new DataDownload<Stream,string>.TaskPack(task, data, cache));
            });
            task.then(stream =>
            {
                stream.Position = 0;
            });
            return task;
        }

        private DataDownload<Stream, string> getDownload(FileData data)
            => getDownload(data.key);

        private DataDownload<Stream, string> getDownload(string fileName)
        {
            if (downloads_.ContainsKey(fileName))
            {
                return downloads_[fileName];
            }
            else {
                DataDownload<Stream,string> download = new DataDownload<Stream, string>();
                downloads_[fileName] = download;
                return download;
            }
        }

        private DataTask<Stream> loadImpl(FileData data, bool cache)
        {

            
            DataTask<Stream> ret = new DataTask<Stream>();
            if(cache)
            {
                if (hasCache(data))
                {
                    ret.pushFront(() =>
                    {
                        ThreadingTask<Stream> task = new ThreadingTask<Stream>((() => _readFromCache(data)));
                        task.pushBack(() =>
                        {
                            ret.resolve(task.data);
                        }).run();
                        
                    });
                }
                else
                {
                    
                    ret.pushFront(() =>
                    {
                        TaskList tl = new TaskList();
                        ThreadingTask<Stream> read = new ThreadingTask<Stream>((() => _readFromWeb(data)));
                   
                        tl.push(read);
                        
                        Task write = new ThreadingTask(() => _writeToCache(data.key, read.data));
                        tl.push(write);
                  
                        tl.pushBack(() =>
                        {
                            ret.resolve(read.data);
                        }).run();
                       
                    });
                    
                }

                
              
            }
            else
            {  
                ret.pushFront(() =>
                {
                    cacheDelete(data);
                    ThreadingTask<Stream> task = new ThreadingTask<Stream>((() => _readFromWeb(data)));
                    task.pushBack(() =>
                    {
                        ret.resolve(task.data);
                    }).run();
                        
                });
              
            }
            return ret;
          
        }


        
        private   System.Threading.Tasks.Task<Stream>  _readFromCache(FileData data)
        {
           
             return  System.Threading.Tasks.Task.Run(async () =>
            {
                return await System.Threading.Tasks.Task.FromResult((Stream)LocalFile.Instance.read(data.key));
            });
          
        }
 
        public bool hasCache(FileData data)
            => LocalFile.Instance.exists(data.key, data.md5);
/*
        public string cachePath(FileData data)
          => LocalFile.Instance.getPathFile(data.key);
*/
        public void cacheDelete(FileData data)
            => LocalFile.Instance.delete(data.key);




        public bool hasFile(FileData data)
            => hasFile(data.key, data.md5);

        private bool hasFile(string fileName, string md5)
        => LocalFile.Instance.exists(fileName, md5);

        /*
        public string getPath(FileData data)
        {
            if (hasCache(data))
            {
                return cachePath(data);
            }
            return null;
        }
*/
  

        private  System.Threading.Tasks.Task _writeToCache(string fileName, Stream stream)
        {
            return System.Threading.Tasks.Task.Run(async () =>
            {
                stream.Position = 0;
                byte[] data = new byte[stream.Length];
                await stream.ReadAsync(data, 0, data.Length);
                var o = LocalFile.Instance.write(fileName);
                await o.WriteAsync(data, 0, data.Length);

                await o.FlushAsync();
                await stream.FlushAsync();
                o.Close();

            });


        }
        
        public async System.Threading.Tasks.Task<Stream> _readFromWeb(FileData file)
        {
            UnityWebRequest request = UnityWebRequest.Get(file.cdn);
            request.timeout = 300;//设置请求时常为五分钟
            UnityWebRequestAsyncOperation operation = request.SendWebRequest();
            while (!operation.isDone)
           {
               await System.Threading.Tasks.Task.Yield();
           }

           
            return (Stream)(new MemoryStream(request.downloadHandler.data));
        }



    }

       
}