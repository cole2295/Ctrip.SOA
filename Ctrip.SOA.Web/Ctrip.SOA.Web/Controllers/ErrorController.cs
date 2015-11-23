using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ctrip.SOA.Web.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        /// <summary>
        /// 重复提交
        /// </summary>
        /// <returns></returns>
        public ActionResult RePostError()
        {
            return View();
        }

    }
}
