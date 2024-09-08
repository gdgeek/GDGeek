using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek {
    public interface ITrigger {

        Task open();
        Task close();
        string triggerName { get; }
        string groupName { get; }
    }
}
