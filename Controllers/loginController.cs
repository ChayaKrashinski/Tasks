using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using todoList.Models;
using todoList.Interfaces;
using System;
using Microsoft.AspNetCore.Authorization;
using todoList.Services;
using System.Security.Claims;


namespace todoList.Controllers;

[ApiController]
[Route("todo")]
public class loginController : ControllerBase
{
    public IUser IUser;

    public loginController(IUser IUser, IHttpContextAccessor httpContextAccessor)
    {
        this.IUser = IUser;
    }

    [HttpPost]
    [Route("[action]")]
    public ActionResult<String> Login([FromBody] User User)
    {
        string Password = User.Password;
        string UserName = User.UserName;

        if (IUser.findMe(Password, UserName) == null)
        {
            return BadRequest();
        }

        User user = IUser.findMe(Password, UserName);

        var claims = new List<Claim> { new Claim("id", user.Id.ToString()) };
        if (User.IsAdmin)
            claims.Add(new Claim("type", "Admin"));
        else
            claims.Add(new Claim("type", "User"));

        var token = TokenServise.GetToken(claims);

        return new OkObjectResult(TokenServise.WriteToken(token));

    }
}