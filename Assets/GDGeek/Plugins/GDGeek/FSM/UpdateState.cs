using UnityEngine;
using System.Collections;
using System;

namespace GDGeek{
	public class UpdateState : State, IUpdateable {
		public delegate void Update(float d);
		public event Update onUpdate;
		public UpdateState(){
			base.onStart += delegate() {
				UpdateManager.Instance.onUpdate += this.update;
			};
			base.onOver += delegate() {
				UpdateManager.Instance.onUpdate -= this.update;
			};
		}
        
        public void update(float d){
			if (onUpdate != null && !isPause) {
				onUpdate (d);
			}
		}

        private bool isPause = false;
        public void pause()
        {
            isPause = true;
        }

        public void continuePlay()
        {
            isPause = false;
        }
    }
}