﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shop.Query.Users._DTOs;

namespace Common.Api.Jwt;

public class JwtTokenBuilder
{
    public static DateTime JwtTokenExpirationDate = DateTime.Now.AddMinutes(5);
    public static DateTime RefreshTokenExpirationDate = DateTime.Now.AddDays(30);

    public static string BuildToken(UserDto userDto, IConfiguration configuration)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.MobilePhone, userDto.PhoneNumber.Value),
            new(ClaimTypes.NameIdentifier, userDto.Id.ToString())
        };

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:SignInKey"]));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JwtConfig:Issuer"],
            audience: configuration["JwtConfig:Audience"],
            claims: claims,
            expires: JwtTokenExpirationDate,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}