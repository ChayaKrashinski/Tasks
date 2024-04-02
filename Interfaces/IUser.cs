using todoList.Models;
namespace todoList.Interfaces;

public interface IUser
{
    List<task> GetAllTasks();
    List<task> GetTasksById(int id);
    task getTaskById(int userId, int taskId);
    int AddTask(int id, task newTask);
    bool UpdateTask(int id, task updetedTask);
    bool DeleteTask(int userId, int taskId);
    User GetMyUser(int id);
    User findMe(string password, string name);
}