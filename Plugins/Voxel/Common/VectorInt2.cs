using UnityEngine;
using System.Collections;
using System;

namespace GDGeek{
	[Serializable]
	public struct VectorInt2 {
		public static VectorInt2 one
		{
			get 
			{
				return new VectorInt2 (1, 1);
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
            VectorInt2 v = (VectorInt2)(obj);// as VectorInt2;
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
        public static VectorInt2 zero
		{
			get
			{
				return new VectorInt2 (0, 0);
			}
		}
		
		public VectorInt2(int x, int y)  
		{
			this.x = x;
			this.y = y;
		}
        
        public VectorInt2(VectorInt2 v2)  
		{
			this.x = v2.x;
			this.y = v2.y;
		}
		
		public static VectorInt2 operator +(VectorInt2 lhs, VectorInt2 rhs)
		{
			VectorInt2 result = new VectorInt2(lhs);
			result.x += rhs.x;
			result.y += rhs.y;
			return result;
		}
		
		public static bool operator ==(VectorInt2 lhs, VectorInt2 rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y;
		}
		
		
		public static bool operator != (VectorInt2 lhs, VectorInt2 rhs)
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
}