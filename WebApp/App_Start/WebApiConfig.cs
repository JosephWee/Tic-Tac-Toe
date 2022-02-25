using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var myApiExplorer = new System.Web.Http.Description.MyApiExplorer();
            myApiExplorer.AllowedApiControllerNames.Add("TicTacToe");
            myApiExplorer.AllowedApiControllerNames.Add("TestApi");

            config.Services.Replace(
                typeof(System.Web.Http.Description.IApiExplorer),
                myApiExplorer
            );
        }
    }
}
