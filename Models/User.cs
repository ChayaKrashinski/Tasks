using todoList.Models;
using System.Collections.Generic;

namespace todoList.Models;

public class User
{
    public string Password {get; set;}
    public bool IsAdmin{get; set;}
    public int Id{get; set;}
    public string UserName{get; set;}
    public List<task> tasksList{get; set;}
}