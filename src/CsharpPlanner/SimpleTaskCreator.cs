namespace CsharpPlanner;

public class SimpleTaskCreator : TaskCreator
{
    public override Task CreateTask(string title, DateTime dueDate, int priority, CompositeTask? parent)
    {
        return new SimpleTask(title, dueDate, priority, parent);
    }
}