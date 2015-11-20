using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHInfratructure.Logging
{
    public class ExceptionHelper
    {

        public const string BuType = "HHT";

        public ExceptionHelper()
        {
            
        }

       

        /// <summary>
        /// 获取错误字符串
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static ErrorCode GetErrorCode(Exception ex)
        {
            Type type = ex.GetType();
            ErrorCode model = new ErrorCode();
            try
            {
                ErrorCodeType errorCodeType = (ErrorCodeType)Enum.Parse(typeof(ErrorCodeType), type.Name);
                ErrorType errorType = (ErrorType)Convert.ToInt32(((int)errorCodeType).ToString().Substring(0, 1));
                model.ErrorValue = string.Format("{0}{1}{2}{3}", BuType, (int)errorType, (int)BusinessType.HHTravel, (int)errorCodeType);
                model.ErrorTitle = string.Format("{0}，{1}，{2}", BusinessType.HHTravel.ToString(), errorType.ToString(), errorCodeType.ToString());
            }
            catch (Exception exp)
            {
                return null;
            }

           
            return model;
        }


        /// <summary>
        /// 业务类型
        /// </summary>
        //public BusinessType _BusinessType { get; set; }
        /// <summary>
        /// 错误类型
        /// </summary
        //public ErrorType _ErrorType { get; set; }
        /// <summary>
        /// 错误编号
        /// </summary>
        //public ErrorCodeType _ErrorTypeCode { get; set; }

        //public ErrorCode ErrorCode { get; set; }
    }



    /// <summary>
    /// 业务类型
    /// </summary>
    public enum BusinessType
    {
        /// <summary>
        /// 鸿鹄
        /// </summary>
        HHTravel = 1,
        /// <summary>
        /// 奔驰旅游
        /// </summary>
        MBT = 2,
        /// <summary>
        /// 天海游轮
        /// </summary>
        Skysea = 3,
    }
    /// <summary>
    /// 错误类型
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// 数据库错误
        /// </summary>
        SqlError = 1,
        /// <summary>
        /// 网络错误
        /// </summary>
        NetworkError = 2,
        /// <summary>
        /// IO错误
        /// </summary>
        IOError = 3,
        /// <summary>
        /// 外部接口错误
        /// </summary>
        ExternalServiceError = 4,
        /// <summary>
        /// 应用错误
        /// </summary>
        AppError = 5,
        /// <summary>
        /// 业务逻辑错误
        /// </summary>
        BusinessError = 6,

    }
    /// <summary>
    /// 错误编号 规则
    /// </summary>
    public enum ErrorCodeType
    {
        #region SqlError

        /// <summary>
        /// 数据库异常
        /// </summary>
        SqlException = 1001,

        DbException=1002,

        #endregion

        #region NetworkError

        ///// <summary>
        ///// 网站异常
        ///// </summary>
        //WebException = 2001,
        ///// <summary>
        ///// 超时
        ///// </summary>
        //TimeoutException = 2002,
        ///// <summary>
        ///// uri格式化异常
        ///// </summary>
        //UriFormatException = 2003,
        ///// <summary>
        ///// Http 侦听异常
        ///// </summary>
        //HttpListenerException = 2004,
        ///// <summary>
        ///// Cookie异常
        ///// </summary>
        //CookieException = 2005,
        ///// <summary>
        ///// Http异常
        ///// </summary>
        //HttpException = 2006,
        ///// <summary>
        ///// Http解析异常
        ///// </summary>
        //HttpParseException = 2007,
        ///// <summary>
        ///// Http请求验证异常
        ///// </summary>
        //HttpRequestValidationException = 2008,
        ///// <summary>
        ///// Http编译异常
        ///// </summary>
        //HttpCompileException = 2009,
        ///// <summary>
        ///// 参数异常
        ///// </summary>
        //ArgumentException = 2010,
        ///// <summary>
        ///// 空参数
        ///// </summary>
        //ArgumentNullException = 2011,
        ///// <summary>
        ///// 参数超出范围
        ///// </summary>
        //ArgumentOutOfRangeException = 2012,

        #endregion

        #region IOError

        ///// <summary>
        ///// IO异常
        ///// </summary>
        //IOException = 3001,
        ///// <summary>
        ///// 路径太长
        ///// </summary>
        //PathTooLongException = 3002,
        ///// <summary>
        ///// 目录未找到
        ///// </summary>
        //DirectoryNotFoundException = 3003,
        ///// <summary>
        ///// 结束流异常
        ///// </summary>
        //EndOfStreamException = 3004,
        ///// <summary>
        ///// 文件未找到
        ///// </summary>
        //FileNotFoundException = 3005,
        ///// <summary>
        ///// 文件加载异常
        ///// </summary>
        //FileLoadException = 3006,
        ///// <summary>
        ///// dll未找到
        ///// </summary>
        //DllNotFoundException = 3007,
        ///// <summary>
        ///// 字段访问异常
        ///// </summary>
        //FieldAccessException = 3008,
        ///// <summary>
        ///// 格式化异常
        ///// </summary>
        //FormatException = 3009,
        ///// <summary>
        ///// 索引超出范围
        ///// </summary>
        //IndexOutOfRangeException = 3010,
        ///// <summary>
        ///// Key未找到
        ///// </summary>
        //KeyNotFoundException = 3011,
        ///// <summary>
        ///// 成员访问异常
        ///// </summary>
        //MemberAccessException = 3012,
        ///// <summary>
        ///// 方法访问异常
        ///// </summary>
        //MethodAccessException = 3013,
        ///// <summary>
        ///// 缺少字段
        ///// </summary>
        //MissingFieldException = 3014,
        ///// <summary>
        ///// 缺少成员
        ///// </summary>
        //MissingMemberException = 3015,
        ///// <summary>
        ///// 缺少方法
        ///// </summary>
        //MissingMethodException = 3016,
        ///// <summary>
        ///// 空引用
        ///// </summary>
        //NullReferenceException = 3017,
        ///// <summary>
        ///// 对象设置异常
        ///// </summary>
        //ObjectDisposedException = 3018,
        #endregion

        #region ExternalServiceError

        ///// <summary>
        ///// Soap异常
        ///// </summary>
        //SoapException = 4001,
        ///// <summary>
        ///// Soap Header异常
        ///// </summary>
        //SoapHeaderException = 4002,
        ///// <summary>
        ///// 序列化异常
        ///// </summary>
        //SerializationException = 4003,
        ///// <summary>
        ///// Xml异常
        ///// </summary>
        //XmlException = 4004,

        #endregion

        #region AppError

        /// <summary>
        /// 应用程序异
        /// </summary>
        ApplicationException = 5001,
        ///// <summary>
        ///// 访问违规异常
        ///// </summary>
        //AccessViolationException = 5002,
        ///// <summary>
        ///// 数组类型不匹配
        ///// </summary>
        //ArrayTypeMismatchException = 5003,
        ///// <summary>
        ///// 内存不足
        ///// </summary>
        //InsufficientMemoryException = 5004,
        ///// <summary>
        ///// 无效操作
        ///// </summary>
        //InvalidOperationException = 5005,
        ///// <summary>
        ///// 无效程序
        ///// </summary>
        //InvalidProgramException = 5006,
        ///// <summary>
        ///// 内存超出
        ///// </summary>
        //OutOfMemoryException = 5007,
        ///// <summary>
        ///// 溢出
        ///// </summary>
        //OverflowException = 5008,
        ///// <summary>
        ///// 堆栈溢出
        ///// </summary>
        //StackOverflowException = 5009,
        /// <summary>
        /// 系统异常
        /// </summary>
        SystemException = 5010,

        #endregion

        #region BusinessError

        #endregion

        #region OtherError
        ///// <summary>
        ///// 其它
        ///// </summary>
        //Other = 9001

        #endregion
    }

    public class ErrorCode
    {
        /// <summary>
        /// 错误值 HHT161002
        /// </summary>
        public string ErrorValue { get; set; }
        /// <summary>
        /// 错误Title HHTravel，BusinessError，NullReference
        /// </summary>
        public string ErrorTitle { get; set; }
    }

}
