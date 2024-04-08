using todoList.Models;
namespace todoList.Interfaces;

public interface IUser
{

    User GetMyUser(int id);
    User findMe(string password, string name);
    bool UpdateUser(int userId, User updatedUser);

}