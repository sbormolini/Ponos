namespace Bos.Todo.Web.Models;

// todo: same model between api and frontend > shared lib?
public class TodoItem
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsComplete { get; set; }
}