using System;
using System.IO;

namespace GDGeek {
    /// <summary>
    /// Contains file helper functions.
    /// </summary>
    public static class FileUtils
    {
        /// <summary>
        /// Gets the short filename from a full filename.
        /// </summary>
        /// <returns>The short filename.</returns>
        /// <param name="filename">Full filename.</param>
        public static string GetShortFilename(string filename)
        {
            var indexOfBackslash = filename.LastIndexOf("\\", StringComparison.Ordinal);
            if (indexOfBackslash >= 0) {
                return filename.Substring (indexOfBackslash + 1);
            }
            int indexOfSlash = filename.LastIndexOf("/", StringComparison.Ordinal);
            if (indexOfSlash >= 0) {
                return filename.Substring (indexOfSlash + 1);
            }
            return filename;
        }

        /// <summary>
        /// Gets the file directory.
        /// </summary>
        /// <returns>The file directory.</returns>
        /// <param name="filename">Full filename.</param>
        public static string GetFileDirectory(string filename) {
            var indexOfBackslash = filename.LastIndexOf("\\", StringComparison.Ordinal);
            if (indexOfBackslash >= 0) {
                return filename.Substring (0, indexOfBackslash);
            }
            var indexOfSlash = filename.LastIndexOf("/", StringComparison.Ordinal);
            if (indexOfSlash >= 0) {
                return filename.Substring (0, indexOfSlash);
            }
            return null;
        }

        /// <summary>
        /// Gets the filename without extension.
        /// </summary>
        /// <returns>The filename without extension.</returns>
        /// <param name="filename">Full filename.</param>
        public static string GetFilenameWithoutExtension(string filename) {
            var indexOfDot = filename.LastIndexOf('.');
            if (indexOfDot < 0)
            {
                return null;
            }
            var indexOfBackslash = filename.LastIndexOf("\\", StringComparison.Ordinal);
            if (indexOfBackslash >= 0) {
                return filename.Substring (indexOfBackslash + 1, indexOfDot - indexOfBackslash - 1);
            }
            var indexOfSlash = filename.LastIndexOf("/", StringComparison.Ordinal);
            if (indexOfSlash >= 0) {
                return filename.Substring(indexOfSlash + 1, indexOfDot - indexOfSlash - 1);
            }
            return null;
        }

        /// <summary>
        /// Gets the filename extension.
        /// </summary>
        /// <returns>The filename extension.</returns>
        /// <param name="filename">Full filename.</param>
        public static string GetFileExtension(string filename) {
            var lastDot = filename.LastIndexOf('.');
            if (lastDot < 0)
            {
                return null;
            }
            return filename.Substring(lastDot).ToLowerInvariant();
        }

        /// <summary>
        /// Gets the path filename.
        /// </summary>
        /// <returns>The filename.</returns>
        /// <param name="path">Path.</param>
        public static string GetFilename (string path) {
            var filename = Path.GetFileName (path);
            if (path == filename) {
                var indexOfBackslash = path.LastIndexOf("\\", StringComparison.Ordinal);
                if (indexOfBackslash >= 0) {
                    return path.Substring (indexOfBackslash + 1);
                }
                var indexOfSlash = path.LastIndexOf("/", StringComparison.Ordinal);
                if (indexOfSlash >= 0) {
                    return path.Substring (indexOfSlash + 1);
                }
                return path;
            }
            return filename;
        }

        /// <summary>
        /// Synchronously loads the file data.
        /// </summary>
        /// <returns>The file data.</returns>
        /// <param name="filename">Filename.</param>
        public static byte[] LoadFileData(string filename) {
            try {
                if (filename == null) {
                    return new byte[0];
                }
                return File.ReadAllBytes(filename.Replace('\\', '/'));
            } catch (Exception) {
                return new byte[0];
            }
        }

        /// <summary>
        /// Creates a file stream for given filename.
        /// </summary>
        /// <returns>The created FileStream.</returns>
        /// <param name="filename">Filename.</param>
        public static FileStream LoadFileStream(string filename)
        {
            try
            {
                if (filename == null)
                {
                    return null;
                }
             //   File.OpenWrite()
                //    File.Exists
                return File.OpenRead(filename.Replace('\\', '/'));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

