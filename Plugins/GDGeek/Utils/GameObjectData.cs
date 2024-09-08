using System;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek
{
    [Serializable]
    public class GameObjectData
    {
        public TransformData transform;
        public bool active;
        public string name;
        public GameObjectData()
        {

        }
        public GameObjectData(GameObject go) {
            active = go.activeSelf;
            name = go.name;



        }
    }
}