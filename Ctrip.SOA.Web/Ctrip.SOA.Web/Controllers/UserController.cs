using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ctrip.SOA.Bussiness.User.DataContract;
using Ctrip.SOA.Bussiness.User.Service;
using Ctrip.SOA.Infratructure.MVC;
using Ctrip.SOA.Infratructure.ServiceProxy;
using Ctrip.SOA.Infratructure.Aop;
using Ctrip.SOA.Web.Attribute;


namespace Ctrip.SOA.Web.Controllers
{
    [LogFilter]
    public class UserController : BaseController
    {
        public readonly IUserService userService = ServiceProxyFactory.CreateChannel<IUserService>();

        //[MetricElapsed("Index")]
        public ActionResult Index()
        {
            //var users = userService.GetAllUsers();

            return View();
        }

        public JsonResult GetAllUsers()
        {
            var users = userService.GetAllUsers();

            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserById(long userId)
        {
            var user = userService.GetUser(new UserModelRequest { UserId = userId });
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateUser(UserModelRequest user)
        {
            var result = userService.UpdateUser(user);
            return Json(result);
        }

        [HttpPost]
        public JsonResult AddUser(UserModelRequest user)
        {
            var result = userService.AddUser(user);
            return Json(result);
        }

        [HttpPost]
        public JsonResult DeleteUser(long userId)
        {
            var result = userService.DeleteUser(new UserModelRequest { UserId = userId });
            return Json(result);
        }
    }
}