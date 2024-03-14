// using System;
// using System.Collections.Generic;
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.IdentityModel.Tokens;


// namespace todoList.Services;


//     public static class tokenService
//     {
//         private static SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SXkSqsKyNUyvGbnHs7ke2NCq8zQzNLW7mPmHbnZZ"));
//         private static string issuer = "https://localhost:7268";
//         public static SecurityToken GetToken(List<Claim> claims) =>
//             new JwtSecurityToken(
//                 issuer,
//                 issuer,
//                 claims,
//                 expires: DateTime.Now.AddDays(6.0),
//                 signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
//             );

//         public static TokenValidationParameters GetTokenValidationParameters() =>
//             new TokenValidationParameters
//             {
//                 ValidIssuer = issuer,
//                 ValidAudience = issuer,
//                 IssuerSigningKey = key,
//                 ClockSkew = TimeSpan.Zero
//             };

//         new JwtSecurityTokenHandler().WriteToken(token);
//     }
