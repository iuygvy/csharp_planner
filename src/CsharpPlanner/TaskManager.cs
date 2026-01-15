namespace CsharpPlanner;

public class TaskManager
{
    private readonly List<Task> _tasks = new();

    public void AddTask(Task task)
    {
        _tasks.Add(task);
    }
    
    public bool RemoveTask(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
            return false;

        RemoveRecursively(task);        
        return true;
    }
    
    private void RemoveRecursively(Task task)
    {
        foreach (var child in task.GetChildren())
        {
            RemoveRecursively(child);
        }

        _tasks.Remove(task);
    }
    
    public IEnumerable<Task> GetAllTasks()
    {
        return _tasks;
    }
}