using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek { 
    public class StoryNone : Story {
      

        protected override Task createTask()
        {
            return new Task();
        }

       
    }
}