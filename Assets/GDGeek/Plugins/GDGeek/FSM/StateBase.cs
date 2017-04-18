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
	public class StateBase {
		
		public delegate StateBase GetCurrState(string name);
		private string name_ = "";
		protected string fatherName_ = "";
		protected string defSubState_ = "";
		public GetCurrState getCurrState = null;

		public string name{
			get{
				return name_;
			}
			set{
				name_ = value;
			}
		}
		
		public string defSubState{
			get{
				return defSubState_;
			}
			set{
				defSubState_ = value;
			}
		}

		public string fatherName{
			get{
				return fatherName_;
			}
			set{
				fatherName_ = value;
			}
		}

		public StateBase(){
			
		}


		public virtual void start(){
			
		}

		public virtual void over(){
			
		}

		public virtual string postEvent(FSMEvent evt){
			return "";
		}
	}
}