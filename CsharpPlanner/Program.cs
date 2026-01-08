namespace CsharpPlanner;

class Program
{
    static void Main()
    {
        var facade = new TaskFacade(
            new TaskManager(),
            new SimpleTaskCreator()
        );

        var task1 = facade.AddTask("Zrobić UML", DateTime.Now.AddDays(1), 2);
        var task2 = facade.AddTask("Napisać prezentację", DateTime.Now.AddDays(2), 1);

        facade.SetTaskCreator(new CompositeTaskCreator());
        var project = facade.AddTask("Projekt C#", DateTime.Now.AddDays(7), 3);
        ((CompositeTask)project).AddSubTask(task1);
        ((CompositeTask)project).AddSubTask(task2);

        facade.SetSortStrategy(new SortByDate());

        Console.WriteLine("\nPo dacie:");
        foreach (var task in facade.ShowTasks())
        {
            Console.WriteLine($"{task.Title} | {task.Status} | {task.DueDate:d}");
        }
        
        facade.SetSortStrategy(new SortByPriority());
        
        Console.WriteLine("\nPo priorytecie:");
        foreach (var task in facade.ShowTasks())
        {
            Console.WriteLine($"{task.Title} | {task.Status} | {task.DueDate:d}");
        }
        
        facade.SetSortStrategy(new SortByTitle());
        
        Console.WriteLine("\nPo Tytule:");
        foreach (var task in facade.ShowTasks())
        {
            Console.WriteLine($"{task.Title} | {task.Status} | {task.DueDate:d}");
        }
        
        project.Complete();

        Console.WriteLine("\nPo zakończeniu projektu:");
        foreach (var task in facade.ShowTasks())
        {
            Console.WriteLine($"{task.Title} | {task.Status}");
        }

        facade.RemoveTask(project.Id);
        foreach (var task in facade.ShowTasks())
        {
            Console.WriteLine($"{task.Title} | {task.Status}");
        }
    }
}