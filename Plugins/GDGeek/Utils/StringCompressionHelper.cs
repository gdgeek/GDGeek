using System.IO;
using System.Text;
using Unity.SharpZipLib.GZip;
namespace GDGeek
{
    public class StringCompressionHelper
    {
        public static async System.Threading.Tasks.Task<string> CompressStringAsync(string input)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                using (MemoryStream outputStream = new MemoryStream())
                {
                    using (GZipOutputStream deflaterStream = new GZipOutputStream(outputStream))
                    {
                        deflaterStream.Write(inputBytes, 0, inputBytes.Length);
                    }
                    byte[] compressedBytes = outputStream.ToArray();
                    return System.Convert.ToBase64String(compressedBytes);
                }
            });
        }

        // 异步解压缩字符串
        public static async System.Threading.Tasks.Task<string> DecompressStringAsync(string input)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                byte[] compressedBytes = System.Convert.FromBase64String(input);

                using (MemoryStream inputStream = new MemoryStream(compressedBytes))
                {
                    using (GZipInputStream inflaterStream = new GZipInputStream(inputStream))
                    {
                        using (MemoryStream outputStream = new MemoryStream())
                        {
                            byte[] buffer = new byte[4096];
                            int bytesRead;
                            while ((bytesRead = inflaterStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                outputStream.Write(buffer, 0, bytesRead);
                            }

                            byte[] decompressedBytes = outputStream.ToArray();
                            return Encoding.UTF8.GetString(decompressedBytes);
                        }
                    }
                }
            });
        }
        // 压缩字符串
        public static string CompressString(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            using (MemoryStream outputStream = new MemoryStream())
            {
                using (GZipOutputStream deflaterStream = new GZipOutputStream(outputStream))
                {
                    deflaterStream.Write(inputBytes, 0, inputBytes.Length);
                }
                byte[] compressedBytes = outputStream.ToArray();
                return System.Convert.ToBase64String(compressedBytes);
            }
        }

        // 解压缩字符串
        public static string DecompressString(string input)
        {
            byte[] compressedBytes = System.Convert.FromBase64String(input);

            using (MemoryStream inputStream = new MemoryStream(compressedBytes))
            {
                using (GZipInputStream inflaterStream = new GZipInputStream(inputStream))
                {
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        byte[] buffer = new byte[4096];
                        int bytesRead;
                        while ((bytesRead = inflaterStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            outputStream.Write(buffer, 0, bytesRead);
                        }

                        byte[] decompressedBytes = outputStream.ToArray();
                        return Encoding.UTF8.GetString(decompressedBytes);
                    }
                }
            }
        }
    }
}
