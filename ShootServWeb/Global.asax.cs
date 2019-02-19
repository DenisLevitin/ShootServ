﻿using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ShootServ
{
    // Примечание: Инструкции по включению классического режима IIS6 или IIS7 
    // см. по ссылке http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            LoggerConfig.RegisterLogger();
            AutofaqConfig.Configure();
            
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            
            //var binder = new ModelDateBinder(new CultureInfo("ru-RU").DateTimeFormat.ShortDatePattern);
            //ModelBinders.Binders.Add(typeof(DateTime), binder);
            //ModelBinders.Binders.Add(typeof(DateTime?), binder);
        }
    }
}