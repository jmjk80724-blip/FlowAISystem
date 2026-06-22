using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using FlowAISystem.Core.Exceptions;
using FlowAISystem.Core.Services.Interfaces;
using FlowAISystem.Data;
using System.Security.Claims;
using FlowAISystem.Data.DTOs.Auth;
using FlowAISystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FlowAISystem.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService( AppDbContext context, IConfiguration configuration)
        {
            _context   = context;
            _configuration = configuration;
        }
    public async Task<TokenResponseDto> LoginAsync(LoginDto dto) 
        {
            // 1. Find User
            var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync( u => u.Username == dto.Username);

            if(user==null) 
            throw new UnauthorizedException("Invalid username or password");

            // 2. Verify Password
            bool isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if(!isValid) 
            throw new UnauthorizedException("Invalid username or pasword");

            // 3. Generate Token JWT
            var token = GenerateJwtToken(user.Username, user.Role.RoleName);

            return new TokenResponseDto
            {
                Token = token,
                Username = user.Username,
                Role = user.Role.RoleName,
                Expiration = DateTime.Now.AddHours(2)
            };
        }
    public Task LogoutAsync()
        {
            // Blazor Sever - Token clear in side
            return Task.CompletedTask;
        }
        // Helper
    private string GenerateJwtToken(string username, string role)
{
    var claims = new[]
    {
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.Role, role)
    };
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: _configuration["Jwt:Issuer"],
        audience: _configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.Now.AddHours(2),
        signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}
    }
    
}