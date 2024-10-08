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
	public class TweenCamera : Tween
	{
		public Vector3 from;
		public Vector3 to;
		
		private Camera camera_;
		
		public Camera cachedCamera { get { if (camera_ == null) camera_ = this.gameObject.GetComponent<Camera>(); return camera_; } }
		public Vector3 parameter { 
			get { 
				Camera camera = cachedCamera;
				return new Vector3(camera.transform.localPosition.x, camera.transform.localPosition.y, camera.orthographicSize);
			} 
			set {
				Camera camera = cachedCamera;
				camera.transform.localPosition = new Vector3(value.x, value.y, cachedCamera.transform.localPosition.z); 
				camera.orthographicSize = value.z;
			} 

		
		}
		
		override protected void onUpdate (float factor, bool isFinished) {
			parameter = from * (1f - factor) + to * factor; 
		}
		
		/// <summary>
		/// Start the tweening operation.
		/// </summary>
	
		static public TweenCamera Begin (GameObject go, float duration, Vector3 parameter)
		{
			TweenCamera comp = Tween.Begin<TweenCamera>(go, duration);
			comp.from = comp.parameter;
			comp.to = parameter;
			
			if (duration <= 0f)
			{
				comp.sample(1f, true);
				comp.enabled = false;
			}
			return comp;
		}	/**/
	}
}