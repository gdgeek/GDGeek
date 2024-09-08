using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek.Archives { 
    public interface IStorable {
     
        void load(string serialize);
    }
}