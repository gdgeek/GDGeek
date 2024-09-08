using System;
using System.IO;
using UnityEngine;

namespace GDGeek.FileDownload
{
    public abstract class ArchiveReader<ARCHIVE, CONTEXT>
    {
        public virtual bool can(bool cache, CONTEXT context) => cache;

        public abstract System.Threading.Tasks.Task<ARCHIVE>  streamToObject(Stream stream,  CONTEXT context = default(CONTEXT));
        

        public abstract System.Threading.Tasks.Task objectToStream(Stream stream, ARCHIVE data, CONTEXT context = default(CONTEXT));

    }
}

