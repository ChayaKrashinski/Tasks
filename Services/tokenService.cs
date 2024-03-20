using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace todoList.Services;

public static class TokenServise
{
    private static SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asdfghjkl;';lkjhgfd7825450"));
    private static string issuer = "https://localhost:7268";
    public static SecurityToken GetToken(List<Claim> claims) =>
        new JwtSecurityToken(
            issuer,
            issuer,
            claims,
            expires: DateTime.Now.AddDays(5.0),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );
    public static TokenValidationParameters GetTokenValidationParameters() =>
        new TokenValidationParameters
        {
            ValidIssuer = issuer,
            ValidAudience = issuer,
            IssuerSigningKey = key,
            ClockSkew = TimeSpan.Zero 
        };
        
    public static string WriteToken(SecurityToken token) =>
        new JwtSecurityTokenHandler().WriteToken(token);
        
}
