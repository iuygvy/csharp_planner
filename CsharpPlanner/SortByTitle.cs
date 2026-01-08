namespace CsharpPlanner;

public class SortByTitle : ITaskSortStrategy
{
    public IEnumerable<Task> Sort(IEnumerable<Task> tasks)
    {
        return tasks.OrderBy(t => t.Title);
    }
}