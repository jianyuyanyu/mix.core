﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Mix.Communicator.Services;
using Mix.Database.Entities.Account;
using Mix.Heart.Models;
using Mix.Identity.Domain.Models;
using Mix.Identity.Dtos;
using Mix.Identity.Models;
using Mix.Identity.Models.AccountViewModels;
using Mix.Lib.Services;

using Mix.Shared.Services;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Mix.Account.Controllers
{
    [Route("api/v2/rest/mix-account/user")]
    [ApiController]
    public class MixUserController : Controller
    {
        private readonly TenantUserManager _userManager;
        private readonly SignInManager<MixUser> _signInManager;
        private readonly RoleManager<MixRole> _roleManager;
        private readonly ILogger<MixUserController> _logger;
        private readonly MixIdentityService _idService;
        private readonly EmailService _emailService;
        private readonly EntityRepository<MixCmsAccountContext, MixUser, Guid> _repository;
        protected readonly MixIdentityService _mixIdentityService;
        private readonly MixDataService _mixDataService;
        protected UnitOfWorkInfo _accUOW;
        protected UnitOfWorkInfo _cmsUOW;
        private readonly MixCmsAccountContext _accContext;
        private readonly MixCmsContext _cmsContext;
        private readonly EntityRepository<MixCmsAccountContext, RefreshTokens, Guid> _refreshTokenRepo;

        protected int MixTenantId { get; set; }
        public MixUserController(
            IHttpContextAccessor httpContextAccessor,
            TenantUserManager userManager,
            SignInManager<MixUser> signInManager,
            RoleManager<MixRole> roleManager,
            ILogger<MixUserController> logger,
            MixIdentityService idService, EntityRepository<MixCmsAccountContext, RefreshTokens, Guid> refreshTokenRepo,
            MixCmsAccountContext accContext, MixIdentityService mixIdentityService, MixCmsContext cmsContext, MixDataService mixDataService, EmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _idService = idService;
            _refreshTokenRepo = refreshTokenRepo;
            _mixIdentityService = mixIdentityService;
            _accUOW = new(accContext);
            _cmsUOW = new(cmsContext);
            _repository = new(_accUOW);
            _cmsContext = cmsContext;
            _accContext = accContext;

            if (httpContextAccessor.HttpContext.Session.GetInt32(MixRequestQueryKeywords.MixTenantId).HasValue)
            {
                MixTenantId = httpContextAccessor.HttpContext.Session.GetInt32(MixRequestQueryKeywords.MixTenantId).Value;
            }
            _mixDataService = mixDataService;
            _emailService = emailService;
        }

        #region Overrides

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (_accUOW.ActiveTransaction != null)
            {
                _accUOW.Complete();
            }

            if (_cmsUOW.ActiveTransaction != null)
            {
                _cmsUOW.Complete();
            }

            base.OnActionExecuted(context);
        }

        #endregion


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin")]
        [Route("my-tenants")]
        [HttpGet]
        public async Task<ActionResult> MyTenants()
        {
            var userId = Guid.Parse(_idService.GetClaim(User, MixClaims.Id));
            var tenantIds = await _accContext.MixUserTenants.Where(m => m.MixUserId == userId).Select(m => m.TenantId).ToListAsync();
            var tenants = await MixTenantSystemViewModel.GetRepository(_cmsUOW).GetListAsync(m => tenantIds.Contains(m.Id));
            return Ok(tenants);
        }

        [Route("register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] RegisterViewModel model)
        {
            var result = await _idService.Register(model, MixTenantId, _cmsUOW);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("Logout")]
        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync().ConfigureAwait(false);
            await _refreshTokenRepo.DeleteAsync(r => r.Username == User.Identity.Name);
            return Ok();
        }

        [Route("token")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> GetToken([FromBody] LoginDto requestDto)
        {
            string key = GlobalConfigService.Instance.AppSettings.ApiEncryptKey;
            string decryptMsg = AesEncryptionHelper.DecryptString(requestDto.Message, key);
            var model = JsonConvert.DeserializeObject<GetTokenModel>(decryptMsg);
            var loginResult = await _idService.GetToken(model);
            if (loginResult != null)
            {
                return Ok(loginResult);
            }
            return Unauthorized();
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] LoginDto requestDto)
        {
            string key = GlobalConfigService.Instance.AppSettings.ApiEncryptKey;
            string decryptMsg = AesEncryptionHelper.DecryptString(requestDto.Message, key);
            var model = JsonConvert.DeserializeObject<LoginViewModel>(decryptMsg);
            var loginResult = await _idService.Login(model);
            return Ok(loginResult);
        }

        [Route("external-login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLogin([FromBody] LoginDto requestDto)
        {
            string key = GlobalConfigService.Instance.AppSettings.ApiEncryptKey;
            string decryptMsg = AesEncryptionHelper.DecryptString(requestDto.Message, key);
            var model = JsonConvert.DeserializeObject<RegisterExternalBindingModel>(decryptMsg);
            var loginResult = await _idService.ExternalLogin(model);
            return Ok(loginResult);
        }

        [Route("get-external-login-providers")]
        [HttpGet]
        public async Task<ActionResult> GetExternalLoginProviders()
        {
            var providers = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return Ok(providers);
        }

        [Route("renew-token")]
        [HttpPost]
        public async Task<ActionResult> RenewToken([FromBody] RenewTokenDto refreshTokenDto)
        {
            var token = await _idService.RenewTokenAsync(refreshTokenDto);
            return Ok(token);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("my-profile")]
        public async Task<ActionResult<MixUserViewModel>> MyProfile()
        {
            var id = _idService.GetClaim(User, MixClaims.Id);
            var user = await _userManager.FindByIdAsync(id); ;

            if (user != null)
            {
                var result = new MixUserViewModel(user, _cmsUOW);
                await result.LoadUserDataAsync(MixTenantId, _mixDataService);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Owner")]
        [Route("details/{id}")]
        public async Task<ActionResult> Details(string id = null)
        {
            MixUser user = await _userManager.FindByIdAsync(id); ;

            if (user != null)
            {
                var result = new MixUserViewModel(user, _cmsUOW);
                await result.LoadUserDataAsync(MixTenantId, _mixDataService);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Owner")]
        [Route("remove-user/{id}")]
        public async Task<ActionResult> Remove(string id)
        {
            MixUser user = await _userManager.FindByIdAsync(id); ;
            if (user != null)
            {
                var logins = await _userManager.GetLoginsAsync(user);
                foreach (var login in logins)
                {
                    var result = await _userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
                    if (!result.Succeeded)
                    {
                        Console.WriteLine(result.Errors);
                    }
                }
                var idRresult = await _userManager.DeleteAsync(user);
                if (idRresult.Succeeded)
                {

                    return Ok();
                }
            }
            throw new MixException(MixErrorStatus.NotFound);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Owner")]
        [Route("user-in-role")]
        public async Task<ActionResult> Details(UserRoleModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            List<string> errors = new List<string>();

            if (role == null)
            {
                errors.Add($"Role: {model.RoleId} does not exists");
            }
            else if (model.IsUserInRole)
            {
                var appUser = await _userManager.FindByIdAsync(model.UserId);

                if (appUser == null)
                {
                    errors.Add($"User: {model.UserId} does not exists");
                }
                else if (!await (_userManager.IsInRoleAsync(appUser, role.Name)))
                {
                    await _userManager.AddToRoleAsync(appUser, role.Name, MixTenantId);
                    return Ok();
                }
            }
            else
            {
                var appUser = await _userManager.FindByIdAsync(model.UserId);

                if (appUser == null)
                {
                    errors.Add($"User: {model.UserId} does not exists");
                }

                var removeResult = _userManager.RemoveFromRoleAsync(appUser, role.Name, MixTenantId);
                return Ok();
            }
            return BadRequest(errors);
        }

        [MixAuthorize(roles: "SuperAdmin, Owner")]
        [HttpGet("list")]
        public virtual async Task<ActionResult<PagingResponseModel<MixUser>>> Get([FromQuery] SearchRequestDto request)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(user);
            Expression<Func<MixUser, bool>> predicate = model =>
                (string.IsNullOrWhiteSpace(request.Keyword)
                || (
                    (EF.Functions.Like(model.UserName, $"%{request.Keyword}%"))
                   || (EF.Functions.Like(model.Email, $"%{request.Keyword}%"))
                   )
                )
                && (!request.FromDate.HasValue
                    || (model.CreatedDateTime >= request.FromDate.Value.ToUniversalTime())
                )
                && (!request.ToDate.HasValue
                    || (model.CreatedDateTime <= request.ToDate.Value.ToUniversalTime())
                );
            var searchQuery = new SearchAccountQueryModel(request);
            var data = await _repository.GetPagingEntitiesAsync(predicate, searchQuery.PagingData)
               .ConfigureAwait(false);
            return data;
        }

        // POST api/template
        [MixAuthorize(roles: $"{MixRoles.SuperAdmin}, {MixRoles.Owner}")]
        [HttpPost]
        [Route("save")]
        public async Task<ActionResult<MixUserViewModel>> Save(
            [FromBody] MixUserViewModel model)
        {
            if (model != null)
            {
                var user = await _userManager.FindByIdAsync(model.Id.ToString());
                user.Email = model.Email;
                var updInfo = await _userManager.UpdateAsync(user);
                //var saveData = await model.UserData.SaveAsync();
                if (model.IsChangePassword)
                {
                    var changePwd = await _userManager.ChangePasswordAsync(
                        user,
                        model.ChangePassword.CurrentPassword,
                        model.ChangePassword.NewPassword);
                    if (!changePwd.Succeeded)
                    {
                        throw new MixException(string.Join(",", changePwd.Errors));
                    }
                    else
                    {
                        // Remove other token if change password success
                        if (Guid.TryParse(_idService.GetClaim(User, MixClaims.RefreshToken), out var refreshTokenId))
                        {
                            await _refreshTokenRepo.DeleteManyAsync(
                                m => m.Username == user.UserName && m.Id != refreshTokenId);
                        }
                    }
                }
                return Ok(model);
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("forgot-password")]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                throw new MixException(MixErrorStatus.Badrequest, "Invalid Email");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                throw new MixException(MixErrorStatus.Badrequest, "Email Not Exist");
            }

            //if (!await _userManager.IsEmailConfirmedAsync(user))
            //    result.Data = "Invalid Email";

            var confrimationCode =
                    await _userManager.GeneratePasswordResetTokenAsync(user);

            var callbackurl = $"{Request.Scheme}://{Request.Host}/security/reset-password/?token={System.Web.HttpUtility.UrlEncode(confrimationCode)}";
            var edmTemplate = await MixTemplateViewModel.GetRepository(_cmsUOW).GetSingleAsync(
                m => m.FolderType == MixTemplateFolderType.Edms && m.FileName == "ForgotPassword");
            string content = callbackurl;
            if (edmTemplate != null)
            {
                content = edmTemplate.Content.Replace("[URL]", callbackurl);
            }
            _emailService.SendMail(
                    to: user.Email,
                    subject: "Reset Password",
                    message: content);

            return Ok();
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                throw new MixException(MixErrorStatus.Badrequest, "Invalid User");
            }
            string code = System.Web.HttpUtility.UrlDecode(model.Code).Replace(' ', '+');
            var idRresult = await _userManager.ResetPasswordAsync(
                                        user, model.Code, model.Password);
            if (!idRresult.Succeeded)
            {
                var errors = idRresult.Errors.Select(m => m.Description);
                throw new MixException(MixErrorStatus.Badrequest, errors);
            }

            return Ok();
        }
        #region Helpers



        #endregion
    }
}
