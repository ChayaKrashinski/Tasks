namespace todoList.Models;

public class task
{
    public task(int id, string name, bool isDone){Id=id;Name=name;IsDone=isDone;}
    
    public int Id { get; set;}
    public string Name { get; set;}
    public bool IsDone {get; set;}
}