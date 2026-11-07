

using System.Text;

namespace GDGeek
{
    public static class FourCharId
    {
        public static uint hashCode(this string data)
        {
            return (uint)data.GetHashCode();
        }
        // 把字符串（取前 4 个字符，ASCII）转换为 uint（小端）
       
    }

}