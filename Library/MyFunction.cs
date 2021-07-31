using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;

namespace Library
{
    public static class MyFunction
    {
        public static string GetUserIP(HttpRequest req)
        {
            var ip = req.Headers["X-Forwarded-For"].FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(ip)) ip = ip.Split(',')[0];

            if (string.IsNullOrWhiteSpace(ip)) ip = Convert.ToString(req.HttpContext.Connection.RemoteIpAddress);

            if (string.IsNullOrWhiteSpace(ip)) ip = req.Headers["REMOTE_ADDR"].FirstOrDefault();

            return ip;
        }
        public static IPAddress GetRemoteIPAddress(this HttpContext context, bool allowForwarded = true)
        {
            if (allowForwarded)
            {
                string header = (context.Request.Headers["CF-Connecting-IP"].FirstOrDefault() ?? context.Request.Headers["X-Forwarded-For"].FirstOrDefault());
                if (IPAddress.TryParse(header, out IPAddress ip))
                {
                    return ip;
                }
            }
            return context.Connection.RemoteIpAddress;
        }
    }
}
