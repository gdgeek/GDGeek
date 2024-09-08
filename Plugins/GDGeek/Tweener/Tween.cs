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

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Base class for all tweening operations.
/// </summary>
namespace GDGeek
{
    public abstract class Tween : IgnoreTimeScale
    {
        [SerializeField]
        private GameObject _target = null;

        protected GameObject target{
            get {
                if (_target == null) {
                    return this.gameObject;
                }
                return _target;
            }
        }

        public void complete() {

            enabled = false;
            _onFinished?.Invoke();
            _factor.reset();
        }


        [SerializeField]
        public UnityEvent _onFinished;

	 

        [SerializeField]
        public UnityEvent _onStarted;

        
        public virtual void onStarted()
        {
            if (_onStarted != null)
            {
                _onStarted.Invoke();
            }
        }

        [SerializeField]
        private Easing.Method _method = Easing.Method.Linear;

        public Easing.Method method {
            get => _method;
            set => _method = value;
        }

        /// <summary>
        /// Optional curve to apply to the tween's time factor value.
        /// </summary>
        [SerializeField]
	    private AnimationCurve _animationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));


        //public bool ignoreTimeScale = fa;
        [SerializeField]
        private bool _doTimeScale = true;

        [SerializeField]
        private float _delay = 0f;

	    /// <summary>
	    /// How long is the duration of the tween?
	    /// </summary>
        [SerializeField]
	    private float _duration = 1f;
      

        private bool started_ = false;
	    private float startTime_ = 0f;

        [SerializeField]
        private Factor _factor = new Factor();


	    /// <summary>
	    /// Update as soon as it's started so that there is no delay.
	    /// </summary>

	    protected virtual void Start () {
            Update();
        }


	    /// <summary>
	    /// Update the tweening factor and call the virtual update function.
	    /// </summary>

	    void Update ()
        {
            float delta = _doTimeScale ? Time.deltaTime : UpdateRealTimeDelta();
            float time = _doTimeScale ? Time.time : realTime;

            if (!started_)
            {
                onStarted();
                started_ = true;
                startTime_ = time + _delay;
            }


		    if (time < startTime_) return;
		    
            _factor.increase(delta/ _duration);
  
            sample(_factor.value, _factor.finished);

            if (_factor.finished)
            {
	            complete();
            }
           
        }

	    /// <summary>
	    /// Mark as not started when finished to enable delay on next play.
	    /// </summary>

	    void OnDisable () { started_ = false; }

	    /// <summary>
	    /// Sample the tween at the specified factor.
	    /// </summary>

	    public void sample (float factor, bool isFinished)
	    {
            

            Easing.Function ease = Easing.GetFunction(this.method);
            float val = ease(0, 1, factor);
            if(isFinished)
			{
				val = 1;
			}
		    onUpdate((_animationCurve != null) ? _animationCurve.Evaluate(val) : val, isFinished);
	    }

	  
	  
	   
        public void reset() {
            started_ = false;
            _factor.reset();
        }

       

	    /// <summary>
	    /// Actual tweening logic should go here.
	    /// </summary>

	    abstract protected void onUpdate (float factor, bool isFinished);

	    /// <summary>
	    /// Starts the tweening operation.
	    /// </summary>

	    static public void End<T>(GameObject go) where T : Tween
	    {
		    
		    T component = go.GetComponent<T>();
		    if (component && component.enabled)
		    {
			    component.sample(1.0f, true);
			    component.complete();
		    }

	    }

	    static public T Begin<T> (GameObject go, float duration) where T : Tween
	    {
		    T component = go.AskComponent<T>();
		   


		    component._duration = duration;
		    component._factor.style = Factor.Style.Once;
		    component._animationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));
		    component.execute();

            return component;
	    }


	    public Factor.Style style
	    {
		    set
		    {
			    this._factor.style = value;
		    }
	    }
	
        public void execute()
        {
            this.reset();
            this.enabled = true;

        }
    }
}
