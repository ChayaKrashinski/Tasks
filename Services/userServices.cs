using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using todoList.Services;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using todoList.Interfaces;
using todoList.Models;

// using System.Security.Cryptography.X509Certificates;

namespace todoList.Services;

public class userServices : IUser
{
    public List<User> users { get; } = new List<User>();
    private string fileName = "users.json";

    public User findMe(string password, string name)
    {
        foreach (var user in users)
            {
                if(user.Password.Equals(password))
                    if(user.UserName.Equals(name))
                        return user;
            }
        return null;
    }

    // todoListServices tasksListService;
    public userServices(/*ITasksListService tasksListService*/)
    {
        // this.tasksListService = tasksListService;
        this.fileName = Path.Combine( "Data", "users.json");
        using (var jsonFile = File.OpenText(fileName))
        {
            users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        
        }
    }


    private void saveToFile()
    {
        File.WriteAllText(fileName, JsonSerializer.Serialize(users));
    }

    public List<task> GetAllTasks()
    {
        List<task> lt = new List<task>();
        foreach (var user in users)
        {
            foreach (var task in user.TasksList)
            {
                lt.Add(task);
            }
        }
        return lt;
    }
    
    public task getTaskById(int userId, int taskId){
        task currentTask = null;
        foreach (var user in users)
        {
            if (user.Id == userId)
            {
                foreach (task t in user.TasksList)
                {
                    if(t.Id==taskId){
                        currentTask = t;
                    }
                }
            }
        }
        return currentTask;
    }

    public List<task> GetTasksById(int id)
    {
        try
        {
            List<task> lt = new List<task>();
            foreach (User user in users)
            {
                if (user.Id == id)
                    foreach (task task in user.TasksList)
                    {
                        lt.Add(task);
                    }
            }
            return lt;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public int AddTask(int id, task newTask)
    {
        try
        {
            foreach (var user in users)
            {
                if (user.Id == id)
                {
                    newTask.Id = user.TasksList.Max(t => t.Id) + 1;
                    user.TasksList.Add(newTask);
                    saveToFile();
                    return newTask.Id;
                }
            }
            return -1;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public bool UpdateTask(int id, task updetedTask)
    {
        try
        {
            foreach (var user in users)
            {
                if (user.Id == id)
                {
                    foreach (task t in user.TasksList)
                    {
                        if(t.Id==updetedTask.Id){
                            t.Name = updetedTask.Name;
                            t.IsDone = updetedTask.IsDone;
                            saveToFile();
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public bool DeleteTask(int userId, int taskId)
    {
        try
        {
            for (int i = 0; i < users.Count; i++)
            {
                User user = users[i];
                if(user.Id == userId)
                {
                    List<task> tasks = user.TasksList;
                    for (int k = 0; k < tasks.Count; k++)
                    {
                        if(tasks[k].Id==taskId)
                        {
                            tasks.RemoveAt(k);
                            saveToFile();
                            return true;
                        }
                    }
                }
            }
            return false;
            
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    public User GetMyUser(int id)
    {
        try
        {
            return users.Find(user=>user.Id==id);
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}

public static class UserUtils
{
    public static void AddUser(this IServiceCollection services)
    {
        services.AddSingleton<IUser, userServices>();
    }
}

// public LoginController(IUsersService usersService, IHttpContextAccessor httpContextAccessor)
//     {
//         this.usersService = usersService;
//     }
