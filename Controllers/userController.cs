using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using todoList.Models;
using todoList.Interfaces;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace todoList.Controllers;

[ApiController]
[Route("todo")]
[Authorize(Policy = "User")]
public class userController : ControllerBase
{
    IUser Iuser;
    public userController(IUser user)
    {
        this.Iuser = user;
    } 

    [HttpGet("{id}")]


    [HttpGet]
    [Route("[action]")]
    public ActionResult<String> AccessPublicFiles()
    {
        return new OkObjectResult("---");
        // $"Public Files Accessed by {userName}"
    }

}

