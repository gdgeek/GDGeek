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
/// Tween the object's position.
/// </summary>
namespace GDGeek{

	public class TweenRotation : Tween
	{
		public Quaternion from;
		public Quaternion to;

		[SerializeField]
		public Space space = Space.World;
	
		public Quaternion rotation
		{
			get
			{
				if (space == Space.Self)
				{
					return transform.localRotation;
				}
				else
				{
					return transform.rotation;
				}

			}
			set
			{
				if (space == Space.Self)
				{
					transform.localRotation = value;
				}
				else
				{
					transform.rotation = value;
				}
				
			}
		}

		override protected void onUpdate (float factor, bool isFinished)
		{
			rotation = Quaternion.Slerp(from, to, factor);
		}

		
		static public TweenRotation Begin (GameObject go, float duration, Quaternion rot, Space space = Space.World)
		{
			
			TweenRotation comp = Tween.Begin<TweenRotation>(go, duration);
			comp.space = space;
			comp.from = comp.rotation;
			comp.to = rot;

			if (duration <= 0f)
			{
				comp.sample(1f, true);
				comp.enabled = false;
			}
			return comp;
		}
	}
}