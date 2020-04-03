using AspNetCoreDemo.ShareWebFramework.Sessions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AspNetCoreDemo.ShareWebFramework
{
    public abstract class BaseRazorPage<T> : RazorPage<T>
    {
        private IServiceProvider ServiceProvider => Context.RequestServices;

        public IUserSession UserSession => ServiceProvider.GetService<IUserSession>();
    }
}
