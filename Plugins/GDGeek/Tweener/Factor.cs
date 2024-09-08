using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek{
    [System.Serializable]
    public class Factor {


        public enum Style
        {
            Once,
            Loop,
            PingPong,
            Forward,
            Rebound,
        }

        [SerializeField]
        private Style _style;
        public Style style {
            set {
                _style = value;
            }
        }

        public float value
        {
            get;
            private set;
        }

        public float accumulation
        {
            get;
            private set;
        } = 0.0f;

       // private float accumulation_ = 0.0f;
        public bool finished 
        {  get; private set; }= false;
    
        public void once() {
            if (accumulation >= 1.0f)
            {
                value = 1.0f;
                finished = true;
            }
            else {
                value = accumulation;
                finished = false;
            }
        }

        public void forward()
        {
            if (accumulation >= 1.0f)
            {
                value = 0.0f;
                finished = true;
            }
            else
            {
                value = 1.0f - accumulation;
                finished = false;
            }
        }

        public void reset() {
            accumulation = 0f;
            finished = false;
        }
        public void loop()
        {
            
            value = Mathf.Repeat(accumulation, 1f);
            finished = false;

        }
        public void pingPong() {
            
            
            value = Mathf.PingPong(accumulation, 1f);
            finished = false;
        }


        public void increase(float delta) {
            accumulation += delta;
            switch (_style)
            {
                case Style.Once:
                    once();
                    break;
                case Style.Loop:
                    loop();
                    break;
                case Style.Forward:
                    forward();
                    break;
                case Style.PingPong:
                    pingPong();
                    break;
                case Style.Rebound:
                    rebound();
                    break;

            }

        }

        private void rebound()
        {
            value = Mathf.PingPong(accumulation, 1f);
            if (accumulation >= 2.0f)
            {
                finished = true;
            }
            else
            {
                finished = false;
            }

        }
    }

}
