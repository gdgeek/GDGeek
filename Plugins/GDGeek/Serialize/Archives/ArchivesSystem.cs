using System;
using System.Collections.Generic;
using GDGeek.FileDownload;
using UnityEngine;
namespace GDGeek.Archives
{

    public class ArchivesSystem : GDGeek.Singleton<ArchivesSystem>
    {
       

        [Serializable]
        public class Data
        {
            [SerializeField]
            public string content;
       

        }
        [SerializeField]
        private Storable _storable;
        

        public Task readTask(GDGeek.Archives.ArchivesSystem.Data data) {
            Task task = new Task();
            TaskManager.PushBack(task, delegate
            {
                read(data);
            });
            return task;
        }
        public Task loadTask(GDGeek.Archives.ArchivesSystem.Data data) {
            if (data == null || data.content == null) {
                return new Task();
            }
        
            return readTask(data);
        }
        public void load(GDGeek.Archives.ArchivesSystem.Data data)
        {
            read(data);
            
        }

        private void read(GDGeek.Archives.ArchivesSystem.Data data)
        {
            _storable.load(data.content);
          
        }

     
    }
}