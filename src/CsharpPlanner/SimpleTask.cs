namespace CsharpPlanner;

public class SimpleTask : Task
{
    public SimpleTask(string title, DateTime dueDate, int priority,  CompositeTask? parent)
        : base(title, dueDate,  priority, parent) { }

    public override void Complete()
    {
        Status = TaskStatus.Done;
    }
}