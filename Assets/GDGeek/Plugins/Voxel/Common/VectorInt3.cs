using UnityEngine;
using System.Collections;
using System;

namespace GDGeek{
	[Serializable]
	public struct VectorInt3 {

		public static VectorInt3 operator-(VectorInt3 lhs, VectorInt3 rhs)
		{
			VectorInt3 to = new VectorInt3(lhs.x-rhs.x, lhs.y-rhs.y, lhs.z-rhs.z);
			return to;
		}
		public static VectorInt3 one
		{
			get
			{
				return new VectorInt3 (1, 1, 1);
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
            VectorInt3 v = (VectorInt3)(obj);// as VectorInt2;
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

        public static VectorInt3 zero
		{
			get
			{
				return new VectorInt3 (0, 0, 0);
			}
		}

		public VectorInt3(int x, int y, int z)  
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public VectorInt3(float x, float y, float z)  
		{
			this.x = Mathf.RoundToInt(x);
			this.y = Mathf.RoundToInt(y);
			this.z = Mathf.RoundToInt(z);
		}
		public VectorInt3(Vector3 v3){
			this.x = Mathf.RoundToInt(v3.x);
			this.y = Mathf.RoundToInt(v3.y);
			this.z = Mathf.RoundToInt(v3.z);
		}
		public VectorInt3(VectorInt3 v3)  
		{
			this.x = v3.x;
			this.y = v3.y;
			this.z = v3.z;
		}

		public static VectorInt3 operator +(VectorInt3 lhs, VectorInt3 rhs)
		{
			VectorInt3 result = new VectorInt3(lhs);
			result.x += rhs.x;
			result.y += rhs.y;
			result.z += rhs.z;
			return result;
		}
		
		public static bool operator ==(VectorInt3 lhs, VectorInt3 rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
		}

		
		public static bool operator !=(VectorInt3 lhs, VectorInt3 rhs)
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
}