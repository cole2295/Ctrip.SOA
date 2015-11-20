using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ctrip.SOA.Web.Security;

namespace Ctrip.SOA.Web.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ValidateReHttpPostTokenAttribute : ValidateInputAttribute
    {
        public IPageTokenView PageTokenView { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateReHttpPostTokenAttribute"/> class.
        /// </summary>
        public ValidateReHttpPostTokenAttribute():base(false)
        {
            //It would be better use DI inject it.
            PageTokenView = new SessionPageTokenView();
        }

        /// <summary>
        /// Called when authorization is required.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (!PageTokenView.TokensMatch)
            {
                //log...
                throw new HttpException(403, "Invaild Http Post!You Can not Re-Post the Form!Back and Refresh!");
            }

        }
    }

    
}