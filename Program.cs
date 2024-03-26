using todoList.Services;
using todoList.Interfaces;
using todoList.Middlewares;
using todoList.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using System.Security.Claims;



var builder = WebApplication.CreateBuilder(args);

// var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
// var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    ).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.TokenValidationParameters = TokenServise.GetTokenValidationParameters();
        }
    );
//auth2
builder.Services.AddAuthorization(cfg =>
    {
        cfg.AddPolicy("Admin", policy => policy.RequireClaim("type", "Admin"));
        cfg.AddPolicy("User", policy => policy.RequireClaim("type", "User"));
    });

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TasksList", Version = "v1" });
    //auth3
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    //auth4
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                { new OpenApiSecurityScheme
                        {
                         Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                        },
                    new string[] {

                    }
                }
    });
});




// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//  .AddJwtBearer(options =>
//  {
//      options.TokenValidationParameters = new TokenValidationParameters
//      {

//          ValidateIssuer = true,
//          ValidateAudience = true,
//          ValidateLifetime = true,
//          ValidateIssuerSigningKey = true,
//          ValidIssuer = jwtIssuer,
//          ValidAudience = jwtIssuer,
//          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
//      };
//  });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTask();
builder.Services.AddUser();
// builder.Services.AddAdmin();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseLogMiddleware("file.log");
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.MapControllers();
app.Run();
