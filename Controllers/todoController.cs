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
    public ITask ITask;
    public int UserId { get; set; }

    public todoController(IUser usersService, ITask tasksService, IHttpContextAccessor httpContextAccessor)
    {
        this.IUser = usersService;
        this.ITask = tasksService;
        UserId = int.Parse(httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value);
    }

    [HttpGet]
    [Authorize]
    [Route("tasksList")]
    public ActionResult<List<task>> GetMyTasksList()
    {
        return ITask.GetTasksById(UserId);
    }

    [HttpGet]
    [Authorize(Policy = "Admin")]
    [Route("tasksList/{id}")]
    public ActionResult<List<task>> GetUserTasksList(int id)
    {
        return ITask.GetTasksById(id);
    }

    [HttpGet]
    [Authorize(Policy = "Admin")]
    public ActionResult<List<task>> GetAllTasksList()
    {
        return ITask.GetAllTasks();
    }

    [HttpPost]
    [Authorize]
    [Route("[action]")]
    public ActionResult<int> AddTask([FromBody] task newTask)
    {
        return ITask.AddTask(UserId, newTask);
    }

    [HttpPut]
    [Authorize]
    [Route("[action]/{id}")]
    public ActionResult<bool> UpdateTask(int id,[FromBody] task newTask)
    {
        return ITask.UpdateTask(UserId, id, newTask);
    }

    [HttpDelete]
    [Authorize]
    [Route("[action]/{id}")]
    public ActionResult<bool> DeleteTask(int id)
    {
        return ITask.DeleteTask(UserId, id);
    }

}