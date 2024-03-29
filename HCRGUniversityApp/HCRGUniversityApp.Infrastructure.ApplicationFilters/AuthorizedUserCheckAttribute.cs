﻿using HCRGUniversityApp.Infrastructure.Global;
using System;
using System.Web;
using System.Web.Mvc;

namespace HCRGUniversityApp.Infrastructure.ApplicationFilters
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
                return httpContext.Session[GlobalConst.SessionKeys.USER] != null;

            return false;
        }
    }
}