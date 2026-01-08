namespace CsharpPlanner;

public abstract class Task
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Title { get; protected set; }
    public DateTime DueDate { get; protected set; }
    public TaskStatus Status { get; protected set; }
    public int Priority { get; protected set; }

    protected Task(string title, DateTime dueDate, int priority)
    {
        Title = title;
        DueDate = dueDate;
        Status = TaskStatus.Todo;
        Priority = priority;
    }
    
    public virtual IEnumerable<Task> GetChildren()
    {
        return Enumerable.Empty<Task>();
    }

    public abstract void Complete();
}