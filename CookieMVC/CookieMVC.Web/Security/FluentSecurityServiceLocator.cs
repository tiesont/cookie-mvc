using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
//
using FluentSecurity;

namespace CookieMVC.Web
{
    public class FluentSecurityServiceLocator : ISecurityServiceLocator
    {
        public object Resolve(Type typeToResolve)
        {
            return DependencyResolver.Current.GetService(typeToResolve);
        }

        public IEnumerable<object> ResolveAll(Type typeToResolve)
        {
            return DependencyResolver.Current.GetServices(typeToResolve).Cast<object>();
        }
    }
}