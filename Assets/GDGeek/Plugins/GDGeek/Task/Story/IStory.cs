using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDGeek;
namespace GDGeek
{
    public interface IStory
    {

        GameObject target {
            get;
        }
        Task task {
            get;
        }

    }
}