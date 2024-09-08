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

        public DataTask t => this;
    }
    public class DataTask<T> : DataTask//, IDataTask<T>
    {
        protected Action<T> doThenWithData;

        public new DataTask<T> then(Action<T> callback)
        {
            doThenWithData += callback;
            return this;
        }
     
        public void resolve(T data) {
            this.data = data;
            base.resolve();
            doThenWithData?.Invoke(this.data);
        }
        public new DataTask<T> error(Action<Exception> callback)
        {
            base.error(callback);
            return this;
        }

        public DataTask(T data):base()
        {
            this.init = ()=> {
                resolve(data);
            };
        }
      

        public DataTask():base()
        {
        }

        public T data { private set; get; }
        public new DataTask<T> t => this;
  }
    
  
}