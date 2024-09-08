using System;
using UnityEngine;

namespace GDGeek
{
    public class VoxelVertex
    {
  

        public Vector3 position
        {
            get;
            set;
        }
        public Color32 color;
        public Vector3Int normal;
        public Vector2 uv;
        public override int GetHashCode() => HashCode.Combine(position, color, normal, uv);

        static public Vector2 ToVector2(Vector3Int normal, Vector3 v3)
        {
            if(normal == Vector3Int.up || normal == Vector3Int.down)
            {
                return new Vector2(v3.x,v3.z);
            }
            else if(normal == Vector3Int.left || normal == Vector3Int.right)
            {
                return new Vector2(v3.z,v3.y);
            }
            else if(normal == Vector3Int.forward || normal == Vector3Int.back)
            {
                return new Vector2(v3.x,v3.y);
            }
            else
            {
                throw new System.Exception("Invalid normal");
            }
            return Vector2.one;
        }

        public Vector2 xy => ToVector2(this.normal, this.position);
       
    }
}