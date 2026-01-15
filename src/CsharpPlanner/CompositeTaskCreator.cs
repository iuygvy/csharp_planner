namespace CsharpPlanner;

public class CompositeTaskCreator : TaskCreator
{
    public override Task CreateTask(string title, DateTime dueDate, int priority, CompositeTask? parent)
    {
        return new CompositeTask(title, dueDate, priority, parent);
    }
}