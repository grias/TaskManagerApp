using DataAccess.ConnectionFactories;
using Microsoft.Extensions.Configuration;
using Domain.Models;
using Domain.Services;
using DataAccess.Repositories;


namespace TaskManager;

internal class Program
{
    static void Main(string[] args)
    {
        //string connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection")!;
        string connectionString = new ConfigurationBuilder().AddUserSecrets<Program>().Build().GetConnectionString("DefaultConnection")!;

        var sqlConnection = new SqlServerConnectionFactory(connectionString);
        var taskRepository = new TasksRepository(sqlConnection);
        var taskService = new TaskService(taskRepository);

        Console.WriteLine("\nInitial state:");
        DisplayTasks(taskService.GetAllTasks());

        var taskToAdd = new TaskModel() { Title = "Groceries", Description = "Buy milk, eggs and beer" };
        var newTask = taskService.AddTask(taskToAdd);
        Console.WriteLine("\nAdding new task:");
        DisplayTasks(taskService.GetAllTasks());

        taskService.TryCompleteTaskById(newTask.Id);
        Console.WriteLine("\nCompleting it:");
        DisplayTasks(taskService.GetAllTasks());

        taskService.TryDeleteTaskById(newTask.Id);
        Console.WriteLine("\nDeleting it:");
        DisplayTasks(taskService.GetAllTasks());
    }

    private static void DisplayTasks(List<TaskModel> tasks)
    {
        foreach (var task in tasks)
        {
            string isCompletedEmoji = task.IsCompleted ? _isCompletedTrueSymbol : _isCompletedFalseSymbol;
            Console.WriteLine($"{task.Id}) {task.Title}: {task.Description} ({isCompletedEmoji})");
        }
        Console.WriteLine($"Tasks total: {tasks.ToArray().Length}");
    }

    private static readonly string _isCompletedTrueSymbol = "V";
    private static readonly string _isCompletedFalseSymbol = "X";
}
