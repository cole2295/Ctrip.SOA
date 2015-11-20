using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Routing;
using Ctrip.SOA.Web.Identity;


namespace Ctrip.SOA.Web.Attribute
{
    /// <summary>
    /// auth check
    /// </summary>
    public class AuthAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// role name
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
         
            if (cookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);

                var customidentity = new CustomIdentity(ticket);
                if (customidentity == null || !customidentity.IsInRole(Role))
                {
                    //ContentResult Content = new ContentResult();
                    //Content.Content = "<script type='text/javascript'>alert('Auth verify Failed！');history.go(-1);</script>";
                    //filterContext.Result = Content;
                    //throw new HttpException(401, null);
    

                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Error",
                        name = customidentity.Name,
                        action = "AccessDenied"

                    }));
                }


                string viewBagUsername = filterContext.Controller.ViewBag.UserName;

                if (!string.IsNullOrEmpty(viewBagUsername))
                {
                    string currentUsername = customidentity.Name;
                    if (viewBagUsername.ToLower() != currentUsername.ToLower())
                    {
                        //if (customidentity.Roles[0] == "User")
                        //{
                        //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        //    {
                        //        action = "AccessDenied",
                        //        controller = "Error"


                        //    }));
                        //}
                        //else if (customidentity.Roles[0] == "PowerUser")
                        //{
                        //    string[] users = null;
                        //    //new ProxyHelper<IMember>().Use(code =>
                        //    //{
                        //    //    users = code.GetOrgUsersByUsername(currentUsername);

                        //    //}, Constants.MEMBER_ENDPOINT);

                        //    MemberService.MemberClient memberService = new MemberService.MemberClient();
                        //    users = memberService.GetOrgUsersByUsername(currentUsername);

                        //    if(!Helpers.HasInVale(users,viewBagUsername))
                        //    {
                        //         filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        //    {
                        //        action = "AccessDenied",
                        //        controller = "Error"


                        //    }));
                        //    }
                            
                        //}
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            
                            controller = "Error",
                            name = customidentity.Name,
                            action = "AccessDenied"

                        }));
                    }
                }
                
            }
        }

        
    }
}