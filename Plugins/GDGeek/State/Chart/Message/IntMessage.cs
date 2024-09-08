using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek {
    public class IntMessage : TypeMessage<int>
    {

        public IntMessage(string key, int val)
        {
            message = key;
            value = val;
        }

    }
    public class UintMessage : TypeMessage<uint>
    {

        public UintMessage(string key, uint val)
        {
            message = key;
            value = val;
        }

    }
}