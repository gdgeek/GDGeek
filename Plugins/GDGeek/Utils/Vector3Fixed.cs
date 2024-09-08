using GDGeek;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek
{
    [System.Serializable]
    public class Vector3Fixed
    {

        public static bool operator ==(Vector3Fixed p1, Vector3Fixed p2)
        {
            if (null == (object)(p1) && null == (object)p2)
            {
                return true;
            }
            if (null == (object)p1 || null == (object)p2) { 
                return false;
            }
            if (p1.Equals(p2))
            {
                return true;
            }

            return false;


        }
        public static bool operator !=(Vector3Fixed p1, Vector3Fixed p2)
        {
            return !(p1 == p2);
        }



        public int x;
        public int y;
        public int z;


        public void read(Vector3 v3, float multi = 1000f)
        {
            this.x = Mathf.RoundToInt(v3.x * multi);
            this.y = Mathf.RoundToInt(v3.y * multi);
            this.z = Mathf.RoundToInt(v3.z * multi);
        }
        public void write(out Vector3 v3, float multi = 1000f)
        {
            v3 = toVector3(multi);

        }
        public Vector3 toVector3(float multi = 1000f)
        {
            return new Vector3(this.x / multi, this.y / multi, this.z / multi);
        }

        public override bool Equals(object obj)
        {
            return obj is Vector3Fixed @fixed &&
                    x == @fixed.x &&
                    y == @fixed.y &&
                    z == @fixed.z;
        }

        public override int GetHashCode()
        {
            var hashCode = 373119288;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            hashCode = hashCode * -1521134295 + z.GetHashCode();
            return hashCode;
        }
    }
       

}