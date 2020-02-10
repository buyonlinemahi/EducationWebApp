using HCRGUniversityMgtApp.Infrastructure.Global;
using System;
using System.Web;
using System.Web.Mvc;

namespace HCRGUniversityMgtApp.Infrastructure.ApplicationFilters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AuthorizedUserCheckAttribute : AuthorizeAttribute
    {
        private readonly bool _authorize;

        public AuthorizedUserCheckAttribute()
        {
            _authorize = true;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (_authorize)
                return httpContext.Session[GlobalConst.SessionKeys.CLIENT] != null;

            return false;
        }
    }
}
