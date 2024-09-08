using System;
using System.Collections;
using System.Collections.Generic;
using GDGeek.StateChart;
using UnityEngine;
namespace GDGeek {
    public abstract class MessageHandler : MonoBehaviour
    {



        public abstract LeafState invoke(FSMEvent evt);
    }
}