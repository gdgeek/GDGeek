
/*-----------------------------------------------------------------------------
The MIT License (MIT)

This source file is part of GDGeek
    (Game Develop & Game Engine Extendable Kits)
For the latest info, see http://gdgeek.com/

Copyright (c) 2014-2021 GDGeek Software Ltd

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
-----------------------------------------------------------------------------
*/
using UnityEngine;
using System.Collections;
using System;
namespace GDGeek
{
    public class TweenParabola : Tween
    {

      


        public Vector3 from
        {
            private get;
            set;
        }

        public Transform to
        {
            private get;
            set;
        }
        
        public float height
        {
            private get;
            set;
        }
        
        override protected void onUpdate(float factor, bool isFinished)
        {
            this.transform.position = GetPointAlongConstrainedParabola(from, to.position, Vector3.up, this.height, factor);//* (1f - factor) + to.position * factor;

        }

    
       

        public static Vector3 GetPointAlongConstrainedParabola(Vector3 origin, Vector3 end, Vector3 upDirection, float height, float normalizedLength)
        {
            float parabolaTime = normalizedLength * 2 - 1;
            Vector3 direction = end - origin;
            Vector3 pos = origin + normalizedLength * direction;
            pos += ((-parabolaTime * parabolaTime + 1) * height) * upDirection.normalized;
            return pos;
        }


        static public TweenParabola Begin(GameObject go, float duration, Transform from, Transform to, float height)
        {
            TweenParabola component = Tween.Begin<TweenParabola>(go, duration);
            component.from = from.position;
            component.to = to;
            component.height = height;
            if (duration <= 0f)
            {
                component.sample(1f, true);
                component.enabled = false;
            }
            return component;
        }
    }
}
