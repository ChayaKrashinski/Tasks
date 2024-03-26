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
    public User User = null;
    
    public loginController(IUser IUser)
    {
        this.IUser = IUser;
    }

    [HttpPost]
    [Route("[action]")]
    public ActionResult<String> Login(string Password, string name)
    {
        if (IUser.findMe(Password)==null)
        {
            return Unauthorized();
        }

        User = IUser.findMe(Password);

        var claims=new List<Claim>{new Claim ("id", User.Id.ToString())};
        if(User.IsAdmin)
            claims.Add(new Claim("type", "Admin"));
        else
            claims.Add(new Claim("type", "User"));

        var token = TokenServise.GetToken(claims);

        return new OkObjectResult(TokenServise.WriteToken(token));
    
    }

    [HttpGet]
    [Route("[action]")]
    [Authorize(Policy="User")]
    public ActionResult<List<task>> Get()
    {
        return IUser.GetAllTasks();
    }

}