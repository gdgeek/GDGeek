using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek { 
    public interface ITaskByName  {
        Task getTask(string name);
    }
}