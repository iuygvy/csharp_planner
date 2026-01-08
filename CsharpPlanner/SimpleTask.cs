namespace CsharpPlanner;

public class SimpleTask : Task
{
    public SimpleTask(string title, DateTime dueDate, int priority)
        : base(title, dueDate,  priority) { }

    public override void Complete()
    {
        Status = TaskStatus.Done;
    }
}