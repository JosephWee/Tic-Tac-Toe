using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using WebApp.Models;

namespace WebApp.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
               OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
                CookieAuthenticationDefaults.AuthenticationType);

            AuthenticationProperties properties = CreateProperties(user.UserName);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }

        public override Task ValidateTokenRequest(OAuthValidateTokenRequestContext context)
        {
            return base.ValidateTokenRequest(context);
        }

        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            if (context.TokenEndpointRequest != null)
            {
                System.Net.IPAddress remoteIpAddress = System.Net.IPAddress.Parse(context.Request.RemoteIpAddress);
                System.Net.IPAddress remoteIpAddressV4 = remoteIpAddress.MapToIPv4();

                string RemoteIpAddress = context.Request.RemoteIpAddress;
                string RemoteIpAddressV4 = string.Empty;
                if (remoteIpAddressV4 != null)
                {
                    RemoteIpAddressV4 = remoteIpAddressV4.ToString();
                }

                string GrantType = context.TokenEndpointRequest.GrantType;

                string UserName = String.Empty;
                if (context.TokenEndpointRequest.IsResourceOwnerPasswordCredentialsGrantType)
                {
                    UserName = context.TokenEndpointRequest.Parameters["UserName"];
                }

                string signedAccessTokenHash = AccessTokenHelper.HashAndSign(context.AccessToken);

                TokenRequest tokenRequest = new TokenRequest()
                {
                    GrantType = GrantType,
                    RemoteIpAddress = RemoteIpAddress,
                    RemoteIpAddressV4 = RemoteIpAddressV4,
                    UserName = UserName,
                    SignedAccessTokenHash = signedAccessTokenHash,
                    RequestDate = DateTime.UtcNow
                };

                using (AccessTokenDbContext dbContext = new AccessTokenDbContext())
                {
                    dbContext.TokenRequests.Add(tokenRequest);
                    dbContext.SaveChanges();
                }
            }

            return base.TokenEndpointResponse(context);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            return base.GrantRefreshToken(context);
        }
    }
}