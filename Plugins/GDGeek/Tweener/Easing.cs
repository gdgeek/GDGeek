using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek
{
    public static class Easing
    {


        public delegate float Function(float start, float end, float value);

        public enum Method
        {
            Linear,
            EaseInQuad,
            EaseOutQuad,
            EaseInOutQuad,
            EaseInCubic,
            EaseOutCubic,
            EaseInOutCubic,
            EaseInQuart,
            EaseOutQuart,
            EaseInOutQuart,
            EaseInQuint,
            EaseOutQuint,
            EaseInOutQuint,
            EaseInSine,
            EaseOutSine,
            EaseInOutSine,
            EaseInExpo,
            EaseOutExpo,
            EaseInOutExpo,
            EaseInCirc,
            EaseOutCirc,
            EaseInOutCirc,
            Spring,
            /* GFX47 MOD START */
            //bounce,
            EaseInBounce,
            EaseOutBounce,
            EaseInOutBounce,
            /* GFX47 MOD END */
            EaseInBack,
            EaseOutBack,
            EaseInOutBack,
            /* GFX47 MOD START */
            //elastic,
            EaseInElastic,
            EaseOutElastic,
            EaseInOutElastic,
            /* GFX47 MOD END */
            // punch

            EaseIn = EaseInQuad,
            EaseOut = EaseOutQuad,
            EaseInOut = EaseInOutQuad,
            BounceIn = EaseInBounce,
            BounceOut = EaseOutBounce,
        }

        public static float EaseIt(Method method, float start, float end, float value)
        {
            Function ease = GetFunction(method);
            return ease(start, end, value);
          
        }

        static public Function GetFunction(Method method)
        {

            Function ease = null;
            switch (method)
            {
                case Method.EaseInQuad:
                    ease = new Function(EaseInQuad);
                    break;
                case Method.EaseOutQuad:
                    ease = new Function(EaseOutQuad);
                    break;
                case Method.EaseInOutQuad:
                    ease = new Function(EaseInOutQuad);
                    break;
                case Method.EaseInCubic:
                    ease = new Function(EaseInCubic);
                    break;
                case Method.EaseOutCubic:
                    ease = new Function(EaseOutCubic);
                    break;
                case Method.EaseInOutCubic:
                    ease = new Function(EaseInOutCubic);
                    break;
                case Method.EaseInQuart:
                    ease = new Function(EaseInQuart);
                    break;
                case Method.EaseOutQuart:
                    ease = new Function(EaseOutQuart);
                    break;
                case Method.EaseInOutQuart:
                    ease = new Function(EaseInOutQuart);
                    break;
                case Method.EaseInQuint:
                    ease = new Function(EaseInQuint);
                    break;
                case Method.EaseOutQuint:
                    ease = new Function(EaseOutQuint);
                    break;
                case Method.EaseInOutQuint:
                    ease = new Function(EaseInOutQuint);
                    break;
                case Method.EaseInSine:
                    ease = new Function(EaseInSine);
                    break;
                case Method.EaseOutSine:
                    ease = new Function(EaseOutSine);
                    break;
                case Method.EaseInOutSine:
                    ease = new Function(EaseInOutSine);
                    break;
                case Method.EaseInExpo:
                    ease = new Function(EaseInExpo);
                    break;
                case Method.EaseOutExpo:
                    ease = new Function(EaseOutExpo);
                    break;
                case Method.EaseInOutExpo:
                    ease = new Function(EaseInOutExpo);
                    break;
                case Method.EaseInCirc:
                    ease = new Function(EaseInCirc);
                    break;
                case Method.EaseOutCirc:
                    ease = new Function(EaseOutCirc);
                    break;
                case Method.EaseInOutCirc:
                    ease = new Function(EaseInOutCirc);
                    break;
                case Method.Linear:
                    ease = new Function(Linear);
                    break;
                case Method.Spring:
                    ease = new Function(Spring);
                    break;
                /* GFX47 MOD START */
                /*case EaseType.bounce:
                    ease = new EasingFunction(bounce);
                    break;*/
                case Method.EaseInBounce:
                    ease = new Function(EaseInBounce);
                    break;
                case Method.EaseOutBounce:
                    ease = new Function(EaseOutBounce);
                    break;
                case Method.EaseInOutBounce:
                    ease = new Function(EaseInOutBounce);
                    break;
                /* GFX47 MOD END */
                case Method.EaseInBack:
                    ease = new Function(EaseInBack);
                    break;
                case Method.EaseOutBack:
                    ease = new Function(EaseOutBack);
                    break;
                case Method.EaseInOutBack:
                    ease = new Function(EaseInOutBack);
                    break;
                /* GFX47 MOD START */
                /*case EaseType.elastic:
                    ease = new EasingFunction(elastic);
                    break;*/
                case Method.EaseInElastic:
                    ease = new Function(EaseInElastic);
                    break;
                case Method.EaseOutElastic:
                    ease = new Function(EaseOutElastic);
                    break;
                case Method.EaseInOutElastic:
                    ease = new Function(EaseInOutElastic);
                    break;
                    /* GFX47 MOD END */
            }
            return ease;
        }






        public static float Linear(float start, float end, float value)
        {
            return Mathf.Lerp(start, end, value);
        }

        public static float Clerp(float start, float end, float value)
        {
            float min = 0.0f;
            float max = 360.0f;
            float half = Mathf.Abs((max - min) / 2.0f);
            float retval = 0.0f;
            float diff = 0.0f;
            if ((end - start) < -half)
            {
                diff = ((max - start) + end) * value;
                retval = start + diff;
            }
            else if ((end - start) > half)
            {
                diff = -((max - end) + start) * value;
                retval = start + diff;
            }
            else retval = start + (end - start) * value;
            return retval;
        }

        public static float Spring(float start, float end, float value)
        {
            value = Mathf.Clamp01(value);
            value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
            return start + (end - start) * value;
        }

        public static float EaseInQuad(float start, float end, float value)
        {
            end -= start;
            return end * value * value + start;
        }

        public static float EaseOutQuad(float start, float end, float value)
        {
            end -= start;
            return -end * value * (value - 2) + start;
        }

        public static float EaseInOutQuad(float start, float end, float value)
        {
            value /= .5f;
            end -= start;
            if (value < 1) return end / 2 * value * value + start;
            value--;
            return -end / 2 * (value * (value - 2) - 1) + start;
        }

        public static float EaseInCubic(float start, float end, float value)
        {
            end -= start;
            return end * value * value * value + start;
        }

        public static float EaseOutCubic(float start, float end, float value)
        {
            value--;
            end -= start;
            return end * (value * value * value + 1) + start;
        }

        public static float EaseInOutCubic(float start, float end, float value)
        {
            value /= .5f;
            end -= start;
            if (value < 1) return end / 2 * value * value * value + start;
            value -= 2;
            return end / 2 * (value * value * value + 2) + start;
        }

        public static float EaseInQuart(float start, float end, float value)
        {
            end -= start;
            return end * value * value * value * value + start;
        }

        public static float EaseOutQuart(float start, float end, float value)
        {
            value--;
            end -= start;
            return -end * (value * value * value * value - 1) + start;
        }

        public static float EaseInOutQuart(float start, float end, float value)
        {
            value /= .5f;
            end -= start;
            if (value < 1) return end / 2 * value * value * value * value + start;
            value -= 2;
            return -end / 2 * (value * value * value * value - 2) + start;
        }

        public static float EaseInQuint(float start, float end, float value)
        {
            end -= start;
            return end * value * value * value * value * value + start;
        }

        public static float EaseOutQuint(float start, float end, float value)
        {
            value--;
            end -= start;
            return end * (value * value * value * value * value + 1) + start;
        }

        public static float EaseInOutQuint(float start, float end, float value)
        {
            value /= .5f;
            end -= start;
            if (value < 1) return end / 2 * value * value * value * value * value + start;
            value -= 2;
            return end / 2 * (value * value * value * value * value + 2) + start;
        }

        public static float EaseInSine(float start, float end, float value)
        {
            end -= start;
            return -end * Mathf.Cos(value / 1 * (Mathf.PI / 2)) + end + start;
        }

        public static float EaseOutSine(float start, float end, float value)
        {
            end -= start;
            return end * Mathf.Sin(value / 1 * (Mathf.PI / 2)) + start;
        }

        public static float EaseInOutSine(float start, float end, float value)
        {
            end -= start;
            return -end / 2 * (Mathf.Cos(Mathf.PI * value / 1) - 1) + start;
        }

        public static float EaseInExpo(float start, float end, float value)
        {
            end -= start;
            return end * Mathf.Pow(2, 10 * (value / 1 - 1)) + start;
        }

        public static float EaseOutExpo(float start, float end, float value)
        {
            end -= start;
            return end * (-Mathf.Pow(2, -10 * value / 1) + 1) + start;
        }

        public static float EaseInOutExpo(float start, float end, float value)
        {
            value /= .5f;
            end -= start;
            if (value < 1) return end / 2 * Mathf.Pow(2, 10 * (value - 1)) + start;
            value--;
            return end / 2 * (-Mathf.Pow(2, -10 * value) + 2) + start;
        }

        public static float EaseInCirc(float start, float end, float value)
        {
            end -= start;
            return -end * (Mathf.Sqrt(1 - value * value) - 1) + start;
        }

        public static float EaseOutCirc(float start, float end, float value)
        {
            value--;
            end -= start;
            return end * Mathf.Sqrt(1 - value * value) + start;
        }

        public static float EaseInOutCirc(float start, float end, float value)
        {
            value /= .5f;
            end -= start;
            if (value < 1) return -end / 2 * (Mathf.Sqrt(1 - value * value) - 1) + start;
            value -= 2;
            return end / 2 * (Mathf.Sqrt(1 - value * value) + 1) + start;
        }

        /* GFX47 MOD START */
        public static float EaseInBounce(float start, float end, float value)
        {
            end -= start;
            float d = 1f;
            return end - EaseOutBounce(0, end, d - value) + start;
        }
        /* GFX47 MOD END */

        /* GFX47 MOD START */
        //private float bounce(float start, float end, float value){
        public static float EaseOutBounce(float start, float end, float value)
        {
            value /= 1f;
            end -= start;
            if (value < (1 / 2.75f))
            {
                return end * (7.5625f * value * value) + start;
            }
            else if (value < (2 / 2.75f))
            {
                value -= (1.5f / 2.75f);
                return end * (7.5625f * (value) * value + .75f) + start;
            }
            else if (value < (2.5 / 2.75))
            {
                value -= (2.25f / 2.75f);
                return end * (7.5625f * (value) * value + .9375f) + start;
            }
            else
            {
                value -= (2.625f / 2.75f);
                return end * (7.5625f * (value) * value + .984375f) + start;
            }
        }
        /* GFX47 MOD END */

        /* GFX47 MOD START */
        public static float EaseInOutBounce(float start, float end, float value)
        {
            end -= start;
            float d = 1f;
            if (value < d / 2) return EaseInBounce(0, end, value * 2) * 0.5f + start;
            else return EaseOutBounce(0, end, value * 2 - d) * 0.5f + end * 0.5f + start;
        }
        /* GFX47 MOD END */

        private static float EaseInBack(float start, float end, float value)
        {
            end -= start;
            value /= 1;
            float s = 1.70158f;
            return end * (value) * value * ((s + 1) * value - s) + start;
        }

        public static float EaseOutBack(float start, float end, float value)
        {
            float s = 1.70158f;
            end -= start;
            value = (value / 1) - 1;
            return end * ((value) * value * ((s + 1) * value + s) + 1) + start;
        }

        public static float EaseInOutBack(float start, float end, float value)
        {
            float s = 1.70158f;
            end -= start;
            value /= .5f;
            if ((value) < 1)
            {
                s *= (1.525f);
                return end / 2 * (value * value * (((s) + 1) * value - s)) + start;
            }
            value -= 2;
            s *= (1.525f);
            return end / 2 * ((value) * value * (((s) + 1) * value + s) + 2) + start;
        }

        public static float punch(float amplitude, float value)
        {
            float s = 9;
            if (value == 0)
            {
                return 0;
            }
            if (value == 1)
            {
                return 0;
            }
            float period = 1 * 0.3f;
            s = period / (2 * Mathf.PI) * Mathf.Asin(0);
            return (amplitude * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * 1 - s) * (2 * Mathf.PI) / period));
        }
        public static float liner(float start, float end, float value)
        {
            return start * (1f - value) + end * value;
        }
        /* GFX47 MOD START */
        public static float EaseInElastic(float start, float end, float value)
        {
            end -= start;

            float d = 1f;
            float p = d * .3f;
            float s = 0;
            float a = 0;

            if (value == 0) return start;

            if ((value /= d) == 1) return start + end;

            if (a == 0f || a < Mathf.Abs(end))
            {
                a = end;
                s = p / 4;
            }
            else
            {
                s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
            }

            return -(a * Mathf.Pow(2, 10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p)) + start;
        }
        /* GFX47 MOD END */

        /* GFX47 MOD START */
        //private float elastic(float start, float end, float value){
        public static float EaseOutElastic(float start, float end, float value)
        {
            /* GFX47 MOD END */
            //Thank you to rafael.marteleto for fixing this as a port over from Pedro's UnityTween
            end -= start;

            float d = 1f;
            float p = d * .3f;
            float s = 0;
            float a = 0;

            if (value == 0) return start;

            if ((value /= d) == 1) return start + end;

            if (a == 0f || a < Mathf.Abs(end))
            {
                a = end;
                s = p / 4;
            }
            else
            {
                s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
            }

            return (a * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) + end + start);
        }

        /* GFX47 MOD START */
        public static float EaseInOutElastic(float start, float end, float value)
        {
            end -= start;

            float d = 1f;
            float p = d * .3f;
            float s = 0;
            float a = 0;

            if (value == 0) return start;

            if ((value /= d / 2) == 2) return start + end;

            if (a == 0f || a < Mathf.Abs(end))
            {
                a = end;
                s = p / 4;
            }
            else
            {
                s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
            }

            if (value < 1) return -0.5f * (a * Mathf.Pow(2, 10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p)) + start;
            return a * Mathf.Pow(2, -10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) * 0.5f + end + start;
        }
        /* GFX47 MOD END */

    }

}