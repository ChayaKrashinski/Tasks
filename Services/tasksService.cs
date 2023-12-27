using Task.Modells;
namespace Task.Services;

namespace Task.Services
{
    public static class TasksService
    {
        private static List<Task> Tasks;

    static TasksService()
    {
        Tasks = new List<Task>
        {
            new Task { Id = 1, Name = "go_home", done = false},
            new Task { Id = 2, Name = "do_homework", done = false},
            new Task { Id = 3, Name = "Special", done = true}
        };
    }

    public static List<Task> GetAll() => Tasks;

    public static Task GetById(int id) 
    {
        return Tasks.FirstOrDefault(p => p.Id == id);
    }

    public static int Add(Task newTask)
    {
        if (Tasks.Count == 0)

            {
                newTask.Id = 1;
            }
            else
            {
        newTask.Id =  Tasks.Max(p => p.Id) + 1;

            }

        Tasks.Add(newTask);

        return newTask.Id;
    }
  
    public static bool Update(int id, Task newTask)
    {
        if (id != newTask.Id)
            return false;

        var existingTask = GetById(id);
        if (existingTask == null )
            return false;

        var index = Tasks.IndexOf(existingTask);
        if (index == -1 )
            return false;

        Tasks[index] = newTask;

        return true;
    }  

      
    public static bool Delete(int id)
    {
        var existingTask = GetById(id);
        if (existingTask == null )
            return false;

        var index = Tasks.IndexOf(existingTask);
        if (index == -1 )
            return false;

        Tasks.RemoveAt(index);
        return true;
    }  

    }
}