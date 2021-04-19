﻿namespace Framework.Web
{
    using Microsoft.AspNetCore.Http;

    public static class AppContext
        {
            private static IHttpContextAccessor _httpContextAccessor;

            public static void Configure(IHttpContextAccessor httpContextAccessor)
            {
                _httpContextAccessor = httpContextAccessor;
            }

            public static HttpContext Current => _httpContextAccessor.HttpContext;
        }
    
}