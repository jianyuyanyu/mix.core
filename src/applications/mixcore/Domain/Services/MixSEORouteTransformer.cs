﻿using Microsoft.AspNetCore.Mvc.Routing;
using Mix.Database.Services.MixGlobalSettings;
using Mix.Lib.Extensions;

namespace Mixcore.Domain.Services
{
    // Ref: https://www.strathweb.com/2019/08/dynamic-controller-routing-in-asp-net-core-3-0/
    public sealed class MixSEORouteTransformer : DynamicRouteValueTransformer
    {
        private IConfiguration _configuration;
        private readonly IMixTenantService _tenantService;
        public MixSEORouteTransformer(IMixTenantService tenantService, IConfiguration configuration)
        {
            _tenantService = tenantService;
            _configuration = configuration;
        }

        public override ValueTask<RouteValueDictionary> TransformAsync(
            HttpContext httpContext, RouteValueDictionary values)
        {
            if (_configuration.IsInit())
            {
                return ValueTask.FromResult(values);
            }

            RouteValueDictionary result = values;

            var keys = values.Keys.ToArray();

            var language = (string)values[keys[0]];
            string seoName = string.Empty;
            if (_tenantService.AllCultures != null && _tenantService.AllCultures.Any(m => m.Specificulture == language))
            {
                seoName = string.Join('/', values.Values.Skip(1));
            }
            else
            {
                language = string.Empty;
                seoName = string.Join('/', values.Values);
            }
            result["controller"] = "home";
            result["culture"] = language;
            result["action"] = "Index";
            result["seoName"] = seoName.TrimStart('/');

            return ValueTask.FromResult(result);
        }
    }
}
