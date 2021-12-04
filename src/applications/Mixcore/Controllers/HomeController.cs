﻿using Microsoft.AspNetCore.Mvc;
using Mix.Lib.Services;
using Mix.Shared.Services;
using Mix.Database.Services;
using Mixcore.Domain.Bases;

namespace Mixcore.Controllers
{
    public class HomeController : MvcBaseController
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(
            ILogger<HomeController> logger,
            IPSecurityConfigService ipSecurityConfigService,
            MixService mixService,
            TranslatorService translator, 
            MixDatabaseService databaseService,
            MixCmsContext context,
            MixCacheService cacheService) 
            : base(ipSecurityConfigService, mixService, translator, databaseService, context, cacheService)
        {
            _logger = logger;
        }
        
        protected override void ValidateRequest()
        {
            base.ValidateRequest();

            // If this site has not been inited yet
            if (GlobalConfigService.Instance.AppSettings.IsInit)
            {
                isValid = false;
                if (string.IsNullOrEmpty(_databaseService.GetConnectionString(MixConstants.CONST_CMS_CONNECTION)))
                {
                    _redirectUrl = "Init";
                }
                else
                {
                    var status = GlobalConfigService.Instance.AppSettings.InitStatus;
                    _redirectUrl = $"/init/step{status}";
                }
            }
        }

        [Route("")]
        public async Task<IActionResult> Index(string seoName, string keyword)
        {
            if (!isValid)
            {
                return Redirect(_redirectUrl);
            }
            return await Home();
        }

        protected async Task<IActionResult> Home()
        {
            // Home Page
            var pageRepo = PageContentViewModel.GetRepository(_uow);
            var page = await pageRepo.GetFirstAsync(p=>p.Type == MixPageType.Home, _cacheService);
            ViewData["Title"] = page.SeoTitle;
            ViewData["Description"] = page.SeoDescription;
            ViewData["Keywords"] = page.SeoKeywords;
            ViewData["Image"] = page.Image;
            ViewData["Layout"] = page.Layout.FilePath;
            ViewData["BodyClass"] = page.ClassName;
            ViewData["ViewMode"] = MixMvcViewMode.Page;
            ViewData["Keyword"] = page.SeoKeywords;

            ViewBag.viewMode = MixMvcViewMode.Page;
            return View(page);
        }
    }
}
