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

/// <summary>
/// Implements common functionality for monobehaviours that wish to have a timeScale-independent deltaTime.
/// </summary>
namespace GDGeek{
public class IgnoreTimeScale : MonoBehaviour
{
	float mRt = 0f;
	float mTimeStart = 0f;
	float mTimeDelta = 0f;
	float mActual = 0f;
	bool mTimeStarted = false;

	/// <summary>
	/// Real time of the last time UpdateRealTimeDelta() was called.
	/// </summary>

	public float realTime { get { return mRt; } }

	/// <summary>
	/// Equivalent of Time.deltaTime not affected by timeScale, provided that UpdateRealTimeDelta() was called in the Update().
	/// </summary>

	public float realTimeDelta { get { return mTimeDelta; } }

	/// <summary>
	/// Clear the started flag;
	/// </summary>

	protected virtual void OnEnable ()
	{
		mTimeStarted = true;
		mTimeDelta = 0f;
		mTimeStart = Time.realtimeSinceStartup;
	}

	/// <summary>
	/// Update the 'realTimeDelta' parameter. Should be called once per frame.
	/// </summary>

	protected float UpdateRealTimeDelta ()
	{
		mRt = Time.realtimeSinceStartup;

		if (mTimeStarted)
		{
			float delta = mRt - mTimeStart;
			mActual += Mathf.Max(0f, delta);
			mTimeDelta = 0.001f * Mathf.Round(mActual * 1000f);
			mActual -= mTimeDelta;
			if (mTimeDelta > 1f) mTimeDelta = 1f;
			mTimeStart = mRt;
		}
		else
		{
			mTimeStarted = true;
			mTimeStart = mRt;
			mTimeDelta = 0f;
		}
		return mTimeDelta;
	}
}
}