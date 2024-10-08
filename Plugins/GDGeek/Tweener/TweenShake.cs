﻿/*-----------------------------------------------------------------------------
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

/// <summary>
/// Tween the object's position.
/// </summary>
namespace GDGeek{
	
	public class TweenShake : Tween
	{
		public Vector3 from;
		public Vector3 to;
		
		Transform mTrans;
		
		public Transform cachedTransform { get { if (mTrans == null) mTrans = target.transform; return mTrans; } }
		public Quaternion rotation { get { return cachedTransform.localRotation; } set { cachedTransform.localRotation = value; } }
		
		override protected void onUpdate (float factor, bool isFinished)
		{

			
			float f = Mathf.Log(factor) *( Mathf.Sin (factor* Mathf.PI *2));

			if (f > 0) {
				cachedTransform.localRotation = Quaternion.Slerp (Quaternion.Euler (from), Quaternion.Euler (to), f);
			} else {
				
				cachedTransform.localRotation = Quaternion.Slerp (Quaternion.Euler (from), Quaternion.Euler (-to), -f);	
			}
			//cachedTransform.localRotation = Quaternion.Slerp(Quaternion.Euler(from), Quaternion.Euler(to), factor);
		}
		
		/// <summary>
		/// Start the tweening operation.
		/// </summary>
		
		static public TweenShake Begin (GameObject go, float duration, Quaternion rot)
		{
			TweenShake comp = Tween.Begin<TweenShake>(go, duration);
			comp.from = comp.rotation.eulerAngles;
			comp.to = rot.eulerAngles;
			
			if (duration <= 0f)
			{
				comp.sample(1f, true);
				comp.enabled = false;
			}
			return comp;
		}
	}
}