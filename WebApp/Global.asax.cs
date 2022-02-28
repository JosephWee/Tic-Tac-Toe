using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApp.Models;

namespace WebApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            TicTacToe.BusinessLogic.ComputerPlayerConfig.RegisterComputerPlayer(new TicTacToe.BusinessLogic.ComputerPlayerV2());

            string MLNetModelPath =
                System.Configuration.ConfigurationManager.AppSettings["MLNetModelPath"];

            TicTacToe.ML.MLModel1.SetMLNetModelPath(
                Path.Combine(MLNetModelPath, "MLModel1.zip")
            );

            TicTacToe.ML.MLModel2.SetMLNetModelPath(
                Path.Combine(MLNetModelPath, "MLModel2.zip")
            );
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current != null && HttpContext.Current.Application != null)
            {
                bool? InitializedIdentityDb =
                    HttpContext.Current.Application["InitializedIdentityDb"] as bool?;

                if (!InitializedIdentityDb.HasValue || !InitializedIdentityDb.Value)
                    CreateBuiltInUsersAndRoles();
            }
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
        }

        protected void Application_PostAcquireRequestState(object sender, EventArgs e)
        {            
        }

        protected void CreateBuiltInUsersAndRoles()
        {
            var roleAccountApiUser = new ApplicationRole()
            {
                Id = new Guid("CD28DE48-583A-43DD-A0F9-308A4C5C8220").ToString(),
                Name = "AccountApiUser"
            };

            var roleValuesApiUser = new ApplicationRole()
            {
                Id = new Guid("F6AD6CBA-CD19-4803-B245-0F7AFEE60EC0").ToString(),
                Name = "ValuesApiUser"
            };

            List<ApplicationRole> builtInRoles = new List<ApplicationRole>();
            builtInRoles.Add(roleAccountApiUser);
            builtInRoles.Add(roleValuesApiUser);

            CreateBuiltInRoles(builtInRoles.ToArray());

            var emailAccountApiUser = "AccountApiUser@example.com".ToLower();
            var emailValuesApiUser = "ValuesApiUser@example.com".ToLower();

            var UserManager =
                HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

            CreateBuiltInUser(UserManager, emailAccountApiUser, "P@ssword1", roleAccountApiUser);
            CreateBuiltInUser(UserManager, emailValuesApiUser, "P@ssword1", roleValuesApiUser);

            HttpContext.Current.Application["InitializedIdentityDb"] = true;
        }

        protected void CreateBuiltInRoles(params ApplicationRole[] roles)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                bool changed = false;

                foreach (var role in roles)
                {
                    if (!dbContext.Roles.Any(x => x.Id == role.Id))
                    {
                        dbContext.Roles.Add(role);
                        changed = true;
                    }
                }

                if (changed)
                {
                    dbContext.SaveChanges();
                    changed = false;
                }
            }
        }

        protected void CreateBuiltInUser(ApplicationUserManager UserManager, string userEmail, string userPassword, ApplicationRole role)
        {
            string email = userEmail.ToLowerInvariant();

            ApplicationUser newUser = UserManager.FindByEmail(email);
            if (newUser == null)
            {
                newUser = new ApplicationUser()
                {
                    UserName = email,
                    Email = email,
                };

                IdentityResult CreateUserResult = UserManager.Create(newUser, userPassword);

                if (CreateUserResult.Succeeded)
                {
                    if (role != null)
                    {
                        var user = UserManager.FindByEmail(email);

                        if (user != null)
                        {
                            if (!UserManager.IsInRole(user.Id, role.Name))
                            {
                                IdentityResult AddUserToRoleResult = UserManager.AddToRole(user.Id, role.Name);
                            }
                        }
                    }
                }
            }
        }
    }
}
