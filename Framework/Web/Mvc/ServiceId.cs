﻿using System;

namespace Framework.Web
{
    public class ServiceId : IServiceId
    {
        private static readonly string UniqueId = $"{Guid.NewGuid():N}";

        public string Id => UniqueId;
    }
}
