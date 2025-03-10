using Autofac;
using EasyLOB;
//using EasyLOB.Persistence; // Entity Framework Log
using Newtonsoft.Json;
using System;
//using System.Data.Entity.Infrastructure.Interception; // Entity Framework Log
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

// Major Events in GLOBAL.ASAX file
// http://www.c-sharpcorner.com/uploadfile/aa04e6/major-events-in-global-asax-file

namespace Northwind.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start() // !?!
        {
            // Syncfusion
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(""); // !!!

            // Autofac
            AppHelper.Setup(new ContainerBuilder());

            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Json.NET
            JsonConvert.DefaultSettings = () => AppHelper.JsonSettings;

            // NHibernate
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new NHibernateContractResolver();

            // Razor        
            ViewEngines.Engines.Clear();
            //ViewEngines.Engines.Add(new RazorViewEngine());
            ViewEngines.Engines.Add(new CustomRazorViewEngine());

            // Validation
            // /App_GlobalResources/ValidationResources.*.resx

            ClientDataTypeModelValidatorProvider.ResourceClassKey = "ValidationResources";
            DefaultModelBinder.ResourceClassKey = "ValidationResources";

#if DEBUG

            // Entity Framework Log
            //ILogManager logManager = EasyLOBHelper.GetService<ILogManager>();
            //DbInterception.Add(new NLogCommandInterceptor(logManager));

#endif
        }

        //protected void Application_End()
        //{
        //}

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["ZCulture"];
            if (cookie == null)
            {
                cookie = new HttpCookie("ZCulture");
                cookie.Value = "pt-BR"; // !?!
                cookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(cookie);
            }

            CultureInfo ci = CultureInfo.CreateSpecificCulture(cookie.Value);

            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            CultureInfo.DefaultThreadCurrentCulture = ci;
            CultureInfo.DefaultThreadCurrentUICulture = ci;

            //CultureInfo ci = new CultureInfo(cookie.Value);
            //Thread.CurrentThread.CurrentCulture = ci;
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(ci.Name);
        }

        //protected void Application_EndRequest(object sender, EventArgs e)
        //{
        //}

        //protected void Session_OnStart(Object sender, EventArgs e)
        //{
        //}

        //protected void Session_OnEnd(Object sender, EventArgs e)
        //{
        //}
    }
}