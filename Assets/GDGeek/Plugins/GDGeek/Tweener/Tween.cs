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

/// <summary>
/// Base class for all tweening operations.
/// </summary>
namespace GDGeek{
public abstract class Tween : IgnoreTimeScale
{
	
	private delegate float EasingFunction(float start, float end, float value);
	
	public enum Method
	{
		Linear,
		EaseIn,
		EaseOut,
		EaseInOut,
		BounceIn,
		BounceOut,
		
		
		easeInQuad,
		easeOutQuad,
		easeInOutQuad,
		easeInCubic,
		easeOutCubic,
		easeInOutCubic,
		easeInQuart,
		easeOutQuart,
		easeInOutQuart,
		easeInQuint,
		easeOutQuint,
		easeInOutQuint,
		easeInSine,
		easeOutSine,
		easeInOutSine,
		easeInExpo,
		easeOutExpo,
		easeInOutExpo,
		easeInCirc,
		easeOutCirc,
		easeInOutCirc,
		//linear,
		spring,
		/* GFX47 MOD START */
		//bounce,
		easeInBounce,
		easeOutBounce,
		easeInOutBounce,
		/* GFX47 MOD END */
		easeInBack,
		easeOutBack,
		easeInOutBack,
		/* GFX47 MOD START */
		//elastic,
		easeInElastic,
		easeOutElastic,
		easeInOutElastic,
		/* GFX47 MOD END */
		punch
	}
	
	

	
	public enum Style
	{
		Once,
		Loop,
		PingPong,
	}

	public delegate void OnFinished (Tween tween);

	/// <summary>
	/// Delegate for subscriptions. Faster than using the 'eventReceiver' and allows for multiple receivers.
	/// </summary>

	public OnFinished onFinished;

	/// <summary>
	/// Tweening method used.
	/// </summary>

	public Method method = Method.Linear;

	/// <summary>
	/// Does it play once? Does it loop?
	/// </summary>

	public Style style = Style.Once;

	/// <summary>
	/// Optional curve to apply to the tween's time factor value.
	/// </summary>

	public AnimationCurve animationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));

	/// <summary>
	/// Whether the tween will ignore the timescale, making it work while the game is paused.
	/// </summary>

	//public bool ignoreTimeScale = fa;
	public bool doTimeScale = true;
	/// <summary>
	/// How long will the tweener wait before starting the tween?
	/// </summary>

	public float delay = 0f;

	/// <summary>
	/// How long is the duration of the tween?
	/// </summary>

	public float duration = 1f;

	/// <summary>
	/// Whether the tweener will use steeper curves for ease in / out style interpolation.
	/// </summary>

	public bool steeperCurves = false;

	/// <summary>
	/// Used by buttons and tween sequences. Group of '0' means not in a sequence.
	/// </summary>

	public int tweenGroup = 0;

	/// <summary>
	/// Target used with 'callWhenFinished', or this game object if none was specified.
	/// </summary>

	public GameObject eventReceiver;

	/// <summary>
	/// Name of the function to call when the tween finishes.
	/// </summary>

	public string callWhenFinished;

	private bool started_ = false;
	private float startTime_ = 0f;
	private float duration_ = 0f;
	private float amountPerDelta_ = 1f;
	private float factor_ = 0f;

	/// <summary>
	/// Amount advanced per delta time.
	/// </summary>

	public float amountPerDelta
	{
		get
		{
			if (duration_ != duration)
			{
				duration_ = duration;
				amountPerDelta_ = Mathf.Abs((duration > 0f) ? 1f / duration : 1000f);
			}
			return amountPerDelta_;
		}
	}

	/// <summary>
	/// Tween factor, 0-1 range.
	/// </summary>

	public float tweenFactor { get { return factor_; } }

	/// <summary>
	/// Direction that the tween is currently playing in.
	/// </summary>

