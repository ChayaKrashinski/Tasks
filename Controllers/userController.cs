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
[Route("todo/user")]
public class userController : ControllerBase
{
    IUser IUser;
    public userController(IUser Iuser)
    {
        this.IUser = Iuser;
    }



    // [HttpGet("{id}")]
    // [Route("[action]")]
    // [Authorize(Policy = "User")]
    // public ActionResult<User> getUserById()
    // {   if(user==null)
    //     return IUser.GetMyUser(user.Id);
    //     return null;
    // }


    // [Authorize(Policy = "User")]
    // [HttpGet]
    // [Route("[action]")]
    // public ActionResult<String> AccessPublicFiles()
    // {
    //     return new OkObjectResult("---");
    //     // $"Public Files Accessed by {userName}"
    // }

}

