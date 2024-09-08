using UnityEngine;
using System.Collections;
using System;
#if !UNITY_2017_2_OR_NEWER
namespace GDGeek{
	[Serializable]
	public struct Vector2Int {
		public static Vector2Int one
		{
			get 
			{
				return new Vector2Int (1, 1);
			}
		}
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
            Vector2Int v = (Vector2Int)(obj);// as Vector2Int;
            if (this.x != v.x || this.y != v.y)
            {
                return false;
            }
            return true;
        }


        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }
        public static Vector2Int zero
		{
			get
			{
				return new Vector2Int (0, 0);
			}
		}
		
		public Vector2Int(int x, int y)  
		{
			this.x = x;
			this.y = y;
		}
        
        public Vector2Int(Vector2Int v2)  
		{
			this.x = v2.x;
			this.y = v2.y;
		}
		
		public static Vector2Int operator +(Vector2Int lhs, Vector2Int rhs)
		{
			Vector2Int result = new Vector2Int(lhs);
			result.x += rhs.x;
			result.y += rhs.y;
			return result;
		}
		
		public static bool operator ==(Vector2Int lhs, Vector2Int rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y;
		}
		
		
		public static bool operator != (Vector2Int lhs, Vector2Int rhs)
		{
			return lhs.x != rhs.x || lhs.y != rhs.y ;
		}
		public int x;
		public int y;

		public override string ToString()
		{
			//重写需要的输出。
			return "("+x+","+y+")";
		}
    }
    /**/
}

#endif