//	public AnimationOrTween.Direction direction { get { return mAmountPerDelta < 0f ? AnimationOrTween.Direction.Reverse : AnimationOrTween.Direction.Forward; } }

	/// <summary>
	/// Update as soon as it's started so that there is no delay.
	/// </summary>

	void Start () { Update(); }

	/// <summary>
	/// Update the tweening factor and call the virtual update function.
	/// </summary>

	void Update ()
	{
		float delta = doTimeScale ? Time.deltaTime : UpdateRealTimeDelta() ;
		float time = doTimeScale ? Time.time : realTime ;


		if (!started_)
		{
			started_ = true;
			startTime_ = time + delay;
		}

		if (time < startTime_) return;

		// Advance the sampling factor
		factor_ += amountPerDelta * delta;

		// Loop style simply resets the play factor after it exceeds 1.
		if (style == Style.Loop)
		{
			if (factor_ > 1f)
			{
				factor_ -= Mathf.Floor(factor_);
			}
		}
		else if (style == Style.PingPong)
		{
			// Ping-pong style reverses the direction
			if (factor_ > 1f)
			{
				factor_ = 1f - (factor_ - Mathf.Floor(factor_));
				amountPerDelta_ = -amountPerDelta_;
			}
			else if (factor_ < 0f)
			{
				factor_ = -factor_;
				factor_ -= Mathf.Floor(factor_);
				amountPerDelta_ = -amountPerDelta_;
			}
		}

		// If the factor goes out of range and this is a one-time tweening operation, disable the script
		if ((style == Style.Once) && (factor_ > 1f || factor_ < 0f))
		{
			factor_ = Mathf.Clamp01(factor_);
			Sample(factor_, true);

			// Notify the listener delegate
			if (onFinished != null) onFinished(this);

			// Notify the event listener target
			if (eventReceiver != null && !string.IsNullOrEmpty(callWhenFinished))
			{
				eventReceiver.SendMessage(callWhenFinished, this, SendMessageOptions.DontRequireReceiver);
			}

			// Disable this script unless the function calls above changed something
			if (factor_ == 1f && amountPerDelta_ > 0f || factor_ == 0f && amountPerDelta_ < 0f)
			{
				enabled = false;
			}
		}
		else Sample(factor_, false);
	}

	/// <summary>
	/// Mark as not started when finished to enable delay on next play.
	/// </summary>

	void OnDisable () { started_ = false; }

	/// <summary>
	/// Sample the tween at the specified factor.
	/// </summary>

	public void Sample (float factor, bool isFinished)
	{
		// Calculate the sampling value
		float val = Mathf.Clamp01(factor);

		if (method == Method.EaseIn)
		{
			val = 1f - Mathf.Sin(0.5f * Mathf.PI * (1f - val));
			if (steeperCurves) val *= val;
		}
		else if (method == Method.EaseOut)
		{
			val = Mathf.Sin(0.5f * Mathf.PI * val);

			if (steeperCurves)
			{
				val = 1f - val;
				val = 1f - val * val;
			}
		}
		else if (method == Method.EaseInOut)
		{
			const float pi2 = Mathf.PI * 2f;
			val = val - Mathf.Sin(val * pi2) / pi2;

			if (steeperCurves)
			{
				val = val * 2f - 1f;
				float sign = Mathf.Sign(val);
				val = 1f - Mathf.Abs(val);
				val = 1f - val * val;
				val = sign * val * 0.5f + 0.5f;
			}
		}
		else if (method == Method.BounceIn)
		{
			val = BounceLogic(val);
		}
		else if (method == Method.BounceOut)
		{
			val = 1f - BounceLogic(1f - val);
		}else if(method != Method.Linear){
			
			EasingFunction ease = GetEasingFunction();
			val = ease(0, 1, val);
		}
		OnUpdate((animationCurve != null) ? animationCurve.Evaluate(val) : val, isFinished);
	}

	/// <summary>
	/// Main Bounce logic to simplify the Sample function
	/// </summary>
	
	float BounceLogic (float val)
	{
		if (val < 0.363636f) // 0.363636 = (1/ 2.75)
		{
			val = 7.5685f * val * val;
		}
		else if (val < 0.727272f) // 0.727272 = (2 / 2.75)
		{
			val = 7.5625f * (val -= 0.545454f) * val + 0.75f; // 0.545454f = (1.5 / 2.75) 
		}
		else if (val < 0.909090f) // 0.909090 = (2.5 / 2.75) 
		{
			val = 7.5625f * (val -= 0.818181f) * val + 0.9375f; // 0.818181 = (2.25 / 2.75) 
		}
		else
		{
			val = 7.5625f * (val -= 0.9545454f) * val + 0.984375f; // 0.9545454 = (2.625 / 2.75) 
		}
		return val;
	}

	/// <summary>
	/// Manually activate the tweening process, reversing it if necessary.
	/// </summary>

	public void Play (bool forward)
	{
		amountPerDelta_ = Mathf.Abs(amountPerDelta);
		if (!forward) amountPerDelta_ = -amountPerDelta_;
		enabled = true;
	}

	/// <summary>
	/// Manually reset the tweener's state to the beginning.
	/// </summary>

	public void Reset () { started_ = false; factor_ = (amountPerDelta_ < 0f) ? 1f : 0f; Sample(factor_, false); }

	/// <summary>
	/// Manually start the tweening process, reversing its direction.
	/// </summary>

	public void Toggle ()
	{
		if (factor_ > 0f)
		{
			amountPerDelta_ = -amountPerDelta;
		}
		else
		{
			amountPerDelta_ = Mathf.Abs(amountPerDelta);
		}
		enabled = true;
	}

	/// <summary>
	/// Actual tweening logic should go here.
	/// </summary>

	abstract protected void OnUpdate (float factor, bool isFinished);

	/// <summary>
	/// Starts the tweening operation.
	/// </summary>

	static public T Begin<T> (GameObject go, float duration) where T : Tween
	{
		T comp = go.GetComponent<T>();

		if (comp == null) comp = go.AddComponent<T>();

		comp.started_ = false;
		comp.duration = duration;
		comp.factor_ = 0f;
		comp.amountPerDelta_ = Mathf.Abs(comp.amountPerDelta_);
		comp.style = Style.Once;
		comp.animationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));
		comp.eventReceiver = null;
		comp.callWhenFinished = null;
		comp.onFinished = null;
		comp.enabled = true;
		return comp;
	}
	
	
	
	
	
	
	
	
	
	public  static float linear(float start, float end, float value){
		return Mathf.Lerp(start, end, value);
	}
	
	public  static float clerp(float start, float end, float value){
		float min = 0.0f;
		float max = 360.0f;
		float half = Mathf.Abs((max - min) / 2.0f);
		float retval = 0.0f;
		float diff = 0.0f;
		if ((end - start) < -half){
			diff = ((max - start) + end) * value;
			retval = start + diff;
		}else if ((end - start) > half){
			diff = -((max - end) + start) * value;
			retval = start + diff;
		}else retval = start + (end - start) * value;
		return retval;
    }

	public  static float spring(float start, float end, float value){
		value = Mathf.Clamp01(value);
		value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
		return start + (end - start) * value;
	}

	public static float easeInQuad(float start, float end, float value){
		end -= start;
		return end * value * value + start;
	}

	public  static float easeOutQuad(float start, float end, float value){
		end -= start;
		return -end * value * (value - 2) + start;
	}

	public  static float easeInOutQuad(float start, float end, float value){
		value /= .5f;
		end -= start;
		if (value < 1) return end / 2 * value * value + start;
		value--;
		return -end / 2 * (value * (value - 2) - 1) + start;
	}

	public  static float easeInCubic(float start, float end, float value){
		end -= start;
		return end * value * value * value + start;
	}

	public static  float easeOutCubic(float start, float end, float value){
		value--;
		end -= start;
		return end * (value * value * value + 1) + start;
	}

	public  static float easeInOutCubic(float start, float end, float value){
		value /= .5f;
		end -= start;
		if (value < 1) return end / 2 * value * value * value + start;
		value -= 2;
		return end / 2 * (value * value * value + 2) + start;
	}

	public  static  float easeInQuart(float start, float end, float value){
		end -= start;
		return end * value * value * value * value + start;
	}

	public  static float easeOutQuart(float start, float end, float value){
		value--;
		end -= start;
		return -end * (value * value * value * value - 1) + start;
	}

	public  static float easeInOutQuart(float start, float end, float value){
		value /= .5f;
		end -= start;
		if (value < 1) return end / 2 * value * value * value * value + start;
		value -= 2;
		return -end / 2 * (value * value * value * value - 2) + start;
	}

	public  static float easeInQuint(float start, float end, float value){
		end -= start;
		return end * value * value * value * value * value + start;
	}

	public  static float easeOutQuint(float start, float end, float value){
		value--;
		end -= start;
		return end * (value * value * value * value * value + 1) + start;
	}

	public  static float easeInOutQuint(float start, float end, float value){
		value /= .5f;
		end -= start;
		if (value < 1) return end / 2 * value * value * value * value * value + start;
		value -= 2;
		return end / 2 * (value * value * value * value * value + 2) + start;
	}

	public  static float easeInSine(float start, float end, float value){
		end -= start;
		return -end * Mathf.Cos(value / 1 * (Mathf.PI / 2)) + end + start;
	}

	public  static float easeOutSine(float start, float end, float value){
		end -= start;
		return end * Mathf.Sin(value / 1 * (Mathf.PI / 2)) + start;
	}

	public static  float easeInOutSine(float start, float end, float value){
		end -= start;
		return -end / 2 * (Mathf.Cos(Mathf.PI * value / 1) - 1) + start;
	}

	public  static float easeInExpo(float start, float end, float value){
		end -= start;
		return end * Mathf.Pow(2, 10 * (value / 1 - 1)) + start;
	}

	public  static float easeOutExpo(float start, float end, float value){
		end -= start;
		return end * (-Mathf.Pow(2, -10 * value / 1) + 1) + start;
	}

	public  static float easeInOutExpo(float start, float end, float value){
		value /= .5f;
		end -= start;
		if (value < 1) return end / 2 * Mathf.Pow(2, 10 * (value - 1)) + start;
		value--;
		return end / 2 * (-Mathf.Pow(2, -10 * value) + 2) + start;
	}

	public static  float easeInCirc(float start, float end, float value){
		end -= start;
		return -end * (Mathf.Sqrt(1 - value * value) - 1) + start;
	}

	public  static float easeOutCirc(float start, float end, float value){
		value--;
		end -= start;
		return end * Mathf.Sqrt(1 - value * value) + start;
	}

	public  static float easeInOutCirc(float start, float end, float value){
		value /= .5f;
		end -= start;
		if (value < 1) return -end / 2 * (Mathf.Sqrt(1 - value * value) - 1) + start;
		value -= 2;
		return end / 2 * (Mathf.Sqrt(1 - value * value) + 1) + start;
	}

	/* GFX47 MOD START */
	public static  float easeInBounce(float start, float end, float value){
		end -= start;
		float d = 1f;
		return end - easeOutBounce(0, end, d-value) + start;
	}
	/* GFX47 MOD END */

	/* GFX47 MOD START */
	//private float bounce(float start, float end, float value){
	public static  float easeOutBounce(float start, float end, float value){
		value /= 1f;
		end -= start;
		if (value < (1 / 2.75f)){
			return end * (7.5625f * value * value) + start;
		}else if (value < (2 / 2.75f)){
			value -= (1.5f / 2.75f);
			return end * (7.5625f * (value) * value + .75f) + start;
		}else if (value < (2.5 / 2.75)){
			value -= (2.25f / 2.75f);
			return end * (7.5625f * (value) * value + .9375f) + start;
		}else{
			value -= (2.625f / 2.75f);
			return end * (7.5625f * (value) * value + .984375f) + start;
		}
	}
	/* GFX47 MOD END */

	/* GFX47 MOD START */
	public static  float easeInOutBounce(float start, float end, float value){
		end -= start;
		float d = 1f;
		if (value < d/2) return easeInBounce(0, end, value*2) * 0.5f + start;
		else return easeOutBounce(0, end, value*2-d) * 0.5f + end*0.5f + start;
	}
	/* GFX47 MOD END */

	private  static float easeInBack(float start, float end, float value){
		end -= start;
		value /= 1;
		float s = 1.70158f;
		return end * (value) * value * ((s + 1) * value - s) + start;
	}

	public  static float easeOutBack(float start, float end, float value){
		float s = 1.70158f;
		end -= start;
		value = (value / 1) - 1;
		return end * ((value) * value * ((s + 1) * value + s) + 1) + start;
	}

	public  static float easeInOutBack(float start, float end, float value){
		float s = 1.70158f;
		end -= start;
		value /= .5f;
		if ((value) < 1){
			s *= (1.525f);
			return end / 2 * (value * value * (((s) + 1) * value - s)) + start;
		}
		value -= 2;
		s *= (1.525f);
		return end / 2 * ((value) * value * (((s) + 1) * value + s) + 2) + start;
	}

	public  static float punch(float amplitude, float value){
		float s = 9;
		if (value == 0){
			return 0;
		}
		if (value == 1){
			return 0;
		}
		float period = 1 * 0.3f;
		s = period / (2 * Mathf.PI) * Mathf.Asin(0);
		return (amplitude * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * 1 - s) * (2 * Mathf.PI) / period));
    }
	public static float liner(float start, float end, float value){
		return  start * (1f - value) + end * value;
	}
	/* GFX47 MOD START */
	public  static float easeInElastic(float start, float end, float value){
		end -= start;
		
		float d = 1f;
		float p = d * .3f;
		float s = 0;
		float a = 0;
		
		if (value == 0) return start;
		
		if ((value /= d) == 1) return start + end;
		
		if (a == 0f || a < Mathf.Abs(end)){
			a = end;
			s = p / 4;
			}else{
			s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
		}
		
		return -(a * Mathf.Pow(2, 10 * (value-=1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p)) + start;
	}		
	/* GFX47 MOD END */

	/* GFX47 MOD START */
	//private float elastic(float start, float end, float value){
	public  static float easeOutElastic(float start, float end, float value){
	/* GFX47 MOD END */
		//Thank you to rafael.marteleto for fixing this as a port over from Pedro's UnityTween
		end -= start;
		
		float d = 1f;
		float p = d * .3f;
		float s = 0;
		float a = 0;
		
		if (value == 0) return start;
		
		if ((value /= d) == 1) return start + end;
		
		if (a == 0f || a < Mathf.Abs(end)){
			a = end;
			s = p / 4;
			}else{
			s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
		}
		
		return (a * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) + end + start);
	}		
	
	/* GFX47 MOD START */
	public static float easeInOutElastic(float start, float end, float value){
		end -= start;
		
		float d = 1f;
		float p = d * .3f;
		float s = 0;
		float a = 0;
		
		if (value == 0) return start;
		
		if ((value /= d/2) == 2) return start + end;
		
		if (a == 0f || a < Mathf.Abs(end)){
			a = end;
			s = p / 4;
			}else{
			s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
		}
		
		if (value < 1) return -0.5f * (a * Mathf.Pow(2, 10 * (value-=1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p)) + start;
		return a * Mathf.Pow(2, -10 * (value-=1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) * 0.5f + end + start;
	}		
	/* GFX47 MOD END */
	
	
	public static float easeIt(Method method, float start, float end, float value){
		float ret = value;
		switch (method){
		case Method.easeInQuad:
			ret  = easeInQuad(start, end, value);
			break;
		case Method.easeOutQuad:
			ret  = easeOutQuad(start, end, value);
			break;
		case Method.easeInOutQuad:
			ret  = easeInOutQuad(start, end, value);
			break;
		case Method.easeInCubic:
			ret  = easeInCubic(start, end, value);
			break;
		case Method.easeOutCubic:
			ret  = easeOutCubic(start, end, value);
			break;
		case Method.easeInOutCubic:
			ret  = easeInOutCubic(start, end, value);
			break;
		case Method.easeInQuart:
			ret  = easeInQuart(start, end, value);
			break;
		case Method.easeOutQuart:
			ret  = easeOutQuart(start, end, value);
			break;
		case Method.easeInOutQuart:
			ret  = easeInOutQuart(start, end, value);
			break;
		case Method.easeInQuint:
			ret  = easeInQuint(start, end, value);
			break;
		case Method.easeOutQuint:
			ret  = easeOutQuint(start, end, value);
			break;
		case Method.easeInOutQuint:
			ret  = easeInOutQuint(start, end, value);
			break;
		case Method.easeInSine:
			ret  = easeInSine(start, end, value);
			break;
		case Method.easeOutSine:
			ret  = easeOutSine(start, end, value);
			break;
		case Method.easeInOutSine:
			ret  = easeInOutSine(start, end, value);
			break;
		case Method.easeInExpo:
			ret  = easeInExpo(start, end, value);
			break;
		case Method.easeOutExpo:
			ret  = easeOutExpo(start, end, value);
			break;
		case Method.easeInOutExpo:
			ret  = easeInOutExpo(start, end, value);
			break;
		case Method.easeInCirc:
			ret  = easeInCirc(start, end, value);
			break;
		case Method.easeOutCirc:
			ret  = easeOutCirc(start, end, value);
			break;
		case Method.easeInOutCirc:
			ret  = easeInOutCirc(start, end, value);
			break;
		case Method.Linear:
			ret  = linear(start, end, value);
			break;
		case Method.spring:
			ret  = spring(start, end, value);
			break;
		/* GFX47 MOD START */
		/*case EaseType.bounce:
			ret  = bounce);
			break;*/
		case Method.easeInBounce:
			ret  = easeInBounce(start, end, value);
			break;
		case Method.easeOutBounce:
			ret  = easeOutBounce(start, end, value);
			break;
		case Method.easeInOutBounce:
			ret  = easeInOutBounce(start, end, value);
			break;
		/* GFX47 MOD END */
		case Method.easeInBack:
			ret  = easeInBack(start, end, value);
			break;
		case Method.easeOutBack:
			ret  = easeOutBack(start, end, value);
			break;
		case Method.easeInOutBack:
			ret  = easeInOutBack(start, end, value);
			break;
		/* GFX47 MOD START */
		/*case EaseType.elastic:
			ret  = elastic);
			break;*/
		case Method.easeInElastic:
			ret  = easeInElastic(start, end, value);
			break;
		case Method.easeOutElastic:
			ret  = easeOutElastic(start, end, value);
			break;
		case Method.easeInOutElastic:
			ret  = easeInOutElastic(start, end, value);
			break;
		/* GFX47 MOD END */
		}
		return ret;
	}
	
	EasingFunction GetEasingFunction(){
		
		EasingFunction ease = null;
		switch (method){
		case Method.easeInQuad:
			ease  = new EasingFunction(easeInQuad);
			break;
		case Method.easeOutQuad:
			ease = new EasingFunction(easeOutQuad);
			break;
		case Method.easeInOutQuad:
			ease = new EasingFunction(easeInOutQuad);
			break;
		case Method.easeInCubic:
			ease = new EasingFunction(easeInCubic);
			break;
		case Method.easeOutCubic:
			ease = new EasingFunction(easeOutCubic);
			break;
		case Method.easeInOutCubic:
			ease = new EasingFunction(easeInOutCubic);
			break;
		case Method.easeInQuart:
			ease = new EasingFunction(easeInQuart);
			break;
		case Method.easeOutQuart:
			ease = new EasingFunction(easeOutQuart);
			break;
		case Method.easeInOutQuart:
			ease = new EasingFunction(easeInOutQuart);
			break;
		case Method.easeInQuint:
			ease = new EasingFunction(easeInQuint);
			break;
		case Method.easeOutQuint:
			ease = new EasingFunction(easeOutQuint);
			break;
		case Method.easeInOutQuint:
			ease = new EasingFunction(easeInOutQuint);
			break;
		case Method.easeInSine:
			ease = new EasingFunction(easeInSine);
			break;
		case Method.easeOutSine:
			ease = new EasingFunction(easeOutSine);
			break;
		case Method.easeInOutSine:
			ease = new EasingFunction(easeInOutSine);
			break;
		case Method.easeInExpo:
			ease = new EasingFunction(easeInExpo);
			break;
		case Method.easeOutExpo:
			ease = new EasingFunction(easeOutExpo);
			break;
		case Method.easeInOutExpo:
			ease = new EasingFunction(easeInOutExpo);
			break;
		case Method.easeInCirc:
			ease = new EasingFunction(easeInCirc);
			break;
		case Method.easeOutCirc:
			ease = new EasingFunction(easeOutCirc);
			break;
		case Method.easeInOutCirc:
			ease = new EasingFunction(easeInOutCirc);
			break;
		case Method.Linear:
			ease = new EasingFunction(linear);
			break;
		case Method.spring:
			ease = new EasingFunction(spring);
			break;
		/* GFX47 MOD START */
		/*case EaseType.bounce:
			ease = new EasingFunction(bounce);
			break;*/
		case Method.easeInBounce:
			ease = new EasingFunction(easeInBounce);
			break;
		case Method.easeOutBounce:
			ease = new EasingFunction(easeOutBounce);
			break;
		case Method.easeInOutBounce:
			ease = new EasingFunction(easeInOutBounce);
			break;
		/* GFX47 MOD END */
		case Method.easeInBack:
			ease = new EasingFunction(easeInBack);
			break;
		case Method.easeOutBack:
			ease = new EasingFunction(easeOutBack);
			break;
		case Method.easeInOutBack:
			ease = new EasingFunction(easeInOutBack);
			break;
		/* GFX47 MOD START */
		/*case EaseType.elastic:
			ease = new EasingFunction(elastic);
			break;*/
		case Method.easeInElastic:
			ease = new EasingFunction(easeInElastic);
			break;
		case Method.easeOutElastic:
			ease = new EasingFunction(easeOutElastic);
			break;
		case Method.easeInOutElastic:
			ease = new EasingFunction(easeInOutElastic);
			break;
		/* GFX47 MOD END */
		}
		return ease;
	}
}
}
