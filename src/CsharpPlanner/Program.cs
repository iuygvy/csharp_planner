namespace CsharpPlanner;

class Program
{
    static void Main()
    {
        var facade = new TaskFacade(new TaskManager());
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== C# PLANNER ===");
            ShowTasksMenu(facade);
            ShowMenu();

            Console.Write("Wybierz opcję: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTaskMenu(facade);
                    break;

                case "2":
                    RemoveTaskMenu(facade);
                    break;

                case "3":
                    CompleteTaskMenu(facade);
                    break;
                
                case "4":
                    ShowCompositeTasksMenu(facade);
                    break;
                    
                case "5":
                    ChangeSortMenu(facade);
                    break;

                case "0":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Nieprawidłowa opcja");
                    Pause();
                    break;
            }
        }
    }

    // ---------------- MENU ----------------

    static void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("1. Dodaj zadanie");
        Console.WriteLine("2. Usuń zadanie");
        Console.WriteLine("3. Zakończ zadanie");
        Console.WriteLine("4. Zarządzaj zadaniami złożonymi");
        Console.WriteLine("5. Zmień sortowanie");
        Console.WriteLine("0. Wyjście");
        Console.WriteLine();
    }
    
    static void AddTaskMenu(TaskFacade facade, CompositeTask? parent = null)
    {
        Console.Write("Tytuł: ");
        string title = Console.ReadLine();

        Console.Write("Termin (yyyy-mm-dd): ");
        DateTime dueDate = DateTime.Parse(Console.ReadLine());

        Console.Write("Priorytet (1–5): ");
        int priority = int.Parse(Console.ReadLine());

        Console.WriteLine("Typ zadania:");
        Console.WriteLine("1. Simple");
        Console.WriteLine("2. Composite");

        string type = Console.ReadLine();
        facade.SetTaskCreator(type == "2"
            ? new CompositeTaskCreator()
            : new SimpleTaskCreator());

        var task = facade.AddTask(title, dueDate, priority, parent);

        Console.WriteLine($"Dodano zadanie: {task.Title}");
        Pause();
    }

    static void RemoveTaskMenu(TaskFacade facade)
    {
        ShowTasks(facade);

        Console.Write("\nPodaj ID zadania do usunięcia: ");
        if (Int32.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine(
                facade.RemoveTask(id)
                    ? "Zadanie usunięte"
                    : "Nie znaleziono zadania"
            );
        }
        else
        {
            Console.WriteLine("Nieprawidłowy GUID");
        }

        Pause();
    }

    static void CompleteTaskMenu(TaskFacade facade)
    {
        ShowTasks(facade);

        Console.Write("\nPodaj ID zadania do zakończenia: ");
        if (Int32.TryParse(Console.ReadLine(), out int id))
        {
            var task = facade.ShowTasks().FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.Complete();
                Console.WriteLine("Zadanie zakończone");
            }
            else
            {
                Console.WriteLine("Nie znaleziono zadania");
            }
        }
        else
        {
            Console.WriteLine("Nieprawidłowe ID");
        }

        Pause();
    }

    static void ShowTasksMenu(TaskFacade facade)
    {
        ShowTasks(facade);
    }

    static void ShowCompositeTasksMenu(TaskFacade facade)
    {
        ShowCompositeTasks(facade);
        
        Console.Write("\nPodaj ID zadania do rozwinięcia: ");
        
        if (Int32.TryParse(Console.ReadLine(), out int id))
        {
            var task = facade.ShowTasks().OfType<CompositeTask>().FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                ShowCompositeTaskMenu(task, facade);
            }
            else
            {
                Console.WriteLine("Nie znaleziono zadania");
            }
        }
        else
        {
            Console.WriteLine("Nieprawidłowe ID");
        }
    }

    static void ShowCompositeTaskMenu(Task task, TaskFacade facade)
    {
        Console.Clear();
        
        ExpandCompositeTask(task, facade);
        
        Console.WriteLine("\nPodaj ID zadania, którym chcesz zarządzać: ");
        Console.WriteLine("(Zadania złożone są oznaczone znakiem (+))");
        
        if (Int32.TryParse(Console.ReadLine(), out int id))
        {
            var task2 = facade.ShowTasks().FirstOrDefault(t => t.Id == id);
            if (task2 != null)
            {
                ShowTaskMenu(task2, facade);
            }
            else
            {
                Console.WriteLine("Nie znaleziono zadania");
            }
        }
        else
        {
            Console.WriteLine("Nieprawidłowe ID");
        }
    }

    static void ShowTaskMenu(Task task, TaskFacade facade)
    {
        Console.Clear();
        
        Console.WriteLine("1. Usuń zadanie");
        Console.WriteLine("2. Zakończ zadanie");
        if (task.GetType() == typeof(CompositeTask))
        {
            Console.WriteLine("3. Dodaj podzadanie");
        }
        Console.WriteLine("0. Wyjście");
        Console.WriteLine();
        
        Console.Write("Wybierz opcję: ");
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1" :
                facade.RemoveTask(task.Id);
                break;
            case "2" :
                task.Complete();
                break;
            case "3" :
                AddTaskMenu(facade, (CompositeTask)task);
                break;
            case "0" :
                break;
            default:
                Console.WriteLine("Nieprawidłowa opcja");
                Pause();
                break;
        }
    }

    static void ChangeSortMenu(TaskFacade facade)
    {
        Console.WriteLine("Sortowanie:");
        Console.WriteLine("1. Po dacie");
        Console.WriteLine("2. Po priorytecie");
        Console.WriteLine("3. Po nazwie");
        
        bool chosen = false;
        while (!chosen)
        {
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": 
                {
                    facade.SetSortStrategy(new SortByDate());
                    chosen = true;
                    break;
                }
                case "2":
                {
                    facade.SetSortStrategy(new SortByPriority());
                    chosen = true;
                    break;
                }
                case "3":
                {
                    facade.SetSortStrategy(new SortByTitle());
                    chosen = true;
                    break;
                }
                default:
                {
                    Console.WriteLine("Nieprawidłowa opcja");
                    Pause();
                    break;
                }
            }
        }

        Console.WriteLine("Zmieniono sortowanie");
        Pause();
    }

    // ---------------- POMOCNICZE ----------------

    static void ShowTasks(TaskFacade facade)
    {
        Console.WriteLine("\n--- Zadania ---");
        if (!facade.ShowTasks().Any())
        {
            Console.WriteLine("Aktualnie nie masz żadnych zadań");
            return;
        }
        foreach (var task in facade.ShowTasks().Where(t => t.Parent == null))
        {
            Console.WriteLine($"  {task.Id}.  {task.Title} | {task.Status} | {task.DueDate:d} | P:{task.Priority}");
        }
    }
    
    static void ShowCompositeTasks(TaskFacade facade)
    {
        Console.Clear();
        Console.WriteLine("\n--- Zadania złożone ---");
        if (!facade.ShowTasks().OfType<CompositeTask>().Any())
        {
            Console.WriteLine("Aktualnie nie masz żadnych zadań złożonych");
            return;
        }
        foreach (var task in facade.ShowTasks().OfType<CompositeTask>().Where(t => t.Parent == null))
        {
            Console.WriteLine($"  {task.Id}.  {task.Title} | {task.Status} | {task.DueDate:d} | P:{task.Priority}");
        }
    }

    static void ExpandCompositeTask(Task task, TaskFacade facade, int level = 0)
    {
        for (int i = 0; i < level; i++)
        {
            Console.Write("  ");
        }
        Console.Write($"  {task.Id}.  {task.Title} | {task.Status} | {task.DueDate:d} | P:{task.Priority}");
        if(task.GetType() == typeof(CompositeTask)) Console.Write("  (+)");
        Console.WriteLine();
        foreach (var child in facade.ShowTasks().Where(t => t.Parent == task))
        {
            ExpandCompositeTask(child, facade, level + 1);
        }
    }

    static void Pause()
    {
        Console.WriteLine("\nNaciśnij ENTER...");
        Console.ReadLine();
    }
}
