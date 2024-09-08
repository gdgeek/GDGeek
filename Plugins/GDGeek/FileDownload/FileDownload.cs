using System.Collections;
using System.Collections.Generic;
using System.IO;
using GDGeek.FileDownload;
using UnityEngine;
namespace GDGeek.FileDownload
{
    public class DataDownload<T,CONTEXT> {

        public class TaskPack
        {
            public DataTask<T> task;
            public FileData data;
            public bool cache = true;
            public CONTEXT context = default(CONTEXT);
            public TaskPack(DataTask<T> task, FileData data, bool cache, CONTEXT context = default(CONTEXT))
            {
                this.task = task;
                this.data = data;
                this.cache = cache;
                this.context = context;
            }
        }
        public bool busy = false;
        public Queue<TaskPack> queue = new Queue<TaskPack>();
    }
    /*
    public class FileDownload: DataDownload<Stream>
    {
    }*/
}
