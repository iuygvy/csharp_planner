namespace CsharpPlanner;

public class SortByPriority : ITaskSortStrategy
{
    public IEnumerable<Task> Sort(IEnumerable<Task> tasks)
    {
        return tasks.OrderByDescending(t => t.Priority);
    }
}