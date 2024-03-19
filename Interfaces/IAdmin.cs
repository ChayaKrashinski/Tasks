using todoList.Models;
namespace todoList.Interfaces;

public interface IAdmin
{
    // List<task> GetAllTasks();
    // List<task> GetById(id);
    // int AddTask(task newTask);
    // bool UpdateTask(int id, task newtask);
    // bool DeleteTask(int id);
    // User GetMyUser();

    List<User> GetAllUsers();
    int AddUser(User user);
    bool DeleteUser(int id);
}