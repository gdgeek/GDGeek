using System;
using System.Text;
using System.Security.Cryptography;
namespace GDGeek
{
    // 注意：MD5 不是加密哈希算法，不能用于安全相关场景
    // 但它是稳定的、分布均匀的哈希函数，适合用于生成唯一 ID、数据分片等场景
    // 如果需要更快但碰撞率略高的哈希，可以考虑 MurmurHash3（见 StableHashUtil）
    // 如果需要更慢但更安全的哈希，可以考虑 SHA256 等（见 System.Security.Cryptography.SHA256）
    // 另外，.NET 自带的 string.GetHashCode() 在不同平台和不同版本间不保证稳定性，不建议用于持久化存储或网络传输
    public static class StableHashMd5
    {
        // 返回 16 字节 MD5 结果
        public static byte[] Md5Bytes(string s)
        {

            if (s == null) return new byte[16];
            using (var md5 = MD5.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                return md5.ComputeHash(bytes);
            }
        }

        // 返回 Guid（使用前 16 字节）
        public static Guid Md5Guid(string s)
        {
            var b = Md5Bytes(s);
            if (b.Length < 16)
            {
                var buf = new byte[16];
                Array.Copy(b, 0, buf, 0, b.Length);
                return new Guid(buf);
            }
            var b16 = new byte[16];
            Array.Copy(b, 0, b16, 0, 16);
            return new Guid(b16);
        }

        // 返回 16 字节的 hex 字符串（小写）
        public static string Md5Hex(string s)
        {
            var b = Md5Bytes(s);
            return BitConverter.ToString(b).Replace("-", "").ToLowerInvariant();
        }

        // 返回两个 ulong（低 64, 高 64）
        public static (ulong low, ulong high) Md5AsUlongs(string s)
        {
            var b = Md5Bytes(s);
            if (b.Length < 16) Array.Resize(ref b, 16);
            ulong low = BitConverter.ToUInt64(b, 0);
            ulong high = BitConverter.ToUInt64(b, 8);
            return (low, high);
        }

        // 简单把 128-bit 压缩成 64-bit（异或低高部分），注意：会降低抗碰撞性
        public static ulong Md5To64(string s)
        {
            var (low, high) = Md5AsUlongs(s);
            return low ^ high;
        }

        public static ulong md5Hash(this string data)
        {
            // data.GetHashCode
            return Md5To64(data);
        }
        public static uint md5Hash32(this string data)
        {
            var b = Md5Bytes(data);
            if (b.Length < 4) return 0u;
            // 明确从 MD5 的前 4 字节取值（小端解释）
            return (uint)(b[0] | (b[1] << 8) | (b[2] << 16) | (b[3] << 24));
        }
    }
}