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

/// <summary>
/// Tween the object's local scale.
/// </summary>

namespace GDGeek{
	public class TweenScale : Tween
	{
		public Vector3 from = Vector3.one;
		public Vector3 to = Vector3.one;
		
		[SerializeField]
		public Space space = Space.World;
		
		public Vector3 scale
		{
			get
			{
				if(space == Space.Self){
					return transform.localScale;
				}
				else
				{
					return transform.lossyScale;
				}
			}
			set
			{
				if(space == Space.Self){
					transform.localScale = value;
				}
				else
				{
					transform.setGlobalScale(value);
				}
			}
		}

		override protected void onUpdate (float factor, bool isFinished)
		{	
			scale = from * (1f - factor) + to * factor;
		}


		static public TweenScale Begin (GameObject go, float duration, Vector3 scale, Space space = Space.World )
		{
	
			TweenScale comp = Tween.Begin<TweenScale>(go, duration);
			comp.space = space;
			comp.from = comp.scale;
			comp.to = TransformData.NotZero(scale);

			if (duration <= 0f)
			{
				comp.sample(1f, true);
				comp.enabled = false;
			}
			return comp;
		}
	}
}
