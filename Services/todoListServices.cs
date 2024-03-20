using todoList.Models;
using todoList.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Text.Json;

namespace todoList.Services;

public class todoListServices : ITaskService
{
    List<task> tasks { get; }
    private string fileName = "tasks.json";

    public todoListServices()
    {
        this.fileName = Path.Combine(/*webHost.ContentRootPath,*/ "Data", "tasks.json");

        using (var jsonFile = File.OpenText(fileName))
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            tasks = JsonSerializer.Deserialize<List<task>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
#pragma warning restore CS8601 // Possible null reference assignment.
        }
    }

    private void saveToFile()
    {
        File.WriteAllText(fileName, JsonSerializer.Serialize(tasks));
    }

    public List<task> GetAll() => tasks;

    public task GetById(int id)
    {

        return tasks.FirstOrDefault(t => t.Id == id);

    }

    public int Add(task newTask)
    {
        if (tasks.Count == 0)

        {
            newTask.Id = 1;
        }
        else
        {
            newTask.Id = tasks.Max(t => t.Id) + 1;

        }

        tasks.Add(newTask);
        saveToFile();
        return newTask.Id;
    }

    public bool Update(int id, task newTask)
    {
        if (id != newTask.Id)
            return false;

        var existingTask = GetById(id);
        if (existingTask == null)
            return false;

        var index = tasks.IndexOf(existingTask);
        if (index == -1)
            return false;

        tasks[index] = newTask;
        saveToFile();
        return true;
    }


    public bool Delete(int id)
    {
        var existingTask = GetById(id);
        if (existingTask == null)
            return false;

        var index = tasks.IndexOf(existingTask);
        if (index == -1)
            return false;

        tasks.RemoveAt(index);
        saveToFile();
        return true;
    }
    public int Count => tasks.Count();


}

public static class TaskUtils
{
    public static void AddTask(this IServiceCollection services)
    {
        services.AddSingleton<ITaskService, todoListServices>();
    }
}
