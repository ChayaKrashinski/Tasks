using todoList.Models;
namespace todoList.Interfaces;

public interface ITaskService
{
    List<task> GetAll();

    task GetById(int id);

    int Add(task newtask);
 
    bool Update(int id, task newtask);

    bool Delete(int id);
}