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
using GDGeek;
public class TaskTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TaskList tl = new TaskList ();
		Task task1 = new Task ();
		task1.init = delegate() {
			Debug.Log("this is firs task!!!");
		};
		task1.isOver = delegate() {
			return true;
		};
		tl.push (task1);
		TaskWait wait = new TaskWait ();
		wait.setAllTime (2f);
		tl.push (wait);
		Task task2 = new Task ();
		task2.init = delegate() {
			Debug.Log("this is second task!!!");
		};
		tl.push (task2);

		TaskSet mt = new TaskSet ();
		Task task3 = new Task ();
		task3.init = delegate() {
			Debug.Log("this is third task!!!");
		};
		mt.push (task3);

		Task task4 = new Task ();
		task4.init = delegate() {
			Debug.Log("this is four task!!!");
		};
		mt.push (task4);
		TaskWait wait2 = new TaskWait ();
		wait2.setAllTime (5f);
		mt.push (wait2);

		Task task5 = new Task ();
		task5.init = delegate() {
			Debug.Log("this is five task!!!");
		};
		mt.push (task5);

		tl.push (mt);


		TaskManager.Run (tl);
	}

}

