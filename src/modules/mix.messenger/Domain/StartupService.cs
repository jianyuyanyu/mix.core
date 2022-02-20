﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mix.SignalR.Constants;
using Mix.SignalR.Hubs;

namespace Mix.Messenger.Domain
{
    public class StartupService
    {
        public void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSignalR()
                  .AddJsonProtocol(options =>
                  {
                      options.PayloadSerializerOptions.PropertyNamingPolicy = null;
                  });
        }

        public void UseApps(IApplicationBuilder app, IConfiguration configuration, bool isDevelop)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<PortalHub>(HubEndpoints.PortalHub);
                endpoints.MapHub<EditFileHub>(HubEndpoints.EditFileHub);
            });
        }
    }
}
