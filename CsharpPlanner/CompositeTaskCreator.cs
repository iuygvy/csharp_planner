namespace CsharpPlanner;

public class CompositeTaskCreator : TaskCreator
{
    public override Task CreateTask(string title, DateTime dueDate, int priority)
    {
        return new CompositeTask(title, dueDate, priority);
    }
}
