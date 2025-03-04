﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Mix.Shared.Models.Configurations;

namespace Mix.Identity.Extensions
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddFacebookIf(
            this AuthenticationBuilder builder,
            bool condition,
            ExternalLogin config,
            string accessDeniedPath)
        {
            return condition ? builder.AddFacebook(options =>
            {
                options.AppId = config.AppId;
                options.AppSecret = config.AppSecret;
                options.AccessDeniedPath = accessDeniedPath;
            }) : builder;
        }

        public static AuthenticationBuilder AddGoogleIf(
            this AuthenticationBuilder builder,
            bool condition,
            ExternalLogin config,
            string accessDeniedPath)
        {
            return condition ? builder.AddGoogle(options =>
            {
                options.ClientId = config.AppId;
                options.ClientSecret = config.AppSecret;
                options.AccessDeniedPath = accessDeniedPath;
            }) : builder;
        }

        public static AuthenticationBuilder AddTwitterIf(
            this AuthenticationBuilder builder,
            bool condition,
            ExternalLogin config,
            string accessDeniedPath)
        {
            return condition ? builder.AddTwitter(options =>
            {
                options.ConsumerKey = config.AppId;
                options.ConsumerSecret = config.AppSecret;
                options.RetrieveUserDetails = true;
                options.AccessDeniedPath = accessDeniedPath;
            }) : builder;
        }

        public static AuthenticationBuilder AddMicrosoftAccountIf(
           this AuthenticationBuilder builder,
           bool condition,
           ExternalLogin config,
           string accessDeniedPath)
        {
            return condition ? builder.AddMicrosoftAccount(options =>
            {
                options.ClientId = config.AppId;
                options.ClientSecret = config.AppSecret;
                options.AccessDeniedPath = accessDeniedPath;
            }) : builder;
        }

        public static void AddMicrosoftIdentityWebApiIf(
           this AuthenticationBuilder builder,
           bool condition,
           IConfiguration configuration)
        {
            if (condition)
            {
                builder.AddMicrosoftIdentityWebApi(configuration, "Authentication:AzureAd");
            }
        }

    }
}
