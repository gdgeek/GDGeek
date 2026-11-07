using System;
using System.Buffers.Binary;
using UnityEngine;
public class UUIDToNumberConverter
{
    /// <summary>
    /// 将UUID字符串转换为包含两个64位ulong的Tuple（高64位和低64位，大端序）
    /// </summary>
    /// <param name="uuid">输入：标准UUID字符串</param>
    /// <returns>输出：包含高64位和低64位的Tuple&lt;ulong, ulong&gt;</returns>
    public static Tuple<ulong, ulong> CompressToNumber(string uuid)
    {
        if (!Guid.TryParse(uuid, out Guid guid))
        {
            Debug.LogError("无效的UUID格式: " + uuid);
            return null;
           // throw new ArgumentException("无效的UUID格式", nameof(uuid));
        }

        byte[] uuidBytes = GetBigEndianBytes(guid);
        ulong high = BinaryPrimitives.ReadUInt64BigEndian(uuidBytes.AsSpan(0, 8));
        ulong low = BinaryPrimitives.ReadUInt64BigEndian(uuidBytes.AsSpan(8, 8));
        return Tuple.Create(high, low); // 返回Tuple类型
    }

    /// <summary>
    /// 将包含两个64位ulong的Tuple转换回UUID字符串
    /// </summary>
    /// <param name="ulongTuple">输入：包含高64位和低64位的Tuple&lt;ulong, ulong&gt;</param>
    /// <returns>输出：标准UUID字符串</returns>
    public static string DecompressFromNumber(Tuple<ulong, ulong> number)
    {
        if (number == null)
        {
            Debug.LogError("输入的Tuple不能为null");
            return null;
            //throw new ArgumentNullException(nameof(number), "输入的Tuple不能为null");
        }

        byte[] uuidBytes = new byte[16];
        // 从Tuple中获取高64位（Item1）和低64位（Item2）
        BinaryPrimitives.WriteUInt64BigEndian(uuidBytes.AsSpan(0, 8), number.Item1);
        BinaryPrimitives.WriteUInt64BigEndian(uuidBytes.AsSpan(8, 8), number.Item2);
        
        Guid guid = new Guid(ToGuidEndianBytes(uuidBytes));
        return guid.ToString();
    }

    // 字节序转换方法保持不变
    private static byte[] GetBigEndianBytes(Guid guid)
    {
        byte[] guidBytes = guid.ToByteArray();
        Array.Reverse(guidBytes, 0, 4);
        Array.Reverse(guidBytes, 4, 2);
        Array.Reverse(guidBytes, 6, 2);
        return guidBytes;
    }

    private static byte[] ToGuidEndianBytes(byte[] bigEndianBytes)
    {
        byte[] guidBytes = (byte[])bigEndianBytes.Clone();
        Array.Reverse(guidBytes, 0, 4);
        Array.Reverse(guidBytes, 4, 2);
        Array.Reverse(guidBytes, 6, 2);
        return guidBytes;
    }
}