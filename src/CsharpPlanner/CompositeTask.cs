using System.Globalization;

namespace CsharpPlanner;

public class CompositeTask : Task
{
    private readonly List<Task> _children = new();

    public CompositeTask(string title, DateTime dueDate, int priority, CompositeTask? parent)
        : base(title, dueDate, priority, parent) { }

    public void AddSubTask(Task task)
    {
        _children.Add(task);
    }

    public override void Complete()
    {
        foreach (var task in _children)
        {
            task.Complete();
        }
        Status = TaskStatus.Done;
    }
    
    public override IEnumerable<Task> GetChildren()
    {
        return _children;
    }
}