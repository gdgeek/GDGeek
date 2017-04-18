using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace GDGeek{
	public class WebTimestamp : Singleton<WebTimestamp> {
		
		private static double synchro_ = 0;

		public static WebTimestamp GetInstance(){
			return WebTimestamp.Instance;
		}
		private double local{
			get{
				double epoch = (System.DateTime.Now.Ticks - 621355968000000000) / 10000000;
				return epoch;
			}
		}
		public double epoch{
			get{
				return local + synchro_;
			}
		}
		public void synchro(double stamp){
			synchro_ = stamp - local;
		}
	}
}

