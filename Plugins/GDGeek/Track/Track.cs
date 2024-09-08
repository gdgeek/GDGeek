using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
namespace GDGeek { 
    public class Track  {
        public struct Method
        {
            public string reflectedTypeName;
            public string methodName;

            public override int GetHashCode()
            {
                return reflectedTypeName.GetHashCode() ^ methodName.GetHashCode();
            }
        }
        public double time {
            get {
                return stopwatch_.Elapsed.TotalSeconds;
            }
        }
        public Track.Method method {
            get;
            set;
        }
        private Stopwatch stopwatch_ = new Stopwatch();
        public Track(string reflectedTypeName, string methodName) {
            Method data = new Method();
            data.reflectedTypeName = reflectedTypeName;
            data.methodName = methodName;
            method = data;
;

            stopwatch_.Start();
          
        }
        public void stop() {

            stopwatch_.Stop();
        }
       
    }
}