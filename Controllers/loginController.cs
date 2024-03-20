using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using todoList.Models;
using todoList.Interfaces;
using System;
using Microsoft.AspNetCore.Authorization;
using todoList.Services;
using todoList.Models;
using System.Security.Claims;

namespace todoList.Controllers;

[ApiController]
[Route("todo")]
public class loginController : ControllerBase
{
    IUser IUser;
    public User User;
    public loginController(IUser IUser)
    {
        this.IUser = IUser;
    }

    [HttpPost]
    [Route("[action]")]
    public ActionResult<String> Login([FromBody] string Password)
    {
        if (IUser.findMe(Password)==null)
        {
            return Unauthorized();
        }

        User = IUser.findMe(Password);

        var claims = User.IsAdmin?new List<Claim>
            {
                new Claim("type", "Admin"),
            }:new List<Claim>
            {
                new Claim("type", "User"),
            };

        var token = TokenServise.GetToken(claims);

        return new OkObjectResult(TokenServise.WriteToken(token));
    
    }


// [Authorize(Policy = "Admin")]

}