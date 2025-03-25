using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
namespace GDGeek
{
    
    public class DataTask : GDGeek.Task
    {
        protected Action doThen;
        protected Action<Exception> doError;

        public DataTask then(Action callback)
        {
            Debug.LogError("then");
            doThen += callback;
            return this;
        }

        public DataTask error(Action<Exception> callback)
        {
            doError += callback;
            return this;
        }
        public void reject(string message) => reject(new Exception(message));
        public void reject(Exception exception) {
            over = true;
            if (doError.IsNotNull())
            {
                doError.Invoke(exception);
            }
            else
            {
               Debug.LogError("Error:"+ exception.Message);
            }
        }
        public void resolve() {
           
            over = true;
            doThen?.Invoke();
        }

        public DataTask()
        {
            this.isOver =()=>over;
          
        }
      

        protected bool over
        {
            get;
            set;
        } = false;
        
        public new static DataTask T {
            get {
                DataTask dt = new DataTask();
                dt.pushFront(()=>{
                    dt.resolve();
                });
                return dt;
            }
        }

        //public  DataTask t => this;
    }
    public class DataTask<D> : DataTask//, IDataTask<T>
    {
        protected Action<D> doThenWithData;

        public  DataTask<D> then(Action<D> callback)
        {
            doThenWithData += callback;
            return this;
        }
     
        public void resolve(D data) {
            this.data = data;
            base.resolve();
            doThenWithData?.Invoke(this.data);
        }
        public new DataTask<D> error(Action<Exception> callback)
        {
            base.error(callback);
            return this;
        }

        public DataTask(D data):base()
        {
            this.init = ()=> {
                resolve(data);
            };
        }
      

        public DataTask():base()
        {
        }

        public D data { private set; get; }


         public new static DataTask<D> T {
            get {
                 DataTask<D> dt = new  DataTask<D>();
                 dt.pushFront(() => dt.resolve(default(D)));
                return dt;
            }
        }
     
       // public new DataTask<T> t => this;
  }
    
  
}