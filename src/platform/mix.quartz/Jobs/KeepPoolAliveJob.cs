﻿using Mix.MixQuartz.Models;
using Mix.Shared.Services;
using System;
using System.Threading.Tasks;

namespace Mix.MixQuartz.Jobs
{
    public class KeepPoolAliveJob : MixJobBase
    {
        private readonly HttpService _httpService;
        public KeepPoolAliveJob(HttpService httpService, IServiceProvider provider) 
            : base(provider)
        {
            _httpService = httpService;
            Schedule = new JobSchedule(GetType())
            {
                StartAt = DateTime.Now,
                Interval = 1,
                IntervalType = Enums.MixIntevalType.Second
            };
        }

        public override async Task ExecuteHandler(IJobExecutionContext context)
        {
            var domain = GlobalConfigService.Instance.AppSettings.Domain;
            if (!string.IsNullOrEmpty(domain))
            {
                var now = DateTime.UtcNow;
                var ping = await _httpService.GetAsync<DateTime>($"{domain}/api/v1/rest/shared/ping");
                Console.WriteLine($"Ping at {now}: {(ping - now).TotalMilliseconds}");
            }
            Console.WriteLine(DateTime.UtcNow);
        }
    }
}