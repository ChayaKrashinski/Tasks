using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using todoList.Models;
using todoList.Interfaces;

namespace todoList.Controllers;

[ApiController]
[Route("todo")]
public class todoListController : ControllerBase
{
    ITaskService TaskService1;
    public todoListController(ITaskService TaskService1)
    {
        this.TaskService1 = TaskService1;
    }
    [HttpGet]
    public ActionResult<List<task>> Get()
    {
        return TaskService1.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<task> Get(int id)
    {
        var task = TaskService1.GetById(id);
        if (task == null)
            return NotFound();
        return task;
    }

    [HttpPost]
    public ActionResult Post(task newTask)
    {
        var newId = TaskService1.Add(newTask);

        return CreatedAtAction("Post", 
            new {id = newId}, TaskService1.GetById(newId));
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id,task newTask)
    {
        var result = TaskService1.Update(id, newTask);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }
     
}
