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
    public IAdmin IAdmin;


    public int UserId { get; set; }

    public userController(IUser usersService, IAdmin adminService, IHttpContextAccessor httpContextAccessor)
    {
        this.IUser = usersService;
        this.IAdmin = adminService;
        UserId = int.Parse(httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value);
    }


    [HttpGet]
    [Authorize]
    public ActionResult<User> GetUser()
    {
        return IUser.GetMyUser(UserId);
    }


    [HttpGet]
    [Authorize(Policy = "Admin")]
    [Route("[action]")]
    public ActionResult<List<User>> GetAllUsers()
    {
        return IAdmin.GetAllUsers();
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    [Route("[action]")]
    public ActionResult<int> AddUser([FromBody] User newUser)
    {
        return IAdmin.AddUser(newUser);
    }

    [HttpPut]
    [Authorize]
    [Route("[action]/{id}")]
    public ActionResult<bool> UpdateUser(int id, [FromBody] User updatedUser)
    {
        return IUser.UpdateUser(id, updatedUser);
    }

    [HttpDelete]
    [Authorize(Policy = "Admin")]
    [Route("[action]/{id}")]
    public ActionResult<bool> DeleteUser(int id)
    {
        return IAdmin.DeleteUser(id);
    }

}

