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
public class todoController : ControllerBase
{
    public IUser IUser;
    public int UserId { get; set; }

    public todoController(IUser usersService, IHttpContextAccessor httpContextAccessor)
    {
        this.IUser = usersService;
        UserId = int.Parse(httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value);
    }

    [HttpGet]
    [Authorize]
    [Route("tasksList")]
    public ActionResult<List<task>> GetMyTasksList()
    {
        return IUser.GetTasksById(UserId);
    }

    [HttpGet]
    [Authorize(Policy = "Admin")]
    [Route("tasksList/{id}")]
    public ActionResult<List<task>> GetMyTasksList(int id)
    {
        return IUser.GetTasksById(id);
    }

    [HttpGet]
    [Authorize(Policy = "Admin")]
    public ActionResult<List<task>> GetAllTasksList()
    {
        return IUser.GetAllTasks();
    }

    [HttpPost]
    [Authorize]
    [Route("[action]")]
    public ActionResult<int> AddNewTask(task newTask)
    {
        return IUser.AddTask(UserId, newTask);
    }

    [HttpPut]
    [Authorize]
    [Route("[action]/{id}")]
    public ActionResult<bool> UpdateTask(int id, task newTask)
    {
        return IUser.UpdateTask(UserId, id, newTask);
    }

    [HttpDelete]
    [Authorize]
    [Route("[action]/{id}")]
    public ActionResult<bool> DeleteTask(int id)
    {
        return IUser.DeleteTask(UserId, id);
    }



}