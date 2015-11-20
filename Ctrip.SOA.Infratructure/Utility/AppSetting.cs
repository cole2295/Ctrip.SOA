using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Ctrip.SOA.Infratructure.Utility
{
    /// <summary>
    /// 获取AppSetting中的配置信息
    /// </summary>
    public class AppSetting
    {
        /// <summary>
        /// ESB 地址
        /// </summary>
        public static string ESBUrl = ConfigurationManager.AppSettings["ESBUrl"] == null ? string.Empty : ConfigurationManager.AppSettings["ESBUrl"].ToString();
        public static string SiteDomain = ConfigurationManager.AppSettings["SiteDomain"] == null ? string.Empty : ConfigurationManager.AppSettings["SiteDomain"].ToString();

        /// <summary>
        /// Cookie domain
        /// </summary>
        public static string CookieDomain = ConfigurationManager.AppSettings["CookieDomain"] == null ? string.Empty : ConfigurationManager.AppSettings["CookieDomain"].ToString();

        /// <summary>
        /// 英文国际机票直连查询地址
        /// </summary>
        public static string IntlFlightDirectSearchWSURL = ConfigurationManager.AppSettings["IntlFlightDirectSearchWSURL"] == null ? "" : ConfigurationManager.AppSettings["IntlFlightDirectSearchWSURL"].ToString();

        /// <summary>
        /// 切换用ESB地址
        /// </summary>
        public static string SwitchEsbUrl = ConfigurationManager.AppSettings["SwitchEsbUrl"] == null ? "" : ConfigurationManager.AppSettings["SwitchEsbUrl"].ToString();
        /// <summary>
        /// 支付平台商户id
        /// </summary>
        public static readonly int PaymentMerchantId = ConfigurationManager.AppSettings["PaymentMerchantId"] == null ? 0 : Convert.ToInt32(ConfigurationManager.AppSettings["PaymentMerchantId"]);

        /// <summary>
        /// 支付平台秘钥
        /// </summary>
        public static readonly string PaymentSecretKey = ConfigurationManager.AppSettings["PaymentSecretKey"] == null ? "" : ConfigurationManager.AppSettings["PaymentSecretKey"];

        /// <summary>
        /// 无线支付平台商户id
        /// </summary>
        public static readonly string H5MerchantId = ConfigurationManager.AppSettings["MerchantID"] == null ? "" : ConfigurationManager.AppSettings["MerchantID"];

        public static readonly string H5MerchantData = ConfigurationManager.AppSettings["MerchantData"] == null ? "" : ConfigurationManager.AppSettings["MerchantData"];

        /// <summary>
        /// 密钥
        /// </summary>
        public static string DesKey = ConfigurationManager.AppSettings["DesKey"] == null ? string.Empty : ConfigurationManager.AppSettings["DesKey"].ToString();

        /// <summary>
        /// 加密使用的盐
        /// </summary>
        public static string Salt = ConfigurationManager.AppSettings["Salt"] == null ? string.Empty : ConfigurationManager.AppSettings["Salt"].ToString();

        /// <summary>
        /// ImageESB 地址
        /// </summary>
        public static string ImageESBUrl = ConfigurationManager.AppSettings["ImageESBUrl"] == null ? "" : ConfigurationManager.AppSettings["ImageESBUrl"].ToString();
        /// <summary>
        /// Image上传保存的地址
        /// </summary>
        //public static string ImageUrl = ConfigurationManager.AppSettings["ImageUrl"] == null ? "" : ConfigurationManager.AppSettings["ImageUrl"].ToString();
        /// <summary>
        /// ESB UserID
        /// </summary>
        public static int AppID = Convert.ToInt32(ConfigurationManager.AppSettings["AppID"] == null ? string.Empty : ConfigurationManager.AppSettings["AppID"].ToString());

        /// <summary>
        /// JS、CSS文件版本号
        /// </summary>
        public static string ReleaseNo = ConfigurationManager.AppSettings["ReleaseNo"] == null ? "" : ConfigurationManager.AppSettings["ReleaseNo"].ToString();

        /// <summary>
        /// CqueryUrl
        /// </summary>
        public static string CqueryUrl = ConfigurationManager.AppSettings["CqueryUrl"] == null ? "" : ConfigurationManager.AppSettings["CqueryUrl"].ToString();
        /// <summary>
        /// The protocol for the URL, such as "http" or "https".
        /// </summary>
        public static string Protocol = "http";

        /// <summary>
        /// 是否使用压缩的js文件
        /// </summary>
        public static string UseMinFile = ConfigurationManager.AppSettings["UseMinFile"] == null ? "" : ConfigurationManager.AppSettings["UseMinFile"];
        //add by brliu 国际机票调用soa地址
        public static string IntlFlightWS = ConfigurationManager.AppSettings["IntlFlightWSURL"] == null ? "" : ConfigurationManager.AppSettings["IntlFlightWSURL"].ToString();
        //added by Allen Cetral logging 开启口令
        public static string CentralPwd = ConfigurationManager.AppSettings["CentralPwd"] == null ? "" : ConfigurationManager.AppSettings["CentralPwd"].ToString();

        public static bool Is6WTest = ConfigurationManager.AppSettings["Is6WTest"] == null ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["Is6WTest"]);
        public static bool IsUsePay = ConfigurationManager.AppSettings["IsUsePay"] == null ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["IsUsePay"]);

        public static string Cachepath = ConfigurationManager.AppSettings["Cachepath"] == null ? "" : ConfigurationManager.AppSettings["Cachepath"].ToString();

        /// <summary>
        /// 是否开启白名单支付流程 true为使用 false为不使用
        /// </summary>
        public static bool IsUseWhiteIPOrderWizard = ConfigurationManager.AppSettings["IsUseWhiteIPOrderWizard"] == null ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["IsUseWhiteIPOrderWizard"]);

        /// <summary>
        /// 是否开启白名单访问限制 true为使用 false为不使用
        /// </summary>
        public static bool IsUseWhiteIPAccess = ConfigurationManager.AppSettings["IsUseWhiteIPAccess"] == null ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["IsUseWhiteIPAccess"]);
        /// <summary>
        /// 是否开启黑名单访问限制 true为使用 false为不使用
        /// </summary>
        public static bool IsUseBlackIPAccess = ConfigurationManager.AppSettings["IsUseBlackIPAccess"] == null ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["IsUseBlackIPAccess"]);

        /// <summary>
        /// 白名单列表
        /// </summary>
        public static string WhiteAccountsList = ConfigurationManager.AppSettings["WhiteIPList"] == null ? string.Empty : ConfigurationManager.AppSettings["WhiteIPList"].ToString();
        public static string BlackAccountsList = ConfigurationManager.AppSettings["BlackIPList"] == null ? string.Empty : ConfigurationManager.AppSettings["BlackIPList"].ToString();
