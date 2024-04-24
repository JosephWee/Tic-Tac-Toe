using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.CompilerServices;

namespace TicTacToe.Cache
{
    public static class Extensions
    {
        static string _appInstanceId;
        public static string _AppInstanceId
        {
            get
            {
                if (string.IsNullOrEmpty(_appInstanceId))
                {
                    string appName = System.AppDomain.CurrentDomain.FriendlyName + "|" + System.Environment.MachineName;
                    _appInstanceId = appName.GetHash_SHA256();
                }

                return _appInstanceId;
            }
        }

        public static string GetHash_SHA256(this string str)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] bytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(str));

                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string AppInstanceId(this Microsoft.Extensions.Caching.Distributed.IDistributedCache distCache)
        {
            return _AppInstanceId;
        }

        public static void SetCache(this Microsoft.Extensions.Caching.Distributed.IDistributedCache distCache, string key, string data, Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions options)
        {
            string actualKey = distCache.AppInstanceId() + key;
            byte[] encodedData = System.Text.Encoding.UTF8.GetBytes(data);
            distCache.Set(actualKey, encodedData, options);
        }

        public static string GetCache(this Microsoft.Extensions.Caching.Distributed.IDistributedCache distCache, string key)
        {
            string actualKey = distCache.AppInstanceId() + key;
            byte[] bytes = distCache.Get(actualKey);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}
