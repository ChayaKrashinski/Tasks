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

public class adminServices : IAdmin
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

    List<User> GetAllUsers(){
        return users;
    }

    int AddUser(User user){
        user.Id = users.Max(u=>u.Id)+1;
        users.Add(user);
        saveToFile();
        return user.Id;
    }

    bool DeleteUser(int id){
        for (int i = 0; i < users.Count; i++)
            {
                User user = users[i];
                if(user.Id == userId)
                {
                    users.RemoveAt(i);
                    saveToFile();
                    return true;
                }
            }
            return false;
    }

}