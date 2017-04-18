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

public class FilterVector3 {

	private int num_ = 0;
	private Vector3[] filters_ = null;

	public FilterVector3(){
		

	}

	public Vector3 interval(Vector3 d){

		if (this.filters_ == null) {
			filters_ = new Vector3[5];
			for(int i = 0;i<filters_.Length; ++i){
				this.filters_[i] = d;
			}
		}

		this.filters_[num_] = d;
		num_++;
		if(num_ >= 5)
			num_ = 0;
		Vector3 all = Vector3.zero;
		for(int i =0; i<5; ++i){
			all += this.filters_[i];
		}
		return (all/5);

	}
}
