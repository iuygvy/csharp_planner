namespace CsharpPlanner;

public class SimpleTaskCreator : TaskCreator
{
    public override Task CreateTask(string title, DateTime dueDate, int priority)
    {
        return new SimpleTask(title, dueDate, priority);
    }
}