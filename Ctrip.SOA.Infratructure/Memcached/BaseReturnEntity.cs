using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrip.SOA.Infratructure.Memcached
{
    [Serializable]
    public class BaseReturnEntity
    {
        private string responseXML;

        /// <summary>
        /// 返回结果的XML
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string ResponseXML
        {
            get { return responseXML; }
            set { responseXML = value; }
        }

        private string requestXML;

        /// <summary>
        /// 序列化后请求的XML
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string RequestXML
        {
            get { return requestXML; }
            set { requestXML = value; }
        }

        private object _ReturnVal;
        /// <summary>
        /// 同步默认返回值
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public object ReturnVal
        {
            get { return _ReturnVal; }
            set { _ReturnVal = value; }
        }

        private bool _CallSuccess = true;
        /// <summary>
        /// 接口调用成功标记 true成功 false失败
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public bool CallSuccess
        {
            get { return _CallSuccess; }
            set { _CallSuccess = value; }
        }

        private string _ErrorMessage = "";

        /// <summary>
        /// 错误消息
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; }
        }

        private APIResultCode resultCode;

        /// <summary>
        /// 返回代码
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public APIResultCode ResultCode
        {
            get { return resultCode; }
            set { resultCode = value; }
        }

        private string errorNo;

        /// <summary>
        /// 错误代码
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string ErrorNo
        {
            get { return errorNo; }
            set { errorNo = value; }
        }

        private string referenceID;

        /// <summary>
        /// 客户流水号
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string ReferenceID
        {
            get { return referenceID; }
            set { referenceID = value; }
        }

        private string uResponseXML;

        /// <summary>
        /// 外部返回体
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string UResponseXML
        {
            get { return uResponseXML; }
            set { uResponseXML = value; }
        }

        private string culture = string.Empty;

        /// <summary>
        /// 语言
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string Culture
        {
            get { return culture; }
            set { culture = value; }
        }

        private bool isGetFromCache;

        /// <summary>
        /// 是否来自缓存
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public bool IsGetFromCache
        {
            get { return isGetFromCache; }
            set { isGetFromCache = value; }
        }

    }
}
