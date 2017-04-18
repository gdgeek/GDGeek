/*-----------------------------------------------------------------------------
The MIT License (MIT)

This source file is part of GDGeek
    (Game Develop & Game Engine Extendable Kits)
For the latest info, see http://gdgeek.com/

Copyright (c) 2014-2015 GDGeek Software Ltd

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
namespace GDGeek{
	public class OffsetCamera : MonoBehaviour {
		public Camera _camera = null;
		private Vector3 oldCamera_;
		private Vector3 oldThis_;
		private Vector3 lastCamera_;
		public Vector3 _radio = Vector3.one;

		public void init(){
			oldThis_ = this.transform.position;
			oldCamera_ = this._camera.transform.position;
			lastCamera_ = this._camera.transform.position;
		}
		void Start () {
			init ();
		}
		
		// Update is called once per frame
		void LateUpdate () {
			if (this._camera.transform.position != lastCamera_) {
				lastCamera_ = this._camera.transform.position;
				Vector3 offset = lastCamera_ - oldCamera_;
				this.gameObject.transform.position = oldThis_ + new Vector3(offset.x * _radio.x, offset.y * _radio.y, offset.z * _radio.z);
			}
		}
	}
}