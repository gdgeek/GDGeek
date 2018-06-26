using UnityEngine;
using System.Collections;
using System;

#if !UNITY_2017_2_OR_NEWER
namespace GDGeek
{
	
	[Serializable]
	public struct Vector3Int {

		public static Vector3Int operator-(Vector3Int lhs, Vector3Int rhs)
		{
			Vector3Int to = new Vector3Int(lhs.x-rhs.x, lhs.y-rhs.y, lhs.z-rhs.z);
			return to;
		}
		public static Vector3Int one
		{
			get
			{
				return new Vector3Int (1, 1, 1);
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
            Vector3Int v = (Vector3Int)(obj);// as VectorInt2;
            if (this.x != v.x || this.y != v.y || this.z != v.z)
            {
                return false;
            }
            return true;
        }


        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
        }

        public static Vector3Int zero
		{
			get
			{
				return new Vector3Int (0, 0, 0);
			}
		}

		public Vector3Int(int x, int y, int z)  
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Vector3Int(float x, float y, float z)  
		{
			this.x = Mathf.RoundToInt(x);
			this.y = Mathf.RoundToInt(y);
			this.z = Mathf.RoundToInt(z);
		}
		public Vector3Int(Vector3 v3){
			this.x = Mathf.RoundToInt(v3.x);
			this.y = Mathf.RoundToInt(v3.y);
			this.z = Mathf.RoundToInt(v3.z);
		}
		public Vector3Int(Vector3Int v3)  
		{
			this.x = v3.x;
			this.y = v3.y;
			this.z = v3.z;
		}

		public static Vector3Int operator +(Vector3Int lhs, Vector3Int rhs)
		{
			Vector3Int result = new Vector3Int(lhs);
			result.x += rhs.x;
			result.y += rhs.y;
			result.z += rhs.z;
			return result;
		}
		
		public static bool operator ==(Vector3Int lhs, Vector3Int rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
		}

		
		public static bool operator !=(Vector3Int lhs, Vector3Int rhs)
		{
			return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z;
		}
	

		public int x;
		public int y;
		public int z;
        
		public override string ToString()
		{
			//重写需要的输出。
			return "("+x+","+y+","+z+")";
		}
	}
    /**/
}
#endif