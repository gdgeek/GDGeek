/*For the latest info, see http://gdgeek.com/
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
using UnityEngine.UI;
/// <summary>
/// Tween the object's local scale.
/// </summary>
namespace GDGeek{

	public class TweenVoxel : Tween
	{

		public VoxelProperty from = null;
		public VoxelProperty to = null;

		private Voxel voxel_ = null;
		public Voxel voxel { get { if (voxel_ == null) voxel_ = this.gameObject.GetComponent<Voxel>(); return voxel_; } }

		public VoxelProperty property { get {
				return voxel.property; 

			} set { 
				voxel.property = value;

			} }

		override protected void OnUpdate (float factor, bool isFinished){
			property = from.mul(1f - factor).add(to.mul(factor));
		}

		/// <summary>
		/// Start the tweening operation.
		/// </summary>

		static public TweenVoxel Begin (GameObject go, float duration, VoxelProperty prop)
		{
			TweenVoxel tween = Tween.Begin<TweenVoxel>(go, duration);
			tween.from = tween.property;
			tween.to = prop;

			if (duration <= 0f)
			{
				tween.Sample(1f, true);
				tween.enabled = false;
			}
			return tween;
		}
	}
}