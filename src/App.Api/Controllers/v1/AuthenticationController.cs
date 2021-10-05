using App.Api.Controllers.Base;
using App.Api.Extensions;
using App.Api.Identity;
using App.Api.ViewModels.Authentication;
using App.Domain.Notifications.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace App.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v:{version:apiVersion}/[controller]")]
    public class AuthenticationController : MainController
    {
        private readonly INotifier _notifier;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TokenSettings _tokenSettings;

        public AuthenticationController(INotifier notifier,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<TokenSettings> tokenSettings) : base(notifier)
        {
            _notifier = notifier;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenSettings = tokenSettings.Value;
        }

        [HttpPost("create-account")]
        public async Task<ActionResult> CreateAccount(RegisterUserViewModel registerUserViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUserViewModel.Email,
                Email = registerUserViewModel.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUserViewModel.Password);

            if (result.Succeeded)
            {
                return CustomResponse(await GenerateJwt(registerUserViewModel.Email));
            }

            foreach (var error in result.Errors)
            {
                NotifyError(error.Description);
            }

            return CustomResponse(registerUserViewModel);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUserViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginUserViewModel.Email, loginUserViewModel.Password, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await GenerateJwt(loginUserViewModel.Email));
            }

            if (result.IsLockedOut)
            {
                NotifyError("O usuário foi temporariamente bloqueado por tentativas inválidas de login");

                return CustomResponse(loginUserViewModel);
            }

            NotifyError("Usuário ou senha inválidos");

            return CustomResponse(loginUserViewModel);
        }

        private async Task<LoginResponseViewModel> GenerateJwt(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var identityClaims = await GetUserClaimsAndRoles(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenSettings.Issuer,
                Audience = _tokenSettings.Audience,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_tokenSettings.HoursToExpire),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return new LoginResponseViewModel
            {
                AccessToken = tokenHandler.WriteToken(token),
                ExpiresIn = TimeSpan.FromHours(_tokenSettings.HoursToExpire).TotalSeconds,
                User = new UserTokenViewModel
                {
                    Id = user.Id,
                    Name = user.UserName,
                    Email = user.Email
                }
            };
        }

        private async Task<ClaimsIdentity> GetUserClaimsAndRoles(IdentityUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.UtcNow.ToUnixEpochDate().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();

            identityClaims.AddClaims(claims);

            return identityClaims;
        }
    }
}
