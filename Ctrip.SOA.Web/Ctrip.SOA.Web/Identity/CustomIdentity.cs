using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace Ctrip.SOA.Web.Identity
{
    public class CustomIdentity : IIdentity, IPrincipal
    {
        private readonly FormsAuthenticationTicket _ticket;

        public CustomIdentity(FormsAuthenticationTicket ticket)
        {
            _ticket = ticket;
        }

        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public string Name
        {
            get { return _ticket.Name; }
        }

        public string UserId
        {
            get { return _ticket.UserData; }
        }

        public bool IsInRole(string role)
        {
            bool flag = true;
            if (role.Contains(","))
            {
                string[] temproles = role.Split(new char[] { ',' });
                foreach (string r in temproles)
                {
                    flag = Roles.IsUserInRole(r);
                    if (flag)
                        break;
                }

                return flag;
            }
            else
                return Roles.IsUserInRole(role);
        }

        public IIdentity Identity
        {
            get { return this; }
        }
    }
}