using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace System.Web.Http.Description
{
    public class MyApiExplorer : ApiExplorer
    {
        private List<string> _allowedApiControllerNames = new List<string>();

        public MyApiExplorer() : base(GlobalConfiguration.Configuration)
        {
        }

        public List<string> AllowedApiControllerNames
        {
            get
            {
                return _allowedApiControllerNames;
            }
        }

        public override bool ShouldExploreController(string controllerVariableValue, HttpControllerDescriptor controllerDescriptor, IHttpRoute route)
        {
            if (_allowedApiControllerNames.Contains(controllerVariableValue))
                return base.ShouldExploreController(controllerVariableValue, controllerDescriptor, route);

            return false;
        }
    }
}