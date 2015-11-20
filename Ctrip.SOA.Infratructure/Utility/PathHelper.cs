using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Ctrip.SOA.Infratructure.Utility
{
    public static class PathHelper
    {
        internal const int MAX_PATH_LENGTH = 260;

        /// <summary>
        /// 获取完全限定位置路径。
        /// </summary>
        /// <param name="fullPathOrRelativePath">完全限定位置路径(D:\\)或相对路径(~/Web)。</param>
        /// <returns>
        /// 如果 <paramref name="fullPathOrRelativePath"/> 是完全限定位置路径，则直接返回。<br/>
        /// 如果 <paramref name="fullPathOrRelativePath"/> 是相对路径，则返回该相对路径的对应的完全限定位置路径。
        /// </returns>
        /// <remarks><paramref name="fullPathOrRelativePath"/> 不可以是站点虚拟目录路径。</remarks>
        public static string GetFullPath(string fullPathOrRelativePath)
        {
            string path = fullPathOrRelativePath;

            if (!IsFullPath(path))
            {
                if (fullPathOrRelativePath.StartsWith("~"))
                {
                    path = path.Substring(1);
                }

                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                if (basePath.EndsWith("Bin", StringComparison.InvariantCultureIgnoreCase))
                {
                    basePath = basePath.Substring(0, basePath.Length - 3);
                }

                path = PathHelper.UrlPathCombine(basePath, path);
            }

            return path;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static string ToVirtualPath(string fullPath)
        {
            return string.Format("~/{0}", fullPath.Replace("\\", "/").Replace(AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/"), "").TrimStart(new char[] { '/' }));
        }

        /// <summary>
        /// 获取一个值，该值指示指定的路径字符串是否包含完全限定位置的文件或目录。
        /// </summary>
        /// <param name="path">要测试的路径。</param>
        /// <returns>如果 <paramref name="path"/> 包含完全限定位置的字符串（例如“C:\”），则为 true；否则为 false。</returns>
        /// <remarks>
        /// 此方法不验证路径或文件名是否存在。
        /// <para>
        /// <typeparamref name="IsFullPath"/> 为 <paramref name="path"/> 字符串（例如 “\\MyDir”和“\\MyDir\\MyFile.txt”和 “C:\\MyDir”和“C:\\MyDir\\MyFile.txt”）返回 true。
        /// 它为 <paramref name="path"/> 字符串（例如“MyDir”和“MyDir\\MyFile.txt”）返回 false。
        /// </para>
        /// </remarks>
        public static bool IsFullPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return false;

            // 磁盘目录模式 (格式：D:\\)
            Regex dirRegex = new Regex(@"^[A-Z]:(\\{1,2}([^\\/:\*\?<>\|]*))*$", RegexOptions.IgnoreCase);
            // 磁盘共享目录模式(格式：\\MyComputer)
            Regex sharedDirRegex = new Regex(@"^(\\{1,2}([^\\/:\*\?<>\|]*))*$", RegexOptions.IgnoreCase);

            return dirRegex.IsMatch(path) || sharedDirRegex.IsMatch(path);
        }

        /// <summary>
        /// 获取一个值，该值指示指定的路径字符串是否包含完全限定位置的文件。
        /// </summary>
        /// <param name="path">要测试的路径。</param>
        /// <returns>如果 <paramref name="path"/> 包含完全限定位置的字符串（例如“C:\MyFile.txt”），则为 true；否则为 false。</returns>
        /// <remarks>
        /// 此方法不验证路径或文件名是否存在。
        /// <para>
        /// <typeparamref name="IsFileFullPath"/> 为 <paramref name="path"/> 字符串（例如“\\MyDir\\MyFile.txt”和“C:\\MyDir\\MyFile.txt”）返回 true。
        /// 它为 <paramref name="path"/> 字符串（例如“MyDir”和“MyDir\\MyFile.txt”）返回 false。
        /// </para>
        /// </remarks>
        public static bool IsFileFullPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return false;

            // 磁盘目录文件模式 (格式：D:\\Test.gif)
            Regex dirRegex = new Regex(@"^[A-Z]:\\{1,2}(([^\\/:\*\?<>\|]+)\\{1,2})+([^\\/:\*\?<>\|]+)(\.[A-Z]+)$", RegexOptions.IgnoreCase);
            // 磁盘共享文件模式(格式：\\MyComputer\Test.gif)
            Regex sharedDirRegex = new Regex(@"^\\{2}(([^\\/:\*\?<>\|]+)\\{1,2})+([^\\/:\*\?<>\|]+)(\.[A-Z]+)$", RegexOptions.IgnoreCase);

            return dirRegex.IsMatch(path) || sharedDirRegex.IsMatch(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        public static string UrlPathCombine(params string[] pathArray)
        {
            if (pathArray == null || pathArray.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            foreach (string path in pathArray)
            {
                if (!string.IsNullOrWhiteSpace(path))
                {
                    if (sb.Length > 0)
                    {
                        if (!path.StartsWith("/") && !path.StartsWith(":"))
                        {
                            sb.Append("/");
                        }
                    }

                    sb.Append(path.TrimEnd('/', '\\'));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetDirectoryName(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return string.Empty;
            }

            int lastBlashPos = path.LastIndexOf('/');
            if (lastBlashPos == -1)
            {
                return path;
            }

            return path.Substring(0, lastBlashPos);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileName(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return string.Empty;
            }

            int lastBlashPos = path.LastIndexOf('/');
            if (lastBlashPos == -1)
            {
                return path;
            }

            return path.Substring(lastBlashPos + 1);
        }
    }
}
