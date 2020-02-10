using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HCRGUniversityMgtApp.Infrastructure.DependencyResolution
{
       public class UnityDependencyResolver : IDependencyResolver
    {
        private readonly IUnityContainer unity;

        public UnityDependencyResolver(IUnityContainer unity)
        {
            this.unity = unity;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return this.unity.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return this.unity.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }
    }
}
