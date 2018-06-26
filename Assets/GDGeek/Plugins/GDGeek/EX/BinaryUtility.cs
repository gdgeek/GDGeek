using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace GDGeek
{
    public static class BinaryUtility{


        public static void Write(this BinaryWriter bw, Color32 color)
        {
            bw.Write(color.r);
            bw.Write(color.g);
            bw.Write(color.b);
            bw.Write(color.a);

        }

        public static void Write(this BinaryWriter bw, Vector3 v3)
        {
            bw.Write(v3.x);
            bw.Write(v3.y);
            bw.Write(v3.z);

        }

        public static void Write(this BinaryWriter bw, Quaternion q)
        {
            bw.Write(q.x);
            bw.Write(q.y);
            bw.Write(q.z);
            bw.Write(q.w);

        }



        public static Vector3 ReadVector3(this BinaryReader br)
        {
            Vector3 v3 = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
            return v3;
        }
        public static Color32 ReadColor32(this BinaryReader br)
        {
            Color32 color = new Color32();
            color.r = br.ReadByte();
            color.g = br.ReadByte();
            color.b = br.ReadByte();
            color.a = br.ReadByte();

            return color;

        }

        public static Quaternion ReadQuaternion(this BinaryReader br)
        {
            Quaternion q = new Quaternion(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
            return q;
        }

    }
}
