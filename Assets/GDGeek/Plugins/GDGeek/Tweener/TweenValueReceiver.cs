using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek { 
    public abstract class TweenValueReceiver : MonoBehaviour {

        public abstract void updateValue(float value);
    }
}