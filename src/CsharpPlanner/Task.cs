namespace CsharpPlanner;

public abstract class Task
{
    private static int _nextId = 0;
    
    public int Id { get; }
    public string Title { get; protected set; }
    public DateTime DueDate { get; protected set; }
    public TaskStatus Status { get; protected set; }
    public int Priority { get; protected set; }
    public CompositeTask? Parent{ get; protected set; }

    protected Task(string title, DateTime dueDate, int priority, CompositeTask? parent = null)
    {
        Id = _nextId++;
        Title = title;
        DueDate = dueDate;
        Status = TaskStatus.Todo;
        Priority = priority;
        Parent = parent;
    }
    
    public virtual IEnumerable<Task> GetChildren()
    {
        return Enumerable.Empty<Task>();
    }

    public abstract void Complete();
}