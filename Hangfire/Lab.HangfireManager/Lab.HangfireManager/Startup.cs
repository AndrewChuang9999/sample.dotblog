﻿using System.Web.Http;
using Hangfire;
using Owin;

namespace Lab.HangfireManager
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            var config = new HttpConfiguration();
            config.Filters.Add(new ErrorHandlerAttribute());
            HangfireConfig.Register(app);
            config.Routes.MapHttpRoute("DefaultApi",
                                       "api/{controller}/{id}",
                                       new {id = RouteParameter.Optional}
                                      );

            app.UseWelcomePage("/");
            app.UseWebApi(config);
            app.UseErrorPage();

            var job = new DemoJob();
            RecurringJob.AddOrUpdate(() => Job.Send("Hi~"), Cron.Daily);
            RecurringJob.AddOrUpdate(() => job.Action(null, JobCancellationToken.Null), Cron.Daily);
        }
    }
}