#if DEBUG
        /// <summary>
        /// 资源文件路径
        /// </summary>
        public static string WebResourcePath = ConfigurationManager.AppSettings["WebResourcePath"] == null ? string.Empty : ConfigurationManager.AppSettings["WebResourcePath"].ToString();
#else
        public static string WebResourcePath = ConfigurationManager.AppSettings["WebResourcePath"] == null ? string.Empty : ConfigurationManager.AppSettings["WebResourcePath"].ToString();
#endif

        public static string ShowL2Interest = ConfigurationManager.AppSettings["ShowL2Interest"] == null ? string.Empty : ConfigurationManager.AppSettings["ShowL2Interest"].ToString();
        public static readonly string ImgServerPath = ConfigurationManager.AppSettings["ImgServerPath"] ?? "http://images4.c-ctrip.com/target";

        public static readonly string MBT_ImgServerPath = ConfigurationManager.AppSettings["MBT_ImgServerPath"] ?? "http://images4.c-ctrip.com/target";

        public static string ProductPatternIDs = ConfigurationManager.AppSettings["ProductPatternIDs"] == null ? string.Empty : ConfigurationManager.AppSettings["ProductPatternIDs"].ToString();

        public static string CustomerServiceRoleIDs = ConfigurationManager.AppSettings["CustomerServiceRoleIDs"] == null ? string.Empty : ConfigurationManager.AppSettings["CustomerServiceRoleIDs"].ToString();

        /// <summary>
        /// 大区经理角色id
        /// </summary>
        public static string DomainManagerRoleID = ConfigurationManager.AppSettings["DomainManagerRoleID"] == null ? string.Empty : ConfigurationManager.AppSettings["DomainManagerRoleID"].ToString();

        /// <summary>
        /// 客服主管Groupid
        /// </summary>
        public static string CustomerManagerGroupID = ConfigurationManager.AppSettings["CustomerManagerGroupID"] == null ? string.Empty : ConfigurationManager.AppSettings["CustomerManagerGroupID"].ToString();

        public static string ValidateFailRedirectPath = ConfigurationManager.AppSettings["ValidateFailRedirectPath"] == null ? string.Empty : ConfigurationManager.AppSettings["ValidateFailRedirectPath"].ToString();

        public static bool WriteSqlLog = ConfigurationManager.AppSettings["WriteSqlLog"] == null ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["WriteSqlLog"].ToString());

        public static string DefaultShowImg = ConfigurationManager.AppSettings["DefaultShowImg"] == null ? string.Empty : ConfigurationManager.AppSettings["DefaultShowImg"].ToString();

        /// <summary>
        /// 发送预约单到相关邮箱
        /// </summary>
        public static string OrderEmailAddress = ConfigurationManager.AppSettings["OrderEmailAddress"] == null ? string.Empty : ConfigurationManager.AppSettings["OrderEmailAddress"].ToString();
        /// <summary>
        /// H33 处理雇员组
        /// </summary>
        public static string HH33GroupID = ConfigurationManager.AppSettings["HH33GroupID"] == null ? string.Empty : ConfigurationManager.AppSettings["HH33GroupID"].ToString();

        /// <summary>
        /// H33形态编号
        /// </summary>
        public static string HH33PatternID = ConfigurationManager.AppSettings["HH33PatternID"] == null ? string.Empty : ConfigurationManager.AppSettings["HH33PatternID"].ToString();

        /// <summary>
        /// 是否自动订单分配
        /// </summary>
        public static bool IsAutoAllocation = ConfigurationManager.AppSettings["IsAutoAllocation"] == null ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["IsAutoAllocation"].ToString());

        /// <summary>
        /// Unity容器名
        /// </summary>
        public static string ContainerName = ConfigurationManager.AppSettings["ContainerName"] == null ? string.Empty : ConfigurationManager.AppSettings["ContainerName"].ToString();

        /// <summary>
        /// 分配页地址
        /// </summary>
        public static string DispatchUrl = ConfigurationManager.AppSettings["DispatchUrl"] == null ? string.Empty : ConfigurationManager.AppSettings["DispatchUrl"].ToString();

        /// <summary>
        /// 订单页地址
        /// </summary>
        public static string OrderUri = ConfigurationManager.AppSettings["OrderUri"] == null ? string.Empty : ConfigurationManager.AppSettings["OrderUri"].ToString();

        /// <summary>
        /// 订单管理系统站点地址
        /// </summary>
        public static string OrderSiteRoot = ConfigurationManager.AppSettings["OrderSiteRoot"] == null ? string.Empty : ConfigurationManager.AppSettings["OrderSiteRoot"].ToString();

        /// <summary>
        /// 客服经理组
        /// </summary>
        public static string ServiceManager = ConfigurationManager.AppSettings["ServiceManager"] == null ? string.Empty : ConfigurationManager.AppSettings["ServiceManager"].ToString();

        /// <summary>
        /// 单点登录地址
        /// </summary>
        public static string SSOLoginUrl = ConfigurationManager.AppSettings["SSOLoginUrl"] == null ? string.Empty : ConfigurationManager.AppSettings["SSOLoginUrl"].ToString();

        /// <summary>
        /// 是否检查登录
        /// </summary>
        public static bool IsCheckLogin = ConfigurationManager.AppSettings["IsCheckLogin"] == null ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["IsCheckLogin"].ToString());

        /// <summary>
        /// 子系统Key
        /// </summary>
        public static string SubSystemKey = ConfigurationManager.AppSettings["SubSystemKey"] == null ? string.Empty : ConfigurationManager.AppSettings["SubSystemKey"].ToString();
        /// <summary>
        /// 返回首页地址
        /// </summary>
        public static string HomeUrl = ConfigurationManager.AppSettings["HomeUrl"] == null ? string.Empty : ConfigurationManager.AppSettings["HomeUrl"].ToString();

        /// <summary>
        /// 注销地址
        /// </summary>
        public static string LogoutUrl = ConfigurationManager.AppSettings["LogoutUrl"] == null ? string.Empty : ConfigurationManager.AppSettings["LogoutUrl"].ToString();

        /// <summary>
        /// 在线预订地址
        /// </summary>
        public static string OnlineBookingUrl = ConfigurationManager.AppSettings["OnlineBookingUrl"] == null ? string.Empty : ConfigurationManager.AppSettings["OnlineBookingUrl"].ToString();

        #region 支付平台

        public static string PaymentGateWay = ConfigurationManager.AppSettings["PaymentGateWay"] == null ? "" : ConfigurationManager.AppSettings["PaymentGateWay"].ToString();
        public static string MerchantID = ConfigurationManager.AppSettings["MerchantID"] == null ? "" : ConfigurationManager.AppSettings["MerchantID"].ToString();
        public static string MerchantKey = ConfigurationManager.AppSettings["MerchantKey"] == null ? "" : ConfigurationManager.AppSettings["MerchantKey"].ToString();

        public static string TravelMoneyType = ConfigurationManager.AppSettings["TravelMoneyType"] == null ? "" : ConfigurationManager.AppSettings["TravelMoneyType"].ToString();

        //意向单状态
        public static string IntentionState = ConfigurationManager.AppSettings["IntentionState"] == null ? "" : ConfigurationManager.AppSettings["IntentionState"].ToString();

        #endregion 支付平台

        public static string Recipient = ConfigurationManager.AppSettings["Recipient"] == null ? "" : ConfigurationManager.AppSettings["Recipient"].ToString();

        public static string CSRecipient = ConfigurationManager.AppSettings["CSRecipient"] == null ? "" : ConfigurationManager.AppSettings["CSRecipient"].ToString();

        //收款城市
        public static string MayReceiveBranch = ConfigurationManager.AppSettings["MayReceiveBranch"] == null ? "" : ConfigurationManager.AppSettings["MayReceiveBranch"].ToString();

        //收款票点
        public static string MayReceiveSite = ConfigurationManager.AppSettings["MayReceiveSite"] == null ? "" : ConfigurationManager.AppSettings["MayReceiveSite"].ToString();

        //走支付流程的账号
        public static string PayUIDList = ConfigurationManager.AppSettings["PayUIDList"] == null ? "" : ConfigurationManager.AppSettings["PayUIDList"].ToString().ToLowerInvariant().Trim();

        //旅游合同查看地址
        public static string ContractUrl = ConfigurationManager.AppSettings["ContractUrl"] == null ? "" : ConfigurationManager.AppSettings["ContractUrl"].ToString().ToLowerInvariant().Trim();

        public static string RootDomain = ConfigurationManager.AppSettings["RootDomain"] == null ? "" : ConfigurationManager.AppSettings["RootDomain"].ToString();

        //邮件头部logo地址
        public static string EmailLogoUrl = ConfigurationManager.AppSettings["EmailLogoUrl"] == null ? "" : ConfigurationManager.AppSettings["EmailLogoUrl"].ToString();

        //订单系统地址
        public static string OrderUrl = ConfigurationManager.AppSettings["OrderUrl"] == null ? "" : ConfigurationManager.AppSettings["OrderUrl"].ToString();

        /// <summary>
        /// 动态切图服务器访问路径
        /// </summary>
        public static readonly string DynamicImgServerPath = ConfigurationManager.AppSettings["DynamicImgServerPath"] ?? "http://dimg04.c-ctrip.com/images/";

        /// <summary>
        /// MBT动态切图服务器访问路径,走内网服务器
        /// </summary>
        public static readonly string MBT_DynamicImgServerPath = ConfigurationManager.AppSettings["MBT_DynamicImgServerPath"] ?? "http://dimg04.fx.ctripcorp.com/";

    }
}
