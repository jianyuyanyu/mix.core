﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Mix.Database.Entities.Cms;
using Mix.Heart.Repository;
using Mix.Heart.UnitOfWork;
using Mix.Lib.Attributes;
using Mix.Lib.Services;
using Mix.Shared.Constants;
using Mix.Shared.Services;

namespace Mix.Lib.Base
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [MixAuthorize]
    public abstract class MixAuthorizedApiControllerBase : Controller
    {
        protected string _lang;
        protected MixCulture _culture;
        protected UnitOfWorkInfo _uow;
        protected readonly ILogger<MixApiControllerBase> _logger;
        protected readonly MixIdentityService _mixIdentityService;
        protected readonly MixService _mixService;
        protected readonly TranslatorService _translator;
        protected readonly EntityRepository<MixCmsContext, MixCulture, int> _cultureRepository;
        public MixAuthorizedApiControllerBase(
            ILogger<MixApiControllerBase> logger,
            MixService mixService,
            TranslatorService translator,
            EntityRepository<MixCmsContext, MixCulture, int> cultureRepository, 
            MixIdentityService mixIdentityService,
            MixCmsContext context
            ) : base()
        {
            _uow = new UnitOfWorkInfo(context);
            _logger = logger;
            _mixService = mixService;
            _translator = translator;
            _cultureRepository = cultureRepository;
            _mixIdentityService = mixIdentityService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (!GlobalConfigService.Instance.AppSettings.IsInit)
            {
                _lang = RouteData?.Values["lang"] != null
                    ? RouteData.Values["lang"].ToString()
                    : GlobalConfigService.Instance.AppSettings.DefaultCulture;
                _culture = _cultureRepository.GetFirst(c => c.Specificulture == _lang);
            }
        }
    }
}
