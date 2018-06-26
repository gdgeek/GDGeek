using UnityEngine;
using System.Collections;
using System;

namespace GDGeek
{
	[Serializable]
	public struct Vector4Int {



        public override bool Equals(object obj)
        {

            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            Vector4Int v = (Vector4Int)(obj);// as Vector2Int;
            if (this.x != v.x || this.y != v.y || this.z != v.z || this.w != v.w)
            {
                return false;
            }
            return true;
        }


        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode() ^ w.GetHashCode();
        }


        public Vector4Int(int x, int y, int z, int w)  
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}
		
		public Vector4Int(Vector4Int v4)  
		{
			this.x = v4.x;
			this.y = v4.y;
			this.z = v4.z;
			this.w = v4.w;
		}
		
		public static Vector4Int operator +(Vector4Int lhs, Vector4Int rhs)
		{
			Vector4Int result = new Vector4Int(lhs);
			result.x += rhs.x;
			result.y += rhs.y;
			result.z += rhs.z;
			result.w += rhs.w;
			return result;
		}
		
		public static bool operator ==(Vector4Int lhs, Vector4Int rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z && lhs.w == rhs.w;
		}
		
		
		public static bool operator !=(Vector4Int lhs, Vector4Int rhs)
		{
			return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z || lhs.w != rhs.w;
		}
		//	public Vector3Int()
		public int x;
		public int y;
		public int z;
		public int w;


		public override string ToString()
		{
			//重写需要的输出。
			return "("+x+","+y+","+z+","+w+")";
		}
	}
}