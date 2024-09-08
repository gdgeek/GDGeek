using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek
{
    public class NoneStory :Story
    {
        protected override Task createTask()
        {
            return new Task();
        }

      
    }
}