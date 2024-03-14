using todoList.Models;
namespace todoList.Interfaces;

public interface IUser
{
    List<task> GetAllTasks();
    List<task> GetById(int id);
    int AddTask(int id, task newTask);
    bool UpdateTask(int id, task updetedTask);
    bool DeleteTask(int userId, int taskId);
    User GetMyUser(int id);
}