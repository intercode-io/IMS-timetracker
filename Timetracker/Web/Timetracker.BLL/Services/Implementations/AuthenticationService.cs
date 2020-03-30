using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Timetracker.BLL.Configuration;
using Timetracker.BLL.Constants;
using Timetracker.BLL.Exceptions;
using Timetracker.BLL.Mappers.Interfaces;
using Timetracker.BLL.Services.Interfaces;
using Timetracker.Entities.Data;
using Timetracker.Models.Data;

namespace Timetracker.BLL.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;

        private readonly AppSettings _appSettings;
        private readonly JwtSettings _jwtSettings;

        private readonly IMapper<UserEntity, UserModel> _userMapper;

        public AuthenticationService(
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            IOptions<AppSettings> appSettings,
            IOptions<JwtSettings> jwtSettings,
            IMapper<UserEntity, UserModel> userMapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;

            _appSettings = appSettings.Value;
            _jwtSettings = jwtSettings.Value;

            _userMapper = userMapper;
        }

        public async Task<UserModel> HandleGoogleLogin(string googleIdToken)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(googleIdToken, new GoogleJsonWebSignature.ValidationSettings());

            if (payload.HostedDomain != _appSettings.AllowedGoogleEmailDomain)
            {
                throw new UnsupportedDomainException("The domain provided for authentication is not supported");
            }

            var signInResult = await _signInManager.ExternalLoginSignInAsync(ExternalProviders.GOOGLE, payload.Subject, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(payload.Email);

                var userModel = _userMapper.Map(user);
                userModel.Token = GenerateJwtToken(userModel);

                return userModel;
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(payload.Email);

                if (user == null)
                {
                    user = new UserEntity
                    {
                        UserName = payload.Email,
                        Email = payload.Email,
                        FirstName = payload.GivenName,
                        LastName = payload.FamilyName,
                        PhotoUrl = payload.Picture,
                    };

                    await _userManager.CreateAsync(user);
                    await _userManager.AddToRoleAsync(user, "User");

                    user = await _userManager.FindByEmailAsync(user.Email);
                }

                await _userManager.AddLoginAsync(user, new UserLoginInfo(ExternalProviders.GOOGLE, payload.Subject, ExternalProviders.GOOGLE));
                await _signInManager.SignInAsync(user, isPersistent: false);

                var userModel = _userMapper.Map(user);
                userModel.Token = GenerateJwtToken(userModel);

                return userModel;
            }
        }

        public async Task HandleLogout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<UserModel> GetCurrentUser(ClaimsPrincipal user)
        {
            var userEntity = await _userManager.GetUserAsync(user);

            return _userMapper.Map(userEntity);
        }

        private string GenerateJwtToken(UserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_jwtSettings.ExpireDays);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
