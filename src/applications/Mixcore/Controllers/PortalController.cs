using Microsoft.AspNetCore.Mvc;
using Mix.Database.Services;
using Mix.Lib.Services;
using Mix.Shared.Services;
using System.Text.RegularExpressions;

namespace Mixcore.Controllers
{
    public class PortalController : MixControllerBase
    {
        private readonly DatabaseService _databaseService;

        public PortalController(
            IHttpContextAccessor httpContextAccessor,
            MixService mixService,
            DatabaseService databaseService, IPSecurityConfigService ipSecurityConfigService,
            MixCacheService cacheService)
            : base(httpContextAccessor, mixService, ipSecurityConfigService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        [Route("admin/{appFolder?}/{param1?}/{param2?}/{param3?}/{param4?}")]
        public IActionResult Index()
        {
            if (isValid)
            {
                return View();
            }
            else
            {
                return Redirect(_redirectUrl);
            }
        }

        [Route("portal/{appFolder?}/{param1?}/{param2?}/{param3?}/{param4?}")]
        [Route("portal-apps/{appFolder?}/{param1?}/{param2?}/{param3?}/{param4?}")]
        public IActionResult Spa(string appFolder, string param1, string param2, string param3, string param4)
        {
            string folder = $"portal-apps/{appFolder}";
            var baseHref = Request.Query["baseHref"].ToString();
            if (string.IsNullOrEmpty(baseHref))
            {
                string subPath = string.Join(
                "/",
                Request.RouteValues
                .Where(m => m.Key.Contains("param"))
                .Select(m => m.Value).ToArray());
                appFolder ??= "mix-portal";
                
                string url = $"/portal-apps/{appFolder}/{subPath}?baseHref=portal-apps/{appFolder}";
                return Redirect(url);
            }

            var indexFile = MixFileHelper.GetFileByFullName($"{MixFolders.WebRootPath}/{folder}/index.html");
            Regex regex = new("((?<=src=\")|(?<=href=\"))(?!(http[^\\s]+))(.+?)(\\.+?)");

            if (indexFile.Content!= null && regex.IsMatch(indexFile.Content))
            {
                indexFile.Content = regex.Replace(indexFile.Content, $"/{folder}/$3$4");
            }

            return View(indexFile);
            
        }
        #region overrides

        protected override void ValidateRequest()
        {
            // If IP retricted in appsettings
            if (ForbiddenPortal)
            {
                isValid = false;
                _redirectUrl = "/403";
            }

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

        #endregion overrides
    }
}
