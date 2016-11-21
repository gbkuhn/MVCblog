using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MvcMovie2.Context;

namespace MvcMovie2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
          // Database.SetInitializer(new DropCreateDatabaseAlways<BlogDBContext>());
           Database.SetInitializer<BlogDBContext>(null);

          //  Database.SetInitializer<BlogDBContext>(new DropCreateDatabaseIfModelChanges<BlogDBContext>());
        }
    }
}
