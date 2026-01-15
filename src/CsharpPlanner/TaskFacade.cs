namespace CsharpPlanner;

public class TaskFacade
{
    private readonly TaskManager _taskManager;
    private TaskCreator _creator;
    private ITaskSortStrategy _sortStrategy;

    public TaskFacade(TaskManager taskManager)
    {
        _taskManager = taskManager;
        _creator = new SimpleTaskCreator();
        _sortStrategy = new SortByDate();
    }

    public Task AddTask(string title, DateTime dueDate, int priority, CompositeTask? parent = null)
    {
        var task = _creator.CreateTask(title, dueDate, priority, parent);
        _taskManager.AddTask(task);
        if (parent != null)
        {
            parent.AddSubTask(task);
        }
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
    
    public bool RemoveTask(int id)
    {
        return _taskManager.RemoveTask(id);
    }
    
    public IEnumerable<Task> ShowTasks()
    {
        return _sortStrategy.Sort(_taskManager.GetAllTasks());
    }
}