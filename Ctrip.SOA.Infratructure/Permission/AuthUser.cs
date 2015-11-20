using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ctrip.SOA.Infratructure.Permission
{
    /// <summary>
    /// 已验证用户实体
    /// </summary>
    [Serializable]
    public class AuthUser
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string userID = "";
        /// <summary>
        /// 用户代号
        /// </summary>
        public string userCode = "";
        /// <summary>
        /// 用户名
        /// </summary>
        public string userName = "";
        /// <summary>
        /// 组编号
        /// </summary>
        public string groupID = "";
        /// <summary>
        /// 部门编号
        /// </summary>
        public string departID = "";

        public bool IsLogin { get; set; }

        /// <summary>
        /// 组合验证用户的全部信息
        /// </summary>
        /// <returns></returns>
        public string GetUserIdentity()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}", userID, userCode, userName, groupID, departID);
        }

        /// <summary>
        /// 得到当前验证用户的实体类
        /// </summary>
        /// <returns></returns>
        public static AuthUser GetCurrentUser()
        {
            AuthUser user = new AuthUser();
            if (!string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
            {
                string[] s = HttpContext.Current.User.Identity.Name.Split('|');
                user.userID = s[0];
                try
                {
                    user.userCode = s[1];
                    user.userName = s[2];
                    user.groupID = s[3];
                    user.departID = s[4];
                }
                catch { }
            }
            return user;
        }
    }
}