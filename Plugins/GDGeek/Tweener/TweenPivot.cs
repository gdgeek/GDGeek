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
    public class TweenPivot : Tween
    {

        [SerializeField]
        private RectTransform _rectTransform;
        public RectTransform rectTransform
        {
            get {

                return target.GetComponent<RectTransform>();
            }
         
        }


        [SerializeField]
        private Vector2 _from;
        public Vector2 from
        {
            get
            {
                return _from;
            }
            set
            {
                _from = value;
            }
        }

        [SerializeField]
        private Vector2 _to;
        public Vector2 to
        {
            get {
                return _to;
            }
            set {
                _to = value;
            }
        }
        override protected void onUpdate(float factor, bool isFinished)
        {
            rectTransform.pivot = from * (1f - factor) + to * factor;

        }



        static public TweenPivot Begin(GameObject go, float duration, Vector2 from, Vector2 to)
        {
            TweenPivot component = Tween.Begin<TweenPivot>(go, duration);
            component.from = from;
            component.to = to;
            if (duration <= 0f)
            {
                component.sample(1f, true);
                component.enabled = false;
            }
            return component;
        }
    }
}