namespace CsharpPlanner;

public abstract class TaskCreator
{
    public abstract Task CreateTask(string title, DateTime dueDate, int priority, CompositeTask? parent);
}