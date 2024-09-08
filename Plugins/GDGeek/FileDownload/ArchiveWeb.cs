using System;
using UnityEngine;

using GDGeek.FileDownload;
namespace GDGeek.FileDownload
{
    public abstract class ArchiveWeb<ARCHIVE,CONTEXT>
    {
    
        public abstract void get(string url, CONTEXT context);
        public abstract bool done { get; }
        public abstract ARCHIVE data { get; }
      
    }
}

