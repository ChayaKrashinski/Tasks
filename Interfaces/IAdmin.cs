using todoList.Models;
namespace todoList.Interfaces;

public interface IAdmin
{
    List<User> GetAllUsers();
    int AddUser(User user);
    bool DeleteUser(int id);
}