using Microsoft.AspNetCore.Mvc;
using Task.Models;
using Task.Services;

namespace Tasks.TasksControllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Task>> Get()
    {
        return TaskService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Task> Get(int id)
    {
        var Task = TaskService.GetById(id);
        if (Task == null)
            return NotFound();
        return Task;
    }

    [HttpPost]
    public ActionResult Post(Task newTask)
    {
        var newId = TaskService.Add(newTask);

        return CreatedAtAction("Post", 
            new {id = newId}, TaskService.GetById(newId));
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id,Task newTask)
    {
        var result = TaskService.Update(id, newTask);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }
}
