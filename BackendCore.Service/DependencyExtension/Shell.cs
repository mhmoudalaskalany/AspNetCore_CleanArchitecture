using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BackendCore.Service.DependencyExtension
{
    public class Shell : IDisposable
    {
        private IServiceProvider _rootProvider;
        protected static Shell App;

        public static IServiceProvider RootInjector => App.RootProvider;

        public Shell()
        {
            
        }
        public virtual void ConfigureHttp(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _rootProvider = app.ApplicationServices;
        }
        public virtual void Dispose()
        {

        }


        protected virtual IServiceProvider RootProvider
        {
            get { return _rootProvider ??= MakeProvider(); }
        }

        private static IServiceProvider MakeProvider()
        {
            ServiceCollection collection = new ServiceCollection();
            // register services here
            return collection.BuildServiceProvider();
        }


        public static IServiceScope GetScope()
        {
            var sc = RootInjector.CreateScope();
            return sc;
        }

        public static void Start(Shell shell)
        {
            App = shell;
        }
    }
}
