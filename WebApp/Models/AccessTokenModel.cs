using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class TokenRequest
    {
        public int Id { get; set; }
        public string RemoteIpAddress { get; set; }
        public string RemoteIpAddressV4 { get; set; }
        public string GrantType { get; set; }
        public string UserName { get; set; }
        public string SignedAccessTokenHash { get; set; }
        public DateTime RequestDate { get; set; }
    }

    public class AccessTokenDbContext : DbContext
    {
        public AccessTokenDbContext() : base("IdentityDbConnString")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<TokenRequest> TokenRequests { get; set; }
    }
}