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
using System.Collections;
namespace GDGeek{
	
	public class TaskPack : Task {
	    private Task task_ = null;
	    private bool isOver_ = false;

	    public delegate Task CreateTask();
	    public new static TaskPack T => new TaskPack();
	    private TaskPack(){}
	    public TaskPack(CreateTask creator)
	    {
		    this.setup(creator);
	    }

	    public TaskPack setup(CreateTask creator)
	    {
		    this.init = () =>
		    {
			    isOver_ = false;
			    task_ = creator();
			    if(task_ == null){
				    isOver_ = true;
			    }else{
				    task_.pushBack(() =>
				    {
					    isOver_ = true;
				    }).run();
				  
			    }
		    };
		    this.isOver = () => {
			    return isOver_;
		    };
		    return this;
	    }

	}
}
