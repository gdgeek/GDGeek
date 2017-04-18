using UnityEngine;
using System.Collections;
using System;

namespace GDGeek{
	[Serializable]
	public struct VectorInt4 {



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
            VectorInt4 v = (VectorInt4)(obj);// as VectorInt2;
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


        public VectorInt4(int x, int y, int z, int w)  
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}
		
		public VectorInt4(VectorInt4 v4)  
		{
			this.x = v4.x;
			this.y = v4.y;
			this.z = v4.z;
			this.w = v4.w;
		}
		
		public static VectorInt4 operator +(VectorInt4 lhs, VectorInt4 rhs)
		{
			VectorInt4 result = new VectorInt4(lhs);
			result.x += rhs.x;
			result.y += rhs.y;
			result.z += rhs.z;
			result.w += rhs.w;
			return result;
		}
		
		public static bool operator ==(VectorInt4 lhs, VectorInt4 rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z && lhs.w == rhs.w;
		}
		
		
		public static bool operator !=(VectorInt4 lhs, VectorInt4 rhs)
		{
			return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z || lhs.w != rhs.w;
		}
		//	public VectorInt3()
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