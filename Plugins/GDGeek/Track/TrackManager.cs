using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
namespace GDGeek {
    public class TrackManager : Singleton<TrackManager> {

      


        private Dictionary<Track.Method, double> logMap_ = new Dictionary<Track.Method, double>();
        void print() {
            foreach (var iter in logMap_) {
                UnityEngine.Debug.Log(iter.Key.methodName +":"+ iter.Value.ToString());
            }
          //Debug.Log("is print");
        }
        public override void OnApplicationQuit()
        {
            base.OnApplicationQuit();
            print();
        }

        public Track loging(StackFrame sf)
        {
            UnityEngine.Debug.Log(sf);
            Track log = new Track(sf.GetMethod().ReflectedType.Name, sf.GetMethod().Name);
            return log;
        }
        public void loged(Track log)
        {
            log.stop();
            if (logMap_.ContainsKey(log.method))
            {
                logMap_[log.method] = logMap_[log.method] + log.time;
            }
            else {
                logMap_[log.method] = log.time;
            }
        }
    }
}
