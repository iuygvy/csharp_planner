namespace CsharpPlanner;

public class SortByDate : ITaskSortStrategy
{
    public IEnumerable<Task> Sort(IEnumerable<Task> tasks)
    {
        return tasks.OrderBy(t => t.DueDate);
    }
}