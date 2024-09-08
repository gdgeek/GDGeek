using System;
using GDGeek;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

using GDGeek.FileDownload;

namespace GDGeek.FileDownload
{
    public class ArchiveManager<ARCHIVE, READER, WEB, CONTEXT>  : MonoBehaviour
        where READER : ArchiveReader<ARCHIVE, CONTEXT>, new()
        where WEB: ArchiveWeb<ARCHIVE,CONTEXT>, new()
    {

        
        private Dictionary<string, DataDownload<ARCHIVE, CONTEXT>> downloads_ = new Dictionary<string, DataDownload<ARCHIVE, CONTEXT>>();
        private READER reader_;

        private READER reader
        {
            get
            {
                if (reader_ == null)
                {
                    reader_ = new READER();
                }
                return reader_;
            }
        }
      
        void Update()
        {
            foreach (var fd in this.downloads_) {

                if (fd.Value.queue.Count != 0 && !fd.Value.busy)
                {
                    DataDownload<ARCHIVE,CONTEXT>.TaskPack pack = fd.Value.queue.Dequeue();
                    DataTask<ARCHIVE> task = loadImpl(pack.data, pack.cache, pack.context);
                    task.then((ARCHIVE archive) =>
                        { 
                            pack.task.resolve(archive);
                        })
                        .error((Exception e) =>
                        {
                            Debug.LogError(e.Message);
                            pack.task.reject(e);
                        });
                 
                    task.pushFront(()=> fd.Value.busy = true ).pushBack(() => fd.Value.busy = false).run();
                }
            }
            
           

        }
       
        public DataTask<ARCHIVE> load(FileData data, bool cache = true, CONTEXT context = default(CONTEXT))
        {

            DataTask<ARCHIVE> task = new DataTask<ARCHIVE>();
            
//            Debug.LogError(JsonUtility.ToJson(data));
            task.pushFront(() =>
            {
                DataDownload<ARCHIVE, CONTEXT> download = this.getDownload(data.md5);
                download.queue.Enqueue(new DataDownload<ARCHIVE, CONTEXT>.TaskPack(task, data, cache, context));
            });
            
            return task;
        }

        
       
        private DataDownload<ARCHIVE, CONTEXT> getDownload(string md5)
        {
        
            if (downloads_.ContainsKey(md5))
            {
                return downloads_[md5];
            }
            else
            {
                DataDownload<ARCHIVE, CONTEXT> download = new DataDownload<ARCHIVE,CONTEXT>();
                downloads_[md5] = download;
                
                return download;
            }
        }
       

        
        private DataTask<ARCHIVE> loadImpl(FileData file, bool cache, CONTEXT context = default(CONTEXT))
        {

            DataTask<ARCHIVE> ret = new DataTask<ARCHIVE>();
            if(reader.can(cache, context))
            {
                if (hasCache(file))
                {
                    ret.pushFront(() =>
                    {
                        ThreadingTask<ARCHIVE> task = new ThreadingTask<ARCHIVE>((() => _readFromCache(file, context)));
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
                        ThreadingTask<ARCHIVE> read = new ThreadingTask<ARCHIVE>((() => _readFromWeb(file, context)));
                   
                        tl.push(read);
                        
                        Task write = new ThreadingTask(() =>
                        {
                             return _writeToCache(file, read.data, context);
                        });
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
                    fileDelete(file);
                    
                    ThreadingTask<ARCHIVE> task = new ThreadingTask<ARCHIVE>((() => _readFromWeb(file, context)));
                    task.pushBack(() =>
                    {
                        ret.resolve(task.data);
                    }).run();
                        
                });
              
            }
            return ret;
          
        }
      
        private System.Threading.Tasks.Task _writeToCache(FileData file, ARCHIVE archive, CONTEXT context)
        {
            return  reader.objectToStream(LocalFile.Instance.write(file.key), archive, context);
        }
        private System.Threading.Tasks.Task<ARCHIVE> _readFromCache(FileData file, CONTEXT context)
        {
            return reader.streamToObject(LocalFile.Instance.read(file.key), context);
        }

        public bool hasCache(FileData file)
        {
            return LocalFile.Instance.exists(file.key);
        }
        

        private void fileDelete(FileData file)
        {
            LocalFile.Instance.delete(file.key);
        }
       
        public async System.Threading.Tasks.Task<ARCHIVE> _readFromWeb(FileData file, CONTEXT context)
        {
            
            WEB web = new WEB();
//            Debug.LogError(file.url);
            web.get(file.cdn, context);
            while (!web.done)
            {
                await System.Threading.Tasks.Task.Yield();
            }

           
            return web.data;
        }
    }
}

