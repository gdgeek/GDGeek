using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek
{
    public class StringMessage : TypeMessage<string>
    {
        public StringMessage(string msg, string val) {
            this.message = msg;
            this.value = val;
        }

    }
}