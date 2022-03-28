﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, bool isDevelop)
        {
            //app.UseDeveloperExceptionPage();
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        throw new MixException(MixErrorStatus.ServerError, contextFeature.Error);
                    }
                });
            });
        }
    }
}
