using Application.Interfaces;
using Application.Models.Requests;
using Domain.IRepositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class CustomAuthenticationService : ICustomAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthenticationServiceOptions _options;

        public CustomAuthenticationService(IUserRepository userRepository, IOptions<AuthenticationServiceOptions> options)
        {
            _userRepository = userRepository;
            _options = options.Value;
        }

        public async Task<string> AuthenticateAsync(UserLoginRequest authenticationRequest)
        {
            var user = await _userRepository.GetUserByEmailOrUsername(authenticationRequest.UserNameOrEmail);

            if (user == null || user.Password != authenticationRequest.Password)
            {
                return null;
            }

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new Claim("sub", user.UserId.ToString()),
                new Claim("userName", user.UserName),
                new Claim(ClaimTypes.Role, user.UserType)
            };



            var jwtSecurityToken = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claimsForToken,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
        public class AuthenticationServiceOptions
        {
            public const string AuthenticationService = "AuthenticationService";
            public string Issuer { get; set; }
            public string Audience { get; set; }
            public string SecretForKey { get; set; }
        }

    }

}