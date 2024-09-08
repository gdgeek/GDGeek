using System.Collections.Generic;

namespace GDGeek
{
   
	public class BranchList:Task{
		public new static BranchList T => new BranchList();
		private List<Task> list_ = new List<Task>(); 
		private bool isOver_ = false;

		public bool forceExit
		{
			get;
			private set;
		}
		private int index_ = 0;
		
		public bool smoothly
		{
			get;
			private set;
		}
		
		private TaskRunner runner_ = null;
		public BranchList(TaskRunner runner = null){
			runner_ = runner;
			this.init = this.initImpl;
			this.isOver = this.isOverImpl;
		}
		
		public BranchList push(Task task)
		{
			//task.runner = this.runner;
			if (task is DataTask)
			{
				DataTask dt = task as DataTask;
				dt.then(delegate {
					index_++;
					runTask();
				});
				dt.error(delegate {
					smoothly = false;
					isOver_ = true;
				});
			}
			else
			{
				task.pushBack(delegate {
					index_++;
					runTask();
				});
			}


		
			list_.Add(task);
			return this;
		}
		
		void runTask ()
		{
			if (forceExit) {
				isOver_ = true;		
			} else {
				if(index_ <list_.Count){
					list_ [index_].run();
				}else{
					isOver_ = true;
				}

			}
		}
		
		public void initImpl(){
			this.isOver_ = false;
			forceExit = false;
			index_ = 0;
			smoothly = true;
			runTask ();
		}
		
		/*
		public override TaskRunner runner { get => base.runner;
			set
			{
				base.runner = this.runner;
				for(int i = 0; i < this.list_.Count; ++i){
					this.list_[i].runner = this.runner;
				}
			}
		} */

		public bool isOverImpl(){
			return this.isOver_;
		}

		public void clear()
		{
			this.list_.Clear();
		}
	};
}