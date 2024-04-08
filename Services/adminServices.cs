using todoList.Models;
using todoList.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System;
using System.Text.Json;
namespace todoList.Services;

public class adminService : IAdmin
{
    List<User> users { get; } = new List<User>();
    private string fileName = "users.json";

    public adminService()
    {
        this.fileName = Path.Combine("Data", "users.json");

        using (var jsonFile = File.OpenText(fileName))
        {
            users = JsonSerializer.Deserialize<List<User>>(
                jsonFile.ReadToEnd(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
        }
    }

    private void saveToFile()
    {
        File.WriteAllText(fileName, JsonSerializer.Serialize(users));
    }

    public List<User> GetAllUsers(){
        return users;
    }

    public int AddUser(User user){
        user.Id = users.Max(u=>u.Id)+1;
        users.Add(user);
        saveToFile();
        return user.Id;
    }

    public bool DeleteUser(int id){
        for (int i = 0; i < users.Count; i++)
            {
                User user = users[i];
                if(user.Id == id)
                {
                    users.RemoveAt(i);
                    saveToFile();
                    return true;
                }
            }
            return false;
    }

}

public static class AdminUtils
{
    public static void AddNewAdmin(this IServiceCollection services)
    {
        services.AddSingleton<IAdmin, adminService>();
    }
}
