﻿namespace Mix.Portal.Domain.Models
{
    public class DashboardModel
    {
        public int TotalPage { get; set; }

        public int TotalPost { get; set; }

        public int TotalProduct { get; set; }

        public int TotalModule { get; set; }

        public int TotalUser { get; set; }

        public DashboardModel(string culture, MixCmsContext context)
        {
            TotalPage = context.MixPageContent.Count(p => p.Specificulture == culture);
            TotalPost = context.MixPostContent.Count(p => p.Specificulture == culture);
        }
    }
}