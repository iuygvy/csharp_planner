namespace CsharpPlanner;

public interface ITaskSortStrategy
{
    IEnumerable<Task> Sort(IEnumerable<Task> tasks);
}