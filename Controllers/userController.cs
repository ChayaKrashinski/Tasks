using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using todoList.Models;
using todoList.Controllers;
using System;
using Microsoft.AspNetCore.Authorization;
using todoList.Services;
using System.Security.Claims;
using todoList.Interfaces;


namespace todoList.Controllers;

[ApiController]
[Route("[controller]")]
public class userController : ControllerBase
{
    public IUser IUser;
    public int UserId{get;set;}

    public userController(IUser usersService,IHttpContextAccessor httpContextAccessor)
    {
        this.IUser = usersService;
        UserId = int.Parse(httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value);
    }
    

    [HttpGet]
    [Authorize]
    public ActionResult<User> GetUser()//here need send from body the user
    {   
        return IUser.GetMyUser(UserId);
    }


    // [Authorize(Policy = "User")]
    // [HttpGet]
    // [Route("[action]")]
    // public ActionResult<String>                                       ()
    // {
    //     return new OkObjectResult("---");
    //     // $"Public Files Accessed by {userName}"
    // }


    // [HttpGet]
    // [Route("[action]")]
    // [Authorize(Policy="User")]
    // public ActionResult<List<task>> Get()
    // {
    //     return IUser.GetAllTasks();
    // }

    // [HttpGet]
    // [Route("[action]")]
    // [Authorize(Policy("Admin"))]
    // public ActionResult<User> getMyProfile()//here need send from body the user
    // {   
    //     return IUser.GetMyUser(id);
    // }


    // [Authorize(Policy = "User")]
    // [HttpGet]
    // [Route("[action]")]
    // public ActionResult<String> AccessPublicFiles()
    // {
    //     return new OkObjectResult("---");
    //     // $"Public Files Accessed by {userName}"
}

