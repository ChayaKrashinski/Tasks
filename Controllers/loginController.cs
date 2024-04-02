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
    public ActionResult<String> Login([FromBody] String Password, [FromBody] String name)
    {
        if (IUser.findMe(Password, name) == null)
        {
            return BadRequest();
        }

        User User = IUser.findMe(Password, name);

        var claims = new List<Claim> { new Claim("id", User.Id.ToString()) };
        if (User.IsAdmin)
            claims.Add(new Claim("type", "Admin"));
        else
            claims.Add(new Claim("type", "User"));

        var token = TokenServise.GetToken(claims);

        return new OkObjectResult(TokenServise.WriteToken(token));

    }
}