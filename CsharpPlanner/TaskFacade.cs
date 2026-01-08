namespace CsharpPlanner;

public class TaskFacade
{
    private readonly TaskManager _taskManager;
    private TaskCreator _creator;
    private ITaskSortStrategy _sortStrategy;

    public TaskFacade(TaskManager taskManager, TaskCreator creator)
    {
        _taskManager = taskManager;
        _creator = creator;
        _sortStrategy = new SortByDate();
    }

    public Task AddTask(string title, DateTime dueDate, int priority)
    {
        var task = _creator.CreateTask(title, dueDate, priority);
        _taskManager.AddTask(task);
        return task;
    }

    public void SetSortStrategy(ITaskSortStrategy strategy)
    {
        _sortStrategy = strategy;
    }

    public void SetTaskCreator(TaskCreator creator)
    {
        _creator = creator;
    }
    
    public bool RemoveTask(Guid id)
    {
        return _taskManager.RemoveTask(id);
    }
    
    public IEnumerable<Task> ShowTasks()
    {
        return _sortStrategy.Sort(_taskManager.GetAllTasks());
    }
}