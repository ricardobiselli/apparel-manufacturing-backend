using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;

    private readonly IPasswordService _passwordService;

    private readonly IConfiguration _configuration;

    public AuthenticationService(
        IUserRepository userRepository,
        IPasswordService passwordService,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _configuration = configuration;
    }

    public async Task<AuthenticationResponseDTO?> Login(UserLoginRequestDTO request)
    {
        var user = await _userRepository
            .GetByEmployeeNumberAsync(request.EmployeeNumber);

        if (user == null)
            return null;

        if (user.State != EntitiesState.Active)
            return null;

        bool validPassword =
            _passwordService.VerifyPassword(
                request.Password,
                user.PasswordHash);

        if (!validPassword)
            return null;

        string token = GenerateToken(user);

        return new AuthenticationResponseDTO
        {
            Token = token,

            UserId = user.UserId,

            FirstName = user.FirstName,

            LastName = user.LastName,

            Role = user.Role.ToString(),

            MustChangePassword = user.MustChangePassword
        };
    }

    private string GenerateToken(User user)
    {
        var key =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["AuthenticationService:SecretForKey"]!));

        var credentials =
            new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
    {

        new Claim(
            JwtRegisteredClaimNames.Sub,
            user.UserId.ToString()),

        new Claim(
            "employeeIdNumber",
            user.EmployeeIdNumber),

        new Claim(
            ClaimTypes.Name,
            $"{user.FirstName} {user.LastName}"),

        new Claim(
            ClaimTypes.Role,
            user.Role.ToString())
    };

        var token =
            new JwtSecurityToken(
                issuer: _configuration["AuthenticationService:Issuer"],

                audience: _configuration["AuthenticationService:Audience"],

                claims: claims,

                expires: DateTime.UtcNow.AddHours(8),

                signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}
