using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek { 
    
    public class KeyValue  {

        private string key_ = null;
        private object val_ = null;

        public override string ToString()
        {

            return "key=" + key_.ToString() + "; value=" +val_.ToString();

        }
        public KeyValue(string k, object v)
        {
            key_ = k;
            val_ = v;

        }
        public string key
        {
            get
            {
                return key_;
            }
            set
            {
                key_ = value;
            }
        }
        public object val
        {
            get
            {
                return val_;
            }
            set
            {
                val_ = value;
            }
        }
    }
}