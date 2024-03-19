using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using todoList.Interfaces;
using todoList.Models;

// using System.Security.Cryptography.X509Certificates;

namespace todoList.Services;

public class userServices : IUser
{
    List<User> users { get; } = new List<User>();
    private string fileName = "users.json";

    public userServices()
    {
        this.fileName = Path.Combine("Data", "users.json");

        using (var jsonFile = File.OpenText(fileName))
        {
            // #pragma warning disable CS8601 // Possible null reference assignment.
            users = JsonSerializer.Deserialize<List<User>>(
                jsonFile.ReadToEnd(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
            // #pragma warning restore CS8601 // Possible null reference assignment.
        }
    }

    private void saveToFile()
    {
        File.WriteAllText(fileName, JsonSerializer.Serialize(users));
    }

    public List<task> GetAllTasks()
    {
        try
        {
            List<task> lt = new List<task>();
            foreach (var user in users)
            {
                foreach (var task in user.tasksList)
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
    
    task getTaskById(int userId, int taskId){
        return users.find(u=>u.Id==userId).tasksList.find(t=>t.Id==taskId);
    }

    public List<task> GetTasksById(int id)
    {
        try
        {
            List<task> lt = new List<task>();
            foreach (User user in users)
            {
                if (user.Id == id)
                    foreach (task task in user.tasksList)
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
                    newTask.Id = user.tasksList.Max(t => t.Id) + 1;
                    user.tasksList.Add(newTask);
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
                    foreach (task t in user.tasksList)
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
                    List<task> tasks = user.tasksList;
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
