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
        public GameObjectData(GameObject go, Space type = Space.World)
        {
            active = go.activeSelf;
            name = go.name;
            transform = new TransformData(go.transform, type);

        }
    }
}