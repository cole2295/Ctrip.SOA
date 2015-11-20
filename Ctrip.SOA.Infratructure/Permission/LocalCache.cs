using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using Ctrip.SOA.Infratructure.Logging;
using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.Permission
{
    /// <summary>
    /// 本地缓存类 - 将重要的内存数据(Dataset)缓存到本地文件，或者从本地文件中恢复到内存中
    /// 2012-07-24 Liuyan Created
    /// </summary>
    public class LocalCache
    {
        /// <summary>
        /// 保存Cache到本地文件
        /// </summary>
        /// <param name="ds">需要保持为本地文件的缓存数据集</param>
        /// <param name="fileName">文件名,会自动加上APPID</param>
        /// <returns>成功|失败，当缓存保持文件失败，不应该影响主流程的继续进行，因此不向上抛出异常</returns>
        public static bool SaveCacheToLocalFile(DataSet ds, string fileName)
        {
            try
            {
                string path = string.Format("{0}\\{1}-{2}.config", GetCachePath(), AppSetting.AppID, fileName);

                if (!IsOpenedFile(path))
                    ds.WriteXml(path, XmlWriteMode.WriteSchema);

                return true;
            }
            catch (Exception e)
            {
                //SysLog.WriteException("HHTravel.Base.Common.Framework.Cache,SaveCacheToLocalFile", e, fileName);
                LogHelper.WriteError("HHTravel.Base.Common.Framework.Cache,SaveCacheToLocalFile", fileName, e);
                return false;
            }
        }

        /// <summary>
        /// 从本地文件中读取Cache
        /// </summary>
        /// <param name="fileName">本地文件名</param>
        /// <returns>数据集，当缓存获取文件失败时，由于之前一般都已经发生从数据库读取数据的失败异常，因此不向上抛出此异常</returns>
        public static DataSet ReadCacheFromLocalFile(string fileName)
        {
            DataSet ds = null;
            try
            {
                string path = string.Format("{0}\\{1}-{2}.config", GetCachePath(), AppSetting.AppID, fileName);

                if (File.Exists(path))
                {
                    if (!IsOpenedFile(path))
                    {
                        ds = new DataSet();
                        ds.ReadXml(path, XmlReadMode.ReadSchema);
                    }
                }
            }
            catch (Exception e)
            {
                //SysLog.WriteException("HHTravel.Base.Common.Framework.Cache,ReadCacheFromLocalFile", e, fileName);
                LogHelper.WriteError("HHTravel.Base.Common.Framework.Cache,ReadCacheFromLocalFile", fileName, e);
                return null;
            }
            return ds;
        }

        /// <summary>
        /// 取得缓存文件的存放路径
        /// </summary>
        /// <returns></returns>
        private static string GetCachePath()
        {
            string cachepath = AppSetting.Cachepath != "" ? AppSetting.Cachepath : "d:\\Cache";
            if (!Directory.Exists(cachepath)) Directory.CreateDirectory(cachepath);
            return cachepath;
        }

        /// <summary>
        /// 判定文件是否被其它进程打开,是返回true,否返回false
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <returns></returns>
        private static bool IsOpenedFile(string filepath)
        {
            bool result = false;
            try
            {
                FileStream fs = File.OpenWrite(filepath);
                fs.Close();
            }
            catch
            {
                result = true;
            }

            return result;
        }
    }
}